using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductoVarianteConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductoVariante>().ToTable("ProductoVariantes");
        EntidadBaseConfiguracionBD<ProductoVariante>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ProductoVariante>().Property(e => e.ProductoId).IsRequired();
        modelBuilder.Entity<ProductoVariante>().Property(e => e.Stock).IsRequired();
        modelBuilder.Entity<ProductoVariante>().Property(e => e.Principal).HasDefaultValue(false);


        modelBuilder.Entity<ProductoVariante>()
                  .HasOne(ci => ci.Producto)
                  .WithMany(ci => ci.ProductosVariantes)
                  .HasForeignKey(ci => ci.ProductoId)
                  .OnDelete(DeleteBehavior.Cascade);
    }
}
