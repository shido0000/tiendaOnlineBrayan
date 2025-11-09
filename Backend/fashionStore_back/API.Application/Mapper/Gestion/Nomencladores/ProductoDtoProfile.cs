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
        }

        public void MapProductoDto()
        {
            //CreateMap<Producto, ListadoPaginadoProductoDto>()
            //    .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo))
            //    .ForMember(dto => dto.Fotos, opt => opt.MapFrom(e => e.Fotos.Select(f => f.Url).ToList()))
            //    .ReverseMap();

   
            CreateMap<Producto, DetallesProductoDto>()
              .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
              .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            // Producto → ListadoPaginadoProductoDto (hereda de ProductoDto)
            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                .ReverseMap();
        }
    }
}
