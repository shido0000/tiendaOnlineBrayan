using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductVariantDto : EntidadBaseDto
    {
        public Guid? ProductoId { get; set; }
        public string? SKU { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }

        public Guid MonedaCostoId { get; set; }
        public Guid MonedaVentaId { get; set; }

        public int Stock { get; set; }

        // Fotos asociadas a la variante
       // public List<ProductoFotoDto> Fotos { get; set; } = new();
        public List<IFormFile> Fotos { get; set; } = new();
    }
}
