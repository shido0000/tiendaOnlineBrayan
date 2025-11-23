using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class GestorRepository : BaseRepository<Gestor>, IGestor
    {
        public GestorRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
