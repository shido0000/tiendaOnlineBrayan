# Arquitectura del Sistema de Notificaciones en Tiempo Real

## 🏗️ Diagrama General

```
┌─────────────────────────────────────────────────────────────────────────┐
│                          CLIENTE (Navegador)                            │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  ConfirmarPedido.vue (Cliente)                                   │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  1. Cliente completa formulario                                  │  │
│  │  2. confirmOrder() se ejecuta                                    │  │
│  │  3. Envía datos al backend (Pedido/GenerarPedido)              │  │
│  │  4. Si es exitoso:                                              │  │
│  │     └─→ notificarNuevoPedido()                                 │  │
│  │         └─→ Construye datosNotificacion                         │  │
│  │         └─→ window.dispatchEvent('nuevoPedidoConfirmado')      │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼                                                              │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  signalRService.js                                               │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  • setupFrontendListeners()                                      │  │
│  │    Escucha: 'nuevoPedidoConfirmado'                             │  │
│  │    Ejecuta: notificarNuevoPedido(datosNotificacion)            │  │
│  │                                                                  │  │
│  │  • notificarNuevoPedido()                                        │  │
│  │    connection.invoke('NotificarNuevoPedido', datos)            │  │
│  │                                                                  │  │
│  │  • handlePedidoGenerado()                                        │  │
│  │    Recibe notificación del backend                             │  │
│  │    Muestra Success() si rol = Admin/Vendedor                   │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼  WebSocket / Long Polling                                   │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  @microsoft/signalr                                              │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  • HubConnection                                                 │  │
│  │  • URL: https://localhost:6005/pedidosHub                      │  │
│  │  • Auth: Bearer {JWT_TOKEN}                                     │  │
│  │  • Protocolo: JSON                                              │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼                                                              │
│           ════════════════════════════════════════════════════════      │
│           Conexión WebSocket a Backend                                  │
│           ════════════════════════════════════════════════════════      │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                    BACKEND (ASP.NET Core API)                           │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  PedidoController                                                │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  [POST] /api/pedido/confirmar                                    │  │
│  │  • Recibe: GenerarPedidoDto                                      │  │
│  │  • Crea pedido en BD                                             │  │
│  │  • Retorna: PedidoResultadoDto                                   │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼  (Opcional: Notificar desde backend)                        │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  IHubContext<PedidosHub>                                         │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  Clients.Groups("Admin", "Vendedor").SendAsync("PedidoGenerado") │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼                                                              │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  PedidosHub : Hub                                                │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  OnConnectedAsync()                                              │  │
│  │  • Lee rol del JWT                                               │  │
│  │  • Agrega conexión al grupo: {role}                             │  │
│  │                                                                  │  │
│  │  NotificarNuevoPedido(NotificacionPedidoDto)                    │  │
│  │  • Valida que sea un cliente                                    │  │
│  │  • Envía a grupos: "Admin", "Administrador", "Vendedor"        │  │
│  │  • Método remoto: "PedidoGenerado"                             │  │
│  │                                                                  │  │
│  │  OnDisconnectedAsync(Exception)                                 │  │
│  │  • Log de desconexión                                           │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│           │                                                              │
│           ▼  SignalR Broadcast                                          │
│           ════════════════════════════════════════════════════════      │
│           Envía 'PedidoGenerado' a grupos                               │
│           ════════════════════════════════════════════════════════      │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│              CLIENTES CONECTADOS (Admin / Vendedor)                      │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  Admin Browser Tab #1                                            │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  • Conectado a /pedidosHub                                       │  │
│  │  • Grupo: "Administrador"                                        │  │
│  │  • Token role: "Administrador"                                   │  │
│  │                                                                  │  │
│  │  Recibe: 'PedidoGenerado' evento                                │  │
│  │  └─→ handlePedidoGenerado()                                     │  │
│  │      └─→ Success("🛒 ¡Nuevo Pedido! #12345")                   │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│                                                                         │
│  ┌──────────────────────────────────────────────────────────────────┐  │
│  │  Vendedor Browser Tab #2                                         │  │
│  │  ─────────────────────────────────────────────────────────────   │  │
│  │  • Conectado a /pedidosHub                                       │  │
│  │  • Grupo: "Vendedor"                                             │  │
│  │  • Token role: "Vendedor"                                        │  │
│  │                                                                  │  │
│  │  Recibe: 'PedidoGenerado' evento                                │  │
│  │  └─→ handlePedidoGenerado()                                     │  │
│  │      └─→ Success("🛒 ¡Nuevo Pedido! #12345")                   │  │
│  │                                                                  │  │
│  └──────────────────────────────────────────────────────────────────┘  │
│                                                                         │
│  ❌ Cliente NO conectado a /pedidosHub                                 │
│     (No recibe notificaciones, solo puede enviarlas)                    │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

## 🔄 Flujo de Datos - Secuencia

```
1. CLIENTE CONFIRMA PEDIDO
   └─ confirmOrder() → POST /api/Pedido/GenerarPedido

