using API.Application.Dtos.Gestion.Nomencladores.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class PedidoDtoProfile : BaseProfile<Pedido, PedidoDto, CrearPedidoInputDto, ActualizarPedidoInputDto, ListadoPaginadoPedidoDto>

    {
        public PedidoDtoProfile()
        {
            MapDetallesPedidoDto();
            MapListaPedidoDto();
        }


        public void MapDetallesPedidoDto()
        {
            CreateMap<Pedido, DetallesPedidoDto>()
            .ForMember(dto => dto.Usuario, opt => opt.MapFrom(e => e.Usuario.NombreCompleto))
            .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo))
            .ForMember(dto => dto.CuponCodigo, opt => opt.MapFrom(e => e.Cupon.Codigo))
            .ReverseMap()
            ;
        }

        public void MapListaPedidoDto()
        {
            CreateMap<Pedido, ListadoPaginadoPedidoDto>()
            .ForMember(dto => dto.Usuario, opt => opt.MapFrom(e => e.Usuario.NombreCompleto))
            .ForMember(dto => dto.MonedaCodigo, opt => opt.MapFrom(e => e.Moneda.Codigo))
            .ForMember(dto => dto.CuponCodigo, opt => opt.MapFrom(e => e.Cupon.Codigo))
            .ReverseMap()
            ;
        }
    }
}
