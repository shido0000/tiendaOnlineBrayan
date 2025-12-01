using API.Application.Dtos.Gestion.Nomencladores.OtraVariante;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class OtraVarianteDtoProfile : BaseProfile<OtraVariante, DetallesOtraVarianteDto, CrearOtraVarianteInputDto, ActualizarOtraVarianteInputDto, ListadoPaginadoOtraVarianteDto>

    {
        public OtraVarianteDtoProfile()
        {
            MapOtraVarianteDto();
            MapOtraVarianteListadoDto();
        }

        public void MapOtraVarianteDto()
        {
            CreateMap<OtraVariante, DetallesOtraVarianteDto>()
                .ReverseMap();
        }

        public void MapOtraVarianteListadoDto()
        {
            CreateMap<OtraVariante, ListadoPaginadoOtraVarianteDto>()
                .ReverseMap();
        }
    }
}
