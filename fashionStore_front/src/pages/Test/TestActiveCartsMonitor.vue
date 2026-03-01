<template>
  <div class="q-pa-md">
    <q-card class="bg-info text-white">
      <q-card-section>
        <div class="text-h6">
          <q-icon name="info" class="q-mr-md" />
          Testing del Monitor de Carritos Activos
        </div>
        <div class="text-caption q-mt-sm">
          Usa este componente para probar el monitor con datos mock antes de implementar el backend
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
              label="Cargar Datos Mock"
              icon="download"
              class="full-width q-mb-md"
              @click="loadMockData"
            />

            <q-btn
              v-if="autoUpdateEnabled"
              color="warning"
              label="Detener Simulación"
              icon="pause"
              class="full-width q-mb-md"
              @click="stopAutoUpdate"
            />
            <q-btn
              v-else
              color="positive"
              label="Iniciar Simulación"
              icon="play_arrow"
              class="full-width q-mb-md"
              @click="startAutoUpdate"
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
                :color="autoUpdateEnabled ? 'positive' : 'warning'"
                text-color="white"
                icon="circle"
                :label="`Simulación: ${autoUpdateEnabled ? 'Activa' : 'Pausada'}`"
              />
            </div>

            <q-list dense>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Usuarios con carrito</q-item-label>
                  <q-item-label class="text-h6">{{ mockUsers.length }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Valor total</q-item-label>
                  <q-item-label class="text-h6">${{ totalMockValue.toFixed(2) }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Items totales</q-item-label>
                  <q-item-label class="text-h6">{{ totalMockItems }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>

            <q-separator class="q-my-md" />

            <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="bug_report" class="q-mr-sm" />
              Debug
            </div>

            <q-checkbox
              v-model="showConsoleLog"
              label="Ver logs en consola"
              size="sm"
            />

            <q-checkbox
              v-model="useMockData"
              label="Usar datos mock en vez de API"
              size="sm"
            />
          </q-card-section>
        </q-card>
      </div>

      <!-- Monitor principal -->
      <div class="col-xs-12 col-md-9">
        <div v-if="useMockData && mockUsers.length > 0">
          <!-- Mostrar componente real con datos mock -->
          <div class="q-mb-md">
            <q-card class="bg-light-green-1">
              <q-card-section class="q-pa-md">
                <div class="text-weight-bold text-positive">
                  ✅ Usando datos mock
                </div>
              </q-card-section>
            </q-card>
          </div>

          <!-- Monitor simulado (pueden reemplazar con componente real) -->
          <q-card>
            <q-card-section class="bg-primary text-white">
              <div class="text-h6">
                <q-icon name="shopping_cart" class="q-mr-sm" />
                Usuarios con Carrito Activo (SIMULACIÓN)
              </div>
              <div class="text-caption q-mt-xs">
                {{ mockUsers.length }} usuario{{ mockUsers.length !== 1 ? 's' : '' }} con carrito
              </div>
            </q-card-section>

            <q-separator />

            <q-list separator>
              <q-item
                v-for="user in mockUsers"
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

            <!-- Tabla de ítems de un usuario -->
            <q-expansion-item
              v-for="user in mockUsers.slice(0, 2)"
              :key="`details-${user.usuarioId}`"
              :label="`Detalles: ${user.nombre} (${user.cartItems.length} productos)`"
              header-class="bg-grey-2"
              class="q-mt-md"
            >
              <q-card>
                <q-card-section>
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
                            <div class="text-caption text-grey">ID: {{ item.id }}</div>
                          </td>
                          <td class="text-center q-pa-md">
                            <q-chip size="sm" class="bg-blue-1">{{ item.variante }}</q-chip>
                          </td>
                          <td class="text-right q-pa-md">
                            <q-badge color="primary">{{ item.cantidad }}</q-badge>
                          </td>
                          <td class="text-right q-pa-md">${{ item.precio.toFixed(2) }}</td>
                          <td class="text-right q-pa-md text-weight-bold">
                            ${{ (item.precio * item.cantidad).toFixed(2) }}
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </q-card-section>
              </q-card>
            </q-expansion-item>
          </q-card>
        </div>

        <div v-else class="q-gutter-md">
          <q-card class="bg-warning text-white">
            <q-card-section>
              <div class="text-weight-bold">
                ⚠️ Sin datos
              </div>
              <div class="text-caption q-mt-sm">
                Haz clic en "Cargar Datos Mock" para ver la demostración
              </div>
            </q-card-section>
          </q-card>

          <!-- Instrucciones -->
          <q-card>
            <q-card-section>
              <div class="text-subtitle1 text-weight-bold q-mb-md">
                Instrucciones de Testing
              </div>

              <q-stepper v-model="step" animated flat>
                <q-step name="1" title="Carga datos" icon="download" :done="step > 1">
                  <div class="q-mb-md">
                    Haz clic en el botón "Cargar Datos Mock" para cargar datos de ejemplo
                  </div>
                </q-step>

                <q-step name="2" title="Visualiza" icon="visibility" :done="step > 2">
                  <div class="q-mb-md">
                    Verás la lista de usuarios con sus carritos activos, datos personales y
                    productos en carrito
                  </div>
                </q-step>

                <q-step name="3" title="Simula cambios" icon="update">
                  <div class="q-mb-md">
                    Inicia la simulación para ver cómo se actualiza en tiempo real cuando los
                    usuarios modifican sus carritos
                  </div>
                </q-step>

                <q-step name="4" title="Backend" icon="storage">
                  <div class="q-mb-md">
                    Una vez el backend esté listo, desactiva "Usar datos mock" para usar datos
                    reales
                  </div>
                </q-step>

                <template v-slot:navigation>
                  <div class="q-mt-md">
                    <q-btn
                      v-if="step > 1"
                      color="primary"
                      label="Atrás"
                      @click="step--"
                      class="q-mr-md"
                    />
                  </div>
                </template>
              </q-stepper>

              <q-separator class="q-my-lg" />

              <div class="text-subtitle2 text-weight-bold q-mb-md">Flujo Backend Requerido:</div>
              <ol class="text-caption">
                <li>
                  Implementar endpoint <code>GET /api/usuarios/carritos-activos</code>
                </li>
                <li>
                  Implementar evento SignalR <code>CarritoActualizado</code>
                </li>
                <li>
                  Agregar usuarios al grupo "AdminVendedores" en SignalR
                </li>
                <li>
                  Emitir eventos cuando se modifique un carrito
                </li>
              </ol>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, computed, ref, onMounted, onUnmounted } from 'vue'
import { MOCK_ACTIVE_CARTS, createMockCartUpdate } from 'src/assets/js/util/mockActiveCartsData'

const step = ref(1)
const mockUsers = ref([])
const autoUpdateEnabled = ref(false)
const showConsoleLog = ref(false)
const useMockData = ref(false)
let updateInterval = null

const totalMockValue = computed(() => {
  return mockUsers.value.reduce((sum, user) => sum + user.cartTotal, 0)
})

const totalMockItems = computed(() => {
  return mockUsers.value.reduce((sum, user) => sum + user.cartCount, 0)
})

const getAvatarColor = (userId) => {
  const colors = ['red', 'orange', 'green', 'blue', 'indigo', 'purple', 'pink', 'cyan', 'teal']
  const hash = userId.charCodeAt(0) + userId.charCodeAt(userId.length - 1)
  return colors[hash % colors.length]
}

const loadMockData = () => {
  mockUsers.value = JSON.parse(JSON.stringify(MOCK_ACTIVE_CARTS))
  useMockData.value = true
  step.value = 2
  if (showConsoleLog.value) {
    console.log('Mock data loaded:', mockUsers.value)
  }
}

const clearData = () => {
  mockUsers.value = []
  useMockData.value = false
  stopAutoUpdate()
  step.value = 1
}

const startAutoUpdate = () => {
  autoUpdateEnabled.value = true
  let updateCount = 0

  updateInterval = setInterval(() => {
    const update = createMockCartUpdate()

    // Buscar y actualizar usuario
    const userIdx = mockUsers.value.findIndex((u) => u.usuarioId === update.usuarioId)
    if (userIdx !== -1) {
      mockUsers.value[userIdx] = {
        ...mockUsers.value[userIdx],
        ...update
      }

      if (showConsoleLog.value) {
        console.log(`Carrito actualizado [${++updateCount}]:`, update)
      }
    }
  }, 3000) // Actualizar cada 3 segundos
}

const stopAutoUpdate = () => {
  autoUpdateEnabled.value = false
  if (updateInterval) {
    clearInterval(updateInterval)
  }
}

onUnmounted(() => {
  stopAutoUpdate()
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

ol {
  padding-left: 20px;
  margin: 10px 0;

  li {
    margin: 8px 0;
  }
}
</style>
