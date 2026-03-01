using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Carrito
{
    public class DatosCarritoDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public ICollection<DatosCarritoDetalleDto> Detalles { get; set; } = new List<DatosCarritoDetalleDto>();

    }
}
