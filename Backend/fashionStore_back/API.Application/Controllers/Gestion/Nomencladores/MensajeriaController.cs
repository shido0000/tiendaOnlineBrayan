using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.Mensajeria;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class MensajeriaController : BasicController<Mensajeria, MensajeriaValidator, DetallesMensajeriaDto, CrearMensajeriaInputDto, ActualizarMensajeriaInputDto, ListadoPaginadoMensajeriaDto, FiltrarConfigurarListadoPaginadoMensajeriaIntputDto>
    {
        // private readonly IMensajeriaService _MensajeriaService;
        private readonly IMensajeriaService _MensajeriaService;

        public MensajeriaController(IMapper mapper, IMensajeriaService servicioMensajeria, IMensajeriaService MensajeriaService) : base(mapper, servicioMensajeria)
        {
            _MensajeriaService = MensajeriaService;
        }
        /// <summary>
        /// Retorna un listado para un Select
        /// </summary>
        /// <param name="inputDto">Datos de entrada</param>
        /// <response code="200">Completado con exito! </response>
        /// <response code="400">Ha ocurrido un error </response>
        [HttpGet("[action]")]
        public new async Task<IActionResult> ObtenerSelectList([FromQuery] ObtenerSelectListInputDto inputDto)
            => await base.ObtenerSelectList(inputDto);


        protected override Task<(IEnumerable<Mensajeria>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoMensajeriaIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Mensajeria, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Mensajeria => Mensajeria.Descripcion.Contains(inputDto.TextoBuscar)

                );

            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas:query=>query.Include(e=>e.Moneda)!, filtros.ToArray());
        }

        protected override async Task<Mensajeria?> ObtenerElementoPorId(Guid id)
           => await _servicioBase.ObtenerPorId(id);


        /* protected override async Task<Mensajeria?> ObtenerElementoPorId(Guid id)
             => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.MensajeriaosMensajerias).ThenInclude(e => e.Mensajeriao).Include(e => e.Mensajeria));
        */
        protected override async Task<IEnumerable<DetallesMensajeriaDto>> ObtenerTodosElementos(string? secuenciaOrdenamiento = null)
      => _mapper.Map<IEnumerable<DetallesMensajeriaDto>>(await _servicioBase.ObtenerTodos(secuenciaOrdenamiento));



    }
}
