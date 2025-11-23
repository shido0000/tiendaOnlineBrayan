using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductoVarianteACrear
    {
        public Guid? ProductoId { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public List<Guid> OtrasVariantesIds { get; set; } = new ();
        public int Stock { get; set; }
        public bool Principal { get; set; }
        //public List<ProductoFotoDto> Fotos { get; set; } = new ();
        public List<IFormFile> Fotos { get; set; } = new ();
    }
}
