using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.ListaDeseos
{
    public class ListaDeseosDto : EntidadBaseDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
