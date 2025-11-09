namespace API.Data.Dto
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
