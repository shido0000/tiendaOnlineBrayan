using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Contabilidad
{
    public class AsientoContableService : BasicService<AsientoContable, AsientoContableValidator>, IAsientoContableService
    {

        public AsientoContableService(IUnitOfWork<AsientoContable> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }
    }
}