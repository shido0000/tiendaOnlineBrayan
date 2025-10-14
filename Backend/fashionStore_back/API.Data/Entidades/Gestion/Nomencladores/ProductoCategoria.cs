using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ProductoCategoria : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public Guid CategoriaId { get; set; }
        public CategoriaProducto Categoria { get; set; } = null!;
    }
}
