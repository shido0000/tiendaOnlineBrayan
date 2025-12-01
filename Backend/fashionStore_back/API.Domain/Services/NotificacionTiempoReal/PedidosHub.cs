using Microsoft.AspNetCore.SignalR;

namespace API.Domain.Services.NotificacionTiempoReal
{
    public class PedidosHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            var rol = Context.User?.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (rol == "Vendedor")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "vendedores");
            }
            else if (rol == "Administrador")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "administradores");
            }
            else if (rol == "Cliente")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "clientes");
            }

            await base.OnConnectedAsync();
        }

        // Método que el servidor puede invocar para notificar
        //public async Task NotificarNuevoPedido(Guid pedidoId)
        //{
        //    await Clients.Group("vendedores").SendAsync("PedidoGenerado", pedidoId);
        //}
    }
}