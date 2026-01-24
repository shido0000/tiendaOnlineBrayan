# 📋 COPIAR Y PEGAR: Código Listo para Backend

Todo el código que necesitas está aquí. Solo copia, pega y listo.

---

## 1️⃣ Archivo: `Hubs/PedidosHub.cs`

**Crear nuevo archivo en tu proyecto en la ruta: `Hubs/PedidosHub.cs`**

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

        [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
        public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
        {
            try
            {
                _logger.LogInformation($"📦 Notificación recibida: Pedido #{datosNotificacion.Codigo}");
                _logger.LogInformation($"   Cliente: {datosNotificacion.Cliente}");
                _logger.LogInformation($"   Total: {datosNotificacion.Total}");

                await Clients.Groups("Admin", "Administrador", "Vendedor")
                    .SendAsync("PedidoGenerado", datosNotificacion);

                _logger.LogInformation($"✅ Notificación enviada a Admin/Vendedor");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userRole = Context.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var userId = Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

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
```

---

## 2️⃣ Archivo: `DTOs/NotificacionPedidoDto.cs`

**Crear nuevo archivo en tu proyecto en la ruta: `DTOs/NotificacionPedidoDto.cs`**

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

---

## 3️⃣ Editar: `Program.cs`

**En tu archivo `Program.cs`, ANTES de `var app = builder.Build();`**

Busca la línea donde tienes tus servicios (algo como):

```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// Aquí van los servicios...
```

Y AGREGA ESTO (cópialo tal cual):

```csharp
// ============ AGREGAR SIGNALR ============
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

// ============ AGREGAR CORS ============
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

**EJEMPLO DE CÓMO DEBE VERSE:**

```csharp
var builder = WebApplicationBuilder.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddScoped<IPedidoService, PedidoService>();

// ============ AGREGAR SIGNALR ============
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

// ============ AGREGAR CORS ============
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

var app = builder.Build();
```

---

## 4️⃣ Editar: `Program.cs` - Parte 2

**Después de `var app = builder.Build();`**

Busca donde usas middleware y agregar ANTES de `app.Run()`:

```csharp
// ============ USAR CORS ============
app.UseCors("SignalRPolicy");

// ============ MAPEAR HUB ============
app.MapHub<PedidosHub>("/pedidosHub");
```

**EJEMPLO:**

```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

// ============ USAR CORS ============
app.UseCors("SignalRPolicy");

// ============ MAPEAR HUB ============
app.MapHub<PedidosHub>("/pedidosHub");

app.MapControllers();

app.Run();
```

---

## ✅ Checklist Final

- [ ] Creé archivo `Hubs/PedidosHub.cs` con el código del paso 1
- [ ] Creé archivo `DTOs/NotificacionPedidoDto.cs` con el código del paso 2
- [ ] Edité `Program.cs` y agregué `AddSignalR()` y `AddCors()` (paso 3)
- [ ] Edité `Program.cs` y agregué `UseCors()` y `MapHub()` (paso 4)
- [ ] Cambié `TuNombreDelProyecto` por el nombre REAL de tu proyecto

---

## 🧪 Verificar que Funcione

### 1. Compila
```bash
cd TuProyecto
dotnet build
```

Si hay errores, probablemente olvidaste cambiar `TuNombreDelProyecto` por el nombre real.

### 2. Ejecuta
```bash
dotnet run
```

Deberías ver en los logs:
```
info: TuNombreDelProyecto.Hubs.PedidosHub[0]
      🔗 Usuario {id} conectado. Rol: Administrador. ConnectionId: ...
info: TuNombreDelProyecto.Hubs.PedidosHub[0]
      ✅ Usuario agregado al grupo: Administrador
```

### 3. Abre el navegador
```
https://localhost:3000/admin/DiagnosticoNotificaciones
```

Debería decir:
```
Estado: ✅ Conectado
Rol: Administrador
```

### 4. Envía notificación de prueba
Haz click en "Enviar Notificación de Prueba"

Debería aparecer en "Notificaciones Recibidas"

---

## 🆘 Si da Error de Namespace

Si ves error: `The type or namespace name 'NotificacionPedidoDto' could not be found`

**Solución:** Agrega `using` al inicio de `PedidosHub.cs`:

```csharp
using TuNombreDelProyecto.DTOs;
```

---

## 📌 Cambios Necesarios

**REEMPLAZAR:**
- `TuNombreDelProyecto` → Nombre real de tu proyecto

**EJEMPLO:**
- Si tu proyecto se llama `TiendaOnlineBrayan.API`
- Cambia: `namespace TuNombreDelProyecto.Hubs`
- Por: `namespace TiendaOnlineBrayan.API.Hubs`

**Y en PedidosHub.cs:**
- Cambia: `using TuNombreDelProyecto.DTOs`
- Por: `using TiendaOnlineBrayan.API.DTOs`

---

## ✨ ¡Listo!

Si todo está bien, verás:
- ✅ Conexión establecida en diagnóstico
- ✅ Rol correcto mostrado
- ✅ Notificaciones recibidas

¡Entonces el sistema está funcionando! 🎉

---

**Tiempo total:** ~10 minutos
**Dificultad:** ⭐☆☆☆☆ Muy fácil (copy-paste)
**Última actualización:** Enero 23, 2026
