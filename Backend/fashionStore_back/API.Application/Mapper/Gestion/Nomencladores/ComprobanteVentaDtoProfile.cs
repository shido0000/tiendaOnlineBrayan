using API.Application.Dtos.Gestion.Nomencladores.ComprobanteVenta;
using API.Application.Dtos.Seguridad.Rol;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class ComprobanteVentaDtoProfile : BaseProfile<ComprobanteVenta, ComprobanteVentaDto, CrearComprobanteVentaInputDto, ActualizarComprobanteVentaInputDto, ListadoPaginadoComprobanteVentaDto>

    {
        public ComprobanteVentaDtoProfile()
        {
            //   MapComprobanteVentaDto();
            MapDetallesComprobanteVentaDto();
        }


        public void MapDetallesComprobanteVentaDto()
        {
            CreateMap<ComprobanteVenta, DetallesComprobanteVentaDto>()
              .ReverseMap()
            ;
        }

         
    }
}
