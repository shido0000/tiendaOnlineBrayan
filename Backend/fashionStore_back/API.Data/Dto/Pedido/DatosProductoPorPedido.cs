using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class DatosProductoPorPedido
    {
        public  Guid ProductoId { get; set; }
   
        public int Cantidad { get; set; }  
    
    }
}
