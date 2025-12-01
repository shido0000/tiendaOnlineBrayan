using API.Application.Dtos.Gestion.Nomencladores.Pedido;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class PedidoController : BasicController<Pedido, PedidoValidator, DetallesPedidoDto, CrearPedidoInputDto, ActualizarPedidoInputDto, ListadoPaginadoPedidoDto, FiltrarConfigurarListadoPaginadoPedidoIntputDto>
    {
        private readonly IPedidoService _PedidoService;

        public PedidoController(IMapper mapper, IPedidoService servicioPedido, IPedidoService PedidoService) : base(mapper, servicioPedido)
        {
            _PedidoService = PedidoService;
        }

        protected override Task<(IEnumerable<Pedido>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoPedidoIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Pedido, bool>>> filtros = new();
            //if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
            //    filtros.Add(Pedido => Pedido.Nombre.Contains(inputDto.TextoBuscar)

            //    );

            if (inputDto.Estado.HasValue)
            {
                if (inputDto.Estado == 2)
                {
                    filtros.Add(Pedido => Pedido.Estado == EstadoPedido.Confirmado);
                }
                else if (inputDto.Estado == 3)
                {
                    filtros.Add(Pedido => Pedido.Estado == EstadoPedido.Rechazado);
                }
                else if (inputDto.Estado == 4)
                {
                    filtros.Add(Pedido => Pedido.Estado == EstadoPedido.Pendiente);
                }
            }

            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, propiedadesIncluidas: query => query.Include(e => e.Usuario).Include(e => e.Moneda).Include(e => e.Cupon)!, filtros.ToArray());
        }

        protected override async Task<Pedido?> ObtenerElementoPorId(Guid id)
           => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query =>
                query.Include(e => e.Usuario)
                     .Include(e => e.Moneda)
                     .Include(e => e.Cupon)
           !);


        /* protected override async Task<Pedido?> ObtenerElementoPorId(Guid id)
             => await _servicioBase.ObtenerPorId(id, propiedadesIncluidas: query => query.Include(e => e.PedidoosPedidos).ThenInclude(e => e.Pedidoo).Include(e => e.Gestor));
        */
        protected override async Task<IEnumerable<DetallesPedidoDto>> ObtenerTodosElementos(string? secuenciaOrdenamiento = null)
      => _mapper.Map<IEnumerable<DetallesPedidoDto>>(await _servicioBase.ObtenerTodos(secuenciaOrdenamiento));

        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarPedido([FromBody] GenerarPedidoDto generarPedidoDto)
        {
            var result = await _PedidoService.GenerarPedido(generarPedidoDto);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObtenerPedidoPorId(Guid id)
        {
            var result = await _PedidoService.ObtenerPedidoPorId(id);
            return Ok(result);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarPedidoConLineas([FromBody] PedidoConfirmarDto dto)
        {
            await _PedidoService.ActualizarPedidoConLineas(dto);
            return Ok();
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> CancelarPedido(Guid id)
        {
            await _PedidoService.CancelarPedido(id);
            return Ok();
        }
    }
}
