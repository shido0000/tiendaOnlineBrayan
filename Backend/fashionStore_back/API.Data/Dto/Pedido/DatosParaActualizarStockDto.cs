using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Pedido
{
    public class DatosParaActualizarStockDto
    {
        public Guid IdDetalle { get; set; }
        public Guid IdProducto { get; set; }
        public int QueHago { get; set; } // 1-aumento la cantidad, 2-disminuyo la cantidad, 3-es igual
        public int Diferencia { get; set; }
    }
}
