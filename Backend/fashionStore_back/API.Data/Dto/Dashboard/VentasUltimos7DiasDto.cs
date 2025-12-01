namespace API.Data.Dto.Dashboard
{
    public class VentasUltimos7DiasDto
    {
        public List<string> Dias { get; set; } = new();
        public List<decimal> Totales { get; set; } = new();
    }
}
