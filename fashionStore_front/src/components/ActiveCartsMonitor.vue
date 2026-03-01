<template>
  <div class="active-carts-monitor">
    <!-- Encabezado -->
    <q-card class="q-mb-md">
      <q-card-section class="bg-primary text-white">
        <div class="row items-center justify-between">
          <div>
            <div class="text-h6">
              <q-icon name="shopping_cart" class="q-mr-sm" />
              Usuarios con Carrito Activo
            </div>
            <div class="text-caption q-mt-xs">
              Monitoreo en tiempo real de carritos de compra
            </div>
          </div>
          <div class="text-right">
            <div class="text-h5 text-white">{{ activeUsers.length }}</div>
            <div class="text-caption">usuario{{ activeUsers.length !== 1 ? 's' : '' }}</div>
          </div>
        </div>
      </q-card-section>
    </q-card>

    <!-- Vista vacía -->
    <q-card v-if="activeUsers.length === 0" class="bg-light-blue-1">
      <q-card-section class="text-center q-pa-lg">
        <q-icon name="shopping_cart_empty" size="48px" color="grey-5" class="q-mb-md" />
        <div class="text-subtitle2 text-grey-6">
          No hay usuarios con carrito activo en este momento
        </div>
      </q-card-section>
    </q-card>

    <!-- Lista de usuarios con carritos -->
    <template v-if="activeUsers.length > 0">
      <q-expansion-item
        v-for="user in activeUsers"
        :key="user.usuarioId"
        :header-class="user.cartCount > 3 ? 'bg-deep-orange-1' : 'bg-light-green-1'"
        class="q-mb-md shadow-1"
      >
        <!-- Header del usuario -->
        <template v-slot:header>
          <q-item class="full-width">
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
                  class="text-caption text-weight-bold q-mb-xs"
                >
                  {{ user.cartCount }} {{ user.cartCount !== 1 ? 'items' : 'item' }}
                </q-badge>
                <div class="text-caption text-grey">
                  ${{ user.cartTotal.toFixed(2) }}
                </div>
              </div>
            </q-item-section>
          </q-item>
        </template>

        <!-- Contenido: Detalles del usuario y carrito -->
        <q-separator />

        <q-card-section class="q-pa-md">
          <!-- Información del usuario -->
          <div class="q-mb-lg">
            <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="person" class="q-mr-sm" />
              Información del Usuario
            </div>
            <q-list dense>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Email</q-item-label>
                  <q-item-label>{{ user.email }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Teléfono</q-item-label>
                  <q-item-label>{{ user.telefono || 'No registrado' }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-item-label caption>Última actividad</q-item-label>
                  <q-item-label>{{ formatDate(user.lastActivity) }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </div>

          <q-separator class="q-my-md" />

          <!-- Tabla de productos en carrito -->
          <div>
            <div class="text-subtitle2 text-weight-bold q-mb-md">
              <q-icon name="list" class="q-mr-sm" />
              Productos en Carrito
            </div>

            <div class="overflow-auto">
              <table class="full-width cart-table">
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
                  <tr v-for="(item, idx) in user.cartItems" :key="idx" class="hover-highlight">
                    <td class="q-pa-md">
                      <div class="row items-center no-wrap">
                        <q-img
                          v-if="item.foto"
                          :src="item.foto"
                          fit="cover"
                          class="rounded-borders q-mr-md"
                          style="width: 40px; height: 40px"
                          @error="item.foto = null"
                        />
                        <div>
                          <div class="text-weight-bold">{{ item.nombre }}</div>
                          <div class="text-caption text-grey">ID: {{ item.id }}</div>
                        </div>
                      </div>
                    </td>
                    <td class="text-center q-pa-md">
                      <q-chip
                        v-if="item.variante"
                        size="sm"
                        class="bg-blue-1"
                      >
                        {{ item.variante }}
                      </q-chip>
                      <span v-else class="text-grey-5">-</span>
                    </td>
                    <td class="text-right q-pa-md">
                      <q-badge color="primary">{{ item.cantidad }}</q-badge>
                    </td>
                    <td class="text-right q-pa-md">
                      ${{ item.precio.toFixed(2) }}
                    </td>
                    <td class="text-right q-pa-md text-weight-bold">
                      ${{ (item.precio * item.cantidad).toFixed(2) }}
                    </td>
                  </tr>
                </tbody>
                <tfoot>
                  <tr class="bg-amber-2 text-weight-bold">
                    <td colspan="3" class="q-pa-md text-right">Total del Carrito:</td>
                    <td class="q-pa-md text-right"></td>
                    <td class="text-right q-pa-md">
                      <span class="text-h6 text-primary">${{ user.cartTotal.toFixed(2) }}</span>
                    </td>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>

          <!-- Acciones -->
          <div class="q-mt-lg row justify-end gap-md">
            <q-btn
              flat
              color="primary"
              label="Contactar Usuario"
              icon="mail"
              @click="contactUser(user)"
              size="sm"
            />
            <q-btn
              flat
              color="positive"
              label="Ver Perfil"
              icon="open_in_new"
              @click="viewUserProfile(user)"
              size="sm"
            />
          </div>
        </q-card-section>
      </q-expansion-item>
    </template>

    <!-- Leyenda de colores -->
    <q-card class="q-mt-lg bg-grey-2" v-if="activeUsers.length > 0">
      <q-card-section class="q-pa-md">
        <div class="text-subtitle2 text-weight-bold q-mb-md">Leyenda</div>
        <div class="row q-col-gutter-md">
          <div class="col-xs-12 col-sm-6">
            <div class="row items-center">
              <div class="bg-deep-orange-1 rounded-borders" style="width: 20px; height: 20px;"></div>
              <span class="q-ml-md text-caption">Carrito con más de 3 items (prioritario)</span>
            </div>
          </div>
          <div class="col-xs-12 col-sm-6">
            <div class="row items-center">
              <div class="bg-light-green-1 rounded-borders" style="width: 20px; height: 20px;"></div>
              <span class="q-ml-md text-caption">Carrito con 1-3 items</span>
            </div>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { reactive, computed, onMounted, onUnmounted } from 'vue'
import { api } from 'src/boot/axios'
import activeCartsService from 'src/services/activeCartsService'
import signalRService from 'src/services/signalRService'
import { Notify } from 'quasar'

const state = reactive({
  activeUsers: [],
  loading: false,
  error: null
})

const activeUsers = computed(() => state.activeUsers)

// Colores consistentes para avatares
const getAvatarColor = (userId) => {
  const colors = ['red', 'orange', 'green', 'blue', 'indigo', 'purple', 'pink', 'cyan', 'teal']
  const hash = userId.charCodeAt(0) + userId.charCodeAt(userId.length - 1)
  return colors[hash % colors.length]
}

// Formatear fecha
const formatDate = (date) => {
  if (!date) return 'N/A'
  try {
    const d = new Date(date)
    return d.toLocaleString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit'
    })
  } catch {
    return 'N/A'
  }
}

// Cargar datos iniciales
const loadActiveUsers = async () => {
  state.loading = true
  state.error = null
  try {
    state.activeUsers = await activeCartsService.getActiveCarts()
  } catch (error) {
    console.error('Error cargando usuarios con carritos activos:', error)
    state.error = 'Error al cargar los datos'
    Notify.create({
      type: 'negative',
      message: 'Error al cargar usuarios con carritos activos',
      timeout: 3000
    })
  } finally {
    state.loading = false
  }
}

// Actualizar carrito en tiempo real
const handleCarritoUpdated = (data) => {
  if (!data || !data.usuarioId) return

  const idx = state.activeUsers.findIndex(u => u.usuarioId === data.usuarioId)

  if (data.cartCount > 0) {
    if (idx !== -1) {
      // Actualizar usuario existente
      state.activeUsers[idx] = {
        ...state.activeUsers[idx],
        ...data,
        lastActivity: new Date().toISOString()
      }
    } else {
      // Agregar nuevo usuario
      state.activeUsers.push({
        ...data,
        lastActivity: new Date().toISOString()
      })
    }
  } else {
    // Remover usuario si su carrito está vacío
    if (idx !== -1) {
      state.activeUsers.splice(idx, 1)
    }
  }
}

// Contactar usuario
const contactUser = (user) => {
  Notify.create({
    type: 'info',
    message: `Abriendo opciones de contacto para ${user.nombre}...`,
    timeout: 2000
  })
  // Implementar lógica de contacto
}

// Ver perfil del usuario
const viewUserProfile = (user) => {
  Notify.create({
    type: 'info',
    message: `Abriendo perfil de ${user.nombre}...`,
    timeout: 2000
  })
  // Implementar lógica para ver perfil
}

onMounted(() => {
  loadActiveUsers()

  // Escuchar actualizaciones de carritos en tiempo real
  if (signalRService.connection) {
    signalRService.connection.on('CarritoActualizado', handleCarritoUpdated)
  }

  // Recargar cada 30 segundos como respaldo
  const intervalId = setInterval(loadActiveUsers, 30000)

  onUnmounted(() => {
    clearInterval(intervalId)
    if (signalRService.connection) {
      signalRService.connection.off('CarritoActualizado', handleCarritoUpdated)
    }
  })
})
</script>

<style scoped lang="scss">
.active-carts-monitor {
  .cart-table {
    border-collapse: collapse;

    tbody tr {
      border-bottom: 1px solid #e0e0e0;
      transition: background-color 0.2s;

      &.hover-highlight:hover {
        background-color: #f5f5f5;
      }
    }

    th, td {
      vertical-align: middle;
    }

    tfoot tr {
      border-top: 2px solid #bdbdbd;
    }
  }

  gap-md {
    gap: 12px;
  }
}
</style>
