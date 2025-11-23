using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductoVarianteAActualizar
    {
        public Guid? ProductoId { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public List<Guid> OtrasVariantesIds { get; set; } = new ();
        public int Stock { get; set; }
        public bool Principal { get; set; }
        // Fotos nuevas (archivos subidos)
        public List<IFormFile> FotosNuevas { get; set; } = new();
        // Fotos existentes que se mantienen (IDs)
        public List<Guid> FotosExistentesIds { get; set; } = new();
    }
}
