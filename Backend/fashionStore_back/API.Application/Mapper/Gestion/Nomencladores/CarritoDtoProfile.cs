using API.Application.Dtos.Gestion.Nomencladores.Carrito;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class CarritoDtoProfile : BaseProfile<Carrito, CarritoDto, CrearCarritoInputDto, ActualizarCarritoInputDto, ListadoPaginadoCarritoDto>

    {
        public CarritoDtoProfile()
        {
            //   MapCarritoDto();
            MapDetallesCarritoDto();
        }


        public void MapDetallesCarritoDto()
        {
            CreateMap<Carrito, DetallesCarritoDto>()
              .ReverseMap()
            ;
        }

         
    }
}
