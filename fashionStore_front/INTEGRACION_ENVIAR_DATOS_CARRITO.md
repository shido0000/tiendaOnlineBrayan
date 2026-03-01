# 🔄 Integración de EnviarDatosAlCarrito

## Resumen

Tu backend tiene un endpoint llamado `EnviarDatosAlCarrito` que espera datos en este formato:

```json
{
  "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "fechaCreacion": "2026-02-21T14:11:22.722Z",
  "detalles": [
    {
      "carritoId": "id-del-item",
      "productoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cantidad": 1,
      "unitPrice": 50.00,
      "lineTotal": 50.00
    }
  ]
}
```

## ✅ Ya Implementado

### 1. Servicio Actualizado: `activeCartsService.js`

✅ Método `formatCartDataForBackend(cartData, usuarioId)`
- Convierte array de items a la estructura esperada

✅ Método `sendCartDataToBackend(cartData)`
- Envía a endpoint `/EnviarDatosAlCarrito`

✅ Método `sendUserCartToBackend(usuarioId, cartItems)`
- Combina ambas operaciones en una función conveniente

### 2. Helper: `cartSyncHelper.js`

✅ Función `syncCartWithBackend(cartItems, usuarioId)`
- Sincroniza carrito automáticamente
- Obtiene usuarioId del token si no se proporciona
- Manejo de errores silencioso

✅ Función `syncCartWithBackendAndNotify(cartItems, usuarioId)`
- Versión con notificaciones al usuario

✅ Hook `useAutoSyncCart(cartItems)`
- Sincronización automática con watchEffect

---

## 📍 Cómo Usar

### Opción 1: Sincronización Manual (Recomendado)

En tu `stores/cartStore.js`, importa y usa después de modificar:

```javascript
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

// En tu función addItem:
export function addItem(product, qty = 1) {
  // ... tu código existente ...

  // Después de agregar el producto:
  state.items[idx].cantidad = ...

  // Sincronizar con backend
  syncCartWithBackend(state.items)
}

// En tu función removeItem:
export function removeItem(id) {
  // ... tu código existente ...

  // Después de remover:
  state.items.splice(idx, 1)

  // Sincronizar con backend
  syncCartWithBackend(state.items)
}

// En tu función clear:
export function clearCart() {
  state.items = []

  // Sincronizar (carrito vacío)
  syncCartWithBackend(state.items)
}
```

### Opción 2: Sincronización Automática

En cualquier componente que modifique el carrito:

```vue
<script setup>
import { useCartStore } from 'src/stores/cartStore'
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'
import { watch } from 'vue'

const cartStore = useCartStore()

// Sincronizar automáticamente cuando cambie el carrito
watch(
  () => cartStore.items,
  async (newItems) => {
    if (newItems.length > 0 || newItems.length === 0) {
      await syncCartWithBackend(newItems)
    }
  },
  { deep: true }
)
</script>
```

### Opción 3: Con Notificaciones

Cuando quieras que el usuario vea confirmación:

```javascript
import { syncCartWithBackendAndNotify } from 'src/assets/js/util/cartSyncHelper'

// Cuando el usuario completa una acción
await syncCartWithBackendAndNotify(state.items)
```

---

## 🔍 Cómo Funciona Internamente

```
Usuario agrega producto
    ↓
cartStore.addItem()
    ↓
syncCartWithBackend(state.items)
    ↓
cartSyncHelper.js mapea datos:
  - Obtiene usuarioId del token JWT
  - Convierte items a estructura backend
    ↓
activeCartsService.sendUserCartToBackend()
    ↓
POST /EnviarDatosAlCarrito
{
  usuarioId: "del-token",
  fechaCreacion: "ahora",
  detalles: [
    {
      carritoId: "item-id",
      productoId: "prod-id",
      cantidad: 1,
      unitPrice: 50.00,
      lineTotal: 50.00
    }
  ]
}
    ↓
Backend recibe y procesa ✅
```

---

