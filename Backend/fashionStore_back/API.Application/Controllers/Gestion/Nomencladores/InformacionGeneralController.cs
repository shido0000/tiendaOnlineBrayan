using API.Application.Dtos.Gestion.Nomencladores.InformacionGeneral;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionGeneralController : BasicController<InformacionGeneral, InformacionGeneralValidator, DetallesInformacionGeneralDto, CrearInformacionGeneralInputDto, ActualizarInformacionGeneralInputDto, ListadoPaginadoInformacionGeneralDto, FiltrarConfigurarListadoPaginadoInformacionGeneralInputDto>
    {
        private readonly IInformacionGeneralService _informacionGeneralService;

        public InformacionGeneralController(IMapper mapper, IInformacionGeneralService informacionGeneralService)
            : base(mapper, informacionGeneralService)
        {
            _informacionGeneralService = informacionGeneralService;
        }

        protected override Task<(IEnumerable<InformacionGeneral>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoInformacionGeneralInputDto inputDto)
        {
            List<Expression<Func<InformacionGeneral, bool>>> filtros = new();
            
            return _servicioBase.ObtenerListadoPaginado(
                inputDto.CantidadIgnorar,
                inputDto.CantidadMostrar,
                inputDto.SecuenciaOrdenamiento,
                null,
                filtros.ToArray());
        }

        protected override async Task<InformacionGeneral?> ObtenerElementoPorId(Guid id)
            => await _servicioBase.ObtenerPorId(id);
    }
}