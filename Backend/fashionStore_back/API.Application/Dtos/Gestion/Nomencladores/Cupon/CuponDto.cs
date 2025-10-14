using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Cupon
{
    public class CuponDto : EntidadBaseDto
    {
        public string Codigo { get; set; } = string.Empty; // Ej: "BLACKFRIDAY"
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Porcentaje { get; set; }
        public decimal? MontoFijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsActivo { get; set; } = true;
        public int MaximoUsos { get; set; } = 0; // 0 = ilimitado
        public int UsosActuales { get; set; } = 0;
        public decimal? MontoMinimoPedido { get; set; } // opcional
    }
}
