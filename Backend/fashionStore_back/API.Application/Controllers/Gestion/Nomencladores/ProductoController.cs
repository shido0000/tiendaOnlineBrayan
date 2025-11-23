using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Services.Gestion.Nomencladores;
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


            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento,
                propiedadesIncluidas: query => query.Include(e => e.ProductoCategorias)
                                                    .Include(e => e.MonedaCosto)
                                                    .Include(e => e.MonedaVenta),
                filtros.ToArray());
        }

        protected override async Task<Producto?> ObtenerElementoPorId(Guid id)
          => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.ProductosVariantes).ThenInclude(e => e.Fotos).Include(e => e.ProductoCategorias).ThenInclude(e => e.Categoria));

 
        [HttpPost("CrearConFotos")]
        public async Task<IActionResult> CrearConFotos([FromForm] ProductoACrear payload)
        {
            if (payload == null)
                return BadRequest("Payload vacío o inválido");

            var result = await _ProductoService.CrearProducto(payload);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerProductoEspecifico(Guid id)
        {
            var result = await _ProductoService.ObtenerProductoEspecifico(id);
            return Ok(result);
        }

        

        [HttpPut("ActualizarConFotos/{id}")]
        public async Task<IActionResult> ActualizarConFotos(
      Guid id,
      [FromForm] ProductoAActualizar objeto)
        {
            var idActualizado = await _ProductoService.ActualizarProducto(id, objeto);
            return Ok(new { Id = idActualizado });
        }

    }
}
