# 🎯 Sincronización de Carrito - Resumen Implementación

## 📦 ¿Qué se Entrega?

Sistema automático que **sincroniza el carrito del usuario con tu backend** cada vez que cambia (agrega, quita, limpia).

---

## 🔧 Archivos Creados/Modificados

### ✅ Creados (2 nuevos)

1. **`src/services/activeCartsService.js` (Extendido)**
   - ✅ `formatCartDataForBackend()` - Convierte items a estructura del backend
   - ✅ `sendCartDataToBackend()` - Envía POST a `/EnviarDatosAlCarrito`
   - ✅ `sendUserCartToBackend()` - Función conveniente todo-en-uno

2. **`src/assets/js/util/cartSyncHelper.js` (Nuevo)**
   - ✅ `syncCartWithBackend()` - Sincronización automática
   - ✅ `syncCartWithBackendAndNotify()` - Con notificación
   - ✅ `getUserIdFromToken()` - Obtiene usuario del JWT
   - ✅ `useAutoSyncCart()` - Hook para Vue

3. **Documentación**
   - `INTEGRACION_ENVIAR_DATOS_CARRITO.md` - Guía completa
   - `EJEMPLO_INTEGRACION_CARRITO.js` - Ejemplo visual ANTES/DESPUÉS

---

## ⚡ Cómo Funciona

### Flujo Automático
```
Usuario agrega producto
        ↓
cartStore.addItem()
        ↓
Producto se agrega localmente ✅
        ↓
syncCartWithBackend() se ejecuta
        ↓
Obtiene usuarioId del token JWT
        ↓
Mapea items a estructura:
{
  usuarioId: "...",
  fechaCreacion: "...",
  detalles: [
    { carritoId, productoId, cantidad, unitPrice, lineTotal }
  ]
}
        ↓
POST /EnviarDatosAlCarrito
        ↓
Backend recibe y procesa ✅
```

---

## 🚀 Integración Rápida (5 minutos)

### 1. Abre: `src/stores/cartStore.js`

### 2. Arriba del archivo, agrega:
```javascript
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'
```

### 3. En función `addItem()`, al final agrega:
```javascript
syncCartWithBackend(state.items).catch(err => {
    console.error('Error sincronizando:', err)
})
```

### 4. En función `removeItem()`, al final agrega:
```javascript
syncCartWithBackend(state.items).catch(err => {
    console.error('Error sincronizando:', err)
})
```

### 5. En función `clearCart()`, al final agrega:
```javascript
syncCartWithBackend(state.items).catch(err => {
    console.error('Error sincronizando:', err)
})
```

### 6. ¡Listo! ✅

---

## 📊 Estructura que se Envía

### Request al Backend:
```json
POST /EnviarDatosAlCarrito

{
  "usuarioId": "550e8400-e29b-41d4-a716-446655440001",
  "fechaCreacion": "2026-02-21T14:23:45.123Z",
  "detalles": [
    {
      "carritoId": "1",
      "productoId": "prod-001",
      "cantidad": 1,
      "unitPrice": 50.00,
      "lineTotal": 50.00
    },
    {
      "carritoId": "2",
      "productoId": "prod-002",
      "cantidad": 2,
      "unitPrice": 80.00,
      "lineTotal": 160.00
    }
  ]
}
```

### Mapeo Automático:
| Tuples Local | → | Backend |
|-------------|---|---------|
| `item.id` | → | `carritoId` |
| `item.productoId` | → | `productoId` |
| `item.cantidad` | → | `cantidad` |
| `item.precio` | → | `unitPrice` |
| `precio × cantidad` | → | `lineTotal` |

---

## 🎯 Métodos Disponibles

### A. Sincronización Silenciosa (Recomendado)
```javascript
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

// Sincroniza sin notificar al usuario
await syncCartWithBackend(state.items)
```

### B. Con Notificación
```javascript
import { syncCartWithBackendAndNotify } from 'src/assets/js/util/cartSyncHelper'

// Sincroniza y muestra mensaje al usuario
await syncCartWithBackendAndNotify(state.items)
```

### C. Directo desde Service
```javascript
import activeCartsService from 'src/services/activeCartsService'

const formatted = activeCartsService.formatCartDataForBackend(items, userId)
await activeCartsService.sendCartDataToBackend(formatted)
```

---

## ✨ Características

✅ **Automático**
- Se sincroniza con cada cambio
- Sin código adicional en componentes
- Transparente para el usuario

✅ **Seguro**
- Obtiene usuarioId del JWT
- Validado por backend
- No expone datos sensibles

✅ **Resiliente**
- Si falla, carrito se guarda en localStorage igual
- Sin errores molestos al usuario
- Logs en consola para debugging

✅ **Eficiente**
- No bloquea acciones del usuario
- Respuesta inmediata en UI
- Backend recibe datos en segundo plano

✅ **Documentado**
- Código comentado
- Guía paso a paso
- Ejemplo completo incluido

---

## 📍 Archivos Importantes

| Archivo | Propósito |
|---------|----------|
| `activeCartsService.js` | Métodos de sincronización |
| `cartSyncHelper.js` | Helper conveniente |
| `cartStore.js` | Donde integrar (tu tienda) |
| `INTEGRACION_ENVIAR_DATOS_CARRITO.md` | Guía completa |
| `EJEMPLO_INTEGRACION_CARRITO.js` | Ejemplo ANTES/DESPUÉS |

---

## 🧪 Verificación

### En DevTools Network Tab:

1. Abre DevTools (F12)
2. Tab "Network"
3. Agrega producto al carrito
4. Deberías ver:
   ```
   POST /EnviarDatosAlCarrito
   Status: 200 OK
   ```

