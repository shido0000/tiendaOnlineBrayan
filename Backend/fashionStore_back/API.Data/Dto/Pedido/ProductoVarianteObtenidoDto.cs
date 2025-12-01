using API.Data.Enum;

namespace API.Data.Dto.Pedido
{
    public class ProductoVarianteObtenidoDto
    {
        public Guid Id { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public string nombreProducto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public decimal PrecioVenta { get; set; }

    }
}
