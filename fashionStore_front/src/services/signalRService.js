import * as signalR from '@microsoft/signalr'
import { Success, Error as ErrorNotify } from 'src/assets/js/util/notify'

class SignalRService {
    constructor() {
        this.connection = null
        this.connectionPromise = null
        this.listeners = []
        this.isConnecting = false
    }

    async connect() {
        // Si ya está conectado, retornar
        if (this.connection?.state === signalR.HubConnectionState.Connected) {
            return this.connection
        }

        // Si ya se está conectando, esperar
        if (this.isConnecting && this.connectionPromise) {
            return this.connectionPromise
        }

        this.isConnecting = true
        const token = localStorage.getItem('token') || sessionStorage.getItem('token')
        if (!token) {
            //  console.log('⚠️ No hay token, no se conectará a SignalR')
            this.isConnecting = false
            throw new Error('No token available')
        }

        this.connectionPromise = new Promise((resolve, reject) => {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('https://localhost:6005/pedidosHub', {
                    accessTokenFactory: () => token,
                    skipNegotiation: true, // Mejor performance
                    transport: signalR.HttpTransportType.WebSockets
                })
                .withAutomaticReconnect({
                    nextRetryDelayInMilliseconds: retryContext => {
                        if (retryContext.elapsedMilliseconds < 10000) {
                            return 1000
                        } else if (retryContext.elapsedMilliseconds < 30000) {
                            return 5000
                        } else {
                            return 10000
                        }
                    }
                })
                .withHubProtocol(new signalR.JsonHubProtocol())
                .configureLogging(signalR.LogLevel.Warning)
                .build()

            // Evento de pedido generado
            this.connection.on('PedidoGenerado', (data) => {
                //console.log('📦 Nuevo pedido recibido:', data)
                this.handlePedidoGenerado(data)
            })

            // Evento de pedido actualizado
            this.connection.on('PedidoActualizado', (data) => {
                //console.log('🔄 Pedido actualizado recibido:', data)
                this.handlePedidoActualizado(data)
            })

            // Evento de pedido cancelado/rechazado
            this.connection.on('PedidoCancelado', (data) => {
                // console.log('❌ Pedido cancelado recibido:', data)
                this.handlePedidoCancelado(data)
            })

            // Eventos de conexión
            this.connection.onreconnected(() => {
                // console.log('✅ Reconectado a SignalR')
                Success('Conexión en tiempo real restablecida')
            })

            this.connection.onreconnecting(() => {
                //  console.log('🔄 Reconectando a SignalR...')
            })

            this.connection.onclose(() => {
                //console.log('🔌 Desconectado de SignalR')
                this.isConnecting = false
            })

            // Iniciar conexión
            this.connection.start()
                .then(() => {
                    // console.log('✅ Conectado exitosamente a SignalR')
                    this.isConnecting = false
                    // Configurar escuchadores de eventos del frontend
                    this.setupFrontendListeners()
                    resolve(this.connection)
                })
                .catch(err => {
                    console.error('❌ Error conectando a SignalR:', err)
                    this.isConnecting = false
                    reject(err)
                })
        })

        return this.connectionPromise
    }

    handlePedidoGenerado(data) {
        // Verificar si el usuario actual es admin o vendedor
        const userData = this.getUserData()
        const userRole = userData?.role?.toLowerCase()
        const totalCalculado = (data.Items
            || []).reduce((acc, p) => acc + (p.Precio * p.Cantidad), 0);

        // Mostrar notificación solo si el usuario es admin o vendedor
        if (userRole === 'admin' || userRole === 'administrador' || userRole === 'vendedor') {
            const mensaje = `
                🛒 ¡Nuevo Pedido!
                Código: ${data.Codigo || 'N/A'}
                Total: $${totalCalculado.toFixed(2)}
                Cliente: ${data.Cliente || 'N/A'}
                Productos: ${data.CantidadProductos || 'N/A'}
                Estado: ${data.Estado || 'Pendiente'}
            `

            Success(mensaje, {
                timeout: 8000,
                position: 'top-right'
            })

            // Disparar evento personalizado
            const event = new CustomEvent('pedido-generado', {
                detail: {
                    ...data,
                    timestamp: new Date().toISOString()
                }
            })
            window.dispatchEvent(event)

            // Ejecutar listeners
            this.listeners.forEach(callback => {
                try {
                    callback(data)
                } catch (error) {
                    console.error('Error en listener:', error)
                }
            })
        }
    }

