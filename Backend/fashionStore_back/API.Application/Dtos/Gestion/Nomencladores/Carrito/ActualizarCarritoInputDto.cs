using API.Application.Dtos.Gestion.Nomencladores.CarritoDetalle;
using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Carrito
{
    public class ActualizarCarritoInputDto : CarritoDto
    {
        public ICollection<ActualizarCarritoDetalleInputDto> Detalles { get; set; } = new List<ActualizarCarritoDetalleInputDto>();

    }
}
