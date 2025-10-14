using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Descuento
{
    public class CrearDescuentoInputDto : DescuentoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

        public List<Guid> ProductosIds { get; set; } = new();
    }
}
