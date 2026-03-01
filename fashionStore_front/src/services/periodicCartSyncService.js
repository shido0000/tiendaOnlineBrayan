/**
 * Servicio de Sincronización Periódica de Carrito
 *
 * Lee el carrito del localStorage cada X segundos
 * y lo envía al backend automáticamente
 */

import activeCartsService from 'src/services/activeCartsService'
import { Error as ErrorNotify } from 'src/assets/js/util/notify'

const STORAGE_KEY = 'fashion_cart_v1'
const DEFAULT_INTERVAL = 30000 // 30 segundos

class PeriodicCartSyncService {
    constructor() {
        this.intervalId = null
        this.isRunning = false
        this.lastSyncTime = null
        this.syncCount = 0
        this.errorCount = 0
    }

    /**
     * Inicia la sincronización periódica del carrito
     * @param {number} intervalMs - Intervalo en milisegundos (default: 30000)
     * @returns {void}
     */
    start(intervalMs = DEFAULT_INTERVAL) {
        if (this.isRunning) {
            console.warn('⚠️ Sincronización periódica ya está en ejecución')
            return
        }

        this.isRunning = true
        console.log(`✅ Iniciando sincronización periódica del carrito cada ${intervalMs}ms`)

        // Ejecutar inmediatamente la primera vez
        this.syncCartFromStorage()

        // Luego cada X ms
        this.intervalId = setInterval(() => {
            this.syncCartFromStorage()
        }, intervalMs)
    }

    /**
     * Detiene la sincronización periódica
     * @returns {void}
     */
    stop() {
        if (this.intervalId) {
            clearInterval(this.intervalId)
            this.intervalId = null
            this.isRunning = false
            console.log('🛑 Sincronización periódica detenida')
            console.log(`📊 Stats: ${this.syncCount} syncs, ${this.errorCount} errores`)
        }
    }

    /**
     * Lee carrito del localStorage y lo envía al backend
     * @returns {Promise<boolean>} True si fue exitoso
     */
    async syncCartFromStorage() {
        try {
            // Obtener token para verificar autenticación
            const token = localStorage.getItem('token') || sessionStorage.getItem('token')
            if (!token) {
                // Usuario no autenticado, no sincronizar
                return false
            }

            // Leer carrito del localStorage
            const cartJson = localStorage.getItem(STORAGE_KEY)
            if (!cartJson) {
                // No hay carrito guardado
                return false
            }

            const cartItems = JSON.parse(cartJson)
            if (!Array.isArray(cartItems) || cartItems.length === 0) {
                // Carrito vacío, no sincronizar (opcional: podrías enviarlo igual)
                return false
            }

            // Obtener usuarioId del token
            const usuarioId = this.getUserIdFromToken(token)
            if (!usuarioId) {
                console.error('❌ No se pudo extraer usuarioId del token')
                return false
            }

            // Formatear datos para el backend
            const cartData = this.formatCartData(cartItems, usuarioId)

            // Enviar al backend
            await activeCartsService.sendCartDataToBackend(cartData)

            this.lastSyncTime = new Date()
            this.syncCount++

            // console.log(`✅ Carrito sincronizado exitosamente (${this.syncCount})`)
            return true

        } catch (error) {
            this.errorCount++
            console.error(`❌ Error en sincronización periódica #${this.errorCount}:`, error.message)
            // No mostrar notificación para no molestar
            return false
        }
    }

    /**
     * Formatea los datos del carrito para enviar al backend
     * @param {Array} cartItems - Items del carrito desde localStorage
     * @param {string} usuarioId - ID del usuario
     * @returns {Object} Datos formateados
     */
    formatCartData(cartItems, usuarioId) {
        return {
            usuarioId: usuarioId,
            fechaCreacion: new Date().toISOString(),
            detalles: cartItems.map(item => ({
                carritoId: item.id || '',
                productoId: item.raw?.id || item.productoId || item.id || '',
                cantidad: item.cantidad || 0,
                unitPrice: item.precioVenta || item.precio || 0,
                lineTotal: (item.precioVenta || item.precio || 0) * (item.cantidad || 0)
            }))
        }
    }

    /**
     * Extrae usuarioId del JWT
     * @param {string} token - Token JWT
     * @returns {string|null} usuarioId o null
     */
    getUserIdFromToken(token) {
        try {
            const decoded = JSON.parse(atob(token.split('.')[1]))
            return decoded.Id
        } catch (error) {
            console.error('Error decodificando token:', error)
            return null
        }
    }

    /**
     * Obtiene estado actual de la sincronización
     * @returns {Object} Información de estado
     */
    getStatus() {
        return {
            isRunning: this.isRunning,
            lastSyncTime: this.lastSyncTime,
            syncCount: this.syncCount,
            errorCount: this.errorCount,
            successRate: this.syncCount > 0
                ? ((this.syncCount - this.errorCount) / this.syncCount * 100).toFixed(2) + '%'
                : 'N/A'
        }
    }

    /**
     * Reinicia la sincronización (detiene y vuelve a comenzar)
     * @param {number} intervalMs - Nuevo intervalo
     * @returns {void}
     */
    restart(intervalMs = DEFAULT_INTERVAL) {
        this.stop()
        this.syncCount = 0
        this.errorCount = 0
        this.lastSyncTime = null
        this.start(intervalMs)
    }
}

// Exportar singleton
const periodicCartSyncService = new PeriodicCartSyncService()
export default periodicCartSyncService
