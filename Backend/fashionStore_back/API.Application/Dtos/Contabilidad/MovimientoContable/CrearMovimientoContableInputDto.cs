using System.Text.Json.Serialization;

namespace API.Application.Dtos.Contabilidad.MovimientoContable
{
    public class CrearMovimientoContableInputDto : MovimientoContableDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
