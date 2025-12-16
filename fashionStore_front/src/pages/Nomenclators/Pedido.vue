<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Pedidos" />
    </q-breadcrumbs>

    <!-- Vista de Tabla Principal -->
    <div v-if="!dialogEditarPedido">
      <q-table
        class="q-pa-md"
        :filter="filter"
        :rows="items"
        :columns="columnasTableaPedidos"
        row-key="id"
        no-data-label="No hay elementos disponibles"
        no-results-label="No hay elementos disponibles"
        loading-label="Cargando..."
        rows-per-page-label="Filas por página"
      >
        <template v-slot:top>
          <div class="col-4 q-table__title">
            <span>Pedidos</span>
            <q-input
              outline
              color="primary"
              flat
              v-model="filter"
              debounce="1000"
              label="Buscar"
            />
          </div>
          <q-space />

          <!-- Filtro de Estado -->
          <q-select
            v-model="filtroEstado"
            :options="opcionesFiltroEstado"
            outlined
            dense
            label="Filtrar por estado"
            emit-value
            map-options
            style="min-width: 200px; margin-right: 12px"
            color="primary"
            @update:model-value="load"
          />

          <q-btn
            outline
            class="bg-white q-ml-sm"
            style="width: 20px"
            color="primary"
            icon="print"
            @click="imprimir()"
          >
            <q-tooltip class="bg-primary" :offset="[10, 10]">
              Imprimir
            </q-tooltip>
          </q-btn>

          <DialogCancelarPedido
            v-if="isDialogoEliminarAbierto"
            :isOpen="isDialogoEliminarAbierto"
            :idElemento="idElementoSeleccionado"
            @eliminar="eliminar"
            @closeDialog="handleCloseDialog"
          />

          <DialogLoad :dialogLoad="dialogLoad" />
        </template>

        <template v-slot:body-cell-estado="props">
          <q-td :props="props">
            <q-chip
              :color="
                props.row.estado === 'Pendiente'
                  ? 'warning'
                  : props.row.estado === 'Confirmado'
                  ? 'positive'
                  : 'negative'
              "
              text-color="white"
              :label="props.row.estado"
              size="sm"
            />
          </q-td>
        </template>

        <template v-slot:body-cell-total="props">
          <q-td :props="props">
            {{ formatearNumero(props.row.total) }}
          </q-td>
        </template>

        <template v-slot:body-cell-acciones="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id, true)"
                text-color="primary"
                icon="visibility"
              >
                <q-tooltip>Ver datos del pedido</q-tooltip>
              </q-btn>
              <q-btn
                :disable="
                  props.row.estado === 'Rechazado' ||
                  props.row.estado === 'Confirmado'
                "
                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id, false)"
                text-color="primary"
                icon="edit"
              >
                <q-tooltip>Editar líneas del pedido</q-tooltip>
              </q-btn>
              <q-btn
                :disable="props.row.estado === 'Rechazado'"
                flat
                dense
                size="sm"
                @click="abrirDialogoEliminar(props.row.id)"
                text-color="negative"
                icon="delete"
              >
                <q-tooltip>Cancelar pedido</q-tooltip>
              </q-btn>
            </div>
          </q-td>
        </template>
      </q-table>
    </div>

    <!-- Vista de Edición de Pedido -->
    <div v-else>
      <q-card class="col-12 q-mt-md">
        <header class="q-pa-sm bg-primary">
          <q-toolbar>
            <q-toolbar-title class="text-subtitle6 text-white">
              {{ verDatosPedido ? 'Ver' : 'Editar' }} Pedido - {{ pedidoSeleccionado?.codigo }}
            </q-toolbar-title>
          </q-toolbar>
        </header>

        <q-card-section class="q-pa-md">
          <!-- Información del Pedido -->
          <div class="row q-col-gutter-md q-mb-lg">
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">ID del Pedido</div>
              <div class="text-body2 text-weight-bold">{{ pedidoSeleccionado?.codigo }}</div>
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Estado</div>
              <q-chip
                :color="
                  pedidoSeleccionado?.estado === 'Pendiente'
                    ? 'warning'
                    : pedidoSeleccionado?.estado === 'Confirmado'
                    ? 'positive'
                    : 'negative'
                "
                text-color="white"
                :label="pedidoSeleccionado?.estado"
              />
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Usuario</div>
              <div class="text-body2">{{ pedidoSeleccionado?.usuario }}</div>
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Moneda</div>
              <div class="text-body2 text-weight-bold">{{ pedidoSeleccionado?.moneda?.toUpperCase() }}</div>
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Cupón</div>
              <div class="text-body2">{{ pedidoSeleccionado?.cupon || '-' }}</div>
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Dirección de Envío</div>
              <div class="text-body2">{{ pedidoSeleccionado?.direccion || 'No especificada' }}</div>
            </div>
            <div class="col-12 col-sm-6">
              <div class="text-caption text-grey">Precio Gestor</div>
              <div class="text-body2 text-weight-bold">{{ formatearNumero(pedidoSeleccionado?.precioGestor) }}</div>
            </div>
          </div>

          <!-- Tabla de Líneas del Pedido -->
          <div class="q-mt-md">
            <div class="text-subtitle2 q-mb-md">Productos en el Pedido</div>

            <q-table
              :rows="lineasPedidoEditando"
              :columns="columnasLineasPedido"
              row-key="id"
              flat
              bordered
              no-data-label="No hay líneas en este pedido"
            >
              <template v-slot:body-cell-nombreProducto="props">
                <q-td :props="props">
                  <div>{{ props.row.nombreProducto }}</div>
                  <div class="text-caption text-grey">SKU: {{ props.row.sku }}</div>
                  <div class="text-caption text-grey">Código: {{ props.row.codigo }}</div>
                  <div class="text-caption text-grey">Talla: {{ props.row.talla }} | Color: {{ props.row.color }}</div>
                </q-td>
              </template>

              <template v-slot:body-cell-precioUnitario="props">
                <q-td :props="props" class="text-center">
                  {{ formatearNumero(props.row.precioUnitario) }}
                </q-td>
              </template>

              <template v-slot:body-cell-cantidad="props">
                <q-td :props="props" class="text-center">
                  <q-input
                    v-if="!verDatosPedido"
                    v-model.number="props.row.cantidad"
                    type="number"
                    min="1"
                    dense
                    outlined
                    @update:model-value="recalcularPrecio(props.row)"
                    style="max-width: 80px"
                  />
                  <span v-else>{{ props.row.cantidad }}</span>
                </q-td>
              </template>

              <template v-slot:body-cell-cambio="props">
                <q-td :props="props" class="text-center">
                  <div v-if="props.row.cantidad > props.row.cantidadInicial" class="row items-center justify-center q-col-gutter-xs">
                    <span class="text-positive text-weight-bold">
                      +{{ props.row.cantidad - props.row.cantidadInicial }}
                    </span>
                    <q-icon name="trending_up" color="positive" size="xs" />
                  </div>
                  <div v-else-if="props.row.cantidad < props.row.cantidadInicial" class="row items-center justify-center q-col-gutter-xs">
                    <span class="text-negative text-weight-bold">
                      {{ props.row.cantidad - props.row.cantidadInicial }}
                    </span>
                    <q-icon name="trending_down" color="negative" size="xs" />
                  </div>
                  <div v-else class="text-grey">
                    Sin cambios
                  </div>
                </q-td>
              </template>

              <template v-slot:body-cell-subtotal="props">
                <q-td :props="props" class="text-center">
                  <span class="text-weight-bold">
                    {{ formatearNumero(props.row.cantidad * props.row.precioUnitario) }}
                  </span>
                </q-td>
              </template>

              <template v-slot:body-cell-acciones="props">
                <q-td :props="props">
                  <q-btn
                    v-if="!verDatosPedido"
                    flat
                    dense
                    size="sm"
                    color="negative"
                    icon="close"
                    @click="rechazarLinea(props.row.id)"
                  >
                    <q-tooltip>Rechazar línea</q-tooltip>
                  </q-btn>
                </q-td>
              </template>
            </q-table>
          </div>

          <!-- Resumen de Precios -->
          <div class="row q-mt-lg justify-end q-col-gutter-md">
            <div class="col-12 col-sm-6">
              <div class="row q-col-gutter-sm">
                <div class="col-12 row items-center justify-between q-pa-md bg-grey-2 rounded-borders">
                  <span class="text-weight-bold">Subtotal (Productos):</span>
                  <span class="text-weight-bold">
                    {{ formatearNumero(precioTotalPedido) }}
                  </span>
                </div>
                <div class="col-12 row items-center justify-between q-pa-md bg-grey-2 rounded-borders">
                  <span class="text-weight-bold">Envío:</span>
                  <span class="text-weight-bold">
                    {{ formatearNumero(pedidoSeleccionado?.shipping || 0) }}
                  </span>
                </div>
                <div class="col-12 row items-center justify-between q-pa-md bg-grey-2 rounded-borders">
                  <span class="text-weight-bold">Descuento:</span>
                  <span class="text-weight-bold text-negative">
                    -{{ formatearNumero(pedidoSeleccionado?.discount || 0) }}
                  </span>
                </div>
                <div class="col-12 row items-center justify-between q-pa-md bg-grey-2 rounded-borders">
                  <span class="text-weight-bold">Precio Gestor:</span>
                  <span class="text-weight-bold">
                    {{ formatearNumero(pedidoSeleccionado?.precioGestor || 0) }}
                  </span>
                </div>
                <div class="col-12 row items-center justify-between q-pa-md bg-blue-1 rounded-borders">
                  <span class="text-weight-bold text-h6">Total:</span>
                  <span class="text-weight-bold text-blue-9 text-h5">
                    {{ formatearNumero(precioTotalPedidoConCargos) }}
                  </span>
                </div>
              </div>
            </div>
          </div>
        </q-card-section>

        <q-card-actions class="justify-end q-pa-md">
          <q-btn
            outline
            color="primary"
            label="Salir"
            @click="cerrarDialogoEditar(false)"
          />
          <q-btn
            v-if="!verDatosPedido && !pedidoConfirmado"
            class="text-white"
            color="primary"
            label="Confirmar Pedido"
            @click="confirmarPedido"
            :disable="lineasPedidoEditando.length === 0"
          />
          <q-btn
            v-else-if="pedidoConfirmado"
            disable
            class="text-white"
            color="positive"
            label="Pedido Confirmado"
          />
        </q-card-actions>
      </q-card>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed } from 'vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import DialogCancelarPedido from 'src/components/DialogBoxes/DialogCancelarPedido.vue'
