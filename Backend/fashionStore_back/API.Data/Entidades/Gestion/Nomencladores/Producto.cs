namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class Producto : EntidadBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsActivo { get; set; } = true;

        public ICollection<ProductoCategoria> ProductoCategorias { get; set; } = new List<ProductoCategoria>();

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
