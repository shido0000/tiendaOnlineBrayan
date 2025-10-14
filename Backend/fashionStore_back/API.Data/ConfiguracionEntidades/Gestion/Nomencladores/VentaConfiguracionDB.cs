using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class VentaConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venta>().ToTable("Ventas");
        EntidadBaseConfiguracionBD<Venta>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Venta>().Property(e => e.Consecutivo).IsRequired();
        modelBuilder.Entity<Venta>().Property(e => e.FechaConfirmacion).IsRequired();
        modelBuilder.Entity<Venta>().Property(e => e.TotalFinal).HasColumnType("decimal(18,2)").IsRequired();


        modelBuilder.Entity<Venta>()
           .HasOne(ci => ci.Pedido)
           .WithOne(ci => ci.Venta)
           .HasForeignKey<Venta>(v => v.PedidoId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venta>()
                   .HasOne(ci => ci.UsuarioVendedor)
                   .WithMany()
                   .HasForeignKey(ci => ci.UsuarioVendedorId)
                   .OnDelete(DeleteBehavior.Restrict);
    }
}
