using System.Text.Json.Serialization;

namespace API.Application.Dtos.Contabilidad.AsientoContable
{
    public class CrearAsientoContableInputDto : AsientoContableDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
