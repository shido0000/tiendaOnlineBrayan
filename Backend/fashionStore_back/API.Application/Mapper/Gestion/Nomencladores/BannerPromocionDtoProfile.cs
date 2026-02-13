using API.Application.Dtos.Gestion.Nomencladores.BannerPromocion;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class BannerPromocionProfile : BaseProfile<BannerPromocion, DetallesBannerPromocionDto, CrearBannerPromocionInputDto, ActualizarBannerPromocionInputDto, ListadoPaginadoBannerPromocionDto>
    {
        public BannerPromocionProfile()
        {
            MapBannerPromocionDto();
            MapBannerPromocionListadoDto();
        }

        public void MapBannerPromocionDto()
        {
            CreateMap<BannerPromocion, BannerPromocionDto>().ReverseMap();
            CreateMap<BannerPromocion, DetallesBannerPromocionDto>().ReverseMap();
            CreateMap<BannerPromocion, ListadoPaginadoBannerPromocionDto>().ReverseMap();
        }

        public void MapBannerPromocionListadoDto()
        {

            CreateMap<BannerPromocion, ListadoPaginadoBannerPromocionDto>()
                .ForMember(dest => dest.CategoriaDescripcion,
                    opt => opt.MapFrom(src => src.CategoriaProducto != null ? src.CategoriaProducto.Descripcion : "-"));
        }


    }
}