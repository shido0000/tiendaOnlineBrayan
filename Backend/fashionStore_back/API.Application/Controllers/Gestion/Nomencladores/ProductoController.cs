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
          => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.Variants).ThenInclude(e => e.Fotos).Include(e => e.ProductoCategorias).ThenInclude(e => e.Categoria));


        [HttpPost("CrearConFotos")]
        public async Task<IActionResult> CrearConFotos(
    [FromForm] CrearProductoInputDto objeto)
        {
            var dtoData = new API.Data.Dto.CrearProductoInputDto
            {
                Codigo = objeto.Codigo,
                Descripcion = objeto.Descripcion,
                EsActivo = objeto.EsActivo,
                CategoriaIds = objeto.CategoriaIds,
                Variants = new()
            };
            foreach (var variant in objeto.Variants)
            {
                var elemento = new Data.Dto.ProductVariantDto
                {
                    SKU = variant.SKU,
                    Talla = variant.Talla,
                    Color = variant.Color,
                    PrecioCosto = variant.PrecioCosto,
                    PrecioVenta = variant.PrecioVenta,
                    MonedaCostoId = variant.MonedaCostoId,
                    MonedaVentaId = variant.MonedaVentaId,
                    Stock = variant.Stock,
                    Fotos = variant.Fotos // 👈 cada variante trae sus propias fotos
                };
                dtoData.Variants.Add(elemento);
            }


            var result = await _ProductoService.CrearAsync(dtoData);
            return Ok(result);
        }

        [HttpPut("ActualizarConFotos/{id}")]
        public async Task<IActionResult> ActualizarConFotos(
     Guid id,
     [FromForm] ActualizarProductoInputDto objeto)
        {
            var dtoData = new API.Data.Dto.ActualizarProductoInputDto
            {
                Codigo = objeto.Codigo,
                Descripcion = objeto.Descripcion,
                EsActivo = objeto.EsActivo,
                CategoriaIds = objeto.CategoriaIds,
                Variants = new()
            };

            // Mapear variantes
            foreach (var variant in objeto.Variants)
            {
                var elemento = new API.Data.Dto.ProductVariantDto
                {
                    Id = variant.Id,
                    SKU = variant.SKU,
                    Talla = variant.Talla,
                    Color = variant.Color,
                    PrecioCosto = variant.PrecioCosto,
                    PrecioVenta = variant.PrecioVenta,
                    MonedaCostoId = variant.MonedaCostoId,
                    MonedaVentaId = variant.MonedaVentaId,
                    Stock = variant.Stock,

                    // 👇 separar fotos existentes y nuevas
                    Fotos = variant.Fotos,
                    //  FotosNuevas = variant.FotosNuevas
                };

                dtoData.Variants.Add(elemento);
            }

            var idActualizado = await _ProductoService.ActualizarAsync(id, dtoData);
            return Ok(new { Id = idActualizado });
        }

        /// <summary>
        /// Obtener Producto especifico por Id
        /// </summary>
        /// <param name="id">Id del elemento</param>
        /// <response code="200">Completado con exito!</response>
        /// <response code="400">Ha ocurrido un error</response>
        /// <response code="404">Elemento no encontrado</response>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerProductoEspecifico(Guid id)
        {
            var result = await _ProductoService.ObtenerProductoEspecifico(id);
            return Ok(result);
        }

        /// <summary>
        /// Obtener Productos por listado de categorias
        /// </summary>
        /// <param name="categoriaIds">Listado de Ids de categorías</param>
        /// <response code="200">Completado con éxito!</response>
        /// <response code="400">Ha ocurrido un error</response>
        /// <response code="404">Elemento no encontrado</response>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerProductoPorCategoriaEspecifico([FromQuery] List<Guid> categoriaIds)
        {
            var result = await _ProductoService.ObtenerProductoPorCategoriaEspecifico(categoriaIds);
            return Ok(result);
        }


        //[HttpPut("ActualizarConFotos/{id}")]
        //public async Task<IActionResult> ActualizarConFotos(
        //Guid id,
        //[FromForm] ActualizarProductoInputDto objeto,
        //[FromForm] List<IFormFile> fotos)
        //{
        //    var dtoData = new API.Data.Dto.ActualizarProductoInputDto
        //    {
        //        Codigo = objeto.Codigo,
        //        Descripcion = objeto.Descripcion,
        //        Color = objeto.Color,
        //        SKU = objeto.SKU,
        //        PrecioCosto = objeto.PrecioCosto,
        //        PrecioVenta = objeto.PrecioVenta,
        //        MonedaId = objeto.MonedaId,
        //        FotosExistentes = objeto.FotosExistentes,
        //        CategoriaIds = objeto.CategoriaIds,
        //        EsActivo = objeto.EsActivo,
        //    };

        //    var idActualizado = await _ProductoService.ActualizarAsync(id, dtoData, fotos);
        //    return Ok(new { Id = idActualizado });
        //}


    }
}
