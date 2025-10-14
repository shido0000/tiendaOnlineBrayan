using API.Data.Entidades.Seguridad;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ListaDeseosDetalle : EntidadBase
    {
        public Guid ListaDeseosId { get; set; }
        public ListaDeseos ListaDeseos { get; set; } = null!;
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

    }
}
