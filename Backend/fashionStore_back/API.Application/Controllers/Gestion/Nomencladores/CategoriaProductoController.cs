using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.CategoriaProducto;
using API.Application.Dtos.Gestion.Nomencladores.Producto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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


        ///// <summary>
        ///// Imprimir Aeropuerto
        ///// </summary>
        //[HttpGet("[action]")]
        //public async Task<IActionResult> ImprimirPorFiltro(string? texto, bool esActivo, int operadorId)
        //{
        //    var name = "Categoria";
        //            try
        //    {
        //        IEnumerable<AeropuertoDto> result = _mapper.Map<IEnumerable<AeropuertoDto>>(await _servicioBase.ObtenerTodos());
        //        var resultList = result.ToList(); // Convertir a lista una sola vez

        //        if (resultList.Count == 0)
        //        {
        //            return BadRequest(new ResponseDto { Status = StatusCodes.Status400BadRequest, ErrorMessage = $"No existen elementos de tipo {name} definidos" });
        //        }


        //        result = result.OrderByDescending(e => e.Activo)
        //            .ThenBy(e => e.Codigo)
        //            .ToList();

        //        RegisteredObjects.AddConnection(typeof(JsonDataSourceConnection));
        //        WebReport webReport = new();
        //        webReport.Report.Load(@$"{_webHostingEnvironment.ContentRootPath}\Reportes\Nomencladores\Reporte{name}.frx");
        //        string json = JsonConvert.SerializeObject(resultList2, Formatting.None,
        //                 new JsonSerializerSettings()
        //                 {
        //                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                 });
        //        webReport.Report.Dictionary.Connections[0].ConnectionString = $@"Json={json}";
        //        webReport.Report.Prepare();
        //        Stream stream = new MemoryStream();
        //        webReport.Report.Export(new PDFExport(), stream);
        //        stream.Position = 0;
        //        return File(stream, "application/pdf", $"Reporte{name}.pdf");
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ResponseDto { Status = StatusCodes.Status400BadRequest, ErrorMessage = ex.InnerException?.Message ?? ex.Message });
        //    }
        //}
    }
}
