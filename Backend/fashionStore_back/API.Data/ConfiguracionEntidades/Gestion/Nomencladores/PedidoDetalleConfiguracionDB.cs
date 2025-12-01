using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class PedidoDetalleConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PedidoDetalle>().ToTable("PedidosDetalles");
        EntidadBaseConfiguracionBD<PedidoDetalle>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<PedidoDetalle>().Property(e => e.Cantidad).IsRequired();
        modelBuilder.Entity<PedidoDetalle>().Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<PedidoDetalle>().Property(e => e.DescuentoAplicado).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        modelBuilder.Entity<PedidoDetalle>().Property(e => e.EstadoLinea).HasConversion<int>().IsRequired();

        modelBuilder.Entity<PedidoDetalle>()
                   .HasOne(ci => ci.Pedido)
                   .WithMany(ci => ci.Detalles)
                   .HasForeignKey(ci => ci.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PedidoDetalle>()
               .HasOne(ci => ci.ProductoVariante)
               .WithMany()
               .HasForeignKey(ci => ci.ProductoVarianteId)
               .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PedidoDetalle>()
               .HasOne(ci => ci.Descuento)
               .WithMany()
               .HasForeignKey(ci => ci.DescuentoId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
