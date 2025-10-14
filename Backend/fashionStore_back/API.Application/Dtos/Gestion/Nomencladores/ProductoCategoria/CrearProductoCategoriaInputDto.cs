using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.ProductoCategoria
{
    public class CrearProductoCategoriaInputDto : ProductoCategoriaDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
    }
}
