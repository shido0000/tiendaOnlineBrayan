<template>
  <div class="q-pa-md">
    <q-card class="q-pa-lg" style="max-width:1100px; margin:auto">
      <q-card-section>
        <div class="row items-center q-col-gutter-md">
          <div class="col">
            <div class="text-h5">Dashboard</div>
            <div class="text-subtitle2 q-mt-sm">Resumen administrativo (datos ficticios)</div>
          </div>
          <div class="col-auto">
            <q-btn flat label="Refrescar" @click="refresh" />
          </div>
        </div>
      </q-card-section>

      <q-separator />

      <q-card-section>
        <div class="row q-col-gutter-lg q-mb-lg">
          <div class="col-3">
            <q-card class="bg-primary text-white">
              <q-card-section class="text-center">
                <div class="text-h6">Ventas Hoy</div>
                <div class="text-h4 q-mt-sm">$ {{ stats.salesToday.toLocaleString() }}</div>
                <div class="text-caption q-mt-sm">Pedidos: {{ stats.ordersToday }}</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-3">
            <q-card class="bg-secondary text-white">
              <q-card-section class="text-center">
                <div class="text-h6">Clientes Activos</div>
                <div class="text-h4 q-mt-sm">{{ stats.activeUsers }}</div>
                <div class="text-caption q-mt-sm">Nuevos: {{ stats.newUsersToday }}</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-3">
            <q-card class="bg-green text-white">
              <q-card-section class="text-center">
                <div class="text-h6">Inventario bajo</div>
                <div class="text-h4 q-mt-sm">{{ stats.lowStock }}</div>
                <div class="text-caption q-mt-sm">Revisar productos críticos</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-3">
            <q-card class="bg-orange text-white">
              <q-card-section class="text-center">
                <div class="text-h6">Tasa conversión</div>
                <div class="text-h4 q-mt-sm">{{ stats.conversion }}%</div>
                <div class="text-caption q-mt-sm">Semanal</div>
              </q-card-section>
            </q-card>
          </div>
        </div>

        <div class="row q-col-gutter-lg">
          <div class="col-7">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Ventas - Últimos 7 días</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div style="height:240px">
                  <canvas id="salesChart" style="width:100%; height:100%"></canvas>
                </div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-5">
            <q-card flat>
              <q-card-section>
                <div class="text-subtitle1">Top productos (ficticio)</div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                  <div style="height:240px">
                    <canvas id="topProductsChart" style="width:100%; height:100%"></canvas>
                  </div>
              </q-card-section>
            </q-card>
          </div>
        </div>

      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { reactive, onMounted } from 'vue'
import { Chart, registerables } from 'chart.js'

Chart.register(...registerables)

function randomInt(min, max) { return Math.floor(Math.random()*(max-min+1))+min }

const stats = reactive({
  salesToday: randomInt(100, 1500),
  ordersToday: randomInt(5, 50),
  activeUsers: randomInt(50, 1000),
  newUsersToday: randomInt(0, 20),
  lowStock: randomInt(1, 12),
  conversion: (Math.random()*5+1).toFixed(2)
})

let last7 = Array.from({length:7}, () => randomInt(200, 1500))
let topProducts = Array.from({length:5}, (_,i) => ({ name: `Producto ${(i+1)}`, sales: randomInt(10,200) }))

let salesChart = null
let topChart = null

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
    options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true } } }
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
      datasets: [{ label: 'Unidades', data: topProducts.map(p => p.sales), backgroundColor: '#5C6BC0' }]
    },
    options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true } } }
  })
}

function refresh() {
  stats.salesToday = randomInt(100, 1500)
  stats.ordersToday = randomInt(5, 50)
  stats.activeUsers = randomInt(50, 1000)
  stats.newUsersToday = randomInt(0, 20)
  stats.lowStock = randomInt(1, 12)
  last7 = Array.from({length:7}, () => randomInt(200, 1500))
  topProducts = Array.from({length:5}, (_,i) => ({ name: `Producto ${(i+1)}`, sales: randomInt(10,200) }))
  buildSalesChart()
  buildTopProductsChart()
}

onMounted(() => {
  buildSalesChart()
  buildTopProductsChart()
})
</script>

<style scoped>
.bg-primary { background: #7C4DFF }
.bg-secondary { background: #5C6BC0 }
.bg-green { background: #26A69A }
.bg-orange { background: #FF9800 }
</style>
