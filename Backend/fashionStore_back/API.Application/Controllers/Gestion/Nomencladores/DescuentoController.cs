using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.Descuento;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class DescuentoController : BasicController<Descuento, DescuentoValidator, DetallesDescuentoDto, CrearDescuentoInputDto, ActualizarDescuentoInputDto, ListadoPaginadoDescuentoDto, FiltrarConfigurarListadoPaginadoDescuentoIntputDto>
    {
        // private readonly IDescuentoService _DescuentoService;
        private readonly IDescuentoService _DescuentoService;

        public DescuentoController(IMapper mapper, IDescuentoService servicioDescuento, IDescuentoService DescuentoService) : base(mapper, servicioDescuento)
        {
            _DescuentoService = DescuentoService;
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


        protected override Task<(IEnumerable<Descuento>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoDescuentoIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Descuento, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Descuento => Descuento.Nombre.Contains(inputDto.TextoBuscar)

                );

            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, null, filtros.ToArray());
        }

        protected override async Task<Descuento?> ObtenerElementoPorId(Guid id)
           => await _servicioBase.ObtenerPorId(id,propiedadesIncluidas: query => query.Include(e => e.ProductoDescuentos).ThenInclude(e => e.Producto));


        /* protected override async Task<Descuento?> ObtenerElementoPorId(Guid id)
             => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.DescuentoosDescuentos).ThenInclude(e => e.Descuentoo).Include(e => e.Gestor));
        */
        protected override async Task<IEnumerable<DetallesDescuentoDto>> ObtenerTodosElementos(string? secuenciaOrdenamiento = null)
      => _mapper.Map<IEnumerable<DetallesDescuentoDto>>(await _servicioBase.ObtenerTodos(secuenciaOrdenamiento));


        //public async override Task<IActionResult> Crear([FromBody] CrearDescuentoInputDto crearDto)
        //{
        //    _servicioBase.ValidarPermisos("gestionar");

        //    Descuento entity = _mapper.Map<Descuento>(crearDto);

        //    try
        //    {
        //        //se usa transaccion para asegurarse que el elemento se guarde
        //        //y que la traza guarde el id del elemento creado
        //        await _servicioBase.IniciarTransaccion();
        //        DescuentoDto entityDto = await CrearElemento(entity);
        //        var lista = new List<ProductoDescuento>();
        //        foreach (var elementoId in crearDto.ProductosIds)
        //        {
        //            var nuevo = new ProductoDescuento()
        //            {
        //                DescuentoId = entityDto.Id,
        //                ProductoId = elementoId,
        //            };
        //            lista.Add(nuevo);
        //        }
        //        await CrearElemento(lista);
        //        await _servicioBase.FinalizarTransaccion();

        //        return Ok(new ResponseDto { Status = StatusCodes.Status200OK, Result = entityDto });
        //    }
        //    catch (Exception)
        //    {
        //        await _servicioBase.RevertirTransaccion();
        //        throw;
        //    }
        //}
    }
}
