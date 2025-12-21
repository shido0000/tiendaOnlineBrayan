using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.BannerPromocion
{
    public class BannerPromocionDto : EntidadBaseDto
    {
        public required string Nombre { get; set; }
        public required string Imagen { get; set; }
        public string? TextoTitulo { get; set; }
        public string? TextoSubtitulo { get; set; }
        public string? BotonTexto { get; set; }
        public string? BotonVinculo { get; set; }
        public bool EsActivo { get; set; }
        public bool Destacado { get; set; }
        public int Orden { get; set; }
        public required string Ubicaciones { get; set; }
        public string? Dispositivos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}