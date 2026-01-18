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
      <q-breadcrumbs-el label="Asientos Contables" />
    </q-breadcrumbs>

    <div class="row items-center q-mb-lg">
      <div class="col-12">
        <h5 class="q-my-none">Asientos Contables</h5>
        <span class="text-caption text-grey">
          Registro de todas las transacciones contables del sistema
        </span>
      </div>
    </div>

    <div class="row q-mb-lg q-gutter-md">
      <q-input
        outlined
        v-model="filtro.descripcion"
        label="Buscar por descripción..."
        class="col-grow"
        dense
        debounce="500"
      />
     <!-- <q-input
        outlined
        v-model="filtro.tipo"
        label="Tipo de referencia..."
        class="col-3"
        dense
        debounce="500"
      />-->
      <q-input
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
      <q-btn
        color="primary"
        icon="filter_list"
        label="Filtrar"
        @click="aplicarFiltros"
        class="col-auto"
      />
      <q-btn
        color="secondary"
        icon="refresh"
        @click="cargarAsientos"
        class="col-auto"
      />
    </div>

    <q-table
      class="q-pa-md"
      :rows="asientos"
      :columns="columnas"
      row-key="id"
      :loading="cargando"
      :pagination="paginacion"
      no-data-label="No hay asientos contables disponibles"
      no-results-label="No hay resultados"
    >
      <template v-slot:body-cell-acciones="props">
        <q-td :props="props">
          <q-btn
            flat
            dense
            round
            icon="visibility"
            color="primary"
            size="sm"
            @click="verDetalles(props.row)"
          >
            <q-tooltip>Ver detalles</q-tooltip>
          </q-btn>
        </q-td>
      </template>

      <template v-slot:body-cell-fecha="props">
        <q-td :props="props">
          {{ formatearFecha(props.row.fecha) }}
        </q-td>
      </template>

      <template v-slot:body-cell-tipoReferencia="props">
        <q-td :props="props">
          <q-chip
            :color="colorTipo(props.row.tipoReferencia)"
            text-color="white"
            size="sm"
          >
            {{ props.row.tipoReferencia }}
          </q-chip>
        </q-td>
      </template>
    </q-table>

    <!-- Dialog de detalles del asiento -->
    <q-dialog
      v-model="dialogoDetalles"
      maximized
    >
      <q-card>
        <q-card-section class="row items-center q-pb-none">
          <div class="text-h6">Detalles del Asiento Contable</div>
          <q-space />
          <q-btn icon="close" flat round dense @click="dialogoDetalles = false" />
        </q-card-section>

        <q-separator />

        <q-card-section class="q-pt-md">
          <div class="row q-col-gutter-lg q-mb-lg">
            <div class="col-md-6 col-xs-12">
              <div class="q-mb-md">
                <label class="text-weight-bold">Fecha:</label>
                <p>{{ formatearFecha(asientoSeleccionado?.fecha) }}</p>
              </div>
              <div class="q-mb-md">
                <label class="text-weight-bold">Descripción:</label>
                <p>{{ asientoSeleccionado?.descripcion }}</p>
              </div>
            </div>
            <div class="col-md-6 col-xs-12">
              <div class="q-mb-md">
                <label class="text-weight-bold">Tipo de Referencia:</label>
                <p>{{ asientoSeleccionado?.tipoReferencia }}</p>
              </div>
             <!-- <div class="q-mb-md">
                <label class="text-weight-bold">ID Referencia:</label>
                <p>{{ asientoSeleccionado?.consecutivo }}</p>
              </div>-->
            </div>
          </div>

          <q-separator class="q-my-lg" />

          <div class="text-h6 q-mb-md">Movimientos Contables</div>

          <q-table
            :rows="asientoSeleccionado?.movimientos || []"
            :columns="columnasMovimientos"
            row-key="id"
            flat
            bordered
            no-data-label="No hay movimientos en este asiento"
          >
            <template v-slot:body-cell-debe="props">
              <q-td :props="props">
                <span class="text-weight-bold text-primary">
                  {{ formatearMoneda(props.row.debe) }}
                </span>
              </q-td>
            </template>

            <template v-slot:body-cell-haber="props">
              <q-td :props="props">
                <span class="text-weight-bold text-negative">
                  {{ formatearMoneda(props.row.haber) }}
                </span>
              </q-td>
            </template>
          </q-table>

          <div class="row justify-end q-mt-lg">
            <div class="col-md-4 col-xs-12">
              <div class="row q-col-gutter-md items-center">
                <div class="col">
                  <strong>Total Debe:</strong>
                </div>
                <div class="col text-right text-primary text-weight-bold">
                  {{ formatearMoneda(calcularTotalDebe()) }}
                </div>
              </div>
              <div class="row q-col-gutter-md items-center">
                <div class="col">
                  <strong>Total Haber:</strong>
                </div>
                <div class="col text-right text-negative text-weight-bold">
                  {{ formatearMoneda(calcularTotalHaber()) }}
                </div>
              </div>
              <q-separator class="q-my-md" />
              <div
                class="row q-col-gutter-md items-center"
                :class="estaEquilibrado() ? 'text-positive' : 'text-negative'"
              >
                <div class="col">
                  <strong>Diferencia:</strong>
                </div>
                <div class="col text-right text-weight-bold">
                  {{ formatearMoneda(calcularDiferencia()) }}
                </div>
              </div>
              <div v-if="estaEquilibrado()" class="text-caption text-positive q-mt-sm">
                <q-icon name="check_circle" /> Asiento equilibrado
              </div>
              <div v-else class="text-caption text-negative q-mt-sm">
                <q-icon name="error" /> Asiento desbalanceado
              </div>
            </div>
          </div>
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
        <dialog-load :dialogLoad="dialogLoad" />

