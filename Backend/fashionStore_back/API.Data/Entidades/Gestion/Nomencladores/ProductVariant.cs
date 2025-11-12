namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ProductVariant : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = default!;
        public string? SKU { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public Guid MonedaCostoId { get; set; }
        public Guid MonedaVentaId { get; set; }
        public Moneda MonedaCosto { get; set; } = null!;
        public Moneda MonedaVenta { get; set; } = null!;
        public int Stock { get; set; }

        public ICollection<ProductoFoto> Fotos { get; set; } = new List<ProductoFoto>();
        public ICollection<ProductoDescuento> ProductoDescuentos { get; set; } = new List<ProductoDescuento>();
       
    }
}
