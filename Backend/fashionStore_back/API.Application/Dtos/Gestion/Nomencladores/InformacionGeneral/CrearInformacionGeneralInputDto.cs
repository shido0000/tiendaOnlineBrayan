using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.InformacionGeneral
{
    public class CrearInformacionGeneralInputDto : InformacionGeneralDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
    }
}