using API.Application.Dtos.Contabilidad.AsientoContable;
using API.Data.Entidades.Contabilidad;
using API.Data.IUnitOfWorks.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Application.Mapper.Contabilidad
{
    public class VentaConsecutivoResolver : IValueResolver<AsientoContable, DetallesAsientoContableDto, string>, IValueResolver<AsientoContable, ListadoPaginadoAsientoContableDto, string>
    {
        private readonly IUnitOfWork<AsientoContable> _unitOfWork;

        public VentaConsecutivoResolver(IUnitOfWork<AsientoContable> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string Resolve(AsientoContable source, DetallesAsientoContableDto destination, string destMember, ResolutionContext context)
            => ResolveInternalAsync(source).GetAwaiter().GetResult();

        public string Resolve(AsientoContable source, ListadoPaginadoAsientoContableDto destination, string destMember, ResolutionContext context)
            => ResolveInternalAsync(source).GetAwaiter().GetResult();

        private async Task<string> ResolveInternalAsync(AsientoContable asiento)
        {
            if (asiento == null) return string.Empty;
            if (asiento.TipoReferencia != "Venta") return string.Empty;

            var venta = await _unitOfWork.Ventas
                .GetQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == asiento.ReferenciaId);

            return venta != null ? venta.Consecutivo.ToString() : "-";
        }
    }
}
