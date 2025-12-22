using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Contabilidad;

namespace API.Domain.Interfaces.Contabilidad
{
    public interface IAsientoContableService : IBaseService<AsientoContable, AsientoContableValidator>
    {
        /// <summary>
        /// Genera asientos contables automáticos cuando se confirma una venta/pedido
        /// </summary>
        /// <param name="venta">Datos de la venta a contabilizar</param>
        /// <returns>Id del asiento contable creado</returns>
        Task<Guid> GenerarAsientoVenta(Venta venta);

        /// <summary>
        /// Genera asientos contables automáticos cuando se devuelve una venta
        /// </summary>
        /// <param name="ventaId">Id de la venta a reversar</param>
        /// <returns>Id del asiento contable creado</returns>
        Task<Guid> GenerarAsientoDevolucion(Guid ventaId);
    }
}