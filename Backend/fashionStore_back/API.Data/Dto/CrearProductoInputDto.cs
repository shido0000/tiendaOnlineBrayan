using System.Text.Json.Serialization;

namespace API.Data.Dto
{
    public class CrearProductoInputDto : ProductoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        [JsonIgnore]
        public new bool EsActivo { get; set; }

    }
}
