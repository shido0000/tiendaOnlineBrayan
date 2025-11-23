using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class ReviewRepository : BaseRepository<Review>, IReview
    {
        public ReviewRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
