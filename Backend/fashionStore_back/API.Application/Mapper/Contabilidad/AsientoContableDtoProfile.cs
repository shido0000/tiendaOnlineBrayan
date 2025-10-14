using API.Application.Dtos.Contabilidad.AsientoContable;
using API.Data.Entidades.Contabilidad;

namespace API.Application.Mapper.Contabilidad
{
    public class AsientoContableDtoProfile : BaseProfile<AsientoContable, AsientoContableDto, CrearAsientoContableInputDto, ActualizarAsientoContableInputDto, ListadoPaginadoAsientoContableDto>

    {
        public AsientoContableDtoProfile()
        {
            //   MapAsientoContableDto();
            MapDetallesAsientoContableDto();
        }


        public void MapDetallesAsientoContableDto()
        {
            CreateMap<AsientoContable, DetallesAsientoContableDto>()
              .ReverseMap()
            ;
        }


    }
}
