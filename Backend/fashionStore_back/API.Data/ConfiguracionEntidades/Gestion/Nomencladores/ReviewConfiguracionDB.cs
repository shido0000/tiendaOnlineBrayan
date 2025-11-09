using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ReviewConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().ToTable("Reviews");
        EntidadBaseConfiguracionBD<Review>.SetEntityBuilder(modelBuilder);

    }
}
