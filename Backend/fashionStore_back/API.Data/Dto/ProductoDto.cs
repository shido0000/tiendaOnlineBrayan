using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductoDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; }
        public string? SKU { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }

        public Guid MonedaCostoId { get; set; }
        public Guid MonedaVentaId { get; set; }

        public int Stock { get; set; }

        // Categorías asociadas al producto
        public List<Guid> CategoriaIds { get; set; } = new();

        //
        public List<IFormFile> Fotos { get; set; } = new();

    }
}
