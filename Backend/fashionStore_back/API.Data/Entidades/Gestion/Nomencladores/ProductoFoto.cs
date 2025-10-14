namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ProductoFoto : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public string Url { get; set; } = string.Empty;   // ruta o URL de la foto
        public string? Descripcion { get; set; }          // opcional
        public bool EsPrincipal { get; set; } = false;    // para marcar la foto principal
        public int Orden { get; set; } = 0;               // para ordenar varias fotos
    }

}
