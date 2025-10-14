using API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class CategoriaProductoController : BasicController<CategoriaProducto, CategoriaProductoValidator, DetallesCategoriaProductoDto, CrearCategoriaProductoInputDto, ActualizarCategoriaProductoInputDto, ListadoPaginadoCategoriaProductoDto, FiltrarConfigurarListadoPaginadoCategoriaProductoIntputDto>
    {
        private readonly ICategoriaProductoService _CategoriaProductoService;

        public CategoriaProductoController(IMapper mapper, ICategoriaProductoService servicioCategoriaProducto, ICategoriaProductoService CategoriaProductoService) : base(mapper, servicioCategoriaProducto)
        {
            _CategoriaProductoService = CategoriaProductoService;
        }

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