import { CancelarPedido, loadGetHastaData, loadGet } from 'src/assets/js/util/funciones'
import { Error, Success } from 'src/assets/js/util/notify'

// Variables Booleanas
const dialogLoad = ref(false)
const verDatosPedido = ref(false)
const isDialogoEliminarAbierto = ref(false)
const dialogEditarPedido = ref(false)
const pedidoConfirmado = ref(false)

// Variables
const idElementoSeleccionado = ref(null)
const filter = ref('')
const orden = ref('fechaCreado:desc')
const filtroEstado = ref('todos')

// Opciones para el filtro de estado
const opcionesFiltroEstado = [
  { label: 'Todos', value: 'todos' },
  { label: 'Confirmados', value: 'Confirmado' },
  { label: 'Rechazados', value: 'Rechazado' },
  { label: 'Pendientes', value: 'Pendiente' }
]

// Arreglos
const items = ref([])
const lineasPedidoEditando = ref([])
const pedidoSeleccionado = ref(null)

// Columnas para la tabla principal
const columnasTableaPedidos = [
  {
    name: 'codigo',
    label: 'Código',
    align: 'left',
    field: 'codigo'
  },
  {
    name: 'usuario',
    label: 'Usuario',
    align: 'left',
    field: 'usuario'
  },
  {
    name: 'estado',
    label: 'Estado',
    align: 'center',
    field: 'estado'
  },
  {
    name: 'moneda',
    label: 'Moneda',
    align: 'center',
    field: 'moneda'
  },
  {
    name: 'total',
    label: 'Total',
    align: 'right',
    field: 'total'
  },
  {
    name: 'acciones',
    label: 'Acciones',
    align: 'center'
  }
]

