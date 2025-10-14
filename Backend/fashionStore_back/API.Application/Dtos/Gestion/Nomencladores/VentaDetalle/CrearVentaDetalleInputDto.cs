using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.VentaDetalle
{
    public class CrearVentaDetalleInputDto : VentaDetalleDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

    }
}
