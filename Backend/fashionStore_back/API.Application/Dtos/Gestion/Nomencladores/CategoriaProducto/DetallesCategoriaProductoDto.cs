using API.Application.Dtos.Gestion.Nomencladores.Producto;

namespace API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto
{
    public class DetallesCategoriaProductoDto : CategoriaProductoDto
    {
        // Cambio de campo a propiedad para que AutoMapper pueda mapearlo correctamente
        public List<ProductoDto> ListadoDeProductos { get; set; } = new();
    }
}
