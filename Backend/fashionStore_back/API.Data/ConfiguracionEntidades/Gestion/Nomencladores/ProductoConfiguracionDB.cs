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
        modelBuilder.Entity<Producto>().Property(e => e.Color).IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.SKU).IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.PrecioCosto).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.PrecioVenta).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<Producto>().Property(e => e.EsActivo).HasDefaultValue(true);

        modelBuilder.Entity<Producto>().HasIndex(e => e.Codigo).IsUnique();


        modelBuilder.Entity<Producto>()
             .HasOne(ci => ci.Moneda)
             .WithMany(ci => ci.Productos)
             .HasForeignKey(ci => ci.MonedaId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Producto>()
                   .HasOne(ci => ci.Inventario)
                   .WithOne(ci => ci.Producto)
                   .HasForeignKey<Inventario>(i => i.ProductoId)
                   .OnDelete(DeleteBehavior.Cascade);
    }
}
