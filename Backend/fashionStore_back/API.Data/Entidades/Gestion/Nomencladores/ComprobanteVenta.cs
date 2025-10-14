using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ComprobanteVenta : EntidadBase
    {
        public Guid VentaId { get; set; }
        public Venta Venta { get; set; } = null!;
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public string Numero { get; set; } = string.Empty; // Ej: FAC-2025-0001

    }
}
