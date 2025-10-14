using API.Data.Entidades.Seguridad;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Annotations;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Pedido : EntidadBase
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public decimal Total { get; set; }
        public Guid MonedaId { get; set; }
        public Moneda Moneda { get; set; } = null!;
        public Guid? CuponId { get; set; }   // 🔑 relación opcional
        public Cupon? Cupon { get; set; }

        public ICollection<PedidoDetalle> Detalles { get; set; } = new List<PedidoDetalle>();
        public Venta? Venta { get; set; }
    }
}
