using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class ProductoCantidadDto
    {
        public Guid Id { get; set; }
        public int Cantidad { get; set; }
        public int QueHago { get; set; }

    }
}
