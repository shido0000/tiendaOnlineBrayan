using API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

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
            CreateMap<CategoriaProducto, DetallesCategoriaProductoDto>()
                .ForMember(dto => dto.ListadoDeProductos, opt => opt.MapFrom(e => e.ProductoCategorias.Select(f => f.Producto).ToList()))

              .ReverseMap()
            ;
        }

         
    }
}
