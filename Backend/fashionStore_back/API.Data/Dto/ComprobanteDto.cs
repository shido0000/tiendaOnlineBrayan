using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Dto
{
    public class ComprobanteDto
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public List<LineaDto> Lineas { get; set; } = new();
        public decimal Total { get; set; }
    }
}
