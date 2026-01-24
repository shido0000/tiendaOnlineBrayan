<template>
  <q-card flat bordered class="q-pa-lg">
    <q-card-section>
      <div class="text-h6 q-mb-md">🔍 Diagnóstico SignalR</div>
    </q-card-section>

    <q-separator />

    <q-card-section>
      <!-- Estado Conexión -->
      <div class="q-mb-lg">
        <div class="text-subtitle2 q-mb-sm">
          <strong>1. Estado Conexión</strong>
        </div>
        <q-chip
          :color="connectionStatus.estado === 'Conectado' ? 'positive' : connectionStatus.estado === 'Conectando' ? 'warning' : 'negative'"
          text-color="white"
        >
          {{ connectionStatus.estado }}
        </q-chip>
        <div class="text-caption q-mt-sm text-grey-8">
          {{ connectionStatus.mensaje }}
        </div>
      </div>

      <!-- Token Info -->
      <div class="q-mb-lg">
        <div class="text-subtitle2 q-mb-sm">
          <strong>2. Token JWT</strong>
        </div>
        <div v-if="tokenInfo.existe" class="bg-green-1 q-pa-md rounded-borders">
          <div><strong>Existe:</strong> ✅ Sí</div>
          <div><strong>Usuario:</strong> {{ tokenInfo.usuario }}</div>
          <div><strong>Rol:</strong> <q-chip color="blue" text-color="white" size="sm">{{ tokenInfo.rol }}</q-chip></div>
          <div><strong>Válido:</strong> {{ tokenInfo.valido ? '✅ Sí' : '❌ No' }}</div>
        </div>
        <div v-else class="bg-red-1 q-pa-md rounded-borders">
          <strong>❌ No hay token</strong>
        </div>
      </div>

      <!-- URL Configuración -->
      <div class="q-mb-lg">
        <div class="text-subtitle2 q-mb-sm">
          <strong>3. Configuración URLs</strong>
        </div>
        <div class="bg-grey-1 q-pa-md rounded-borders text-mono text-caption">
          <div><strong>Frontend:</strong> {{ urls.frontend }}</div>
          <div><strong>Backend:</strong> {{ urls.backend }}</div>
          <div><strong>Hub:</strong> {{ urls.hub }}</div>
        </div>
      </div>

      <!-- Logs -->
      <div class="q-mb-lg">
        <div class="text-subtitle2 q-mb-sm">
          <strong>4. Logs Recientes</strong>
        </div>
        <q-scroll-area style="height: 200px" class="bg-grey-1 rounded-borders">
          <div class="q-pa-md">
            <div v-for="(log, idx) in logs" :key="idx" class="q-mb-sm text-caption">
              <q-chip
                :color="log.tipo === 'error' ? 'negative' : log.tipo === 'warning' ? 'warning' : 'info'"
                text-color="white"
                size="sm"
              >
                {{ log.tipo.toUpperCase() }}
              </q-chip>
              <span>{{ log.mensaje }}</span>
              <span class="text-grey-7">{{ log.hora }}</span>
            </div>
            <div v-if="logs.length === 0" class="text-grey-6">
              Sin logs aún...
            </div>
          </div>
        </q-scroll-area>
      </div>

      <!-- Acciones -->
      <div class="q-mb-lg">
        <div class="text-subtitle2 q-mb-sm">
          <strong>5. Acciones</strong>
        </div>
        <q-btn
          color="primary"
          label="Reconectar"
          @click="reconectar"
          class="q-mr-md"
        />
        <q-btn
          color="negative"
          label="Desconectar"
          @click="desconectar"
          :disable="connectionStatus.estado !== 'Conectado'"
        />
      </div>
    </q-card-section>
  </q-card>
</template>

<script setup>
import { ref, reactive, onMounted, onUnmounted } from 'vue'
import signalRService from 'src/services/signalRService'
import * as signalR from '@microsoft/signalr'

const connectionStatus = reactive({
  estado: 'Desconectado',
  mensaje: 'Sin intentar conexión'
})

const tokenInfo = reactive({
  existe: false,
  usuario: 'N/A',
  rol: 'N/A',
  valido: false
})

const urls = reactive({
  frontend: window.location.origin,
  backend: 'https://localhost:6005',
  hub: 'https://localhost:6005/pedidosHub'
})

const logs = ref([])

// Monitorear estado de conexión
const monitorearEstado = () => {
  const interval = setInterval(() => {
    if (signalRService.connection) {
      const estado = signalRService.connection.state
      if (estado === signalR.HubConnectionState.Connected) {
        connectionStatus.estado = 'Conectado'
        connectionStatus.mensaje = `ConnectionId: ${signalRService.connection.connectionId}`
      } else if (estado === signalR.HubConnectionState.Connecting) {
        connectionStatus.estado = 'Conectando'
        connectionStatus.mensaje = 'Estableciendo conexión...'
      } else if (estado === signalR.HubConnectionState.Reconnecting) {
        connectionStatus.estado = 'Reconectando'
        connectionStatus.mensaje = 'Reintentando conexión...'
      } else {
        connectionStatus.estado = 'Desconectado'
        connectionStatus.mensaje = 'No hay conexión activa'
      }
    }
  }, 1000)

  onUnmounted(() => clearInterval(interval))
}

const agregarLog = (tipo, mensaje) => {
  const ahora = new Date().toLocaleTimeString()
  logs.value.unshift({
    tipo,
    mensaje,
    hora: ahora
  })
  if (logs.value.length > 20) logs.value.pop()
  console.log(`[${tipo.toUpperCase()}] ${mensaje}`)
}

const verificarToken = () => {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')

  tokenInfo.existe = !!token

  if (token) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]))
      tokenInfo.usuario = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || payload.unique_name || 'N/A'
      tokenInfo.rol = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role || 'N/A'
      tokenInfo.valido = true
      agregarLog('info', `Token válido - Usuario: ${tokenInfo.usuario}, Rol: ${tokenInfo.rol}`)
    } catch (error) {
      tokenInfo.valido = false
      agregarLog('error', `Token inválido: ${error.message}`)
    }
  } else {
    agregarLog('warning', 'No hay token disponible')
  }
}

const reconectar = async () => {
  try {
    agregarLog('info', 'Iniciando reconexión...')
    signalRService.disconnect()
    await signalRService.connect()
    agregarLog('info', '✅ Reconectado exitosamente')
  } catch (error) {
    agregarLog('error', `Error al reconectar: ${error.message}`)
  }
}

const desconectar = () => {
  try {
    signalRService.disconnect()
    agregarLog('info', 'Desconectado manualmente')
  } catch (error) {
    agregarLog('error', `Error al desconectar: ${error.message}`)
  }
}

onMounted(async () => {
  agregarLog('info', 'Iniciando diagnóstico...')
  verificarToken()
  monitorearEstado()

  try {
    await signalRService.connect()
    agregarLog('info', '✅ Conectado a SignalR')
  } catch (error) {
    agregarLog('error', `No se pudo conectar: ${error.message}`)
  }
})
</script>

<style scoped>
.text-mono {
  font-family: 'Courier New', monospace;
}
</style>
