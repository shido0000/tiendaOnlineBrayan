using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class VentaDetalle : EntidadBase
    {
        public Guid VentaId { get; set; }
        public Venta Venta { get; set; } = null!;
        public Guid ProductoVarianteId { get; set; }
        public ProductoVariante ProductoVariante { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoAplicado { get; set; }
    }
}
