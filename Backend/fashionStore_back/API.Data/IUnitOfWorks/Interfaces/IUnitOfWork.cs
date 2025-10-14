using API.Data.Entidades;
using API.Data.IUnitOfWorks.Interfaces.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;

namespace API.Data.IUnitOfWorks.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : EntidadBase
    {
        // SEGURIDAD
        IPermisoRepository Permisos { get; }
        IRolPermisoRepository RolesPermisos { get; }
        IRolRepository Roles { get; }
        IUsuarioRepository Usuarios { get; }

        // BASE
        IBaseRepository<TEntity> BasicRepository { get; }
        ITrazaRepository Trazas { get; }

        // GESTION

        ICategoriaProducto CategoriasProductos { get; }
        IDescuento Descuentos { get; }
        IInventario Inventarios { get; }
        IMoneda Monedas { get; }
        IPedido Pedidos { get; }
        IPedidoDetalle PedidosDetalles { get; }
        IProducto Productos { get; }
        IProductoCategoria ProductosCategorias { get; }
        IVenta Ventas { get; }
        IVentaDetalle VentasDetalles { get; }
        ICupon Cupones { get; }
        ICarrito Carritos { get; }
        ICarritoDetalle CarritosDetalles { get; }
        IListaDeseos ListasDeseos { get; }
        IListaDeseosDetalle ListasDeseosDetalles { get; }
        IComprobanteVenta ComprobantesVentas { get; }
        IProductoDescuento ProductosDescuentos { get; }
        IProductoFoto ProductosFotos { get; }

        // CONTABILIDAD 
        IAsientoContable AsientosContables { get; }
        ICuentaContable CuentasContables { get; }
        IMovimientoContable MovimientosContables { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
