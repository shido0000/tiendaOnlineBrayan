using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class GestorPedido : EntidadBase
    {
        public Guid? GestorId { get; set; } 
        public Gestor? Gestor { get; set; } 
        public Guid? PedidoId { get; set; } 
        public Pedido? Pedido { get; set; } 

    }
}