## 🧠 Estructura de Mapeo

### From (Tu carrito local - cartStore.js):
```javascript
{
  id: "1",
  productoId: "prod-001",
  nombre: "Camiseta",
  precio: 50.00,
  cantidad: 1,
  foto: "...",
  variante: "L"
}
```

### To (Lo que espera el backend):
```javascript
{
  carritoId: "1",        // De: item.id
  productoId: "prod-001", // De: item.productoId
  cantidad: 1,            // De: item.cantidad
  unitPrice: 50.00,       // De: item.precio
  lineTotal: 50.00        // Calculado: precio * cantidad
}
```

---

## 📋 Ejemplo Completo: Integración en cartStore.js

```javascript
// En la parte superior del archivo
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

// Modificar la función addItem existente:
function addItem(product, qty = 1) {
    if (!product) return

    let id = null
    if (product.selectedVariant?.id) {
        id = product.selectedVariant.id
    } else if (product.varianteId) {
        id = product.varianteId
    } else {
        id = product.id ?? product.productoId ?? product.productId
    }

    if (!id) return

    const idx = findIndexById(id)
    if (idx !== -1) {
        state.items[idx].cantidad = (state.items[idx].cantidad || 1) + qty
    } else {
        // ... tu código de agregar item ...
        state.items.push({...})
    }

    // ✅ AGREGADO: Sincronizar con backend
    syncCartWithBackend(state.items).catch(err => {
        console.error('Error sincronizando carrito:', err)
    })
}

// Modificar removeItem:
function removeItem(id) {
    const idx = findIndexById(id)
    if (idx !== -1) {
        state.items.splice(idx, 1)
    }

    // ✅ AGREGADO: Sincronizar con backend
    syncCartWithBackend(state.items).catch(err => {
        console.error('Error sincronizando carrito:', err)
    })
}

// Modificar clearCart:
function clearCart() {
    state.items = []

    // ✅ AGREGADO: Sincronizar con backend (vacío)
    syncCartWithBackend(state.items).catch(err => {
        console.error('Error sincronizando carrito:', err)
    })
}
```

---

## 🚀 Formas de Enviar Datos

### Opción A: Directo desde Service (Más simple)
```javascript
import activeCartsService from 'src/services/activeCartsService'

const data = {
  usuarioId: "abc-123",
  fechaCreacion: "2026-02-21T14:11:22.722Z",
  detalles: [...]
}

await activeCartsService.sendCartDataToBackend(data)
```

### Opción B: Desde Helper (Recomendado)
```javascript
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

const cartItems = [...] // array desde store
await syncCartWithBackend(cartItems) // Mapea automáticamente
```

### Opción C: Manual con API
```javascript
import { api } from 'src/boot/axios'

const cartData = {
  usuarioId: "abc-123",
  fechaCreacion: new Date().toISOString(),
  detalles: cartItems.map(item => ({
    carritoId: item.id,
    productoId: item.productoId,
    cantidad: item.cantidad,
    unitPrice: item.precio,
    lineTotal: item.precio * item.cantidad
  }))
}

await api.post('/EnviarDatosAlCarrito', cartData)
```

---

## ⚙️ Variables que se Obtienen Automáticamente

### Usuario ID (usuarioId)
- Se obtiene del token JWT en localStorage/sessionStorage
- No necesitas pasarlo manualmente

### Fecha de Creación (fechaCreacion)
- Se genera automáticamente con `new Date().toISOString()`
- Formato: "2026-02-21T14:11:22.722Z"

---

## 🔒 Seguridad

✅ Token JWT se valida automáticamente
✅ Solo se envía si usuario está autenticado
✅ Errores se capturan sin exponer detalles
✅ No hay datos sensibles expuestos

---

## 🧪 Testing

### Test Local sin Backend

