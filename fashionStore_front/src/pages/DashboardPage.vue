<template>
  <div class="q-pa-md">

    <q-card class="q-pa-lg dashboard-card q-mt-md">
      <q-card-section>
        <div class="row items-center q-col-gutter-md">
          <div class="col">
            <div class="text-h5">Dashboard</div>
            <div class="text-subtitle2 q-mt-sm">Resumen administrativo</div>
          </div>
          <div class="col-auto">
            <q-btn flat label="Refrescar" @click="refresh" />
          </div>
        </div>
      </q-card-section>

      <q-separator />

      <q-card-section>
        <!-- Fila 1: Ventas -->
        <div class="row q-col-gutter-lg q-mb-lg">
          <div class="col-xs-12 col-sm-6 col-md-3" v-for="card in cardsFila1" :key="card.label">
            <q-card :class="`${card.color} text-white full-height`">
              <q-card-section class="text-center">
                <div class="text-h6">{{ card.label }}</div>
                <div class="text-h4 q-mt-sm">{{ card.value }}</div>
                <div v-if="card.caption" class="text-caption q-mt-sm">{{ card.caption }}</div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <!-- Fila 2: Clientes -->
        <div class="row q-col-gutter-lg q-mb-lg">
          <div class="col-xs-12 col-sm-6 col-md-3" v-for="card in cardsFila2" :key="card.label">
            <q-card :class="`${card.color} text-white full-height`">
              <q-card-section class="text-center">
                <div class="text-h6">{{ card.label }}</div>
                <div class="text-h4 q-mt-sm">{{ card.value }}</div>
                <div v-if="card.caption" class="text-caption q-mt-sm">{{ card.caption }}</div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <!-- Fila 3: Logística -->
        <div class="row q-col-gutter-lg q-mb-lg">
          <div class="col-xs-12 col-sm-6" v-for="card in cardsFila3" :key="card.label">
            <q-card :class="`${card.color} text-white full-height`">
              <q-card-section class="text-center">
                <div class="text-h6">{{ card.label }}</div>
                <div class="text-h4 q-mt-sm">{{ card.value }}</div>
                <div v-if="card.caption" class="text-caption q-mt-sm">{{ card.caption }}</div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <!-- Fila 4: Confirmados vs Cancelados -->
        <div class="row q-col-gutter-lg q-mb-lg">
          <div class="col-xs-12 col-sm-6" v-for="card in cardsFila4" :key="card.label">
            <q-card :class="`${card.color} text-white full-height`">
              <q-card-section class="text-center">
                <div class="text-h6">{{ card.label }}</div>
                <div class="text-h4 q-mt-sm">{{ card.value }}</div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <!-- Gráficas -->
        <div class="row q-col-gutter-lg">
          <div class="col-xs-12 col-md-6">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Ventas - Últimos 7 días</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div style="height:240px">
                  <canvas id="salesChart"></canvas>
                </div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-xs-12 col-md-6">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Top productos</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div style="height:240px">
                  <canvas id="topProductsChart"></canvas>
                </div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <!-- Gráfico donut y tendencia -->
        <div class="row q-col-gutter-lg q-mb-lg q-mt-xs">
          <div class="col-xs-12 col-md-6">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Pedidos Confirmados vs Cancelados</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div style="height:240px">
                  <canvas id="ordersChart"></canvas>
                </div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-xs-12 col-md-6">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Tendencia semanal de pedidos</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div style="height:240px">
                  <canvas id="ordersTrendChart"></canvas>
                </div>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </q-card-section>
    </q-card>

    <!-- Monitor de Carritos Activos - Solo para Admin y Vendedores -->
    <div v-if="checkAnyRole(['admin', 'administrador', 'vendedor'])" class="q-mt-lg">
      <ActiveCartsMonitor />
    </div>

    <DialogLoad :dialogLoad="dialogLoad" />
  </div>
</template>



<script setup>
import { reactive, onMounted, ref, computed } from 'vue'
import { Chart, registerables } from 'chart.js'
import { api } from 'src/boot/axios'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import ActiveCartsMonitor from 'src/components/ActiveCartsMonitor.vue'
import TestSignalR from './Test/TestSignalR.vue'
import DiagnosticoSignalR from './Test/DiagnosticoSignalR.vue'
import { useAuth } from 'src/assets/js/composables/useAuth'

const dialogLoad = ref(false)
const { checkAnyRole } = useAuth()
Chart.register(...registerables)