</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { loadGet, loadGetPaginado } from 'src/assets/js/util/funciones'
import { Error } from 'src/assets/js/util/notify'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'

// Variables Booleanas
const dialogoDetalles = ref(false)
const cargando = ref(false)
const dialogLoad = ref(false)

// Variables Nulas
const asientoSeleccionado = ref(null)

// Arreglos
const asientos = ref([])

// Objeto Reactive para paginación y filtros
const paginacion = reactive({
  sortBy: 'fecha',
  descending: true,
  page: 1,
  rowsPerPage: 10,
  rowsNumber: 0,
})

const filtro = reactive({
  descripcion: '',
  tipo: '',
  fechaInicio: '',
  fechaFin: '',
})

// Columnas para tabla de asientos
const columnas = [
  {
    name: 'fecha',
    label: 'Fecha',
    field: 'fecha',
    align: 'left',
    sortable: true,
  },
  {
    name: 'descripcion',
    label: 'Descripción',
    field: 'descripcion',
    align: 'left',
    sortable: true,
  },
  {
    name: 'tipoReferencia',
    label: 'Tipo',
    field: 'tipoReferencia',
    align: 'center',
    sortable: true,
  },
  /*{
    name: 'referenciaId',
    label: 'ID Referencia',
    field: 'consecutivo',
    align: 'left',
  },*/
  {
    name: 'acciones',
    label: 'Acciones',
    field: 'acciones',
    align: 'center',
  },
]

// Columnas para tabla de movimientos
const columnasMovimientos = [
  {
    name: 'cuenta',
    label: 'Cuenta',
    field: row => row.nombreCuenta || 'N/A',
    align: 'left',
  },
  {
    name: 'codigo',
    label: 'Código',
    field: row => row.codigoCuenta || 'N/A',
    align: 'left',
  },
  {
    name: 'debe',
    label: 'Debe',
    field: 'debe',
    align: 'right',
  },
  {
    name: 'haber',
    label: 'Haber',
    field: 'haber',
    align: 'right',
  },
]

// Funciones
const cargarAsientos = async () => {
    dialogLoad.value = true

  cargando.value = true
  try {
    const params = {
     // pageNumber: paginacion.page,
    //  pageSize: paginacion.rowsPerPage,
    }

    if (filtro.descripcion) {
      params.descripcion = filtro.descripcion
    }
    if (filtro.tipo) {
      params.tipoReferencia = filtro.tipo
    }
    if (filtro.fechaInicio) {
      params.fechaInicio = filtro.fechaInicio
    }
    if (filtro.fechaFin) {
      params.fechaFin = filtro.fechaFin
    }

    const response = await loadGet(`AsientoContable/ObtenerListadoPaginado?fechaInicio=${filtro.fechaInicio}&fechaFin=${filtro.fechaFin}&textoBuscar=${filtro.descripcion}`) ?? {}
    asientos.value = response || []
    paginacion.rowsNumber = response.cantidad || 0
  } catch (error) {
    Error('Error al cargar los asientos contables')
    console.error(error)
  } finally {
    cargando.value = false
    dialogLoad.value = false

  }
}

const aplicarFiltros = async () => {
  paginacion.page = 1
  await cargarAsientos()
}

const verDetalles = (asiento) => {
  asientoSeleccionado.value = asiento
  dialogoDetalles.value = true
}

const formatearFecha = (fecha) => {
  if (!fecha) return '-'
  const date = new Date(fecha)
  return date.toLocaleDateString('es-ES', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
  })
}

const formatearMoneda = (valor) => {
  if (!valor) return '0.00'
  return parseFloat(valor).toLocaleString('es-ES', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

const colorTipo = (tipo) => {
  const tipos = {
    Venta: 'primary',
    Devolucion: 'warning',
    Ajuste: 'info',
    Cierre: 'secondary',
  }
  return tipos[tipo] || 'grey'
}

const calcularTotalDebe = () => {
  return (asientoSeleccionado.value?.movimientos || []).reduce(
    (sum, mov) => sum + (mov.debe || 0),
    0
  )
}

const calcularTotalHaber = () => {
  return (asientoSeleccionado.value?.movimientos || []).reduce(
    (sum, mov) => sum + (mov.haber || 0),
    0
  )
}

const calcularDiferencia = () => {
  return calcularTotalDebe() - calcularTotalHaber()
}

const estaEquilibrado = () => {
  const diferencia = Math.abs(calcularDiferencia())
  return diferencia < 0.01
}

// Ciclo de vida
onMounted(async () => {

  await cargarAsientos()
})
</script>


