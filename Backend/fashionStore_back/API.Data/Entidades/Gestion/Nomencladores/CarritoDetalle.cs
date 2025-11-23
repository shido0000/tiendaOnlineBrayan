namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class CarritoDetalle : EntidadBase
    {
        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; } = null!;
        public Guid? ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal UnitPrice { get; set; } // snapshot de precio
        public decimal LineTotal { get; set; }
    }
}
