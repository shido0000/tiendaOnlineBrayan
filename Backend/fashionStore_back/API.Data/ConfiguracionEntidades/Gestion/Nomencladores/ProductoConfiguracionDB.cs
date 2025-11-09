using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>().ToTable("Productos");
        EntidadBaseConfiguracionBD<Producto>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Producto>().Property(e => e.Codigo).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.Descripcion).HasMaxLength(200).IsRequired();
        
        modelBuilder.Entity<Producto>().Property(e => e.EsActivo).HasDefaultValue(true);

        modelBuilder.Entity<Producto>().HasIndex(e => e.Codigo).IsUnique();
         
 
    }
}
