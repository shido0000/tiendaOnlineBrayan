namespace API.Data.Dto
{
    public class ProductoDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Color { get; set; }
        public string? SKU { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public Guid MonedaId { get; set; }
        public bool EsActivo { get; set; }
        public List<Guid> CategoriaIds { get; set; } = new();

        // 👇 Lista de URLs de fotos
        public List<string> Fotos { get; set; } = new();
    }
}
