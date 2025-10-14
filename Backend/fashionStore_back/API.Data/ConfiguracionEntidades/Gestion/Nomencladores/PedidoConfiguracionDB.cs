using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class PedidoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>().ToTable("Pedidos");
        EntidadBaseConfiguracionBD<Pedido>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Pedido>().Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<Pedido>().Property(e => e.Estado).HasConversion<int>().IsRequired();

        modelBuilder.Entity<Pedido>()
                   .HasOne(ci => ci.Usuario)
                   .WithMany(ci => ci.Pedidos)
                   .HasForeignKey(ci => ci.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pedido>()
                  .HasOne(ci => ci.Moneda)
                  .WithMany()
                  .HasForeignKey(ci => ci.MonedaId)
                  .OnDelete(DeleteBehavior.Restrict);
    }
}
