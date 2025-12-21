using API.Application.Dtos.Gestion.Nomencladores.BannerPromocion;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerPromocionController : BasicController<BannerPromocion, BannerPromocionValidator, DetallesBannerPromocionDto, CrearBannerPromocionInputDto, ActualizarBannerPromocionInputDto, ListadoPaginadoBannerPromocionDto, FiltrarConfigurarListadoPaginadoBannerPromocionInputDto>
    {
        private readonly IBannerPromocionService _bannerService;

        public BannerPromocionController(IMapper mapper, IBannerPromocionService bannerService)
            : base(mapper, bannerService)
        {
            _bannerService = bannerService;
        }

        protected override Task<(IEnumerable<BannerPromocion>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoBannerPromocionInputDto inputDto)
        {
            List<Expression<Func<BannerPromocion, bool>>> filtros = new();

            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(b => b.Nombre.Contains(inputDto.TextoBuscar));

            return _servicioBase.ObtenerListadoPaginado(
                inputDto.CantidadIgnorar,
                inputDto.CantidadMostrar,
                inputDto.SecuenciaOrdenamiento,
                null,
                filtros.ToArray());
        }

        protected override async Task<BannerPromocion?> ObtenerElementoPorId(Guid id)
            => await _servicioBase.ObtenerPorId(id);

        [HttpGet("ObtenerActivos")]
        public async Task<ActionResult<List<ListadoPaginadoBannerPromocionDto>>> ObtenerActivos()
        {
            var banners = await _bannerService.ObtenerActivos();
            return Ok(_mapper.Map<List<ListadoPaginadoBannerPromocionDto>>(banners));
        }

        [HttpGet("ObtenerActivosPorUbicacion/{ubicacion}")]
        public async Task<ActionResult<List<ListadoPaginadoBannerPromocionDto>>> ObtenerActivosPorUbicacion(string ubicacion)
        {
            var banners = await _bannerService.ObtenerActivosPorUbicacion(ubicacion);
            return Ok(_mapper.Map<List<ListadoPaginadoBannerPromocionDto>>(banners));
        }

        [HttpGet("ObtenerDestacados")]
        public async Task<ActionResult<List<ListadoPaginadoBannerPromocionDto>>> ObtenerDestacados()
        {
            var banners = await _bannerService.ObtenerDestacados();
            return Ok(_mapper.Map<List<ListadoPaginadoBannerPromocionDto>>(banners));
        }
    }
}