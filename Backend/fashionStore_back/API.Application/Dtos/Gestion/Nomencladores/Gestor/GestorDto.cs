using API.Application.Dtos.Comunes;
using API.Data.Entidades.Gestion.Nomencladores;

namespace API.Application.Dtos.Gestion.Nomencladores.Gestor
{
    public class GestorDto : EntidadBaseDto
    {
        public required string Nombre { get; set; } = string.Empty;
        public required string Descripcion { get; set; } = string.Empty;
        public required bool EsActiva { get; set; } = true;
    }
}
