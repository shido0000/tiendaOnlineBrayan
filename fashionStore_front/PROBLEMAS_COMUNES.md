# 🚨 Problemas Comunes y Soluciones

## 1. "No recibe notificaciones pero la conexión está ✅ Conectada"

### Síntomas
- `Estado: ✅ Conectado`
- `Rol: Administrador`
- Pero NO recibe notificaciones cuando se confirma un pedido

### Causas Posibles (en orden de probabilidad)

#### A. Backend NO tiene el hub implementado ⭐ MÁS PROBABLE
**Síntoma:** Ves en F12 console: `Error invoking hub method NotificarNuevoPedido`

**Solución:**
1. Verifica que en tu proyecto backend exista `PedidosHub.cs`
2. Implementa el hub según `EJEMPLO_BACKEND_SIGNALR.cs`
3. Registra en `Program.cs`:
```csharp
services.AddSignalR();
endpoints.MapHub<PedidosHub>("/pedidosHub");
```
4. Reinicia el backend
5. Vuelve a intentar

#### B. El backend recibe pero no envía a los grupos
**Síntoma:** Ves en backend logs: "NotificarNuevoPedido received"

**Solución:**
1. Verifica que OnConnectedAsync agrega el usuario al grupo:
```csharp
public override async Task OnConnectedAsync()
{
    var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
    if (!string.IsNullOrEmpty(userRole))
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
    }
    await base.OnConnectedAsync();
}
```
2. Verifica que los nombres de grupos coincidan:
   - Frontend envía a: "Admin", "Administrador", "Vendedor"
   - Backend debe recibir al usuario en uno de estos grupos
3. En logs del backend deberías ver: "Usuario agregado al grupo: Administrador"

#### C. El rol en el token no coincide
**Síntoma:**
- Rol mostrado en diagnóstico es diferente al que esperas
- O solo ves "Cliente"

**Solución:**
1. Verifica que tu usuario admin tiene el rol correcto en BD
2. Genera un nuevo token iniciando sesión
3. En diagnóstico debería mostrar: `Rol: Administrador`

---

## 2. "Error: SignalR no está conectado. No se puede enviar la notificación."

### Síntomas
- Ves el error en F12 console
- El cliente que confirma el pedido ve este mensaje

### Causas Posibles

#### A. El administrador NO tiene una ventana abierta conectada
**Solución:**
1. Abre una pestaña como Administrador
2. Navega a: `/admin/DiagnosticoNotificaciones`
3. Verifica que diga: `✅ Conectado`
4. Ahora confirma el pedido desde otra pestaña como Cliente
5. Deberías recibir la notificación

#### B. La conexión se desconectó
**Solución:**
1. En la página de diagnóstico, haz click en "Reconectar"
2. Verifica que dice: `✅ Conectado`
3. Intenta de nuevo

#### C. Backend está caído
**Síntomas:**
- Diagnóstico muestra: `❌ Desconectado`
- O intenta conectar pero falla
- En F12 console: `Error connecting to https://localhost:6005/pedidosHub`

**Solución:**
1. Verifica que el backend está corriendo: `https://localhost:6005`
2. Abre en navegador: `https://localhost:6005/health` (si existe endpoint)
3. Reinicia el backend
4. Actualiza la página (F5)

---

## 3. "Recibo error: Error invoking hub method 'NotificarNuevoPedido'"

### Síntomas
En F12 Console ves:
```
❌ Error al notificar nuevo pedido
   Tipo de error: Error invoking hub method 'NotificarNuevoPedido'
   Stack: HubInvocationUnauthorizedException...
```

### Causas Posibles

#### A. El método no existe en el backend
**Solución:** Implementar en PedidosHub.cs:
```csharp
public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
{
    // ...
}
```

#### B. Falta autorización [Authorize]
**Solución:** Asegúrate que el hub tiene `[Authorize]`:
```csharp
[Authorize]
public class PedidosHub : Hub { ... }
```

#### C. Nombre del método incorrecto
**Verificar:**
- Frontend invoca: `connection.invoke('NotificarNuevoPedido', ...)`
- Backend tiene: `public async Task NotificarNuevoPedido(...)`
- Deben coincidir exactamente (mayúsculas/minúsculas)

#### D. Falta de DTO en el backend
**Solución:** Define en backend:
```csharp
public class NotificacionPedidoDto
{
    public string PedidoId { get; set; }
    public string Codigo { get; set; }
    public decimal Total { get; set; }
    public string Cliente { get; set; }
    public int CantidadProductos { get; set; }
    public string Estado { get; set; }
    public DateTime Fecha { get; set; }
    public List<ProductoNotificacionDto> Productos { get; set; }
}
```

---

## 4. "La página de diagnóstico no carga"

### Síntomas
- Error 404 o error de componente
- La ruta `/admin/DiagnosticoNotificaciones` no funciona

