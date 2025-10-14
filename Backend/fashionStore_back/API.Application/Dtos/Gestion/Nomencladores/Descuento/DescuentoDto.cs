using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Descuento
{
    public class DescuentoDto : EntidadBaseDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal? Porcentaje { get; set; }
        public decimal? MontoFijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsActivo { get; set; } = true;
    }
}
