namespace API.Data.Dto.Dashboard
{
    public class PedidosUltimos7DiasDto
    {
        public List<string> Dias { get; set; } = new();
        public List<int> Confirmados { get; set; } = new();
        public List<int> Cancelados { get; set; } = new();
    }
}
