using API.Application.Dtos.Contabilidad.AsientoContable;
using API.Data.Entidades.Contabilidad;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using AutoMapper;

namespace API.Application.Controllers.Contabilidad
{

    public class AsientoContableController : BasicController<AsientoContable, AsientoContableValidator, DetallesAsientoContableDto, CrearAsientoContableInputDto, ActualizarAsientoContableInputDto, ListadoPaginadoAsientoContableDto, FiltrarConfigurarListadoPaginadoAsientoContableIntputDto>
    {
        private readonly IAsientoContableService _AsientoContableService;

        public AsientoContableController(IMapper mapper, IAsientoContableService servicioAsientoContable, IAsientoContableService AsientoContableService) : base(mapper, servicioAsientoContable)
        {
            _AsientoContableService = AsientoContableService;
        }
    }
}
