using API.Data.Dto.Pedido;
using API.Data.Entidades.Gestion.Nomencladores;
using API.Domain.Validators.Gestion.Nomencladores;

namespace API.Domain.Interfaces.Gestion.Nomencladores
{
    public interface IPedidoService : IBaseService<Pedido, PedidoValidator>
    {
        Task ConfirmarPedidoAsync(Guid pedidoId, Guid vendedorId, IEnumerable<Guid> detalleIdsConfirmados);
        Task RechazarLineasAsync(Guid pedidoId, IEnumerable<Guid> detalleIdsRechazados);
        Task RechazarPedidoCompletoAsync(Guid pedidoId);
        Task<Guid> GenerarPedido(GenerarPedidoDto generarPedidoDto);
    }
}