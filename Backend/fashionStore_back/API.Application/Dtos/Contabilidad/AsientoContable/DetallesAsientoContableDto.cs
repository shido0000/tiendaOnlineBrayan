using API.Application.Dtos.Contabilidad.MovimientoContable;

namespace API.Application.Dtos.Contabilidad.AsientoContable
{
    public class DetallesAsientoContableDto : AsientoContableDto
    {
       // public required string Consecutivo { get; set; }
        public List<DetallesMovimientoContableDto> Movimientos { get; set; } = new List<DetallesMovimientoContableDto>();

    }
}
