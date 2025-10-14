using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;

namespace API.Application.Dtos.Gestion.Nomencladores.PedidoDetalle
{
    public class PedidoDetalleDto : EntidadBaseDto
    {
        public Guid PedidoId { get; set; }
        public Guid ProductoId { get; set; }
        public Guid? DescuentoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; } // valor aplicado
    }
}