const stats = reactive({
  salesToday: 0,
  ordersToday: 0,
  activeUsers: 0,
  newUsersToday: 0,
  lowStock: 0,
  pendingOrders: 0,
  weekRevenue: 0,
  monthRevenue: 0,
  topProductToday: '',
  recurrentUsers: 0,
  newUsersWeek: 0,
  grossMargin: 0,
  confirmedOrders: 0,
  cancelledOrders: 0,
  ordersTrendDays: [],
  ordersTrendConfirmed: [],
  ordersTrendCancelled: []
})

let last7 = []
let topProducts = []
let salesChart = null
let topChart = null
let ordersChart = null
let ordersTrendChart = null

// Arrays para cards (simplificación)
const cardsFila1 = computed(() => [
  { label: 'Ventas Hoy', value: `$ ${stats.salesToday.toLocaleString()}`, caption: `Pedidos: ${stats.ordersToday}`, color: 'bg-primary' },
  { label: 'Ingresos Semana', value: `$ ${stats.weekRevenue.toLocaleString()}`, color: 'bg-indigo' },
  { label: 'Ingresos Mes', value: `$ ${stats.monthRevenue.toLocaleString()}`, color: 'bg-teal' },
  { label: 'Margen Bruto', value: `${stats.grossMargin}%`, caption: 'Promedio', color: 'bg-brown' }
])

const cardsFila2 = computed(() => [
  { label: 'Clientes Activos', value: stats.activeUsers, caption: `Nuevos: ${stats.newUsersToday}`, color: 'bg-secondary' },
  { label: 'Clientes Recurrentes', value: stats.recurrentUsers, color: 'bg-purple' },
  { label: 'Clientes Nuevos Semana', value: stats.newUsersWeek, color: 'bg-cyan' },
  { label: 'Top Producto Hoy', value: stats.topProductToday, color: 'bg-pink' }
])

const cardsFila3 = computed(() => [
  { label: 'Inventario Bajo', value: stats.lowStock, caption: 'Revisar productos críticos', color: 'bg-green' },
  { label: 'Pedidos Pendientes', value: stats.pendingOrders, caption: 'Por despachar', color: 'bg-orange' }
])

const cardsFila4 = computed(() => [
  { label: 'Pedidos Confirmados', value: stats.confirmedOrders, color: 'bg-light-green' },
  { label: 'Pedidos Cancelados', value: stats.cancelledOrders, color: 'bg-red' }
])

// Funciones de gráficas (idénticas a las que ya tienes)
function buildSalesChart() {
  const ctx = document.getElementById('salesChart')
  if (!ctx) return
  const labels = ['Lun','Mar','Mié','Jue','Vie','Sáb','Dom']
  if (salesChart) salesChart.destroy()
  salesChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels,
      datasets: [{
        label: 'Ventas',
        data: last7,
        borderColor: '#7C4DFF',
        backgroundColor: 'rgba(124,77,255,0.15)',
        fill: true,
        tension: 0.3
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: { legend: { display: false } },
      scales: { y: { beginAtZero: true } }
    }
  })
}

function buildTopProductsChart() {
  const ctx = document.getElementById('topProductsChart')
  if (!ctx) return
  if (topChart) topChart.destroy()
  topChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels: topProducts.map(p => p.name),
      datasets: [{
        label: 'Unidades',
        data: topProducts.map(p => p.sales),
        backgroundColor: '#5C6BC0'
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: { legend: { display: false } },
      scales: { y: { beginAtZero: true } }
    }
  })
}

function buildOrdersChart() {
  const ctx = document.getElementById('ordersChart')
  if (!ctx) return
  if (ordersChart) ordersChart.destroy()
  ordersChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
      labels: ['Confirmados', 'Cancelados'],
      datasets: [{
        data: [stats.confirmedOrders, stats.cancelledOrders],
        backgroundColor: ['#4CAF50', '#F44336']
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: { legend: { position: 'bottom' } }
    }
  })
}

function buildOrdersTrendChart() {
  const ctx = document.getElementById('ordersTrendChart')
  if (!ctx) return
  if (ordersTrendChart) ordersTrendChart.destroy()
  ordersTrendChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: stats.ordersTrendDays,
      datasets: [
        {
          label: 'Confirmados',
          data: stats.ordersTrendConfirmed,
          borderColor: '#4CAF50',
          backgroundColor: 'rgba(76,175,80,0.2)',
          fill: true,
          tension: 0.3
        },
        {
          label: 'Cancelados',
          data: stats.ordersTrendCancelled,
          borderColor: '#F44336',
          backgroundColor: 'rgba(244,67,54,0.2)',
          fill: true,
          tension: 0.3
        }
      ]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: { legend: { position: 'bottom' } },
      scales: { y: { beginAtZero: true } }
    }
  })
}


