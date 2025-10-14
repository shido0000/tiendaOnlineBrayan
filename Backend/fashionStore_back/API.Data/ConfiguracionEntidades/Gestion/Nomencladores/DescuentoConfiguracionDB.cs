using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class DescuentoConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Descuento>().ToTable("Descuentos");
        EntidadBaseConfiguracionBD<Descuento>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Descuento>().Property(e => e.Nombre).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Descuento>().Property(e => e.Porcentaje).HasColumnType("decimal(5,2)").IsRequired();
        modelBuilder.Entity<Descuento>().Property(e => e.MontoFijo).HasColumnType("decimal(18,2)").IsRequired();
        modelBuilder.Entity<Descuento>().Property(e => e.EsActivo).HasDefaultValue(true);
    }
}
