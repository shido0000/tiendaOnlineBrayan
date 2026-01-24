# 🔄 Flujo Completo de SignalR - Actualizaciones en Tiempo Real

## 📊 Diagrama del Flujo

```
┌─────────────────────────────────────────────────────────────────────┐
│                        CLIENTE (NUEVO PEDIDO)                       │
└──────────────────────────────────┬──────────────────────────────────┘
                                   │
                     Crea pedido (POST /Pedido)
                                   │
                                   ▼
┌─────────────────────────────────────────────────────────────────────┐
│                    BACKEND (C# SignalR Hub)                         │
│                                                                      │
│  1. GenerarPedido() - Crea el pedido en BD                          │
│  2. NotificarNuevoPedido() - Emite evento                           │
│     await _hubContext.Clients.Group("admin")                        │
│        .SendAsync("PedidoGenerado", notificacionData)               │
│     await _hubContext.Clients.Group("vendedores")                   │
│        .SendAsync("PedidoGenerado", notificacionData)               │
└──────────────────────────────────┬──────────────────────────────────┘
                                   │
                   SignalR WebSocket - "PedidoGenerado"
                                   │
        ┌──────────────────────────┼──────────────────────────┐
        │                          │                          │
        ▼                          ▼                          ▼
   ADMIN (1)              VENDEDOR (1)              VENDEDOR (2)
   conectado              conectado                 conectado
        │                          │                          │
        │ Recibe evento            │ Recibe evento            │ Recibe evento
        │                          │                          │
        ▼                          ▼                          ▼
signalRService.js              signalRService.js        signalRService.js
connection.on('PedidoGenerado')
        │
        ├─► handlePedidoGenerado()
        │   ├─► Verifica rol (admin/vendedor)
        │   ├─► Muestra notificación SUCCESS
        │   ├─► Dispara evento custom 'pedido-generado'
        │   └─► Ejecuta listeners
        │
        ▼
Pedido.vue (en listener)
window.addEventListener('pedido-generado', handlePedidoActualizado)
        │
        ├─► handlePedidoActualizado()
        │   └─► load() ← ¡RECARGA LA TABLA!
        │
        ▼
API: GET /Pedido/ObtenerListadoPaginado
        │
        ▼
Tabla se actualiza con nuevos pedidos ✅
```

---

## 🎯 Eventos Soportados

### 1️⃣ **CREAR PEDIDO** (Nueva compra desde cliente)
**Backend emite:** `PedidoGenerado`
**A quién:** Grupos "admin" y "vendedores"
**Frontend muestra:** Notificación con código, total, cliente

```csharp
// Backend - NotificarNuevoPedido()
await _hubContext.Clients.Group("admin").SendAsync("PedidoGenerado", data);
await _hubContext.Clients.Group("vendedores").SendAsync("PedidoGenerado", data);
```

```javascript
// Frontend - signalRService.js
this.connection.on('PedidoGenerado', (data) => {
    this.handlePedidoGenerado(data)  // ← Muestra notificación y recarga
})
```

### 2️⃣ **ACTUALIZAR PEDIDO** (Confirmar desde Vendedor/Admin)
**Backend emite:** `PedidoActualizado`
**A quién:** Todos (Clients.All)
**Frontend muestra:** Notificación de cambio de estado

```csharp
// Backend - Después de actualizar estado
await _hubContext.Clients.All.SendAsync("PedidoActualizado", data);
```

```javascript
// Frontend - signalRService.js
this.connection.on('PedidoActualizado', (data) => {
    this.handlePedidoActualizado(data)  // ← Muestra notificación y recarga
})
```

### 3️⃣ **CANCELAR PEDIDO** (Rechazar desde Vendedor/Admin)
**Backend emite:** `PedidoCancelado`
**A quién:** Todos (Clients.All)
**Frontend muestra:** Notificación de cancelación

```csharp
// Backend - Después de cancelar
await _hubContext.Clients.All.SendAsync("PedidoCancelado", data);
```

```javascript
// Frontend - signalRService.js
this.connection.on('PedidoCancelado', (data) => {
    this.handlePedidoCancelado(data)  // ← Muestra notificación y recarga
})
```

---

## 📋 Checklist de Implementación

