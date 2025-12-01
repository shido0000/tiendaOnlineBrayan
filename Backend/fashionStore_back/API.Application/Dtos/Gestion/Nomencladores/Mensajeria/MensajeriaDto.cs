using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.Mensajeria
{
    public class MensajeriaDto : EntidadBaseDto
    {
        public required string Descripcion { get; set; } = string.Empty;
        public required int Precio { get; set; }
        public required Guid MonedaId { get; set; }
    }
}
