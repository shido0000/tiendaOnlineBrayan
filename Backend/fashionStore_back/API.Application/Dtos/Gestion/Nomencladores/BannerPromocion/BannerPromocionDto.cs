using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.BannerPromocion
{
    public class BannerPromocionDto : EntidadBaseDto
    {
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
    }
}