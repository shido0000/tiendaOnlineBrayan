using API.Application.Dtos.Gestion.Nomencladores.ListaDeseos;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ListaDeseosDtoProfile : BaseProfile<ListaDeseos, ListaDeseosDto, CrearListaDeseosInputDto, ActualizarListaDeseosInputDto, ListadoPaginadoListaDeseosDto>

    {
        public ListaDeseosDtoProfile()
        {
            //   MapListaDeseosDto();
            MapDetallesListaDeseosDto();
        }


        public void MapDetallesListaDeseosDto()
        {
            CreateMap<ListaDeseos, DetallesListaDeseosDto>()
              .ReverseMap()
            ;
        }

         
    }
}
