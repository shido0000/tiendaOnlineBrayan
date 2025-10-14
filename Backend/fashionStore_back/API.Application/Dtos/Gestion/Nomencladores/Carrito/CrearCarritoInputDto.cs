using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Carrito
{
    public class CrearCarritoInputDto : CarritoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
