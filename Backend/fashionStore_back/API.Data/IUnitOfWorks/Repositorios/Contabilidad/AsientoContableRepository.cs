using API.Data.DbContexts;
using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces.Contabilidad;

namespace API.Data.IUnitOfWorks.Repositorios.Contabilidad
{
    public class AsientoContableRepository : BaseRepository<AsientoContable>, IAsientoContable
    {
        public AsientoContableRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
