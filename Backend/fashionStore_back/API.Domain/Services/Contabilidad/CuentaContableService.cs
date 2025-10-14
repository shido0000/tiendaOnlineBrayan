using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Contabilidad
{
    public class CuentaContableService : BasicService<CuentaContable, CuentaContableValidator>, ICuentaContableService
    {

        public CuentaContableService(IUnitOfWork<CuentaContable> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }
    }
}