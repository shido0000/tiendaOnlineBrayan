namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Producto : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; } = true;
        public string? SKU { get; set; }
        
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public Guid MonedaCostoId { get; set; }
        public Guid MonedaVentaId { get; set; }
        public Moneda MonedaCosto { get; set; } = null!;
        public Moneda MonedaVenta { get; set; } = null!;
        public int StockTotal { get; set; }
        public ICollection<ProductoCategoria> ProductoCategorias { get; set; } = new List<ProductoCategoria>();
        public ICollection<ProductoVariante> ProductosVariantes { get; set; } = new List<ProductoVariante>();
      //  public ICollection<ProductoFoto> Fotos { get; set; } = new List<ProductoFoto>();
        public ICollection<ProductoDescuento> ProductoDescuentos { get; set; } = new List<ProductoDescuento>();

    }
}
