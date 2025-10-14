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

        public PedidoService(IUnitOfWork<Pedido> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
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
    }
}