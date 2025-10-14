using API.Application.Dtos.Gestion.Nomencladores.Moneda;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class MonedaDtoProfile : BaseProfile<Moneda, MonedaDto, CrearMonedaInputDto, ActualizarMonedaInputDto, ListadoPaginadoMonedaDto>

    {
        public MonedaDtoProfile()
        {
            //   MapMonedaDto();
            MapDetallesMonedaDto();
        }


        public void MapDetallesMonedaDto()
        {
            CreateMap<Moneda, DetallesMonedaDto>()
              .ReverseMap()
            ;
        }

         
    }
}
