using API.Application.Dtos.Contabilidad.MovimientoContable;
using API.Data.Entidades.Contabilidad;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using AutoMapper;

namespace API.Application.Controllers.Contabilidad
{

    public class MovimientoContableController : BasicController<MovimientoContable, MovimientoContableValidator, DetallesMovimientoContableDto, CrearMovimientoContableInputDto, ActualizarMovimientoContableInputDto, ListadoPaginadoMovimientoContableDto, FiltrarConfigurarListadoPaginadoMovimientoContableIntputDto>
    {
        private readonly IMovimientoContableService _MovimientoContableService;

        public MovimientoContableController(IMapper mapper, IMovimientoContableService servicioMovimientoContable, IMovimientoContableService MovimientoContableService) : base(mapper, servicioMovimientoContable)
        {
            _MovimientoContableService = MovimientoContableService;
        }
    }
}
