using API.Application.Dtos.Comunes;
using API.Application.Dtos.Gestion.Nomencladores.Inventario;
using API.Data.Dto.VisualProductosPorCategoria;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Services.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class InventarioController : BasicController<Inventario, InventarioValidator, DetallesInventarioDto, CrearInventarioInputDto, ActualizarInventarioInputDto, ListadoPaginadoInventarioDto, FiltrarConfigurarListadoPaginadoInventarioIntputDto>
    {
        // private readonly IInventarioService _InventarioService;
        private readonly IInventarioService _InventarioService;

        public InventarioController(IMapper mapper, IInventarioService servicioInventario, IInventarioService InventarioService) : base(mapper, servicioInventario)
        {
            _InventarioService = InventarioService;
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


        protected override Task<(IEnumerable<Inventario>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoInventarioIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Inventario, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
            {
                filtros.Add(Inventario => Inventario.Producto.Codigo.ToLower().Contains(inputDto.TextoBuscar.ToLower())
                );
            }
            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas: query => query.Include(e => e.Producto), filtros.ToArray());
        }

        protected override async Task<Inventario?> ObtenerElementoPorId(Guid id)
           => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.Producto));


        /* protected override async Task<Inventario?> ObtenerElementoPorId(Guid id)
             => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.InventarioosInventarios).ThenInclude(e => e.Inventarioo).Include(e => e.Gestor));
        */
        protected override async Task<IEnumerable<DetallesInventarioDto>> ObtenerTodosElementos(string? secuenciaOrdenamiento = null)
      => _mapper.Map<IEnumerable<DetallesInventarioDto>>(await _servicioBase.ObtenerTodos(secuenciaOrdenamiento));


        public async override Task<IActionResult> Crear([FromBody] CrearInventarioInputDto crearDto)
        {
            _servicioBase.ValidarPermisos("gestionar");
            try
            {
                //se usa transaccion para asegurarse que el elemento se guarde
                //y que la traza guarde el id del elemento creado
                await _servicioBase.IniciarTransaccion();
                foreach (var productoId in crearDto.ProductoIds)
                {

                    Inventario entity = new Inventario()
                    {
                        Id = Guid.NewGuid(),
                        ProductoId = productoId,
                        CantidadDisponible = crearDto.CantidadDisponible,
                        CantidadReservada = crearDto.CantidadReservada,
                        Ubicacion = crearDto.Ubicacion,
                        EstadoProductoInventario= EstadoProductoInventario.Disponible
                    };
                    await CrearElemento(entity);
                }

                await _servicioBase.FinalizarTransaccion();
                return Ok(new ResponseDto { Status = StatusCodes.Status200OK });
            }
            catch (Exception)
            {
                await _servicioBase.RevertirTransaccion();
                throw;
            }
        }


        [HttpGet("ObtenerProductosDelInventarioPorCategoria/{categoriaId}")]
        public async Task<IActionResult> ObtenerProductosDelInventarioPorCategoria(Guid categoriaId)
        {
            var result = await _InventarioService.ObtenerProductosDelInventarioPorCategoria(categoriaId);
            return Ok(result);
        }
    }
}
