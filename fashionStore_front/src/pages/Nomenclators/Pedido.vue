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
        v-model:pagination="pagination"
         @request="onRequest"
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
            @update:model-value="load()"
          />

        <!--  <q-btn
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
          </q-btn>-->


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
              <q-btn v-show="props.row.estado === 'Rechazado'||
                  props.row.estado === 'Confirmado'"

                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id, true)"
                text-color="primary"
                icon="visibility"
              >
                <q-tooltip>Ver datos del pedido</q-tooltip>
              </q-btn>
              <q-btn v-show="props.row.estado !== 'Rechazado' &&
                  props.row.estado !== 'Confirmado'"

                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id, false)"
                text-color="primary"
                :icon="rol==='Cliente' ? 'visibility' : 'edit'"
              >
                <q-tooltip>{{ rol==='Cliente' ? 'Ver datos del pedido' : 'Editar líneas del pedido' }}</q-tooltip>
              </q-btn>
         <!--     <q-btn
                :disable="props.row.estado === 'Rechazado'"
                flat
                dense
                size="sm"
                @click="abrirDialogoEliminar(props.row.id)"
                text-color="negative"
                icon="delete"
              >
                <q-tooltip>Cancelar pedido</q-tooltip>
              </q-btn>-->
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
    <div v-if="props.row.tieneDescuento" class="row items-center justify-center q-gutter-sm">
      <span class="text-caption text-grey-6">
        <s>{{ formatearNumero(props.row.precioUnitarioOriginal) }}</s>
      </span>
      <span class="text-weight-bold text-primary">
        {{ formatearNumero(props.row.precioVentaDescuento) }}
      </span>
    </div>
    <div v-else>
      {{ formatearNumero(props.row.precioUnitario) }}
    </div>
  </q-td>
</template>


              <template v-slot:body-cell-cantidad="props">
                <q-td :props="props" class="text-center">
                  <q-input
                    v-if="!verDatosPedido && rol !== 'Cliente'"
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
    <div v-if="props.row.descuentoAplicado > 0" class="row items-center justify-center q-gutter-sm">
      <!-- Subtotal original tachado -->
      <span class="text-subtitle2 text-grey-6">
        <s>{{ formatearNumero((props.row.precioUnitario || 0) * (props.row.cantidad || 0)) }}</s>
      </span>
      <!-- Subtotal con descuento -->
      <span class="text-weight-bold text-primary">
        {{ formatearNumero(props.row.lineTotal || ((props.row.total || 0) * (props.row.cantidad || 0))) }}
      </span>
    </div>
    <div v-else>
      <span class="text-weight-bold">
        {{ formatearNumero((props.row.precioUnitario || 0) * (props.row.cantidad || 0)) }}
      </span>
    </div>
  </q-td>
</template>


              <template v-slot:body-cell-acciones="props">
                <q-td :props="props">
                  <q-btn
                    v-if="!verDatosPedido && rol !== 'Cliente'"
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
            v-show="!verDatosPedido && !pedidoConfirmado && rol !== 'Cliente'"
            class="text-white"
            color="primary"
            label="Rechazar Pedido"
            @click="abrirDialogoEliminar(pedidoSeleccionado.id)"
          />
          <q-btn
            v-if="!verDatosPedido && !pedidoConfirmado && rol !== 'Cliente'"
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

  <DialogCancelarPedido
            v-if="isDialogoEliminarAbierto"
            :isOpen="isDialogoEliminarAbierto"
            :idElemento="idElementoSeleccionado"
            @eliminar="eliminar"
            @closeDialog="handleCloseDialog"
          />

          <DialogLoad :dialogLoad="dialogLoad" />

          <q-dialog v-model="dialogComprobante">
  <q-card style="width: 400px; max-width: 90vw;">
    <q-card-section>
      <pre>{{ comprobanteTexto }}</pre>
    </q-card-section>
    <q-card-actions align="right">
      <q-btn flat label="Imprimir" color="primary" @click="printComprobante" />
      <q-btn flat label="Cerrar" color="primary" v-close-popup />
    </q-card-actions>
  </q-card>
</q-dialog>

</template>

<script setup>
import { ref, reactive, onMounted, computed, onBeforeUnmount } from 'vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import DialogCancelarPedido from 'src/components/DialogBoxes/DialogCancelarPedido.vue'
import { CancelarPedido, loadGetHastaData, loadGet, loadGetPaginados } from 'src/assets/js/util/funciones'
import { Error, Success } from 'src/assets/js/util/notify'
import signalRService from 'src/services/signalRService'

