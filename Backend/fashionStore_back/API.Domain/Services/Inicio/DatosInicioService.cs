using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Inicio;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Interfaces.Inicio;
using Microsoft.AspNetCore.Http;


namespace API.Domain.Services.Inicio
{
    public class DatosInicioService : IDatosInicioService
    {
        readonly IProductoService _productoService;
        readonly ICategoriaProductoService _categoriaProductoService;

        public DatosInicioService(IUnitOfWork<Producto> repositorios, IHttpContextAccessor httpContext, IProductoService productoService, ICategoriaProductoService categoriaProductoService)
        {
            _productoService = productoService;
            _categoriaProductoService = categoriaProductoService;
        }


        public async Task<DatosInicio> ObtenerDatosInicio()
        {
            var productosNovedades = await _productoService.ObtenerProductosNovedades();
            var categorias = (await _categoriaProductoService.ObtenerTodos()).ToList();

            return new DatosInicio()
            {
                ProductosNovedades = productosNovedades,
                CategoriasProductos = categorias
            };
        }

    }
}