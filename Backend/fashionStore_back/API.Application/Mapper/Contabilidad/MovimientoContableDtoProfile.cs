using API.Application.Dtos.Contabilidad.MovimientoContable;
using API.Data.Entidades.Contabilidad;

namespace API.Application.Mapper.Contabilidad
{
    public class MovimientoContableDtoProfile : BaseProfile<MovimientoContable, MovimientoContableDto, CrearMovimientoContableInputDto, ActualizarMovimientoContableInputDto, ListadoPaginadoMovimientoContableDto>

    {
        public MovimientoContableDtoProfile()
        {
            //   MapMovimientoContableDto();
            MapDetallesMovimientoContableDto();
        }


        public void MapDetallesMovimientoContableDto()
        {
            CreateMap<MovimientoContable, DetallesMovimientoContableDto>()
              .ReverseMap()
            ;
        }


    }
}
