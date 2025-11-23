using API.Application.Dtos.Gestion.Nomencladores.Mensajeria;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class MensajeriaDtoProfile : BaseProfile<Mensajeria, DetallesMensajeriaDto, CrearMensajeriaInputDto, ActualizarMensajeriaInputDto, ListadoPaginadoMensajeriaDto>

    {
        public MensajeriaDtoProfile()
        {
            MapMensajeriaDto();
            MapMensajeriaListadoDto();
        }

        public void MapMensajeriaDto()
        {
            CreateMap<Mensajeria, DetallesMensajeriaDto>()
              .ReverseMap();
        }

        public void MapMensajeriaListadoDto()
        {
            CreateMap<Mensajeria, ListadoPaginadoMensajeriaDto>()
                .ForMember(dto => dto.TextoMensajeriaPrecio, opt => opt.MapFrom(e => e.Descripcion + " - "+ e.Precio + " - " + e.Moneda.Codigo ?? "-"))
                .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo ?? "-"))
                .ReverseMap();
        }


    }
}
