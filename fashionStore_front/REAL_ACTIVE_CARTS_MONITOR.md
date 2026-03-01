---
# 📊 RealActiveCartsMonitor.vue - Monitor con Datos de Base de Datos

## 🎯 ¿Qué es?

Una nueva página de testing del sistema de monitoreo de carritos que carga datos **directamente de la base de datos** a través del backend, en lugar de usar datos mock/simulados.

## ✨ Características

### ✅ Carga de Datos Reales
- Obtiene carritos activos del endpoint `/api/usuarios/carritos-activos`
- Muestras datos reales de la BD
- Transforma automáticamente al formato esperado

### ✅ Autorecargar Automático
- Botón para habilitar/deshabilitar autorecargar
- Intervalo configurable (por defecto 15 segundos)
- Actualiza en segundo plano

### ✅ Actualización en Tiempo Real con SignalR
- Escucha evento `CarritoActualizado` del backend
- Actualiza carritos cuando se modifican remotamente
- Sin necesidad de recargar

### ✅ Panel de Control
- **Botón Cargar Datos**: Obtiene carritos del backend manualmente
- **Botón Autorecargar**: Activa/desactiva recargas periódicas
- **Botón Vaciar Datos**: Limpia la lista
- Muestra estado de conexión SignalR
- Mostra última hora de actualización
- Estadísticas en tiempo real

### ✅ Visualización Detallada
- Lista de usuarios con carritos activos
- Información personal (nombre, email, ID)
- Badge con cantidad de items y valor total
- Tabla expandible con detalles de cada producto
- Precios, cantidades y subtotales

### ✅ Herramientas de Debug
- Logs en consola (opcional)
- Botón "Ver datos en consola"
- Console.table para visualizar datos fácilmente
- Errores detallados con notificaciones

---

## 🚀 Cómo Acceder

### Desde el Router
```
URL: http://localhost:8080/#/RealActiveCartsMonitor
```

### Desde el Código
```vue
<router-link to="/RealActiveCartsMonitor">
  Monitor de Carritos (Base de Datos)
</router-link>
```

### Desde la Línea de Comandos
Abre tu navegador y escribe:
```
http://localhost:PORT/#/RealActiveCartsMonitor
```

---

## 📋 Flujo de Uso

### 1. **Abrir la Página**
```
Ir a http://localhost:8080/#/RealActiveCartsMonitor
```

### 2. **Cargar Datos**
Haz clic en el botón **"Cargar Datos"**
- Frontend: Envía GET a `/api/usuarios/carritos-activos`
- Backend: Retorna lista de usuarios con carritos activos
- Frontend: Transforma y muestra en la tabla

### 3. **Ver Estadísticas**
Se mostrarán automáticamente:
- Cantidad de usuarios con carrito
- Valor total de todos los carritos
- Total de items

### 4. **Revisar Detalles (Opcional)**
Haz clic en cualquier usuario para expandir y ver:
- Productos específicos en su carrito
- Cantidades y precios
- Subtotales por producto

### 5. **Autorecargar (Opcional)**
Haz clic en **"Autorecargar"** para:
- Recargar datos cada 15 segundos
- Ver cambios en tiempo real
- El badge cambiará de color según cantidad

### 6. **Tiempo Real con SignalR**
Si todo está funcionando:
- Backend emite `CarritoActualizado`
- Carrito se actualiza **sin recargar**
- Timestamp "Última actualización" cambia

---

## 🔧 Configuración

### Intervalo de Autorecargar
**Ubicación:** Componente `RealActiveCartsMonitor.vue`, línea ~60
```javascript
const autoRefreshInterval = ref(15000) // Millisegundos
// 15000 = 15 segundos
// 5000 = 5 segundos
// 30000 = 30 segundos
```

Para cambiar:
```javascript
autoRefreshInterval.value = 10000 // cada 10 segundos
```

### Colores y Temas
Los colores se usan según cantidad de items:
```javascript
:class="user.cartCount > 3 ? 'bg-deep-orange-1' : 'bg-light-green-1'"
// > 3 items = Orange (alerta)
// <= 3 items = Verde (ok)
```

---

## 💾 Comparación: TestActiveCartsMonitor vs RealActiveCartsMonitor

| Característica | TestActiveCartsMonitor | RealActiveCartsMonitor |
|---|---|---|
| Fuente de datos | Mock (fixtures) | API Backend |
| Endpoint | Ninguno | GET /api/usuarios/carritos-activos |
| Actualización manual | ✅ mediante botón "Iniciar Simulación" | ✅ mediante botón "Autorecargar" |
| Cambios simulados | ✅ cada 3 segundos | ❌ no simula, usa reales |
| Datos reales de BD | ❌ | ✅ |
| SignalR | Visual de cómo funciona | Real (si backend emite) |
| Uso | 👨‍💻 Desarrollo sin backend | 📊 Testing con backend |

---

## 🔍 Debug

### Ver Datos en Consola
```javascript
// Opción 1: Botón en la app
Haz clic en "Ver datos en consola"

// Opción 2: Manualmente en console
window.__activeCartsData // Si fue expuesto
```

### Habilitar Logs
```vue
<script>
const showConsoleLog = ref(true) // Verá todos los logs en DevTools
</script>
```

