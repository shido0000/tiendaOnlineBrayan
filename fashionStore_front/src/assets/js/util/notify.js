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
        position: 'bottom',
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
