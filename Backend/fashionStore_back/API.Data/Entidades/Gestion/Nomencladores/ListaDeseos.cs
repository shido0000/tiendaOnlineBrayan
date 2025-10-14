using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ListaDeseos : EntidadBase
    {
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<ListaDeseosDetalle> Detalles { get; set; } = new List<ListaDeseosDetalle>();

    }
}
