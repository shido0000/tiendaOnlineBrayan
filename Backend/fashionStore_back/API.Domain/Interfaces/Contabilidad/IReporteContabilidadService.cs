using System;

namespace API.Domain.Interfaces.Contabilidad
{
    public interface IReporteContabilidadService
    {
        /// <summary>
        /// Exporta un libro contable (.xlsx) con estado de cuentas, movimientos por periodo y resumen.
        /// </summary>
        /// <param name="desde">Fecha inicio (inclusive). Si null, toma desde el inicio.</param>
        /// <param name="hasta">Fecha fin (inclusive). Si null, hasta la fecha actual.</param>
        /// <returns>Contenido del archivo .xlsx en bytes</returns>
        Task<byte[]> ExportarLibroContableAsync(DateTime? desde, DateTime? hasta);
    }
}
