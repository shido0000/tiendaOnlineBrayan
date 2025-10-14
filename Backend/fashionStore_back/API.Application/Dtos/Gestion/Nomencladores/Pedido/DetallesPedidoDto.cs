namespace API.Application.Dtos.Gestion.Nomencladores.Pedido
{
    public class DetallesPedidoDto : PedidoDto
    {
        public string Usuario { get; set; } = "-";
        public string MonedaCodigo { get; set; } = "-";
        public string CuponCodigo { get; set; } = "-";

    }
}
