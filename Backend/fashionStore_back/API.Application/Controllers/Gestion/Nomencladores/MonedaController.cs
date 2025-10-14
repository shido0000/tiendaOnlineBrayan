using API.Application.Dtos.Gestion.Nomencladores.Moneda;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class MonedaController : BasicController<Moneda, MonedaValidator, DetallesMonedaDto, CrearMonedaInputDto, ActualizarMonedaInputDto, ListadoPaginadoMonedaDto, FiltrarConfigurarListadoPaginadoMonedaIntputDto>
    {
        private readonly IMonedaService _MonedaService;


        public MonedaController(IMapper mapper, IMonedaService servicioMoneda, IMonedaService MonedaService) : base(mapper, servicioMoneda)
        {
            _MonedaService = MonedaService;
        }

        protected override Task<(IEnumerable<Moneda>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoMonedaIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<Moneda, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
            {
                filtros.Add(Moneda => Moneda.Codigo.Contains(inputDto.TextoBuscar));
            }
            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, null, filtros.ToArray());
        }

        protected override async Task<Moneda?> ObtenerElementoPorId(Guid id)
            => await _servicioBase.ObtenerPorId(id);


    }
}
