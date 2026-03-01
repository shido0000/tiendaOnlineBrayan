import { Notify } from 'quasar'

export const Success = (message) => {
    Notify.create({
        message,
        type: 'positive',
        position: 'bottom',
        progress: true
    })
}

export const Info = (message) => {
    Notify.create({
        message,
        type: 'warning',
        textColor: 'white',
        position: 'bottom',
        progress: true
    })
}

export const Verify = (message) => {
    Notify.create({
        message,
        type: 'warning',
        position: 'bottom',
        progress: true
    })
}

export const Warning = (message) => {
    Notify.create({
        message,
        type: 'warning',
        position: 'top-right',
        progress: true
    })
}

export const Error = (message) => {
    Notify.create({
        message,
        type: 'negative',
        position: 'bottom',
        progress: true
    })
}

// 🎯 Nueva función para errores de validación de stock
export const StockError = (producto, validation) => {
    Notify.create({
        type: 'warning',
        message: `Stock agotado: "${producto}"`,
        caption: validation.remaining === 0
            ? `Ya tienes ${validation.current} en carrito (máx: ${validation.max})`
            : `Solo puedes añadir ${validation.remaining} más (máx: ${validation.max})`,
        timeout: 4000,
        position: 'bottom',
        progress: true,
        actions: [{ icon: 'close', color: 'white', round: true }]
    })
}
