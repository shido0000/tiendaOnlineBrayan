using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class OtraVarianteProductoVarianteConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OtraVarianteProductoVariante>().ToTable("OtraVarianteProductoVariantes");
        EntidadBaseConfiguracionBD<OtraVarianteProductoVariante>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<OtraVarianteProductoVariante>().Property(e => e.ProductoVarianteId).IsRequired();
        modelBuilder.Entity<OtraVarianteProductoVariante>().Property(e => e.OtraVarianteId).IsRequired();


        modelBuilder.Entity<OtraVarianteProductoVariante>()
                  .HasOne(ci => ci.ProductoVariante)
                  .WithMany(ci => ci.OtraVarianteProductoVariantes)
                  .HasForeignKey(ci => ci.ProductoVarianteId)
                  .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<OtraVarianteProductoVariante>()
             .HasOne(ci => ci.OtraVariante)
             .WithMany(ci => ci.OtraVarianteProductoVariantes)
             .HasForeignKey(ci => ci.OtraVarianteId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
