using API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto;
using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class CategoriaProductoDtoProfile : BaseProfile<CategoriaProducto, DetallesCategoriaProductoDto, CrearCategoriaProductoInputDto, ActualizarCategoriaProductoInputDto, ListadoPaginadoCategoriaProductoDto>

    {
        public CategoriaProductoDtoProfile()
        {
            //   MapCategoriaProductoDto();
            MapDetallesCategoriaProductoDto();
        }

        public void MapDetallesCategoriaProductoDto()
        {
            // Mapeo explícito para la lista de productos usando el mapeo Producto -> ProductoDto
            CreateMap<CategoriaProducto, DetallesCategoriaProductoDto>()
                .ForMember(dto => dto.ListadoDeProductos, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.Producto)))
                .ReverseMap();

            // Asegurar mapeo entre CategoriaProductoDto (usado en formularios) y la entidad
            CreateMap<CategoriaProducto, CategoriaProductoDto>().ReverseMap();
        }
    }
}
