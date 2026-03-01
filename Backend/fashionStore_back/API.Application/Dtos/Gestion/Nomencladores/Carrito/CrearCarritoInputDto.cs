using API.Application.Dtos.Gestion.Nomencladores.CarritoDetalle;
using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Carrito
{
    public class CrearCarritoInputDto : CarritoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        public ICollection<CrearCarritoDetalleInputDto> Detalles { get; set; } = new List<CrearCarritoDetalleInputDto>();

    }
}
