/**
 * Boot file para iniciar sincronización periódica del carrito
 *
 * Este archivo se ejecuta cuando la app inicia y:
 * 1. Si hay usuario autenticado: Inicia sincronización periódica
 * 2. Si no: Espera autenticación
 */

import periodicCartSyncService from 'src/services/periodicCartSyncService'
import { useRouter } from 'vue-router'

let syncServiceInitialized = false

export default async ({ app, router }) => {
    // Escuchar cambios de ruta
    router.beforeEach((to, from, next) => {
        // Verificar si usuario se autenticó o desautenticó
        const token = localStorage.getItem('token') || sessionStorage.getItem('token')
        const wasAuthenticated = !!localStorage.getItem('token') || !!sessionStorage.getItem('token')

        if (token && !syncServiceInitialized) {
            // Usuario acaba de autenticarse
            console.log('👤 Usuario autenticado, iniciando sincronización periódica del carrito...')

            // Iniciar sincronización cada 30 segundos
            periodicCartSyncService.start(1800000)
            syncServiceInitialized = true

        } else if (!token && syncServiceInitialized) {
            // Usuario se desconectó
            console.log('👤 Usuario desconectado, deteniendo sincronización.')
            periodicCartSyncService.stop()
            syncServiceInitialized = false
        }

        next()
    })

    // Verificar estado inicial (si página se recarga)
    const initialToken = localStorage.getItem('token') || sessionStorage.getItem('token')
    if (initialToken && !syncServiceInitialized) {
        console.log('♻️ Página recargada con usuario autenticado, reiniciando sincronización...')
        periodicCartSyncService.start(1800000)
        syncServiceInitialized = true
    }

    // Exponer servicio globalmente para debugging
    if (process.env.DEV) {
        window.__periodicCartSyncService = periodicCartSyncService
        console.log('🔧 Debug: window.__periodicCartSyncService disponible')
    }
}


