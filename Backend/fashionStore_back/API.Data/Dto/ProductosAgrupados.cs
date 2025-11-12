using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductosAgrupados
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; }
        public string? SKU { get; set; }
        public List<string> Talla { get; set; }=new List<string>();
        public List<string> Color { get; set; } = new List<string>();
        public decimal PrecioVenta { get; set; }
        public string? MonedaVenta { get; set; }
        public int Stock { get; set; }
        // Categorías asociadas al producto
        public List<Guid> CategoriaIds { get; set; } = new();
        public List<IFormFile> Fotos { get; set; } = new();
    }
}
