using API.Data.IUnitOfWorks.Interfaces;
using API.Data.Entidades.Contabilidad;
using API.Domain.Interfaces.Contabilidad;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace API.Domain.Services.Contabilidad
{
    public class ReporteContabilidadService : IReporteContabilidadService
    {
        private readonly IUnitOfWork<AsientoContable> _repositorios;

        public ReporteContabilidadService(IUnitOfWork<AsientoContable> repositorios)
        {
            _repositorios = repositorios;
        }

        public async Task<byte[]> ExportarLibroContableAsync(DateTime? desde, DateTime? hasta)
        {
            //desde ??= DateTime.MinValue;
            desde ??= await _repositorios.MovimientosContables
                            .GetQuery()
                            .AsNoTracking()
                            .MinAsync(e => e.FechaCreado);
            hasta ??= DateTime.UtcNow.Date;

            // Obtener cuentas con saldos (sumar movimientos)
            var cuentas = await _repositorios.CuentasContables
                .GetQuery()
                .AsNoTracking()
                .ToListAsync();

            var movimientos = await _repositorios.MovimientosContables
                .GetQuery()
                .AsNoTracking()
                .Include(m => m.Cuenta)
                .Include(m => m.Asiento)
                .Where(m => m.Asiento.Fecha.Date >= desde.Value.Date && m.Asiento.Fecha.Date <= hasta.Value.Date)
                .OrderBy(m => m.Asiento.Fecha)
                .ToListAsync();

            // Calcular saldos por cuenta (considerando todos los movimientos historicos)
            var saldos = await _repositorios.MovimientosContables
                .GetQuery()
                .AsNoTracking()
                .Include(m => m.Cuenta)
                .GroupBy(m => new { m.CuentaContableId, m.Cuenta.Codigo, m.Cuenta.Nombre })
                .Select(g => new
                {
                    CuentaId = g.Key.CuentaContableId,
                    Codigo = g.Key.Codigo,
                    Nombre = g.Key.Nombre,
                    Debe = g.Sum(x => x.Debe),
                    Haber = g.Sum(x => x.Haber)
                })
                .OrderBy(x => x.Codigo)
                .ToListAsync();

            // EPPlus v8 requires explicit license setting. Use non-commercial personal with current user.
            ExcelPackage.License.SetNonCommercialPersonal(Environment.UserName ?? "NonCommercialUser");

            using var package = new ExcelPackage();

            // Hoja 1: Estado de Cuentas
            var ws1 = package.Workbook.Worksheets.Add("EstadoCuentas");
            ws1.Cells[1, 1].Value = "Código";
            ws1.Cells[1, 2].Value = "Nombre";
            ws1.Cells[1, 3].Value = "Debe";
            ws1.Cells[1, 4].Value = "Haber";
            ws1.Cells[1, 5].Value = "Saldo";
            ws1.Row(1).Style.Font.Bold = true;

            int row = 2;
            foreach (var c in saldos)
            {
                ws1.Cells[row, 1].Value = c.Codigo;
                ws1.Cells[row, 2].Value = c.Nombre;
                ws1.Cells[row, 3].Value = (double)c.Debe;
                ws1.Cells[row, 4].Value = (double)c.Haber;
                ws1.Cells[row, 5].Value = (double)(c.Debe - c.Haber);
                row++;
            }
            ws1.Cells[1, 1, row - 1, 5].AutoFitColumns();

            // Hoja 2: Movimientos por período
            var ws2 = package.Workbook.Worksheets.Add("Movimientos");
            ws2.Cells[1, 1].Value = "Fecha";
          //  ws2.Cells[1, 2].Value = "AsientoId";
            ws2.Cells[1, 2].Value = "TipoRef";
            ws2.Cells[1, 3].Value = "CuentaCodigo";
            ws2.Cells[1, 4].Value = "CuentaNombre";
            ws2.Cells[1, 5].Value = "Debe";
            ws2.Cells[1, 6].Value = "Haber";
            ws2.Cells[1, 7].Value = "Descripcion";
            ws2.Row(1).Style.Font.Bold = true;

            row = 2;
            foreach (var m in movimientos)
            {
                ws2.Cells[row, 1].Value = m.Asiento.Fecha.ToString("yyyy-MM-dd");
              //  ws2.Cells[row, 2].Value = m.Asiento.Id.ToString();
                ws2.Cells[row, 2].Value = m.Asiento.TipoReferencia;
                ws2.Cells[row, 3].Value = m.Cuenta?.Codigo ?? string.Empty;
                ws2.Cells[row, 4].Value = m.Cuenta?.Nombre ?? string.Empty;
                ws2.Cells[row, 5].Value = (double)m.Debe;
                ws2.Cells[row, 6].Value = (double)m.Haber;
                ws2.Cells[row, 7].Value = m.Asiento.Descripcion;
                row++;
            }
            ws2.Cells[1, 1, row - 1, 7].AutoFitColumns();

            // Hoja 3: Resumen (agregar columna Periodo)
            var ws3 = package.Workbook.Worksheets.Add("Resumen");
            ws3.Cells[1, 1].Value = "Periodo";
            ws3.Cells[1, 2].Value = "Total Debe (Periodo)";
            ws3.Cells[1, 3].Value = "Total Haber (Periodo)";
            ws3.Row(1).Style.Font.Bold = true;

            var totalDebePeriodo = movimientos.Sum(x => x.Debe);
            var totalHaberPeriodo = movimientos.Sum(x => x.Haber);

            ws3.Cells[2, 1].Value = $"{desde.Value:yyyy-MM-dd} - {hasta.Value:yyyy-MM-dd}";
            ws3.Cells[2, 2].Value = (double)totalDebePeriodo;
            ws3.Cells[2, 3].Value = (double)totalHaberPeriodo;

            ws3.Cells[1, 1, 2, 3].AutoFitColumns();

            return await Task.FromResult(package.GetAsByteArray());
        }
    }
}
