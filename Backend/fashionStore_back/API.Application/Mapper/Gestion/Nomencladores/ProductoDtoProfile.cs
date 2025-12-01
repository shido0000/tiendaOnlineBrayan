using API.Application.Dtos.Gestion.Nomencladores.Producto;
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
            // Map base Producto -> ProductoDto so AutoMapper can map collections to ProductoDto
            CreateMap<Producto, ProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                .ReverseMap();

            CreateMap<Producto, DetallesProductoDto>()
                .IncludeBase<Producto, ProductoDto>()
                .ForMember(dto => dto.CategoriaIds,
                    opt => opt.MapFrom(e => e.ProductoCategorias.Select(pc => pc.CategoriaId).ToList()))
                .ReverseMap();
        }

        public void MapProductoListadoDto()
        {
            // Producto → ListadoPaginadoProductoDto (hereda de ProductoDto)
            CreateMap<Producto, ListadoPaginadoProductoDto>()
                .ForMember(dto => dto.CategoriaIds, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.CategoriaId).ToList()))
                .ForMember(dto => dto.MonedaCostoCodigo, opt => opt.MapFrom(e => e.MonedaCosto.Codigo ?? "-"))
                .ForMember(dto => dto.MonedaVentaCodigo, opt => opt.MapFrom(e => e.MonedaVenta.Codigo ?? "-"))
                .IncludeBase<Producto, ProductoDto>()
                .ReverseMap();
        }


    }
}
