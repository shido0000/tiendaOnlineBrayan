using API.Application.Dtos.Gestion.Nomencladores.ListaDeseosDetalle;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ListaDeseosDetalleDtoProfile : BaseProfile<ListaDeseosDetalle, ListaDeseosDetalleDto, CrearListaDeseosDetalleInputDto, ActualizarListaDeseosDetalleInputDto, ListadoPaginadoListaDeseosDetalleDto>

    {
        public ListaDeseosDetalleDtoProfile()
        {
            //   MapListaDeseosDetalleDto();
            MapDetallesListaDeseosDetalleDto();
        }


        public void MapDetallesListaDeseosDetalleDto()
        {
            CreateMap<ListaDeseosDetalle, DetallesListaDeseosDetalleDto>()
              .ReverseMap()
            ;
        }

         
    }
}
