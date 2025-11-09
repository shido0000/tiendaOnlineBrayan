import { Success } from 'src/boot/notify'
import { reactive, computed, watch } from 'vue'

const STORAGE_KEY = 'fashion_wishlist_v1'

function loadFromStorage() {
    try {
        const raw = localStorage.getItem(STORAGE_KEY)
        if (!raw) return []
        return JSON.parse(raw)
    } catch (e) {
        console.warn('wishlistStore: failed to parse localStorage', e)
        return []
    }
}

const state = reactive({
    items: loadFromStorage()
})

watch(() => state.items, (v) => {
    try { localStorage.setItem(STORAGE_KEY, JSON.stringify(v)) } catch (e) { console.warn(e) }
}, { deep: true })

function findIndexById(id) {
    return state.items.findIndex(i => String(i.id) === String(id))
}

function add(product) {
    if (!product) return
    const id = product.id ?? product.productoId ?? product.productId
    if (!id) return
    const idx = findIndexById(id)
    if (idx !== -1) return // already in wishlist
    const toAdd = {
        id,
        nombre: product.nombre || product.descripcion || '',
        precioVenta: product.precioVenta ?? product.precio ?? 0,
        foto: (product.fotos && product.fotos[0]) || product.imagen || product.fotoUrl || null,
        raw: product
    }
    state.items.push(toAdd)
    Success("Producto agregado a la lista de deseos")
}

function remove(id) {
    const idx = findIndexById(id)
    if (idx !== -1) state.items.splice(idx, 1)
        Success("Producto eliminado a la lista de deseos")
}

function toggle(product) {
    if (!product) return
    const id = product.id ?? product.productoId ?? product.productId
    if (!id) return
    const idx = findIndexById(id)
    if (idx === -1) add(product)
    else remove(id)
}

function isFavorito(id) {
    return findIndexById(id) !== -1
}

const count = computed(() => state.items.length)

export function useWishlist() {
    return {
        items: state.items,
        add,
        remove,
        toggle,
        isFavorito,
        count,
        state
    }
}

export default useWishlist
