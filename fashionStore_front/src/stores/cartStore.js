import { Success } from 'src/boot/notify'
import { reactive, computed, watch } from 'vue'

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

    // 🔑 Preferimos el id de la variante seleccionada
    const variantId = product.selectedVariant?.id
    const id = variantId ?? product.id ?? product.productoId ?? product.productId
    if (!id) return

    const idx = findIndexById(id)
    if (idx !== -1) {
        state.items[idx].cantidad = (state.items[idx].cantidad || 1) + qty
    } else {
        const toAdd = {
            id, // ahora es el id de la variante
            nombre: product.nombre || product.descripcion || '',
            precioVenta: product.selectedVariant?.precioVenta ?? product.precioVenta ?? product.precio ?? 0,
            cantidad: qty,
            foto: (product.selectedVariant?.fotos?.[0]?.url) || (product.fotos && product.fotos[0]) || product.imagen || product.fotoUrl || null,
            talla: product.selectedVariant?.talla || null,
            color: product.selectedVariant?.color || null,
            raw: product
        }
        state.items.push(toAdd)
    }

    state.lastAddedAt = Date.now()
    Success("Producto agregado al Carrito")
    this.$q.notify({ type: 'positive', message: 'Producto agregado al Carrito' })
}

function removeItem(id) {
    const idx = findIndexById(id)
    if (idx !== -1) state.items.splice(idx, 1)
    Success("Producto eliminado del Carrito")
    this.$q.notify({ type: 'positive', message: 'Producto eliminado del Carrito' })
}

function updateQuantity(id, cantidad) {
    const idx = findIndexById(id)
    if (idx !== -1) state.items[idx].cantidad = cantidad
}

const totalCount = computed(() => state.items.reduce((s, i) => s + (i.cantidad || 0), 0))
const totalPrice = computed(() => state.items.reduce((s, i) => s + ((i.precioVenta || 0) * (i.cantidad || 0)), 0))

export function useCart() {
    return {
        items: state.items,
        lastAddedAt: state.lastAddedAt,
        addItem,
        removeItem,
        updateQuantity,
        totalCount,
        totalPrice,
        state
    }
}

export default useCart
