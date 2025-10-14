using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class InventarioConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventario>().ToTable("Inventarios");
        EntidadBaseConfiguracionBD<Inventario>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<Inventario>().Property(e => e.CantidadDisponible).IsRequired().HasDefaultValue(0);
        modelBuilder.Entity<Inventario>().Property(e => e.CantidadReservada).IsRequired().HasDefaultValue(0);
        modelBuilder.Entity<Inventario>().Property(e => e.Ubicacion).HasMaxLength(100);
        modelBuilder.Entity<Inventario>().Property(e => e.EstadoProductoInventario).HasDefaultValue(EstadoProductoInventario.Disponible);

        modelBuilder.Entity<Inventario>().HasIndex(e => e.ProductoId).IsUnique();
 
    }
}