2. BACKEND PROCESA
   └─ PedidoController.GenerarPedido()
      └─ Crea registro en BD
      └─ Retorna PedidoResultadoDto { pedidoId, codigo, total, ... }

3. FRONTEND NOTIFICA
   └─ notificarNuevoPedido(resultado)
      └─ Construye datosNotificacion
      └─ window.dispatchEvent('nuevoPedidoConfirmado', { detail: datos })

4. SIGNALR CAPTURA EVENTO
   └─ setupFrontendListeners() escucha 'nuevoPedidoConfirmado'
      └─ connection.invoke('NotificarNuevoPedido', datos)

5. BACKEND RECIBE EN HUB
   └─ PedidosHub.NotificarNuevoPedido(datosNotificacion)
      └─ Valida que sea Cliente
      └─ Clients.Groups("Admin", "Administrador", "Vendedor")
         .SendAsync('PedidoGenerado', datosNotificacion)

6. ADMIN/VENDEDOR RECIBE
   └─ connection.on('PedidoGenerado', (data) => ...)
      └─ handlePedidoGenerado(data)
         └─ Success("🛒 ¡Nuevo Pedido! #12345")
```

## 📊 Estructura de Datos

```javascript
// FRONTEND ENVÍA (datosNotificacion)
{
  pedidoId: "uuid-123",           // ID único del pedido
  codigo: "#12345",               // Código visible del pedido
  total: 199.99,                  // Total del pedido
  cliente: "Juan Pérez",          // Nombre del cliente
  cantidadProductos: 3,           // Cantidad de productos
  estado: "Pendiente",            // Estado actual
  fecha: "2026-01-23T10:30Z",    // Timestamp ISO
  productos: [                    // Productos en el pedido
    {
      nombre: "Camiseta Azul",
      cantidad: 1,
      precio: 29.99
    },
    {
      nombre: "Pantalón Negro",
      cantidad: 2,
      precio: 79.99
    }
  ]
}

// BACKEND ENVÍA (mismo formato o similar)
{
  Codigo: "#12345",
  Total: 199.99,
  Cliente: "Juan Pérez",
  CantidadProductos: 3,
  Estado: "Pendiente",
  Fecha: "2026-01-23T10:30Z",
  Productos: [...]
}
```

## 🔐 Seguridad - Flujo

```
1. CLIENTE CONECTA A SIGNALR
   └─ localStorage.getItem('token') → JWT
   └─ Token contiene: { Id, email, role, name }
   └─ Authorization header: "Bearer {token}"

2. BACKEND VALIDA TOKEN
   └─ [Authorize] attribute
   └─ JWT middleware valida firma
   └─ Extrae claims: ClaimTypes.NameIdentifier, ClaimTypes.Role

3. HUB AGREGA A GRUPO
   └─ OnConnectedAsync()
   └─ User?.FindFirst(ClaimTypes.Role)?.Value → "Administrador"
   └─ Groups.AddToGroupAsync(connectionId, "Administrador")

