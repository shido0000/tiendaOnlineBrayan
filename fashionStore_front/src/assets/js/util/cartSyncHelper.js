/**
 * Helper para integrar activeCartsService con cartStore
 * Envía automáticamente datos del carrito al backend cuando cambia
 *
 * Uso en cartStore.js:
 * 1. Importar: import { syncCartWithBackend } from 'src/assets/js/util/cartSyncHelper'
 * 2. Al modificar carrito: await syncCartWithBackend(state.items)
 * 3. O usar watchEffect para sincronización automática
 */

import activeCartsService from 'src/services/activeCartsService'
import { Error as ErrorNotify, Success } from 'src/assets/js/util/notify'

/**
 * Sincroniza el carrito actual del usuario con el backend
 * @param {Array} cartItems - Items del carrito desde el store
 * @param {string} usuarioId - ID del usuario (opcional, obtiene del token si no se proporciona)
 * @returns {Promise<boolean>} True si se envió exitosamente
 */
export async function syncCartWithBackend(cartItems, usuarioId = null) {
    try {
        // Si no se proporciona usuarioId, obtenerlo del token
        if (!usuarioId) {
            const token = localStorage.getItem('token') || sessionStorage.getItem('token')
            if (!token) {
                console.warn('⚠️ No hay usuario autenticado para sincronizar carrito')
                return false
            }

            try {
                // Decodificar JWT para obtener usuarioId
                const decoded = JSON.parse(atob(token.split('.')[1]))
                usuarioId = decoded.nameid || decoded.sub || decoded.userId
            } catch (e) {
                console.error('Error decodificando token:', e)
                return false
            }
        }

        if (!usuarioId) {
            console.warn('⚠️ No se pudo obtener usuarioId para sincronizar')
            return false
        }

        // Enviar al backend
        const response = await activeCartsService.sendUserCartToBackend(usuarioId, cartItems)

        // console.log('✅ Carrito sincronizado con backend:', response)
        return true
    } catch (error) {
        console.error('❌ Error sincronizando carrito:', error)
        // No mostrar notificación de error para no molestar al usuario
        // ErrorNotify('Error al sincronizar carrito')
        return false
    }
}

/**
 * Versión con notificación para acciones críticas
 * @param {Array} cartItems - Items del carrito
 * @param {string} usuarioId - ID del usuario
 * @returns {Promise<boolean>}
 */
export async function syncCartWithBackendAndNotify(cartItems, usuarioId = null) {
    try {
        const result = await syncCartWithBackend(cartItems, usuarioId)

        if (result) {
            Success('Carrito actualizado en servidor', {
                timeout: 2000,
                position: 'top'
            })
        } else {
            ErrorNotify('Error al actualizar carrito en servidor', {
                timeout: 3000
            })
        }

        return result
    } catch (error) {
        ErrorNotify('Error sincronizando carrito', {
            timeout: 3000
        })
        return false
    }
}

/**
 * Obtiene usuarioId del token actual
 * @returns {string|null} ID del usuario o null
 */
export function getUserIdFromToken() {
    try {
        const token = localStorage.getItem('token') || sessionStorage.getItem('token')
        if (!token) return null

        const decoded = JSON.parse(atob(token.split('.')[1]))
        return decoded.nameid || decoded.sub || decoded.userId || null
    } catch (error) {
        console.error('Error obteniendo userId del token:', error)
        return null
    }
}

/**
 * Hook para sincronizar carrito automáticamente en cambios
 * Usar en componentes:
 *
 * import { useAutoSyncCart } from 'src/assets/js/util/cartSyncHelper'
 *
 * export default {
 *   setup() {
 *     const cartStore = useCartStore()
 *     useAutoSyncCart(cartStore.items)
 *
 *     return { cartStore }
 *   }
 * }
 */
export function useAutoSyncCart(cartItems) {
    const { watchEffect } = require('vue')

    watchEffect(async () => {
        if (cartItems && cartItems.length > 0) {
            await syncCartWithBackend(cartItems)
        }
    })
}

export default {
    syncCartWithBackend,
    syncCartWithBackendAndNotify,
    getUserIdFromToken,
    useAutoSyncCart
}
