namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class DetallesProductoDto : ProductoDto
    {
        public string MonedaCostoCodigo { get; set; } = string.Empty;
        public string MonedaVentaCodigo { get; set; } = string.Empty;
        public string Categorias { get; set; } = string.Empty;
    }
}
