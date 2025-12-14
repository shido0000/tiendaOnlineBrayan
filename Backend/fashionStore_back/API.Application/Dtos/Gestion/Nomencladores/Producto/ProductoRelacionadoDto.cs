using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class ProductoRelacionadoDto
    {
        public Guid ProductoActualId { get; set; }
        public List<Guid> CategoriasIds { get; set; } = new();

    }
}
