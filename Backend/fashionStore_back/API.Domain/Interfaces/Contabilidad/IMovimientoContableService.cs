using API.Data.Entidades.Contabilidad;
using API.Domain.Validators.Contabilidad;

namespace API.Domain.Interfaces.Contabilidad
{
    public interface IMovimientoContableService : IBaseService<MovimientoContable, MovimientoContableValidator>
    {
    }
}