// Columnas para la tabla de líneas del pedido
const columnasLineasPedido = [
  {
    name: 'nombreProducto',
    label: 'Producto',
    align: 'left',
    field: 'nombreProducto'
  },
  {
    name: 'precioUnitario',
    label: 'Precio Unitario',
    align: 'center',
    field: 'precioUnitario'
  },
  {
    name: 'cantidad',
    label: 'Cantidad',
    align: 'center',
    field: 'cantidad'
  },
  {
    name: 'cambio',
    label: 'Cambio',
    align: 'center',
    field: 'cambio'
  },
  {
    name: 'subtotal',
    label: 'Subtotal',
    align: 'center',
    field: 'subtotal'
  },
  {
    name: 'acciones',
    label: 'Acciones',
    align: 'center'
  }
]

// Computed para calcular el precio total del pedido
const precioTotalPedido = computed(() => {
  return lineasPedidoEditando.value.reduce((total, linea) => {
    return total + (linea.cantidad * linea.precioUnitario)
  }, 0)
})

// Computed para calcular el precio total con shipping y descuento
const precioTotalPedidoConCargos = computed(() => {
  const subtotal = lineasPedidoEditando.value.reduce((total, linea) => {
    return total + (linea.cantidad * linea.precioUnitario)
  }, 0)

  const shipping = pedidoSeleccionado.value?.shipping || 0
  const discount = pedidoSeleccionado.value?.discount || 0
  const precioGestor = pedidoSeleccionado.value?.precioGestor || 0

  return subtotal + precioGestor + shipping - discount
})

