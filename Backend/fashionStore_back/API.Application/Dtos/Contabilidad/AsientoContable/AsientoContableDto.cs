using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Contabilidad.AsientoContable
{
    public class AsientoContableDto : EntidadBaseDto
    {
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Descripcion { get; set; } = string.Empty;
        public Guid ReferenciaId { get; set; } // Ej: VentaId
        public string TipoReferencia { get; set; } = string.Empty; // "Venta", "Devolucion"
    }
}
