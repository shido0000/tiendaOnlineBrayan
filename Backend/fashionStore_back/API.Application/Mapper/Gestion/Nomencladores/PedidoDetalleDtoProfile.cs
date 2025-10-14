using API.Application.Dtos.Gestion.Nomencladores.PedidoDetalle;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class PedidoDetalleDtoProfile : BaseProfile<PedidoDetalle, PedidoDetalleDto, CrearPedidoDetalleInputDto, ActualizarPedidoDetalleInputDto, ListadoPaginadoPedidoDetalleDto>

    {
        public PedidoDetalleDtoProfile()
        {
            MapPedidoDetalleDto();
        }

        public void MapPedidoDetalleDto()
        {
            CreateMap<PedidoDetalle, DetallesPedidoDetalleDto>()
                .ReverseMap();
        }
    }
}
