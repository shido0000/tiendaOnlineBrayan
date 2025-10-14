using API.Application.Dtos.Gestion.Nomencladores.ListaDeseos;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using AutoMapper;

namespace API.Application.Controllers.Gestion.Nomencladores
{

    public class ListaDeseosController : BasicController<ListaDeseos, ListaDeseosValidator, DetallesListaDeseosDto, CrearListaDeseosInputDto, ActualizarListaDeseosInputDto, ListadoPaginadoListaDeseosDto, FiltrarConfigurarListadoPaginadoListaDeseosIntputDto>
    {
        private readonly IListaDeseosService _ListaDeseosService;

        public ListaDeseosController(IMapper mapper, IListaDeseosService servicioListaDeseos, IListaDeseosService ListaDeseosService) : base(mapper, servicioListaDeseos)
        {
            _ListaDeseosService = ListaDeseosService;
        }
    }
}
