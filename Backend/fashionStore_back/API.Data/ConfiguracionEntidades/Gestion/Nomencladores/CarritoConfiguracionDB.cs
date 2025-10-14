using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class CarritoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>().ToTable("Carritos");
        EntidadBaseConfiguracionBD<Carrito>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Carrito>().Property(e => e.UsuarioId).IsRequired();
        modelBuilder.Entity<Carrito>().Property(e => e.FechaCreacion).IsRequired();
    }
}
