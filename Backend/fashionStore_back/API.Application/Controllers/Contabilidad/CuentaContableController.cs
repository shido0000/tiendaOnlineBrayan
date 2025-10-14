using API.Application.Dtos.Contabilidad.CuentaContable;
using API.Data.Entidades.Contabilidad;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using AutoMapper;

namespace API.Application.Controllers.Contabilidad
{

    public class CuentaContableController : BasicController<CuentaContable, CuentaContableValidator, DetallesCuentaContableDto, CrearCuentaContableInputDto, ActualizarCuentaContableInputDto, ListadoPaginadoCuentaContableDto, FiltrarConfigurarListadoPaginadoCuentaContableIntputDto>
    {
        private readonly ICuentaContableService _CuentaContableService;

        public CuentaContableController(IMapper mapper, ICuentaContableService servicioCuentaContable, ICuentaContableService CuentaContableService) : base(mapper, servicioCuentaContable)
        {
            _CuentaContableService = CuentaContableService;
        }
    }
}
