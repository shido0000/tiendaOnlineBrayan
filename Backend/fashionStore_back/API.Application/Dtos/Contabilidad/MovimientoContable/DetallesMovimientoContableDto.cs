namespace API.Application.Dtos.Contabilidad.MovimientoContable
{
    public class DetallesMovimientoContableDto : MovimientoContableDto
    {
        public required string CodigoCuenta { get; set; }
        public required string NombreCuenta { get; set; }
    }
}
