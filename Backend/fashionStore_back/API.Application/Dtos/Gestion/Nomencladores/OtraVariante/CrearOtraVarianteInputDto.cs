using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.OtraVariante
{
    public class CrearOtraVarianteInputDto : OtraVarianteDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
