using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class InventarioRepository : BaseRepository<Inventario>, IInventario
    {
        public InventarioRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
