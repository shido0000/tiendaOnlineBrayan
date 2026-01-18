using API.Data.Entidades.Seguridad;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Data.Entidades.Contabilidad
{
    public class MovimientoContable : EntidadBase
    {
        public Guid AsientoContableId { get; set; }
        public AsientoContable Asiento { get; set; } = null!;
        public Guid CuentaContableId { get; set; }
        public CuentaContable Cuenta { get; set; } = null!;
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        
        // Moneda en la que se realiza el movimiento
        public Guid MonedaId { get; set; }
        public Moneda Moneda { get; set; } = null!;
    }
}
