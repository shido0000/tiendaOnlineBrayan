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
        /// </summary>
        public async Task<Guid> GenerarAsientoVenta(Venta venta)
        {
            if (venta == null)
                throw new CustomException() { Status = 400, Message = "La venta no puede ser nula" };

            // Obtener las cuentas contables necesarias
            var cuentaCaja = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("1.1")); // Caja/Bancos

            var cuentaIngresos = await _repositorios.CuentasContables
                .GetQuery()
                .FirstOrDefaultAsync(c => c.EsActivo && c.EsDeMovimiento && c.Codigo.StartsWith("4")); // Ingresos por Ventas

            if (cuentaCaja == null)
                throw new CustomException() { Status = 400, Message = "No se encontró la cuenta de Caja/Bancos (1.1.x) configurada" };

            if (cuentaIngresos == null)
                throw new CustomException() { Status = 400, Message = "No se encontró la cuenta de Ingresos por Ventas (4.x.x) configurada" };

            // Crear el asiento contable
            var asiento = new AsientoContable
            {
                Id = Guid.NewGuid(),
                Fecha = venta.FechaConfirmacion,
                Descripcion = $"Venta confirmada - Pedido {venta.Pedido?.Codigo.ToString() ?? "N/A"} - Cliente: {venta.UsuarioVendedor?.NombreCompleto ?? "N/A"}",
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
                Haber = 0m
            };

            // MOVIMIENTO 2: HABER - Ingresos por Ventas (aumenta ingresos)
            var movimientoIngresos = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaIngresos.Id,
                Debe = 0m,
                Haber = venta.TotalFinal
            };

            asiento.Movimientos.Add(movimientoCaja);
            asiento.Movimientos.Add(movimientoIngresos);

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
                .Include(v => v.UsuarioVendedor)
                .FirstOrDefaultAsync(v => v.Id == ventaId)
                ?? throw new CustomException() { Status = 404, Message = "La venta no fue encontrada" };

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
                Descripcion = $"Devolución de venta - Pedido {venta.Pedido?.Codigo.ToString() ?? "N/A"} - Cliente: {venta.UsuarioVendedor?.NombreCompleto ?? "N/A"}",
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
                Haber = 0m
            };

            // MOVIMIENTO 2: HABER - Caja (disminuye con crédito)
            var movimientoCaja = new MovimientoContable
            {
                Id = Guid.NewGuid(),
                AsientoContableId = asiento.Id,
                CuentaContableId = cuentaCaja.Id,
                Debe = 0m,
                Haber = venta.TotalFinal
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