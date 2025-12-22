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
            MapListadoAsientoContableDto();
        }


        public void MapDetallesAsientoContableDto()
        {
            CreateMap<AsientoContable, DetallesAsientoContableDto>()
              .ForMember(dest => dest.Movimientos, opt => opt.MapFrom(src => src.Movimientos));
             // .ForMember(dest => dest.Consecutivo, opt => opt.MapFrom<VentaConsecutivoResolver>());
        }
        public void MapListadoAsientoContableDto()
        {
            CreateMap<AsientoContable, ListadoPaginadoAsientoContableDto>()
              .ForMember(dest => dest.Movimientos, opt => opt.MapFrom(src => src.Movimientos));
            //  .ForMember(dest => dest.Consecutivo, opt => opt.MapFrom<VentaConsecutivoResolver>());
        }

    }
}
