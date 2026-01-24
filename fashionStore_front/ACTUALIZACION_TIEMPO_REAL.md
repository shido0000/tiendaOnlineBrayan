# Sistema de Actualización en Tiempo Real de Pedidos

## 📡 Descripción General

Se ha implementado un sistema de actualización en tiempo real usando **SignalR** que sincroniza la página de Pedidos en múltiples ventanas/pestañas simultáneamente.

**Ahora funciona bidireccional:** Cuando un vendedor/administrador cambia el estado de un pedido, los clientes ven la actualización automáticamente en sus pantallas.

## 🔧 Componentes Implementados

### 1. **SignalRService** (`src/services/signalRService.js`)
- Servicio que gestiona la conexión WebSocket con el servidor
- **Eventos escuchados:**
  - `PedidoGenerado`: Cuando se crea un nuevo pedido
  - `PedidoActualizado`: Cuando se actualiza el estado de un pedido existente
  - `PedidoCancelado`: Cuando se cancela/rechaza un pedido

- **Métodos principales:**
  - `connect()`: Conecta al hub de SignalR
  - `onPedidoGenerado(callback)`: Registra listeners personalizados
  - `handlePedidoActualizado(data)`: Procesa actualizaciones de pedidos
  - `disconnect()`: Desconecta del servidor

### 2. **Página de Pedidos** (`src/pages/Nomenclators/Pedido.vue`)
- **onMounted()**: Conecta a SignalR y registra listeners
- **onBeforeUnmount()**: Limpia los listeners pero mantiene la conexión activa

#### Eventos Disparados:
1. **`nuevoPedidoConfirmado`** - Cuando se confirma un pedido
2. **`pedidoEliminado`** - Cuando se cancela un pedido
3. Escucha eventos de SignalR automáticamente:
   - `pedido-generado` - Nuevo pedido creado
   - `pedido-actualizado` - Pedido cambió de estado
   - `pedido-cancelado` - Pedido fue cancelado

## 🔄 Flujo de Actualización

```
Usuario A en Ventana 1         Usuario B en Ventana 2
        |                              |
        | Confirma pedido              | Escucha cambios
        |---------> Servidor <---------|
                        |
                   SignalR Hub
                        |
        |<------- Emite evento -------|
        |                              |
    load()                         load()
   (Recarga                       (Recarga
    la lista)                      la lista)
```

## 📦 Eventos Custom Window

### 1. `nuevoPedidoConfirmado`
**Cuándo se dispara:** Cuando el usuario confirma un pedido
```javascript
{
  id: "uuid-del-pedido",
  codigo: "PED-001",
  estado: "Confirmado",
  timestamp: "2024-01-23T10:30:00Z"
}
```

### 2. `pedidoEliminado`
**Cuándo se dispara:** Cuando se cancela/rechaza un pedido
```javascript
{
  id: "uuid-del-pedido",
  timestamp: "2024-01-23T10:30:00Z"
}
```

### 3. `pedido-generado` y `pedido-actualizado`
**Cuándo se dispara:** Por el servidor a través de SignalR
```javascript
{
  // Datos del pedido del servidor
  codigo: "PED-001",
  cliente: "Juan Pérez",
  total: 150.50,
  estado: "Confirmado",
  timestamp: "2024-01-23T10:30:00Z"
}
```

## 🎯 Comportamiento Actual

1. **Al abrir la página de Pedidos:**
   - Se conecta automáticamente a SignalR
   - Se registran 4 listeners de eventos
   - **Clientes:** Solo ven sus propios pedidos (filtro por `usuarioId`)
   - **Vendedores/Administradores:** Ven todos los pedidos

2. **Al confirmar un pedido (Vendedor/Admin):**
   - Se dispara `nuevoPedidoConfirmado`
   - Se espera 500ms para que SignalR procese el evento
   - Se recarga la lista en esa ventana
   - **IMPORTANTE:** El servidor emite `PedidoActualizado` a TODOS (incluyendo clientes)
   - Los clientes reciben la notificación y su lista se actualiza automáticamente
   - El cliente ve: "📦 Tu pedido PED-001 ha cambiado a: Confirmado"

3. **Al cancelar un pedido (Vendedor/Admin):**
   - Se dispara `pedidoEliminado`
   - El servidor emite `PedidoCancelado` a TODOS
   - Los clientes reciben la notificación: "❌ Tu pedido PED-001 ha sido cancelado"
   - La lista se actualiza automáticamente en 1.5 segundos

4. **Cambios en otras ventanas (en tiempo real):**
   - SignalR emite `pedido-generado`, `pedido-actualizado` o `pedido-cancelado`
   - Todos los listeners ejecutan `handlePedidoActualizado()`
   - Se ejecuta `load()` para actualizar automáticamente la tabla
   - **SIN NECESIDAD DE RECARGAR LA PÁGINA**

## 🔐 Control por Roles

**Clientes:**
- ✅ Solo ven sus propios pedidos (filtro por `usuarioId` en la API)
- ✅ Modo lectura completo (no pueden editar)
- ✅ Reciben notificaciones cuando sus pedidos cambian de estado
- ✅ Notificación: "📦 Tu pedido XXX ha cambiado a: Confirmado"
- ✅ Reciben alerta cuando sus pedidos son cancelados

**Vendedores/Administradores:**
- ✅ Ven todos los pedidos
- ✅ Pueden editar y confirmar
- ✅ Reciben notificaciones en tiempo real de nuevos pedidos
- ✅ Sus cambios se reflejan automáticamente en los pedidos de los clientes

## 🚀 Ventajas

✅ **Sincronización en tiempo real** entre múltiples ventanas
✅ **Sin necesidad de F5** - Las páginas se actualizan automáticamente
✅ **Control por roles** - Los Clientes solo ven sus pedidos
✅ **Eventos personalizados** - Fácil de extender a otros componentes
✅ **Reconexión automática** - Si cae la conexión, se reconecta solo

## 📝 Próximas Mejoras Recomendadas

1. Agregar sonido/notificación visual cuando hay cambios
2. Mostrar indicador de conexión SignalR en la UI
3. Extender a otros nomencladores (Productos, Categorías, etc.)
4. Agregar historial de cambios de pedidos
5. Implementar cola de offline si se pierde conexión

## 🐛 Troubleshooting

Si no se actualiza en tiempo real:

1. **Verificar conexión SignalR:**
   ```javascript
   console.log(signalRService.getConnectionState())
   ```

2. **Revisar consola** para errores de conexión

3. **Verificar que el backend** esté enviando eventos:
   - El hub debe estar configurado en `https://localhost:6005/pedidosHub`

4. **Limpiar cache** y recargar página completa (Ctrl + F5)