// Variables Booleanas
const dialogLoad = ref(false)
const verDatosPedido = ref(false)
const isDialogoEliminarAbierto = ref(false)
const dialogEditarPedido = ref(false)
const pedidoConfirmado = ref(false)
const rol = ref('')

const dialogComprobante = ref(false)
const comprobanteTexto = ref('')

// Paginación
const pagination = ref({
  page: 1,
  rowsPerPage: 10,
  rowsNumber: 0,
  sortBy: 'id',
  descending: false
})

// Variables
const idElementoSeleccionado = ref(null)
const filter = ref('')
const orden = ref('fechaCreado:desc')
const filtroEstado = ref('todos')

// Opciones para el filtro de estado
const opcionesFiltroEstado = [
  { label: 'Todos', value:null},
  { label: 'Confirmados', value: 2},
  { label: 'Rechazados', value: 3},
  { label: 'Pendientes', value:4 }
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
    field: 'monedaCodigo'
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
  // Si el usuario es Cliente, siempre es modo vista (solo lectura)
  verDatosPedido.value = rol.value === 'Cliente' ? true : soloVer

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
  precioUnitario: detalle.precioUnitario || 0,              // precio original
  precioUnitarioOriginal: detalle.precioUnitario || 0,      // 👈 guardamos el original
  precioVentaDescuento: detalle.descuentoAplicado > 0
    ? (detalle.lineTotal / (detalle.cantidad || 1))
    : detalle.precioUnitario || 0,                          // 👈 unitario con descuento
  cantidad: detalle.cantidad || 1,
  cantidadInicial: detalle.cantidad || 1,
  descuentoAplicado: detalle.descuentoAplicado || 0,
  lineTotal: detalle.lineTotal || 0,                        // 👈 guardamos el total con descuento
  estadoLinea: detalle.estadoLinea || 'Pendiente',
  tieneDescuento: (detalle.descuentoAplicado || 0) > 0
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

    if (response.data.success || response.status === 200) {
      pedidoConfirmado.value = true
      Success('Pedido confirmado exitosamente')

      // Aquí recibes el comprobante como string
      const comprobante = response.data // Mostrarlo en un diálogo o imprimir directamente
      mostrarComprobante(comprobante)

      // Disparar evento para actualizar en tiempo real en todas las ventanas
      const event = new CustomEvent('nuevoPedidoConfirmado', {
        detail: {
          id: pedidoSeleccionado.value.id,
          codigo: pedidoSeleccionado.value.codigo,
          estado: 'Confirmado',
          timestamp: new Date().toISOString()
        }
      })
      window.dispatchEvent(event)

      // Esperar a que el evento se procese completamente antes de recargar
      await new Promise(resolve => setTimeout(resolve, 500))

     // load()
       cerrarDialogoEditar(false)

     // window.location.reload()
    }
  } catch (error) {
    console.error('Error al confirmar pedido:', error)
    Error('Error al confirmar el pedido')
  } finally {
     await load()

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
/*
const load = async () => {
    dialogLoad.value = true

    // Si es Cliente, filtrar solo sus pedidos
    if(rol.value==='Cliente'){
      const token = localStorage.getItem('token') || sessionStorage.getItem('token')
      const payload = JSON.parse(atob(token.split('.')[1]))
      const usuarioId = payload.Id
      items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}&usuarioId=${usuarioId}`) ?? []
    }
    else{
      items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`) ?? []
    }

    dialogLoad.value = false
}*/
const load = async () => {
    dialogLoad.value = true
 const params = {
      cantidadIgnorar: (pagination.value.page - 1) * pagination.value.rowsPerPage,
      cantidadMostrar:
            pagination.value.rowsPerPage !== 0
                ? pagination.value.rowsPerPage
                : pagination.value.rowsNumber,
      secuenciaOrdenamiento: orden,
      textoBuscar: filter.value
    }

    let elementos = []
    let total = 0
    // Si es Cliente, filtrar solo sus pedidos
    if(rol.value==='Cliente'){
      const token = localStorage.getItem('token') || sessionStorage.getItem('token')
      const payload = JSON.parse(atob(token.split('.')[1]))
      const usuarioId = payload.Id

       const { elementos: elems, total: tot } =
        (await loadGetPaginados(
          `Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}&usuarioId=${usuarioId}`,
          params
        )) ?? { elementos: [], total: 0 }

      elementos = elems
      total = tot
    }
    else{

         const { elementos: elems, total: tot } =
        (await loadGetPaginados(
          `Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`,
          params
        )) ?? { elementos: [], total: 0 }

      elementos = elems
      total = tot
    }

     items.value = elementos
    pagination.value.rowsNumber = total

    dialogLoad.value = false
}

const eliminar = async () => {
    dialogLoad.value=true
  await CancelarPedido(
    'Pedido/CancelarPedido',
    idElementoSeleccionado.value,
  )

  // Disparar evento para actualizar en tiempo real en todas las ventanas
  const event = new CustomEvent('PedidoCancelado', {
    detail: {
      id: idElementoSeleccionado.value,
      timestamp: new Date().toISOString()
    }
  })
  window.dispatchEvent(event)

  await load()
    dialogLoad.value=false
dialogEditarPedido.value=false
  setTimeout(() => {
   // window.location.reload()
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
   const token = localStorage.getItem('token') || sessionStorage.getItem('token')
   const payload = JSON.parse(atob(token.split('.')[1]))
   rol.value= payload.Rol
   /*if(rol.value==='Cliente'){
    const usuarioId= payload.Id
  items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}&usuarioId=${usuarioId}`) ?? []
  }
  else{
  items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`) ?? []
  }*/

  // Conectar a SignalR para recibir actualizaciones en tiempo real
  await load()
  try {
    await signalRService.connect()

    // Listener para cuando se genera un nuevo pedido
    window.addEventListener('pedido-generado', handlePedidoActualizado)
    window.addEventListener('pedido-actualizado', handlePedidoActualizado)
    window.addEventListener('pedidoEliminado', handlePedidoActualizado)
    window.addEventListener('pedido-cancelado', handlePedidoActualizado)

    // Listener personalizado para actualizaciones de pedidos
    signalRService.onPedidoGenerado(async() => {
      //console.log('🔄 Recibido evento de actualización de pedido, recargando lista...')
     await load()

    })
  } catch (error) {
    console.error('Error conectando a SignalR:', error)
  }
})

// Limpiar listeners al desmontar componente
onBeforeUnmount(() => {
  window.removeEventListener('pedido-generado', handlePedidoActualizado)
  window.removeEventListener('pedido-actualizado', handlePedidoActualizado)
  window.removeEventListener('pedidoEliminado', handlePedidoActualizado)
  window.removeEventListener('pedido-cancelado', handlePedidoActualizado)
  // Mantener la conexión SignalR activa para otras páginas
})

// Manejador de evento de pedido actualizado
const handlePedidoActualizado = async(event) => {
  console.log('📦 Evento de pedido actualizado recibido:', event.detail)
  // Recargar la lista de pedidos cuando hay cambios
 await load()
}

const onRequest = async (props) => {
  pagination.value = props.pagination
  await load()
}



function mostrarComprobante(texto) {
  comprobanteTexto.value = texto
  dialogComprobante.value = true
}

// Función para imprimir
function printComprobante() {
  // Abrir ventana temporal con el comprobante y disparar impresión
  const printWindow = window.open('', '', 'width=600,height=400')
  printWindow.document.write('<pre>' + comprobanteTexto.value + '</pre>')
  printWindow.document.close()
  printWindow.focus()
  printWindow.print()
  printWindow.close()
}


</script>

<style scoped lang="scss">
/* Media Queries para responsividad - Nomencladores */
@media (max-width: 599px) {
  :deep(.q-table) {
    font-size: 12px;
  }

  :deep(.q-table__card) {
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  }

  :deep(.q-th) {
    padding: 8px 4px;
    font-size: 11px;
  }

  :deep(.q-td) {
    padding: 8px 4px;
    font-size: 11px;
  }

  :deep(.q-input) {
    font-size: 14px;
  }

  :deep(.q-dialog__inner) {
    padding: 10px;
  }

  :deep(.q-field__control) {
    min-height: 32px;
  }
}

@media (max-width: 1023px) and (min-width: 600px) {
  :deep(.q-table) {
    font-size: 13px;
  }

  :deep(.q-th) {
    padding: 12px 6px;
  }

  :deep(.q-td) {
    padding: 12px 6px;
  }
}

@media (min-width: 1366px) {
  :deep(.q-table) {
    font-size: 14px;
  }

  :deep(.q-th) {
    padding: 16px 8px;
  }

  :deep(.q-td) {
    padding: 16px 8px;
  }
}
</style>
