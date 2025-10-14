namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class CarritoDetalle : EntidadBase
    {
        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; } = null!;
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }

    }
}
