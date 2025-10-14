using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto
{
    public class CrearCategoriaProductoInputDto : CategoriaProductoDto
    {
        [JsonIgnore]
        public new Guid Id { get; set; }
        
    }
}
