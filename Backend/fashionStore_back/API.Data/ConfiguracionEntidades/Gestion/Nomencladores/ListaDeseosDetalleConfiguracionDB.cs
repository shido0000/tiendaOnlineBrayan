using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ListaDeseosDetalleConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListaDeseosDetalle>().ToTable("ListasDeseosDetalles");
        EntidadBaseConfiguracionBD<ListaDeseosDetalle>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ListaDeseosDetalle>().Property(e => e.ListaDeseosId).IsRequired();
        modelBuilder.Entity<ListaDeseosDetalle>().Property(e => e.ProductoId).IsRequired();
    }
}
