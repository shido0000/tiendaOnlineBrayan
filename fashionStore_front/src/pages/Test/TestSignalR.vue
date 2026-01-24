<template>
  <div class="q-pa-md bg-blue-1 rounded-borders q-mb-md">
    <div class="text-h6 q-mb-md">🧪 Test SignalR</div>

    <div class="q-mb-md">
      <strong>Estado:</strong>
      <q-chip
        :color="connected ? 'positive' : 'negative'"
        text-color="white"
      >
        {{ connected ? '✅ Conectado' : '❌ Desconectado' }}
      </q-chip>
    </div>

    <div class="q-mb-md">
      <strong>Rol:</strong> {{ userRole || 'No detectado' }}
    </div>

    <q-btn
      color="primary"
      label="Enviar notificación de prueba"
      @click="enviarPrueba"
      :disable="!connected"
    />

    <div v-if="notificaciones.length > 0" class="q-mt-md">
      <strong>Últimas notificaciones:</strong>
      <q-list>
        <q-item v-for="(notif, idx) in notificaciones" :key="idx">
          <q-item-section>{{ JSON.stringify(notif) }}</q-item-section>
        </q-item>
      </q-list>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import signalRService from 'src/services/signalRService'

const connected = ref(false)
const userRole = ref(null)
const notificaciones = ref([])

onMounted(async () => {
  try {
    await signalRService.connect()
    connected.value = true

    // Obtener rol del token
    const token = localStorage.getItem('token') || sessionStorage.getItem('token')
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]))
      userRole.value = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'Cliente'
    }

    // Escuchar notificaciones
    signalRService.onPedidoGenerado((data) => {
      notificaciones.value.unshift(data)
      if (notificaciones.value.length > 5) notificaciones.value.pop()
    })
  } catch (error) {
    console.error('Error:', error)
    connected.value = false
  }
})

const enviarPrueba = async () => {
  console.log('Enviando notificación de prueba...')
  // Esto solo registra en consola
  console.log('Para ver notificaciones, crea un pedido real')
}
</script>
