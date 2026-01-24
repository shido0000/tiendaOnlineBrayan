import signalRService from 'src/services/signalRService'

export default async () => {
    const token = localStorage.getItem('token') || sessionStorage.getItem('token')

    if (token) {
        try {
            await signalRService.connect()
        } catch (error) {
            console.error('Error inicializando SignalR:', error)
            // No es fatal, la app continúa funcionando
        }
    }
}
