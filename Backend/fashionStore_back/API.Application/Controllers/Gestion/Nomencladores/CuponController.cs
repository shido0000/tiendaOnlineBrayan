using API.Application.Dtos.Gestion.Nomencladores.Cupon;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Services.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class CuponController : BasicController<Cupon, CuponValidator, DetallesCuponDto, CrearCuponInputDto, ActualizarCuponInputDto, ListadoPaginadoCuponDto, FiltrarConfigurarListadoPaginadoCuponIntputDto>
    {
        private readonly ICuponService _CuponService;

        public CuponController(IMapper mapper, ICuponService servicioCupon, ICuponService CuponService) : base(mapper, servicioCupon)
        {
            _CuponService = CuponService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ObtenerCuponPorCodigo(VerificarCuponDto verificarCuponDto)
        {
            var result = await _CuponService.ObtenerCuponPorCodigo(verificarCuponDto.Codigo, verificarCuponDto.ImportePedido);
            return Ok(result);
        }

        [HttpPost("IncrementarUsos/{cuponId}")]
        public async Task<IActionResult> IncrementarUsos(Guid cuponId)
        {
            try
            {
                await _CuponService.IncrementarUsos(cuponId);
                return Ok(new { mensaje = "Usos incrementados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
