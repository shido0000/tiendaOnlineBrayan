using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class GestorService : BasicService<Gestor, GestorValidator>, IGestorService
    {

        public GestorService(IUnitOfWork<Gestor> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }
    }
}