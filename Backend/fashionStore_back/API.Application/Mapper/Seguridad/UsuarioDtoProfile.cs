using API.Application.Dtos.Seguridad.Usuario;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Seguridad
{
    public class UsuarioDtoProfile : BaseProfile<Usuario, UsuarioDto, CrearUsuarioInputDto, ActualizarUsuarioInputDto, ListadoPaginadoUsuarioDto>
    {
        public UsuarioDtoProfile()
        {
            MapDetallesUsuarioDto();
            MapListadoUsuarioDto();
        }

        public void MapDetallesUsuarioDto()
        {
            CreateMap<Usuario, DetallesUsuarioDto>()
                 .ForMember(dto => dto.Rol, opt => opt.MapFrom(e => e.Rol.Nombre))
                 .ReverseMap();
        }

        public void MapListadoUsuarioDto()
        {
            CreateMap<Usuario, ListadoPaginadoUsuarioDto>()
                 .ForMember(dto => dto.Rol, opt => opt.MapFrom(e => e.Rol.Nombre))
                 .ReverseMap();
        }
    }
}


