using API.Application.Dtos.Gestion.Nomencladores.BannerPromocion;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class BannerPromocionProfile : BaseProfile<BannerPromocion, DetallesBannerPromocionDto, CrearBannerPromocionInputDto, ActualizarBannerPromocionInputDto, ListadoPaginadoBannerPromocionDto>
    {
        public BannerPromocionProfile()
        {
            MapBannerPromocionDto();
        }

        public void MapBannerPromocionDto()
        {
            CreateMap<BannerPromocion, BannerPromocionDto>().ReverseMap();
            CreateMap<BannerPromocion, DetallesBannerPromocionDto>().ReverseMap();
            CreateMap<BannerPromocion, ListadoPaginadoBannerPromocionDto>().ReverseMap();
        }
    }
}