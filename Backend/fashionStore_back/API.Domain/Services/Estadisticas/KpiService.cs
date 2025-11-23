using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Estadisticas;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Estadisticas
{
    public class KpiService : IKpiService
    {
        private readonly IUnitOfWork<Producto> _repositorio;

        public KpiService(IUnitOfWork<Producto> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<object> GetKpis()
        {
            Guid rolClienteId = new Guid("C0B7E3B3-A06E-4580-B985-BB2FC4336524");
            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1);

            var totalSalesMonth = await _repositorio.Pedidos
                .GetQuery()
                .Where(o => o.Estado == EstadoPedido.Confirmado && o.FechaCreado >= monthStart)
                .SumAsync(o => (decimal?)o.Total) ?? 0m;

            var pendingOrders = await _repositorio.Pedidos.CountAsync(o => o.Estado == EstadoPedido.Pendiente);
            var activeProducts = await _repositorio.Productos.CountAsync(p => p.EsActivo);
            var customers = await _repositorio.Usuarios.CountAsync(u => u.RolId == rolClienteId);

            return new
            {
                totalSalesMonth,
                pendingOrders,
                activeProducts,
                customers
            };
        }



        public async Task<object> GetSellerKpis()
        {
            var today = DateTime.UtcNow.Date;

            var reservedToday = await _repositorio.Pedidos.CountAsync(o => o.Estado == EstadoPedido.Pendiente && o.FechaCreado.Date == today);
            var confirmedToday = await _repositorio.Pedidos.CountAsync(o => o.Estado == EstadoPedido.Confirmado && o.FechaCreado.Date == today);
            var canceledToday = await _repositorio.Pedidos.CountAsync(o => o.Estado == EstadoPedido.Rechazado && o.FechaCreado.Date == today);
            var totalPending = await _repositorio.Pedidos
                .GetQuery()
                .Where(o => o.Estado == EstadoPedido.Pendiente)
                .SumAsync(o => (decimal?)o.Total) ?? 0m;

            return new
            {
                reservedToday,
                confirmedToday,
                canceledToday,
                totalPending
            };
        }
    }
}