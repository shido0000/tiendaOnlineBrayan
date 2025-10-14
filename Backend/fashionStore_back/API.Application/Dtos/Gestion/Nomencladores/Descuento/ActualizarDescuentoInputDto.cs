using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Descuento
{
    public class ActualizarDescuentoInputDto : DescuentoDto
    {
        public List<Guid> ProductosIds { get; set; } = new();
    }
}
