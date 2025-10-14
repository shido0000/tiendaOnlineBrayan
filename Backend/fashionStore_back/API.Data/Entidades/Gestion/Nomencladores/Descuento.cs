using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Descuento : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal? Porcentaje { get; set; }
        public decimal? MontoFijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsActivo { get; set; } = true;

        public ICollection<ProductoDescuento> ProductoDescuentos { get; set; } = new List<ProductoDescuento>();

    }
}
