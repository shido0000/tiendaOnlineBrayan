using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductoCategoriaConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductoCategoria>().ToTable("ProductosCategorias");
        EntidadBaseConfiguracionBD<ProductoCategoria>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ProductoCategoria>().Property(e => e.ProductoId).IsRequired();
        modelBuilder.Entity<ProductoCategoria>().Property(e => e.CategoriaId).IsRequired();

        //modelBuilder.Entity<ProductoCategoria>().HasIndex(e => e.ProductoId e.CategoriaId).IsUnique();


        modelBuilder.Entity<ProductoCategoria>()
            .HasOne(ci => ci.Producto)
            .WithMany(ci => ci.ProductoCategorias)
            .HasForeignKey(ci => ci.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductoCategoria>()
                   .HasOne(ci => ci.Categoria)
                   .WithMany(ci => ci.ProductoCategorias)
                    .HasForeignKey(x => x.CategoriaId)
                   .OnDelete(DeleteBehavior.Cascade);
    }
}
