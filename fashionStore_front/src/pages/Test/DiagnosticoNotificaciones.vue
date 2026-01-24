<template>
  <q-page class="q-pa-md">
    <div class="text-h4 q-mb-lg">🔍 Diagnóstico de Notificaciones en Tiempo Real</div>

    <!-- Estado de Conexión -->
    <q-card class="q-mb-lg">
      <q-card-section>
        <div class="text-h6 q-mb-md">1️⃣ Estado de Conexión SignalR</div>

        <div class="q-mb-md">
          <strong>Estado:</strong>
          <q-chip :color="estadoConexion === 1 ? 'positive' : 'negative'" text-color="white">
            {{ textoEstado }}
          </q-chip>
        </div>

        <div class="q-mb-md">
          <strong>Detalles:</strong>
          <ul>
            <li>URL Hub: <code>{{ urlHub }}</code></li>
            <li>Connection ID: <code>{{ connectionId || 'No disponible' }}</code></li>
            <li>Estado Numérico: <code>{{ estadoConexion }}</code> (0=Desconectado, 1=Conectado, 2=Reconectando, 3=Conectando)</li>
          </ul>
        </div>

        <q-btn
          color="primary"
          label="Actualizar Estado"
          @click="actualizarEstado"
          class="q-mr-md"
        />
        <q-btn
          color="warning"
          label="Reconectar"
          @click="reconectar"
        />
      </q-card-section>
    </q-card>

    <!-- Datos del Usuario -->
    <q-card class="q-mb-lg">
      <q-card-section>
        <div class="text-h6 q-mb-md">2️⃣ Datos del Usuario (JWT Token)</div>

        <div v-if="userData">
          <div class="q-mb-md">
            <strong>ID:</strong> {{ userData.id }}
          </div>
          <div class="q-mb-md">
            <strong>Email:</strong> {{ userData.email }}
          </div>
          <div class="q-mb-md">
            <strong>Rol:</strong>
            <q-chip :color="esAdminVendedor ? 'positive' : 'warning'" text-color="white">
              {{ userData.role }}
            </q-chip>
            <span v-if="esAdminVendedor" class="text-positive">✅ Puede recibir notificaciones</span>
            <span v-else class="text-warning">⚠️ No recibirá notificaciones (rol: Cliente)</span>
          </div>
          <div class="q-mb-md">
            <strong>Nombre:</strong> {{ userData.name }}
          </div>
        </div>
        <div v-else class="text-warning">
          ⚠️ No hay token. Por favor inicia sesión.
        </div>
      </q-card-section>
    </q-card>

    <!-- Prueba de Envío -->
    <q-card class="q-mb-lg">
      <q-card-section>
        <div class="text-h6 q-mb-md">3️⃣ Prueba de Envío de Notificación</div>

        <div class="q-mb-md">
          <p>Simula un pedido para probar si la notificación se envía correctamente.</p>
        </div>

        <q-btn
          color="primary"
          label="Enviar Notificación de Prueba"
          @click="enviarNotificacionPrueba"
          :disable="estadoConexion !== 1"
          class="q-mr-md"
        />
        <q-btn
          v-if="notificacionEnviada"
          color="positive"
          label="✅ Notificación Enviada"
          disable
        />

        <div v-if="errorEnvio" class="q-mt-md text-negative">
          <strong>Error:</strong> {{ errorEnvio }}
        </div>
      </q-card-section>
    </q-card>

    <!-- Receptoras de Notificaciones -->
    <q-card class="q-mb-lg">
      <q-card-section>
        <div class="text-h6 q-mb-md">4️⃣ Notificaciones Recibidas</div>

        <div v-if="notificacionesRecibidas.length === 0" class="text-grey-6">
          No se han recibido notificaciones aún.
        </div>
        <div v-else>
          <div class="text-subtitle2 q-mb-md">Última notificación:</div>
          <q-card flat bordered class="bg-blue-1 q-mb-md">
            <q-card-section>
              <div><strong>Código:</strong> {{ notificacionesRecibidas[0].Codigo }}</div>
              <div><strong>Cliente:</strong> {{ notificacionesRecibidas[0].Cliente }}</div>
              <div><strong>Total:</strong> ${{ notificacionesRecibidas[0].Total }}</div>
              <div><strong>Productos:</strong> {{ notificacionesRecibidas[0].CantidadProductos }}</div>
              <div><strong>Estado:</strong> {{ notificacionesRecibidas[0].Estado }}</div>
            </q-card-section>
          </q-card>

          <div class="text-subtitle2 q-mb-md">Historial (últimas 5):</div>
          <q-list bordered separator>
            <q-item v-for="(notif, idx) in notificacionesRecibidas.slice(0, 5)" :key="idx">
              <q-item-section>
                <q-item-label><strong>#{{ notif.Codigo }}</strong> - {{ notif.Cliente }}</q-item-label>
                <q-item-label caption>${{ notif.Total }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </div>
      </q-card-section>
    </q-card>

    <!-- Logs de Consola -->
    <q-card>
      <q-card-section>
        <div class="text-h6 q-mb-md">5️⃣ Instrucciones de Debugging</div>

        <div class="q-mb-md">
          <strong>Paso 1: Verificar conexión</strong>
          <ul>
            <li>Abre la consola (F12)</li>
            <li>Verifica que veas: "✅ Conectado exitosamente a SignalR"</li>
            <li>Si ves error, el problema está en la conexión</li>
          </ul>
        </div>

        <div class="q-mb-md">
          <strong>Paso 2: Verificar rol en token</strong>
          <ul>
            <li>En esta página deberías ver tu rol</li>
            <li>Si es "Administrador" o "Vendedor" → OK</li>
            <li>Si es "Cliente" → No recibirás notificaciones (este es el usuario actual)</li>
          </ul>
        </div>

        <div class="q-mb-md">
          <strong>Paso 3: Simular pedido</strong>
          <ul>
            <li>Este usuario debe ser Admin/Vendedor para recibir</li>
            <li>Abre otra pestaña como Cliente</li>
            <li>Confirma un pedido</li>
            <li>Deberías ver la notificación aquí ↑</li>
          </ul>
        </div>

        <div class="bg-orange-1 q-pa-md rounded-borders q-mt-md">
          <strong>⚠️ Problema Común:</strong>
          <p>Si la notificación no llega:</p>
          <ol>
            <li>Verifica en F12 que "NotificarNuevoPedido" sea invocado sin errores</li>
            <li>Verifica que el backend tenga el hub implementado</li>
            <li>Verifica que el rol del admin esté en la BD como "Administrador"</li>
            <li>Verifica que ambas ventanas estén en el mismo dominio/puerto</li>
          </ol>
        </div>
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import signalRService from 'src/services/signalRService'
import { Success, Error as ErrorNotify } from 'src/assets/js/util/notify'

const estadoConexion = ref(0)
const userData = ref(null)
const notificacionesRecibidas = ref([])
const notificacionEnviada = ref(false)
const errorEnvio = ref('')
const connectionId = ref('')
const urlHub = 'https://localhost:6005/pedidosHub'

const textoEstado = computed(() => {
  const estados = {
    0: '❌ Desconectado',
    1: '✅ Conectado',
    2: '🔄 Reconectando',
    3: '⏳ Conectando'
  }
  return estados[estadoConexion.value] || 'Desconocido'
})

const esAdminVendedor = computed(() => {
  if (!userData.value) return false
  const role = userData.value.role?.toLowerCase()
  return role === 'admin' || role === 'administrador' || role === 'vendedor'
})

function actualizarEstado() {
  estadoConexion.value = signalRService.getConnectionState()
  connectionId.value = signalRService.connection?.connectionId || 'No disponible'
  //console.log('Estado actualizado:', textoEstado.value)
}

function extraerDatosUsuario() {
  try {
    const token = localStorage.getItem('token') || sessionStorage.getItem('token')
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]))


      userData.value = {
        id: payload.Id,
        email: payload.Correo,
        role: payload.Rol,
        name: payload.NombreCompleto
      }
     // console.log('Datos del usuario extraídos:', userData.value)
    }
  } catch (error) {
    console.error('Error extrayendo datos del usuario:', error)
  }
}

