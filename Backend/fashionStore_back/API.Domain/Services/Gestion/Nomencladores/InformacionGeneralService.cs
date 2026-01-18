using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class InformacionGeneralService : BasicService<InformacionGeneral, InformacionGeneralValidator>, IInformacionGeneralService
    {
        public InformacionGeneralService(IUnitOfWork<InformacionGeneral> unitOfWork, IHttpContextAccessor httpContext)
            : base(unitOfWork, httpContext)
        {
        }
    }
}