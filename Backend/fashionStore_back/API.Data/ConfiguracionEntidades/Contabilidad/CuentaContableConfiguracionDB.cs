using API.Data.Entidades.Contabilidad;
using Microsoft.EntityFrameworkCore;
using System;

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

        // Seed inicial de cuentas contables básicas
        modelBuilder.Entity<CuentaContable>().HasData(
            new CuentaContable
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Codigo = "1.1.01",
                Nombre = "Caja",
                EsActivo = true,
                EsDeMovimiento = true,
                CuentaPadreId = null
            },
            new CuentaContable
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Codigo = "5.1.01",
                Nombre = "Gastos",
                EsActivo = true,
                EsDeMovimiento = true,
                CuentaPadreId = null
            },
            new CuentaContable
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Codigo = "4.1.01",
                Nombre = "Ingresos por Ventas",
                EsActivo = true,
                EsDeMovimiento = true,
                CuentaPadreId = null
            }
        );
    }
}
