using API.Data.ClasesAuxiliares;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.Enum;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Contabilidad;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using API.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.Gestion.Nomencladores
{
    public class PedidoService : BasicService<Pedido, PedidoValidator>, IPedidoService
    {
        readonly IDescuentoService _DescuentoService;
        private readonly IHubContext<PedidosHub> _hubContext;
        private readonly IAsientoContableService _AsientoContableService;

        public PedidoService(IUnitOfWork<Pedido> repositorios, IHttpContextAccessor httpContext, IDescuentoService descuentoService, IHubContext<PedidosHub> hubContext, IAsientoContableService asientoContableService) : base(repositorios, httpContext)
        {
            _DescuentoService = descuentoService;
            _hubContext = hubContext;
            _AsientoContableService = asientoContableService;
        }

        public async Task<Guid> GenerarPedido(GenerarPedidoDto generarPedidoDto)
        {
            var cantPedidos = await _repositorios.Pedidos.CountAsync();

            var primerProductoVarianteId = generarPedidoDto.Productos.FirstOrDefault()?.ProductoId;

            // 🔹 Obtener producto y moneda desde la variante
            var productoVariante = new ProductoVariante();
            var productoVarianteOpcion1 = await _repositorios.ProductoVariantes
                .GetQuery()
                .AsNoTracking()
                .Include(v => v.Producto)
                    .ThenInclude(p => p.MonedaVenta)
                .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

            if (productoVarianteOpcion1 == null)
            {
                var producto = await _repositorios.Productos
                .GetQuery()
                .AsNoTracking()
                .Include(e => e.ProductosVariantes)
                .Include(e => e.MonedaVenta)
                .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

                productoVariante = producto.ProductosVariantes.FirstOrDefault();
            }
            else
            {
                productoVariante = productoVarianteOpcion1;
            }

            if (productoVariante == null)
                throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La variante de producto no existe." };

            var MonedaVentaId = productoVariante.Producto.MonedaVentaId;
            var MonedaVentaTasa = productoVariante.Producto.MonedaVenta.TasaCambio;

            // 🔹 Obtener mensajería con moneda
            var mensajeria = generarPedidoDto.MensajeriaId.HasValue
                ? await _repositorios.Mensajerias
                    .GetQuery()
                    .Include(e => e.Moneda)
                    .AsNoTracking()
                    .Where(e => e.Id == generarPedidoDto.MensajeriaId)
                    .Select(e => new { e.Precio, MonedaId = e.Moneda.Id, e.Moneda.TasaCambio })
                    .FirstOrDefaultAsync()
                : null;

            var costoEnvio = mensajeria?.Precio ?? 0m;

            // 🔹 Cupón
            Guid? cuponId = generarPedidoDto.CuponId;
            var descuentoCupon = cuponId.HasValue
                ? await _repositorios.Cupones
                    .GetQuery()
                    .AsNoTracking()
                    .Where(e => e.Id == cuponId.Value)
                    .Select(e => new { e.MontoFijo, e.Porcentaje })
                    .FirstOrDefaultAsync()
                : null;

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
                Shipping = costoEnvio,
                Discount = 0m,
                Total = 0m,
                Direccion = generarPedidoDto.Direccion,
                MonedaId = MonedaVentaId,
                Detalles = new List<PedidoDetalle>(),
                GestorPedidos = new(),
            };

            // 🔹 Ajuste de moneda si mensajería está en otra moneda
            if (mensajeria != null && mensajeria.MonedaId != MonedaVentaId)
            {
                if (MonedaVentaTasa <= 0)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La tasa de cambio debe ser mayor que 0." };

                nuevoPedido.Shipping = nuevoPedido.Shipping / MonedaVentaTasa;
            }

            var listadoDetalles = new List<PedidoDetalle>();

            foreach (var productoDto in generarPedidoDto.Productos)
            {
                var variante = new ProductoVariante();


                var varianteOpcion1 = await _repositorios.ProductoVariantes
                    .GetQuery()
                    .Include(v => v.Producto)
                    .FirstOrDefaultAsync(v => v.Id == productoDto.ProductoId);


                if (varianteOpcion1 == null)
                {
                    var producto1 = await _repositorios.Productos
                    .GetQuery()
                    .AsNoTracking()
                    .Include(e => e.ProductosVariantes)
                    .Include(e => e.MonedaVenta)
                    .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

                    variante = producto1.ProductosVariantes.FirstOrDefault();
                }
                else
                {
                    variante = varianteOpcion1;
                }


                if (variante == null)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La variante de producto no existe." };

                var producto = variante.Producto;

                var descuento = await _DescuentoService.ObtenerDescuentoActivoDelProducto(producto.Id);

                var descuentoAplicado = descuento != null
                    ? (descuento.EsMontoFijo ? descuento.Valor : producto.PrecioVenta * (descuento.Valor / 100))
                    : 0m;

                listadoDetalles.Add(new PedidoDetalle()
                {
                    PedidoId = nuevoPedido.Id,
                    ProductoVarianteId = variante.Id,
                    DescuentoId = descuento?.DescuentoId,
                    Cantidad = productoDto.Cantidad,
                    PrecioUnitario = producto.PrecioVenta,
                    DescuentoAplicado = descuentoAplicado,
                    LineTotal = (productoDto.Cantidad * producto.PrecioVenta) - (productoDto.Cantidad * descuentoAplicado),
                    EstadoLinea = EstadoLinea.Pendiente,
                });
            }

            // 🔹 Descuento por cupón
            if (descuentoCupon != null)
            {
                decuentoPorCupon = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
                    ? descuentoCupon.MontoFijo.Value
                    : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0
                        ? descuentoCupon.Porcentaje.Value
                        : 0m;

                esPorciento = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
                    ? false
                    : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0;
            }

            // 🔹 Calcular totales
            var descuentoDetalles = listadoDetalles.Sum(e => e.Cantidad * e.DescuentoAplicado);
            var subtotalProductos = listadoDetalles.Sum(e => e.Cantidad * e.PrecioUnitario);
            var descuentoPorCupon = !esPorciento
                ? decuentoPorCupon
                : subtotalProductos * decuentoPorCupon / 100m;

            nuevoPedido.Discount = descuentoDetalles + descuentoPorCupon;
            nuevoPedido.Subtotal = subtotalProductos;
            nuevoPedido.Total = nuevoPedido.Subtotal - nuevoPedido.Discount + nuevoPedido.Shipping;

            // 🔹 Gestor
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

                nuevoPedido.Total += (decimal)nuevoGestorPedido.PrecioAdicional.Value;
            }

            // Actualizar Stock

            var productos = await _repositorios.ProductoVariantes
                                        .GetQuery()
                                        .Where(p => listadoDetalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
                                        .ToListAsync();

            var productosCantidades = new List<ProductoCantidadDto>();
            foreach (var producto in productos)
            {
                var detalle = listadoDetalles.First(d => d.ProductoVarianteId == producto.Id);
                producto.Stock -= detalle.Cantidad;

                if (producto.Stock == 0)
                {
                    producto.EsActivo = false;
                }

                if (!productosCantidades.Any())
                {
                    var prodct = new ProductoCantidadDto()
                    {
                        Id = producto.ProductoId.Value,
                        Cantidad = detalle.Cantidad,
                    };
                    productosCantidades.Add(prodct);
                }
                else
                {
                    var elemento = productosCantidades.FirstOrDefault(e => e.Id == producto.ProductoId.Value);
                    if (elemento == null)
                    {
                        var prodct = new ProductoCantidadDto()
                        {
                            Id = producto.ProductoId.Value,
                            Cantidad = detalle.Cantidad,
                        };
                        productosCantidades.Add(prodct);
                    }
                    else
                    {
                        elemento.Cantidad += detalle.Cantidad;
                    }
                }
            }
            _repositorios.ProductoVariantes.UpdateRange(productos);

            var productosGenerales = await _repositorios.Productos
                               .GetQuery()
                               .ToListAsync();

            foreach (var prod in productosGenerales)
            {
                var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
                if (element != null)
                {
                    prod.StockTotal -= element.Cantidad;
                }
            }

            _repositorios.Productos.UpdateRange(productosGenerales);
            await _repositorios.SaveChangesAsync();

            // 🔹 Guardar
            await _repositorios.Pedidos.AddAsync(nuevoPedido);
            await _repositorios.PedidosDetalles.AddRangeAsync(listadoDetalles);
            await _repositorios.SaveChangesAsync();

            // 🔹 Notificar
            // 🔹 Notificar en tiempo real
            await NotificarNuevoPedido(nuevoPedido, generarPedidoDto, listadoDetalles);


            return nuevoPedido.Id;
        }

        //public async Task<Guid> GenerarPedido(GenerarPedidoDto generarPedidoDto)
        //{
        //    var cantPedidos = await _repositorios.Pedidos.CountAsync();

        //    var primerProductoVarianteId = generarPedidoDto.Productos.FirstOrDefault()?.ProductoId;

        //    // 🔹 Obtener producto y moneda desde la variante
        //    var productoVariante = new ProductoVariante();
        //    var productoVarianteOpcion1 = await _repositorios.ProductoVariantes
        //        .GetQuery()
        //        .AsNoTracking()
        //        .Include(v => v.Producto)
        //            .ThenInclude(p => p.MonedaVenta)
        //        .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

        //    if (productoVarianteOpcion1 == null)
        //    {
        //        var producto = await _repositorios.Productos
        //        .GetQuery()
        //        .AsNoTracking()
        //        .Include(e => e.ProductosVariantes)
        //        .Include(e => e.MonedaVenta)
        //        .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

        //        productoVariante = producto.ProductosVariantes.FirstOrDefault();
        //    }
        //    else
        //    {
        //        productoVariante = productoVarianteOpcion1;
        //    }

        //    if (productoVariante == null)
        //        throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La variante de producto no existe." };

        //    var MonedaVentaId = productoVariante.Producto.MonedaVentaId;
        //    var MonedaVentaTasa = productoVariante.Producto.MonedaVenta.TasaCambio;

        //    // 🔹 Obtener mensajería con moneda
        //    var mensajeria = generarPedidoDto.MensajeriaId.HasValue
        //        ? await _repositorios.Mensajerias
        //            .GetQuery()
        //            .Include(e => e.Moneda)
        //            .AsNoTracking()
        //            .Where(e => e.Id == generarPedidoDto.MensajeriaId)
        //            .Select(e => new { e.Precio, MonedaId = e.Moneda.Id, e.Moneda.TasaCambio })
        //            .FirstOrDefaultAsync()
        //        : null;

        //    var costoEnvio = mensajeria?.Precio ?? 0m;

        //    // 🔹 Cupón
        //    Guid? cuponId = generarPedidoDto.CuponId;
        //    var descuentoCupon = cuponId.HasValue
        //        ? await _repositorios.Cupones
        //            .GetQuery()
        //            .AsNoTracking()
        //            .Where(e => e.Id == cuponId.Value)
        //            .Select(e => new { e.MontoFijo, e.Porcentaje })
        //            .FirstOrDefaultAsync()
        //        : null;

        //    var decuentoPorCupon = 0m;
        //    var esPorciento = false;

        //    var nuevoPedido = new Pedido()
        //    {
        //        Id = Guid.NewGuid(),
        //        Codigo = cantPedidos + 1,
        //        UsuarioId = generarPedidoDto.UsuarioId,
        //        CuponId = generarPedidoDto.CuponId,
        //        Estado = EstadoPedido.Pendiente,
        //        Subtotal = 0m,
        //        Shipping = costoEnvio,
        //        Discount = 0m,
        //        Total = 0m,
        //        Direccion = generarPedidoDto.Direccion,
        //        MonedaId = MonedaVentaId,
        //        Detalles = new List<PedidoDetalle>(),
        //        GestorPedidos = new(),
        //    };

        //    // 🔹 Ajuste de moneda si mensajería está en otra moneda
        //    if (mensajeria != null && mensajeria.MonedaId != MonedaVentaId)
        //    {
        //        if (MonedaVentaTasa <= 0)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La tasa de cambio debe ser mayor que 0." };

        //        nuevoPedido.Shipping = nuevoPedido.Shipping / MonedaVentaTasa;
        //    }

        //    var listadoDetalles = new List<PedidoDetalle>();

        //    foreach (var productoDto in generarPedidoDto.Productos)
        //    {
        //        var variante = new ProductoVariante();


        //        var varianteOpcion1 = await _repositorios.ProductoVariantes
        //            .GetQuery()
        //            .Include(v => v.Producto)
        //            .FirstOrDefaultAsync(v => v.Id == productoDto.ProductoId);


        //        if (varianteOpcion1 == null)
        //        {
        //            var producto1 = await _repositorios.Productos
        //            .GetQuery()
        //            .AsNoTracking()
        //            .Include(e => e.ProductosVariantes)
        //            .Include(e => e.MonedaVenta)
        //            .FirstOrDefaultAsync(v => v.Id == primerProductoVarianteId);

        //            variante = producto1.ProductosVariantes.FirstOrDefault();
        //        }
        //        else
        //        {
        //            variante = varianteOpcion1;
        //        }


        //        if (variante == null)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "La variante de producto no existe." };

        //        var producto = variante.Producto;

        //        var descuento = await _DescuentoService.ObtenerDescuentoActivoDelProducto(producto.Id);

        //        var descuentoAplicado = descuento != null
        //            ? (descuento.EsMontoFijo ? descuento.Valor : producto.PrecioVenta * (descuento.Valor / 100))
        //            : 0m;

        //        listadoDetalles.Add(new PedidoDetalle()
        //        {
        //            PedidoId = nuevoPedido.Id,
        //            ProductoVarianteId = variante.Id,
        //            DescuentoId = descuento?.DescuentoId,
        //            Cantidad = productoDto.Cantidad,
        //            PrecioUnitario = producto.PrecioVenta,
        //            DescuentoAplicado = descuentoAplicado,
        //            LineTotal = (productoDto.Cantidad * producto.PrecioVenta) - (productoDto.Cantidad * descuentoAplicado),
        //            EstadoLinea = EstadoLinea.Pendiente,
        //        });
        //    }

        //    // 🔹 Descuento por cupón
        //    if (descuentoCupon != null)
        //    {
        //        decuentoPorCupon = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
        //            ? descuentoCupon.MontoFijo.Value
        //            : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0
        //                ? descuentoCupon.Porcentaje.Value
        //                : 0m;

        //        esPorciento = descuentoCupon.MontoFijo.HasValue && descuentoCupon.MontoFijo != 0
        //            ? false
        //            : descuentoCupon.Porcentaje.HasValue && descuentoCupon.Porcentaje != 0;
        //    }

        //    // 🔹 Calcular totales
        //    var descuentoDetalles = listadoDetalles.Sum(e => e.Cantidad * e.DescuentoAplicado);
        //    var subtotalProductos = listadoDetalles.Sum(e => e.Cantidad * e.PrecioUnitario);
        //    var descuentoPorCupon = !esPorciento
        //        ? decuentoPorCupon
        //        : subtotalProductos * decuentoPorCupon / 100m;

        //    nuevoPedido.Discount = descuentoDetalles + descuentoPorCupon;
        //    nuevoPedido.Subtotal = subtotalProductos;
        //    nuevoPedido.Total = nuevoPedido.Subtotal - nuevoPedido.Discount + nuevoPedido.Shipping;

        //    // 🔹 Gestor
        //    if (generarPedidoDto.GestorId.HasValue)
        //    {
        //        var nuevoGestorPedido = new GestorPedido()
        //        {
        //            Id = Guid.NewGuid(),
        //            PedidoId = nuevoPedido.Id,
        //            GestorId = generarPedidoDto.GestorId,
        //            PrecioAdicional = generarPedidoDto.ImpuestoGestor,
        //        };
        //        await _repositorios.GestorPedidos.AddAsync(nuevoGestorPedido);

        //        nuevoPedido.Total += (decimal)nuevoGestorPedido.PrecioAdicional.Value;
        //    }

        //    // 🔹 Guardar
        //    await _repositorios.Pedidos.AddAsync(nuevoPedido);
        //    await _repositorios.PedidosDetalles.AddRangeAsync(listadoDetalles);
        //    await _repositorios.SaveChangesAsync();

        //    // 🔹 Notificar
        //    // 🔹 Notificar en tiempo real
        //    await NotificarNuevoPedido(nuevoPedido, generarPedidoDto, listadoDetalles);


        //    return nuevoPedido.Id;
        //}

        private async Task NotificarNuevoPedido(Pedido pedido, GenerarPedidoDto dto, List<PedidoDetalle> detalles)
        {
            var nombreCliente = await _repositorios.Usuarios
                                    .GetQuery()
                                    .AsNoTracking()
                                    .Where(e => e.Id == pedido.UsuarioId)
                                    .Select(e => e.NombreCompleto)
                                    .FirstOrDefaultAsync();
            try
            {
                var notificacionData = new
                {
                    PedidoId = pedido.Id,
                    Cliente = nombreCliente,
                    Codigo = pedido.Codigo,
                    Total = pedido.Total,
                    Fecha = DateTime.UtcNow,
                    CantidadProductos = detalles.Count,
                    TipoNotificacion = "Nuevo Pedido",
                    Estado = pedido.Estado.ToString(),
                    MonedaId = pedido.MonedaId,
                    Items = detalles.Select(d => new
                    {
                        ProductoId = d.ProductoVarianteId,
                        Cantidad = d.Cantidad,
                        Precio = d.PrecioUnitario
                    })
                };

                // Notificar a administradores
                await _hubContext.Clients.Group("Administrador")
                    .SendAsync("PedidoGenerado", notificacionData);

                // Notificar a vendedores
                await _hubContext.Clients.Group("Vendedor")
                    .SendAsync("PedidoGenerado", notificacionData);

                Console.WriteLine($"📢 Notificación enviada - Pedido #{pedido.Codigo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar notificación: {ex.Message}");
                // No lanzar excepción para no interrumpir el flujo del pedido
            }
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


        public async Task<string> ActualizarPedidoConLineas(PedidoConfirmarDto dto)
        {
            await using var transaction = await _repositorios.BasicRepository.StartTransaction();
            var listaIguales = new List<DatosParaActualizarStockDto>();
            try
            {
                var pedido = await _repositorios.Pedidos
                    .GetQuery()
                    .Include(p => p.Detalles)
                    .FirstOrDefaultAsync(p => p.Id == dto.Id);

                if (pedido == null)
                    throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

                // Actualizar datos del pedido
                pedido.Subtotal = dto.Subtotal;
                pedido.Shipping = dto.Shipping;
                pedido.Discount = dto.Discount;
                pedido.Total = dto.Total;
                pedido.Direccion = dto.Direccion;
                pedido.Estado = EstadoPedido.Confirmado;

                // Sincronizar detalles
                var idsEnviado = dto.Detalles.Where(d => d.Id != null).Select(d => d.Id).ToList();
                var aEliminar = pedido.Detalles.Where(d => !idsEnviado.Contains(d.Id)).ToList();
                _repositorios.PedidosDetalles.RemoveRange(aEliminar);

                foreach (var detDto in dto.Detalles)
                {
                    if (detDto.Id == null)
                    {
                        // Insertar nuevo
                        var nuevo = new PedidoDetalle
                        {
                            Id = Guid.NewGuid(),
                            PedidoId = pedido.Id,
                            ProductoVarianteId = detDto.ProductoVarianteId,
                            Cantidad = detDto.Cantidad,
                            PrecioUnitario = detDto.PrecioUnitario,
                            DescuentoAplicado = detDto.DescuentoAplicado,
                            LineTotal = detDto.LineTotal,
                            EstadoLinea = EstadoLinea.Confirmada,
                        };
                        pedido.Detalles.Add(nuevo);

                        listaIguales.Add(new DatosParaActualizarStockDto
                        {
                            IdDetalle = nuevo.Id,
                            IdProducto = nuevo.ProductoVarianteId,
                            QueHago = 1,
                            Diferencia = nuevo.Cantidad
                        });
                    }
                    else
                    {
                        // Actualizar existente
                        var existente = pedido.Detalles.First(d => d.Id == detDto.Id);
                        var diferencia = detDto.Cantidad - existente.Cantidad;

                        existente.Cantidad = detDto.Cantidad;
                        existente.PrecioUnitario = detDto.PrecioUnitario;
                        existente.DescuentoAplicado = detDto.DescuentoAplicado;
                        existente.LineTotal = existente.Cantidad * existente.PrecioUnitario - existente.Cantidad * existente.DescuentoAplicado;
                        existente.EstadoLinea = EstadoLinea.Confirmada;

                        listaIguales.Add(new DatosParaActualizarStockDto
                        {
                            IdDetalle = existente.Id,
                            IdProducto = existente.ProductoVarianteId,
                            QueHago = diferencia > 0 ? 1 : diferencia < 0 ? 2 : 3,
                            Diferencia = Math.Abs(diferencia)
                        });
                    }
                }

                var ultimoConsecutivo = await _repositorios.Ventas.CountAsync();

                // Crear Venta
                var venta = new Venta()
                {
                    Id = Guid.NewGuid(),
                    Consecutivo = ultimoConsecutivo + 1,
                    PedidoId = pedido.Id,
                    UsuarioVendedorId = dto.VendedorId,
                    FechaConfirmacion = DateTime.Now,
                    TotalFinal = pedido.Total,
                    Detalles = new List<VentaDetalle>()
                };

                foreach (var det in pedido.Detalles)
                {
                    venta.Detalles.Add(new VentaDetalle()
                    {
                        VentaId = venta.Id,
                        ProductoVarianteId = det.ProductoVarianteId,
                        Cantidad = det.Cantidad,
                        PrecioUnitario = det.PrecioUnitario,
                        DescuentoAplicado = det.DescuentoAplicado,
                    });
                }
                await _repositorios.Ventas.AddAsync(venta);

                // Actualizar Stock
                var productos = await _repositorios.ProductoVariantes
                    .GetQuery()
                    .Where(p => pedido.Detalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
                    .ToListAsync();

                var productosCantidades = new List<ProductoCantidadDto>();
                foreach (var producto in productos)
                {
                    var detalle = pedido.Detalles.First(d => d.ProductoVarianteId == producto.Id);
                    var primerElementoListaStock = listaIguales.FirstOrDefault(e => e.IdProducto == producto.Id && e.IdDetalle == detalle.Id);

                    if (primerElementoListaStock != null)
                    {
                        if (primerElementoListaStock.QueHago == 2)
                        {
                            producto.Stock += primerElementoListaStock.Diferencia;
                        }
                        else if (primerElementoListaStock.QueHago == 1)
                        {
                            producto.Stock -= primerElementoListaStock.Diferencia;
                        }

                        if (producto.Stock == 0)
                        {
                            producto.EsActivo = false;
                        }

                        var cantidadAfectada = primerElementoListaStock.QueHago == 3 ? 0 : primerElementoListaStock.Diferencia;
                        if (cantidadAfectada > 0)
                        {
                            var elemento = productosCantidades.FirstOrDefault(e => e.Id == producto.ProductoId.Value);
                            if (elemento == null)
                            {
                                productosCantidades.Add(new ProductoCantidadDto
                                {
                                    Id = producto.ProductoId.Value,
                                    Cantidad = cantidadAfectada,
                                    QueHago = primerElementoListaStock.QueHago,
                                });
                            }
                            else
                            {
                                if (primerElementoListaStock.QueHago == 2)
                                {
                                    elemento.Cantidad += cantidadAfectada;
                                }
                                else if (primerElementoListaStock.QueHago == 1)
                                {
                                    elemento.Cantidad -= cantidadAfectada;
                                }
                            }
                        }
                    }
                }
                _repositorios.ProductoVariantes.UpdateRange(productos);

                var productosGenerales = await _repositorios.Productos.GetQuery().ToListAsync();
                foreach (var prod in productosGenerales)
                {
                    var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
                    if (element != null)
                    {
                        if (element.QueHago == 1)
                        {
                            prod.StockTotal -= element.Cantidad;
                        }
                        else if (element.QueHago == 2)
                        {
                            prod.StockTotal += element.Cantidad;
                        }
                    }
                }
                _repositorios.Productos.UpdateRange(productosGenerales);

                _repositorios.Pedidos.Update(pedido);
                await _repositorios.SaveChangesAsync();

                // 📊 GENERAR ASIENTO CONTABLE
                venta.Pedido = pedido;
                await _AsientoContableService.GenerarAsientoVenta(venta);

                var datosNotificacion = new
                {
                    Codigo = pedido.Codigo,
                    Estado = pedido.Estado.ToString(),
                    Total = pedido.Total,
                    UsuarioId = pedido.UsuarioId,
                    Timestamp = DateTime.UtcNow
                };
                //await _hubContext.Clients.All.SendAsync("PedidoActualizado", datosNotificacion);
                // Notificar a administradores
                await _hubContext.Clients.Group("Administrador")
                    .SendAsync("PedidoActualizado", datosNotificacion);

                // Notificar a vendedores
                await _hubContext.Clients.Group("Vendedor")
                    .SendAsync("PedidoActualizado", datosNotificacion);

                await transaction.CommitAsync();

                // Generar comprobante
                var printer = new ComprobantePrinter();
                var ventaImprimir= await _repositorios.Ventas
                                            .GetQuery()
                                            .Include(e=>e.Detalles)
                                                .ThenInclude(e=>e.ProductoVariante)
                                                    .ThenInclude(e=>e.Producto)
                                            .Include(e=>e.UsuarioVendedor)
                                            .Include(e=>e.Pedido)
                                                .ThenInclude(e=>e.Usuario)
                                            .Include(e => e.Pedido)
                                                .ThenInclude(e => e.Moneda)
                                            .FirstOrDefaultAsync(e=>e.Id == venta.Id);

                var comprobante = printer.GenerarComprobante(ventaImprimir);
                return comprobante;
            }
            catch (CustomException ex)
            {
                await transaction.RollbackAsync();
                throw new CustomException() { Status = 400, Message = ex.Message };
            }
        }


        //public async Task ActualizarPedidoConLineas(PedidoConfirmarDto dto)
        //{
        //    await using var transaction = await _repositorios.BasicRepository.StartTransaction();
        //    var listaIguales = new List<DatosParaActualizarStockDto>();
        //    try
        //    {
        //        var pedido = await _repositorios.Pedidos
        //            .GetQuery()
        //            .Include(p => p.Detalles)
        //            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        //        if (pedido == null)
        //            throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //        // Actualizar datos del pedido
        //        pedido.Subtotal = dto.Subtotal;
        //        pedido.Shipping = dto.Shipping;
        //        pedido.Discount = dto.Discount;
        //        pedido.Total = dto.Total;
        //        pedido.Direccion = dto.Direccion;
        //        pedido.Estado = EstadoPedido.Confirmado;

        //        // Sincronizar detalles
        //        var idsEnviado = dto.Detalles.Where(d => d.Id != null).Select(d => d.Id).ToList();
        //        var aEliminar = pedido.Detalles.Where(d => !idsEnviado.Contains(d.Id)).ToList();
        //        _repositorios.PedidosDetalles.RemoveRange(aEliminar);

        //        foreach (var detDto in dto.Detalles)
        //        {
        //            if (detDto.Id == null)
        //            {
        //                // Insertar nuevo
        //                var nuevo = new PedidoDetalle
        //                {
        //                    Id = Guid.NewGuid(),
        //                    PedidoId = pedido.Id,
        //                    ProductoVarianteId = detDto.ProductoVarianteId,
        //                    Cantidad = detDto.Cantidad,
        //                    PrecioUnitario = detDto.PrecioUnitario,
        //                    DescuentoAplicado = detDto.DescuentoAplicado,
        //                    LineTotal = detDto.LineTotal,
        //                    EstadoLinea = EstadoLinea.Confirmada,
        //                };
        //                pedido.Detalles.Add(nuevo);

        //                listaIguales.Add(new DatosParaActualizarStockDto
        //                {
        //                    IdDetalle = nuevo.Id,
        //                    IdProducto = nuevo.ProductoVarianteId,
        //                    QueHago = 1,
        //                    Diferencia = nuevo.Cantidad
        //                });
        //            }
        //            else
        //            {
        //                // Actualizar existente
        //                var existente = pedido.Detalles.First(d => d.Id == detDto.Id);
        //                var diferencia = detDto.Cantidad - existente.Cantidad;

        //                existente.Cantidad = detDto.Cantidad;
        //                existente.PrecioUnitario = detDto.PrecioUnitario;
        //                existente.DescuentoAplicado = detDto.DescuentoAplicado;
        //                existente.LineTotal = existente.Cantidad * existente.PrecioUnitario - existente.Cantidad * existente.DescuentoAplicado;
        //                existente.EstadoLinea = EstadoLinea.Confirmada;

        //                listaIguales.Add(new DatosParaActualizarStockDto
        //                {
        //                    IdDetalle = existente.Id,
        //                    IdProducto = existente.ProductoVarianteId,
        //                    QueHago = diferencia > 0 ? 1 : diferencia < 0 ? 2 : 3,
        //                    Diferencia = Math.Abs(diferencia)
        //                });
        //            }
        //        }

        //        var ultimoConsecutivo = await _repositorios.Ventas.CountAsync();

        //        // Crear Venta
        //        var venta = new Venta()
        //        {
        //            Id = Guid.NewGuid(),
        //            Consecutivo = ultimoConsecutivo + 1,
        //            PedidoId = pedido.Id,
        //            UsuarioVendedorId = dto.VendedorId,
        //            FechaConfirmacion = DateTime.Now,
        //            TotalFinal = pedido.Total,
        //            Detalles = new List<VentaDetalle>()
        //        };

        //        foreach (var det in pedido.Detalles)
        //        {
        //            venta.Detalles.Add(new VentaDetalle()
        //            {
        //                VentaId = venta.Id,
        //                ProductoVarianteId = det.ProductoVarianteId,
        //                Cantidad = det.Cantidad,
        //                PrecioUnitario = det.PrecioUnitario,
        //                DescuentoAplicado = det.DescuentoAplicado,
        //            });
        //        }
        //        await _repositorios.Ventas.AddAsync(venta);

        //        // Actualizar Stock
        //        var productos = await _repositorios.ProductoVariantes
        //            .GetQuery()
        //            .Where(p => pedido.Detalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
        //            .ToListAsync();

        //        var productosCantidades = new List<ProductoCantidadDto>();
        //        foreach (var producto in productos)
        //        {
        //            var detalle = pedido.Detalles.First(d => d.ProductoVarianteId == producto.Id);
        //            var primerElementoListaStock = listaIguales.FirstOrDefault(e => e.IdProducto == producto.Id && e.IdDetalle == detalle.Id);

        //            if (primerElementoListaStock != null)
        //            {
        //                if (primerElementoListaStock.QueHago == 2)
        //                {
        //                    producto.Stock += primerElementoListaStock.Diferencia;
        //                }
        //                else if (primerElementoListaStock.QueHago == 1)
        //                {
        //                    producto.Stock -= primerElementoListaStock.Diferencia;
        //                }

        //                if (producto.Stock == 0)
        //                {
        //                    producto.EsActivo = false;
        //                }

        //                var cantidadAfectada = primerElementoListaStock.QueHago == 3 ? 0 : primerElementoListaStock.Diferencia;
        //                if (cantidadAfectada > 0)
        //                {
        //                    var elemento = productosCantidades.FirstOrDefault(e => e.Id == producto.ProductoId.Value);
        //                    if (elemento == null)
        //                    {
        //                        productosCantidades.Add(new ProductoCantidadDto
        //                        {
        //                            Id = producto.ProductoId.Value,
        //                            Cantidad = cantidadAfectada,
        //                            QueHago = primerElementoListaStock.QueHago,
        //                        });
        //                    }
        //                    else
        //                    {
        //                        if (primerElementoListaStock.QueHago == 2)
        //                        {
        //                            elemento.Cantidad += cantidadAfectada;
        //                        }
        //                        else if (primerElementoListaStock.QueHago == 1)
        //                        {
        //                            elemento.Cantidad -= cantidadAfectada;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        _repositorios.ProductoVariantes.UpdateRange(productos);

        //        var productosGenerales = await _repositorios.Productos.GetQuery().ToListAsync();
        //        foreach (var prod in productosGenerales)
        //        {
        //            var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
        //            if (element != null)
        //            {
        //                if (element.QueHago == 1)
        //                {
        //                    prod.StockTotal -= element.Cantidad;
        //                }
        //                else if (element.QueHago == 2)
        //                {
        //                    prod.StockTotal += element.Cantidad;
        //                }
        //            }
        //        }
        //        _repositorios.Productos.UpdateRange(productosGenerales);

        //        _repositorios.Pedidos.Update(pedido);
        //        await _repositorios.SaveChangesAsync();

        //        // 📊 GENERAR ASIENTO CONTABLE
        //        venta.Pedido = pedido;
        //        await _AsientoContableService.GenerarAsientoVenta(venta);

        //        var datosNotificacion = new
        //        {
        //            Codigo = pedido.Codigo,
        //            Estado = pedido.Estado.ToString(),
        //            Total = pedido.Total,
        //            UsuarioId = pedido.UsuarioId,
        //            Timestamp = DateTime.UtcNow
        //        };
        //        //await _hubContext.Clients.All.SendAsync("PedidoActualizado", datosNotificacion);
        //        // Notificar a administradores
        //        await _hubContext.Clients.Group("Administrador")
        //            .SendAsync("PedidoActualizado", datosNotificacion);

        //        // Notificar a vendedores
        //        await _hubContext.Clients.Group("Vendedor")
        //            .SendAsync("PedidoActualizado", datosNotificacion);

        //        await transaction.CommitAsync();
        //    }
        //    catch (CustomException ex)
        //    {
        //        await transaction.RollbackAsync();
        //        throw new CustomException() { Status = 400, Message = ex.Message };
        //    }
        //}


        //public async Task ActualizarPedidoConLineas(PedidoConfirmarDto dto)
        //{
        //    await using var transaction = await _repositorios.BasicRepository.StartTransaction();
        //    try
        //    {
        //        var pedido = await _repositorios.Pedidos
        //        .GetQuery()
        //        .Include(p => p.Detalles)
        //        .FirstOrDefaultAsync(p => p.Id == dto.Id);

        //        if (pedido == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //        // Actualizar datos del pedido
        //        pedido.Subtotal = dto.Subtotal;
        //        pedido.Shipping = dto.Shipping;
        //        pedido.Discount = dto.Discount;
        //        pedido.Total = dto.Total;
        //        pedido.Direccion = dto.Direccion;
        //        pedido.Estado = EstadoPedido.Confirmado;

        //        // Sincronizar detalles
        //        var idsEnviado = dto.Detalles.Where(d => d.Id != null).Select(d => d.Id).ToList();

        //        // Eliminar los que no están en el DTO
        //        var aEliminar = pedido.Detalles.Where(d => !idsEnviado.Contains(d.Id)).ToList();
        //        _repositorios.PedidosDetalles.RemoveRange(aEliminar);

        //        foreach (var detDto in dto.Detalles)
        //        {
        //            if (detDto.Id == null)
        //            {
        //                // Insertar nuevo
        //                var nuevo = new PedidoDetalle
        //                {
        //                    PedidoId = pedido.Id,
        //                    ProductoVarianteId = detDto.ProductoVarianteId,
        //                    Cantidad = detDto.Cantidad,
        //                    PrecioUnitario = detDto.PrecioUnitario,
        //                    DescuentoAplicado = detDto.DescuentoAplicado,
        //                    LineTotal = detDto.LineTotal,
        //                    EstadoLinea = EstadoLinea.Confirmada,
        //                };
        //                pedido.Detalles.Add(nuevo);
        //            }
        //            else
        //            {
        //                // Actualizar existente
        //                var existente = pedido.Detalles.First(d => d.Id == detDto.Id);
        //                existente.Cantidad = detDto.Cantidad;
        //                existente.PrecioUnitario = detDto.PrecioUnitario;
        //                existente.DescuentoAplicado = detDto.DescuentoAplicado;
        //                existente.LineTotal = existente.Cantidad * existente.PrecioUnitario - existente.Cantidad * existente.DescuentoAplicado;
        //                existente.EstadoLinea = EstadoLinea.Confirmada;
        //            }
        //        }

        //        var ultimoConsecutivo = await _repositorios.Ventas.CountAsync();

        //        // Crear Venta
        //        var venta = new Venta()
        //        {
        //            Id = Guid.NewGuid(),
        //            Consecutivo = ultimoConsecutivo + 1,
        //            PedidoId = pedido.Id,
        //            UsuarioVendedorId = dto.VendedorId,
        //            FechaConfirmacion = DateTime.Now,
        //            TotalFinal = pedido.Total,
        //            Detalles = new List<VentaDetalle>()
        //        };

        //        foreach (var det in pedido.Detalles)
        //        {
        //            var ventaDet = new VentaDetalle()
        //            {
        //                VentaId = venta.Id,
        //                ProductoVarianteId = det.ProductoVarianteId,
        //                Cantidad = det.Cantidad,
        //                PrecioUnitario = det.PrecioUnitario,
        //                DescuentoAplicado = det.DescuentoAplicado,
        //            };
        //            venta.Detalles.Add(ventaDet);
        //        }
        //        await _repositorios.Ventas.AddAsync(venta);

        //        // Actualizar Stock
        //        var productos = await _repositorios.ProductoVariantes
        //                                .GetQuery()
        //                                .Where(p => pedido.Detalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
        //                                .ToListAsync();

        //        var productosCantidades = new List<ProductoCantidadDto>();
        //        foreach (var producto in productos)
        //        {
        //            var detalle = pedido.Detalles.First(d => d.ProductoVarianteId == producto.Id);
        //            producto.Stock -= detalle.Cantidad;

        //            if (producto.Stock == 0)
        //            {
        //                producto.EsActivo = false;
        //            }

        //            if (!productosCantidades.Any())
        //            {
        //                var prodct = new ProductoCantidadDto()
        //                {
        //                    Id = producto.ProductoId.Value,
        //                    Cantidad = detalle.Cantidad,
        //                };
        //                productosCantidades.Add(prodct);
        //            }
        //            else
        //            {
        //                var elemento = productosCantidades.FirstOrDefault(e => e.Id == producto.ProductoId.Value);
        //                if (elemento == null)
        //                {
        //                    var prodct = new ProductoCantidadDto()
        //                    {
        //                        Id = producto.ProductoId.Value,
        //                        Cantidad = detalle.Cantidad,
        //                    };
        //                    productosCantidades.Add(prodct);
        //                }
        //                else
        //                {
        //                    elemento.Cantidad += detalle.Cantidad;
        //                }
        //            }
        //        }
        //        _repositorios.ProductoVariantes.UpdateRange(productos);

        //        var productosGenerales = await _repositorios.Productos
        //                           .GetQuery()
        //                           .ToListAsync();

        //        foreach (var prod in productosGenerales)
        //        {
        //            var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
        //            if (element != null)
        //            {
        //                prod.StockTotal -= element.Cantidad;
        //            }
        //        }

        //        _repositorios.Productos.UpdateRange(productosGenerales);

        //        _repositorios.Pedidos.Update(pedido);
        //        await _repositorios.SaveChangesAsync();

        //        // 📊 GENERAR ASIENTO CONTABLE - Después de confirmar la venta

        //        venta.Pedido = pedido; // Asignar pedido para la descripción del asiento
        //        await _AsientoContableService.GenerarAsientoVenta(venta);
        //        var datosNotificacion = new
        //        {
        //            Codigo = pedido.Codigo,
        //            Estado = pedido.Estado.ToString(),
        //            Total = pedido.Total,
        //            UsuarioId = pedido.UsuarioId,
        //            Timestamp = DateTime.UtcNow
        //        };
        //        await _hubContext.Clients.All.SendAsync("PedidoActualizado", datosNotificacion);
        //        // Confirmar transacción ANTES del asiento contable
        //        await transaction.CommitAsync();
        //    }
        //    catch (CustomException ex)
        //    {
        //        await transaction.RollbackAsync();
        //        throw new CustomException() { Status = 400, Message = ex.Message };
        //    }
        //}

        //public async Task ActualizarPedidoConLineas(PedidoConfirmarDto dto)
        //{
        //    await using var transaction = await _repositorios.BasicRepository.StartTransaction();
        //    var listaIguales = new List<DatosParaActualizarStockDto>();
        //    try
        //    {
        //        var pedido = await _repositorios.Pedidos
        //        .GetQuery()
        //        .Include(p => p.Detalles)
        //        .FirstOrDefaultAsync(p => p.Id == dto.Id);

        //        if (pedido == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //        // Actualizar datos del pedido
        //        pedido.Subtotal = dto.Subtotal;
        //        pedido.Shipping = dto.Shipping;
        //        pedido.Discount = dto.Discount;
        //        pedido.Total = dto.Total;
        //        pedido.Direccion = dto.Direccion;
        //        pedido.Estado = EstadoPedido.Confirmado;

        //        // Sincronizar detalles
        //        var idsEnviado = dto.Detalles.Where(d => d.Id != null).Select(d => d.Id).ToList();

        //        // Eliminar los que no están en el DTO
        //        var aEliminar = pedido.Detalles.Where(d => !idsEnviado.Contains(d.Id)).ToList();
        //        _repositorios.PedidosDetalles.RemoveRange(aEliminar);

        //        foreach (var detDto in dto.Detalles)
        //        {
        //            if (detDto.Id == null)
        //            {
        //                // Insertar nuevo
        //                var nuevo = new PedidoDetalle
        //                {
        //                    Id = Guid.NewGuid(),
        //                    PedidoId = pedido.Id,
        //                    ProductoVarianteId = detDto.ProductoVarianteId,
        //                    Cantidad = detDto.Cantidad,
        //                    PrecioUnitario = detDto.PrecioUnitario,
        //                    DescuentoAplicado = detDto.DescuentoAplicado,
        //                    LineTotal = detDto.LineTotal,
        //                    EstadoLinea = EstadoLinea.Confirmada,
        //                };
        //                pedido.Detalles.Add(nuevo);

        //                listaIguales.Add(new DatosParaActualizarStockDto
        //                {
        //                    IdDetalle = nuevo.Id,
        //                    IdProducto = nuevo.ProductoVarianteId,
        //                    QueHago = 1,
        //                    Diferencia = nuevo.Cantidad // opcional, para saber cuánto sumar/restar

        //                });
        //            }
        //            else
        //            {
        //                // Actualizar existente
        //                var existente = pedido.Detalles.First(d => d.Id == detDto.Id);
        //                var diferencia = detDto.Cantidad - existente.Cantidad;

        //                existente.Cantidad = detDto.Cantidad;
        //                existente.PrecioUnitario = detDto.PrecioUnitario;
        //                existente.DescuentoAplicado = detDto.DescuentoAplicado;
        //                existente.LineTotal = existente.Cantidad * existente.PrecioUnitario - existente.Cantidad * existente.DescuentoAplicado;
        //                existente.EstadoLinea = EstadoLinea.Confirmada;

        //                listaIguales.Add(new DatosParaActualizarStockDto
        //                {
        //                    IdDetalle = existente.Id,
        //                    IdProducto = existente.ProductoVarianteId,
        //                    QueHago = detDto.Cantidad - existente.Cantidad > 0 ? 1 : detDto.Cantidad - existente.Cantidad < 0 ? 2 : 3,
        //                    Diferencia = Math.Abs(diferencia) // opcional, para saber cuánto sumar/restar
        //                });
        //            }
        //        }

        //        var ultimoConsecutivo = await _repositorios.Ventas.CountAsync();

        //        // Crear Venta
        //        var venta = new Venta()
        //        {
        //            Id = Guid.NewGuid(),
        //            Consecutivo = ultimoConsecutivo + 1,
        //            PedidoId = pedido.Id,
        //            UsuarioVendedorId = dto.VendedorId,
        //            FechaConfirmacion = DateTime.Now,
        //            TotalFinal = pedido.Total,
        //            Detalles = new List<VentaDetalle>()
        //        };

        //        foreach (var det in pedido.Detalles)
        //        {
        //            var ventaDet = new VentaDetalle()
        //            {
        //                VentaId = venta.Id,
        //                ProductoVarianteId = det.ProductoVarianteId,
        //                Cantidad = det.Cantidad,
        //                PrecioUnitario = det.PrecioUnitario,
        //                DescuentoAplicado = det.DescuentoAplicado,
        //            };
        //            venta.Detalles.Add(ventaDet);
        //        }
        //        await _repositorios.Ventas.AddAsync(venta);

        //        // Actualizar Stock
        //        var productos = await _repositorios.ProductoVariantes
        //                                .GetQuery()
        //                                .Where(p => pedido.Detalles.Select(d => d.ProductoVarianteId).Contains(p.Id))
        //                                .ToListAsync();

        //        var productosCantidades = new List<ProductoCantidadDto>();
        //        foreach (var producto in productos)
        //        {
        //            var detalle = pedido.Detalles.First(d => d.ProductoVarianteId == producto.Id);

        //            var primerElementoListaStock = listaIguales.FirstOrDefault(e => e.IdProducto == producto.Id && e.IdDetalle == detalle.Id);

        //            if (primerElementoListaStock.QueHago == 1)
        //            {
        //                producto.Stock -= primerElementoListaStock.Diferencia;
        //            }
        //            else if (primerElementoListaStock.QueHago == 2)
        //            {
        //                producto.Stock += primerElementoListaStock.Diferencia;
        //            }

        //            if (producto.Stock == 0)
        //            {
        //                producto.EsActivo = false;
        //            }

        //            //var cantidadAfectada = primerElementoListaStock.QueHago == 3 ? 0 : primerElementoListaStock.Diferencia;

        //            //if (!productosCantidades.Any())
        //            //{
        //            //    var prodct = new ProductoCantidadDto()
        //            //    {
        //            //        Id = producto.ProductoId.Value,
        //            //        Cantidad = detalle.Cantidad,
        //            //    };
        //            //    productosCantidades.Add(prodct);
        //            //}
        //            //else
        //            {
        //                var elemento = productosCantidades.FirstOrDefault(e => e.Id == producto.ProductoId.Value);
        //                if (elemento == null)
        //                {
        //                    var prodct = new ProductoCantidadDto()
        //                    {
        //                        Id = producto.ProductoId.Value,
        //                       // Cantidad = detalle.Cantidad,
        //                        Cantidad = cantidadAfectada,
        //                    };
        //                    productosCantidades.Add(prodct);
        //                }
        //                else
        //                {
        //                    elemento.Cantidad += detalle.Cantidad;
        //                }
        //            }
        //        }
        //      //  _repositorios.ProductoVariantes.UpdateRange(productos);

        //        var productosGenerales = await _repositorios.Productos
        //                           .GetQuery()
        //                           .ToListAsync();

        //        foreach (var prod in productosGenerales)
        //        {
        //            var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
        //            if (element != null)
        //            {
        //                prod.StockTotal -= element.Cantidad;
        //            }
        //        }

        //        _repositorios.Productos.UpdateRange(productosGenerales);

        //        _repositorios.Pedidos.Update(pedido);
        //        await _repositorios.SaveChangesAsync();

        //        // 📊 GENERAR ASIENTO CONTABLE - Después de confirmar la venta

        //        venta.Pedido = pedido; // Asignar pedido para la descripción del asiento
        //        await _AsientoContableService.GenerarAsientoVenta(venta);
        //        var datosNotificacion = new
        //        {
        //            Codigo = pedido.Codigo,
        //            Estado = pedido.Estado.ToString(),
        //            Total = pedido.Total,
        //            UsuarioId = pedido.UsuarioId,
        //            Timestamp = DateTime.UtcNow
        //        };
        //        await _hubContext.Clients.All.SendAsync("PedidoActualizado", datosNotificacion);
        //        // Confirmar transacción ANTES del asiento contable
        //        await transaction.CommitAsync();
        //    }
        //    catch (CustomException ex)
        //    {
        //        await transaction.RollbackAsync();
        //        throw new CustomException() { Status = 400, Message = ex.Message };
        //    }
        //}

        public async Task CancelarPedido(Guid id)
        {
            var pedido = await _repositorios.Pedidos
                .GetQuery()
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

            var listaProductosVariantesActualizar = new List<ProductoVariante>();
            var productosCantidades = new List<ProductoCantidadDto>();

            // Cambiar estado del pedido
            pedido.Estado = EstadoPedido.Rechazado;

            foreach (var det in pedido.Detalles)
            {
                det.EstadoLinea = EstadoLinea.Rechazada;

                var productoVariante = await _repositorios.ProductoVariantes
                    .GetQuery()
                    .FirstOrDefaultAsync(e => e.Id == det.ProductoVarianteId);

                if (productoVariante == null) continue;

                // Devolver al stock la cantidad del detalle
                productoVariante.Stock += det.Cantidad;

                listaProductosVariantesActualizar.Add(productoVariante);

                // Acumular la cantidad devuelta por producto general
                var elemento = productosCantidades.FirstOrDefault(e => e.Id == productoVariante.ProductoId.Value);
                if (elemento == null)
                {
                    productosCantidades.Add(new ProductoCantidadDto
                    {
                        Id = productoVariante.ProductoId.Value,
                        Cantidad = det.Cantidad
                    });
                }
                else
                {
                    elemento.Cantidad += det.Cantidad;
                }
            }

            _repositorios.ProductoVariantes.UpdateRange(listaProductosVariantesActualizar);

            // Actualizar stock total de productos generales
            var productosGenerales = await _repositorios.Productos.GetQuery().ToListAsync();
            foreach (var prod in productosGenerales)
            {
                var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
                if (element != null)
                {
                    prod.StockTotal += element.Cantidad;
                }
            }

            _repositorios.Productos.UpdateRange(productosGenerales);
            _repositorios.Pedidos.Update(pedido);
            await _repositorios.SaveChangesAsync();

            // ✅ Emitir evento correcto con datos del pedido
            var datosNotificacion = new
            {
                Codigo = pedido.Codigo,
                Estado = pedido.Estado.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _hubContext.Clients.All.SendAsync("PedidoCancelado", datosNotificacion);
        }


        //public async Task CancelarPedido(Guid id)
        //{
        //    var pedido = await _repositorios.Pedidos
        //          .GetQuery()
        //          .Include(p => p.Detalles)
        //          .FirstOrDefaultAsync(p => p.Id == id);

        //    if (pedido == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Pedido no encontrado." };

        //    var listaProductosVariantesActualizar = new List<ProductoVariante>();
        //    var productosCantidades = new List<ProductoCantidadDto>();

        //    pedido.Estado = EstadoPedido.Rechazado;
        //    foreach (var det in pedido.Detalles)
        //    {
        //        det.EstadoLinea = EstadoLinea.Rechazada;
        //        var productoVariante = await _repositorios.ProductoVariantes
        //                                       .GetQuery()
        //                                       .FirstOrDefaultAsync(e => e.Id == det.ProductoVarianteId);

        //        productoVariante.Stock += det.Cantidad;

        //        listaProductosVariantesActualizar.Add(productoVariante);

        //        if (!productosCantidades.Any())
        //        {
        //            var prodct = new ProductoCantidadDto()
        //            {
        //                Id = productoVariante.ProductoId.Value,
        //                Cantidad = productoVariante.Stock,
        //            };
        //            productosCantidades.Add(prodct);
        //        }
        //        else
        //        {
        //            var elemento = productosCantidades.FirstOrDefault(e => e.Id == productoVariante.ProductoId.Value);
        //            if (elemento == null)
        //            {
        //                var prodct = new ProductoCantidadDto()
        //                {
        //                    Id = productoVariante.ProductoId.Value,
        //                    Cantidad = productoVariante.Stock,
        //                };
        //                productosCantidades.Add(prodct);
        //            }
        //            else
        //            {
        //                elemento.Cantidad += productoVariante.Stock;
        //            }
        //        }

        //    }
        //    _repositorios.ProductoVariantes.UpdateRange(listaProductosVariantesActualizar);

        //    var productosGenerales = await _repositorios.Productos
        //                        .GetQuery()
        //                        .ToListAsync();

        //    foreach (var prod in productosGenerales)
        //    {
        //        var element = productosCantidades.FirstOrDefault(e => e.Id == prod.Id);
        //        if (element != null)
        //        {
        //            prod.StockTotal += element.Cantidad;
        //        }
        //    }

        //    _repositorios.Productos.UpdateRange(productosGenerales);
        //    _repositorios.Pedidos.Update(pedido);
        //    await _repositorios.SaveChangesAsync();

        //    // ✅ Emitir evento correcto con datos del pedido
        //    var datosNotificacion = new
        //    {
        //        Codigo = pedido.Codigo,
        //        Estado = pedido.Estado.ToString(),
        //        Timestamp = DateTime.UtcNow
        //    };

        //    // Notificar a todos los clientes
        //    await _hubContext.Clients.All.SendAsync("PedidoCancelado", datosNotificacion);
        //}

    }
}