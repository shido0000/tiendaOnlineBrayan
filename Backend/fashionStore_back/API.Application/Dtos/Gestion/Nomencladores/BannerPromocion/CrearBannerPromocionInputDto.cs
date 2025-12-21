using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.BannerPromocion
{
    public class CrearBannerPromocionInputDto : BannerPromocionDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
    }
}