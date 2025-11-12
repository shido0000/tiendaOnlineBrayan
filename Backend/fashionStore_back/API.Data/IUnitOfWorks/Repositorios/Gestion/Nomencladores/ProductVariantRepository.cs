using API.Data.DbContexts;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;

namespace API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores
{
    public class ProductVariantRepository : BaseRepository<ProductVariant>, IProductVariant
    {
        public ProductVariantRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
