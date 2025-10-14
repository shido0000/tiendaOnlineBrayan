using API.Application.Dtos.Gestion.Nomencladores.Venta;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Mapper.Gestion.Nomencladores
{
    public class VentaDtoProfile : BaseProfile<Venta, VentaDto, CrearVentaInputDto, ActualizarVentaInputDto, ListadoPaginadoVentaDto>

    {
        public VentaDtoProfile()
        {
            MapVentaDto();
        }

        public void MapVentaDto()
        {
            CreateMap<Venta, DetallesVentaDto>()
                .ReverseMap();
        }
    }
}