4. HUB VERIFICA ROL
   └─ NotificarNuevoPedido()
   └─ [Authorize(Roles = "Cliente,Admin,Administrador,Vendedor")]
   └─ Solo clientes pueden notificar
   └─ Solo admin/vendedor pueden recibir

5. BROADCAST A GRUPOS
   └─ Clients.Groups("Admin", "Administrador", "Vendedor")
   └─ No se envía a clientes
   └─ No se envía a usuarios sin rol
```

## 🔧 Componentes Clave

| Componente | Responsabilidad | Tecnología |
|-----------|-----------------|-----------|
| ConfirmarPedido.vue | Iniciar proceso, disparar evento | Vue 3, Quasar |
| signalRService | Conectar, escuchar, enviar | @microsoft/signalr |
| PedidosHub | Recibir, validar, broadcast | SignalR, ASP.NET Core |
| JWT Token | Autenticación y roles | JWT |
| WebSocket | Comunicación bidireccional | HTML5 WebSocket |

## 📡 Transporte

### Primario: WebSocket
```
Ventajas:
✓ Baja latencia
✓ Comunicación bidireccional real
✓ Eficiente en ancho de banda
✓ Moderno (HTML5)
```

### Fallback: Long Polling
```
Ventajas:
✓ Compatible con proxies/firewalls
✓ No requiere configuración especial
✓ Funciona en redes antiguas
```

## 🚀 Performance

### Optimizaciones Implementadas

1. **Reconexión Automática**
   - Intenta reconectar si la conexión se pierde
   - Aumenta el tiempo de espera gradualmente
   - Máximo 30 segundos de espera

2. **Single Notification**
   - No duplica notificaciones
   - Valida datos antes de enviar
   - Evita broadcast innecesarios

3. **Grupos por Rol**
   - No envía a todos (broadcast total)
   - Solo a roles específicos
   - Reduce tráfico de red

## 🔍 Debugging

### Verificar Conexión
```javascript
// Console (F12)
signalRService.connection?.state
// 1 = Connected
// 0 = Disconnected
// 2 = Reconnecting
// 3 = Connecting
```

### Ver Eventos Enviados
```javascript
// Console
window.addEventListener('nuevoPedidoConfirmado', (e) => {
  console.log('Evento disparado:', e.detail)
})
```

### Ver Eventos Recibidos
```javascript
// Console
// Ya está loguado en signalRService.handlePedidoGenerado()
// Busca: "📦 Nuevo pedido recibido"
```

## 🌐 Network Timeline

```
Time  | Evento
------|----------------------------------------------------------
0ms   | Cliente abre ConfirmarPedido.vue
100ms | signalRService.connect() inicia
150ms | Handshake WebSocket completo
200ms | Token enviado, autenticado
250ms | Usuario agregado a grupo "Administrador"
300ms | onMounted completado
------|----------------------------------------------------------
400ms | Cliente completa formulario y click "Confirmar"
500ms | confirmOrder() envía POST /api/Pedido/GenerarPedido
600ms | Backend procesa, guarda en BD
700ms | Backend retorna PedidoResultadoDto
800ms | notificarNuevoPedido() dispara evento
850ms | setupFrontendListeners() captura evento
900ms | connection.invoke('NotificarNuevoPedido') enviado
950ms | PedidosHub.NotificarNuevoPedido() recibe
1000ms| Valida datos y user role
1050ms| Clients.Groups().SendAsync('PedidoGenerado')
1100ms| Conexiones admin/vendedor reciben evento
1150ms| handlePedidoGenerado() ejecutado
1200ms| Success("🛒 ¡Nuevo Pedido!") mostrado en pantalla
```

**Latencia Total**: ~800ms (0.8 segundos)

---

**Documento**: Arquitectura del Sistema de Notificaciones
**Última Actualización**: Enero 23, 2026
**Versión**: 1.0
