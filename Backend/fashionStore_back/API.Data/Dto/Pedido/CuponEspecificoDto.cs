using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class CuponEspecificoDto
    {
        public Guid Id { get; set; }
        public bool EsMontoFijo { get; set; }
        public decimal Valor { get; set; }
        public int UsosActuales { get; set; }
        public int MaximoUsos { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
