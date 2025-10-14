using API.Application.Dtos.Gestion.Nomencladores.Venta;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class VentaController : BasicController<Venta, VentaValidator, DetallesVentaDto, CrearVentaInputDto, ActualizarVentaInputDto, ListadoPaginadoVentaDto, FiltrarConfigurarListadoPaginadoVentaIntputDto>
    {
        private readonly IVentaService _VentaService;

        public VentaController(IMapper mapper, IVentaService servicioVenta, IVentaService VentaService) : base(mapper, servicioVenta)
        {
            _VentaService = VentaService;
        }
    }
}