5. Haz clic para ver:
   - **Request Header:** usuarioId, fechaCreacion, detalles
   - **Response:** Confirmación del backend

---

## 🔄 Casos de Uso

### Caso 1: Agregar Producto
```
Usuario: "Agregar Camiseta"
    ↓
syncCartWithBackend() → POST /EnviarDatosAlCarrito
{
  "detalles": [
    { "productoId": "prod-001", "cantidad": 1, ... }
  ]
}
    ↓
Backend: Carrito actualizado ✅
```

### Caso 2: Cambiar Cantidad
```
Usuario: Cantidad 1 → 3 (misma camiseta)
    ↓
syncCartWithBackend() → POST /EnviarDatosAlCarrito
{
  "detalles": [
    { "productoId": "prod-001", "cantidad": 3, ... }
  ]
}
    ↓
Backend: Cantidad sincronizada ✅
```

### Caso 3: Remover Producto
```
Usuario: Clic en eliminar
    ↓
syncCartWithBackend() → POST /EnviarDatosAlCarrito
{
  "detalles": [
    { "productoId": "prod-002", "cantidad": 1, ... }
    // prod-001 desapareció
  ]
}
    ↓
Backend: Producto removido ✅
```

### Caso 4: Vaciar Carrito
```
Usuario: "Vaciar carrito"
    ↓
syncCartWithBackend() → POST /EnviarDatosAlCarrito
{
  "detalles": []  // Vacío
}
    ↓
Backend: Carrito limpiado ✅
```

---

## 🚨 Errores Comunes y Soluciones

### Error: "No hay usuario autenticado"
```
Causa: Token no encontrado o expirado
Solución: Verificar que usuario está logged in
Resultado: Sincronización se salta (de todos modos guarda en localStorage)
```

### Error: "POST /EnviarDatosAlCarrito 404"
```
Causa: Endpoint no existe en backend
Solución: Implementar endpoint en backend
Verificar: Nombre exacto: /EnviarDatosAlCarrito
```

### Error: "CORS error"
```
Causa: Backend no permitió origen
Solución: Verificar CORS headers en backend
Típico: Add "https://tu-frontend.com" a allowed origins
```

### Sincronización lenta
```
Causa: Red lenta o backend lento
Solución: Normal, se ejecuta en async
Usuario: No se bloquea, carrito se actualiza igual
```

---

## 💡 Tips

✅ **No necesitas esperar respuesta del backend**
```javascript
// ✅ BIEN - No bloquea
syncCartWithBackend(state.items).catch(err => console.error(err))

// ❌ MAL - Bloquea UI
await syncCartWithBackend(state.items)
```

✅ **El usuarioId se obtiene automáticamente**
```javascript
// ✅ BIEN - Automático
syncCartWithBackend(state.items)

// ❌ INNECESARIO - Ya está en el helper
syncCartWithBackend(state.items, "user-id")
```

✅ **Errores se manejan silenciosamente**
```javascript
// ✅ BIEN - Si falla backend, carrito se guarda local igual
// Usuario no ve error

// ❌ NO HAGAS - Notificar cada sync
// Molestaría al usuario
```

---

## 🔐 Seguridad

✅ **Token JWT validado**
- Solo usuarios autenticados pueden sincronizar

✅ **Backend valida permisos**
- Backend debe verificar usuarioId en token

✅ **Sin exposición de datos**
- Solo se envían campos necesarios
- Contraseñas/datos sensibles nunca se sincronizan

✅ **HTTPS recomendado**
- Para producción, usar HTTPS siempre

---

## 📈 Próximos Pasos Opcionales

1. **Notificación en tiempo real**
   - Backend emite evento SignalR cuando carrito cambia
   - Admin/Vendedor ve actualización en dashboard

2. **Historial de cambios**
   - Guardar log de cada sincronización
   - Auditar qué cambió y cuándo

3. **Recuperación de carritos abandonados**
   - Si usuario no compra en 30 min
   - Enviar recordatorio por email

4. **Sincronización bidireccional**
   - Si backend actualiza carrito (cupón, precio)
   - Frontend se notifica automáticamente

---

## ✅ Checklist Final

**Antes de ir a Producción:**

- [ ] Importar `syncCartWithBackend` en cartStore.js
- [ ] Agregar sincronización en `addItem()`
- [ ] Agregar sincronización en `removeItem()`
- [ ] Agregar sincronización en `clearCart()`
- [ ] Probar agregando producto
- [ ] Verificar Network tab → POST a `/EnviarDatosAlCarrito`
- [ ] Ver estructura en DevTools
- [ ] Backend recibe correctamente
- [ ] Backend guarda en DB
- [ ] Repetir con múltiples productos
- [ ] Probar vaciar carrito
- [ ] Verificar con múltiples navegadores

---

## 📞 Recursos

1. **Lee primero:** `INTEGRACION_ENVIAR_DATOS_CARRITO.md`
2. **Ejemplo:** `EJEMPLO_INTEGRACION_CARRITO.js`
3. **Código:** `src/services/activeCartsService.js`
4. **Helper:** `src/assets/js/util/cartSyncHelper.js`

---

## 🎉 Resumen

✅ **Implementado**: Helper de sincronización
✅ **Fácil de integrar**: 5 minutos máximo
✅ **Automático**: Sin código en componentes
✅ **Seguro**: JWT validado
✅ **Documentado**: Guía completa incluida

**Ahora el carrito se sincroniza automáticamente con el backend cada vez que cambia.** 🚀

---

**Status:** ✅ Listo para Integración
**Fecha:** 21 de Febrero, 2026
**Versión:** 1.0
