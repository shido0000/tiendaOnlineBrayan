using API.Data.Dto.Notificacion;
using API.Domain.Interfaces.NotificacionTiempoReal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Hubs
{
    [Authorize]
    public class PedidosHub : Hub
    {
        private readonly ILogger<PedidosHub> _logger;
        private readonly INotificacionPedidoService _notificacionService;

        public PedidosHub(ILogger<PedidosHub> logger, INotificacionPedidoService notificacionService)
        {
            _logger = logger;
            _notificacionService = notificacionService;
        }

        //[Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
        [Authorize]
        public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                _logger.LogInformation($"📦 Notificación recibida: Pedido #{datosNotificacion.Codigo}");

                // Llamar al servicio para enviar
                await _notificacionService.EnviarNotificacionPedidoAsync(datosNotificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            //var userRole = Context.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var userRole = Context.User?.FindFirst("Rol")?.Value;
           // var userId = Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var userId = Context.User?.FindFirst("Id")?.Value;

            _logger.LogInformation($"🔗 Usuario {userId} conectado. Rol: {userRole}. ConnectionId: {Context.ConnectionId}");

            if (!string.IsNullOrEmpty(userRole))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
                _logger.LogInformation($"✅ Usuario agregado al grupo: {userRole}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            _logger.LogInformation($"🔌 Usuario {userId} desconectado. ConnectionId: {Context.ConnectionId}");

            if (exception != null)
                _logger.LogError($"⚠️ Excepción: {exception.Message}");

            await base.OnDisconnectedAsync(exception);
        }
    }
}