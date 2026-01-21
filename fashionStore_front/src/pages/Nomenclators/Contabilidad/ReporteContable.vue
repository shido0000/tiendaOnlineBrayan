<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
        <q-breadcrumbs-el
        label="Contabilidad"
        icon="dashboard"
        @click="$router.push('/Contabilidad')"
      />
      <q-breadcrumbs-el label="Reportes Contables" />
    </q-breadcrumbs>

    <div class="row items-center q-mb-lg">
      <div class="col-12">
        <h5 class="q-my-none">Reportes Contables</h5>
        <span class="text-caption text-grey">
          Análisis de estado de cuentas y resultados
        </span>
      </div>
    </div>

    <div class="row q-mb-lg q-gutter-md">
     <!-- <q-input
        outlined
        v-model="filtro.fechaInicio"
        type="date"
        label="Desde"
        class="col-2"
        dense
      />
      <q-input
        outlined
        v-model="filtro.fechaFin"
        type="date"
        label="Hasta"
        class="col-2"
        dense
      />
      <q-select
        outlined
        v-model="filtro.cuenta"
        :options="cuentas"
        label="Filtrar por cuenta"
        class="col-grow"
        dense
        emit-value
        map-options
        option-value="id"
        option-label="label"
        clearable
      />
      <q-btn
        color="primary"
        icon="filter_list"
        label="Aplicar"
        @click="aplicarFiltros"
        class="col-auto"
      />-->
      <q-btn
        color="primary"
        icon="file_download"
        label="Exportar"
        @click="exportarReporte"
        class="col-auto"
      />
    </div>

    <div class="row q-col-gutter-lg">
      <!-- Estado de Cuentas -->
      <div class="col-12">
        <q-card>
          <q-card-section>
            <div class="text-h6 q-mb-md">Estado de Cuentas</div>

            <q-table
              :rows="estadoCuentas"
              :columns="columnasEstado"
              row-key="id"
              no-data-label="No hay datos disponibles"
              flat
              bordered
            >
              <template v-slot:body-cell-saldo="props">
                <q-td :props="props">
                  <span
                    :class="{
                      'text-positive text-weight-bold': props.row.saldo > 0,
                      'text-negative text-weight-bold': props.row.saldo < 0,
                    }"
                  >
                    {{ formatearMoneda(props.row.saldo) }}
                  </span>
                </q-td>
              </template>

              <template v-slot:body-cell-debe="props">
                <q-td :props="props">
                  {{ formatearMoneda(props.row.totalDebe) }}
                </q-td>
              </template>

              <template v-slot:body-cell-haber="props">
                <q-td :props="props">
                  {{ formatearMoneda(props.row.totalHaber) }}
                </q-td>
              </template>
            </q-table>
          </q-card-section>
        </q-card>
      </div>

      <!-- Resumen por tipo de movimiento -->
      <div class="col-12">
        <q-card>
          <q-card-section>
            <div class="text-h6 q-mb-md">Resumen por Tipo de Movimiento</div>

            <div class="row q-col-gutter-lg">
              <div class="col-md-6 col-xs-12" v-for="tipo in tiposMovimiento" :key="tipo">
                <q-card flat bordered>
                  <q-card-section class="text-center">
                    <div class="text-h6">{{ tipo }}</div>
                    <div class="text-caption text-grey q-mb-md">
                      Cantidad: {{ contarMovimientosPorTipo(tipo) }}
                    </div>
                    <div class="text-h5 text-primary">
                      {{ formatearMoneda(sumarMovimientosPorTipo(tipo)) }}
                    </div>
                  </q-card-section>
                </q-card>
              </div>
            </div>
          </q-card-section>
        </q-card>
      </div>

      <!-- Movimientos por período -->
      <div class="col-12">
        <q-card>
          <q-card-section>
            <div class="text-h6 q-mb-md">Movimientos por Período</div>

            <q-table
              :rows="movimientosPorPeriodo"
              :columns="columnasMovimientos"
              row-key="fecha"
              no-data-label="No hay datos disponibles"
              flat
              bordered
            >
              <template v-slot:body-cell-montoTotal="props">
                <q-td :props="props">
                  <span class="text-weight-bold">
                    {{ formatearMoneda(props.row.montoTotal) }}
                  </span>
                </q-td>
              </template>
            </q-table>
          </q-card-section>
        </q-card>
      </div>

      <!-- Indicadores Clave -->
      <div class="col-12">
        <div class="row q-col-gutter-lg">
          <div class="col-md-3 col-xs-6">
            <q-card class="bg-primary text-white">
              <q-card-section class="text-center">
                <div class="text-h4">{{ totalAsientos }}</div>
                <div class="text-subtitle2">Total de Asientos</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-md-3 col-xs-6">
            <q-card class="bg-positive text-white">
              <q-card-section class="text-center">
                <div class="text-h4">
                  {{ formatearMoneda(totalDebe) }}
                </div>
                <div class="text-subtitle2">Total Debe</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-md-3 col-xs-6">
            <q-card class="bg-negative text-white">
              <q-card-section class="text-center">
                <div class="text-h4">
                  {{ formatearMoneda(totalHaber) }}
                </div>
                <div class="text-subtitle2">Total Haber</div>
              </q-card-section>
            </q-card>
          </div>

          <div class="col-md-3 col-xs-6">
            <q-card :class="estaEquilibrado ? 'bg-info' : 'bg-warning'" class="text-white">
              <q-card-section class="text-center">
                <div class="text-h4">
                  {{ formatearMoneda(Math.abs(diferencia)) }}
                </div>
                <div class="text-subtitle2">
                  {{ estaEquilibrado ? "Equilibrio" : "Diferencia" }}
                </div>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </div>
    </div>
  </div>
        <dialog-load :dialogLoad="dialogLoad" />

