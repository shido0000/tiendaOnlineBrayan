using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.ProductVariant
{
    public class FiltrarConfigurarListadoPaginadoProductVariantIntputDto : ConfiguracionListadoPaginadoDto
    {
        public bool? MostrarNovedades { get; set; }
        public bool? EsProductVariantsParaInventario { get; set; }
    }
}
