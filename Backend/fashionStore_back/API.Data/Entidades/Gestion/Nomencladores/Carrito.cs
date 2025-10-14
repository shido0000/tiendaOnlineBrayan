using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Carrito : EntidadBase
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public ICollection<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();

    }
}
