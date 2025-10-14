using API.Application.Dtos.Comunes;
using API.Data.Entidades.Seguridad;

namespace API.Application.Dtos.Gestion.Nomencladores.Carrito
{
    public class CarritoDto : EntidadBaseDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
