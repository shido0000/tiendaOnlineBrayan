using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class PedidoConfirmarDto
    {
        public Guid Id { get; set; }              // Id del pedido (para actualizar)
        public int Codigo { get; set; }           // Código del pedido
        public string Usuario { get; set; }       // Usuario que hizo el pedido
        public string Estado { get; set; }        // Estado del pedido (Pendiente, Confirmado, etc.)
        public string Moneda { get; set; }        // Moneda (ej: "USD")
        public string? Cupon { get; set; }        // Cupón aplicado
        public string? Direccion { get; set; }    // Dirección de envío
        public decimal PrecioGestor { get; set; } // Precio del gestor
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        // 🔑 Array de líneas del pedido
        public List<PedidoDetalleConfirmarDto> Detalles { get; set; } = new();

    }
}
