using API.Application.Dtos.Gestion.Nomencladores.ComprobanteVenta;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class ComprobanteVentaController : BasicController<ComprobanteVenta, ComprobanteVentaValidator, DetallesComprobanteVentaDto, CrearComprobanteVentaInputDto, ActualizarComprobanteVentaInputDto, ListadoPaginadoComprobanteVentaDto, FiltrarConfigurarListadoPaginadoComprobanteVentaIntputDto>
    {
        private readonly IComprobanteVentaService _ComprobanteVentaService;

        public ComprobanteVentaController(IMapper mapper, IComprobanteVentaService servicioComprobanteVenta, IComprobanteVentaService ComprobanteVentaService) : base(mapper, servicioComprobanteVenta)
        {
            _ComprobanteVentaService = ComprobanteVentaService;
        }

        [HttpGet("venta/{ventaId}/comprobante")]
        public async Task<IActionResult> ObtenerComprobante(Guid ventaId)
        {
            var comprobante = await ObtenerComprobante(ventaId);
            return Ok(comprobante);
        }
    }
}
