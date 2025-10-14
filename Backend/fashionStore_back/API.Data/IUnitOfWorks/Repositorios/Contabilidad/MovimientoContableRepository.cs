using API.Data.DbContexts;
using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces.Contabilidad;

namespace API.Data.IUnitOfWorks.Repositorios.Contabilidad
{
    public class MovimientoContableRepository : BaseRepository<MovimientoContable>, IMovimientoContable
    {
        public MovimientoContableRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
