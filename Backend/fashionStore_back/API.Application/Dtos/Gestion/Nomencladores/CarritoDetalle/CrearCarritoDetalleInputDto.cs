using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.CarritoDetalle
{
    public class CrearCarritoDetalleInputDto : CarritoDetalleDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
