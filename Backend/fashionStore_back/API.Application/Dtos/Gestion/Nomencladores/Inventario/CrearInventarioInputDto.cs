using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Inventario
{
    public class CrearInventarioInputDto : InventarioDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

        [JsonIgnore]
        public new Guid ProductoId { get; set; }
        public List<Guid> ProductoIds { get; set; } = new();

    }
}
