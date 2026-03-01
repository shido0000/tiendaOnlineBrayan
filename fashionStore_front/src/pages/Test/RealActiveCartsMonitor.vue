<template>
  <div class="q-pa-md">
    <q-card class="bg-info text-white">
      <q-card-section>
        <div class="text-h6">
          <q-icon name="database" class="q-mr-md" />
          Monitor de Carritos Activos
        </div>
        <div class="text-caption q-mt-sm">
          Cargando datos de la base de datos en tiempo real
        </div>
      </q-card-section>
    </q-card>

    <div class="q-mt-lg row q-col-gutter-md">
      <!-- Panel de control -->
      <div class="col-xs-12 col-md-3">
        <q-card>
          <q-card-section>
            <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="settings" class="q-mr-sm" />
              Controles
            </div>

            <q-btn
              color="primary"
              label="Cargar Datos"
              icon="refresh"
              class="full-width q-mb-md"
              @click="loadCartData"
              :loading="isLoading"
            />

            <q-btn
              color="warning"
              label="Autorecargar"
              icon="repeat"
              :color="autoRefreshEnabled ? 'positive' : 'warning'"
              class="full-width q-mb-md"
              @click="toggleAutoRefresh"
            />

            <q-btn
              color="negative"
              label="Vaciar Datos"
              icon="delete"
              class="full-width q-mb-md"
              @click="clearData"
            />

            <q-separator class="q-my-md" />

            <div class="text-subtitle2 text-weight-bold q-mb-md">Estado</div>

            <div class="q-mb-md">
              <q-chip
                :color="dataLoaded ? 'positive' : 'warning'"
                text-color="white"
                icon="circle"
                :label="`Datos: ${dataLoaded ? 'Cargados' : 'Vacíos'}`"
              />
            </div>

            <div class="q-mb-md">
              <q-chip
                :color="autoRefreshEnabled ? 'positive' : 'grey-5'"
                :text-color="autoRefreshEnabled ? 'white' : 'black'"
                icon="refresh"
                :label="`Actualización: ${autoRefreshEnabled ? 'Cada ' + autoRefreshInterval / 1000 + 's' : 'Manual'}`"
              />
            </div>

            <q-list dense>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Usuarios con carrito</q-item-label>
                  <q-item-label class="text-h6">{{ activeUsers.length }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Valor total</q-item-label>
                  <q-item-label class="text-h6">${{ totalCartValue.toFixed(2) }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Items totales</q-item-label>
                  <q-item-label class="text-h6">{{ totalItems }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>

            <q-separator class="q-my-md" />

            <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="info" class="q-mr-sm" />
              Información
            </div>

            <q-list dense>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Última actualización</q-item-label>
                  <q-item-label class="text-caption">{{ lastUpdateTime }}</q-item-label>
                </q-item-section>
              </q-item>
              <!--<q-item>
                <q-item-section>
                  <q-item-label caption>Conexión SignalR</q-item-label>
                  <q-item-label>
                    <q-badge
                      :color="signalRConnected ? 'positive' : 'negative'"
                      :label="signalRConnected ? 'Conectado' : 'Desconectado'"
                    />
                  </q-item-label>
                </q-item-section>
              </q-item>-->
            </q-list>

            <q-separator class="q-my-md" />

           <!-- <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="bug_report" class="q-mr-sm" />
              Debug
            </div>

            <q-checkbox
              v-model="showConsoleLog"
              label="Logs en consola"
              size="sm"
            />

            <q-btn
              flat
              dense
              color="primary"
              label="Ver datos en consola"
              icon="code"
              class="full-width q-mt-md text-caption"
              @click="logDataToConsole"
            />-->
          </q-card-section>
        </q-card>
      </div>

      <!-- Contenido principal -->
      <div class="col-xs-12 col-md-9">
        <!-- Estado de carga -->
        <q-linear-progress v-if="isLoading" indeterminate color="primary" class="q-mb-md" />

        <!-- Error -->
        <q-card v-if="errorMessage" class="bg-negative text-white q-mb-md">
          <q-card-section>
            <div class="text-weight-bold">
              <q-icon name="error" class="q-mr-sm" />
              Error al cargar datos
            </div>
            <div class="text-caption q-mt-sm">{{ errorMessage }}</div>
          </q-card-section>
        </q-card>

        <!-- Sin datos -->
        <q-card v-if="!isLoading && activeUsers.length === 0" class="bg-warning text-white">
          <q-card-section>
            <div class="text-weight-bold">
              <q-icon name="info" class="q-mr-sm" />
              Sin datos disponibles
            </div>
            <div class="text-caption q-mt-sm">
              Haz clic en "Cargar Datos" para obtener los carritos activos de la base de datos
            </div>
          </q-card-section>
        </q-card>

        <!-- Monitor de carritos activos -->
        <q-card v-if="activeUsers.length > 0">
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">
              <q-icon name="shopping_cart" class="q-mr-sm" />
              Usuarios con Carrito Activo
            </div>
            <div class="text-caption q-mt-xs">
              {{ activeUsers.length }} usuario{{ activeUsers.length !== 1 ? 's' : '' }} con carrito en la base de datos
            </div>
          </q-card-section>

          <q-separator />

          <q-list separator>
            <q-item
              v-for="user in activeUsers"
              :key="user.usuarioId"
              clickable
              :class="user.cartCount > 3 ? 'bg-deep-orange-1' : 'bg-light-green-1'"
            >
              <q-item-section avatar>
                <q-avatar
                  :color="getAvatarColor(user.usuarioId)"
                  text-color="white"
                  :label="`${user.nombre?.charAt(0) || 'U'}${user.apellido?.charAt(0) || 'S'}`"
                />
              </q-item-section>

              <q-item-section>
                <q-item-label class="text-weight-bold">
                  {{ user.nombre }} {{ user.apellido }}
                </q-item-label>
                <q-item-label caption>{{ user.email }}</q-item-label>
                <q-item-label caption class="text-grey-7">
                  Teléfono: {{ user.telefono }}
                </q-item-label>
              </q-item-section>

              <q-item-section side top>
                <div class="text-center">
                  <q-badge
                    :color="user.cartCount > 3 ? 'deep-orange' : 'positive'"
                    text-color="white"
                    rounded
                  >
                    {{ user.cartCount }} items
                  </q-badge>
                  <div class="text-caption text-grey">
                    ${{ user.cartTotal.toFixed(2) }}
                  </div>
                </div>
              </q-item-section>
            </q-item>
          </q-list>

          <!-- Detalles expandibles de cada usuario -->
          <div v-if="activeUsers.length > 0">
            <q-expansion-item
              v-for="(user, idx) in activeUsers"
              :key="`details-${user.usuarioId}`"
              :label="`Detalles: ${user.nombre} (${user.cartItems?.length || 0} productos)`"
              header-class="bg-grey-2"
              class="q-mt-md"
            >
              <q-card>
                <q-card-section v-if="user.cartItems && user.cartItems.length > 0">
                  <div class="overflow-auto">
                    <table class="full-width" style="border-collapse: collapse">
                      <thead>
                        <tr class="bg-grey-3">
                          <th class="text-left q-pa-md">Producto</th>
                          <th class="text-center q-pa-md">Variante</th>
                          <th class="text-right q-pa-md">Cantidad</th>
                          <th class="text-right q-pa-md">Precio</th>
                          <th class="text-right q-pa-md">Subtotal</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr
                          v-for="item in user.cartItems"
                          :key="item.id"
                          style="border-bottom: 1px solid #e0e0e0"
                        >
                          <td class="q-pa-md">
                            <div class="text-weight-bold">{{ item.nombre }}</div>
                            <div class="text-caption text-grey">Código: {{ item.codigo || 'N/A' }}</div>
                            <div class="text-caption text-grey">ID: {{ item.id }}</div>
                          </td>
                          <td class="text-center q-pa-md">
                            <q-chip size="sm" class="bg-blue-1">{{ item.productoId?.substring(0, 8) || 'N/A' }}</q-chip>
                          </td>
                          <td class="text-right q-pa-md">
                            <q-badge color="primary">{{ item.cantidad }}</q-badge>
                          </td>
                          <td class="text-right q-pa-md">${{ (item.unitPrice || 0).toFixed(2) }}</td>
                          <td class="text-right q-pa-md text-weight-bold">
                            ${{ (item.lineTotal || 0).toFixed(2) }}
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </q-card-section>

                <q-card-section v-else class="text-center text-grey">
                  Sin detalles de productos disponibles
                </q-card-section>
              </q-card>
            </q-expansion-item>
          </div>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, computed, ref, onMounted, onUnmounted } from 'vue'
import { useQuasar } from 'quasar'
import activeCartsService from 'src/services/activeCartsService'
import signalRService from 'src/services/signalRService'

const $q = useQuasar()

const activeUsers = ref([])
const isLoading = ref(false)
const dataLoaded = ref(false)
const errorMessage = ref('')
const autoRefreshEnabled = ref(false)
const autoRefreshInterval = ref(15000) // 15 segundos por defecto
const showConsoleLog = ref(false)
const lastUpdateTime = ref('Nunca')
const signalRConnected = ref(false)
let refreshInterval = null

const totalCartValue = computed(() => {
  return activeUsers.value.reduce((sum, user) => sum + (user.cartTotal || 0), 0)
})

const totalItems = computed(() => {
  return activeUsers.value.reduce((sum, user) => sum + (user.cartCount || 0), 0)
})

const getAvatarColor = (userId) => {
  const colors = [
    'red',
    'orange',
    'green',
    'blue',
    'indigo',
    'purple',
    'pink',
    'cyan',
    'teal'
  ]
  const hash = userId.charCodeAt(0) + userId.charCodeAt(userId.length - 1)
  return colors[hash % colors.length]
}

/**
 * Carga los carritos activos del backend
 */
const loadCartData = async () => {
  isLoading.value = true
  errorMessage.value = ''

  try {
    if (showConsoleLog.value) {
      console.log('📊 Cargando carritos activos del backend...')
    }

    const response = await activeCartsService.getActiveCarts()

    // El servicio retorna directamente el array transformado
    if (Array.isArray(response) && response.length > 0) {
      activeUsers.value = response
      dataLoaded.value = true
      lastUpdateTime.value = new Date().toLocaleTimeString()

      if (showConsoleLog.value) {
        console.log('✅ Carritos cargados:', activeUsers.value)
      }

      $q.notify({
        type: 'positive',
        message: `${activeUsers.value.length} carritos cargados exitosamente`,
        position: 'top',
        timeout: 2000
      })
    } else if (Array.isArray(response) && response.length === 0) {
      activeUsers.value = []
      dataLoaded.value = true
      errorMessage.value = 'No hay carritos activos en la base de datos'

      $q.notify({
        type: 'info',
        message: 'No hay carritos activos',
        position: 'top',
        timeout: 2000
      })
    } else {
      errorMessage.value = 'No se recibieron datos del servidor'

      $q.notify({
        type: 'warning',
        message: 'Error: Respuesta inválida del servidor',
        position: 'top',
        timeout: 2000
      })
    }
  } catch (error) {
    console.error('❌ Error cargando carritos:', error)
    errorMessage.value =
      error.response?.data?.message ||
      error.message ||
      'Error al cargar los carritos activos'

    $q.notify({
      type: 'negative',
      message: 'Error: ' + errorMessage.value,
      position: 'top',
      timeout: 3000
    })
  } finally {
    isLoading.value = false
  }
}

/**
 * Alterna la recarga automática
 */
const toggleAutoRefresh = () => {
  autoRefreshEnabled.value = !autoRefreshEnabled.value

  if (autoRefreshEnabled.value) {
    startAutoRefresh()
    $q.notify({
      type: 'positive',
      message: `Autorecargar habilitado cada ${autoRefreshInterval.value / 1000}s`,
      position: 'top',
      timeout: 2000
    })
  } else {
    stopAutoRefresh()
    $q.notify({
      type: 'info',
      message: 'Autorecargar deshabilitado',
      position: 'top',
      timeout: 2000
    })
  }
}

/**
 * Inicia la recarga automática
 */
const startAutoRefresh = () => {
  refreshInterval = setInterval(() => {
    if (showConsoleLog.value) {
      console.log('🔄 Recargando datos automáticamente...')
    }
    loadCartData()
  }, autoRefreshInterval.value)
}

/**
 * Detiene la recarga automática
 */
const stopAutoRefresh = () => {
  if (refreshInterval) {
    clearInterval(refreshInterval)
    refreshInterval = null
  }
}

/**
 * Limpia los datos
 */
const clearData = () => {
  activeUsers.value = []
  dataLoaded.value = false
  errorMessage.value = ''
  stopAutoRefresh()
  autoRefreshEnabled.value = false
  lastUpdateTime.value = 'Nunca'

  $q.notify({
    type: 'info',
    message: 'Datos vaciados',
    position: 'top',
    timeout: 1500
  })
}

/**
 * Registra datos en consola para debug
 */
const logDataToConsole = () => {
  console.group('📊 DATOS DEL MONITOR DE CARRITOS')
  console.table(
    activeUsers.value.map((u) => ({
      'Usuario': `${u.nombre} ${u.apellido}`,
      'Email': u.email,
      'Items': u.cartCount,
      'Total': `$${u.cartTotal.toFixed(2)}`
    }))
  )
  console.log('Usuarios completos:', activeUsers.value)
  console.log('Valor total:', `$${totalCartValue.value.toFixed(2)}`)
  console.log('Total items:', totalItems.value)
  console.groupEnd()
}

/**
 * Maneja actualizaciones en tiempo real por SignalR
 */
const handleRealtimeUpdate = (updateData) => {
  if (showConsoleLog.value) {
    console.log('🔔 Actualización en tiempo real recibida:', updateData)
  }

  // Buscar usuario en lista y actualizar
  const userIndex = activeUsers.value.findIndex((u) => u.usuarioId === updateData.usuarioId)

  if (userIndex !== -1) {
    // Usuario existe, actualizar sus datos
    activeUsers.value[userIndex] = {
      ...activeUsers.value[userIndex],
      cartCount: updateData.totalItems || activeUsers.value[userIndex].cartCount,
      cartTotal: updateData.totalValue || activeUsers.value[userIndex].cartTotal
    }
  } else if (updateData.totalItems > 0) {
    // Nuevo usuario con carrito
    activeUsers.value.push({
      usuarioId: updateData.usuarioId,
      nombre: updateData.nombre || 'Usuario',
      apellido: updateData.apellido || 'Desconocido',
      email: updateData.email || 'N/A',
      cartCount: updateData.totalItems || 0,
      cartItems: [],
      cartTotal: updateData.totalValue || 0
    })
  }

  lastUpdateTime.value = new Date().toLocaleTimeString()
}

/**
 * Lifecycle
 */
onMounted(() => {
  // Verificar conexión SignalR
  if (window.__signalRConnected !== undefined) {
    signalRConnected.value = window.__signalRConnected
  }

  // Escuchar evento de carrito actualizado desde SignalR
  try {
    if (signalRService && typeof signalRService.on === 'function') {
      signalRService.on('CarritoActualizado', handleRealtimeUpdate)

      if (showConsoleLog.value) {
        console.log('✅ Escuchador SignalR registrado para CarritoActualizado')
      }
    }
  } catch (error) {
    console.warn('⚠️ No se pudo registrar escuchador SignalR:', error)
  }

  // Cargar datos iniciales
  loadCartData()
})

onUnmounted(() => {
  stopAutoRefresh()

  // Desuscribirse de eventos
  try {
    if (signalRService && typeof signalRService.off === 'function') {
      signalRService.off('CarritoActualizado', handleRealtimeUpdate)
    }
  } catch (error) {
    console.warn('⚠️ Error desuscribiendo:', error)
  }
})
</script>

<style scoped lang="scss">
code {
  background-color: #f5f5f5;
  padding: 2px 6px;
  border-radius: 3px;
  font-family: 'Courier New', monospace;
  font-size: 0.85em;
}

table {
  font-size: 0.9em;
}

th, td {
  border: 1px solid #e0e0e0;
}
</style>
