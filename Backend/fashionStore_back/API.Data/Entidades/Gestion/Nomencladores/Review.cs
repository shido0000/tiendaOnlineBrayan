using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Review : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = default!;
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = default!;
        public int Rating { get; set; } // 1..5
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; } = false;

    }
}
