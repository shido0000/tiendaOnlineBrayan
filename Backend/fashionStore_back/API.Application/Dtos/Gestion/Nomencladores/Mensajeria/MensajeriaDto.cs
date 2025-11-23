using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.Mensajeria
{
    public class MensajeriaDto : EntidadBaseDto
    {
        public Guid CarritoId { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
