using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Moneda
{
    public class MonedaDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal TasaCambio { get; set; }
        public bool EsActiva { get; set; }

    }
}