</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { loadGet, loadGetPaginado } from 'src/assets/js/util/funciones'
import { Error } from 'src/assets/js/util/notify'
import { Success } from 'src/boot/notify'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import { api } from 'src/boot/axios'

// Variables Booleanas
// (ninguna necesaria en este componente)
const dialogLoad = ref(false)

// Arreglos
const asientos = ref([])
const cuentas = ref([])

// Objeto Reactive para filtros
const filtro = reactive({
  fechaInicio: '',
  fechaFin: '',
  cuenta: null,
})

// Constantes
const tiposMovimiento = ['Venta', 'Devolucion', 'Ajuste', 'Cierre']

// Columnas para tabla de estado de cuentas
const columnasEstado = [
  {
    name: 'codigo',
    label: 'Código',
    field: row => row.cuenta?.codigo || '-',
    align: 'left',
  },
  {
    name: 'nombre',
    label: 'Nombre',
    field: row => row.cuenta?.nombre || '-',
    align: 'left',
  },
  {
    name: 'debe',
    label: 'Total Debe',
    field: 'totalDebe',
    align: 'right',
  },
  {
    name: 'haber',
    label: 'Total Haber',
    field: 'totalHaber',
    align: 'right',
  },
  {
    name: 'saldo',
    label: 'Saldo',
    field: 'saldo',
    align: 'right',
  },
]

// Columnas para tabla de movimientos por período
const columnasMovimientos = [
  {
    name: 'fecha',
    label: 'Período',
    field: 'fecha',
    align: 'left',
  },
  {
    name: 'cantidad',
    label: 'Cantidad de Asientos',
    field: 'cantidad',
    align: 'center',
  },
  {
    name: 'montoTotal',
    label: 'Monto Total',
    field: 'montoTotal',
    align: 'right',
  },
]

// Computed para estado de cuentas
const estadoCuentas = computed(() => {
  const estadoMap = {}

  asientos.value.forEach(asiento => {
    asiento.movimientos?.forEach(mov => {
      const cuentaId = mov.cuentaContableId
      if (!estadoMap[cuentaId]) {
        estadoMap[cuentaId] = {
          id: cuentaId,
          cuenta: {
            codigo: mov.codigoCuenta,
            nombre: mov.nombreCuenta,
          },
          totalDebe: 0,
          totalHaber: 0,
        }
      }
      estadoMap[cuentaId].totalDebe += mov.debe || 0
      estadoMap[cuentaId].totalHaber += mov.haber || 0
    })
  })

  return Object.values(estadoMap).map(estado => ({
    ...estado,
    saldo: estado.totalDebe - estado.totalHaber,
  }))
})

// Computed para movimientos por período
const movimientosPorPeriodo = computed(() => {
  const periodoMap = {}

  asientos.value.forEach(asiento => {
    const fecha = new Date(asiento.fecha).toLocaleDateString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
    })

    if (!periodoMap[fecha]) {
      periodoMap[fecha] = {
        fecha,
        cantidad: 0,
        montoTotal: 0,
      }
    }

    periodoMap[fecha].cantidad += 1
    asiento.movimientos?.forEach(mov => {
      //periodoMap[fecha].montoTotal += mov.debe + mov.haber
      periodoMap[fecha].montoTotal +=   mov.haber

    })
  })

  return Object.values(periodoMap).sort(
    (a, b) => new Date(b.fecha) - new Date(a.fecha)
  )
})

// Computed para total de asientos
const totalAsientos = computed(() => {
  return asientos.value.length
})

