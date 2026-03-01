# 🔄 Sincronización Periódica del Carrito

## ¿Qué Hace?

Cada 30 segundos, lee automáticamente el carrito guardado en `localStorage` con la key `fashion_cart_v1` y lo envía al backend usando el endpoint `/EnviarDatosAlCarrito`.

---

## ✅ Implementado

### 1. Servicio: `periodicCartSyncService.js`
- ✅ Lee localStorage automáticamente
- ✅ Envía al backend cada X segundos
- ✅ Obtiene usuarioId del JWT automáticamente
- ✅ Mapea estructura de datos
- ✅ Estadísticas y debugging

### 2. Boot File: `periodicCartSync.js`
- ✅ Se ejecuta automáticamente al iniciar la app
- ✅ Detecta autenticación/desautenticación
- ✅ Inicia/detiene sincronización automáticamente

### 3. Configuración: `quasar.config.js`
- ✅ Boot file agregado a la lista

---

## 🚀 Cómo Funciona

### Flujo Automático

```
App se inicia
    ↓
Boot file verifica si hay token
    ↓
Usuario autenticado?
├─ SÍ: Inicia periodicCartSyncService.start()
│   ├─ Ejecuta sync inmediatamente
│   ├─ Luego cada 30 segundos:
│   │  ├─ Lee localStorage (fashion_cart_v1)
│   │  ├─ Obtiene usuarioId del token
│   │  ├─ Mapea items a estructura backend
│   │  └─ POST /EnviarDatosAlCarrito ✅
│   └─ Continúa hasta desconexión
│
└─ NO: Espera autenticación
```

### Flujo de Datos

```
localStorage (fashion_cart_v1):
[
  {
    "id": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
    "nombre": "tenis de salir",
    "precioVenta": 20,
    "cantidad": 1,
    "foto": "...",
    "raw": {...}
  }
]
    ↓
periodicCartSyncService.formatCartData()
    ↓
Backend esperado:
{
  "usuarioId": "from-token",
  "fechaCreacion": "2026-02-21T14:23:45.123Z",
  "detalles": [
    {
      "carritoId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
      "productoId": "producto-id",
      "cantidad": 1,
      "unitPrice": 20,
      "lineTotal": 20
    }
  ]
}
    ↓
POST /EnviarDatosAlCarrito ✅
```

---

## 🎯 Intervalo de Sincronización

### Default: 30 segundos

```javascript
// En boot/periodicCartSync.js
periodicCartSyncService.start(30000) // 30000 ms = 30 segundos
```

### Cambiar intervalo:

```javascript
// Cada 15 segundos
periodicCartSyncService.start(15000)

// Cada 1 minuto
periodicCartSyncService.start(60000)

// Cada 5 minutos
periodicCartSyncService.start(300000)
```

---

## 🔧 Métodos Disponibles

### `start(intervalMs)`
Inicia la sincronización periódica
```javascript
import periodicCartSyncService from 'src/services/periodicCartSyncService'

periodicCartSyncService.start(30000) // Cada 30 segundos
```

### `stop()`
Detiene la sincronización
```javascript
periodicCartSyncService.stop()
```

### `getStatus()`
Obtiene información de sincronización
```javascript
const status = periodicCartSyncService.getStatus()
// {
//   isRunning: true,
//   lastSyncTime: Date,
//   syncCount: 15,
//   errorCount: 1,
//   successRate: "93.33%"
// }
```

### `restart(intervalMs)`
Reinicia con nuevo intervalo
```javascript
periodicCartSyncService.restart(60000) // Reinicia con 60 segundos
```

### `syncCartFromStorage()`
Ejecuta sincronización manual
```javascript
const result = await periodicCartSyncService.syncCartFromStorage()
// true si fue exitoso, false si falló o carrito vacío
```

---

## 🧪 Debugging

### En la Consola (DevTools)

Verifica que está funcionando:
```javascript
// Acceder al servicio (en dev mode)
window.__periodicCartSyncService

// Ver estado actual
window.__periodicCartSyncService.getStatus()

// Resultado:
{
  isRunning: true,
  lastSyncTime: 2026-02-21T14:25:45.123Z,
  syncCount: 5,
  errorCount: 0,
  successRate: "100%"
}
```

### Logs en Consola

```
✅ Iniciando sincronización periódica del carrito cada 30000ms
✅ Carrito sincronizado exitosamente (1)
✅ Carrito sincronizado exitosamente (2)
...
```

### Ver Requests al Backend

1. Abre DevTools (F12)
2. Tab **Network**
3. Filter: `/EnviarDatosAlCarrito`
4. Cada 30 segundos verás un POST ✅

---

## 🔍 Casos de Uso

### Caso 1: Usuario nuevo se autentica
```
1. Usuario hace login
2. Token se guarda en localStorage
3. Boot file lo detecta
4. Inicia periodicCartSyncService.start()
5. Comienza sync cada 30 segundos ✅
```

### Caso 2: Usuario agrega/quita producto
```
1. Usuario interactúa con carrito
2. Se guarda en localStorage
3. Cada 30 segundos:
   ├─ Servicio lee localStorage
   ├─ Envía datos actualizados al backend
   └─ Backend actualiza BD ✅
```

### Caso 3: Usuario se desconecta
```
1. Usuario hace logout
2. Token se elimina
3. Boot file lo detecta
4. Detiene periodicCartSyncService.stop()
5. Ya no sincroniza ✅
```

### Caso 4: Página se recarga
```
1. Usuario en sesión
2. Recarga página (F5)
3. App inicia, boot file detecta token
4. Reinicia sincronización automáticamente ✅
5. No hay pérdida de datos
```

