using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.ListaDeseosDetalle
{
    public class CrearListaDeseosDetalleInputDto : ListaDeseosDetalleDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
