using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;
using API.Data.Enum;

namespace API.Application.Dtos.Gestion.Nomencladores.Pedido
{
    public class PedidoDto : EntidadBaseDto
    {
        public Guid UsuarioId { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public decimal Total { get; set; }
        public Guid MonedaId { get; set; }
    }
}
