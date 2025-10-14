using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Dto
{
    public class ResultadoDescuento
    {
        public decimal ValorAplicado { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // "Porcentaje" o "MontoFijo"
        public decimal? Porcentaje { get; set; }
        public decimal? MontoFijo { get; set; }
    }
}
