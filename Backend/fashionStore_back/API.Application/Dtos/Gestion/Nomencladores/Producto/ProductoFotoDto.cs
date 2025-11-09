using API.Application.Dtos.Gestion.Nomencladores.ProductVariant;
using System.Text.Json.Serialization;

namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class ProductoFotoDto
    {
        public Guid ProductVariantId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
    }
}
