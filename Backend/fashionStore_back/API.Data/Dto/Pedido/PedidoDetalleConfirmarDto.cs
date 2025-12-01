using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class PedidoDetalleConfirmarDto
    {
        public Guid? Id { get; set; }                 // null → insertar nueva línea
        public Guid ProductoVarianteId { get; set; }  // clave foránea
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; }
        public decimal LineTotal { get; set; }
        public string EstadoLinea { get; set; } = "Pendiente";

    }
}
