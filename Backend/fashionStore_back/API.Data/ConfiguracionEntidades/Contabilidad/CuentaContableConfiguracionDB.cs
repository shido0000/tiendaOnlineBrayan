using API.Data.Entidades.Contabilidad;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Contabilidad;

public class CuentaContableConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CuentaContable>().ToTable("CuentasContables");
        EntidadBaseConfiguracionBD<CuentaContable>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<CuentaContable>().Property(e => e.Codigo).IsRequired();
        modelBuilder.Entity<CuentaContable>().Property(e => e.Nombre).IsRequired();
        modelBuilder.Entity<CuentaContable>().Property(e => e.EsActivo).IsRequired();
        modelBuilder.Entity<CuentaContable>().Property(e => e.EsDeMovimiento).IsRequired();
    }
}
