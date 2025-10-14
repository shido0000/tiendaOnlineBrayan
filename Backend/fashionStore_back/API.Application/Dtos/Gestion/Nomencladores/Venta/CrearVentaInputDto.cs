using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Venta
{
    public class CrearVentaInputDto : VentaDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

    }
}
