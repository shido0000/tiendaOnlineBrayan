using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Moneda
{
    public class CrearMonedaInputDto : MonedaDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

        [JsonIgnore]
        public new bool EsActiva { get; set; }

    }
}
