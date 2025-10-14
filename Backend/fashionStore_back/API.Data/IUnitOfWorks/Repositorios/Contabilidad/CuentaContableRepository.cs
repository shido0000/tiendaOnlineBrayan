using API.Data.DbContexts;
using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces.Contabilidad;

namespace API.Data.IUnitOfWorks.Repositorios.Contabilidad
{
    public class CuentaContableRepository : BaseRepository<CuentaContable>, ICuentaContable
    {
        public CuentaContableRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
