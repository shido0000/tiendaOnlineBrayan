using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class GestorConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gestor>().ToTable("Gestores");
        EntidadBaseConfiguracionBD<Gestor>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Gestor>().Property(e => e.Nombre).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Gestor>().Property(e => e.Descripcion).IsRequired().HasMaxLength(100);
    }
}
