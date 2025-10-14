using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Contabilidad
{
    public class MovimientoContableService : BasicService<MovimientoContable, MovimientoContableValidator>, IMovimientoContableService
    {

        public MovimientoContableService(IUnitOfWork<MovimientoContable> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }
    }
}