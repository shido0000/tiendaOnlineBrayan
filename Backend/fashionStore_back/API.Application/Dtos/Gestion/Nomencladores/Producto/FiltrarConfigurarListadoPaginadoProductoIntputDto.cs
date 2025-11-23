using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class FiltrarConfigurarListadoPaginadoProductoIntputDto : ConfiguracionListadoPaginadoDto
    {
        public bool? MostrarNovedades { get; set; }
        public bool? AgruparPorSKU { get; set; }
        public bool? EsProductosParaInventario { get; set; }
    }
}
