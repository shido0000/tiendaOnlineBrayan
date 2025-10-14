using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Pedido
{
    public class CrearPedidoInputDto : PedidoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
    }
}
