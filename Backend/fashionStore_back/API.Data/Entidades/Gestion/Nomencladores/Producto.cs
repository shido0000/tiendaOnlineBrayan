namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Producto : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Color { get; set; }
        public string? SKU { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool EsActivo { get; set; } = true;

        public Guid MonedaId { get; set; }
        public Moneda Moneda { get; set; } = null!;


        public Inventario Inventario { get; set; } = null!;
        public ICollection<ProductoCategoria> ProductoCategorias { get; set; } = new List<ProductoCategoria>();
        public ICollection<ProductoDescuento> ProductoDescuentos { get; set; } = new List<ProductoDescuento>();
        // 👇 Nueva relación
        public ICollection<ProductoFoto> Fotos { get; set; } = new List<ProductoFoto>();
    }
}
