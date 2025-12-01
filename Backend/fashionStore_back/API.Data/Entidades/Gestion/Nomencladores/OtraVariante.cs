using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class OtraVariante : EntidadBase
    {
        public string Nombre { get; set; } = string.Empty;
        public List<OtraVarianteProductoVariante> OtraVarianteProductoVariantes { get; set; } = new();
    }
}
