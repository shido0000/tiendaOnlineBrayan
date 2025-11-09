using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductVariantConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductVariant>().ToTable("ProductVariants");
        EntidadBaseConfiguracionBD<ProductVariant>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ProductVariant>().Property(e => e.ProductoId).IsRequired();
        modelBuilder.Entity<ProductVariant>().Property(e => e.MonedaCostoId).IsRequired();
        modelBuilder.Entity<ProductVariant>().Property(e => e.MonedaVentaId).IsRequired();
        modelBuilder.Entity<ProductVariant>().Property(e => e.Stock).IsRequired().HasDefaultValue(1);
        modelBuilder.Entity<ProductVariant>().Property(e => e.SKU).IsRequired();
        modelBuilder.Entity<ProductVariant>().Property(e => e.PrecioCosto).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<ProductVariant>().Property(e => e.PrecioVenta).HasColumnType("decimal(18,2)").IsRequired();

        modelBuilder.Entity<ProductVariant>().HasIndex(e => e.SKU).IsUnique();

        modelBuilder.Entity<ProductVariant>()
             .HasOne(ci => ci.MonedaCosto)
             .WithMany(ci => ci.ProductosCosto)
             .HasForeignKey(ci => ci.MonedaCostoId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductVariant>()
           .HasOne(ci => ci.MonedaVenta)
           .WithMany(ci => ci.ProductosVentas)
           .HasForeignKey(ci => ci.MonedaVentaId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
