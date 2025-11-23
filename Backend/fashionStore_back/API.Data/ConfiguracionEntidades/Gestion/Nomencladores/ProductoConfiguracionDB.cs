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
        modelBuilder.Entity<Producto>().Property(e => e.SKU).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.Descripcion).HasMaxLength(200).IsRequired();
        
        modelBuilder.Entity<Producto>().Property(e => e.EsActivo).HasDefaultValue(true);

        modelBuilder.Entity<Producto>().HasIndex(e => e.SKU).IsUnique();
        modelBuilder.Entity<Producto>().HasIndex(e => e.Codigo).IsUnique();

        modelBuilder.Entity<Producto>()
                  .HasOne(ci => ci.MonedaCosto)
                  .WithMany(ci => ci.ProductosCosto)
                  .HasForeignKey(ci => ci.MonedaCostoId)
                  .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Producto>()
                   .HasOne(ci => ci.MonedaVenta)
                   .WithMany(ci => ci.ProductosVentas)
                    .HasForeignKey(x => x.MonedaVentaId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
