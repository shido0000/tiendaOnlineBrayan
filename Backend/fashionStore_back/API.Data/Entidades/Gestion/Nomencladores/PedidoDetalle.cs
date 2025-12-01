using API.Data.Entidades.Seguridad;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class PedidoDetalle : EntidadBase
    {
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public Guid ProductoVarianteId { get; set; }
        public ProductoVariante ProductoVariante { get; set; } = null!;
 
        public Guid? DescuentoId { get; set; }
        public Descuento? Descuento { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; } // valor aplicado
        public decimal LineTotal { get; set; }
        public EstadoLinea EstadoLinea { get; set; } = EstadoLinea.Pendiente;
    }
}
