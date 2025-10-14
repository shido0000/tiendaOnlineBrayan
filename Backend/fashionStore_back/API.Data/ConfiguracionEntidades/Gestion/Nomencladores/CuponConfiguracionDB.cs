using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class CuponConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cupon>().ToTable("Cupones");
        EntidadBaseConfiguracionBD<Cupon>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Cupon>().Property(e => e.Codigo).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Cupon>().Property(e => e.Descripcion).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Cupon>().Property(e => e.FechaInicio).IsRequired();
        modelBuilder.Entity<Cupon>().Property(e => e.FechaFin).IsRequired();
        modelBuilder.Entity<Cupon>().Property(e => e.MaximoUsos).IsRequired();
        modelBuilder.Entity<Cupon>().Property(e => e.UsosActuales).IsRequired();
    }
}
