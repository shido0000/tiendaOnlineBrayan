using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class ProductoController : BasicController<Producto, ProductoValidator, DetallesProductoDto, CrearProductoInputDto, ActualizarProductoInputDto, ListadoPaginadoProductoDto, FiltrarConfigurarListadoPaginadoProductoIntputDto>
    {
        private readonly IProductoService _ProductoService;

        public ProductoController(IMapper mapper, IProductoService servicioProducto, IProductoService ProductoService) : base(mapper, servicioProducto)
        {
            _ProductoService = ProductoService;

        }


        protected override Task<(IEnumerable<Producto>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoProductoIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Producto, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Producto => Producto.Codigo.Contains(inputDto.TextoBuscar));

            if (inputDto.MostrarNovedades.HasValue && inputDto.MostrarNovedades.Value)
            {
                var hoy = DateTime.Today; // solo la fecha, sin hora
                var desde = hoy.AddDays(-5);

                filtros.Add(p => p.FechaCreado >= desde && p.FechaCreado <= hoy);
            }

            if (inputDto.EsProductosParaInventario.HasValue && inputDto.EsProductosParaInventario.Value)
            {
                // filtros.Add(p => p.Inventario == null);
            }


            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas: query => query.Include(e => e.ProductoCategorias), filtros.ToArray());
        }

        protected override async Task<Producto?> ObtenerElementoPorId(Guid id)
          => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.Fotos).Include(e => e.ProductoCategorias).ThenInclude(e => e.Categoria));


        [HttpPost("CrearConFotos")]
        public async Task<IActionResult> CrearConFotos(
    [FromForm] CrearProductoInputDto objeto, [FromForm] List<IFormFile> fotos)
        {
            var dtoData = new API.Data.Dto.CrearProductoInputDto
            {
                Codigo = objeto.Codigo,
                Descripcion = objeto.Descripcion,
                EsActivo = objeto.EsActivo,
                CategoriaIds = objeto.CategoriaIds,
                MonedaCostoId = objeto.MonedaCostoId,
                MonedaVentaId = objeto.MonedaVentaId,
                Talla = objeto.Talla,
                Color = objeto.Color,
                SKU = objeto.SKU,
                Stock = objeto.Stock,
            };

            var result = await _ProductoService.CrearAsync(dtoData, fotos);
            return Ok(result);
        }

        [HttpPut("ActualizarConFotos/{id}")]
        public async Task<IActionResult> ActualizarConFotos(
        Guid id,
        [FromForm] ActualizarProductoInputDto objeto,
        [FromForm] List<IFormFile> fotos)
        {
            var dtoData = new API.Data.Dto.ActualizarProductoInputDto
            {
                Codigo = objeto.Codigo,
                Descripcion = objeto.Descripcion,
                Color = objeto.Color,
                SKU = objeto.SKU,
                Talla = objeto.Talla,
                PrecioCosto = objeto.PrecioCosto,
                PrecioVenta = objeto.PrecioVenta,
                MonedaVentaId = objeto.MonedaVentaId,
                MonedaCostoId = objeto.MonedaCostoId,
                Stock = objeto.Stock,
                FotosExistentes = objeto.FotosExistentes,
                CategoriaIds = objeto.CategoriaIds,
                EsActivo = objeto.EsActivo,
            };

            var idActualizado = await _ProductoService.ActualizarAsync(id, dtoData, fotos);
            return Ok(new { Id = idActualizado });
        }

        /// <summary>
        ///  Obtener Productos Agrupados
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerProductosAgrupados()
        {
            var productos = await _ProductoService.ObtenerProductosAgrupados();
            return Ok(productos);
        }
        /// <summary>
        ///  Obtener Producto Agrupado por SKU 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerProductoAgrupadoPorSku(string sku)
        {
            var producto = await _ProductoService.ObtenerProductoAgrupadoPorSku(sku);
            return Ok(producto);
        }
    }
}