### Monitorear Requests
```
DevTools → Network → Filtra por "carritos-activos"
```

Deberías ver:
- Primer request: Manual (botón Cargar)
- Requests posteriores (cada 15s): Autorecargar
- Eventos SignalR: En la pestaña "Messages" si hay WebSocket

---

## ⚠️ Requisitos del Backend

Para que esta página funcione correctamente, el backend DEBE implementar:

### 1. Endpoint GET
```
GET /api/usuarios/carritos-activos
```

**Retorna:**
```json
{
  "success": true,
  "data": [
    {
      "usuarioId": "550e8400-e29b-41d4-a716-446655440001",
      "nombre": "Juan",
      "apellido": "Pérez",
      "email": "juan@example.com",
      "detalles": [
        {
          "carritoId": "abc-123",
          "productoId": "prod-1",
          "cantidad": 2,
          "unitPrice": 50,
          "lineTotal": 100
        }
      ]
    }
  ]
}
```

### 2. Evento SignalR (Opcional pero Recomendado)
```csharp
// Backend emite esto cuando se actualiza un carrito:
await Clients.Groups("Admins", "Vendedores")
  .SendAsync("CarritoActualizado", new
  {
    usuarioId = "...",
    totalItems = 5,
    totalValue = 250.00,
    timestamp = DateTime.UtcNow
  })
```

---

## 🐛 Troubleshooting

### Problema: "Error al cargar datos"
**Causa:** El endpoint no existe o el backend no responde
**Solución:**
1. Verificar que el backend esté corriendo
2. Revisar la URL del endpoint
3. Ver error en DevTools Console

### Problema: No muestra datos
**Causa:** Respuesta vacía del backend
**Solución:**
1. Verificar que hay usuarios con carritos en BD
2. Check endpoint retorna formato correcto
3. Habilitar logs: `showConsoleLog = true`

### Problema: Autorecargar no funciona
**Causa:** Puede ser error en el endpoint
**Solución:**
1. Probar botón "Cargar Datos" primero
2. Si funciona manual pero no auto, reducir intervalo
3. Buscar errores en Backend

### Problema: SignalR no actualiza en tiempo real
**Causa:** Backend no emite evento o grupo no configurado
**Solución:**
1. Verificar que el backend emite `CarritoActualizado`
2. Verificar que admins están en el grupo "Admins" o "Vendedores"
3. Probar evento manualmente desde backend

---

## 📱 Responsive

La página es completamente responsive:
- **Desktop:** Panel de control a la izquierda (25%), monitor a la derecha (75%)
- **Tablet:** Stack vertical con buen espaciado
- **Mobile:** Panel completo arriba, monitor abajo

---

## 🎨 Estructura Visual

```
┌─────────────────────────────────────────────────────┐
│ 📊 Monitor de Carritos (Datos Reales)              │
│ Cargando de la base de datos en tiempo real...     │
└─────────────────────────────────────────────────────┘

┌─────────────────────┬────────────────────────────────┐
│  PANEL IZQUIERDA    │    CONTENIDO PRINCIPAL         │
│                     │                                 │
│ [Cargar Datos]      │ ┌──────────────────────────────┐│
│ [Autorecargar]      │ │ 🛒 Usuarios con Carrito      ││
│ [Vaciar Datos]      │ │ 3 usuarios con carrito       ││
│                     │ ├──────────────────────────────┤│
│ Estado:             │ │ Usuario 1: Juan Pérez        ││
│ ✅ Cargados         │ │ Email: juan@mail.com         ││
│                     │ │ Carrito: 5 items - $250      ││
│ Usuarios: 3         │ │ ┌─ Expandir detalles ─────┐  ││
│ Total: $1,250.00    │ │                             │  ││
│ Items: 12           │ │ Usuario 2: María García      ││
│                     │ │ Email: maria@mail.com        ││
│ Última actualización:│ │ Carrito: 2 items - $120      ││
│ 14:23:45            │ │                              ││
│                     │ │ Usuario 3: Carlos López      ││
│ SignalR: ✅ Conect. │ │ Email: carlos@mail.com       ││
│                     │ │ Carrito: 5 items - $880      ││
│ [📋 Ver en consola]│ │                              ││
│                     │ └──────────────────────────────┘│
└─────────────────────┴────────────────────────────────┘
```

---

## 📝 Resumen

| Aspecto | Detalles |
|---|---|
| **Archivo** | `src/pages/Test/RealActiveCartsMonitor.vue` |
| **Ruta** | `/RealActiveCartsMonitor` |
| **Requiere Auth** | Sí (usuario autenticado) |
| **Roles permitidos** | Admin, Vendedor, Cualquiera autenticada |
| **Endpoint** | GET `/api/usuarios/carritos-activos` |
| **SignalR Event** | `CarritoActualizado` |
| **Autorecargar** | ✅ Soportado (15s por defecto) |
| **Tiempo Real** | ✅ Via SignalR |
| **Estado** | 🟢 Lista para usar |

---

## 🎯 Próximos Pasos

1. ✅ Frontend: Página creada
2. ⏳ Backend: Implementar endpoint
3. ⏳ Backend: Emitir evento SignalR
4. ⏳ Testing: Validar datos en tiempo real
5. ⏳ Producción: Integrar en Dashboard real

