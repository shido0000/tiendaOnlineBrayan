using API.Application.Dtos.Gestion.Nomencladores.VentaDetalle;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class VentaDetalleDtoProfile : BaseProfile<VentaDetalle, VentaDetalleDto, CrearVentaDetalleInputDto, ActualizarVentaDetalleInputDto, ListadoPaginadoVentaDetalleDto>

    {
        public VentaDetalleDtoProfile()
        {
            MapVentaDetalleDto();
        }

        public void MapVentaDetalleDto()
        {
            CreateMap<VentaDetalle, DetallesVentaDetalleDto>()
             .ReverseMap();
        }
    }
}
