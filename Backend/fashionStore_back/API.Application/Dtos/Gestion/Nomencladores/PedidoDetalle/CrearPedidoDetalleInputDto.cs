using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.PedidoDetalle
{
    public class CrearPedidoDetalleInputDto : PedidoDetalleDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

    }
}
