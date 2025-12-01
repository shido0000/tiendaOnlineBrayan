using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class OtraVarianteProductoVariante : EntidadBase
    {
        public Guid? OtraVarianteId { get; set; }
        public OtraVariante? OtraVariante { get; set; }
        public Guid? ProductoVarianteId { get; set; } 
        public ProductoVariante? ProductoVariante { get; set; } 
    }
}
