using API.Data.Entidades.Contabilidad;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Contabilidad;

public class MovimientoContableConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovimientoContable>().ToTable("MovimientosContables");
        EntidadBaseConfiguracionBD<MovimientoContable>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<MovimientoContable>().Property(e => e.AsientoContableId).IsRequired();
        modelBuilder.Entity<MovimientoContable>().Property(e => e.CuentaContableId).IsRequired();
        modelBuilder.Entity<MovimientoContable>().Property(e => e.Debe).IsRequired();
        modelBuilder.Entity<MovimientoContable>().Property(e => e.Haber).IsRequired();
    }
}