async function reconectar() {
  try {
   // console.log('Reconectando...')
    await signalRService.disconnect()
    await signalRService.connect()
    actualizarEstado()
    Success('Reconectado a SignalR')
  } catch (error) {
    ErrorNotify('Error al reconectar: ' + error.message)
    console.error('Error reconectando:', error)
  }
}

async function enviarNotificacionPrueba() {
  try {
    notificacionEnviada.value = false
    errorEnvio.value = ''

    const notificacionPrueba = {
      pedidoId: 'prueba-' + Date.now(),
      codigo: '#99999',
      total: 299.99,
      cliente: 'Cliente Prueba',
      cantidadProductos: 3,
      estado: 'Pendiente',
      fecha: new Date().toISOString(),
      productos: [
        {
          nombre: 'Producto Prueba 1',
          cantidad: 1,
          precio: 99.99
        },
        {
          nombre: 'Producto Prueba 2',
          cantidad: 2,
          precio: 100.00
        }
      ]
    }

    //console.log('Enviando notificación de prueba:', notificacionPrueba)

    // Disparar evento personalizado
    const event = new CustomEvent('nuevoPedidoConfirmado', {
      detail: notificacionPrueba
    })
    window.dispatchEvent(event)

    notificacionEnviada.value = true
    Success('Notificación de prueba enviada')

    // Resetear después de 3 segundos
    setTimeout(() => {
      notificacionEnviada.value = false
    }, 3000)
  } catch (error) {
    errorEnvio.value = error.message
    console.error('Error enviando notificación de prueba:', error)
  }
}

onMounted(async () => {
  //console.log('🔍 Iniciando diagnóstico de notificaciones...')

  // Conectar a SignalR
  try {
    await signalRService.connect()
   // console.log('✅ Conectado a SignalR')
  } catch (error) {
    console.error('❌ Error conectando a SignalR:', error)
  }

  // Actualizar estado
  actualizarEstado()

  // Extraer datos del usuario
  extraerDatosUsuario()

  // Escuchar notificaciones
  signalRService.onPedidoGenerado((data) => {
    console.log('📦 Notificación recibida:', data)
    notificacionesRecibidas.value.unshift(data)
    if (notificacionesRecibidas.value.length > 10) {
      notificacionesRecibidas.value.pop()
    }
    Success(`🛒 Nuevo Pedido: #${data.Codigo}`)
  })

  // Actualizar estado cada 2 segundos
  const intervalId = setInterval(() => {
    actualizarEstado()
  }, 2000)

  onUnmounted(() => {
    clearInterval(intervalId)
  })
})
</script>

<style scoped>
code {
  background-color: #f0f0f0;
  padding: 2px 6px;
  border-radius: 3px;
  font-family: 'Courier New', monospace;
  font-size: 12px;
}

ul {
  margin-left: 20px;
}
</style>
