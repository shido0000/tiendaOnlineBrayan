using API.Application.Dtos.Comunes;
using API.Data.Entidades.Seguridad;

namespace API.Application.Dtos.Gestion.Nomencladores.ComprobanteVenta
{
    public class LineaDto
    {
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
