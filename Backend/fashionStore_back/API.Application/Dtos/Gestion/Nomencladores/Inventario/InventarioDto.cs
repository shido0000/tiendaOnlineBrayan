using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Inventario
{
    public class InventarioDto : EntidadBaseDto
    {
        public Guid ProductoId { get; set; }
        public int CantidadDisponible { get; set; }
        public int CantidadReservada { get; set; }
        public string? Ubicacion { get; set; }
        public string? EstadoProductoInventario { get; set; }
    }
}
