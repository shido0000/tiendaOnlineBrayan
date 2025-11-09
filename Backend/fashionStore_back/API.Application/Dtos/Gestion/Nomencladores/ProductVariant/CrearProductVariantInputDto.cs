using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.ProductVariant
{
    public class CrearProductVariantInputDto : ProductVariantDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        [JsonIgnore]
        public new Guid? ProductoId { get; set; }

      //  public List<IFormFile> Fotos { get; set; } = new();

    }
}
