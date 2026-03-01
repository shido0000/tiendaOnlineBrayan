using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class PedidosPendientesDto
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }  
        public decimal Total { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
    }
}
