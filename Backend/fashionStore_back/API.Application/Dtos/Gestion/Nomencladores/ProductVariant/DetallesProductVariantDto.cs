namespace API.Application.Dtos.Gestion.Nomencladores.ProductVariant
{
    public class DetallesProductVariantDto : ProductVariantDto
    {
        public string MonedaCodigo { get; set; } = string.Empty;
        // Fotos asociadas a la variante (salida)
        public List<string> FotosExistentes { get; set; } = new();
    }
}
