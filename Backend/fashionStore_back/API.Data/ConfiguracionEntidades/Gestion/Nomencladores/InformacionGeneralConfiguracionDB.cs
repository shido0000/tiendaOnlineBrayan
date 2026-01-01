using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class InformacionGeneralConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InformacionGeneral>().ToTable("InformacionesGenerales");
        EntidadBaseConfiguracionBD<InformacionGeneral>.SetEntityBuilder(modelBuilder);
    }
}
