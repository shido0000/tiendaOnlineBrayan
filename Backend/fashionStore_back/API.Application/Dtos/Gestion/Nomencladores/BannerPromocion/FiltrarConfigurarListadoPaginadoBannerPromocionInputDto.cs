using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.BannerPromocion
{
    public class FiltrarConfigurarListadoPaginadoBannerPromocionInputDto : ConfiguracionListadoPaginadoDto
    {
        public bool? Activo { get; set; } = true;
    }
}