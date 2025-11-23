using Microsoft.AspNetCore.Http;

namespace API.Data.Dto
{
    public class ProductoEspecificoDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; } = true;
        public string? SKU { get; set; }

        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public Guid MonedaCostoId { get; set; }
        public Guid MonedaVentaId { get; set; }
        public int StockTotal { get; set; }
        public List<Guid> CategoriasIds { get; set; } = new();
        public List<ProductoEspecificoVarianteDto> ProductoVariantes { get; set; } = new();
    }
}
