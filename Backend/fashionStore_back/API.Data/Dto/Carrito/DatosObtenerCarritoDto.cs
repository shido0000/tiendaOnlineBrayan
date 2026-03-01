using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Carrito
{
    public class DatosObtenerCarritoDto
    {
        public Guid UsuarioId { get; set; }
        public string NombreCliente { get; set; } = "";
        public string ApellidoCliente { get; set; } = "";
        public string EmailCliente { get; set; } = "";
        public string TelefonoCliente { get; set; } = "";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public ICollection<DatosObtenerCarritoDetalleDto> Detalles { get; set; } = new List<DatosObtenerCarritoDetalleDto>();

    }
}
