using API.Data.Entidades.Contabilidad;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Contabilidad;

public class AsientoContableConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsientoContable>().ToTable("AsientosContables");
        EntidadBaseConfiguracionBD<AsientoContable>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<AsientoContable>().Property(e => e.Fecha).IsRequired();
        modelBuilder.Entity<AsientoContable>().Property(e => e.Descripcion).IsRequired();
        modelBuilder.Entity<AsientoContable>().Property(e => e.ReferenciaId).IsRequired();
        modelBuilder.Entity<AsientoContable>().Property(e => e.TipoReferencia).IsRequired();
    }
}
