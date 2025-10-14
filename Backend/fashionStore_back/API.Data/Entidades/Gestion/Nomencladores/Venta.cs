using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Venta : EntidadBase
    {
        public int Consecutivo { get; set; }
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public Guid UsuarioVendedorId { get; set; }
        public Usuario UsuarioVendedor { get; set; } = null!;
        public DateTime FechaConfirmacion { get; set; }
        public decimal TotalFinal { get; set; }

        public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
