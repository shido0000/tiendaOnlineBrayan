using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class MonedaRepository : BaseRepository<Moneda>, IMoneda
    {
        public MonedaRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
