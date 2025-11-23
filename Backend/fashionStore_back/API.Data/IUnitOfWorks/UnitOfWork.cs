using API.Data.DbContexts;
using API.Data.Entidades;
using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Data.IUnitOfWorks.Interfaces.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces.Seguridad;
using API.Data.IUnitOfWorks.Repositorios;
using API.Data.IUnitOfWorks.Repositorios.Contabilidad;
using API.Data.IUnitOfWorks.Repositorios.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Repositorios.Seguridad;

namespace API.Data.IUnitOfWorks
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : EntidadBase
    {
        private readonly ApiDbContext _context;
        private readonly TrazasDbContext _trazasContext;

        // SEGURIDAD
        public IPermisoRepository Permisos { get; }
        public IRolPermisoRepository RolesPermisos { get; }
        public IRolRepository Roles { get; }
        public IUsuarioRepository Usuarios { get; }
        public ITrazaRepository Trazas { get; }

        // GESTION

        public ICategoriaProducto CategoriasProductos { get; }
        public IDescuento Descuentos { get; }
        public IInventario Inventarios { get; }
        public IMoneda Monedas { get; }
        public IPedido Pedidos { get; }
        public IPedidoDetalle PedidosDetalles { get; }
        public IProducto Productos { get; }
        public IProductoCategoria ProductosCategorias { get; }
        public IVenta Ventas { get; }
        public IVentaDetalle VentasDetalles { get; }
        public ICupon Cupones { get; }
        public ICarrito Carritos { get; }
        public ICarritoDetalle CarritosDetalles { get; }
        public IListaDeseos ListasDeseos { get; }
        public IListaDeseosDetalle ListasDeseosDetalles { get; }
        public IComprobanteVenta ComprobantesVentas { get; }
        public IProductoDescuento ProductosDescuentos { get; }
        public IProductoFoto ProductosFotos { get; }
        public IReview Reviews { get; }
        public IOtraVariante OtraVariantes { get; }
        public IProductoVariante ProductoVariantes { get; }
        public IGestor Gestores { get; }
        public IGestorPedido GestorPedidos { get; }
        public IMensajeria Mensajerias { get; }


        // CONTABILIDAD 
        public IAsientoContable AsientosContables { get; }
        public ICuentaContable CuentasContables { get; }
        public IMovimientoContable MovimientosContables { get; }

        // BASE
        public IBaseRepository<TEntity> BasicRepository { get; }

        public UnitOfWork(ApiDbContext context, TrazasDbContext trazasContext)
        {
            _context = context;

            // SEGURIDAD
            Permisos = new PermisoRepository(context);
            RolesPermisos = new RolPermisoRepository(context);
            Roles = new RolRepository(context);
            Usuarios = new UsuarioRepository(context);
            Trazas = new TrazaRepository(trazasContext);

            // GESTION
            CategoriasProductos = new CategoriaProductoRepository(context);
            Descuentos = new DescuentoRepository(context);
            Inventarios = new InventarioRepository(context);
            Monedas = new MonedaRepository(context);
            Pedidos = new PedidoRepository(context);
            PedidosDetalles = new PedidoDetalleRepository(context);
            Productos = new ProductoRepository(context);
            ProductosCategorias = new ProductoCategoriaRepository(context);

            Ventas = new VentaRepository(context);
            VentasDetalles = new VentaDetalleRepository(context);
            Cupones = new CuponRepository(context);
            Carritos = new CarritoRepository(context);
            CarritosDetalles = new CarritoDetalleRepository(context);
            ListasDeseos = new ListaDeseosRepository(context);
            ListasDeseosDetalles = new ListaDeseosDetalleRepository(context);
            ComprobantesVentas = new ComprobanteVentaRepository(context);
            ProductosDescuentos = new ProductoDescuentoRepository(context);
            ProductosFotos = new ProductoFotoRepository(context);
            Reviews = new ReviewRepository(context);
            OtraVariantes = new OtraVarianteRepository(context);
            ProductoVariantes = new ProductoVarianteRepository(context);
            Gestores = new GestorRepository(context);
            GestorPedidos = new GestorPedidoRepository(context);
            Mensajerias = new MensajeriaRepository(context);

            // CONTABILIDAD
            CuentasContables = new CuentaContableRepository(context);
            AsientosContables = new AsientoContableRepository(context);
            MovimientosContables = new MovimientoContableRepository(context);

            // BASE
            BasicRepository = new BaseRepository<TEntity>(context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => await _context.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();


    }
}
