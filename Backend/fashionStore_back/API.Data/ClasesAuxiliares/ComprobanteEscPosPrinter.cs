using API.Data.Entidades.Gestion.Nomencladores;
using System.Text;

namespace API.Data.ClasesAuxiliares
{
    public class ComprobanteEscPosPrinter
    {
        public byte[] GenerarComprobante(Venta venta)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms, Encoding.UTF8);

            // Inicializar impresora
            writer.Write(new byte[] { 0x1B, 0x40 }); // ESC @

            // Encabezado centrado y en negrita
            writer.Write(new byte[] { 0x1B, 0x61, 0x01 }); // ESC a 1 (centrado)
            writer.Write(new byte[] { 0x1B, 0x45, 0x01 }); // ESC E 1 (negrita ON)
            writer.Write(Encoding.UTF8.GetBytes("*** TIENDA ONLINE ***\n"));
            writer.Write(new byte[] { 0x1B, 0x45, 0x00 }); // ESC E 0 (negrita OFF)
            writer.Write(Encoding.UTF8.GetBytes("Comprobante de Venta\n\n"));

            // Alinear a la izquierda
            writer.Write(new byte[] { 0x1B, 0x61, 0x00 }); // ESC a 0 (izquierda)
            writer.Write(Encoding.UTF8.GetBytes($"No: FAC-{venta.FechaConfirmacion:yyyy}-{venta.Consecutivo}\n"));
            writer.Write(Encoding.UTF8.GetBytes($"Fecha: {venta.FechaConfirmacion:dd/MM/yyyy HH:mm}\n"));
            writer.Write(Encoding.UTF8.GetBytes($"Cliente: {venta.Pedido.Usuario.Nombre}\n"));
            writer.Write(Encoding.UTF8.GetBytes(new string('-', 32) + "\n"));

            // Detalles de productos
            foreach (var d in venta.Detalles)
            {
                var nombre = d.Producto.Descripcion.Length > 15
                    ? d.Producto.Descripcion.Substring(0, 15)
                    : d.Producto.Descripcion;

                var subtotal = (d.Cantidad * d.PrecioUnitario) - d.DescuentoAplicado;
                string linea = $"{nombre.PadRight(15)} {d.Cantidad,2} x {d.PrecioUnitario,6:C} = {subtotal,6:C}\n";
                writer.Write(Encoding.UTF8.GetBytes(linea));
            }

            writer.Write(Encoding.UTF8.GetBytes(new string('-', 32) + "\n"));

            // Total en negrita
            writer.Write(new byte[] { 0x1B, 0x45, 0x01 }); // Negrita ON
            writer.Write(Encoding.UTF8.GetBytes($"TOTAL: {venta.TotalFinal,22:C}\n"));
            writer.Write(new byte[] { 0x1B, 0x45, 0x00 }); // Negrita OFF

            writer.Write(Encoding.UTF8.GetBytes(new string('-', 32) + "\n"));

            // Mensaje final centrado
            writer.Write(new byte[] { 0x1B, 0x61, 0x01 }); // Centrado
            writer.Write(Encoding.UTF8.GetBytes("¡Gracias por su compra!\n"));

            // Espacios y corte de papel
            writer.Write(Encoding.UTF8.GetBytes("\n\n\n"));
            writer.Write(new byte[] { 0x1D, 0x56, 0x00 }); // GS V 0 (corte total)

            writer.Flush();
            return ms.ToArray();
        }
    }
}
