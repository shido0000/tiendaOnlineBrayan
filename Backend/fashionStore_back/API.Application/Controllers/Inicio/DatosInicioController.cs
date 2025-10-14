using API.Data.Entidades.Inicio;
using API.Domain.Interfaces.Inicio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Inicio
{

    public class DatosInicioController : ControllerBase   // 👈 importante
    {
        private readonly IDatosInicioService _DatosInicioService;

        public DatosInicioController(IMapper mapper, IDatosInicioService servicioDatosInicio, IDatosInicioService DatosInicioService)
        {
            _DatosInicioService = DatosInicioService;
        }

        [HttpGet("ObtenerDatosInicio")]
        public async Task<IActionResult> CrearConFotos()
        {

            var result = await _DatosInicioService.ObtenerDatosInicio();
            return Ok(result);
        }
    }
}