// Funciones
const formatearNumero = (num) => {
  if (num === null || num === undefined) return '0,00'
  return Number(num).toLocaleString('es-ES', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  })
}

const recalcularPrecio = (linea) => {
  if (linea.cantidad < 1) {
    linea.cantidad = 1
  }
}

const rechazarLinea = (idLinea) => {
  const index = lineasPedidoEditando.value.findIndex(l => l.id === idLinea)
  if (index > -1) {
    lineasPedidoEditando.value.splice(index, 1)
  }
}

const abrirDialogoEditarPedido = async (id, soloVer) => {
  pedidoConfirmado.value = false
  dialogLoad.value = true
  verDatosPedido.value = soloVer

  try {
    const { api } = await import('src/boot/axios')
    const response = await api.get(`Pedido/ObtenerPedidoPorId/${id}`)
    const pedidoTemp = response.data

    pedidoSeleccionado.value = {
      id: pedidoTemp.id,
      codigo: pedidoTemp.codigo,
      usuario: pedidoTemp.usuario,
      cupon: pedidoTemp.cupon,
      estado: pedidoTemp.estado,
      moneda: pedidoTemp.moneda,
      subtotal: pedidoTemp.subtotal,
      shipping: pedidoTemp.shipping,
      discount: pedidoTemp.discount,
      total: pedidoTemp.total,
      precioGestor: pedidoTemp.precioGestor || 0,
      direccion: pedidoTemp.direccion || ''
    }

    if (pedidoTemp.detalles && Array.isArray(pedidoTemp.detalles)) {
      lineasPedidoEditando.value = pedidoTemp.detalles.map(detalle => ({
        id: detalle.id,
        productoVarianteId: detalle.productoVarianteObtenidoDto?.id || '',
        nombreProducto: detalle.productoVarianteObtenidoDto?.nombreProducto || detalle.productoVarianteObtenidoDto?.descripcion || '',
        sku: detalle.productoVarianteObtenidoDto?.sku || '',
        codigo: detalle.productoVarianteObtenidoDto?.codigo || '',
        talla: detalle.productoVarianteObtenidoDto?.talla || '',
        color: detalle.productoVarianteObtenidoDto?.color || '-',
        precioUnitario: detalle.precioUnitario || 0,
        cantidad: detalle.cantidad || 1,
        cantidadInicial: detalle.cantidad || 1,
        descuentoAplicado: detalle.descuentoAplicado || 0,
        estadoLinea: detalle.estadoLinea || 'Pendiente'
      }))
    } else {
      lineasPedidoEditando.value = []
    }

    dialogEditarPedido.value = true
  } catch (error) {
    console.error('Error al obtener el pedido:', error)
    Error('Error al cargar el pedido')
  } finally {
    dialogLoad.value = false
  }
}

