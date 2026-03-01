import { api } from 'src/boot/axios'
import { Notify } from 'quasar'

/**
 * Servicio para gestionar y obtener información de carritos activos
 * Proporciona datos en tiempo real sobre qué usuarios tienen productos en carrito
 *
 * Método backend: EnviarDatosAlCarrito
 */
class ActiveCartsService {
    /**
     * Formatea datos del carrito para enviar al backend (EnviarDatosAlCarrito)
     * @param {Object} cartData - Datos del carrito del store
     * @param {string} usuarioId - ID del usuario
     * @returns {Object} Objeto formateado para enviar al backend
     */
    formatCartDataForBackend(cartData, usuarioId) {
        // cartData es el array de items desde el store
        const items = Array.isArray(cartData) ? cartData : (cartData.items || [])

        return {
            usuarioId: usuarioId,
            fechaCreacion: new Date().toISOString(),
            detalles: items.map(item => ({
                carritoId: item.id || item.carritoId || '',
                productoId: item.productoId || item.id || '',
                cantidad: item.cantidad || 0,
                unitPrice: item.precio || 0,
                lineTotal: (item.precio || 0) * (item.cantidad || 0)
            }))
        }
    }

    /**
     * Envía datos del carrito al backend usando EnviarDatosAlCarrito
     * @param {Object} cartData - Datos del carrito formateados
     * @returns {Promise<Object>} Respuesta del backend
     */
    async sendCartDataToBackend(cartData) {
        try {
            const response = await api.post('/Carrito/EnviarDatosAlCarrito', cartData)
            return response.data
        } catch (error) {
            console.error('Error enviando datos del carrito:', error)
            throw error
        }
    }

    /**
     * Envía carrito de usuario específico al backend
     * @param {string} usuarioId - ID del usuario
     * @param {Array} cartItems - Items del carrito
     * @returns {Promise<Object>} Respuesta del backend
     */
    async sendUserCartToBackend(usuarioId, cartItems) {
        try {
            const formattedData = this.formatCartDataForBackend(cartItems, usuarioId)
            return await this.sendCartDataToBackend(formattedData)
        } catch (error) {
            console.error(`Error enviando carrito del usuario ${usuarioId}:`, error)
            throw error
        }
    }

    /**
     * Obtiene lista de usuarios con carritos activos
     * @returns {Promise<Array>} Lista de usuarios con sus carritos
     * @example
     * {
     *   usuarioId: "uuid",
     *   nombre: "Juan",
     *   apellido: "Pérez",
     *   email: "juan@example.com",
     *   telefono: "+34 123 456 789",
     *   cartCount: 5,
     *   cartTotal: 250.50,
     *   cartItems: [
     *     {
     *       id: "1",
     *       productoId: "prod-1",
     *       nombre: "Producto 1",
     *       variante: "Talla L",
     *       precio: 50.00,
     *       cantidad: 1,
     *       foto: "http://..."
     *     }
     *   ],
     *   lastActivity: "2024-02-21T10:30:00Z"
     * }
     */
    async getActiveCarts() {
        try {
            const response = await api.get('/Carrito/ObtenerDatosCarritoReal')
            const data = response.data || []

            // Transformar estructura del backend al formato esperado por el frontend
            return Array.isArray(data)
                ? data.map((cart) => ({
                    usuarioId: cart.usuarioId,
                    nombre: cart.nombreCliente,
                    apellido: cart.apellidoCliente,
                    email: cart.emailCliente,
                    telefono: cart.telefonoCliente,
                    fechaCreacion: cart.fechaCreacion,
                    cartCount: cart.detalles ? cart.detalles.length : 0,
                    cartTotal: cart.detalles
                        ? cart.detalles.reduce((sum, item) => sum + (item.lineTotal || 0), 0)
                        : 0,
                    cartItems: (cart.detalles || []).map((item) => ({
                        id: item.carritoId,
                        carritoId: item.carritoId,
                        productoId: item.productoId,
                        nombre: item.descripcionProducto || item.codigoProducto,
                        codigo: item.codigoProducto,
                        descripcion: item.descripcionProducto,
                        cantidad: item.cantidad,
                        precio: item.unitPrice,
                        unitPrice: item.unitPrice,
                        lineTotal: item.lineTotal
                    }))
                }))
                : []
        } catch (error) {
            console.error('Error obteniendo carritos activos:', error)
            throw error
        }
    }

    /**
     * Obtiene detalles del carrito de un usuario específico
     * @param {string} usuarioId - ID del usuario
     * @returns {Promise<Object>} Detalles del carrito del usuario
     */
    async getUserCart(usuarioId) {
        try {
            const response = await api.get(`/api/usuarios/${usuarioId}/carrito`)
            return response.data
        } catch (error) {
            console.error(`Error obteniendo carrito del usuario ${usuarioId}:`, error)
            throw error
        }
    }

    /**
     * Observa cambios en carritos activos (requiere SignalR)
     * @param {Function} callback - Función a ejecutar cuando hay cambios
     * @returns {Function} Función para desuscribirse
     */
    watchActiveCarts(callback) {
        const handler = (event) => {
            callback(event.detail)
        }

        window.addEventListener('carrito-actualizado', handler)

        // Retornar función de desuscripción
        return () => {
            window.removeEventListener('carrito-actualizado', handler)
        }
    }

    /**
     * Obtiene estadísticas de carritos
     * @returns {Promise<Object>} Estadísticas de carritos activos
     */
    async getCartsStatistics() {
        try {
            const response = await api.get('/api/usuarios/estadisticas-carritos')
            return response.data || {
                totalActiveCarts: 0,
                totalValue: 0,
                averageCartValue: 0,
                topProducts: []
            }
        } catch (error) {
            console.error('Error obteniendo estadísticas de carritos:', error)
            throw error
        }
    }

    /**
     * Obtiene carritos que requieren atención (pendientes hace más de X tiempo)
     * @param {number} minutesThreshold - Minutos para considerar un carrito como "abandonado"
     * @returns {Promise<Array>} Carritos que requieren seguimiento
     */
    async getAbandonedCarts(minutesThreshold = 30) {
        try {
            const response = await api.get('/api/usuarios/carritos-abandonados', {
                params: { minutosUmbral: minutesThreshold }
            })
            return response.data || []
        } catch (error) {
            console.error('Error obteniendo carritos abandonados:', error)
            throw error
        }
    }

    /**
     * Envía notificación a un usuario sobre su carrito
     * @param {string} usuarioId - ID del usuario
     * @param {string} mensaje - Mensaje a enviar
     * @returns {Promise<Object>} Respuesta del servidor
     */
    async notifyUserAboutCart(usuarioId, mensaje) {
        try {
            const response = await api.post(`/api/usuarios/${usuarioId}/notificar-carrito`, {
                mensaje
            })
            return response.data
        } catch (error) {
            console.error(`Error notificando usuario ${usuarioId}:`, error)
            throw error
        }
    }

    /**
     * Descarga reporte de carritos activos
     * @param {string} formato - 'csv' o 'pdf'
     * @returns {Promise<Blob>} Archivo descargable
     */
    async downloadCartsReport(formato = 'csv') {
        try {
            const response = await api.get('/api/usuarios/reporte-carritos', {
                params: { formato },
                responseType: 'blob'
            })
            return response.data
        } catch (error) {
            console.error('Error descargando reporte:', error)
            throw error
        }
    }
}

export default new ActiveCartsService()