async function refresh() {
  dialogLoad.value = true
  const { data } = await api.get('/DatosDashboard/GetDashboardData')

  // KPIs actuales
  stats.salesToday = data.ventasHoy
  stats.ordersToday = data.pedidosHoy
  stats.activeUsers = data.clientesActivos
  stats.newUsersToday = data.nuevosClientesHoy
  stats.lowStock = data.inventarioBajo

  // Nuevos KPIs
  stats.pendingOrders = data.pedidosPendientes
  stats.weekRevenue = data.ingresosSemana
  stats.monthRevenue = data.ingresosMes
  stats.topProductToday = `${data.topProductoHoy} (${data.topProductoHoyCantidad})`
  stats.recurrentUsers = data.clientesRecurrentes
  stats.newUsersWeek = data.clientesNuevosSemana
  stats.grossMargin = data.margenBrutoPromedio
  stats.confirmedOrders = data.pedidosConfirmados
  stats.cancelledOrders = data.pedidosCancelados

  // Tendencia semanal pedidos
  stats.ordersTrendDays = data.pedidosUltimos7Dias.dias
  stats.ordersTrendConfirmed = data.pedidosUltimos7Dias.confirmados
  stats.ordersTrendCancelled = data.pedidosUltimos7Dias.cancelados

  // Gráficas
  last7 = data.ventasUltimos7Dias.totales
  topProducts = data.topProductos.map(p => ({ name: p.nombre, sales: p.cantidadVendida }))

  buildSalesChart()
  buildTopProductsChart()
  buildOrdersChart()
  buildOrdersTrendChart()

  dialogLoad.value = false
}

onMounted(async () => {
  await refresh()
})
</script>



<style scoped lang="scss">
.bg-primary {
  background: #7c4dff;
}

.bg-secondary {
  background: #5c6bc0;
}

.bg-green {
  background: #26a69a;
}

.bg-orange {
  background: #ff9800;
}

.bg-indigo {
  background: #3f51b5;
}

.bg-teal {
  background: #009688;
}

.bg-pink {
  background: #e91e63;
}

.bg-brown {
  background: #795548;
}

.bg-purple {
  background: #9c27b0;
}

.bg-cyan {
  background: #00bcd4;
}

.bg-light-green {
  background: #8bc34a;
}

.bg-red {
  background: #f44336;
}

.full-height {
  min-height: 160px;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

:root {
  --card-bg: #ffffff;
  --card-text: #000000;
}

body.body--dark {
  --card-bg: #1e1e1e;
  --card-text: #ffffff;
}

.dashboard-card {
  background-color: var(--card-bg);
  color: var(--card-text);
}

/* Media Queries para responsividad */
@media (max-width: 599px) {
  .full-height {
    min-height: 120px;
  }

  .text-h5 {
    font-size: 18px !important;
  }

  .text-h6 {
    font-size: 14px !important;
  }

  .text-h4 {
    font-size: 20px !important;
  }

  .text-caption {
    font-size: 11px !important;
  }

  .row.q-col-gutter-lg {
    margin-left: -12px;
    margin-right: -12px;
  }

  .row.q-col-gutter-lg > [class*="col-"] {
    padding-left: 6px;
    padding-right: 6px;
  }

  :deep(.q-card__section) {
    padding: 12px;
  }

  :deep(canvas) {
    max-height: 200px !important;
  }
}

@media (max-width: 1023px) and (min-width: 600px) {
  .full-height {
    min-height: 140px;
  }

  .col-xs-12.col-sm-6 {
    flex: 0 0 50%;
    max-width: 50%;
  }

  .col-xs-12.col-sm-6.col-md-3 {
    flex: 0 0 50%;
    max-width: 50%;
  }
}

@media (min-width: 1024px) {
  .col-xs-12.col-sm-6.col-md-3 {
    flex: 0 0 25%;
    max-width: 25%;
  }

  .col-xs-12.col-sm-6.col-md-6 {
    flex: 0 0 50%;
    max-width: 50%;
  }
}

@media (min-width: 1366px) {
  .full-height {
    min-height: 180px;
  }

  :deep(.q-card__section) {
    padding: 16px;
  }
}
</style>
