using API.Data.Dto.Notificacion;
using API.Domain.Interfaces.NotificacionTiempoReal;
using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Domain.Services.NotificacionTiempoReal
{
    public class NotificacionPedidoService : INotificacionPedidoService
    {
        private readonly IHubContext<PedidosHub> _hubContext;
        private readonly ILogger<NotificacionPedidoService> _logger;

        public NotificacionPedidoService(IHubContext<PedidosHub> hubContext, ILogger<NotificacionPedidoService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task EnviarNotificacionPedidoAsync(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                _logger.LogInformation($"📦 Enviando notificación de pedido: #{datosNotificacion.Codigo}");

                // Enviar a grupos de Admin y Vendedor
                await _hubContext.Clients
                    .Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", datosNotificacion);

                _logger.LogInformation($"✅ Notificación enviada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error al enviar notificación: {ex.Message}");
            }
        }
    }
}