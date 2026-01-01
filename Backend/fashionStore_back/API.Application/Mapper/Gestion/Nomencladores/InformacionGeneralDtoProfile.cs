using API.Application.Dtos.Gestion.Nomencladores.InformacionGeneral;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class InformacionGeneralProfile : BaseProfile<InformacionGeneral, DetallesInformacionGeneralDto, CrearInformacionGeneralInputDto, ActualizarInformacionGeneralInputDto, ListadoPaginadoInformacionGeneralDto>
    {
        public InformacionGeneralProfile()
        {
            MapInformacionGeneralDto();
        }

        public void MapInformacionGeneralDto()
        {
            CreateMap<InformacionGeneral, InformacionGeneralDto>().ReverseMap();
            CreateMap<InformacionGeneral, DetallesInformacionGeneralDto>().ReverseMap();
            CreateMap<InformacionGeneral, ListadoPaginadoInformacionGeneralDto>().ReverseMap();
        }
    }
}