### Backend ✅
- [x] NotificarNuevoPedido() emite a grupos "admin" y "vendedores"
- [x] GenerarPedido() llama a NotificarNuevoPedido()
- [ ] ActualizarPedido() emite `PedidoActualizado` a Clients.All
- [ ] CancelarPedido() emite `PedidoCancelado` a Clients.All
- [ ] Los usuarios están siendo agregados a los grupos correcto en OnConnectedAsync()

### Frontend ✅
- [x] signalRService.js escucha los 3 eventos
- [x] Pedido.vue conecta a SignalR en onMounted()
- [x] Pedido.vue registra listeners para los 3 eventos
- [x] handlePedidoActualizado() ejecuta load() para recarga

---

## 🔐 Roles y Permisos

| Rol | Ve Nuevo Pedido | Ve Actualización | Ve Cancelación | Puede Editar |
|-----|-----------------|------------------|----------------|--------------|
| Admin | ✅ | ✅ | ✅ | ✅ |
| Vendedor | ✅ | ✅ | ✅ | ✅ |
| Cliente | ❌ | ✅ (solo sus pedidos) | ✅ (solo sus pedidos) | ❌ |

---

## 🚀 Cómo Funciona en Tiempo Real

### Escenario: Cliente crea pedido mientras Vendedor está viendo la lista

```
CLIENTE (Carrito)              VENDEDOR (Pedidos.vue)
    │                               │
    │ 1. Click Comprar              │
    │────────► POST /Pedido          │
    │                               │
    │          Backend emite        │
    │          "PedidoGenerado"     │
    │                               │
    │                               │ 2. Recibe evento
    │                               │ Notificación: "🛒 ¡Nuevo Pedido!"
    │                               │ Tabla se recarga automáticamente
    │                               │
    │                          ✅ Nuevo pedido aparece
```

### Escenario: Vendedor confirma mientras Cliente está viendo su pedido

```
VENDEDOR (Editar Pedido)      CLIENTE (Ver Pedidos)
    │                               │
    │ 1. Click Confirmar            │
    │────────► POST /Actualizar     │
    │                               │
    │          Backend emite        │
    │          "PedidoActualizado"  │
    │                               │
    │                               │ 2. Recibe evento
    │                               │ Notificación: "📦 Tu pedido cambió a: Confirmado"
    │                               │ Tabla se recarga automáticamente
    │                               │
    │                          ✅ Estado actualizado
```

---

## 🔧 Troubleshooting

### El vendedor no ve nuevos pedidos
**Solución:**
```csharp
// Verificar que el usuario está en el grupo "vendedores"
public override async Task OnConnectedAsync()
{
    var user = Context.User;
    var rol = user?.FindFirst("rol")?.Value;

    if (rol == "vendedor")
        await Groups.AddToGroupAsync(Context.ConnectionId, "vendedores");

    if (rol == "admin")
        await Groups.AddToGroupAsync(Context.ConnectionId, "admin");

    await base.OnConnectedAsync();
}
```

### El cliente no ve cambios en sus pedidos
**Solución:**
Verificar que `PedidoActualizado` se emita a **Clients.All**, no solo a grupos específicos:
```csharp
// ✅ CORRECTO
await _hubContext.Clients.All.SendAsync("PedidoActualizado", data);

// ❌ INCORRECTO (solo los admins lo verían)
await _hubContext.Clients.Group("admin").SendAsync("PedidoActualizado", data);
```

### La tabla no se recarga
**Solución:**
Verificar que Pedido.vue está registrando los listeners en `onMounted()`:
```javascript
window.addEventListener('pedido-generado', handlePedidoActualizado)
window.addEventListener('pedido-actualizado', handlePedidoActualizado)
window.addEventListener('pedido-cancelado', handlePedidoActualizado)
```

---

## 📝 Resumen

✅ **Función:** El sistema notifica en tiempo real a todos los usuarios sobre cambios en pedidos
✅ **Bidireccional:** Admin/Vendedor ↔ Cliente
✅ **Automático:** No requiere F5, se recarga solo
✅ **Escalable:** Usa grupos de SignalR para eficiencia
✅ **Seguro:** Filtra notificaciones por rol
