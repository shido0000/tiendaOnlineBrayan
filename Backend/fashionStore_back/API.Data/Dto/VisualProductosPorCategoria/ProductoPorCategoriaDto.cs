namespace API.Data.Dto.VisualProductosPorCategoria
{
    public class ProductoPorCategoriaDto
    {
        public Guid ProductoId { get; set; } 
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Color { get; set; }
        public string? SKU { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Moneda { get; set; } = "-";
        public string Estado { get; set; } = "-";
        public int CantidadDisponible { get; set; }
        public List<Guid> CategoriaIds { get; set; } = new();

        // 👇 Lista de URLs de fotos
        public List<string> Fotos { get; set; } = new();
    }
}
