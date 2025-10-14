using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.ComprobanteVenta
{
    public class CrearComprobanteVentaInputDto : ComprobanteVentaDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
