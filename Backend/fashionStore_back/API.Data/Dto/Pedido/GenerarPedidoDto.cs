using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class GenerarPedidoDto
    {
        public List<DatosProductoPorPedido> Productos { get; set; } = new();
        public Guid UsuarioId { get; set; }  
        public Guid? GestorId { get; set; }  
        public int? ImpuestoGestor { get; set; }  
        public string Direccion { get; set; } = string.Empty;
        public Guid MensajeriaId { get; set; }
        public Guid CuponId { get; set; }
    }
}
