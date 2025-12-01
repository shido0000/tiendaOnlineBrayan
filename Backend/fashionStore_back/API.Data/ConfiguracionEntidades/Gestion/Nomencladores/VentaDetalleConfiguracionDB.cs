using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class VentaDetalleConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VentaDetalle>().ToTable("VentaDetalles");
        EntidadBaseConfiguracionBD<VentaDetalle>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<VentaDetalle>().Property(e => e.Cantidad).IsRequired();
        modelBuilder.Entity<VentaDetalle>().Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<VentaDetalle>().Property(e => e.DescuentoAplicado).HasColumnType("decimal(18,2)").HasDefaultValue(0);

        modelBuilder.Entity<VentaDetalle>()
                .HasOne(ci => ci.Venta)
                .WithMany(ci => ci.Detalles)
                .HasForeignKey(ci => ci.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VentaDetalle>()
        .HasOne(ci => ci.ProductoVariante)
        .WithMany()
        .HasForeignKey(ci => ci.ProductoVarianteId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
