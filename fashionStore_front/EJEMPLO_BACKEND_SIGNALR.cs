// PedidosHub.cs
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TuTiendaOnline.DTOs;
using TuTiendaOnline.Services;

namespace TuTiendaOnline.Hubs
{
    [Authorize]
    public class PedidosHub : Hub
    {
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<PedidosHub> _logger;

        public PedidosHub(IPedidoService pedidoService, ILogger<PedidosHub> logger)
        {
            _pedidoService = pedidoService;
            _logger = logger;
        }

        /// <summary>
        /// Se ejecuta cuando un cliente se conecta
        /// Agrega el usuario a su grupo basado en su rol
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            try
            {
                var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                _logger.LogInformation($"Usuario {userId} con rol {userRole} conectado. ConnectionId: {Context.ConnectionId}");

                // Agregar el usuario a su grupo de rol
                if (!string.IsNullOrEmpty(userRole))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
                    _logger.LogInformation($"Usuario agregado al grupo: {userRole}");
                }

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en OnConnectedAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Se ejecuta cuando un cliente se desconecta
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _logger.LogInformation($"Usuario {userId} desconectado. ConnectionId: {Context.ConnectionId}");

            if (exception != null)
            {
                _logger.LogError($"Excepción en desconexión: {exception.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Recibe la notificación de un nuevo pedido confirmado desde el frontend
        /// y la envía a todos los administradores y vendedores conectados
        /// </summary>
        [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
        public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

                _logger.LogInformation(
                    $"Notificación de nuevo pedido recibida. " +
                    $"PedidoId: {datosNotificacion.PedidoId}, " +
                    $"Cliente: {datosNotificacion.Cliente}, " +
                    $"Total: {datosNotificacion.Total}, " +
                    $"Usuario: {userId}"
                );

                // Validaciones
                if (string.IsNullOrEmpty(datosNotificacion.PedidoId))
                {
                    _logger.LogWarning("PedidoId vacío en notificación");
                    return;
                }

                // Enriquecer datos si es necesario
                if (datosNotificacion.Fecha == DateTime.MinValue)
                {
                    datosNotificacion.Fecha = DateTime.UtcNow;
                }

                // Registrar en la base de datos si es necesario
                // await _pedidoService.RegistrarNotificacion(datosNotificacion);

                // Enviar notificación a los grupos de Administrador y Vendedor
                // IMPORTANTE: Solo se envía a los conectados, no a los desconectados
                await Clients.Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", datosNotificacion);

                _logger.LogInformation(
                    $"Notificación de pedido #{datosNotificacion.Codigo} enviada a Administradores y Vendedores"
                );

                // Opcional: Registrar en un log o tabla de auditoría
                LogNotificacionEnviada(datosNotificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al notificar nuevo pedido: {ex.Message}\n{ex.StackTrace}");

                // Notificar al cliente que ocurrió un error
                await Clients.Caller.SendAsync("ErrorNotificacion",
                    new { mensaje = "Error al procesar la notificación del pedido" });
            }
        }

        /// <summary>
        /// Enviar notificación de actualización de estado de pedido
        /// (puede ser llamado desde el backend cuando cambia el estado)
        /// </summary>
        [Authorize(Roles = "Admin,Administrador,Vendedor")]
        public async Task ActualizarEstadoPedido(ActualizacionEstadoPedidoDto datos)
        {
            try
            {
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                _logger.LogInformation(
                    $"Actualización de estado de pedido: {datos.PedidoId} -> {datos.NuevoEstado}"
                );

                // Enviar a todos los conectados
                await Clients.All.SendAsync("PedidoActualizado", datos);

                // Opcional: Enviar notificación específica al cliente del pedido
                if (!string.IsNullOrEmpty(datos.ClienteConnectionId))
                {
                    await Clients.Client(datos.ClienteConnectionId)
                        .SendAsync("EstadoPedidoActualizado", datos);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar estado de pedido: {ex.Message}");
            }
        }

        /// <summary>
        /// Método de prueba para verificar la conexión
        /// </summary>
        public async Task Ping()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"Ping recibido de usuario: {userId}");

            await Clients.Caller.SendAsync("Pong", new { timestamp = DateTime.UtcNow });
        }

        /// <summary>
        /// Obtener información de conexión actual
        /// </summary>
        public async Task ObtenerInfoConexion()
        {
            try
            {
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userEmail = Context.User?.FindFirst(ClaimTypes.Email)?.Value;
                var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

                var info = new
                {
                    connectionId = Context.ConnectionId,
                    userId = userId,
                    userEmail = userEmail,
                    userRole = userRole,
                    conectadoEn = DateTime.UtcNow
                };

                await Clients.Caller.SendAsync("InfoConexion", info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo info de conexión: {ex.Message}");
            }
        }

        /// <summary>
        /// Registrar notificación enviada en un log (para auditoría)
        /// </summary>
        private void LogNotificacionEnviada(NotificacionPedidoDto datos)
        {
            // Aquí puedes implementar lógica para guardar en una tabla de auditoría
            // o en un archivo de log
            _logger.LogInformation(
                $"[NOTIFICACIÓN] Código: {datos.Codigo}, " +
                $"Cliente: {datos.Cliente}, " +
                $"Total: {datos.Total}, " +
                $"Productos: {datos.Productos?.Count ?? 0}"
            );
        }
    }

    // ============================================================
    // DTOs
    // ============================================================

    /// <summary>
    /// DTO para notificación de nuevo pedido
    /// </summary>
    public class NotificacionPedidoDto
    {
        public string PedidoId { get; set; }
        public string Codigo { get; set; }
        public decimal Total { get; set; }
        public string Cliente { get; set; }
        public int CantidadProductos { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public List<ProductoNotificacionDto> Productos { get; set; } = new();
    }

    /// <summary>
    /// DTO para producto en notificación
    /// </summary>
    public class ProductoNotificacionDto
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    /// <summary>
    /// DTO para actualización de estado de pedido
    /// </summary>
    public class ActualizacionEstadoPedidoDto
    {
        public string PedidoId { get; set; }
        public string Codigo { get; set; }
        public string NuevoEstado { get; set; }
        public string ClienteConnectionId { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public string Motivo { get; set; }
    }
}

// ============================================================
// CONFIGURACIÓN EN Startup.cs / Program.cs
// ============================================================

/*
En Program.cs o ConfigureServices en Startup.cs, agregar:

// Configurar SignalR
services.AddSignalR()
    .AddMessagePackProtocol()
    .AddHubOptions<PedidosHub>(options =>
    {
        // Tiempo máximo de conexión inactiva
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);
        options.MaximumParallelInvocationsPerClient = 100;
    });

// En Configure o MapEndpoints:
endpoints.MapHub<PedidosHub>("/pedidosHub");

// CORS (si el frontend está en diferente dominio)
services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:3000", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// En Configure:
app.UseCors("SignalRPolicy");
*/

// ============================================================
// EJEMPLO DE USO - DESDE UN CONTROLADOR
// ============================================================

/*
[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IHubContext<PedidosHub> _hubContext;

    public PedidoController(IHubContext<PedidosHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("confirmar")]
    public async Task<IActionResult> ConfirmarPedido([FromBody] GenerarPedidoDto dto)
    {
        // ... lógica para crear el pedido ...
        var pedido = await CrearPedido(dto);

        // Notificar a los administradores y vendedores
        if (pedido != null)
        {
            var notificacion = new NotificacionPedidoDto
            {
                PedidoId = pedido.Id.ToString(),
                Codigo = pedido.Codigo,
                Total = pedido.Total,
                Cliente = $"{pedido.Cliente.Nombre} {pedido.Cliente.Apellido}",
                CantidadProductos = pedido.Productos.Count,
                Estado = pedido.Estado,
                Fecha = DateTime.UtcNow,
                Productos = pedido.Productos.Select(p => new ProductoNotificacionDto
                {
                    Nombre = p.Nombre,
                    Cantidad = p.Cantidad,
                    Precio = p.Precio
                }).ToList()
            };

            // Enviar directamente desde el backend a admin/vendedores
            await _hubContext.Clients.Groups("Admin", "Administrador", "Vendedor")
                .SendAsync("PedidoGenerado", notificacion);
        }

        return Ok(pedido);
    }
}
*/
