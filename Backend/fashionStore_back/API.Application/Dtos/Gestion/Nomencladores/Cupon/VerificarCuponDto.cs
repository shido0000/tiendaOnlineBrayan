using API.Application.Dtos.Comunes;

namespace API.Application.Dtos.Gestion.Nomencladores.Cupon
{
    public class VerificarCuponDto 
    {
        public required string Codigo { get; set; } = string.Empty; // Ej: "BLACKFRIDAY"
        public required decimal ImportePedido { get; set; }  
    }
}
