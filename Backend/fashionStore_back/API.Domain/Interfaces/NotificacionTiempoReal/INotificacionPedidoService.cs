using API.Data.Dto.Notificacion;

namespace API.Domain.Interfaces.NotificacionTiempoReal
{
    public interface INotificacionPedidoService
    {
        Task EnviarNotificacionPedidoAsync(NotificacionPedidoDto datosNotificacion);
    }
}