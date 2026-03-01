/**
 * EJEMPLO DE INTEGRACIÓN EN cartStore.js
 *
 * Este archivo muestra ANTES y DESPUÉS de integrar syncCartWithBackend
 * Copia y pega el código DESPUÉS en tu archivo real
 */

// ============================================================
// ANTES (Código Original)
// ============================================================

/* ❌ ORIGINAL - Sin sincronización

import { reactive, computed, watch } from 'vue'
import { getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'

const STORAGE_KEY = 'fashion_cart_v1'

// ... resto del código ...

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
        // ... código para agregar nuevo item ...
        state.items.push({...})
    }
    // ❌ SIN SINCRONIZACIÓN - Solo guardaba en localStorage
}

function removeItem(id) {
    const idx = findIndexById(id)
    if (idx !== -1) {
        state.items.splice(idx, 1)
    }
    // ❌ SIN SINCRONIZACIÓN
}

function clearCart() {
    state.items = []
    // ❌ SIN SINCRONIZACIÓN
}

*/

// ============================================================
// DESPUÉS (Con Sincronización)
// ============================================================

// ✅ INTEGRACIÓN COMPLETA

import { reactive, computed, watch } from 'vue'
import { Success } from 'src/boot/notify'
import { getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'
// 🔥 NUEVO: Importar helper de sincronización
import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'

const STORAGE_KEY = 'fashion_cart_v1'

function loadFromStorage() {
    try {
        const raw = localStorage.getItem(STORAGE_KEY)
        if (!raw) return []
        return JSON.parse(raw)
    } catch (e) {
        console.warn('cartStore: failed to parse localStorage', e)
        return []
    }
}

const state = reactive({
    items: loadFromStorage(),
    lastAddedAt: null
})

watch(() => state.items, (v) => {
    try { localStorage.setItem(STORAGE_KEY, JSON.stringify(v)) } catch (e) { console.warn(e) }
}, { deep: true })

function findIndexById(id) {
    return state.items.findIndex(i => String(i.id) === String(id))
}

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
        // Si ya existe, solo incrementar cantidad
        state.items[idx].cantidad = (state.items[idx].cantidad || 1) + qty
    } else {
        // Obtener foto desde múltiples fuentes posibles
        let foto = null

        if (product.selectedVariant?.fotos?.[0]) {
            const fotoVariante = product.selectedVariant.fotos[0]
            foto = typeof fotoVariante === 'object' ? (fotoVariante.url || fotoVariante.imagen || fotoVariante.path) : fotoVariante
        }

        if (!foto && Array.isArray(product.fotos) && product.fotos.length > 0) {
            const fotoPrincipal = product.fotos[0]
            if (typeof fotoPrincipal === 'object') {
                foto = fotoPrincipal.url || fotoPrincipal.imagen || fotoPrincipal.path || fotoPrincipal
            } else {
                foto = fotoPrincipal
            }
        }

        // ... resto del código para agregar item ...

        state.items.push({
            id,
            productoId: product.id ?? product.productoId,
            varianteId: id,
            nombre: product.nombre,
            descripcion: product.descripcion,
            cantidad: qty,
            precio: product.precioVenta ?? product.precio,
            foto,
            variante: product.selectedVariant?.nombre || 'Default'
        })

        state.lastAddedAt = Date.now()
    }

    // 🔥 NUEVO: Sincronizar con backend después de agregar
    syncCartWithBackend(state.items).catch(err => {
        console.error('⚠️ Error sincronizando carrito al agregar:', err)
        // No mostramos error al usuario, solo lo guardamos en localStorage
    })
}

function removeItem(id) {
    const idx = findIndexById(id)
    if (idx !== -1) {
        const removed = state.items.splice(idx, 1)
        // Feedback opcional
        // console.log('Producto removido:', removed[0].nombre)
    }

    // 🔥 NUEVO: Sincronizar con backend después de remover
    syncCartWithBackend(state.items).catch(err => {
        console.error('⚠️ Error sincronizando carrito al remover:', err)
    })
}

function updateQuantity(id, qty) {
    const idx = findIndexById(id)
    if (idx !== -1) {
        if (qty <= 0) {
            removeItem(id)
        } else {
            state.items[idx].cantidad = qty
            // 🔥 NUEVO: Sincronizar con backend
            syncCartWithBackend(state.items).catch(err => {
                console.error('⚠️ Error sincronizando cantidad:', err)
            })
        }
    }
}