---

## 📊 Estadísticas

El servicio mantiene estadísticas:

```javascript
const status = window.__periodicCartSyncService.getStatus()

console.log(status)
// {
//   isRunning: true,                    // ¿Está sincronizando?
//   lastSyncTime: Date,                 // Última sincronización
//   syncCount: 42,                      // Total de syncs
//   errorCount: 2,                      // Total de errores
//   successRate: "95.24%"               // Porcentaje éxito
// }
```

---

## ⚙️ Configuración Avanzada

### Cambiar intervalo en boot file

```javascript
// src/boot/periodicCartSync.js

// Línea 24, cambiar:
periodicCartSyncService.start(30000)

// A por ejemplo:
periodicCartSyncService.start(60000) // 1 minuto
```

### Desactivar completamente

```javascript
// En boot file, deshabilitar:
// periodicCartSyncService.start(30000)
// // periodicCartSyncService.start(30000)  ← Comentado
```

### Custom interval según usuario

```javascript
// En boot file, agregar lógica:

if (token) {
  const user = decodeToken(token)

  // VIP: cada 10 segundos
  if (user.isVIP) {
    periodicCartSyncService.start(10000)
  }
  // Admin: cada 5 segundos
  else if (user.isAdmin) {
    periodicCartSyncService.start(5000)
  }
  // Normal: cada 30 segundos
  else {
    periodicCartSyncService.start(30000)
  }
}
```

---

## 🚨 Manejo de Errores

### Automatizado

- ✅ Si falla, silenciosamente continúa
- ✅ Contador de errores incrementa
- ✅ Sin notificaciones molestas
- ✅ Log en consola para debugging

### Causas Comunes de Error

**1. Usuario no autenticado**
```
Causa: Token expirado o no existe
Resultado: Sincronización se salta
Solución: Usuario debe loguearse
```

**2. Backend no disponible**
```
Causa: Server está down
Resultado: Error capturado, continúa intentando
Solución: Backend vuelve a estar up
```

**3. Carrito vacío**
```
Causa: No hay items en localStorage
Resultado: No sincroniza (optimization)
Solución: Normal, reanuda cuando hay items
```

---

## 💡 Tips Importantes

✅ **No bloquea la aplicación**
- Sincronización es async
- UI siempre responsiva

✅ **Automático en todo**
- Inicia con autenticación
- Para con desautenticación
- Se reinicia con F5

✅ **Silencioso por defecto**
- Sin notificaciones al usuario
- Solo logs en consola dev

✅ **Data integrity**
- Si falla, carrito se guarda local igual
- No hay pérdida de datos

---

## 📱 Comportamiento en Diferentes Escenarios

### Escenario 1: Usuario Normal
```
- Abre app → Autentica → Sync comienza
- Agrega producto → Se guarda local + sync en 30s
- Quita producto → Se guarda local + sync en 30s
- Cierra tab → Desautentica → Sync se detiene
```

### Escenario 2: Usuario con Mala Conexión
```
- Usuario en WiFi lenta
- Sync toma 5 segundos
- Cada 30 segundos intenta de todos modos
- Si falla, contador de errores incrementa
- Si mejora conexión, reintenta
```

### Escenario 3: Admin con Múltiples Pestañas
```
- Abre admin en tab 1
- Abre tienda en tab 2
- Ambas sincronizando (independiente)
- Cada una mantiene su estat
- localStorage compartido pero operaciones independientes
```

---

## ✅ Checklist de Funcionamiento

- [ ] Boot file cargado (verificar en DevTools console)
- [ ] `window.__periodicCartSyncService` existe
- [ ] Estado muestra `isRunning: true`
- [ ] Logs muestran mensajes cada 30s
- [ ] Network tab muestra POSTs a `/EnviarDatosAlCarrito`
- [ ] Backend recibe requests
- [ ] Base de datos actualiza carrito
- [ ] Logout detiene sincronización
- [ ] Nuevo login reinicia sincronización
- [ ] F5 no interrumpe sincronización

---

## 🎯 Próximas Mejoras Opcionales

1. **Compresión de requests**
   - Solo enviar si carrito cambió desde último sync
   - Reducir carga del servidor

2. **Notificación visual**
   - Pequeño indicador de "sincronizando"
   - Con emoji 🔄

3. **Alertas de problemas**
   - Si >5 errores consecutivos
   - Notificar al usuario

4. **Sincronización bidireccional**
   - Backend actualiza precios → Frontend recibe
   - Producto ya no disponible → Se quita del carrito

5. **Analytics**
   - Cuándo se sincroniza
   - Cuál es la tasa de éxito
   - Usuarios activos

---

## 📞 Resumen

| Aspecto | Valor |
|--------|-------|
| Inicio automático | ✅ Sí |
| Intervalo | 30 segundos |
| Endpoint | `/EnviarDatosAlCarrito` |
| User ID | Del JWT (automático) |
| Manejo errores | Silencioso |
| Bloqueante | ✅ No |
| Data loss | ✅ No |

---

## 🎉 ¡Listo!

Tu carrito ahora se sincroniza automáticamente con el backend cada 30 segundos. 🚀

**No requiere cambios adicionales en componentes.** Todo funciona automáticamente en background.

### Debug:
```javascript
window.__periodicCartSyncService.getStatus()
```

### Ver logs:
```
Abre DevTools → Console → Filtra por "Carrito sincronizado"
```

---

**Status:** ✅ Implementado y Automático
**Activación:** Automática al autenticarse
**Desactivación:** Automática al desconectarse
**Fecha:** 21 de Febrero, 2026
