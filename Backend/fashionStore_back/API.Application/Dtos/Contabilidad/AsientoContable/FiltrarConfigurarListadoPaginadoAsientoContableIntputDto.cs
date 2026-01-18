using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Contabilidad.AsientoContable
{
    public class FiltrarConfigurarListadoPaginadoAsientoContableIntputDto : ConfiguracionListadoPaginadoDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public Guid? Cuenta { get; set; }
    }
}
