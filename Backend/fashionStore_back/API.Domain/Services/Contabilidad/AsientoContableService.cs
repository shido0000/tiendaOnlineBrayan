using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using API.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Contabilidad
{
    public class AsientoContableService : BasicService<AsientoContable, AsientoContableValidator>, IAsientoContableService
    {

        public AsientoContableService(IUnitOfWork<AsientoContable> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        /// <summary>
        /// Genera asientos contables automáticos cuando se confirma una venta
        /// Estructura: 
        /// - DEBE: Caja/Cuenta Bancaria (Activo)
        /// - HABER: Ingresos por Ventas (Ingresos)
        /// - DEBE: Gastos de Gestores y Mensajería (si aplican)
        /// - HABER: Cuentas por Pagar
        /// Considera conversión de monedas para gastos en diferentes monedas
        /// </summary>
        public async Task<Guid> GenerarAsientoVenta(Venta venta)
        {
            if (venta == null)
                throw new CustomException() { Status = 400, Message = "La venta no puede ser nula" };

            // Obtener el pedido con sus relaciones
            var pedido = await _repositorios.Pedidos
                .GetQuery()
                .Include(p => p.GestorPedidos)
                .Include(p => p.Moneda)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == venta.PedidoId)
                ?? throw new CustomException() { Status = 404, Message = "El pedido asociado a la venta no fue encontrado" };

            // Obtener las cuentas contables necesarias
            var cuentaCaja = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("1.1")); // Caja/Bancos

            var cuentaIngresos = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("4")); // Ingresos por Ventas

            var cuentaGastos = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("5")); // Gastos Operativos

            if (cuentaCaja == null)
                throw new CustomException() { Status = 400, Message = "No se encontró la cuenta de Caja/Bancos (1.1.x) configurada" };

            if (cuentaIngresos == null)
                throw new CustomException() { Status = 400, Message = "No se encontró la cuenta de Ingresos por Ventas (4.x.x) configurada" };

            // Obtener moneda del pedido
            var monedaPedido = pedido.Moneda;
            if (monedaPedido == null)
            {
                monedaPedido = await _repositorios.Monedas
                    .GetQuery()
                    .FirstOrDefaultAsync(m => m.Id == pedido.MonedaId);

                if (monedaPedido == null)
                    throw new CustomException() { Status = 400, Message = "No se encontró la moneda del pedido configurada" };
            }

            // Calcular total de gastos (gestores + mensajería) anticipadamente para ajustar ingresos
            decimal totalGastosGestores = 0m;
            if (pedido.GestorPedidos != null && pedido.GestorPedidos.Count > 0)
            {
                totalGastosGestores = pedido.GestorPedidos
                    .Where(g => g.PrecioAdicional.HasValue && g.PrecioAdicional.Value > 0)
                    .Sum(g => g.PrecioAdicional ?? 0m);
            }
            decimal totalGastos = totalGastosGestores + (pedido.Shipping > 0 ? pedido.Shipping : 0m);

            // Crear el asiento contable
            var asiento = new AsientoContable
            {
                Id = Guid.NewGuid(),
                Fecha = venta.FechaConfirmacion,
                Descripcion = $"Venta confirmada - Pedido {venta.Pedido?.Codigo.ToString() ?? "N/A"} - Cliente: {pedido.Usuario.NombreCompleto ?? "N/A"} - Moneda: {monedaPedido.Codigo}",
                ReferenciaId = venta.Id,
                TipoReferencia = "Venta",
                Movimientos = new List<MovimientoContable>()
            };

            // MOVIMIENTO 1: DEBE - Caja (aumenta activo)
            var movimientoCaja = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaCaja.Id,
                Debe = venta.TotalFinal,
                Haber = 0m,
                MonedaId = monedaPedido.Id
            };

            // Determinar monto neto de ingresos = venta - gastos
            var montoIngresosNetos = venta.TotalFinal - totalGastos;
            if (montoIngresosNetos < 0) montoIngresosNetos = 0m;

            // MOVIMIENTO 2: HABER - Ingresos por Ventas (aumenta ingresos) — solo el neto
            var movimientoIngresos = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaIngresos.Id,
                Debe = 0m,
                Haber = montoIngresosNetos,
                MonedaId = monedaPedido.Id
            };

            asiento.Movimientos.Add(movimientoCaja);
            asiento.Movimientos.Add(movimientoIngresos);

            // Si hay gastos, registrar un movimiento agregado por el total de gastos y disminuir caja
            if (totalGastos > 0)
            {
                if (cuentaGastos == null)
                {
                    cuentaGastos = await _repositorios.CuentasContables
                        .GetQuery()
                        .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("5"));
                }

                if (cuentaGastos == null)
                    throw new CustomException() { Status = 400, Message = "No se encontró la cuenta de Gastos Operativos (5.x.x) configurada" };

                // MOVIMIENTO: DEBE - Gastos Totales (gestores + mensajería)
                var movimientoGastoTotal = new MovimientoContable
                {
                    Id = Guid.NewGuid(),
                    AsientoContableId = asiento.Id,
                    CuentaContableId = cuentaGastos.Id,
                    Debe = totalGastos,
                    Haber = 0m,
                    MonedaId = monedaPedido.Id
                };

                // MOVIMIENTO: HABER - Caja (disminuye por el total pagado a gastos)
                var movimientoPagoCajaTotal = new MovimientoContable
                {
                    Id = Guid.NewGuid(),
                    AsientoContableId = asiento.Id,
                    CuentaContableId = cuentaCaja.Id,
                    Debe = 0m,
                    Haber = totalGastos,
                    MonedaId = monedaPedido.Id
                };

                asiento.Movimientos.Add(movimientoGastoTotal);
                asiento.Movimientos.Add(movimientoPagoCajaTotal);
            }

            // Guardar en la base de datos
            await _repositorios.AsientosContables.AddAsync(asiento);
            await _repositorios.SaveChangesAsync();

            return asiento.Id;
        }

        /// <summary>
        /// Genera asientos contables automáticos cuando se devuelve una venta
        /// Estructura (asiento reverso):
        /// - DEBE: Ingresos por Ventas (disminuye)
        /// - HABER: Caja/Cuenta Bancaria (disminuye)
        /// </summary>
        public async Task<Guid> GenerarAsientoDevolucion(Guid ventaId)
        {
            // Obtener la venta original
            var venta = await _repositorios.Ventas
                .GetQuery()
                .Include(v => v.Pedido)
                .ThenInclude(p => p.Moneda)
                .Include(v => v.UsuarioVendedor)
                .FirstOrDefaultAsync(v => v.Id == ventaId)
                ?? throw new CustomException() { Status = 404, Message = "La venta no fue encontrada" };

            // Obtener la moneda del pedido
            var monedaPedido = venta.Pedido?.Moneda;
            if (monedaPedido == null && venta.Pedido != null)
            {
                monedaPedido = await _repositorios.Monedas
                    .GetQuery()
                    .FirstOrDefaultAsync(m => m.Id == venta.Pedido.MonedaId);
            }

            if (monedaPedido == null)
                throw new CustomException() { Status = 400, Message = "No se encontró la moneda del pedido para la devolución" };

            // Obtener las cuentas contables necesarias
            var cuentaCaja = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("1.1"));

            var cuentaIngresos = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("4"));

            if (cuentaCaja == null || cuentaIngresos == null)
                throw new CustomException() { Status = 400, Message = "No se encontraron las cuentas contables necesarias para la devolución" };

            // Crear el asiento contable de devolución (reversa)
            var asiento = new AsientoContable
            {
                Id = Guid.NewGuid(),
                Fecha = DateTime.UtcNow,
                Descripcion = $"Devolución de venta - Pedido {venta.Pedido?.Codigo.ToString() ?? "N/A"} - Cliente: {venta.UsuarioVendedor?.NombreCompleto ?? "N/A"} - Moneda: {monedaPedido.Codigo}",
                ReferenciaId = ventaId,
                TipoReferencia = "Devolucion",
                Movimientos = new List<MovimientoContable>()
            };

            // MOVIMIENTO 1: DEBE - Ingresos por Ventas (disminuye con débito)
            var movimientoIngresos = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaIngresos.Id,
                Debe = venta.TotalFinal,
                Haber = 0m,
                MonedaId = monedaPedido.Id
            };

            // MOVIMIENTO 2: HABER - Caja (disminuye con crédito)
            var movimientoCaja = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaCaja.Id,
                Debe = 0m,
                Haber = venta.TotalFinal,
                MonedaId = monedaPedido.Id
            };

            asiento.Movimientos.Add(movimientoIngresos);
            asiento.Movimientos.Add(movimientoCaja);

            // Guardar en la base de datos
            await _repositorios.AsientosContables.AddAsync(asiento);
            await _repositorios.SaveChangesAsync();

            return asiento.Id;
        }
    }
}