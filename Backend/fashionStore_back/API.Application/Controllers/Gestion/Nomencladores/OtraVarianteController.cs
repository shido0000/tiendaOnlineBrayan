using API.Application.Dtos.Gestion.Nomencladores.OtraVariante;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;
using System.Linq.Expressions;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class OtraVarianteController : BasicController<OtraVariante, OtraVarianteValidator, DetallesOtraVarianteDto, CrearOtraVarianteInputDto, ActualizarOtraVarianteInputDto, ListadoPaginadoOtraVarianteDto, FiltrarConfigurarListadoPaginadoOtraVarianteIntputDto>
    {
        private readonly IOtraVarianteService _OtraVarianteService;

        public OtraVarianteController(IMapper mapper, IOtraVarianteService servicioOtraVariante, IOtraVarianteService OtraVarianteService) : base(mapper, servicioOtraVariante)
        {
            _OtraVarianteService = OtraVarianteService;
        }

        protected override Task<(IEnumerable<OtraVariante>, int)> AplicarFiltrosIncluirPropiedades(FiltrarConfigurarListadoPaginadoOtraVarianteIntputDto inputDto)
        {
            //agregando filtros
            List<Expression<Func<OtraVariante, bool>>> filtros = new();
            if (!string.IsNullOrEmpty(inputDto.TextoBuscar))
                filtros.Add(Categoria => Categoria.Nombre.Contains(inputDto.TextoBuscar));


            return _servicioBase.ObtenerListadoPaginado(inputDto.CantidadIgnorar, inputDto.CantidadMostrar, inputDto.SecuenciaOrdenamiento, null, filtros.ToArray());
        }

        protected override async Task<OtraVariante?> ObtenerElementoPorId(Guid id)
       => await _servicioBase.ObtenerPorId(id);




    }
}
