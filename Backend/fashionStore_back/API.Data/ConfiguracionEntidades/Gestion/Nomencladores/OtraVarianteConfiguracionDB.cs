using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class OtraVarianteConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OtraVariante>().ToTable("OtrasVariantes");
        EntidadBaseConfiguracionBD<OtraVariante>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<OtraVariante>().Property(e => e.Nombre).IsRequired().HasMaxLength(50);

       
    }
}
