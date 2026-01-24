# ✅ Checklist Backend - Qué Implementar

## 🎯 Objetivo
Asegurarte de que tu backend tiene TODO lo necesario para recibir y enviar notificaciones en tiempo real.

---

## 1️⃣ Crear la Clase PedidosHub.cs

**Ubicación:** `YourProject/Hubs/PedidosHub.cs`

```csharp
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace YourProject.Hubs
{
    [Authorize]
    public class PedidosHub : Hub
    {
        private readonly ILogger<PedidosHub> _logger;

        public PedidosHub(ILogger<PedidosHub> logger)
        {
            _logger = logger;
        }

        // Se ejecuta cuando un usuario se conecta
        public override async Task OnConnectedAsync()
        {
            try
            {
                var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                _logger.LogInformation($"Usuario {userId} con rol {userRole} conectado. ID: {Context.ConnectionId}");

                // Agregar a grupo por rol
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

        // Recibe notificación del frontend cuando se confirma un pedido
        [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
        public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                _logger.LogInformation(
                    $"Notificación de nuevo pedido recibida. " +
                    $"PedidoId: {datosNotificacion.PedidoId}, " +
                    $"Cliente: {datosNotificacion.Cliente}"
                );

                // Enviar a todos los administradores y vendedores
                await Clients.Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", datosNotificacion);

                _logger.LogInformation(
                    $"Notificación de pedido #{datosNotificacion.Codigo} enviada a Admin/Vendedor"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al notificar nuevo pedido: {ex.Message}");
                await Clients.Caller.SendAsync("ErrorNotificacion",
                    new { mensaje = "Error al procesar la notificación" });
            }
        }

        // Se ejecuta cuando un usuario se desconecta
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"Usuario {userId} desconectado. ID: {Context.ConnectionId}");

            if (exception != null)
                _logger.LogError($"Excepción: {exception.Message}");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
```

---

## 2️⃣ Crear las Clases DTO

**Ubicación:** `YourProject/DTOs/NotificacionPedidoDto.cs`

```csharp
namespace YourProject.DTOs
{
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

    public class ProductoNotificacionDto
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
```

---

## 3️⃣ Configurar SignalR en Program.cs

**Ubicación:** `Program.cs`

```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// ... otros servicios ...

// AGREGAR SIGNALR
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

// AGREGAR CORS PARA SIGNALR
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy.WithOrigins(
            "https://localhost:3000",  // Vue dev server
            "http://localhost:3000",
            "http://localhost:5173"    // Vite dev server
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();

// ... configuración de middleware ...

// USAR CORS
app.UseCors("SignalRPolicy");

// MAPEAR HUB
app.MapHub<PedidosHub>("/pedidosHub");

app.Run();
```

---

## 4️⃣ Verificar Autenticación JWT

**Ubicación:** `Program.cs` (Sección de Authentication)

Asegúrate que tu configuración JWT incluya:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "YOUR_AUTHORITY";
        options.Audience = "YOUR_AUDIENCE";
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // SignalR envía el token en query string
                var accessToken = context.Request.Query["access_token"];

                // Si viene en headers
                if (string.IsNullOrEmpty(accessToken))
                    accessToken = context.Request.Headers["Authorization"]
                        .ToString()
                        .Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(accessToken))
                    context.Token = accessToken;

                return Task.CompletedTask;
            }
        };
    });
