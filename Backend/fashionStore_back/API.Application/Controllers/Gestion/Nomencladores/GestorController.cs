using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.Gestor;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class GestorController : BasicController<Gestor, GestorValidator, DetallesGestorDto, CrearGestorInputDto, ActualizarGestorInputDto, ListadoPaginadoGestorDto, FiltrarConfigurarListadoPaginadoGestorIntputDto>
    {
        // private readonly IGestorService _GestorService;
        private readonly IGestorService _GestorService;

        public GestorController(IMapper mapper, IGestorService servicioGestor, IGestorService GestorService) : base(mapper, servicioGestor)
        {
            _GestorService = GestorService;
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


        protected override Task<(IEnumerable<Gestor>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoGestorIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Gestor, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Gestor => Gestor.Nombre.Contains(inputDto.TextoBuscar)

                );

            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, null, filtros.ToArray());
        }

        protected override async Task<Gestor?> ObtenerElementoPorId(Guid id)
           => await _servicioBase.ObtenerPorId(id);


        /* protected override async Task<Gestor?> ObtenerElementoPorId(Guid id)
             => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.GestorosGestors).ThenInclude(e => e.Gestoro).Include(e => e.Gestor));
        */
        protected override async Task<IEnumerable<DetallesGestorDto>> ObtenerTodosElementos(string? secuenciaOrdenamiento = null)
      => _mapper.Map<IEnumerable<DetallesGestorDto>>(await _servicioBase.ObtenerTodos(secuenciaOrdenamiento));


        
    }
}
