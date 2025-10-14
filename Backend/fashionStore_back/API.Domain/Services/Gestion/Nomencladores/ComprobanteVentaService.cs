using API.Data.ClasesAuxiliares;
using API.Data.Dto;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Data.IUnitOfWorks.Interfaces;
using API.Domain.Exceptions;
using API.Domain.Interfaces.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace API.Domain.Services.Gestion.Nomencladores
{
    public class ComprobanteVentaService : BasicService<ComprobanteVenta, ComprobanteVentaValidator>, IComprobanteVentaService
    {

        public ComprobanteVentaService(IUnitOfWork<ComprobanteVenta> repositorios, IHttpContextAccessor httpContext) : base(repositorios, httpContext)
        {
        }

        public async Task<ComprobanteDto> ObtenerComprobante(Guid ventaId)
        {
            var venta = await _repositorios.Ventas
                    .GetQuery()
                    .Include(v => v.Pedido).ThenInclude(p => p.Usuario)
                    .Include(v => v.Detalles).ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(v => v.Id == ventaId);

            if (venta == null) throw new CustomException() { Status = StatusCodes.Status404NotFound, Message = "Venta no encontrada." };

            var comprobante = new ComprobanteDto
            {
                Numero = $"FAC-{venta.FechaConfirmacion:yyyy}-{venta.Consecutivo}",
                Fecha = venta.FechaConfirmacion,
                Cliente = venta.Pedido.Usuario.Nombre,
                Lineas = venta.Detalles.Select(d => new LineaDto
                {
                    Producto = d.Producto.Descripcion,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Cantidad * d.PrecioUnitario - d.DescuentoAplicado
                }).ToList(),
                Total = venta.TotalFinal
            };

            return comprobante;
        }

        public async Task ImprimirComprobanteAsync(Venta venta)
        {
            //var printer = new ComprobantePrinter();
            //var texto = printer.GenerarComprobante(venta);

            // Aquí envías el texto a QZ Tray o a la impresora directamente
            // Ejemplo con RawPrinterHelper (Windows)
            //RawPrinterHelper.SendStringToPrinter("NombreImpresora", texto);

            var printer = new ComprobanteEscPosPrinter();
            var data = printer.GenerarComprobante(venta);

            // Enviar a la impresora (ejemplo con RawPrinterHelper en Windows)
            //RawPrinterHelper.SendBytesToPrinter("NombreImpresora", data, data.Length);
        }
    }
}