    getUserData() {
        try {
            const token = localStorage.getItem('token') || sessionStorage.getItem('token')
            if (token) {
                const payload = JSON.parse(atob(token.split('.')[1]))

                return {
                    id: payload.Id,
                    email: payload.Correo,
                    role: payload.Rol,
                    name: payload.NombreCompleto
                }
            }
        } catch (error) {
            console.error('Error obteniendo datos del usuario:', error)
        }
        return null
    }

    onPedidoGenerado(callback) {
        this.listeners.push(callback)
        return () => {
            this.listeners = this.listeners.filter(cb => cb !== callback)
        }
    }

    handlePedidoActualizado(data) {

        // Validar que data exista antes de procesarla
        if (!data) {
            console.warn('⚠️ PedidoActualizado recibido sin datos')
            // Simplemente recargar la lista sin notificación
            const event = new CustomEvent('pedido-actualizado', {
                detail: {
                    timestamp: new Date().toISOString()
                }
            })
            window.dispatchEvent(event)
            return
        }

        // Disparar evento personalizado para que se actualicen todas las ventanas
        const event = new CustomEvent('pedido-actualizado', {
            detail: {
                ...data,
                timestamp: new Date().toISOString()
            }
        })
        window.dispatchEvent(event)

        // Mostrar notificación visual al cliente sobre cambio de estado
        const userData = this.getUserData()
        const userRole = userData?.role?.toLowerCase()

        // Mostrar notificación a todos (vendedores, administradores y clientes)
        if (data.Estado || data.estado) {
            const estado = data.Estado || data.estado
            const codigo = data.Codigo || data.codigo || 'N/A'

            // Para clientes, notificar cambios en sus pedidos
            if (userRole === 'cliente') {
                const mensaje = `📦 Tu pedido ${codigo} ha cambiado a: ${estado}`
                Success(mensaje, {
                    timeout: 5000,
                    position: 'top-right'
                })
            }
        }

        // Ejecutar listeners personalizados
        this.listeners.forEach(callback => {
            try {
                callback(data)
            } catch (error) {
                console.error('Error en listener de actualización:', error)
            }
        })
    }

    handlePedidoCancelado(data) {

        // Disparar evento personalizado
        const event = new CustomEvent('PedidoCancelado', {
            detail: {
                ...data,
                timestamp: new Date().toISOString()
            }
        })
        window.dispatchEvent(event)

        // Notificar a todos los usuarios
        const userData = this.getUserData()
        const userRole = userData?.role?.toLowerCase()

        const codigo = data.Codigo || data.codigo || 'N/A'

        if (userRole === 'cliente') {
            const mensaje = `❌ Tu pedido ${codigo} ha sido cancelado`
            ErrorNotify(mensaje, {
                timeout: 5000,
                position: 'top-right'
            })
        } else if (userRole === 'vendedor' || userRole === 'administrador') {
            const mensaje = `Pedido ${codigo} ha sido cancelado`
            Success(mensaje, {
                timeout: 4000,
                position: 'top-right'
            })
        }

        // Ejecutar listeners personalizados
        this.listeners.forEach(callback => {
            try {
                callback(data)
            } catch (error) {
                console.error('Error en listener de cancelación:', error)
            }
        })
    }

    async disconnect() {
        if (this.connection) {
            try {
                await this.connection.stop()
           //     console.log('🔌 Desconectado manualmente de SignalR')
            } catch (error) {
                console.error('Error desconectando:', error)
            }
        }
    }

    // Método para verificar estado
    getConnectionState() {
        return this.connection?.state || 'Disconnected'
    }

    // Método para notificar a todos sobre un nuevo pedido (para admin y vendedores)
    // NOTA: El servidor emite el evento automáticamente, no necesitas invocarlo desde aquí
    async notificarNuevoPedido(datosNotificacion) {
    }

    // Escuchar eventos del frontend
    setupFrontendListeners() {
        // Cuando se confirma un pedido, simplemente dejar que el servidor notifique
        window.addEventListener('nuevoPedidoConfirmado', (event) => {
            //console.log('📢 Evento de nuevo pedido confirmado:', event.detail)
          //  console.log('⏳ Esperando que el servidor emita PedidoActualizado a todos...')
        })

        window.addEventListener('PedidoGenerado', (event) => {
         //   console.log('📢 Evento de nuevo pedido confirmado:', event.detail)
          //  console.log('⏳ Esperando que el servidor emita PedidoGenerado a vendedores y admin...')
        })

        // Cuando se cancela un pedido, simplemente dejar que el servidor notifique
        window.addEventListener('PedidoCancelado', (event) => {
           // console.log('🗑️ Evento de pedido eliminado:', event.detail)
           // console.log('⏳ Esperando que el servidor emita PedidoCancelado a todos...')
        })
    }
}

// Exportar singleton
const signalRService = new SignalRService()
export default signalRService
