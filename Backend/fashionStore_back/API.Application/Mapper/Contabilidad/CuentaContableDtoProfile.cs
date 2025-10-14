using API.Application.Dtos.Contabilidad.CuentaContable;
using API.Data.Entidades.Contabilidad;

namespace API.Application.Mapper.Contabilidad
{
    public class CuentaContableDtoProfile : BaseProfile<CuentaContable, CuentaContableDto, CrearCuentaContableInputDto, ActualizarCuentaContableInputDto, ListadoPaginadoCuentaContableDto>

    {
        public CuentaContableDtoProfile()
        {
            //   MapCuentaContableDto();
            MapDetallesCuentaContableDto();
        }


        public void MapDetallesCuentaContableDto()
        {
            CreateMap<CuentaContable, DetallesCuentaContableDto>()
              .ReverseMap()
            ;
        }


    }
}
