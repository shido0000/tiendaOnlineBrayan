using API.Data.Dto.VisualProductosPorCategoria;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface IInventarioService : IBaseService<Inventario, InventarioValidator>
    {
        Task<List<ProductoPorCategoriaDto>> ObtenerProductosDelInventarioPorCategoria(Guid categoriaId);
    }
}