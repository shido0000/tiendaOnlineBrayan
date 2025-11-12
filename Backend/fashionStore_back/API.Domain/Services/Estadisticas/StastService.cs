using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Estadisticas;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Estadisticas
{
    public class StastService : IStastService
    {
        private readonly IUnitOfWork<Producto> _repositorio;

        public StastService(IUnitOfWork<Producto> repositorio)
        {
            _repositorio = repositorio;
        }

        //public async Task<object> Get(CancellationToken ct)
        //{
        //    var orders = await _repositorio.Pedidos
        //        .GetQuery()
        //        .Include(o => o.Detalles).ThenInclude(i => i.Producto).ThenInclude(p => p.ProductoCategorias)
        //        .ToListAsync(ct);

        //    var byDay = orders.GroupBy(o => o.FechaCreado.Date)
        //        .Select(g => new { date = g.Key, count = g.Count(), total = g.Sum(o => o.Total) })
        //        .OrderBy(g => g.date);

        //    var byCategory = orders.SelectMany(o => o.Detalles)
        //        .GroupBy(i => i.Producto.ProductoCategorias.)
        //        .Select(g => new { category = g.Key, count = g.Sum(i => i.Quantity), total = g.Sum(i => i.LineTotal) });

        //    var topProducts = orders.SelectMany(o => o.Detalles)
        //        .GroupBy(i => i.Producto.Descripcion)
        //        .Select(g => new { product = g.Key, count = g.Sum(i => i.Cantidad), total = g.Sum(i => i.LineTotal) })
        //        .OrderByDescending(g => g.total).Take(10);

        //    return new
        //    {
        //        dailyOrders = byDay,
        //        byCategory,
        //        topProducts
        //    };
        //}
    }
}