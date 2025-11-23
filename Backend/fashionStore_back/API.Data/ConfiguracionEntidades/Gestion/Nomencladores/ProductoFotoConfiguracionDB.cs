using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ProductoFotoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductoFoto>().ToTable("ProductosFotos");
        EntidadBaseConfiguracionBD<ProductoFoto>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ProductoFoto>().Property(e => e.ProductoVarianteId).IsRequired();
        modelBuilder.Entity<ProductoFoto>().Property(e => e.Descripcion).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<ProductoFoto>().Property(e => e.Url).IsRequired();
        modelBuilder.Entity<ProductoFoto>().Property(e => e.EsPrincipal).IsRequired();
        modelBuilder.Entity<ProductoFoto>().Property(e => e.Orden).HasColumnType("decimal(18,2)").IsRequired();

        modelBuilder.Entity<ProductoFoto>()
             .HasOne(ci => ci.ProductoVariante)
             .WithMany(ci => ci.Fotos)
             .HasForeignKey(ci => ci.ProductoVarianteId)
             .OnDelete(DeleteBehavior.Cascade);
 
    }
}
