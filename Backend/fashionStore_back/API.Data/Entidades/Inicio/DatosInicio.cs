using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Data.Entidades.Inicio
{
    public class DatosInicio
    {
        public List<Producto> ProductosNovedades { get; set; } = new();
        public List<CategoriaProducto> CategoriasProductos { get; set; } = new();

    }
}
