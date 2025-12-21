using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class BannerPromocionService : BasicService<BannerPromocion, BannerPromocionValidator>, IBannerPromocionService
    {
        public BannerPromocionService(IUnitOfWork<BannerPromocion> unitOfWork, IHttpContextAccessor httpContext)
            : base(unitOfWork, httpContext)
        {
        }

        public async Task<List<BannerPromocion>> ObtenerActivos()
        {
            var hoy = DateTime.UtcNow.Date;
            var banners = await _repositorios.BannerPromociones
                                .GetQuery()
                                .AsNoTracking()
                                .Where(e=>e.EsActivo && e.FechaInicio==null || e.FechaInicio<=hoy && e.FechaFin==null || e.FechaFin>=hoy)
                                .ToListAsync();
            
            return banners.OrderByDescending(b => b.Destacado)
                         .ThenBy(b => b.Orden)
                         .ToList();
        }

        public async Task<List<BannerPromocion>> ObtenerActivosPorUbicacion(string ubicacion)
        {
            var hoy = DateTime.UtcNow.Date;
            var banners = await _repositorios.BannerPromociones
                               .GetQuery()
                               .AsNoTracking()
                               .Where(e => e.EsActivo && e.Ubicaciones.Contains(ubicacion) && e.FechaInicio == null || e.FechaInicio <= hoy && e.FechaFin == null || e.FechaFin >= hoy)
                               .ToListAsync();
           
            return banners.OrderByDescending(b => b.Destacado)
                         .ThenBy(b => b.Orden)
                         .ToList();
        }

        public async Task<List<BannerPromocion>> ObtenerDestacados()
        {
            var hoy = DateTime.UtcNow.Date;

            var banners = await _repositorios.BannerPromociones
                              .GetQuery()
                              .AsNoTracking()
                              .Where(e => e.EsActivo && e.Destacado && e.FechaInicio == null || e.FechaInicio <= hoy && e.FechaFin == null || e.FechaFin >= hoy)
                              .ToListAsync();

            return banners.OrderBy(b => b.Orden).ToList();
        }
    }
}