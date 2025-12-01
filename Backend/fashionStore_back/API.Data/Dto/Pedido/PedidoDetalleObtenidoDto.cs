using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;

namespace API.Data.Dto.Pedido
{
    public class PedidoDetalleObtenidoDto
    {
        public Guid Id { get; set; }
        public string Descuento { get; set; }
        public ProductoVarianteObtenidoDto ProductoVarianteObtenidoDto { get; set; } = new();
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; } // valor aplicado
        public decimal LineTotal { get; set; }
        public EstadoLinea EstadoLinea { get; set; } = EstadoLinea.Pendiente;
    }
}
