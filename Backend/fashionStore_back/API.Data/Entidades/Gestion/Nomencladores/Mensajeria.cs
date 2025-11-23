using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Mensajeria : EntidadBase
    {
        public string Descripcion { get; set; } = string.Empty;
        public int? Precio { get; set; }  
        public Guid? MonedaId { get; set; }  
        public Moneda? Moneda { get; set; }  
    }
}
