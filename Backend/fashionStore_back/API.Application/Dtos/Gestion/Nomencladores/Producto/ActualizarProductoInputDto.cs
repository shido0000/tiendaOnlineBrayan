namespace API.Application.Dtos.Gestion.Nomencladores.Producto
{
    public class ActualizarProductoInputDto : ProductoDto
    {
        public List<string> FotosExistentes { get; set; } = new();

    }
}
