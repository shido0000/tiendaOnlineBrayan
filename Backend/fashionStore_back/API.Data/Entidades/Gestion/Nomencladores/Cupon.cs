using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Cupon : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty; // Ej: "BLACKFRIDAY"
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Porcentaje { get; set; }
        public decimal? MontoFijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsActivo { get; set; } = true;
        public int MaximoUsos { get; set; } = 0; // 0 = ilimitado
        public int UsosActuales { get; set; } = 0;
        public decimal? MontoMinimoPedido { get; set; } // opcional

    }
}
