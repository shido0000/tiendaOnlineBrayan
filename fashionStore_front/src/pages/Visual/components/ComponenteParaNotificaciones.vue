<template>
  <!-- Este componente puede estar en tu layout principal -->
  <div v-if="showNotification" class="notification-overlay">
    <div class="notification-card">
      <h3>🛒 Nuevo Pedido Recibido</h3>
      <p><strong>Código:</strong> #{{ notificationData.Codigo }}</p>
      <p><strong>Total:</strong> ${{ notificationData.Total?.toFixed(2) }}</p>
      <p><strong>Cliente:</strong> {{ notificationData.Cliente }}</p>
      <button @click="closeNotification">Cerrar</button>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import signalRService from 'src/services/signalRService'
import { useRouter } from 'vue-router'

const router = useRouter()
const showNotification = ref(false)
const notificationData = ref({})

onMounted(async () => {
  try {
    await signalRService.connect()

    // Escuchar eventos de pedido
    signalRService.onPedidoGenerado((data) => {
      notificationData.value = data
      showNotification.value = true

      // Auto-ocultar después de 10 segundos
      setTimeout(() => {
        showNotification.value = false
      }, 10000)
    })

    // Escuchar evento global
    window.addEventListener('pedido-generado', (event) => {
      //console.log('Evento global recibido:', event.detail)
    })
  } catch (error) {
    console.error('Error inicializando SignalR:', error)
  }
})

onUnmounted(() => {
  signalRService.disconnect()
})

const closeNotification = () => {
  showNotification.value = false
}

const viewOrder = () => {
  if (notificationData.value.PedidoId) {
    router.push(`/admin/pedidos/${notificationData.value.PedidoId}`)
    showNotification.value = false
  }
}
</script>

<style scoped>
.notification-overlay {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
}

.notification-card {
  background: white;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
  border-left: 4px solid #4CAF50;
  min-width: 300px;
}
</style>