### Solución
1. Verifica que el archivo existe:
   ```
   src/pages/Test/DiagnosticoNotificaciones.vue
   ```
2. Verifica que está registrado en `src/router/routes.js`
3. Recarga la página (Ctrl+Shift+R hard refresh)

---

## 5. "Token expirado o no válido"

### Síntomas
En F12 console ves:
```
⚠️ No hay token. Por favor inicia sesión.
```

O en conectar SignalR:
```
❌ Error conectando a SignalR
   Error: 401 Unauthorized
```

### Solución
1. Cierra sesión en todas las pestañas
2. Vuelve a iniciar sesión
3. Verifica que el token se guarda en localStorage:
```javascript
// En F12 console
localStorage.getItem('token')
// Debería mostrar algo como: eyJhbGc...
```

---

## 6. "Recibe notificación pero con datos vacíos"

### Síntomas
- Notificación llega pero muestra: `Código: undefined`, `Total: $0`

### Causas Posibles

#### A. Los nombres de propiedades no coinciden
**Frontend envía:**
```javascript
{
  pedidoId: "...",  // minúscula
  codigo: "#12345",
  total: 199.99,
  ...
}
```

**Backend espera:**
```csharp
{
  PedidoId: "...",  // MAYÚSCULA
  Codigo: "#12345",
  Total: 199.99,
  ...
}
```

**Solución:** Hacer coincidir los nombres (preferentemente usar PascalCase)

#### B. Backend enriquece datos incorrectamente
**Solución:** Asegúrate que en backend:
```csharp
public async Task NotificarNuevoPedido(NotificacionPedidoDto datos)
{
    // Enriquecer datos si es necesario
    if (datos.Fecha == DateTime.MinValue)
        datos.Fecha = DateTime.UtcNow;

    // Luego enviar
    await Clients.Groups("Admin", "Administrador", "Vendedor")
        .SendAsync("PedidoGenerado", datos);
}
```

---

## 7. "Múltiples administradores, pero solo uno recibe notificaciones"

### Síntomas
- Admin 1 está en `/admin/DiagnosticoNotificaciones` → recibe ✅
- Admin 2 está en otra página → no recibe ❌

### Solución
1. TODOS los que quieran recibir deben estar conectados a SignalR
2. La conexión se establece en `ConfirmarPedido.vue` onMounted
3. Si está en otra página, puede no estar conectado
4. Solución: Conectar SignalR globalmente en MainLayout.vue:

```javascript
// En MainLayout.vue onMounted
onMounted(async () => {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  if (token) {
    try {
      await signalRService.connect()
      console.log('✅ SignalR conectado globalmente')
    } catch (error) {
      console.warn('No se pudo conectar a SignalR:', error)
    }
  }
})
```

---

## 8. "Algunos clientes ven notificaciones, otros no"

### Síntomas
- Cliente A confirma pedido → Admin recibe ✅
- Cliente B confirma pedido → Admin NO recibe ❌

### Causas Posibles

#### A. Cliente B tiene rol "Cliente" pero el endpoint de pedidos requiere otro rol
**Solución:** Verifica en backend que Pedido/GenerarPedido:
```csharp
[Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
public async Task<IActionResult> GenerarPedido(GenerarPedidoDto dto)
```

#### B. La invocación falla silenciosamente para algunos clientes
**Solución:** En F12 console del Cliente B, verifica:
- Que aparezca: "✅ Notificación de nuevo pedido enviada"
- Que NO aparezca: "❌ Error al notificar nuevo pedido"

---

## 9. "CORS error: Access-Control-Allow-Origin"

### Síntomas
En F12 console:
```
Access to XMLHttpRequest at 'https://localhost:6005/pedidosHub'
from origin 'https://localhost:3000' has been blocked by CORS policy
```

### Solución
En el backend, configurar CORS:
```csharp
// En Program.cs ANTES de MapHub
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

// Luego en app.UseCors()
app.UseCors("SignalRPolicy");
```

---

## 10. Checklist Rápido Si Nada Funciona

```
[  ] Backend está corriendo en https://localhost:6005
[  ] PedidosHub.cs existe y está implementado
[  ] MapHub<PedidosHub>("/pedidosHub") en Program.cs
[  ] Usuario admin tiene rol "Administrador" en BD
[  ] Token tiene el claim "role" con valor "Administrador"
[  ] Página diagnóstico muestra: ✅ Conectado
[  ] Página diagnóstico muestra: Rol = Administrador
[  ] Enviaste notificación de prueba sin errores
[  ] Notificación de prueba aparece en sección 4
[  ] Confirmaste un pedido real desde otra pestaña/navegador
[  ] Viste la notificación en admin
```

Si todos están checkeados → **TODO FUNCIONA** ✅

Si alguno falla → Enfócate en ese paso específico

---

**Última actualización:** Enero 23, 2026
**Versión:** 1.0
