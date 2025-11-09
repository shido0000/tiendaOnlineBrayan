using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Application.Dtos.Gestion.Nomencladores.ProductVariant;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ProductoDtoProfile : BaseProfile<Producto, DetallesProductoDto, CrearProductoInputDto, ActualizarProductoInputDto, ListadoPaginadoProductoDto>

    {
        public ProductoDtoProfile()
        {
            MapProductoDto();
            MapProductoListadoDto();
            MapProductVariantDto();
        }

        public void MapProductoDto()
        {
            CreateMap<Producto, DetallesProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                .ForMember(dto => dto.Variants,
                    opt => opt.MapFrom(e => e.Variants)) // 👈 ahora sí mapea variantes
                .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            // Producto → ListadoPaginadoProductoDto (hereda de ProductoDto)
            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                .ReverseMap();
        }

        public void MapProductVariantDto()
        {
            CreateMap<ProductVariant, DetallesProductVariantDto>()
                .ForMember(dto => dto.ProductoId, opt => opt.MapFrom(v => v.ProductoId))
                .ForMember(dto => dto.FotosExistentes,
                    opt => opt.MapFrom(v => v.Fotos.Select(f => f.Url).ToList()))
                .ReverseMap()
                .ForMember(v => v.Fotos, opt => opt.Ignore());
        }
    }
}
