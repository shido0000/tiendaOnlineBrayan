using API.Application.Dtos.Gestion.Nomencladores.Producto;

namespace API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto
{
    public class DetallesCategoriaProductoDto : CategoriaProductoDto
    {
        public List<ProductoDto> ListadoDeProductos = new();
    }
}
