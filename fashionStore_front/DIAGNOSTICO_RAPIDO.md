# 🔍 Guía Rápida: Diagnosticar Notificaciones No Recibidas

## Problema
El cliente confirma un pedido, ves el log `✅ Notificación de nuevo pedido enviada` pero el administrador/vendedor NO recibe la notificación.

## Solución Paso a Paso

### Paso 1: Abre la Página de Diagnóstico
```
https://localhost:3000/admin/DiagnosticoNotificaciones
```
(Acceso como Administrador o Vendedor)

### Paso 2: Verifica la Conexión SignalR
En la sección **"1️⃣ Estado de Conexión SignalR"**:
- Deberías ver: `✅ Conectado`
- Si ves `❌ Desconectado`: Haz click en "Reconectar"

**Si sigue sin conectar:**
- Abre F12 (Consola)
- Busca mensajes de error de SignalR
- Verificar que la URL es: `https://localhost:6005/pedidosHub`
- Asegúrate que el backend está corriendo en el puerto 6005

### Paso 3: Verifica tu Rol
En la sección **"2️⃣ Datos del Usuario (JWT Token)"**:
- Deberías ver: `Rol: Administrador` o `Rol: Vendedor`
- Con un chip verde ✅ que dice: "Puede recibir notificaciones"

**Si ves "Cliente":**
- Este usuario NO recibirá notificaciones
- Inicia sesión como un administrador/vendedor real

### Paso 4: Envía una Notificación de Prueba
En la sección **"3️⃣ Prueba de Envío de Notificación"**:
- Haz click en "Enviar Notificación de Prueba"
- Debería mostrar `✅ Notificación Enviada`

**Si ves error:**
- Lee el mensaje de error
- Abre F12 y busca: "Error al notificar nuevo pedido"
- Copia el error y verifica en backend

### Paso 5: Verifica que Recibas Notificaciones
En la sección **"4️⃣ Notificaciones Recibidas"**:
- Deberías ver la notificación de prueba que enviaste

**Si NO aparece:**
```
┌─────────────────────────────────────────────┐
│ PROBLEMA PROBABLE: Backend no implementado  │
└─────────────────────────────────────────────┘
```

## Checklist de Diagnóstico

- [ ] Página de diagnóstico carga (`/admin/DiagnosticoNotificaciones`)
- [ ] Estado de conexión: `✅ Conectado`
- [ ] Rol: `Administrador` o `Vendedor`
- [ ] Prueba de envío: `✅ Notificación Enviada`
- [ ] Sección "Notificaciones Recibidas" muestra datos

## Si Todo Está Conectado pero Sigue Sin Funcionar

### Verifica el Backend

1. **¿El PedidosHub existe?**
```csharp
public class PedidosHub : Hub
{
    [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
    public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
    {
        await Clients.Groups("Admin", "Administrador", "Vendedor")
            .SendAsync("PedidoGenerado", datosNotificacion);
    }
}
```

2. **¿El Hub está registrado?**
```csharp
// En Program.cs
services.AddSignalR();
// ...
endpoints.MapHub<PedidosHub>("/pedidosHub");
```

3. **¿Los usuarios se agregan a los grupos?**
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

## Debugging Avanzado (F12 Console)

### Ver logs de SignalR
```javascript
// Esto ya está hecho, pero puedes ver en consola:
signalRService.connection?.serverTimeoutInMilliseconds
signalRService.connection?.state // 1 = Connected
signalRService.getConnectionState()
```

### Ver si el evento se dispara
```javascript
// Abre consola y busca:
// "📢 Evento de nuevo pedido confirmado recibido"
// "📤 Intentando enviar notificación al hub"
// "🔄 Invocando método NotificarNuevoPedido en el hub"
```

### Ver si el backend responde
```javascript
// En F12, Network tab:
// Busca "websocket" o "signalr"
// Debería haber conexión abierta a: localhost:6005/pedidosHub
```

## Casos Comunes

### Caso 1: "Conexión no está en estado Connected"
```
❌ ERROR: Conexión no está en estado Connected
   Estado actual: 0 (Desconectado)
```
**Solución:**
- Backend no está corriendo
- URL incorrecta en signalRService.js
- Token expirado

### Caso 2: "No hay token"
```
⚠️ No hay token. Por favor inicia sesión.
```
**Solución:**
- Cierra sesión y vuelve a iniciar
- Asegúrate que el token se guarda en localStorage

### Caso 3: "Rol es Cliente"
```
⚠️ No recibirá notificaciones (rol: Cliente)
```
**Solución:**
- Este usuario NO es admin/vendedor
- Inicia sesión como administrador
- O crea un usuario admin en la BD

## URLs Útiles

| Página | URL | Propósito |
|--------|-----|-----------|
| Diagnóstico | `/admin/DiagnosticoNotificaciones` | Ver estado |
| Test Pedido | `/carrito` | Crear pedido para probar |
| TestSignalR | `/admin/TestSignalR` | Test básico |

## Flujo de Debugging

```
1. ¿Ves "✅ Conectado"?
   ├─ NO → Problema de conexión (Backend no está corriendo)
   └─ SÍ → Continúa

2. ¿Tu rol es Admin/Vendedor?
   ├─ NO → Inicia sesión como admin
   └─ SÍ → Continúa

3. ¿Se envía la notificación de prueba sin errores?
   ├─ NO → Error en invocar el método (revisar backend)
   └─ SÍ → Continúa

4. ¿Recibes la notificación de prueba?
   ├─ NO → Backend no tiene el hub implementado
   └─ SÍ → TODO FUNCIONA ✅

5. ¿Ahora confirma un pedido real
   ├─ Recibe notificación → ✅ ÉXITO
   └─ No recibe → Revisa logs en backend
```

## Archivos Importantes

- Frontend: [src/services/signalRService.js](src/services/signalRService.js)
- Frontend: [src/pages/Visual/components/ConfirmarPedido.vue](src/pages/Visual/components/ConfirmarPedido.vue)
- Backend: `PedidosHub.cs` (debe estar en tu proyecto ASP.NET)
- Backend: `Program.cs` (debe tener MapHub configuration)

## Soporte Rápido

Si algo falla, verifica:
1. Logs en F12 Console (Ctrl+Shift+K)
2. Busca mensajes con: 🔍 📢 📤 🔄 ✅ ❌
3. Lee el mensaje completo
4. Sigue las instrucciones en pantalla

---

**Versión:** 1.0
**Última actualización:** Enero 23, 2026
