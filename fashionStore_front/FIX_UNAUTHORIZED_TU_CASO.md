# 🔐 FIX: "user is unauthorized" - Tu Caso Específico

## ❌ Lo que FALTA en tu Program.cs

Tu configuración de SignalR está **casi correcta**, pero le faltan 2 cosas:

1. **No está validando tokens JWT en SignalR** (no hay `OnMessageReceived`)
2. **No tienes `.RequireAuthorization()` en MapHub**

---

## ✅ SOLUCIÓN: Cambios en tu Program.cs

### Cambio #1: Agregar validación JWT con OnMessageReceived

**Busca donde configuras JWT** (en `AddRegistration()` o similar) y agrega esto:

En tu `AddAuthentication` (probablemente está en `IoCRegister.AddLogsRegistration()` o `AddRegistration()`), necesita tener:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // ... tu configuración actual ...

        // ⚠️ AGREGAR ESTO:
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // SignalR envía token en query string, no en header
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

### Cambio #2: Agregar .RequireAuthorization() en MapHub

**Busca esta línea:**

```csharp
app.MapHub<PedidosHub>("/pedidosHub");
```

**Cámbiala a:**

```csharp
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();
```

---

## 📄 Tu Program.cs Modificado (Versión Completa)

**REEMPLAZA TODO TU PROGRAM.CS CON ESTO:**

```csharp
using API.Application.IoC;
using API.Data.ClasesAuxiliares.Correo;
using API.Domain.Services.NotificacionTiempoReal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Habilitar Kestrel y leer endpoints de appsettings.json
builder.WebHost
    .UseKestrel()
    .UseConfiguration(builder.Configuration);

// Add services
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddRegistration(configuration);
IoCRegister.AddLogsRegistration(builder);

// ============ SIGNALR CONFIGURATION ============
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

// ============ CORS CONFIGURATION ============
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:8080",
                "http://localhost:9000",
                "http://localhost:6004",
                "http://localhost:3000",
                "http://localhost:5173",
                "https://localhost:8080",
                "https://localhost:6005",
                "https://localhost:9000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // ✅ IMPORTANTE PARA SIGNALR
    });
});

var app = builder.Build();

// ============ MIDDLEWARE ORDER (IMPORTANTE!) ============
app.UseCors("SignalRPolicy"); // ANTES que UseAuthorization

app.UseStaticFiles();

// ============ MAPEAR HUB CON AUTORIZACION ============
app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization(); // ✅ AGREGADO

app.Use(async (context, next) =>
{
    await next();

    // Si el resultado es 404 y no es una API ni un archivo estático
    if (context.Response.StatusCode == 404 &&
        !context.Request.Path.Value.StartsWith("/api") &&
        !System.IO.Path.HasExtension(context.Request.Path.Value))
    {
        context.Response.StatusCode = 200;
        await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
    }
});

// Rutas de tu API
IoCRegister.AddRegistration(app, app.Environment);

// SMTP
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));

app.Run();
```

---

## 🔍 Dónde Está el Problema Real

El problema está en **cómo se configura JWT**. Probablemente está en:

- `IoCRegister.AddLogsRegistration(builder)` o
- `builder.Services.AddRegistration(configuration)` o
- `IoCRegister.AddRegistration(app, app.Environment)`

**Necesito que busques dónde está configurado `AddAuthentication` con `JwtBearerDefaults.AuthenticationScheme`**

Por favor comparte el archivo que contiene esa configuración.

---

## ⚠️ Si No Encuentras la Configuración JWT

Si no encuentras dónde está, aquí hay dos opciones:

### Opción A: Agregar JWT directamente en Program.cs (RECOMENDADO)

**Después de `builder.Services.AddRegistration(configuration);`, agrega:**

```csharp
// ============ JWT AUTHENTICATION ============
var jwtSettings = configuration.GetSection("Jwt");
var secretKey = jwtSettings["Secret"] ?? jwtSettings["SecretKey"] ?? "";

if (!string.IsNullOrEmpty(secretKey))
{
    var key = System.Text.Encoding.ASCII.GetBytes(secretKey);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
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
}
```

### Opción B: Agregar en el método de AddRegistration (SI SABES DÓNDE ESTÁ)

Abre el archivo donde está `AddRegistration` y busca `AddAuthentication`. Dentro de `.AddJwtBearer(options => { ... })`, agrega:

```csharp
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
```

---

## ✅ Checklist

- [ ] Cambié `app.MapHub<PedidosHub>("/pedidosHub");` a `app.MapHub<PedidosHub>("/pedidosHub").RequireAuthorization();`
- [ ] Encontré dónde está configurado JWT (AddAuthentication)
- [ ] Agregué `OnMessageReceived` en las opciones de JwtBearer
- [ ] Compilé: `dotnet build`
- [ ] Ejecuté: `dotnet run`
- [ ] Abrí: `https://localhost:3000/admin/DiagnosticoNotificaciones`
- [ ] Envié una notificación de prueba
- [ ] ✅ Funciona!

---

## 🧪 Verificación Rápida

Después de los cambios:

```bash
# Terminal en tu carpeta de backend
dotnet build
dotnet run
```

Deberías ver en los logs algo como:

```
info: API.Hubs.PedidosHub[0]
      🔗 Usuario {id} conectado. Rol: Administrador. ConnectionId: ...
info: API.Hubs.PedidosHub[0]
      ✅ Usuario agregado al grupo: Administrador
```

**Si ves eso = ✅ Authentication funciona correctamente**

---

## 💡 Por qué Pasa Esto

1. SignalR envía el token en la **query string**: `/pedidosHub?access_token=...`
2. JWT por defecto busca el token en el **header Authorization**
3. Por eso necesitamos `OnMessageReceived` para decirle a JWT: "Mira en query string"
4. Sin eso, JWT nunca valida el token, y SignalR rechaza la conexión como "unauthorized"

---

## 📞 Siguiente Paso

Por favor comparte:
1. **El contenido del archivo donde está `AddAuthentication`** (probablemente en una clase de IoC)
2. **El nombre exacto de esa clase/archivo**

Con eso ajustaré exactamente dónde agregar `OnMessageReceived`.

---
