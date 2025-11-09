using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductoDescuentoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductoDescuento>().ToTable("ProductosDescuentos");
        EntidadBaseConfiguracionBD<ProductoDescuento>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ProductoDescuento>().Property(e => e.ProductVariantId).IsRequired();
        modelBuilder.Entity<ProductoDescuento>().Property(e => e.DescuentoId).IsRequired();

        //modelBuilder.Entity<ProductoDescuento>().HasIndex(e => e.ProductoId e.CategoriaId).IsUnique();


        modelBuilder.Entity<ProductoDescuento>()
            .HasOne(ci => ci.ProductVariant)
            .WithMany(ci => ci.ProductoDescuentos)
            .HasForeignKey(ci => ci.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductoDescuento>()
                   .HasOne(ci => ci.Descuento)
                   .WithMany(ci => ci.ProductoDescuentos)
                    .HasForeignKey(x => x.DescuentoId)
                   .OnDelete(DeleteBehavior.Cascade);
    }
}
