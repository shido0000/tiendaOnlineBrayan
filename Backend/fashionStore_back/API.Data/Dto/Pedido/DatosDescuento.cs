using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class DatosDescuento
    {
        public  Guid DescuentoId { get; set; }
   
        public decimal Valor { get; set; }  
        public bool EsMontoFijo { get; set; }  
    
    }
}
