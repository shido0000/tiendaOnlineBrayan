using API.Application.Dtos.Comunes;
using API.Data.Entidades.Seguridad;

namespace API.Application.Dtos.Gestion.Nomencladores.ComprobanteVenta
{
    public class ComprobanteVentaDto : EntidadBaseDto
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public List<LineaDto> Lineas { get; set; } = new();
        public decimal Total { get; set; }
    }
}
