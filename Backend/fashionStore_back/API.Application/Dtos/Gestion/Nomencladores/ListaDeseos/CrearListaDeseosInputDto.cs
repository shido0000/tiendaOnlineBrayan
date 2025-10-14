using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.ListaDeseos
{
    public class CrearListaDeseosInputDto : ListaDeseosDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
