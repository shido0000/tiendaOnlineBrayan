namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ProductoVariante : EntidadBase
    {
        public Guid? ProductoId { get; set; }  
        public Producto? Producto { get; set; }  
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public List<OtraVarianteProductoVariante> OtraVarianteProductoVariantes { get; set; } = new List<OtraVarianteProductoVariante>();
        public int Stock { get; set; }
        public bool Principal { get; set; }
        public ICollection<ProductoFoto> Fotos { get; set; } = new List<ProductoFoto>();
        public ICollection<PedidoDetalle> PedidosDetalles { get; set; } = new List<PedidoDetalle>();
    }
}
