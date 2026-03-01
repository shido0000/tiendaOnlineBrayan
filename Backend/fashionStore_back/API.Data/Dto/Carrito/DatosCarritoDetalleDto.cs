using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;

namespace API.Data.Dto.Carrito
{
    public class DatosCarritoDetalleDto
    {
        public Guid CarritoId { get; set; }
        public Guid? ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal UnitPrice { get; set; } // snapshot de precio
        public decimal LineTotal { get; set; }

    }
}