```

---

## 5️⃣ Verificar Controlador de Pedidos (Opcional)

Si quieres notificar desde el backend cuando se crea un pedido:

```csharp
[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IHubContext<PedidosHub> _hubContext;

    public PedidoController(IHubContext<PedidosHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("GenerarPedido")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> GenerarPedido([FromBody] GenerarPedidoDto dto)
    {
        try
        {
            // Crear pedido en BD
            var pedido = await CrearPedidoEnBD(dto);

            // Notificar a admin/vendedores
            if (pedido != null)
            {
                var notificacion = new NotificacionPedidoDto
                {
                    PedidoId = pedido.Id.ToString(),
                    Codigo = pedido.Codigo,
                    Total = pedido.Total,
                    Cliente = $"{pedido.Cliente.Nombre} {pedido.Cliente.Apellido}",
                    CantidadProductos = pedido.DetallesPedido.Count,
                    Estado = "Pendiente",
                    Fecha = DateTime.UtcNow,
                    Productos = pedido.DetallesPedido.Select(p => new ProductoNotificacionDto
                    {
                        Nombre = p.Producto.Nombre,
                        Cantidad = p.Cantidad,
                        Precio = p.PrecioUnitario
                    }).ToList()
                };

                // Enviar directamente a admin/vendedores
                await _hubContext.Clients
                    .Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", notificacion);
            }

            return Ok(new { pedidoId = pedido.Id, codigo = pedido.Codigo });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensajeError = ex.Message });
        }
    }

    private async Task<PedidoModel> CrearPedidoEnBD(GenerarPedidoDto dto)
    {
        // Tu lógica de creación de pedido
        // ...
    }
}
```

---

## 6️⃣ Verificar que el Rol esté en el Token JWT

Tu token JWT debe incluir el rol. Cuando generas el token, asegúrate:

```csharp
// Donde generas el JWT
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
    new Claim(ClaimTypes.Email, usuario.Email),
    new Claim(ClaimTypes.Name, usuario.Nombre),
    new Claim(ClaimTypes.Role, usuario.Rol), // ← IMPORTANTE: Rol del usuario
};

var token = new JwtSecurityToken(
    issuer: _issuer,
    audience: _audience,
    claims: claims,
    expires: DateTime.UtcNow.AddHours(24),
    signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
);

return new JwtSecurityTokenHandler().WriteToken(token);
```

El rol debe ser uno de:
- `"Admin"`
- `"Administrador"`
- `"Vendedor"`
- `"Cliente"` (puede enviar pero no recibir)

---

## ✅ Checklist de Implementación

### Archivos
- [ ] `PedidosHub.cs` creado y completo
- [ ] `NotificacionPedidoDto.cs` y `ProductoNotificacionDto.cs` creados
- [ ] `ProductoNotificacionDto.cs` en carpeta DTOs

### Program.cs
- [ ] `AddSignalR()` agregado a servicios
- [ ] `AddCors()` configurado para SignalR
- [ ] `UseCors()` llamado en app
- [ ] `MapHub<PedidosHub>("/pedidosHub")` configurado
- [ ] JWT con soporte para OnMessageReceived (para query string)

### Autenticación
- [ ] JWT se genera con el Claim `ClaimTypes.Role`
- [ ] El Claim tiene el valor correcto: "Administrador", "Vendedor", etc.

### Testing
- [ ] Backend compila sin errores
- [ ] Visitaste página diagnóstico: `/admin/DiagnosticoNotificaciones`
- [ ] Estado de conexión: `✅ Conectado`
- [ ] Rol es Admin/Vendedor (no Cliente)
- [ ] Enviaste notificación de prueba
- [ ] Aparece en "Notificaciones Recibidas"
- [ ] Confirmaste un pedido real
- [ ] Recibiste la notificación

---

## 🚀 Después de Implementar

1. **Compila el backend**
   ```bash
   dotnet build
   ```

2. **Ejecuta el backend**
   ```bash
   dotnet run
   ```

3. **Abre en navegador la página de diagnóstico**
   ```
   https://localhost:3000/admin/DiagnosticoNotificaciones
   ```

4. **Verifica estado**
   - Debería decir: `✅ Conectado`

5. **Prueba envío**
   - Click en "Enviar Notificación de Prueba"
   - Debería aparecer en la lista

6. **Prueba con pedido real**
   - Desde otra pestaña como Cliente
   - Confirma un pedido
   - Debería recibir la notificación

---

## 📞 Debugging

Si algo no funciona:

1. **Revisa logs del backend**
   ```
   Usuario {userId} con rol {role} conectado
   Usuario agregado al grupo: Administrador
   Notificación de nuevo pedido recibida
   Notificación de pedido #12345 enviada a Admin/Vendedor
   ```

2. **Revisa F12 Console en navegador**
   ```
   ✅ Conectado exitosamente a SignalR
   📢 Evento de nuevo pedido confirmado recibido
   📤 Intentando enviar notificación al hub
   🔄 Invocando método NotificarNuevoPedido en el hub
   ✅ Notificación de nuevo pedido enviada al hub correctamente
   📦 Nuevo pedido recibido
   ```

3. **Verifica que no hay errores**
   - Busca mensajes con ❌
   - Lee el error completo
   - Compara con problemas comunes en `PROBLEMAS_COMUNES.md`

---

**Versión:** 1.0
**Última actualización:** Enero 23, 2026
**Estado:** Listo para implementar
