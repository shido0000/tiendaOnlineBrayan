using API.Application.Dtos.Gestion.Nomencladores.Carrito;
using API.Data.Dto.Carrito;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class CarritoController : BasicController<Carrito, CarritoValidator, DetallesCarritoDto, CrearCarritoInputDto, ActualizarCarritoInputDto, ListadoPaginadoCarritoDto, FiltrarConfigurarListadoPaginadoCarritoIntputDto>
    {
        private readonly ICarritoService _CarritoService;

        public CarritoController(IMapper mapper, ICarritoService servicioCarrito, ICarritoService CarritoService) : base(mapper, servicioCarrito)
        {
            _CarritoService = CarritoService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EnviarDatosAlCarrito(DatosCarritoDto datosCarritoDto)
        {
            var result = await _CarritoService.EnviarDatosAlCarrito(datosCarritoDto);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerDatosCarritoReal()
        {
            var result = await _CarritoService.ObtenerDatosCarritoReal();
            return Ok(result);
        }
    }
}
