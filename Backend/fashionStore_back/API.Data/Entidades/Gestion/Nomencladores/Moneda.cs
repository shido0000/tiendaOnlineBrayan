using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Moneda : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal TasaCambio { get; set; }
        public bool EsActiva { get; set; } = true;

        public ICollection<Producto> ProductosCosto { get; set; } = new List<Producto>();
        public ICollection<Producto> ProductosVentas { get; set; } = new List<Producto>();
    }
}
