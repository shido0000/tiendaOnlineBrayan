using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;

namespace API.Application.Dtos.Gestion.Nomencladores.ProductoCategoria
{
    public class ProductoCategoriaDto : EntidadBaseDto
    {
        public Guid ProductoId { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
