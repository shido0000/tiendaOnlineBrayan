using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class CarritoDetalleConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarritoDetalle>().ToTable("CarritosDetalles");
        EntidadBaseConfiguracionBD<CarritoDetalle>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<CarritoDetalle>().Property(e => e.CarritoId).IsRequired();
        modelBuilder.Entity<CarritoDetalle>().Property(e => e.ProductoId).IsRequired();
        modelBuilder.Entity<CarritoDetalle>().Property(e => e.Cantidad).IsRequired();
    }
}
