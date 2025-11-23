using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class MensajeriaConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mensajeria>().ToTable("Mensajerias");
        EntidadBaseConfiguracionBD<Mensajeria>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Mensajeria>().Property(e => e.MonedaId).IsRequired();
        modelBuilder.Entity<Mensajeria>().Property(e => e.Descripcion).IsRequired();
        modelBuilder.Entity<Mensajeria>().Property(e => e.Precio).IsRequired();

        modelBuilder.Entity<Mensajeria>()
                  .HasOne(ci => ci.Moneda)
                  .WithMany(ci => ci.Mensajerias)
                  .HasForeignKey(ci => ci.MonedaId)
                  .OnDelete(DeleteBehavior.Cascade);
         
    }
}
