namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class BannerPromocion : EntidadBase
    {
        // Texto
        public string? TextoTitulo { get; set; }
        public string? TextoSubtitulo { get; set; }
        public string? TextoBoton { get; set; }
        // Imagen
        public string Imagen { get; set; } = string.Empty; // ruta o URL

        // Control
        public bool EsActivo { get; set; } = true;
        public bool Rebajas { get; set; } = false;

        // Ubicaciones (separadas por coma: "home,productos,categorias")
        public Guid? CategoriaProductoId { get; set; }
        public CategoriaProducto? CategoriaProducto { get; set; }
    }
}