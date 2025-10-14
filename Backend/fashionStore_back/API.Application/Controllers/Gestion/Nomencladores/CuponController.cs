using API.Application.Dtos.Gestion.Nomencladores.Cupon;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class CuponController : BasicController<Cupon, CuponValidator, DetallesCuponDto, CrearCuponInputDto, ActualizarCuponInputDto, ListadoPaginadoCuponDto, FiltrarConfigurarListadoPaginadoCuponIntputDto>
    {
        private readonly ICuponService _CuponService;

        public CuponController(IMapper mapper, ICuponService servicioCupon, ICuponService CuponService) : base(mapper, servicioCupon)
        {
            _CuponService = CuponService;
        }
    }
}
