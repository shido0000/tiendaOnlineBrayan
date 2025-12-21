using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class BannerPromocionConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BannerPromocion>().ToTable("BannerPromocions");
        EntidadBaseConfiguracionBD<BannerPromocion>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<BannerPromocion>().Property(e => e.Nombre).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Imagen).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.EsActivo).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Destacado).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Orden).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Ubicaciones).IsRequired();
       
    }
}
