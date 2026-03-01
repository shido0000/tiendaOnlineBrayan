<template style="background-color: #686262">
  <q-layout view="hHh Lpr lff" id="mainLayout">
    <q-header class="text-white" style="height: 60px">
      <q-toolbar class="ll">
        <div class="h row">
          <q-btn
            size="md"
            class="qqq"
            flat
            @click="drawer = !drawer"
            round
            dense
            icon="menu"
          >
            <q-tooltip>{{ drawer ? "Cerrar Menú" : "Abrir Menú" }}</q-tooltip>
          </q-btn>
          <q-toolbar-title class="text-subtitle6 text-white">
            Bryma
          </q-toolbar-title>
        </div>
        <q-space />

      <!-- Botón de pedidos pendientes SOLO si rol es admin o vendedor -->
        <q-btn
          v-if="userRole === 'administrador' || userRole === 'vendedor'" flat round dense icon="shopping_cart" class="q-mr-sm">
          <q-badge color="red" floating>{{ pedidos.length }}</q-badge>
          <q-tooltip>Pedidos pendientes</q-tooltip>

          <!-- Menú desplegable -->
          <q-menu>
            <q-list style="min-width: 250px">
              <q-item v-for="pedido in pedidos" :key="pedido.codigo" clickable>
                <q-item-section>
                  <div>
                    <strong>Pedido #{{ pedido.codigo }}</strong>
                    <div class="text-caption">Monto: {{ pedido.total.toFixed(2) }}</div>
                    <div class="text-caption">
                      Fecha: {{ dayjs(pedido.fechaCreado).format("DD/MM/YYYY HH:mm") }}
                    </div>
                    <div class="text-caption">Estado: {{ pedido.estado || 'Pendiente' }}</div>
                  </div>
                </q-item-section>
                <q-item-section side>
                  <q-btn
                    dense
                    flat
                    round
                    icon="close"
                    color="negative"
                    @click.stop="eliminarPedido(pedido.codigo)"
                  />
                </q-item-section>
              </q-item>
              <q-item v-if="pedidos.length === 0">
                <q-item-section>No hay pedidos pendientes</q-item-section>
              </q-item>
            </q-list>
          </q-menu>
        </q-btn>

        <!-- Botón de Logout -->
        <q-btn
          class="q-mr-xl"
          flat
          round
          dense
          icon="logout"
          @click="onLogout"
        >
          <q-tooltip>Salir</q-tooltip>
        </q-btn>
      </q-toolbar>
    </q-header>

    <!-- Drawer lateral -->
    <q-drawer
      v-model="drawer"
      show-if-above
      :width="275"
      :breakpoint="500"
      class="fondo"
    >
      <q-scroll-area class="fit">
        <q-list padding class="qq menu-list">
          <EssentialLink />
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, onMounted, onUnmounted, ref } from 'vue'
import { emp } from 'src/boot/axios'
import EssentialLink from 'components/EssentialLink.vue'
import { useRouter } from 'vue-router'
import { loadGetHastaData } from 'src/assets/js/util/funciones'
import dayjs from 'dayjs'
import signalRService from 'src/services/SignalRService'

export default defineComponent({
  name: 'MainLayout',
  components: { EssentialLink },

  setup () {
    const router = useRouter()
    const drawer = ref(false)
    const pedidos = ref([])
    const userRole = ref(null)

    // Decodificar token y obtener rol
    const decodeToken = () => {
      try {
        const token = localStorage.getItem('token') || sessionStorage.getItem('token')
        if (token) {
          const payload = JSON.parse(atob(token.split('.')[1]))
          userRole.value = payload.Rol?.toLowerCase()
          console.log("userRole.value: ",userRole.value)
        }
      } catch (err) {
        console.error('Error decodificando token:', err)
      }
    }

    const onLogout = () => {
      localStorage.removeItem('token')
      localStorage.removeItem('token_exp')
      sessionStorage.clear()
      router.push('/login')
    }

    const eliminarPedido = (codigo) => {
      pedidos.value = pedidos.value.filter(p => p.codigo !== codigo)
    }

    onMounted(async () => {
        decodeToken()
      pedidos.value = await loadGetHastaData('Pedido/ObtenerPedidosPendientes') ?? []

      try {
        await signalRService.connect()

        // Nuevo pedido (solo si es pendiente)
        signalRService.onPedidoGenerado((data) => {
          if (data.Estado?.toLowerCase() === 'pendiente') {
            pedidos.value.push({
              codigo: data.Codigo,
              total: data.Total,
              fechaCreado: data.Fecha,
              estado: data.Estado
            })
          }
        })

        // Pedido actualizado
        window.addEventListener('pedido-actualizado', (event) => {
          const actualizado = event.detail
          console.log('Evento de pedido actualizado recibido:', actualizado)

          const index = pedidos.value.findIndex(p => p.codigo === actualizado.Codigo)
          const estado = actualizado.Estado?.toLowerCase()

          if (estado === 'pendiente') {
            if (index !== -1) {
              pedidos.value[index] = {
                ...pedidos.value[index],
                estado: actualizado.Estado,
                fechaCreado: actualizado.Timestamp
              }
            } else {
              pedidos.value.push({
                codigo: actualizado.Codigo,
                total: actualizado.Total,
                fechaCreado: actualizado.Timestamp,
                estado: actualizado.Estado
              })
            }
          } else {
            if (index !== -1) {
              pedidos.value.splice(index, 1)
              console.log(`Pedido ${actualizado.Codigo} eliminado de pendientes (estado: ${actualizado.Estado})`)
            }
          }
        })

        // Pedido cancelado
        window.addEventListener('PedidoCancelado', (event) => {
          const cancelado = event.detail
          pedidos.value = pedidos.value.filter(p => p.codigo !== cancelado.Codigo)
        })
      } catch (err) {
        console.error('Error conectando a SignalR:', err)
      }
    })

    onUnmounted(() => {
      signalRService.disconnect()
    })

    return {
      emp,
      drawer,
      pedidos,
      eliminarPedido,
      onLogout,
      dayjs,
      userRole
    }
  }
})
</script>

<style>
.h {
  justify-content: space-between;
}

.fondo {
  background-color: white;
}

.fondo.custom-drawer {
  transition: transform 1s ease-in-out;
}
</style>
