using API.Data.Entidades.Gestion.Nomencladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.ClasesAuxiliares
{
    public class ComprobantePrinter
    {
        public string GenerarComprobante(Venta venta)
        {
            var sb = new StringBuilder();

            sb.AppendLine("      *** TIENDA ONLINE ***");
            sb.AppendLine("   Comprobante de Venta");
            sb.AppendLine($"No: FAC-{venta.FechaConfirmacion:yyyy}-{venta.Consecutivo}");
            sb.AppendLine($"Fecha: {venta.FechaConfirmacion:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Cliente: {venta.Pedido.Usuario.Nombre}");
            sb.AppendLine(new string('-', 32));

            foreach (var d in venta.Detalles)
            {
                var nombre = d.ProductoVariante.Producto.Descripcion.Length > 15
                    ? d.ProductoVariante.Producto.Descripcion.Substring(0, 15)
                    : d.ProductoVariante.Producto.Descripcion;

                var subtotal = (d.Cantidad * d.PrecioUnitario) - d.DescuentoAplicado;
                sb.AppendLine($"{nombre.PadRight(15)} {d.Cantidad,2} x {d.PrecioUnitario,6:C} = {subtotal,6:C}");
            }

            sb.AppendLine(new string('-', 32));
            sb.AppendLine($"TOTAL: {venta.TotalFinal,22:C}");
            sb.AppendLine(new string('-', 32));
            sb.AppendLine("   Gracias por su compra!");
            sb.AppendLine("\n\n\n"); // espacio para corte

            return sb.ToString();
        }
    }

}
