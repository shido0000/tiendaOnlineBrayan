using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class MonedaConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Moneda>().ToTable("Monedas");
        EntidadBaseConfiguracionBD<Moneda>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Moneda>().Property(e => e.Codigo).HasMaxLength(10).IsRequired();
        modelBuilder.Entity<Moneda>().Property(e => e.Descripcion).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Moneda>().Property(e => e.TasaCambio).HasColumnType("decimal(18,4)").IsRequired();
        modelBuilder.Entity<Moneda>().Property(e => e.EsActiva).HasDefaultValue(true);

        modelBuilder.Entity<Moneda>().HasIndex(e => e.Codigo).IsUnique();
        modelBuilder.Entity<Moneda>().HasIndex(e => e.Descripcion).IsUnique();


        //modelBuilder.Entity<Moneda>()
        //        .HasOne(ci => ci.Product)
        //        .WithMany(ci => ci.CartsItems)
        //        .HasForeignKey(ci => ci.ProductId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Moneda>()
        //           .HasOne(ci => ci.Usuario)
        //           .WithMany(ci => ci.CartsItems)
        //           .HasForeignKey(ci => ci.UserId)
        //           .OnDelete(DeleteBehavior.Cascade);
    }
}
