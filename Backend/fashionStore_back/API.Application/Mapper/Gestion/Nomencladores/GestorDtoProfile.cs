using API.Application.Dtos.Gestion.Nomencladores.Gestor;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class GestorDtoProfile : BaseProfile<Gestor, DetallesGestorDto, CrearGestorInputDto, ActualizarGestorInputDto, ListadoPaginadoGestorDto>

    {
        public GestorDtoProfile()
        {
            MapGestorDto();
            MapGestorListadoDto();
        }

        public void MapGestorDto()
        {
            CreateMap<Gestor, DetallesGestorDto>()
                .ReverseMap();
        }

        public void MapGestorListadoDto()
        {
            CreateMap<Gestor, ListadoPaginadoGestorDto>()
                .ReverseMap();
        }
    }
}
