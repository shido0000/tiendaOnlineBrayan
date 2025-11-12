using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class ReviewService :  IReviewService
    {

        public ReviewService(IUnitOfWork<Review> repositorios, IHttpContextAccessor httpContext) 
        {
        }

       
    }
}