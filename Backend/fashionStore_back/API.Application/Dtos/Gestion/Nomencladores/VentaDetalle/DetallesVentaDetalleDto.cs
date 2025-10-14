namespace API.Application.Dtos.Gestion.Nomencladores.VentaDetalle
{
    public class DetallesVentaDetalleDto : VentaDetalleDto
    {
        public required string ProductoCodigo { get; set; }
        public required string ProductoDescripcion { get; set; }
    }
}
