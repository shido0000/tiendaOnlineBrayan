using API.Data.Entidades.Contabilidad;
using API.Domain.Validators.Contabilidad;

namespace API.Domain.Interfaces.Contabilidad
{
    public interface IAsientoContableService : IBaseService<AsientoContable, AsientoContableValidator>
    {
    }
}