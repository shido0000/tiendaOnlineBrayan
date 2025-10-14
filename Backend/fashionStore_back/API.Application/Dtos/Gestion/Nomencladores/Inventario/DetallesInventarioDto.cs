namespace API.Application.Dtos.Gestion.Nomencladores.Inventario
{
    public class DetallesInventarioDto : InventarioDto
    {
        public string ProductoCodigo { get; set; }
        public string ProductoDescripcion { get; set; }
        public string ProductoSku { get; set; }
    }
}
