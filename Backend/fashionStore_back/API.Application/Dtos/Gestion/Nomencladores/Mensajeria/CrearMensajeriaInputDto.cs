using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Mensajeria
{
    public class CrearMensajeriaInputDto : MensajeriaDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
