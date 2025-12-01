using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Services.NotificacionTiempoReal;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class PedidoService : BasicService<Pedido, PedidoValidator>, IPedidoService
    {
        readonly IDescuentoService _DescuentoService;
        private readonly IHubContext<PedidosHub> _hubContext;
        public PedidoService(IUnitOfWork<Pedido> repositorios, IHttpContextAccessor httpContext, IDescuentoService descuentoService, IHubContext<PedidosHub> hubContext) : base(repositorios, httpContext)
        {
            _DescuentoService = descuentoService;
            _hubContext = hubContext;
        }

        //public async Task ConfirmarPedidoAsync(Guid pedidoId, Guid vendedorId, IEnumerable<Guid> detalleIdsConfirmados)
        //{
        //    using var tx = await _repositorios.BasicRepository.StartTransaction();

        //    var pedido = await _repositorios.Pedidos
        //                    .GetQuery()
        //                    .Include(p => p.Detalles)
        //                    .FirstOrDefaultAsync(p => p.Id == pedidoId)
        //                 ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //    var confirmarIds = detalleIdsConfirmados.ToHashSet();

        //    foreach (var d in pedido.Detalles.Where(x => confirmarIds.Contains(x.Id)))
        //    {
        //        if (d.EstadoLinea != EstadoLinea.Pendiente)
        //            continue;

        //        var inv = await _repositorios.Inventarios.FirstAsync(i => i.ProductoId == d.ProductoId);

        //        if (inv.CantidadReservada < d.Cantidad || inv.CantidadDisponible < d.Cantidad)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario al confirmar." };

        //        inv.CantidadReservada -= d.Cantidad;
        //        inv.CantidadDisponible -= d.Cantidad;

        //        _repositorios.Inventarios.Update(inv);
        //        d.EstadoLinea = EstadoLinea.Confirmada;
        //        _repositorios.PedidosDetalles.Update(d);
        //    }

        //    // Recalcular estado del pedido
        //    var pendientes = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Pendiente);
        //    var confirmadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Confirmada);

        //    pedido.Estado = confirmadas > 0 && pendientes > 0 ? EstadoPedido.Parcial
        //                  : confirmadas > 0 && pendientes == 0 ? EstadoPedido.Confirmado
        //                  : EstadoPedido.Pendiente;

        //    // Crear venta si hay confirmadas
        //    if (confirmadas > 0)
        //    {
        //        var venta = new Venta
        //        {
        //            PedidoId = pedido.Id,
        //            UsuarioVendedorId = vendedorId,
        //            FechaConfirmacion = DateTime.UtcNow,
        //            TotalFinal = pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Confirmada)
        //                                        .Sum(x => x.PrecioUnitario * x.Cantidad - x.DescuentoAplicado)
        //        };
        //        await _repositorios.Ventas.AddAsync(venta);
        //        await _repositorios.SaveChangesAsync();

        //        var detallesVenta = pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Confirmada)
        //                                           .Select(x => new VentaDetalle
        //                                           {
        //                                               VentaId = venta.Id,
        //                                               ProductoId = x.ProductoId,
        //                                               Cantidad = x.Cantidad,
        //                                               PrecioUnitario = x.PrecioUnitario,
        //                                               DescuentoAplicado = x.DescuentoAplicado
        //                                           });

        //        await _repositorios.VentasDetalles.AddRangeAsync(detallesVenta);

        //        var comprobante = new ComprobanteVenta
        //        {
        //            VentaId = venta.Id,
        //            Numero = $"FAC-{DateTime.UtcNow:yyyy}-{venta.Consecutivo}"
        //        };
        //        await _repositorios.ComprobantesVentas.AddAsync(comprobante);
        //    }

        //    _repositorios.Pedidos.Update(pedido);
        //    await _repositorios.SaveChangesAsync();
        //    await tx.CommitAsync();
        //}

        //public async Task RechazarLineasAsync(Guid pedidoId, IEnumerable<Guid> detalleIdsRechazados)
        //{
        //    using var tx = await _repositorios.BasicRepository.StartTransaction();

        //    var pedido = await _repositorios.Pedidos
        //                    .GetQuery()
        //                    .Include(p => p.Detalles)
        //                    .FirstOrDefaultAsync(p => p.Id == pedidoId)
        //                 ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };


        //    var rechazarIds = detalleIdsRechazados.ToHashSet();

        //    foreach (var d in pedido.Detalles.Where(x => rechazarIds.Contains(x.Id)))
        //    {
        //        if (d.EstadoLinea != EstadoLinea.Pendiente)
        //            continue;

        //        var inv = await _repositorios.Inventarios
        //            .GetQuery()
        //            .FirstOrDefaultAsync(i => i.ProductoId == d.ProductoId);
        //        if (inv.CantidadReservada < d.Cantidad)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario al rechazar." };

        //        inv.CantidadReservada -= d.Cantidad;
        //        _repositorios.Inventarios.Update(inv);

        //        d.EstadoLinea = EstadoLinea.Rechazada;
        //        _repositorios.PedidosDetalles.Update(d);
        //    }

        //    var pendientes = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Pendiente);
        //    var confirmadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Confirmada);
        //    var rechazadas = pedido.Detalles.Count(x => x.EstadoLinea == EstadoLinea.Rechazada);

        //    pedido.Estado = confirmadas > 0 && pendientes > 0 ? EstadoPedido.Parcial
        //                  : confirmadas > 0 && pendientes == 0 ? EstadoPedido.Confirmado
        //                  : rechazadas > 0 && confirmadas == 0 && pendientes == 0 ? EstadoPedido.Rechazado
        //                  : EstadoPedido.Pendiente;

        //    _repositorios.Pedidos.Update(pedido);
        //    await _repositorios.SaveChangesAsync();
        //    await tx.CommitAsync();
        //}

        //public async Task RechazarPedidoCompletoAsync(Guid pedidoId)
        //{
        //    using var tx = await _repositorios.BasicRepository.StartTransaction();

        //    var pedido = await _repositorios.Pedidos
        //                .GetQuery()
        //                .Include(p => p.Detalles)
        //                .FirstOrDefaultAsync(p => p.Id == pedidoId)
        //                 ?? throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //    foreach (var d in pedido.Detalles.Where(x => x.EstadoLinea == EstadoLinea.Pendiente))
        //    {
        //        var inv = await _repositorios.Inventarios
        //            .GetQuery()
        //            .FirstOrDefaultAsync(i => i.ProductoId == d.ProductoId);
        //        if (inv.CantidadReservada < d.Cantidad)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Inconsistencia de inventario." };

        //        inv.CantidadReservada -= d.Cantidad;
        //        _repositorios.Inventarios.Update(inv);

        //        d.EstadoLinea = EstadoLinea.Rechazada;
        //        _repositorios.PedidosDetalles.Update(d);
        //    }

        //    pedido.Estado = EstadoPedido.Rechazado;
        //    _repositorios.Pedidos.Update(pedido);

        //    await _repositorios.SaveChangesAsync();
        //    await tx.CommitAsync();
        //}


        public async Task<Guid> GenerarPedido(GenerarPedidoDto generarPedidoDto)
        {
            var cantPedidos = await _repositorios.Pedidos.CountAsync();

            var MonedaVentaId = await _repositorios.ProductoVariantes
                                    .GetQuery()
                                    .AsNoTracking()
                                    .Include(e => e.Producto)
                                    .Where(e => e.Id == generarPedidoDto.Productos.FirstOrDefault()!.ProductoId)
                                    .Select(e => e.Producto.MonedaVentaId)
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
                Codigo = cantPedidos + 1,
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
                var productoExistente = await _repositorios.ProductoVariantes
                                       .GetQuery()
                                       .Include(e => e.Producto)
                                       .FirstOrDefaultAsync(e => e.Id == producto.ProductoId);

                var descuento = await _DescuentoService.ObtenerDescuentoActivoDelProducto(productoExistente.Id);

                var descuentoAplicado = 0m;

                if (descuento != null)
                {
                    descuentoAplicado = descuento.EsMontoFijo ? descuento.Valor : productoExistente.Producto.PrecioVenta * (descuento.Valor / 100);
                }
                listadoDetalles.Add(new PedidoDetalle()
                {
                    PedidoId = nuevoPedido.Id,
                    ProductoVarianteId = producto.ProductoId,
                    DescuentoId = descuento != null ? descuento.DescuentoId : null,
                    Cantidad = producto.Cantidad,
                    PrecioUnitario = productoExistente.Producto.PrecioVenta,
                    DescuentoAplicado = descuentoAplicado,
                    LineTotal = (producto.Cantidad * productoExistente.Producto.PrecioVenta) - (producto.Cantidad * descuentoAplicado),
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
            nuevoPedido.Subtotal = subtotalProductos;
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

            // Notificar al vendedor en tiempo real
            //await _hubContext.Clients.Group("vendedores").SendAsync("PedidoGenerado", nuevoPedido.Id);
            // 🔴 Notificar solo a vendedores
            await _hubContext.Clients.Group("vendedores")
                .SendAsync("PedidoGenerado", new
                {
                    PedidoId = nuevoPedido.Id,
                    Total = nuevoPedido.Total,
                    Cliente = generarPedidoDto.Direccion,
                    Fecha = DateTime.UtcNow
                });
            return nuevoPedido.Id;
        }

        public async Task<PedidoObtenidoDto?> ObtenerPedidoPorId(Guid id)
        {
            var pedido = await _repositorios.Pedidos
                                 .GetQuery()
                                 .AsNoTracking()
                                 .Include(e => e.Usuario)
                                 .Include(e => e.Moneda)
                                 .Include(e => e.Cupon)
                                 .Include(e => e.Detalles)
                                    .ThenInclude(e => e.ProductoVariante)
                                 .Include(e => e.Detalles)
                                    .ThenInclude(e => e.ProductoVariante)
                                        .ThenInclude(e => e.Producto)
                                 .Include(e => e.Detalles)
                                    .ThenInclude(e => e.ProductoVariante)
                                        .ThenInclude(e => e.Producto)
                                             .ThenInclude(e => e.ProductoDescuentos)
                                 .Include(e => e.Detalles)
                                    .ThenInclude(e => e.Descuento)
                                 .Include(e => e.GestorPedidos)
                                 .FirstOrDefaultAsync(e => e.Id == id);

            var nuevoPedido = new PedidoObtenidoDto()
            {
                Id = pedido.Id,
                Codigo = pedido.Codigo,
                Usuario = pedido.Usuario.NombreCompleto,
                Estado = pedido.Estado,
                Subtotal = pedido.Subtotal,
                Shipping = pedido.Shipping,
                Discount = pedido.Discount,
                PrecioGestor = pedido.GestorPedidos.FirstOrDefault()?.PrecioAdicional ?? 0m,
                Total = pedido.Total,
                Moneda = pedido.Moneda?.Codigo ?? "-",
                Cupon = pedido.Cupon?.Codigo ?? "-",
                Direccion = pedido.Direccion ?? "-",
                Detalles = new()
            };

            foreach (var det in pedido.Detalles)
            {
                var detalle = new PedidoDetalleObtenidoDto()
                {
                    Id = det.Id,
                    Descuento = det.Descuento?.Nombre ?? "-",
                    Cantidad = det.Cantidad,
                    PrecioUnitario = det.PrecioUnitario,
                    DescuentoAplicado = det.DescuentoAplicado,
                    LineTotal = det.LineTotal,
                    EstadoLinea = det.EstadoLinea,
                    ProductoVarianteObtenidoDto = new ProductoVarianteObtenidoDto()
                    {
                        Id = det.ProductoVarianteId,
                        Talla = det.ProductoVariante?.Talla ?? "-",
                        Color = det.ProductoVariante?.Color ?? "-",
                        nombreProducto = det.ProductoVariante?.Producto?.Codigo ?? "-",
                        Descripcion = det.ProductoVariante?.Producto?.Descripcion ?? "-",
                        SKU = det.ProductoVariante?.Producto?.SKU ?? "-",
                        PrecioVenta = det.ProductoVariante?.Producto?.PrecioVenta ?? 0m,
                    }
                };
                nuevoPedido.Detalles.Add(detalle);
            }

            return nuevoPedido;
        }

        public async Task ActualizarPedidoConLineas(PedidoConfirmarDto dto)
        {
            var pedido = await _repositorios.Pedidos
                .GetQuery()
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (pedido == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

            // Actualizar datos del pedido
            pedido.Subtotal = dto.Subtotal;
            pedido.Shipping = dto.Shipping;
            pedido.Discount = dto.Discount;
            pedido.Total = dto.Total;
            pedido.Direccion = dto.Direccion;
            pedido.Estado = EstadoPedido.Confirmado;

            // Sincronizar detalles
            var idsEnviado = dto.Detalles.Where(d => d.Id != null).Select(d => d.Id).ToList();

            // Eliminar los que no están en el DTO
            var aEliminar = pedido.Detalles.Where(d => !idsEnviado.Contains(d.Id)).ToList();
            _repositorios.PedidosDetalles.RemoveRange(aEliminar);

            foreach (var detDto in dto.Detalles)
            {
                if (detDto.Id == null)
                {
                    // Insertar nuevo
                    var nuevo = new PedidoDetalle
                    {
                        PedidoId = pedido.Id,
                        ProductoVarianteId = detDto.ProductoVarianteId,
                        Cantidad = detDto.Cantidad,
                        PrecioUnitario = detDto.PrecioUnitario,
                        DescuentoAplicado = detDto.DescuentoAplicado,
                        LineTotal = detDto.LineTotal,
                        EstadoLinea = EstadoLinea.Confirmada,
                    };
                    pedido.Detalles.Add(nuevo);
                }
                else
                {
                    // Actualizar existente
                    var existente = pedido.Detalles.First(d => d.Id == detDto.Id);
                    existente.Cantidad = detDto.Cantidad;
                    existente.PrecioUnitario = detDto.PrecioUnitario;
                    existente.DescuentoAplicado = detDto.DescuentoAplicado;
                    existente.LineTotal = existente.Cantidad * existente.PrecioUnitario - existente.Cantidad * existente.DescuentoAplicado;
                    existente.EstadoLinea = EstadoLinea.Confirmada;
                }
            }

            var ultimoConsecutivo = await _repositorios.Ventas.CountAsync();

            // Crear Venta
            var venta = new Venta()
            {
                Id = Guid.NewGuid(),
                Consecutivo = ultimoConsecutivo + 1,
                PedidoId = pedido.Id,
                UsuarioVendedorId = pedido.Id,
                FechaConfirmacion = DateTime.Now,
                TotalFinal = pedido.Total,
                Detalles = new List<VentaDetalle>()
            };

            foreach (var det in pedido.Detalles)
            {
                var ventaDet = new VentaDetalle()
                {
                    VentaId = venta.Id,
                    ProductoVarianteId = det.ProductoVarianteId,
                    Cantidad = det.Cantidad,
                    PrecioUnitario = det.PrecioUnitario,
                    DescuentoAplicado = det.DescuentoAplicado,
                };
                venta.Detalles.Add(ventaDet);
            }
            await _repositorios.Ventas.AddAsync(venta);

            // Actualizar Stock
            var productos = await _repositorios.ProductoVariantes
                                    .GetQuery()
                                    .Where(p => pedido.Detalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
                                    .ToListAsync();


            foreach (var producto in productos)
            {
                var detalle = pedido.Detalles.First(d => d.ProductoVarianteId == producto.Id);
                producto.Stock -= detalle.Cantidad;
            }
            _repositorios.ProductoVariantes.UpdateRange(productos);


            _repositorios.Pedidos.Update(pedido);
            await _repositorios.SaveChangesAsync();
        }

        public async Task CancelarPedido(Guid id)
        {
            var pedido = await _repositorios.Pedidos
                  .GetQuery()
                  .Include(p => p.Detalles)
                  .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

            pedido.Estado = EstadoPedido.Rechazado;
            foreach (var det in pedido.Detalles)
            {
                det.EstadoLinea = EstadoLinea.Rechazada;
            }

            _repositorios.Pedidos.Update(pedido);
            await _repositorios.SaveChangesAsync();
        }
    }
}