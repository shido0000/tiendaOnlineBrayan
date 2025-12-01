using API.Data.ConfiguracionEntidades.Contabilidad;
using API.Data.ConfiguracionEntidades.Gestion.Nomencladores;
using API.Data.ConfiguracionEntidades.Seguridad;
using API.Data.Entidades.Contabilidad;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Entidades.Seguridad;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DbContexts
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        // SEGURIDAD
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermiso { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // GESTION  

        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCategoria> ProductosCategorias { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }

        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidosDetalles { get; set; }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentasDetalles { get; set; }
        public DbSet<Cupon> Cupones { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoDetalle> CarritosDetalles { get; set; }
        public DbSet<ListaDeseos> ListasDeseos { get; set; }
        public DbSet<ListaDeseosDetalle> ListasDeseosDetalles { get; set; }
        public DbSet<ComprobanteVenta> ComprobantesVentas { get; set; }
        public DbSet<ProductoDescuento> ProductosDescuentos { get; set; }
        public DbSet<ProductoFoto> ProductosFotos { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OtraVariante> OtraVariantes { get; set; }
        public DbSet<ProductoVariante> ProductoVariantes { get; set; }
        public DbSet<Gestor> Gestores { get; set; }
        public DbSet<GestorPedido> GestorPedidos { get; set; }
        public DbSet<Mensajeria> Mensajerias { get; set; }
        public DbSet<OtraVarianteProductoVariante> OtraVarianteProductoVariantes { get; set; }



        // CONTABILIDAD
        public DbSet<AsientoContable> AsientosContables { get; set; }
        public DbSet<CuentaContable> CuentasContables { get; set; }
        public DbSet<MovimientoContable> MovimientosContables { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // SEGURIDAD
            RolPermisoConfiguracionBD.SetEntityBuilder(modelBuilder);
            RolConfiguracionBD.SetEntityBuilder(modelBuilder);
            PermisoConfiguracionBD.SetEntityBuilder(modelBuilder);
            UsuarioConfiguracionBD.SetEntityBuilder(modelBuilder);


            // GESTION
            CategoriaProductoConfiguracionDB.SetEntityBuilder(modelBuilder);
            DescuentoConfiguracionDB.SetEntityBuilder(modelBuilder);
            InventarioConfiguracionDB.SetEntityBuilder(modelBuilder);
            MonedaConfiguracionDB.SetEntityBuilder(modelBuilder);
            PedidoConfiguracionDB.SetEntityBuilder(modelBuilder);
            PedidoDetalleConfiguracionDB.SetEntityBuilder(modelBuilder);
            ProductoCategoriaConfiguracionDB.SetEntityBuilder(modelBuilder);
            ProductoConfiguracionDB.SetEntityBuilder(modelBuilder);

            VentaConfiguracionDB.SetEntityBuilder(modelBuilder);
            VentaDetalleConfiguracionDB.SetEntityBuilder(modelBuilder);
            CuponConfiguracionDB.SetEntityBuilder(modelBuilder);
            CarritoConfiguracionDB.SetEntityBuilder(modelBuilder);
            CarritoDetalleConfiguracionDB.SetEntityBuilder(modelBuilder);
            ListaDeseosConfiguracionDB.SetEntityBuilder(modelBuilder);
            ListaDeseosDetalleConfiguracionDB.SetEntityBuilder(modelBuilder);
            ComprobanteVentaConfiguracionDB.SetEntityBuilder(modelBuilder);
            ProductoDescuentoConfiguracionDB.SetEntityBuilder(modelBuilder);
            ProductoFotoConfiguracionDB.SetEntityBuilder(modelBuilder);
            ReviewConfiguracionDB.SetEntityBuilder(modelBuilder);
            OtraVarianteConfiguracionDB.SetEntityBuilder(modelBuilder);
            ProductoVarianteConfiguracionDB.SetEntityBuilder(modelBuilder);
            GestorConfiguracionDB.SetEntityBuilder(modelBuilder);
            GestorPedidoConfiguracionDB.SetEntityBuilder(modelBuilder);
            MensajeriaConfiguracionDB.SetEntityBuilder(modelBuilder);
            OtraVarianteProductoVarianteConfiguracionDB.SetEntityBuilder(modelBuilder);


            // CONTABILIDAD
            AsientoContableConfiguracionDB.SetEntityBuilder(modelBuilder);
            CuentaContableConfiguracionDB.SetEntityBuilder(modelBuilder);
            MovimientoContableConfiguracionDB.SetEntityBuilder(modelBuilder);

            // BASE
            base.OnModelCreating(modelBuilder);
        }
    }
}
