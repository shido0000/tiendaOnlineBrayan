using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Contabilidad
{
    public class AsientoContable : EntidadBase
    {
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Descripcion { get; set; } = string.Empty;
        public Guid ReferenciaId { get; set; } // Ej: VentaId
        public string TipoReferencia { get; set; } = string.Empty; // "Venta", "Devolucion"
        public ICollection<MovimientoContable> Movimientos { get; set; } = new List<MovimientoContable>();

    }
}