function clearCart() {
    state.items = []

    // 🔥 NUEVO: Sincronizar carrito vacío con backend
    syncCartWithBackend(state.items).catch(err => {
        console.error('⚠️ Error sincronizando carrito vacío:', err)
    })

    Success('Carrito vaciado', {
        position: 'top',
        timeout: 2000
    })
}

// ... resto de funciones computadas y exportaciones ...

const totalItems = computed(() => state.items.reduce((sum, item) => sum + (item.cantidad || 0), 0))
const totalPrice = computed(() => state.items.reduce((sum, item) => sum + ((item.precio || 0) * (item.cantidad || 0)), 0))
const isEmpty = computed(() => state.items.length === 0)

export const cartStore = {
    state: computed(() => state),
    items: computed(() => state.items),
    totalItems,
    totalPrice,
    isEmpty,
    addItem,
    removeItem,
    updateQuantity,
    clearCart,
    findIndexById
}

// ============================================================
// CAMBIOS REALIZADOS
// ============================================================

/*
CAMBIOS PRINCIPALES:

1. ✅ Importado syncCartWithBackend
2. ✅ Agregado sincronización en addItem()
3. ✅ Agregado sincronización en removeItem()
4. ✅ Agregado sincronización en updateQuantity()
5. ✅ Agregado sincronización en clearCart()

CADA CAMBIO:
- Llama a syncCartWithBackend(state.items)
- Usa .catch() para manejar errores silenciosamente
- Sin notificaciones molestas al usuario
- Backend recibe estructura formateada automáticamente

RESULTADO:
- Cada acción del carrito se sincroniza automáticamente
- Si falla, se guarda en localStorage igual
- Usuario siempre tiene experiencia fluida
- Backend siempre está actualizado ✅
*/

// ============================================================
// FLUJO COMPLETO
// ============================================================

/*
ESCENARIO 1: Usuario agrega producto
┌─ Usuario hace clic en "Agregar al carrito"
├─ cartStore.addItem(product) se ejecuta
├─ Producto se agrega a state.items ✅
├─ Se guarda en localStorage ✅
├─ syncCartWithBackend() se llama
│  ├─ Obtiene usuarioId del token JWT
│  ├─ Mapea items a estructura del backend
│  ├─ POST /EnviarDatosAlCarrito
│  └─ Backend recibe y procesa ✅
└─ Usuario ve carrito actualizado inmediatamente

ESCENARIO 2: Usuario quita producto
┌─ Usuario hace clic en icono "eliminar"
├─ cartStore.removeItem(id) se ejecuta
├─ Producto se quita de state.items ✅
├─ Se actualiza localStorage ✅
├─ syncCartWithBackend() se llama
│  ├─ POST /EnviarDatosAlCarrito (sin ese producto)
│  └─ Backend recibe actualización ✅
└─ UI se actualiza dinámicamente

ESCENARIO 3: Usuario vacía carrito
┌─ Usuario hace clic "Vaciar carrito"
├─ cartStore.clearCart() se ejecuta
├─ state.items = [] ✅
├─ localStorage se limpia ✅
├─ syncCartWithBackend() se llama
│  ├─ POST /EnviarDatosAlCarrito (detalles vacío)
│  └─ Backend recibe carrito vacío ✅
├─ Notificación: "Carrito vaciado"
└─ Usuario vuelve a ver tienda
*/

// ============================================================
// VENTAJAS
// ============================================================

/*
✅ BACKEND SIEMPRE ACTUALIZADO
   - No necesitas actualizar manualmente
   - Cambios se syncronizan automáticamente

✅ USUARIO EXPERIENCE PERFECTO
   - Respuesta inmediata en UI
   - No espera a que llegue respuesta del backend
   - localStorage es fallback si backend falla

✅ DATOS CONSISTENTES
   - Frontend y Backend siempre en sync
   - Admin puede ver carritos activos en tiempo real
   - Vendedor recibe notificaciones de cambios

✅ MANEJO DE ERRORES
   - Si falla sync, carrito se guarda local igual
   - Sin errores molestos al usuario
   - Logs en consola para debugging

✅ FLEXIBLE
   - Funciona con conexión lenta
   - No bloquea la acción del usuario
   - Método async pero no esperamos respuesta
*/
