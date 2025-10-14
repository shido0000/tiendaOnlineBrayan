using API.Application.Dtos.Gestion.Nomencladores.Carrito;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class CarritoController : BasicController<Carrito, CarritoValidator, DetallesCarritoDto, CrearCarritoInputDto, ActualizarCarritoInputDto, ListadoPaginadoCarritoDto, FiltrarConfigurarListadoPaginadoCarritoIntputDto>
    {
        private readonly ICarritoService _CarritoService;

        public CarritoController(IMapper mapper, ICarritoService servicioCarrito, ICarritoService CarritoService) : base(mapper, servicioCarrito)
        {
            _CarritoService = CarritoService;
        }
    }
}
