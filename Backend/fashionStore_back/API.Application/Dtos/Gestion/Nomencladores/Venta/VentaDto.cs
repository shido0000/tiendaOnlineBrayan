using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;

namespace API.Application.Dtos.Gestion.Nomencladores.Venta
{
    public class VentaDto : EntidadBaseDto
    {
        public Guid PedidoId { get; set; }
        public Guid UsuarioVendedorId { get; set; }
        public DateTime FechaConfirmacion { get; set; }
        public decimal TotalFinal { get; set; }
    }
}
