using API.Data.Dto.Dashboard;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Dashboard
{
    public class DatosDashboardService : IDatosDashboardService
    {
        readonly IUnitOfWork<VentaDetalle> _repositorios;

        public DatosDashboardService(IUnitOfWork<VentaDetalle> repositorios, IHttpContextAccessor httpContext)
        {
            _repositorios = repositorios;
        }


        // 1. Ventas últimos 7 días
        public async Task<VentasUltimos7DiasDto> GetVentasUltimos7DiasAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var inicio = hoy.AddDays(-6);

            var ventas = await _repositorios.Ventas
                .GetQuery()
                .Where(v => v.FechaConfirmacion.Date >= inicio && v.FechaConfirmacion.Date <= hoy)
                .GroupBy(v => v.FechaConfirmacion.Date)
                .Select(g => new { Dia = g.Key, Total = g.Sum(x => x.TotalFinal) })
                .ToListAsync();

            var dto = new VentasUltimos7DiasDto();
            for (int i = 0; i < 7; i++)
            {
                var fecha = inicio.AddDays(i);
                dto.Dias.Add(fecha.ToString("ddd")); // Lun, Mar, etc.
                dto.Totales.Add(ventas.FirstOrDefault(v => v.Dia == fecha)?.Total ?? 0);
            }

            return dto;
        }

        // 2. Top productos
        public async Task<List<TopProductoDto>> GetTopProductosAsync(int top = 5)
        {
            var productos = await _repositorios.VentasDetalles
                .GetQuery()
                .Include(e => e.ProductoVariante)
                    .ThenInclude(e => e.Producto)
                .GroupBy(d => d.ProductoVariante)
                .Select(g => new TopProductoDto
                {
                    Nombre = g.Key.Producto.Codigo ?? "-", // asumiendo que ProductoVariante tiene Nombre
                    CantidadVendida = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(top)
                .ToListAsync();

            return productos;
        }

        // Ventas y pedidos de hoy
        public async Task<(decimal ventasHoy, int pedidosHoy)> GetVentasPedidosHoyAsync()
        {
            var hoy = DateTime.UtcNow.Date;

            var ventasHoy = await _repositorios.Ventas
                .GetQuery()
                .Where(v => v.FechaConfirmacion.Date == hoy)
                .SumAsync(v => v.TotalFinal);

            var pedidosHoy = await _repositorios.Ventas
                .GetQuery()
                .CountAsync(v => v.FechaConfirmacion.Date == hoy);

            return (ventasHoy, pedidosHoy);
        }

        // Clientes activos y nuevos hoy
        public async Task<(int clientesActivos, int nuevosHoy)> GetClientesAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var textoCliente = "Cliente";
            var clientesActivos = await _repositorios.Usuarios
                .GetQuery()
                .Include(e => e.Rol)
                .CountAsync(u => u.EsActivo && u.Rol.Nombre.ToLower().Trim() == textoCliente.ToLower().Trim()); // asumiendo que Usuario tiene campo Activo

            var nuevosHoy = await _repositorios.Usuarios
                .GetQuery()
                .Include(e => e.Rol)
                .CountAsync(u => u.FechaCreado.Date == hoy && u.Rol.Nombre.ToLower().Trim() == textoCliente.ToLower().Trim()); // asumiendo que Usuario tiene FechaRegistro

            return (clientesActivos, nuevosHoy);
        }

        // Inventario bajo
        public async Task<int> GetInventarioBajoAsync(int umbral = 5)
        {
            var inventarioBajo = await _repositorios.ProductoVariantes
                .CountAsync(pv => pv.Stock <= umbral); // asumiendo que ProductoVariante tiene StockActual

            return inventarioBajo;
        }

        // Ingresos semana y mes
        public async Task<(decimal semana, decimal mes)> GetIngresosAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek); // domingo
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);

            var semana = await _repositorios.Ventas
                .GetQuery()
                .Where(v => v.FechaConfirmacion.Date >= inicioSemana)
                .SumAsync(v => v.TotalFinal);

            var mes = await _repositorios.Ventas
                .GetQuery()
                .Where(v => v.FechaConfirmacion.Date >= inicioMes)
                .SumAsync(v => v.TotalFinal);

            return (semana, mes);
        }

        // Pedidos pendientes
        public async Task<int> GetPedidosPendientesAsync()
        {
            return await _repositorios.Pedidos
                .CountAsync(p => p.Estado == EstadoPedido.Pendiente); // asumiendo que Pedido tiene campo Despachado
        }

        // Top producto hoy
        public async Task<(string nombre, int cantidad)> GetTopProductoHoyAsync()
        {
            var hoy = DateTime.UtcNow.Date;

            var top = await _repositorios.VentasDetalles
                .GetQuery()
                .Include(e => e.Venta)
                .Include(e => e.ProductoVariante)
                    .ThenInclude(e => e.Producto)
                .Where(d => d.Venta.FechaConfirmacion.Date == hoy)
                .GroupBy(d => d.ProductoVariante)
                .Select(g => new { Nombre = g.Key.Producto.Codigo, Cantidad = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefaultAsync();

            return top == null ? ("", 0) : (top.Nombre, top.Cantidad);
        }

        // Clientes recurrentes vs nuevos semana
        public async Task<(int recurrentes, int nuevosSemana)> GetClientesRecurrentesAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek);
            var textoCliente = "Cliente";

            var nuevosSemana = await _repositorios.Usuarios
                .GetQuery()
                .Include(e => e.Rol)
                .CountAsync(u => u.FechaCreado.Date >= inicioSemana && u.Rol.Nombre.ToLower().Trim() == textoCliente.ToLower().Trim());

            var recurrentes = await _repositorios.Ventas
                .GetQuery()
                .Include(e=>e.Pedido)
                .Where(v => v.FechaConfirmacion.Date >= inicioSemana)
                .Select(v => v.Pedido.UsuarioId)
                .Distinct()
                .CountAsync();

            return (recurrentes, nuevosSemana);
        }

        // Margen bruto promedio
        public async Task<decimal> GetMargenBrutoPromedioAsync()
        {
            var detalles = await _repositorios.VentasDetalles
                .GetQuery()
                .Include(e=>e.ProductoVariante)
                    .ThenInclude(e=>e.Producto)
                .Select(d => new { d.PrecioUnitario, d.DescuentoAplicado, PrecioCosto=d.ProductoVariante.Producto.PrecioCosto })
                .ToListAsync();

            if (!detalles.Any()) return 0;

            var margenPromedio = detalles.Average(d =>
            {
                var ingreso = d.PrecioUnitario - d.DescuentoAplicado;
                var margen = ingreso - d.PrecioCosto;
                return ingreso == 0 ? 0 : (margen / ingreso) * 100;
            });

            return Math.Round(margenPromedio, 2);
        }

        public async Task<(int confirmados, int cancelados)> GetPedidosEstadoAsync()
        {
            var confirmados = await _repositorios.Pedidos
                .CountAsync(p => p.Estado == EstadoPedido.Confirmado); // o p.Confirmado == true

            var cancelados = await _repositorios.Pedidos
                .CountAsync(p => p.Estado == EstadoPedido.Rechazado); // o p.Cancelado == true

            return (confirmados, cancelados);
        }

        public async Task<PedidosUltimos7DiasDto> GetPedidosUltimos7DiasAsync()
        {
            var hoy = DateTime.UtcNow.Date;
            var inicio = hoy.AddDays(-6);

            var pedidos = await _repositorios.Pedidos
                .GetQuery()
                .Where(p => p.FechaCreado.Date >= inicio && p.FechaCreado.Date <= hoy)
                .GroupBy(p => new { p.FechaCreado.Date, p.Estado })
                .Select(g => new { Dia = g.Key.Date, Estado = g.Key.Estado, Cantidad = g.Count() })
                .ToListAsync();

            var dto = new PedidosUltimos7DiasDto();
            for (int i = 0; i < 7; i++)
            {
                var fecha = inicio.AddDays(i);
                dto.Dias.Add(fecha.ToString("ddd"));

                dto.Confirmados.Add(pedidos
                    .FirstOrDefault(p => p.Dia == fecha && p.Estado == EstadoPedido.Confirmado)?.Cantidad ?? 0);

                dto.Cancelados.Add(pedidos
                    .FirstOrDefault(p => p.Dia == fecha && p.Estado == EstadoPedido.Rechazado)?.Cantidad ?? 0);
            }

            return dto;
        }


        // 3. Método orquestador
        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var ventas = await GetVentasUltimos7DiasAsync();
            var topProductos = await GetTopProductosAsync();
            var (ventasHoy, pedidosHoy) = await GetVentasPedidosHoyAsync();
            var (clientesActivos, nuevosHoy) = await GetClientesAsync();
            var inventarioBajo = await GetInventarioBajoAsync();

            var (ingresosSemana, ingresosMes) = await GetIngresosAsync();
            var pedidosPendientes = await GetPedidosPendientesAsync();
            var (topNombre, topCantidad) = await GetTopProductoHoyAsync();
            var (recurrentes, nuevosSemana) = await GetClientesRecurrentesAsync();
            var margenBruto = await GetMargenBrutoPromedioAsync();
            var (confirmados, cancelados) = await GetPedidosEstadoAsync();

            var pedidos7 = await GetPedidosUltimos7DiasAsync();

            return new DashboardDto
            {
                VentasUltimos7Dias = ventas,
                TopProductos = topProductos,
                VentasHoy = ventasHoy,
                PedidosHoy = pedidosHoy,
                ClientesActivos = clientesActivos,
                NuevosClientesHoy = nuevosHoy,
                InventarioBajo = inventarioBajo,

                IngresosSemana = ingresosSemana,
                IngresosMes = ingresosMes,
                PedidosPendientes = pedidosPendientes,
                TopProductoHoy = topNombre,
                TopProductoHoyCantidad = topCantidad,
                ClientesRecurrentes = recurrentes,
                ClientesNuevosSemana = nuevosSemana,
                MargenBrutoPromedio = margenBruto,
                PedidosConfirmados = confirmados,
                PedidosCancelados = cancelados,
                PedidosUltimos7Dias = pedidos7
            };
        }

    }
}