using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface ICategoriaProductoService : IBaseService<CategoriaProducto, CategoriaProductoValidator>
    {
        Task<Guid> CrearCategoriaAsync(CategoriaProducto dto, IFormFile? foto);
        Task<Guid> ActualizarCategoriaAsync(Guid id, CategoriaProducto dto, IFormFile? foto);
    }
}