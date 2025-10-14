using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ListaDeseosConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListaDeseos>().ToTable("ListasDeseos");
        EntidadBaseConfiguracionBD<ListaDeseos>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ListaDeseos>().Property(e => e.UsuarioId).IsRequired();
        modelBuilder.Entity<ListaDeseos>().Property(e => e.FechaCreacion).IsRequired();
    }
}