const confirmarPedido = async () => {
  try {
    dialogLoad.value = true
    const { api } = await import('src/boot/axios')

    const responseAuth = await api.get('/Autenticacion/UsuarioActual')
    const obj = responseAuth.data.result
    const Id = obj.id || ''

    const dtoActualizarPedido = {
      id: pedidoSeleccionado.value.id,
      codigo: pedidoSeleccionado.value.codigo,
      usuario: pedidoSeleccionado.value.usuario,
      estado: pedidoSeleccionado.value.estado,
      moneda: pedidoSeleccionado.value.moneda,
      cupon: pedidoSeleccionado.value.cupon || '',
      direccion: pedidoSeleccionado.value.direccion || '',
      precioGestor: pedidoSeleccionado.value.precioGestor || 0,
      subtotal: lineasPedidoEditando.value.reduce((total, linea) => total + (linea.cantidad * linea.precioUnitario), 0),
      shipping: pedidoSeleccionado.value.shipping || 0,
      discount: pedidoSeleccionado.value.discount || 0,
      total: precioTotalPedidoConCargos.value,
      vendedorId: Id,
      detalles: lineasPedidoEditando.value.map(linea => ({
        id: linea.id || null,
        productoVarianteId: linea.productoVarianteId,
        cantidad: linea.cantidad,
        precioUnitario: linea.precioUnitario,
        descuentoAplicado: linea.descuentoAplicado || 0,
        lineTotal: linea.cantidad * linea.precioUnitario,
        estadoLinea: linea.estadoLinea || 'Pendiente'
      }))
    }

    const response = await api.post('Pedido/ActualizarPedidoConLineas', dtoActualizarPedido)
    dialogLoad.value = false

    if (response.data.success || response.status === 200) {
      pedidoConfirmado.value = true
      Success('Pedido confirmado exitosamente')
      setTimeout(() => {
        load()
        cerrarDialogoEditar(false)
        window.location.reload()
      }, 150)
    }
  } catch (error) {
    console.error('Error al confirmar pedido:', error)
    Error('Error al confirmar el pedido')
  } finally {
    dialogLoad.value = false
  }
}

const cerrarDialogoEditar = (valor) => {
  if (valor === false) {
    dialogEditarPedido.value = false
    pedidoSeleccionado.value = null
    lineasPedidoEditando.value = []
    pedidoConfirmado.value = false
    verDatosPedido.value = false
  }
}

const load = async () => {
  items.value = await loadGetHastaData(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`) ?? []
}

const eliminar = async () => {
  await CancelarPedido(
    'Pedido/CancelarPedido',
    idElementoSeleccionado.value,
    load,
    dialogLoad
  )
  await load()
  setTimeout(() => {
    window.location.reload()
  }, 1500)
}

const abrirDialogoEliminar = (id) => {
  idElementoSeleccionado.value = id
  isDialogoEliminarAbierto.value = true
}

const handleCloseDialog = () => {
  isDialogoEliminarAbierto.value = false
}

const imprimir = () => {
  window.print()
}

onMounted(async () => {
  dialogLoad.value = true
  items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`) ?? []
  dialogLoad.value = false
})
</script>
