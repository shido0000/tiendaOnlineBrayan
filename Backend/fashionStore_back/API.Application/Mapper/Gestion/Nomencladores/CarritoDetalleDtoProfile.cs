using API.Application.Dtos.Gestion.Nomencladores.CarritoDetalle;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class CarritoDetalleDtoProfile : BaseProfile<CarritoDetalle, CarritoDetalleDto, CrearCarritoDetalleInputDto, ActualizarCarritoDetalleInputDto, ListadoPaginadoCarritoDetalleDto>

    {
        public CarritoDetalleDtoProfile()
        {
            //   MapCarritoDetalleDto();
            MapDetallesCarritoDetalleDto();
        }


        public void MapDetallesCarritoDetalleDto()
        {
            CreateMap<CarritoDetalle, DetallesCarritoDetalleDto>()
              .ReverseMap()
            ;
        }

         
    }
}
