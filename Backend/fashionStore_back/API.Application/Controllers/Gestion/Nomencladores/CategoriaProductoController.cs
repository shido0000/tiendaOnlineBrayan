using API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto;
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

    public class CategoriaProductoController : BasicController<CategoriaProducto, CategoriaProductoValidator, DetallesCategoriaProductoDto, CrearCategoriaProductoInputDto, ActualizarCategoriaProductoInputDto, ListadoPaginadoCategoriaProductoDto, FiltrarConfigurarListadoPaginadoCategoriaProductoIntputDto>
    {
        private readonly ICategoriaProductoService _CategoriaProductoService;

        public CategoriaProductoController(IMapper mapper, ICategoriaProductoService servicioCategoriaProducto, ICategoriaProductoService CategoriaProductoService) : base(mapper, servicioCategoriaProducto)
        {
            _CategoriaProductoService = CategoriaProductoService;
        }

        protected override Task<(IEnumerable<CategoriaProducto>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoCategoriaProductoIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<CategoriaProducto, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Categoria => Categoria.Descripcion.Contains(inputDto.TextoBuscar));

           
            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas: query => query.Include(e => e.ProductoCategorias).ThenInclude(e => e.Producto), filtros.ToArray());
        }

        protected override async Task<CategoriaProducto?> ObtenerElementoPorId(Guid id)
       => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.ProductoCategorias).ThenInclude(e => e.Producto));


        [HttpPost("CrearConFotos")]
        public async Task<IActionResult> CrearCategoriaAsync(
    [FromForm] CrearCategoriaProductoInputDto objeto,
    [FromForm] IFormFile? foto)
        {
            CategoriaProducto dtoData = _mapper.Map<CategoriaProducto>(objeto);
            var result = await _CategoriaProductoService.CrearCategoriaAsync(dtoData, foto);
            return Ok(result);
        }

        [HttpPut("ActualizarConFotos/{id}")]
        public async Task<IActionResult> Actualizar(
    Guid id,
    [FromForm] CategoriaProductoDto dto,
    [FromForm] IFormFile? foto)
        {
            CategoriaProducto dtoData = _mapper.Map<CategoriaProducto>(dto);
            var idActualizado = await _CategoriaProductoService.ActualizarCategoriaAsync(id, dtoData, foto);
            return Ok(new { Id = idActualizado });
        }

    }
}
