namespace API.Data.Dto.Notificacion
{
    public class NotificacionPedidoDto
    {
        public string PedidoId { get; set; }
        public string Codigo { get; set; }
        public decimal Total { get; set; }
        public string Cliente { get; set; }
        public int CantidadProductos { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public List<ProductoNotificacionDto> Productos { get; set; } = new();
    }

  
}
