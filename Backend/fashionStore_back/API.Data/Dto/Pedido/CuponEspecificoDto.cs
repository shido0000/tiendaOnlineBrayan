using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class CuponEspecificoDto
    {
        public Guid Id { get; set; }
        public bool EsMontoFijo { get; set; }
        public decimal Valor { get; set; }

    }
}
