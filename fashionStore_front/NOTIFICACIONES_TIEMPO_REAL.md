# Sistema de Notificaciones en Tiempo Real para Nuevos Pedidos

## Descripción General

El sistema implementa notificaciones en tiempo real utilizando **SignalR** para informar a los administradores y vendedores sobre los nuevos pedidos que se generan en la plataforma.

## Flujo de Funcionamiento

### 1. **Cliente (Frontend) - Confirmación del Pedido**

Cuando un cliente confirma un pedido en el componente `ConfirmarPedido.vue`:

```javascript
// Archivo: src/pages/Visual/components/ConfirmarPedido.vue
async function confirmOrder() {
  // ... validaciones ...

  // Enviar pedido al backend
  await saveDataPronosticoEnviarObjeto(ruta, generarPedidoDto, dialogLoad)
    .then(resultado => {
      if (!resultado?.mensajeError) {
        Success("Pedido enviado con éxito")

        // ← AQUÍ se dispara la notificación
        notificarNuevoPedido(resultado, form.value.nombre)

        // Limpiar y navegar
        cart.clearCart()
        router.push({ name: 'IndexPage' })
      }
    })
}
```

### 2. **Función de Notificación**

La función `notificarNuevoPedido()` construye los datos del pedido y dispara un evento personalizado:

```javascript
async function notificarNuevoPedido(resultadoPedido, nombreCliente) {
  const datosNotificacion = {
    pedidoId: resultadoPedido?.pedidoId || resultadoPedido?.id,
    codigo: resultadoPedido?.codigo || `#${Date.now()}`,
    total: resultadoPedido?.total || totalConExtras.value,
    cliente: nombreCliente,
    cantidadProductos: items.value.length,
    estado: resultadoPedido?.estado || 'Pendiente',
    fecha: new Date().toISOString(),
    productos: items.value.map(p => ({
      nombre: p.nombre || p.descripcion,
      cantidad: p.cantidad,
      precio: p.precioVenta
    }))
  }

  // Dispara evento que SignalR captura
  const event = new CustomEvent('nuevoPedidoConfirmado', {
    detail: datosNotificacion
  })
  window.dispatchEvent(event)
}
```

### 3. **Servicio SignalR - Captura y Envío**

El servicio `signalRService.js` escucha el evento y lo envía al backend:

```javascript
// Archivo: src/services/signalRService.js

setupFrontendListeners() {
  window.addEventListener('nuevoPedidoConfirmado', async (event) => {
    console.log('📢 Evento de nuevo pedido confirmado recibido:', event.detail)
    await this.notificarNuevoPedido(event.detail)
  })
}

async notificarNuevoPedido(datosNotificacion) {
  if (this.connection?.state === signalR.HubConnectionState.Connected) {
    await this.connection.invoke('NotificarNuevoPedido', datosNotificacion)
    console.log('✅ Notificación enviada al hub')
  }
}
```

### 4. **Backend - Recepción y Broadcast**

El backend (SignalR Hub) recibe la notificación y la envía a todos los usuarios conectados que sean administrador o vendedor.

El backend debe tener un método como:

```csharp
[Authorize]
public async Task NotificarNuevoPedido(NotificacionPedidoDto datosNotificacion)
{
    var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

    // Solo procesar si viene de un cliente autenticado
    if (userRole == "Cliente")
    {
        // Enviar notificación a todos los administradores y vendedores
        await Clients.Groups("Administrador", "Vendedor")
            .SendAsync("PedidoGenerado", datosNotificacion);
    }
}
```

### 5. **Recepción en Frontend (Admin/Vendedores)**

Los administradores y vendedores conectados reciben la notificación:

```javascript
// En signalRService.js
this.connection.on('PedidoGenerado', (data) => {
  console.log('📦 Nuevo pedido recibido:', data)
  this.handlePedidoGenerado(data)
})

