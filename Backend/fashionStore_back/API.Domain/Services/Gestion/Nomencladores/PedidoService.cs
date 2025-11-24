using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class PedidoService : BasicService<Pedido, PedidoValidator>, IPedidoService
    {
        readonly IDescuentoService _DescuentoService;

        public PedidoService(IUnitOfWork<Pedido> repositorios, IHttpContextAccessor httpContext, IDescuentoService descuentoService) : base(repositorios, httpContext)
        {
            _DescuentoService = descuentoService;
        }

        public async Task ConfirmarPedidoAsync(Guid pedidoId, Guid vendedorId, IEnumerable<Guid> detalleIdsConfirmados)
        {
            using var tx = await _repositorios.BasicRepository.StartTransaction();

            var pedido = await _repositorios.Pedidos
                            .GetQuery()
                            .Include(p => p.Detalles)
                            .FirstOrDefaultAsync(p => p.Id == pedidoId)
                         ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

            var confirmarIds = detalleIdsConfirmados.ToHashSet();

            foreach (var d in pedido.Detalles.Where(x => confirmarIds.Contains(x.Id)))
            {
                if (d.EstadoLinea != EstadoLinea.Pendiente)
                    continue;

                var inv = await _repositorios.Inventarios.FirstAsync(i => i.ProductoId == d.ProductoId);

                if (inv.CantidadReservada < d.Cantidad || inv.CantidadDisponible < d.Cantidad)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario al confirmar." };

                inv.CantidadReservada -= d.Cantidad;
                inv.CantidadDisponible -= d.Cantidad;

                _repositorios.Inventarios.Update(inv);
                d.EstadoLinea = EstadoLinea.Confirmada;
                _repositorios.PedidosDetalles.Update(d);
            }

            // Recalcular estado del pedido
            var pendientes = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Pendiente);
            var confirmadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Confirmada);

            pedido.Estado = confirmadas > 0 && pendientes > 0 ? EstadoPedido.Parcial
                          : confirmadas > 0 && pendientes == 0 ? EstadoPedido.Confirmado
                          : EstadoPedido.Pendiente;

            // Crear venta si hay confirmadas
            if (confirmadas > 0)
            {
                var venta = new Venta
                {
                    PedidoId = pedido.Id,
                    UsuarioVendedorId = vendedorId,
                    FechaConfirmacion = DateTime.UtcNow,
                    TotalFinal = pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Confirmada)
                                                .Sum(x => x.PrecioUnitario * x.Cantidad - x.DescuentoAplicado)
                };
                await _repositorios.Ventas.AddAsync(venta);
                await _repositorios.SaveChangesAsync();

                var detallesVenta = pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Confirmada)
                                                   .Select(x => new VentaDetalle
                                                   {
                                                       VentaId = venta.Id,
                                                       ProductoId = x.ProductoId,
                                                       Cantidad = x.Cantidad,
                                                       PrecioUnitario = x.PrecioUnitario,
                                                       DescuentoAplicado = x.DescuentoAplicado
                                                   });

                await _repositorios.VentasDetalles.AddRangeAsync(detallesVenta);

                var comprobante = new ComprobanteVenta
                {
                    VentaId = venta.Id,
                    Numero = $"FAC-{DateTime.UtcNow:yyyy}-{venta.Consecutivo}"
                };
                await _repositorios.ComprobantesVentas.AddAsync(comprobante);
            }

            _repositorios.Pedidos.Update(pedido);
            await _repositorios.SaveChangesAsync();
            await tx.CommitAsync();
        }

        public async Task RechazarLineasAsync(Guid pedidoId, IEnumerable<Guid> detalleIdsRechazados)
        {
            using var tx = await _repositorios.BasicRepository.StartTransaction();

            var pedido = await _repositorios.Pedidos
                            .GetQuery()
                            .Include(p => p.Detalles)
                            .FirstOrDefaultAsync(p => p.Id == pedidoId)
                         ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };


            var rechazarIds = detalleIdsRechazados.ToHashSet();

            foreach (var d in pedido.Detalles.Where(x => rechazarIds.Contains(x.Id)))
            {
                if (d.EstadoLinea != EstadoLinea.Pendiente)
                    continue;

                var inv = await _repositorios.Inventarios
                    .GetQuery()
                    .FirstOrDefaultAsync(i => i.ProductoId == d.ProductoId);
                if (inv.CantidadReservada < d.Cantidad)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario al rechazar." };

                inv.CantidadReservada -= d.Cantidad;
                _repositorios.Inventarios.Update(inv);

                d.EstadoLinea = EstadoLinea.Rechazada;
                _repositorios.PedidosDetalles.Update(d);
            }

            var pendientes = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Pendiente);
            var confirmadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Confirmada);
            var rechazadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Rechazada);

            pedido.Estado = confirmadas > 0 && pendientes > 0 ? EstadoPedido.Parcial
                          : confirmadas > 0 && pendientes == 0 ? EstadoPedido.Confirmado
                          : rechazadas > 0 && confirmadas == 0 && pendientes == 0 ? EstadoPedido.Rechazado
                          : EstadoPedido.Pendiente;

            _repositorios.Pedidos.Update(pedido);
            await _repositorios.SaveChangesAsync();
            await tx.CommitAsync();
        }

        public async Task RechazarPedidoCompletoAsync(Guid pedidoId)
        {
            using var tx = await _repositorios.BasicRepository.StartTransaction();

            var pedido = await _repositorios.Pedidos
                        .GetQuery()
                        .Include(p => p.Detalles)
                        .FirstOrDefaultAsync(p => p.Id == pedidoId)
                         ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

            foreach (var d in pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Pendiente))
            {
                var inv = await _repositorios.Inventarios
                    .GetQuery()
                    .FirstOrDefaultAsync(i => i.ProductoId == d.ProductoId);
                if (inv.CantidadReservada < d.Cantidad)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario." };

                inv.CantidadReservada -= d.Cantidad;
                _repositorios.Inventarios.Update(inv);

                d.EstadoLinea = EstadoLinea.Rechazada;
                _repositorios.PedidosDetalles.Update(d);
            }

            pedido.Estado = EstadoPedido.Rechazado;
            _repositorios.Pedidos.Update(pedido);

            await _repositorios.SaveChangesAsync();
            await tx.CommitAsync();
        }


        public async Task<Guid> GenerarPedido(GenerarPedidoDto generarPedidoDto)
        {
            var MonedaVentaId = await _repositorios.Productos
                                    .GetQuery()
                                    .AsNoTracking()
                                    .Where(e => e.Id == generarPedidoDto.Productos.FirstOrDefault()!.ProductoId)
                                    .Select(e => e.MonedaVentaId)
                                    .FirstOrDefaultAsync();

            var costoEnvio = await _repositorios.Mensajerias
                                    .GetQuery()
                                    .AsNoTracking()
                                    .Where(e => e.Id == generarPedidoDto.MensajeriaId)
                                    .Select(e => e.Precio)
                                    .FirstOrDefaultAsync();

            var descuentoCupon = await _repositorios.Cupones
                                   .GetQuery()
                                   .AsNoTracking()
                                   .Where(e => e.Id == generarPedidoDto.CuponId)
                                   .Select(e => new { e.MontoFijo, e.Porcentaje })
                                   .FirstOrDefaultAsync();

            var decuentoPorCupon = 0m;
            var esPorciento = false;

            var nuevoPedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                UsuarioId = generarPedidoDto.UsuarioId,
                CuponId = generarPedidoDto.CuponId,
                Estado = EstadoPedido.Pendiente,
                Subtotal = 0m,
                Shipping = (decimal)costoEnvio, // conversión segura de int a decimal
                Discount = 0m,
                Total = 0m,
                Direccion = generarPedidoDto.Direccion,
                MonedaId = MonedaVentaId,
                Detalles = new List<PedidoDetalle>(),
                GestorPedidos = new(),
            };

            var listadoDetalles = new List<PedidoDetalle>();

            foreach (var producto in generarPedidoDto.Productos)
            {
                var descuento = await _DescuentoService.ObtenerDescuentoActivoDelProducto(producto.ProductoId);
                var productoExistente = await _repositorios.Productos
                                        .GetQuery()
                                        .FirstOrDefaultAsync(e => e.Id == producto.ProductoId);

                var descuentoAplicado = 0m;

                if (descuento != null)
                {
                    descuentoAplicado = descuento.EsMontoFijo ? descuento.Valor : productoExistente.PrecioVenta * (descuento.Valor / 100);
                }
                listadoDetalles.Add(new PedidoDetalle()
                {
                    PedidoId = nuevoPedido.Id,
                    ProductoId = producto.ProductoId,
                    DescuentoId = descuento != null ? descuento.DescuentoId : null,
                    Cantidad = producto.Cantidad,
                    PrecioUnitario = productoExistente.PrecioVenta,
                    DescuentoAplicado = descuentoAplicado,
                    LineTotal = (producto.Cantidad * productoExistente.PrecioVenta) - (producto.Cantidad * descuentoAplicado),
                    EstadoLinea = EstadoLinea.Pendiente,
                });
            }

            if (descuentoCupon != null)
            {
                decuentoPorCupon = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
                                                ? descuentoCupon.MontoFijo.Value
                                                : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0
                                                ? descuentoCupon.Porcentaje.Value
                                                : 0m;

                esPorciento = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
                                                ? false
                                                : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0
                                                ? true
                                                : false;
            }

            // Calcular descuentos por detalle
            var descuentoDetalles = listadoDetalles.Sum(e => e.Cantidad * e.DescuentoAplicado);

            // Calcular subtotal de los productos
            var subtotalProductos = listadoDetalles.Sum(e => e.Cantidad * e.PrecioUnitario);

            // Calcular descuento por cupón
            var descuentoPorCupon = !esPorciento
                ? decuentoPorCupon
                : subtotalProductos * decuentoPorCupon / 100m;

            // Asignar al pedido
            nuevoPedido.Discount = descuentoDetalles + descuentoPorCupon;
            nuevoPedido.Subtotal = descuentoDetalles;
            nuevoPedido.Total = nuevoPedido.Subtotal + nuevoPedido.Shipping - descuentoPorCupon;

            if (generarPedidoDto.GestorId.HasValue)
            {
                var nuevoGestorPedido = new GestorPedido()
                {
                    Id = Guid.NewGuid(),
                    PedidoId = nuevoPedido.Id,
                    GestorId = generarPedidoDto.GestorId,
                    PrecioAdicional = generarPedidoDto.ImpuestoGestor,
                };
                await _repositorios.GestorPedidos.AddAsync(nuevoGestorPedido);

                nuevoPedido.Total = nuevoPedido.Subtotal
                    + nuevoPedido.Shipping
                    - descuentoPorCupon
                    + (decimal)nuevoGestorPedido.PrecioAdicional.Value;
            }

            await _repositorios.Pedidos.AddAsync(nuevoPedido);
            await _repositorios.PedidosDetalles.AddRangeAsync(listadoDetalles);
            await _repositorios.SaveChangesAsync();

            return nuevoPedido.Id;
        }
    }
}