using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.VentaDetalle
{
    public class VentaDetalleDto : EntidadBaseDto
    {
        public Guid VentaId { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; }
    }
}
