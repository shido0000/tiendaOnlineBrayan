namespace API.Application.Dtos.Gestion.Nomencladores.Descuento
{
    public class DetallesDescuentoDto : DescuentoDto
    {
        public List<Guid> ProductosIds { get; set; } = new();
    }
}
