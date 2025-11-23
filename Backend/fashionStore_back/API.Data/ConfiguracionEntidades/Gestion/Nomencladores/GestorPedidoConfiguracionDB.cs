using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class GestorPedidoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GestorPedido>().ToTable("GestorPedidos");
        EntidadBaseConfiguracionBD<GestorPedido>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<GestorPedido>().Property(e => e.GestorId).IsRequired();
        modelBuilder.Entity<GestorPedido>().Property(e => e.PedidoId).IsRequired();

        modelBuilder.Entity<GestorPedido>()
                  .HasOne(ci => ci.Gestor)
                  .WithMany(ci => ci.GestorPedidos)
                  .HasForeignKey(ci => ci.GestorId)
                  .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GestorPedido>()
                  .HasOne(ci => ci.Pedido)
                  .WithMany(ci => ci.GestorPedidos)
                  .HasForeignKey(ci => ci.PedidoId)
                  .OnDelete(DeleteBehavior.Cascade);
    }
}
