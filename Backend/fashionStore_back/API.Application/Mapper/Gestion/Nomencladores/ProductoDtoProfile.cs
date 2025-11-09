using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ProductoDtoProfile : BaseProfile<Producto, ProductoDto, CrearProductoInputDto, ActualizarProductoInputDto, ListadoPaginadoProductoDto>

    {
        public ProductoDtoProfile()
        {
            MapProductoDto();
            MapProductoListadoDto();
        }

        public void MapProductoDto()
        {
            CreateMap<Producto, DetallesProductoDto>()
                .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo))
                .ForMember(dto => dto.Fotos, opt => opt.MapFrom(e => e.Fotos.Select(f => f.Url).ToList()))
                .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                .ForMember(dto => dto.Categorias, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.Categoria.Nombre).ToList()))
                .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo))
                .ForMember(dto => dto.Fotos, opt => opt.MapFrom(e => e.Fotos.Select(f => f.Url).ToList()))
                .ReverseMap();
        }
    }
}
