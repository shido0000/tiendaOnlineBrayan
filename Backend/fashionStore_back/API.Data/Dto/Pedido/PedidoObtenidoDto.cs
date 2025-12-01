using API.Data.Enum;

namespace API.Data.Dto.Pedido
{
    public class PedidoObtenidoDto
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Usuario { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioGestor { get; set; }
        public string Moneda { get; set; }
        public string Cupon { get; set; }
        public string Direccion { get; set; }
        public List<PedidoDetalleObtenidoDto> Detalles { get; set; } = new();
    }
}
