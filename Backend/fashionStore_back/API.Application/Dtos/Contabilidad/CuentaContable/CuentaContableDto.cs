using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Contabilidad.CuentaContable
{
    public class CuentaContableDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty; // Ej: 1.1.01
        public string Nombre { get; set; } = string.Empty; // Ej: Caja, Inventario
        public bool EsActivo { get; set; } = true;
        public bool EsDeMovimiento { get; set; } = true; // si admite asientos
        public Guid? CuentaPadreId { get; set; }
    }
}
