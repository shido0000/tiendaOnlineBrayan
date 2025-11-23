using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface IProductoService : IBaseService<Producto, ProductoValidator>
    {
        Task<Guid> CrearProducto(ProductoACrear producto);
        Task<Guid> ActualizarProducto(Guid productoId, ProductoAActualizar producto);
        Task<ProductoEspecificoDto> ObtenerProductoEspecifico(Guid id);
        Task<List<Producto>> ObtenerProductosNovedades();
        //Task<List<ProductosAgrupados>> ObtenerProductosAgrupados();
        //Task<ProductosAgrupados?> ObtenerProductoAgrupadoPorSku(string sku);
    }
}