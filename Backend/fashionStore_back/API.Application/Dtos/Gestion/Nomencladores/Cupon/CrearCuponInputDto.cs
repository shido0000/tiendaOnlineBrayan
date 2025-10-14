using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Cupon
{
    public class CrearCuponInputDto : CuponDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