```javascript
// Simular la solicitud
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

const mockCartItems = [
  {
    id: "1",
    productoId: "prod-001",
    nombre: "Camiseta",
    precio: 50,
    cantidad: 2
  },
  {
    id: "2",
    productoId: "prod-002",
    nombre: "Pantalón",
    precio: 80,
    cantidad: 1
  }
]

// Ver qué estructura se enviaría
const formatted = activeCartsService.formatCartDataForBackend(mockCartItems, "user-123")
console.log(JSON.stringify(formatted, null, 2))

// Ver en consola:
// {
//   "usuarioId": "user-123",
//   "fechaCreacion": "2026-02-21T14:11:22.722Z",
//   "detalles": [
//     { "carritoId": "1", "productoId": "prod-001", "cantidad": 2, "unitPrice": 50, "lineTotal": 100 },
//     { "carritoId": "2", "productoId": "prod-002", "cantidad": 1, "unitPrice": 80, "lineTotal": 80 }
//   ]
// }
```

---

## 📞 Métodos Disponibles

| Método | Ubicación | Uso |
|--------|-----------|-----|
| `syncCartWithBackend()` | cartSyncHelper.js | Sincronizar silenciosamente |
| `syncCartWithBackendAndNotify()` | cartSyncHelper.js | Con notificación al usuario |
| `sendUserCartToBackend()` | activeCartsService.js | Más control |
| `sendCartDataToBackend()` | activeCartsService.js | Envío directo |
| `formatCartDataForBackend()` | activeCartsService.js | Solo formatear |

---

## ✨ Flujo Recomendado

```
Usuario abre tienda
    ↓
Carga carrito guardado (localStorage)
    ↓
Usuario agrega producto
    ↓
cartStore.addItem() → syncCartWithBackend()
    ↓
Backend recibe en EnviarDatosAlCarrito ✅
    ↓
Usuario quita producto
    ↓
cartStore.removeItem() → syncCartWithBackend()
    ↓
Backend recibe actualización ✅
    ↓
Usuario vacía carrito
    ↓
cartStore.clearCart() → syncCartWithBackend()
    ↓
Backend recibe carrito vacío ✅
```

---

## 🎯 Quick Integration (5 minutos)

1. **Abre:** `src/stores/cartStore.js`

2. **Arriba del archivo, agrega:**
   ```javascript
   import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'
   ```

3. **Busca función `addItem` y al final agrega:**
   ```javascript
   syncCartWithBackend(state.items)
   ```

4. **Busca función `removeItem` y al final agrega:**
   ```javascript
   syncCartWithBackend(state.items)
   ```

5. **Busca función `clearCart` y al final agrega:**
   ```javascript
   syncCartWithBackend(state.items)
   ```

6. **¡Listo!** Ahora cada cambio se sincroniza automáticamente

---

## 📊 Ejemplo de Request que se Envía

### Cuando usuario agrega 1 camiseta de $50:
```bash
POST /EnviarDatosAlCarrito HTTP/1.1
Host: tu-backend.com
Authorization: Bearer eyJhbGc...
Content-Type: application/json

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
    }
  ]
}
```

---

## 🐛 Debugging

### Ver qué se está enviando:

```javascript
// En cartSyncHelper.js, descomentar en syncCartWithBackend:
console.log('📤 Enviando datos:', formattedData)

// O agregar en tu componente:
import activeCartsService from 'src/services/activeCartsService'

const formatted = activeCartsService.formatCartDataForBackend(cartItems, userId)
console.log('Estructura a enviar:', formatted)
```

---

## ✅ Checklist de Integración

- [ ] Importé `syncCartWithBackend` en `cartStore.js`
- [ ] Agregué sincronización en `addItem()`
- [ ] Agregué sincronización en `removeItem()`
- [ ] Agregué sincronización en `clearCart()`
- [ ] Probé añadiendo un producto
- [ ] Verifico en Network tab de DevTools
- [ ] POST a `/EnviarDatosAlCarrito` aparece
- [ ] Estructura de datos es correcta
- [ ] Backend recibe la solicitud ✅

---

¡Tu carrito ahora se sincroniza automáticamente con el backend! 🎉
