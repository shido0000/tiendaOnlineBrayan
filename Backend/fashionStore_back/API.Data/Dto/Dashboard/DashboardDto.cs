namespace API.Data.Dto.Dashboard
{
    public class DashboardDto
    {
        public VentasUltimos7DiasDto VentasUltimos7Dias { get; set; } = new();
        public List<TopProductoDto> TopProductos { get; set; } = new();

        // Nuevos campos
        public decimal VentasHoy { get; set; }
        public int PedidosHoy { get; set; }

        public int ClientesActivos { get; set; }
        public int NuevosClientesHoy { get; set; }
        public int InventarioBajo { get; set; }

        public decimal IngresosSemana { get; set; }
        public decimal IngresosMes { get; set; }
        public int PedidosPendientes { get; set; }
        public int PedidosConfirmados { get; set; }
        public int PedidosCancelados { get; set; }
        public string TopProductoHoy { get; set; } = string.Empty;
        public int TopProductoHoyCantidad { get; set; }
        public int ClientesRecurrentes { get; set; }
        public int ClientesNuevosSemana { get; set; }
        public decimal MargenBrutoPromedio { get; set; }
        public PedidosUltimos7DiasDto PedidosUltimos7Dias { get; set; } = new();

    }
}
