using API.Data.Entidades.Gestion.Nomencladores;
using Microsoft.EntityFrameworkCore;

namespace API.Data.ConfiguracionEntidades.Gestion.Nomencladores;

public class ComprobanteVentaConfiguracionDB
{
    public static void SetEntityBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComprobanteVenta>().ToTable("ComprobantesVentas");
        EntidadBaseConfiguracionBD<ComprobanteVenta>.SetEntityBuilder(modelBuilder);

        modelBuilder.Entity<ComprobanteVenta>().Property(e => e.VentaId).IsRequired();
        modelBuilder.Entity<ComprobanteVenta>().Property(e => e.FechaEmision).IsRequired();
        modelBuilder.Entity<ComprobanteVenta>().Property(e => e.Numero).IsRequired();
    }
}
