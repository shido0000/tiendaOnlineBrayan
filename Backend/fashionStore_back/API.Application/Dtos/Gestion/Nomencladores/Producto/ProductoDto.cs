using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.ProductVariant;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class ProductoDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; }

        // Categorías asociadas al producto
        public List<Guid> CategoriaIds { get; set; } = new();

        // Variantes del producto
        public List<ProductVariantDto> Variants { get; set; } = new();
    }
}