// Computed para total debe
const totalDebe = computed(() => {
  return estadoCuentas.value.reduce((sum, est) => sum + est.totalDebe, 0)
})

// Computed para total haber
const totalHaber = computed(() => {
  return estadoCuentas.value.reduce((sum, est) => sum + est.totalHaber, 0)
})

// Computed para diferencia
const diferencia = computed(() => {
  return totalDebe.value - totalHaber.value
})

// Computed para saber si está equilibrado
const estaEquilibrado = computed(() => {
  return Math.abs(diferencia.value) < 0.01
})

// Funciones
const cargarDatos = async () => {
    dialogLoad.value = true

  try {
    const params = {}
    if (filtro.fechaInicio) params.fechaInicio = filtro.fechaInicio
    if (filtro.fechaFin) params.fechaFin = filtro.fechaFin

    const response = await loadGet('AsientoContable/ObtenerListadoPaginado', params) ?? {}
    asientos.value = response.items || response || []

    // Cargar cuentas
    const cuentasResponse = await loadGet('CuentaContable/ObtenerListadoPaginado') ?? {}
    const todasLasCuentas = cuentasResponse.items || cuentasResponse || []
    cuentas.value = todasLasCuentas.map(c => ({
      id: c.id,
      label: `${c.codigo} - ${c.nombre}`,
    }))
  } catch (error) {
    Error('Error al cargar los datos')
    dialogLoad.value = false
  }
    dialogLoad.value = false

}

const cargarDatosFiltros = async () => {
    dialogLoad.value = true

  try {
    const params = {}
    if (filtro.fechaInicio) params.fechaInicio = filtro.fechaInicio
    if (filtro.fechaFin) params.fechaFin = filtro.fechaFin
    if (filtro.cuenta) params.cuenta = filtro.cuenta


    const response = await loadGetPaginado('AsientoContable/ObtenerListadoPaginado', params) ?? {}
    asientos.value = response.elementos || []

  } catch (error) {
    Error('Error al cargar los datos')
    dialogLoad.value = false
  }
    dialogLoad.value = false

}

const formatearMoneda = (valor) => {
  if (!valor) return '0.00'
  return parseFloat(valor).toLocaleString('es-ES', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

const contarMovimientosPorTipo = (tipo) => {
  return asientos.value.filter(a => a.tipoReferencia === tipo).length
}

/*const sumarMovimientosPorTipo = (tipo) => {
  return asientos.value
    .filter(a => a.tipoReferencia === tipo)
    .reduce((sum, asiento) => {
      return (
        sum +
        (asiento.movimientos?.reduce((s, m) => s + m.debe + m.haber, 0) || 0)
      )
    }, 0)
}*/
const sumarMovimientosPorTipo = (tipo) => {
  return asientos.value
    .filter(a => a.tipoReferencia === tipo)
    .reduce((sum, asiento) => {
      return (
        sum +
        (asiento.movimientos?.reduce((s, m) => s +m.haber, 0) || 0)
      )
    }, 0)
}

const aplicarFiltros = async () => {
  await cargarDatosFiltros()
}

const exportarReporte = async () => {
  try {
    dialogLoad.value = true

    // Construir parámetros con fechas si están disponibles
    const params = new URLSearchParams()
    if (filtro.fechaInicio) {
      params.append('desde', filtro.fechaInicio)
    }
    if (filtro.fechaFin) {
      params.append('hasta', filtro.fechaFin)
    }

    // Llamar al endpoint para descargar el archivo usando axios (api)
    const response = await api.get(
      `AsientoContable/ExportarLibroContable?${params.toString()}`,
      {
        responseType: 'blob' // Importante para descargas de archivos
      }
    )

    // Obtener el nombre del archivo del header Content-Disposition
    const contentDisposition = response.headers['content-disposition']
    let filename = 'LibroContable.xlsx'
    if (contentDisposition) {
      const filenameMatch = contentDisposition.match(/filename="?([^"]+)"?/)
      if (filenameMatch && filenameMatch[1]) {
        filename = filenameMatch[1]
      }
    }

    // Crear un blob desde la respuesta
    const blob = response.data

    // Crear un URL para el blob
    const url = window.URL.createObjectURL(blob)

    // Crear un elemento <a> temporal para descargar
    const link = document.createElement('a')
    link.href = url
    link.download = filename
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)

    // Limpiar el URL
    window.URL.revokeObjectURL(url)

    Success('Archivo descargado correctamente')
  } catch (error) {
    Error('Error al exportar el reporte: ' + error.message)
    console.error(error)
  } finally {
    dialogLoad.value = false
  }
}

// Ciclo de vida
onMounted(async () => {
  await cargarDatos()
})
</script>


