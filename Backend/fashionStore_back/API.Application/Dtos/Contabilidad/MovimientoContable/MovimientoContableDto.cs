using API.Application.Dtos.Comunes;
using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Seguridad;

namespace API.Application.Dtos.Contabilidad.MovimientoContable
{
    public class MovimientoContableDto : EntidadBaseDto
    {
        public Guid AsientoContableId { get; set; }
        public Guid CuentaContableId { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}
