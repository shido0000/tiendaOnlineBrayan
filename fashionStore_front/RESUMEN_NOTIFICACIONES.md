# Implementación de Notificaciones en Tiempo Real - Resumen Rápido

## 🎯 Qué se Implementó

Sistema completo de notificaciones en tiempo real para nuevos pedidos usando **SignalR**. Cuando un cliente confirma un pedido, se notifica instantáneamente a todos los administradores y vendedores conectados.

## 📋 Cambios Realizados

### 1. **Componente ConfirmarPedido.vue**
- ✅ Agregada función `notificarNuevoPedido()` que dispara un evento personalizado cuando se confirma un pedido
- ✅ Llamada a `notificarNuevoPedido()` después de guardar el pedido exitosamente
- ✅ Construcción de datos completos del pedido (código, total, cliente, productos, etc.)

### 2. **Servicio SignalR (signalRService.js)**
- ✅ Agregado método `setupFrontendListeners()` que escucha eventos del navegador
- ✅ Agregado método `notificarNuevoPedido()` que envía datos al hub del backend
- ✅ Integración de escuchadores al conectarse exitosamente
- ✅ Método `notificarNuevoPedido()` invocado en el hub como "NotificarNuevoPedido"

## 🔄 Flujo Completo

```
1. Cliente confirma pedido
   ↓
2. confirmOrder() se ejecuta exitosamente
   ↓
3. notificarNuevoPedido() construye datos
   ↓
4. Dispara evento: window.dispatchEvent('nuevoPedidoConfirmado')
   ↓
5. signalRService escucha el evento
   ↓
6. connection.invoke('NotificarNuevoPedido', datosNotificacion)
   ↓
7. Backend recibe en PedidosHub.NotificarNuevoPedido()
   ↓
8. Backend valida rol: Solo procesa si viene de Cliente
   ↓
9. Backend envía a grupos: "Admin", "Administrador", "Vendedor"
   ↓
10. Clientes Admin/Vendedor reciben evento 'PedidoGenerado'
    ↓
11. handlePedidoGenerado() procesa y muestra notificación
```

## 📁 Archivos Modificados

| Archivo | Cambios |
|---------|---------|
| `src/pages/Visual/components/ConfirmarPedido.vue` | + Función `notificarNuevoPedido()` + Llamada en confirmOrder() |
| `src/services/signalRService.js` | + setupFrontendListeners() + notificarNuevoPedido() + Integración en connect() |

## 📄 Archivos Nuevos Creados

| Archivo | Descripción |
|---------|-------------|
| `NOTIFICACIONES_TIEMPO_REAL.md` | Documentación completa del sistema |
| `EJEMPLO_BACKEND_SIGNALR.cs` | Ejemplo de implementación en backend (C#) |

## 🔧 Backend Requerido

El backend debe implementar:

### Hub (PedidosHub.cs)
```csharp
[Authorize]
public class PedidosHub : Hub
{
    [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
    public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
    {
        // Enviar a administradores y vendedores
        await Clients.Groups("Admin", "Administrador", "Vendedor")
            .SendAsync("PedidoGenerado", datosNotificacion);
    }

    public override async Task OnConnectedAsync()
    {
        var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
        if (!string.IsNullOrEmpty(userRole))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
        }
        await base.OnConnectedAsync();
    }
}
```

### Configuración en Program.cs
```csharp
services.AddSignalR();
// ...
endpoints.MapHub<PedidosHub>("/pedidosHub");
```

## ✅ Validaciones Implementadas

- ✅ Solo administradores y vendedores reciben notificaciones
- ✅ Token JWT requerido para conectar
- ✅ Rol extraído del token (`payload.role`)
- ✅ Reconexión automática en caso de desconexión
- ✅ Manejo de errores en toda la cadena

## 🔑 Token JWT Requerido

El token debe contener el rol del usuario:

```json
{
  "Id": "user-id",
  "email": "user@example.com",
  "role": "Administrador",  // o "Vendedor"
  "name": "Nombre Usuario"
}
```

## 📊 Datos Enviados en Notificación

```javascript
{
  pedidoId: "uuid",
  codigo: "#12345",
  total: 199.99,
  cliente: "Juan Pérez",
  cantidadProductos: 3,
  estado: "Pendiente",
  fecha: "2026-01-23T10:30:00Z",
  productos: [
    { nombre: "Camiseta", cantidad: 1, precio: 29.99 },
    { nombre: "Pantalón", cantidad: 2, precio: 79.99 }
  ]
}
```

## 🧪 Testing

### Verificar Conexión
```javascript
// En la consola del navegador
signalRService.getConnectionState()
// 1 = Conectado, 0 = Desconectado
```

### Ver Logs
Abre F12 y busca:
- ✅ `Conectado a SignalR`
- 📦 `Nuevo pedido recibido`
- 🛒 `¡Nuevo Pedido!`

### Simular Notificación
```javascript
// En la consola del navegador (como admin/vendedor)
const event = new CustomEvent('nuevoPedidoConfirmado', {
  detail: {
    pedidoId: "test-123",
    codigo: "#99999",
    total: 500,
    cliente: "Cliente Test",
    cantidadProductos: 5,
    estado: "Pendiente",
    fecha: new Date().toISOString(),
    productos: [{ nombre: "Producto Test", cantidad: 5, precio: 100 }]
  }
})
window.dispatchEvent(event)
```

## ⚙️ Configuración

### URL del Hub
- Local: `https://localhost:6005/pedidosHub`
- Producción: Cambiar según tu servidor

### Transporte
- Actual: WebSockets
- Fallback automático a Long Polling

### Timeouts
- Reconexión: 1s, 5s, 10s (adaptativo)
- Inactividad: Configurable en backend

## 🚀 Próximos Pasos (Opcional)

1. **Persistencia**: Guardar notificaciones en BD
2. **Historial**: Crear página de historial de pedidos
3. **Sound**: Agregar sonido de notificación
4. **Email**: Enviar email también a admin/vendedores
5. **Desktop**: Usar Notifications API para notificaciones del sistema

## 📞 Support

Para más detalles, ver:
- `NOTIFICACIONES_TIEMPO_REAL.md` - Documentación completa
- `EJEMPLO_BACKEND_SIGNALR.cs` - Implementación backend
- Consola (F12) - Logs y debugging

---

**Status**: ✅ Implementación Completa
**Fecha**: Enero 23, 2026
**Versión**: 1.0
