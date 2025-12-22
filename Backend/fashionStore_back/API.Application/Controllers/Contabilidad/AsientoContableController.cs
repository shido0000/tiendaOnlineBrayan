using API.Application.Dtos.Contabilidad.AsientoContable;
using API.Data.Entidades.Contabilidad;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Validators.Contabilidad;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Contabilidad
{

    public class AsientoContableController : BasicController<AsientoContable, AsientoContableValidator, DetallesAsientoContableDto, CrearAsientoContableInputDto, ActualizarAsientoContableInputDto, ListadoPaginadoAsientoContableDto, FiltrarConfigurarListadoPaginadoAsientoContableIntputDto>
    {
        private readonly IAsientoContableService _AsientoContableService;
        private readonly IReporteContabilidadService _ReporteService;

        public AsientoContableController(IMapper mapper, IAsientoContableService servicioAsientoContable, IAsientoContableService AsientoContableService, IReporteContabilidadService reporteService) : base(mapper, servicioAsientoContable)
        {
            _AsientoContableService = AsientoContableService;
            _ReporteService = reporteService;
        }

        protected override Task<(IEnumerable<AsientoContable>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoAsientoContableIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<AsientoContable, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Categoria => Categoria.Descripcion.Contains(inputDto.TextoBuscar));


            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas: query => query.Include(e => e.Movimientos).ThenInclude(e => e.Cuenta), filtros.ToArray());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ExportarLibroContable([FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
        {
            var bytes = await _ReporteService.ExportarLibroContableAsync(desde, hasta);
            var nombre = $"LibroContable_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx";
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
        }
    }
}
