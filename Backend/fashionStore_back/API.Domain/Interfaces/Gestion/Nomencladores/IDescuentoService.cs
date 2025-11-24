using API.Data.Dto;
using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface IDescuentoService : IBaseService<Descuento, DescuentoValidator>
    {
        //Task<decimal> CalcularDescuentoAsync(Guid productoId, int cantidad, bool acumulativo, bool aplicarMayor);
        Task<DatosDescuento?> ObtenerDescuentoActivoDelProducto(Guid productoId);
        Task<ResultadoDescuento?> CalcularDescuentoAsync(Guid productoId, int cantidad, bool acumulativo, bool aplicarMayor);
    }
}