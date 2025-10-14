using API.Data.Entidades.Seguridad;

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

    }
}
