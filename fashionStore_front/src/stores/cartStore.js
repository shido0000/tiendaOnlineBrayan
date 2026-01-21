import { Success } from 'src/boot/notify'
import { reactive, computed, watch } from 'vue'
import { getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'

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

    // 🔑 Usar un identificador consistente:
    // 1. Si hay una variante seleccionada, usar su ID
    // 2. Si existe varianteId (normalizado), usar ese
    // 3. Si no, usar el ID del producto principal
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

        // Prioridad 1: Foto de la variante seleccionada
        if (product.selectedVariant?.fotos?.[0]) {
            const fotoVariante = product.selectedVariant.fotos[0]
            foto = typeof fotoVariante === 'object' ? (fotoVariante.url || fotoVariante.imagen || fotoVariante.path) : fotoVariante
        }

        // Prioridad 2: Fotos del producto principal
        if (!foto && Array.isArray(product.fotos) && product.fotos.length > 0) {
            const fotoPrincipal = product.fotos[0]
            if (typeof fotoPrincipal === 'object') {
                foto = fotoPrincipal.url || fotoPrincipal.imagen || fotoPrincipal.path || fotoPrincipal
            } else {
                foto = fotoPrincipal
            }
        }

        // Prioridad 3: Fotos de la primera variante (si existe) - con herencia
        if (!foto && Array.isArray(product.variants) && product.variants.length > 0) {
            const primerVariante = product.variants[0]
            // Intentar obtener foto directa de la variante
            if (Array.isArray(primerVariante.fotos) && primerVariante.fotos.length > 0) {
                const fotoVar = primerVariante.fotos[0]
                foto = typeof fotoVar === 'object' ? (fotoVar.url || fotoVar.imagen || fotoVar.path) : fotoVar
            } else {
                // Si la variante no tiene foto, buscar en variantes hermanas
                const fotoHeredada = getFotoFromVarianteWithFallback(primerVariante, product)
                if (fotoHeredada) {
                    foto = typeof fotoHeredada === 'object' ? (fotoHeredada.url || fotoHeredada.imagen || fotoHeredada.path) : fotoHeredada
                }
            }
        }

        // Prioridad 4: Fotos de productosVariantes (estructura del API)
        if (!foto && Array.isArray(product.productosVariantes) && product.productosVariantes.length > 0) {
            const primerVariante = product.productosVariantes[0]
            // Intentar obtener foto directa de la variante
            if (Array.isArray(primerVariante.fotos) && primerVariante.fotos.length > 0) {
                const fotoVar = primerVariante.fotos[0]
                foto = typeof fotoVar === 'object' ? (fotoVar.url || fotoVar.imagen || fotoVar.path) : fotoVar
            } else {
                // Si la variante no tiene foto, buscar en variantes hermanas
                const fotoHeredada = getFotoFromVarianteWithFallback(primerVariante, product)
                if (fotoHeredada) {
                    foto = typeof fotoHeredada === 'object' ? (fotoHeredada.url || fotoHeredada.imagen || fotoHeredada.path) : fotoHeredada
                }
            }
        }

        // Prioridad 5: Propiedades directas de foto
        if (!foto && (product.imagen || product.fotoUrl)) {
            foto = product.imagen || product.fotoUrl
        }
        const toAdd = {
            id, // ID consistente (variante o producto)
            tieneDescuento: product.tieneDescuento || false,
            precioVentaDescuento: product.precioVentaDescuento || product.precioVenta,
            nombre: product.nombre || product.descripcion || '',
            precioVenta: product.selectedVariant?.precioVenta ?? product.precioVenta ?? product.precio ?? 0,
            cantidad: qty,
            foto: foto,
            talla: product.selectedVariant?.talla || null,
            color: product.selectedVariant?.color || null,

            raw: product,

        }
        state.items.push(toAdd)
    }

    state.lastAddedAt = Date.now()
    Success("Producto agregado al Carrito")
}

function removeItem(id) {
    const idx = findIndexById(id)
    if (idx !== -1) state.items.splice(idx, 1)
    Success("Producto eliminado del Carrito")
}

function updateQuantity(id, cantidad) {
    const idx = findIndexById(id)
    if (idx !== -1) state.items[idx].cantidad = cantidad
}

function clearCart() {
    state.items = []
    state.lastAddedAt = null
}

//const totalCount = computed(() => state.items.reduce((s, i) => s + (i.cantidad || 0), 0))
//const totalPrice = computed(() => state.items.reduce((s, i) => s + ((i.precioVenta || 0) * (i.cantidad || 0)), 0))
const totalPrice = computed(() =>
    state.items.reduce((s, i) => {
        const unitPrice = i.tieneDescuento
            ? (i.precioVentaDescuento || i.precioVenta || 0)
            : (i.precioVenta || 0)
        return s + unitPrice * (i.cantidad || 0)
    }, 0)
)

// Ahorro total: diferencia entre precio original y precio con descuento
const totalSavings = computed(() =>
    state.items.reduce((s, i) => {
        if (i.tieneDescuento) {
            const original = (i.precioVenta || 0) * (i.cantidad || 0)
            const discounted = (i.precioVentaDescuento || i.precioVenta || 0) * (i.cantidad || 0)
            return s + (original - discounted)
        }
        return s
    }, 0)
)
const totalCount = computed(() => state.items.reduce((s, i) => s + (i.cantidad || 0), 0))
export function useCart() {
    return {
        items: state.items,
        lastAddedAt: state.lastAddedAt,
        addItem,
        removeItem,
        updateQuantity,
        clearCart,
        totalCount,
        totalPrice,
        totalSavings,
        state
    }
}

export default useCart
