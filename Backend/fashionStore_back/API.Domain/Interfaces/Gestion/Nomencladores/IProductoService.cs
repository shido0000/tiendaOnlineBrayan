using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface IProductoService : IBaseService<Producto, ProductoValidator>
    {
        Task<Guid> CrearAsync(CrearProductoInputDto dto);
        Task<Guid> ActualizarAsync(Guid id, ActualizarProductoInputDto dto);
        Task<List<Producto>> ObtenerProductosNovedades();
        Task<Producto> ObtenerProductoEspecifico(Guid id);
        Task<List<Producto>> ObtenerProductoPorCategoriaEspecifico(List<Guid> categoriaIds);
    }
}