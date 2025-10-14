﻿using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Entidades.Gestion.Nomencladores
{
    public class ProductoDescuento : EntidadBase
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
        public Guid DescuentoId { get; set; }
        public Descuento Descuento { get; set; } = null!;
    }
}