handlePedidoGenerado(data) {
  const userRole = this.getUserData()?.role?.toLowerCase()

  // Solo mostrar si el usuario es admin o vendedor
  if (['admin', 'administrador', 'vendedor'].includes(userRole)) {
    Success(`🛒 ¡Nuevo Pedido! #${data.Codigo}`)
  }
}
```

## Estructura de Datos

### Formato de la Notificación

```json
{
  "pedidoId": "uuid-del-pedido",
  "codigo": "#12345",
  "total": 199.99,
  "cliente": "Juan Pérez",
  "cantidadProductos": 3,
  "estado": "Pendiente",
  "fecha": "2026-01-23T10:30:00Z",
  "productos": [
    {
      "nombre": "Camiseta Azul",
      "cantidad": 1,
      "precio": 29.99
    },
    {
      "nombre": "Pantalón Negro",
      "cantidad": 2,
      "precio": 79.99
    }
  ]
}
```

## Instalación y Configuración

### 1. Instalar Dependencias

```bash
npm install @microsoft/signalr
```

### 2. Configurar la URL del Hub

En `signalRService.js`, verificar que la URL sea correcta:

```javascript
.withUrl('https://localhost:6005/pedidosHub', {
  accessTokenFactory: () => token,
  skipNegotiation: true,
  transport: signalR.HttpTransportType.WebSockets
})
```

### 3. Token JWT

El token JWT debe incluir el rol del usuario para que el sistema pueda identificar si es admin o vendedor:

```javascript
// El token debe tener una estructura como:
{
  "Id": "user-id",
  "email": "user@example.com",
  "role": "Administrador", // o "Vendedor"
  "name": "Nombre Usuario"
}
```

## Características

✅ **Notificaciones en Tiempo Real**: Usa WebSockets para comunicación instantánea
✅ **Filtrado por Rol**: Solo administradores y vendedores reciben notificaciones
✅ **Reconexión Automática**: Si la conexión se pierde, intenta reconectar
✅ **Manejo de Errores**: Captura y registra errores en la consola
✅ **Eventos Personalizados**: Usa eventos del navegador para comunicación entre componentes

## Debugging

### Verificar Conexión

```javascript
console.log(signalRService.getConnectionState())
// Salida: 1 (Connected) o 0 (Disconnected)
```

### Ver Logs

Abre la consola del navegador (F12) y busca:
- `✅ Conectado a SignalR`
- `📦 Nuevo pedido recibido`
- `🛒 ¡Nuevo Pedido!`

### Común Problemas

| Problema | Solución |
|----------|----------|
| No se conecta | Verificar que el backend esté corriendo en `https://localhost:6005` |
| No recibe notificaciones | Verificar que el rol en el token sea "Admin", "Administrador" o "Vendedor" |
| Reconexión lenta | Aumentar los tiempos en `withAutomaticReconnect` |

## Integración con Backend

### Modelo de Datos (C#)

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

public class ProductoNotificacionDto
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
```

### Hub en Backend

```csharp
public class PedidosHub : Hub
{
    public async Task NotificarNuevoPedido(NotificacionPedidoDto datos)
    {
        // Procesar y enviar a administradores y vendedores
        await Clients.Groups("Administrador", "Vendedor")
            .SendAsync("PedidoGenerado", datos);
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

## Flujo Completo - Diagrama

```
Cliente Confirma Pedido
        ↓
notificarNuevoPedido() ejecuta
        ↓
Dispara evento: nuevoPedidoConfirmado
        ↓
signalRService escucha el evento
        ↓
connection.invoke('NotificarNuevoPedido', datos)
        ↓
Backend recibe en PedidosHub
        ↓
Backend valida rol del usuario
        ↓
Broadcast a grupos: "Administrador", "Vendedor"
        ↓
Clientes Admin/Vendedor reciben 'PedidoGenerado'
        ↓
handlePedidoGenerado() procesa
        ↓
Muestra notificación con Success()
```

## Métodos Disponibles

### signalRService

```javascript
// Conectar a SignalR
await signalRService.connect()

// Registrar listener personalizado
signalRService.onPedidoGenerado((data) => {
  console.log('Nuevo pedido:', data)
})

// Notificar nuevo pedido
await signalRService.notificarNuevoPedido(datosNotificacion)

// Verificar estado
signalRService.getConnectionState()

// Desconectar
await signalRService.disconnect()
```

## Notas Importantes

1. **Token Requerido**: Debe haber un token JWT válido en localStorage o sessionStorage
2. **HTTPS**: SignalR requiere HTTPS en producción
3. **CORS**: Configurar CORS en el backend si el frontend está en diferente dominio
4. **Timeouts**: El backend puede desconectar por inactividad, SignalR reintentará automáticamente
5. **Datos Sensibles**: No incluir información sensible en las notificaciones

---

**Última Actualización**: Enero 23, 2026
**Versión**: 1.0
