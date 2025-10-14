using System.Text.Json.Serialization;

namespace API.Application.Dtos.Contabilidad.CuentaContable
{
    public class CrearCuentaContableInputDto : CuentaContableDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
