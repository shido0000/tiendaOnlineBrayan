using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.CarritoDetalle
{
    public class CarritoDetalleDto : EntidadBaseDto
    {
        public Guid CarritoId { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
