# 🔐 Error: "user is unauthorized" - SOLUCIÓN

## 🔴 El Problema

```
Error: Failed to invoke 'NotificarNuevoPedido' because user is unauthorized
```

## ✅ Lo que SÍ está funcionando

- ✅ Frontend se conecta al hub
- ✅ Token se envía correctamente en `accessTokenFactory: () => token`
- ✅ El método `NotificarNuevoPedido` existe en el backend
- ✅ El hub responde (no hay error de conexión)

## ❌ Lo que NO está funcionando

- ❌ El backend RECHAZA al usuario como no autenticado
- ❌ Problema: Validación de token en PedidosHub.cs

---

## 🔍 Causa Raíz

El error `user is unauthorized` significa que:

1. El hub tiene `[Authorize]` attribute
2. El token se envía correctamente PERO
3. **El backend NO puede validar el token**

Esto ocurre porque:
- El middleware de JWT no reconoce el token
- O el token no se está pasando correctamente a SignalR
- O la configuración de CORS/autenticación en Program.cs no es correcta

---

## ✅ SOLUCIÓN: Verificar Program.cs

### Problema Típico #1: Falta MapSignalR en Program.cs

Tu `Program.cs` debe tener ESTO:

```csharp
// 1️⃣ En builder.Services
builder.Services.AddSignalR();
builder.Services.AddAuthentication(...); // JWT ya debe estar configurado
builder.Services.AddCors(...);

// 2️⃣ En app.Use - ESTE ORDEN ES IMPORTANTE:
app.UseCors("AllowAll"); // ⚠️ ANTES que UseAuthorization
app.UseAuthentication(); // Token validation
app.UseAuthorization();  // Permission check

// 3️⃣ MapHub CON AuthorizePolicy
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
```

### Problema Típico #2: MapHub sin RequireAuthorization

Si tu código tiene ESTO:

```csharp
❌ MALO: Sin autorización
app.MapHub<PedidosHub>("/pedidosHub");
```

Debes cambiar a ESTO:

```csharp
✅ BUENO: Con autorización requerida
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
```

---

## 🔧 FIXES (Elige uno según tu caso)

### FIX #1: Si Program.cs usa MapHub sin RequireAuthorization

**Cambio en Program.cs:**

```csharp
// ❌ CAMBIAR ESTO:
app.MapHub<PedidosHub>("/pedidosHub");

// ✅ POR ESTO:
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
```

Luego ejecuta:
```bash
dotnet build
dotnet run
```

---

### FIX #2: Si falta la configuración de CORS

**En Program.cs, agregar ANTES de MapHub:**

```csharp
// 1️⃣ En builder.Services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("https://localhost:3000", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // ⚠️ IMPORTANTE para SignalR
    });
});

// 2️⃣ En app.Use - ANTES de UseAuthorization
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// 3️⃣ MapHub
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
```

---

### FIX #3: Si falta configuración JWT completa

**En Program.cs, agregar:**

```csharp
// En builder.Services
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };

    // ⚠️ IMPORTANTE: Para SignalR que envía token en query string
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
```

---

### FIX #4: Si PedidosHub.cs está mal configurado

**Tu PedidosHub.cs debe tener ESTO:**

```csharp
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

[Authorize] // ⚠️ IMPORTANTE
public class PedidosHub : Hub
{
    public async Task NotificarNuevoPedido(NotificacionPedidoDto datos)
    {
        // El usuario DEBE estar autenticado aquí
        var userId = Context.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userRole = Context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        Console.log($"✅ Usuario {userId} con rol {userRole} notificó pedido");

        // Enviar a grupos basados en rol
        await Clients.Group("Administrador").SendAsync("PedidoGenerado", datos);
        await Clients.Group("Vendedor").SendAsync("PedidoGenerado", datos);
    }

    public override async Task OnConnectedAsync()
    {
        var role = Context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (!string.IsNullOrEmpty(role))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, role);
            Console.WriteLine($"✅ Usuario agregado a grupo: {role}");
        }

        await base.OnConnectedAsync();
    }
}
```

---

## 🧪 Verificación

Después de aplicar los fixes:

### 1️⃣ Compila el backend
```bash
cd tuProyectoBackend
dotnet build
```

### 2️⃣ Ejecuta el backend
```bash
dotnet run
```

### 3️⃣ Abre el diagnóstico en frontend
```
https://localhost:3000/admin/DiagnosticoNotificaciones
```

### 4️⃣ Espera a ver:
- ✅ Estado: `1 (Conectado)`
- ✅ Usuario: Tu usuario actual
- ✅ Rol: `Administrador` o `Vendedor`

### 5️⃣ Haz clic en "Enviar Notificación de Prueba"

### 6️⃣ Deberías ver en consola del backend:
```
✅ Usuario [ID] con rol [Rol] notificó pedido
```

---

## 📋 Checklist de Solución

- [ ] Verificar que `[Authorize]` está en PedidosHub.cs
- [ ] Verificar que `RequireAuthorization()` está en MapHub
- [ ] Verificar que `AddCors()` está configurado
- [ ] Verificar que `UseCors()` está ANTES de UseAuthorization
- [ ] Verificar que JWT está configurado en AddAuthentication
- [ ] Verificar que OnMessageReceived captura token de query string
- [ ] Compilar backend: `dotnet build`
- [ ] Ejecutar backend: `dotnet run`
- [ ] Abrir `/admin/DiagnosticoNotificaciones`
- [ ] Enviar notificación de prueba
- [ ] Ver en consola del backend el mensaje de éxito

---

## 🆘 Si Nada de Esto Funciona

### Opción 1: Desactivar temporalmente autorización para debuggear

**En PedidosHub.cs:**
```csharp
// ❌ Comentar temporalmente
// [Authorize]
public class PedidosHub : Hub
{
    public async Task NotificarNuevoPedido(NotificacionPedidoDto datos)
    {
        // ...
    }
}
```

Si esto funciona, entonces el problema es 100% de autenticación en Program.cs.

### Opción 2: Agregar logging detallado

**En Program.cs:**
```csharp
builder.Services.AddLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Debug);
    logging.AddConsole();
});
```

Verás en consola qué token se recibe y por qué no valida.

### Opción 3: Verificar token JWT

En `/admin/DiagnosticoNotificaciones`:
- Mira el token en la consola
- Cópialo
- Pega en: https://jwt.io/
- Verifica que tenga claims correctos: `NameIdentifier`, `Role`, etc.

---

## 📞 Información para Debuggear

Por favor comparte:

1. **Contenido de tu Program.cs** (líneas 1-50)
2. **Contenido completo de PedidosHub.cs**
3. **¿Qué está en appsettings.json bajo "Jwt"?**
4. **Mensaje exacto que ves en consola del backend**

Con eso podré identificar exactamente qué falta.

---

## 🎯 Resumen

**El error `user is unauthorized` significa:**
- ✅ Frontend envía token correctamente
- ✅ Backend recibe el token
- ❌ Backend NO puede validar el token

**La solución es validar el token:**
1. Agregar `OnMessageReceived` en JWT options
2. Asegurar que CORS está configurado
3. Asegurar que `RequireAuthorization()` está en MapHub
4. Compilar y ejecutar nuevamente

**Tiempo para solucionar: 5 minutos**

---
