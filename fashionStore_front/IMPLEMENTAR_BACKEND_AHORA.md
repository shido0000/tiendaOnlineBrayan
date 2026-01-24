# 🔴 SOLUCIÓN: Implementar PedidosHub en Backend

## El Problema
Frontend envía: `connection.invoke('NotificarNuevoPedido', datos)`
Backend responde: `HubException: Method does not exist`

**Esto significa: El hub NO tiene el método implementado**

---

## ✅ SOLUCIÓN PASO A PASO

### Paso 1: Crear el Hub

**Crea un archivo nuevo:** `Hubs/PedidosHub.cs` en tu proyecto backend

```csharp
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace TuNombreDelProyecto.Hubs
{
    [Authorize]
    public class PedidosHub : Hub
    {
        private readonly ILogger<PedidosHub> _logger;

        public PedidosHub(ILogger<PedidosHub> logger)
        {
            _logger = logger;
        }

        // Este método recibe la notificación del frontend
        [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
        public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                _logger.LogInformation($"Notificación recibida: Pedido #{datosNotificacion.Codigo}");

                // Enviar a todos los administradores y vendedores conectados
                await Clients.Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", datosNotificacion);

                _logger.LogInformation($"Notificación enviada a Admin/Vendedor");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
        }

        // Se ejecuta cuando alguien se conecta
        public override async Task OnConnectedAsync()
        {
            var userRole = Context.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var userId = Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            _logger.LogInformation($"Usuario {userId} (Rol: {userRole}) conectado. ID: {Context.ConnectionId}");

            if (!string.IsNullOrEmpty(userRole))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
                _logger.LogInformation($"Usuario agregado al grupo: {userRole}");
            }

            await base.OnConnectedAsync();
        }

        // Se ejecuta cuando alguien se desconecta
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation($"Usuario desconectado. ID: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
```

### Paso 2: Crear los DTOs

**Crea un archivo nuevo:** `DTOs/NotificacionPedidoDto.cs`

```csharp
namespace TuNombreDelProyecto.DTOs
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

### Paso 3: Configurar en Program.cs

**En tu archivo `Program.cs`, ANTES de `app.Build()`, agrega:**

```csharp
// Busca la sección donde agregas servicios (builder.Services...)

// ✅ AGREGAR ESTO
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy.WithOrigins(
            "https://localhost:3000",
            "https://localhost:5173",
            "http://localhost:3000",
            "http://localhost:5173"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
```

**En la sección de middleware (después de `var app = builder.Build()`), agrega:**

```csharp
// ✅ USAR CORS
app.UseCors("SignalRPolicy");

// ✅ MAPEAR EL HUB
app.MapHub<PedidosHub>("/pedidosHub");
```

**Debe quedar algo así:**

```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// Agregamos servicios
builder.Services.AddControllers();
builder.Services.AddSignalR()  // ← AQUÍ
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddCors(options =>  // ← Y AQUÍ
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:3000", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("SignalRPolicy");  // ← AQUÍ
app.MapHub<PedidosHub>("/pedidosHub");  // ← Y AQUÍ
app.MapControllers();

app.Run();
```

### Paso 4: Asegurar que JWT incluye el Rol

En el lugar donde generas el JWT, asegúrate que incluya el rol:

```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
    new Claim(ClaimTypes.Email, usuario.Email),
    new Claim("nombre", usuario.Nombre),
    new Claim(ClaimTypes.Role, usuario.Rol)  // ← IMPORTANTE
};

var token = new JwtSecurityToken(
    issuer: configuration["Jwt:Issuer"],
    audience: configuration["Jwt:Audience"],
    claims: claims,
    expires: DateTime.UtcNow.AddHours(24),
    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
);
```

### Paso 5: Verificar JWT en SignalR

En tu configuración de JWT (en Program.cs), asegúrate que SignalR pueda leer el token:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["Jwt:Authority"];
        options.Audience = configuration["Jwt:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
        };

        // ✅ IMPORTANTE PARA SIGNALR
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // SignalR envía token en query string en WebSocket
                var accessToken = context.Request.Query["access_token"];

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

## ✅ Checklist de Implementación

- [ ] Creaste `Hubs/PedidosHub.cs`
- [ ] Creaste `DTOs/NotificacionPedidoDto.cs`
- [ ] Agregaste `builder.Services.AddSignalR()` en Program.cs
- [ ] Agregaste `builder.Services.AddCors(...)` en Program.cs
- [ ] Agregaste `app.UseCors("SignalRPolicy")` en Program.cs
- [ ] Agregaste `app.MapHub<PedidosHub>("/pedidosHub")` en Program.cs
- [ ] El JWT incluye el claim `ClaimTypes.Role`
- [ ] JWT tiene `OnMessageReceived` para SignalR

---

## 🔍 Verificar que Funcione

### 1. Compila el backend
```bash
dotnet build
```

### 2. Ejecuta el backend
```bash
dotnet run
```

### 3. En la consola verás algo como:
```
info: TuNombreDelProyecto.Hubs.PedidosHub[0]
      Usuario admin-id (Rol: Administrador) conectado. ID: xyz123
info: TuNombreDelProyecto.Hubs.PedidosHub[0]
      Usuario agregado al grupo: Administrador
```

### 4. Abre en navegador
```
https://localhost:3000/admin/DiagnosticoNotificaciones
```

### 5. Debe decir:
```
Estado: ✅ Conectado
Rol: Administrador
```

### 6. Haz click en "Enviar Notificación de Prueba"

### 7. En la sección "Notificaciones Recibidas" debería aparecer

---

## 🆘 Si Sigue Sin Funcionar

### Verifica logs del backend:
```
Usuario {userId} con rol {role} conectado
Usuario agregado al grupo: Administrador
Notificación recibida: Pedido #...
Notificación enviada a Admin/Vendedor
```

### Verifica en F12 Console:
```
🔄 Invocando método NotificarNuevoPedido en el hub
✅ Notificación de nuevo pedido enviada al hub correctamente
📦 Nuevo pedido recibido
```

### Si ves error "Method does not exist":
1. Asegúrate que creaste `PedidosHub.cs` con el método exacto: `NotificarNuevoPedido`
2. Asegúrate que mapeaste el hub en Program.cs: `app.MapHub<PedidosHub>("/pedidosHub")`
3. Reinicia el backend: `dotnet run`

---

## 📌 Resumen

**Lo que hace el código:**

1. **Cliente confirma pedido** → Dispara evento `nuevoPedidoConfirmado`
2. **SignalR escucha evento** → Invoca `connection.invoke('NotificarNuevoPedido', datos)`
3. **Backend recibe en hub** → Método `NotificarNuevoPedido` se ejecuta
4. **Backend valida rol** → Solo procesa si el usuario es Client/Admin/Vendedor
5. **Backend envía a grupos** → Broadcast a todos los Administrador/Vendedor conectados
6. **Admin/Vendedor recibe** → Se ejecuta el listener `connection.on('PedidoGenerado', ...)`
7. **Muestra notificación** → `Success("🛒 ¡Nuevo Pedido!")`

---

**Última actualización:** Enero 23, 2026
**Estado:** Listo para copiar y pegar
