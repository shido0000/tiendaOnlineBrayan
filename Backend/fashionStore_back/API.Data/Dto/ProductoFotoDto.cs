namespace API.Data.Dto
{
    public class ProductoFotoDto
    {
        public Guid ProductVariantId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsPrincipal { get; set; } = false;
        public int Orden { get; set; } = 0;
    }
}
