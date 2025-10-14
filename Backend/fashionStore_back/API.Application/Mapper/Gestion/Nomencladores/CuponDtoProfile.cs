using API.Application.Dtos.Gestion.Nomencladores.Cupon;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class CuponDtoProfile : BaseProfile<Cupon, CuponDto, CrearCuponInputDto, ActualizarCuponInputDto, ListadoPaginadoCuponDto>

    {
        public CuponDtoProfile()
        {
            //   MapCuponDto();
            MapDetallesCuponDto();
        }


        public void MapDetallesCuponDto()
        {
            CreateMap<Cupon, DetallesCuponDto>()
              .ReverseMap()
            ;
        }

         
    }
}
