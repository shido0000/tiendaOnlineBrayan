using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto
{
    public class CategoriaProductoDto : EntidadBaseDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? FotoUrl { get; set; }
    }
}
