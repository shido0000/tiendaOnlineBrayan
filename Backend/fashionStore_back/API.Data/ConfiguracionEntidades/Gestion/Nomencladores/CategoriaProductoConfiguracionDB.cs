using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class CategoriaProductoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>().ToTable("CategoriasProductos");
        EntidadBaseConfiguracionBD<CategoriaProducto>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<CategoriaProducto>().Property(e => e.Nombre).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<CategoriaProducto>().Property(e => e.Descripcion).IsRequired().HasMaxLength(100);
    }
}
