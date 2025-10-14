using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.ListaDeseosDetalle
{
    public class ListaDeseosDetalleDto : EntidadBaseDto
    {
        public Guid ListaDeseosId { get; set; }
        public Guid ProductoId { get; set; }
    }
}
