using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class OtraVarianteRepository : BaseRepository<OtraVariante>, IOtraVariante
    {
        public OtraVarianteRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
