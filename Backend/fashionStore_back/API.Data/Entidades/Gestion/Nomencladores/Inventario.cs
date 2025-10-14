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
    public class Inventario : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int CantidadDisponible { get; set; }
        public int CantidadReservada { get; set; }
        public string? Ubicacion { get; set; }
        public EstadoProductoInventario EstadoProductoInventario { get; set; } = EstadoProductoInventario.Disponible;
    }
}
