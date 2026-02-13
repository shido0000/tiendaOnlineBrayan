using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class BannerPromocionConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BannerPromocion>().ToTable("BannerPromocions");
        EntidadBaseConfiguracionBD<BannerPromocion>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<BannerPromocion>().Property(e => e.TextoTitulo).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.TextoSubtitulo).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.TextoBoton).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Imagen).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.EsActivo).IsRequired();
        modelBuilder.Entity<BannerPromocion>().Property(e => e.Rebajas).IsRequired().HasDefaultValue(false);


        modelBuilder.Entity<BannerPromocion>()
                .HasOne(ci => ci.CategoriaProducto)
                .WithMany(ci => ci.BannersPromociones)
                .HasForeignKey(ci => ci.CategoriaProductoId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
