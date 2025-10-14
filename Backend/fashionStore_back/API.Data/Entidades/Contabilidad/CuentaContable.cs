using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Contabilidad
{
    public class CuentaContable : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty; // Ej: 1.1.01
        public string Nombre { get; set; } = string.Empty; // Ej: Caja, Inventario
        public bool EsActivo { get; set; } = true;
        public bool EsDeMovimiento { get; set; } = true; // si admite asientos
        public Guid? CuentaPadreId { get; set; }
        public CuentaContable? CuentaPadre { get; set; }
        public ICollection<CuentaContable> SubCuentas { get; set; } = new List<CuentaContable>();

    }
}
