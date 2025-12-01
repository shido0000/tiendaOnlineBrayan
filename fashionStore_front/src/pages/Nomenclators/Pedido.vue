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
        :columns="dataColumnPedido"
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
        <template v-slot:body-cell-codigo="props">
          <q-td :props="props">
            <div>
              {{
                PonerPuntosSupensivosACampo(
                  props.row?.codigo,
                  30
                )
              }}
              <q-tooltip>{{ props.row?.codigo }}</q-tooltip>
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-sku="props">
          <q-td :props="props">
            <div>
              {{
                PonerPuntosSupensivosACampo(
                  props.row?.sku,
                  30
                )
              }}
              <q-tooltip>{{ props.row?.sku }}</q-tooltip>
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-descripcion="props">
          <q-td :props="props">
            <div>
              {{
                PonerPuntosSupensivosACampo(
                  props.row?.descripcion,
                  30
                )
              }}
              <q-tooltip>{{ props.row?.descripcion }}</q-tooltip>
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-monedaCodigo="props">
          <q-td :props="props">
            <div>
              {{
                PonerPuntosSupensivosACampo(
                  props.row?.monedaCodigo,
                  30
                )
              }}
              <q-tooltip>{{ props.row?.monedaCodigo }}</q-tooltip>
            </div>
          </q-td>
        </template>

        <template v-slot:body-cell-color="props">
          <q-td :props="props">
            <div class="row items-center no-wrap">
              <div
                :style="{
                  width: '20px',
                  height: '20px',
                  borderRadius: '4px',
                  border: '1px solid #ccc',
                  backgroundColor: props.row?.color || '#ffffff'
                }"
              ></div>
            </div>
          </q-td>
        </template>

        <template v-slot:body-cell-esActivo="props">
          <q-td :props="props">
            <q-icon
              flat
              :name="
                props.value == 0 ? 'highlight_off' : 'check_circle'
              "
              :class="props.value == 0 ? 'text-grey' : 'text-primary'"
              size="20px"
            />
          </q-td>
        </template>

        <template v-slot:body-cell-total="props">
          <q-td :props="props">
            <div>
              {{
                props.row?.total != null
                  ? Number(props.row.total).toLocaleString('es-ES', {
                      minimumFractionDigits: 2,
                      maximumFractionDigits: 2
                    })
                  : ''
              }}
              <q-tooltip>
                {{
                  props.row?.total != null
                    ? Number(props.row.total).toLocaleString('es-ES', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                      })
                    : ''
                }}
              </q-tooltip>
            </div>
          </q-td>
        </template>

        <template v-slot:body-cell-precioVenta="props">
          <q-td :props="props">
            <div>
              {{
                props.row?.precioVenta != null
                  ? Number(props.row.precioVenta).toLocaleString('es-ES', {
                      minimumFractionDigits: 2,
                      maximumFractionDigits: 2
                    })
                  : ''
              }}
              <q-tooltip>
                {{
                  props.row?.precioVenta != null
                    ? Number(props.row.precioVenta).toLocaleString('es-ES', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                      })
                    : ''
                }}
              </q-tooltip>
            </div>
          </q-td>
        </template>

        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
                  <q-btn
                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id,true)"
                text-color="primary"
                icon="visibility"
              >
                <q-tooltip>Ver datos del pedido</q-tooltip>
              </q-btn>
              <q-btn
               :disable="props.row.estado==='Rechazado' || props.row.estado==='Confirmado'"
                flat
                dense
                size="sm"
                @click="abrirDialogoEditarPedido(props.row.id,false)"
                text-color="primary"
                icon="edit"
              >
                <q-tooltip>Editar líneas del pedido</q-tooltip>
              </q-btn>
              <q-btn
              :disable="props.row.estado==='Rechazado'"
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
              Editar Pedido - {{ pedidoSeleccionado?.codigo }}
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
                    :color="pedidoSeleccionado?.estado === 'Pendiente' ? 'warning' : 'positive'"
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

              <!-- Resumen de Precios Original -->
              <div class="row q-col-gutter-md q-mb-lg">
                <div class="col-12">
                  <div class="text-subtitle2 q-mb-md">Resumen Inicial del Pedido</div>
                </div>
                <div class="col-6 col-sm-3">
                  <div class="q-pa-md bg-grey-2 rounded-borders">
                    <div class="text-caption text-grey">Subtotal</div>
                    <div class="text-weight-bold">{{ formatearNumero(pedidoSeleccionado?.subtotal) }}</div>
                  </div>
                </div>
                <div class="col-6 col-sm-3">
                  <div class="q-pa-md bg-grey-2 rounded-borders">
                    <div class="text-caption text-grey">Envío</div>
                    <div class="text-weight-bold">{{ formatearNumero(pedidoSeleccionado?.shipping) }}</div>
                  </div>
                </div>
                <div class="col-6 col-sm-3">
                  <div class="q-pa-md bg-grey-2 rounded-borders">
                    <div class="text-caption text-grey">Descuento</div>
                    <div class="text-weight-bold text-negative">-{{ formatearNumero(pedidoSeleccionado?.discount) }}</div>
                  </div>
                </div>
                <div class="col-6 col-sm-3">
                  <div class="q-pa-md bg-blue-1 rounded-borders">
                    <div class="text-caption text-grey">Total Original</div>
                    <div class="text-weight-bold text-blue-9">{{ formatearNumero(pedidoSeleccionado?.total) }}</div>
                  </div>
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
                    <q-td :props="props" class="text-center" v-if="!verDatosPedido">
                      <q-input
                        v-model.number="props.row.cantidad"
                        type="number"
                        min="1"
                        dense
                        outlined
                        @update:model-value="recalcularPrecio(props.row)"
                        style="max-width: 80px"
                      />

                    </q-td>
                      <q-td :props="props" class="text-center" v-else>
                      {{ props.row.cantidad }}
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

                  <template  v-slot:body-cell-acciones="props">
                    <q-td :props="props">
                      <q-btn
                     :disable="verDatosPedido"
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

              <!-- Resumen de Precios Actualizado -->
              <div class="row q-mt-lg justify-end q-col-gutter-md">
                <div class="col-12 col-sm-6">
                  <div class="row q-col-gutter-sm">
                    <div class="col-12 row items-center justify-between q-pa-md bg-grey-2 rounded-borders">
                      <div class="row items-center q-col-gutter-xs">
                        <span class="text-weight-bold">Nuevo Subtotal (Productos):</span>
                        <div v-if="lineasPedidoEditando.some(linea => linea.cantidad !== linea.cantidadInicial)" class="row items-center q-col-gutter-xs">
                          <div v-if="precioTotalPedido > lineasPedidoEditando.reduce((total, linea) => total + (linea.cantidadInicial * linea.precioUnitario), 0)" class="row items-center q-col-gutter-xs">
                            <q-icon name="trending_up" color="positive" size="xs" />
                            <span class="text-positive text-caption">+{{ formatearNumero(precioTotalPedido - lineasPedidoEditando.reduce((total, linea) => total + (linea.cantidadInicial * linea.precioUnitario), 0)) }}</span>
                          </div>
                          <div v-else-if="precioTotalPedido < lineasPedidoEditando.reduce((total, linea) => total + (linea.cantidadInicial * linea.precioUnitario), 0)" class="row items-center q-col-gutter-xs">
                            <q-icon name="trending_down" color="negative" size="xs" />
                            <span class="text-negative text-caption">{{ formatearNumero(precioTotalPedido - lineasPedidoEditando.reduce((total, linea) => total + (linea.cantidadInicial * linea.precioUnitario), 0)) }}</span>
                          </div>
                        </div>
                      </div>
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
                      <span class="text-weight-bold text-h6">Nuevo Total:</span>
                      <span class="text-weight-bold text-blue-9 text-h5">
                        {{ formatearNumero(precioTotalPedidoConCargos) }}
                      </span>
                    </div>
                    <div class="col-12 row items-center justify-between q-pa-md" :class="precioTotalPedidoConCargos !== pedidoSeleccionado?.total ? 'bg-orange-1' : 'bg-green-1'">
                      <span class="text-weight-bold">Diferencia respecto al original:</span>
                      <span class="text-weight-bold text-h6" :class="precioTotalPedidoConCargos !== pedidoSeleccionado?.total ? 'text-orange-9' : 'text-green-9'">
                        {{ (precioTotalPedidoConCargos - (pedidoSeleccionado?.total || 0)) >= 0 ? '+' : '' }}{{ formatearNumero(precioTotalPedidoConCargos - (pedidoSeleccionado?.total || 0)) }}
                      </span>
                    </div>
                  </div>
                </div>
              </div>
            </q-card-section>

            <q-card-actions v-if="!verDatosPedido" class="justify-end q-pa-md">
              <q-btn
                outline
                color="primary"
                label="Cancelar"
                @click="cerrarDialogoEditar(false)"
              />
              <q-btn
                v-if="!pedidoConfirmado"
                class="text-white"
                color="primary"
                label="Confirmar Pedido"
                @click="confirmarPedido"
                :disable="lineasPedidoEditando.length === 0"
              />
              <q-btn
                v-else
                disable
                class="text-white"
                color="positive"
                label="Pedido Confirmado"
              />
            </q-card-actions>
                    <q-card-actions v-else class="justify-end q-pa-md">
              <q-btn
                outline
                color="primary"
                label="Salir"
                @click="cerrarDialogoEditar(false)"
                style="width: 100px;"

              />
            </q-card-actions>
      </q-card>
    </div>

    <!-- Dialogo de Eliminar -->
    <div v-if="!dialogEditarPedido">
      <DialogCancelarPedido
        v-if="isDialogoEliminarAbierto"
        :isOpen="isDialogoEliminarAbierto"
        :idElemento="idElementoSeleccionado"
        @eliminar="eliminar"
        @closeDialog="handleCloseDialog"
      />

      <DialogLoad :dialogLoad="dialogLoad" />
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed } from 'vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import DialogEliminar from 'components/DialogBoxes/DialogEliminar.vue'

import { dataColumnPedido } from 'src/assets/js/column_data/columnDataNomencladores'
import { CancelarPedido, closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'
import DialogCancelarPedido from 'src/components/DialogBoxes/DialogCancelarPedido.vue'
import { Error, Success } from 'src/assets/js/util/notify'
//import {  iniciarConexionPedidos } from "src/assets/js/util/pedidosHub";

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const verDatosPedido = ref(false)

const isDialogoEliminarAbierto = ref(false)
const dialogEditarPedido = ref(false)
const pedidoConfirmado = ref(false)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)

// Variables vacias
const filter = ref('')
const orden = ref('fechaCreado:desc')
const filtroEstado = ref('todos')

// Opciones para el filtro de estado
const opcionesFiltroEstado = [
  { label: 'Todos', value: 1 },
  { label: 'Confirmados', value: 2 },
  { label: 'Rechazados', value: 3 },
  { label: 'Pendientes', value: 4}
]


// Arreglos
const items = ref([])
const itemsMoneda = ref([])
const filtradoMoneda = ref([])
const lineasPedidoEditando = ref([])

// Objeto reactivo para el pedido en edición
const pedidoSeleccionado = ref(null)

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

const objetoInicial = {
  codigo: null,
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })

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


  return subtotal +precioGestor+ shipping - discount
})

// Funciones
// Función para formatear números
const formatearNumero = (num) => {
  if (num === null || num === undefined) return '0,00'
  return Number(num).toLocaleString('es-ES', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  })
}

// Función para recalcular el precio cuando cambia la cantidad
const recalcularPrecio = (linea) => {
  if (linea.cantidad < 1) {
    linea.cantidad = 1
  }
  // El computed precioTotalPedido se actualiza automáticamente
}

// Función para rechazar una línea del pedido
const rechazarLinea = (idLinea) => {
  const index = lineasPedidoEditando.value.findIndex(l => l.id === idLinea)
  if (index > -1) {
    lineasPedidoEditando.value.splice(index, 1)
  }
}

// Función para abrir el diálogo de editar pedido
const abrirDialogoEditarPedido = async (id,soloVer) => {
  pedidoConfirmado.value = false
  dialogLoad.value = true
verDatosPedido.value=soloVer
  try {
    // Importar axios si no está disponible globalmente
    const { api } = await import('src/boot/axios')

    // Obtener el pedido directamente desde la API
    const response = await api.get(`Pedido/ObtenerPedidoPorId/${id}`)
    const pedidoTemp = response.data

    console.log("pedidoTemp: ", pedidoTemp)

    // Asignar el pedido seleccionado con los datos obtenidos
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

    // Cargar las líneas del pedido desde detalles
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
        cantidadInicial: detalle.cantidad || 1, // Guardar la cantidad inicial
        descuentoAplicado: detalle.descuentoAplicado || 0,
        descuento: detalle.descuento || '-',
        estadoLinea: detalle.estadoLinea || 'Pendiente'
      }))
    } else {
      lineasPedidoEditando.value = []
    }

    dialogEditarPedido.value = true
  } catch (error) {
    console.error('Error al obtener el pedido:', error)
  } finally {
    dialogLoad.value = false
  }
}

// Función para confirmar el pedido
/*const confirmarPedido = async () => {
  try {
    dialogLoad.value = true

    // Construir el DTO para enviar al backend
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
      // Array de líneas del pedido actualizado
      detalles: lineasPedidoEditando.value.map(linea => ({
        id: linea.id,
        descuento: linea.descuento || '-',
        productoVarianteObtenidoDto: {
          id: linea.productoVarianteId || '',
          talla: linea.talla || '',
          color: linea.color || '-',
          codigo: linea.codigo || '',
          nombreProducto: linea.nombreProducto || '',
          descripcion: linea.nombreProducto || '',
          sku: linea.sku || '',
          precioVenta: linea.precioUnitario
        },
        cantidad: linea.cantidad,
        precioUnitario: linea.precioUnitario,
        descuentoAplicado: linea.descuentoAplicado || 0,
        lineTotal: linea.cantidad * linea.precioUnitario,
        estadoLinea: linea.estadoLinea || 'Pendiente'
      }))
    }

    console.log("DTO a enviar al backend:", dtoActualizarPedido)

    // Importar axios para hacer la llamada
    const { api } = await import('src/boot/axios')

    // Enviar actualización al servidor
    const response = await api.put('Pedido/ActualizarPedidoConLineas', dtoActualizarPedido)

    if (response.data.success || response.status === 200) {
      pedidoConfirmado.value = true

      // Mostrar notificación de éxito
      const { notifySuccess } = await import('src/assets/js/util/notify')
      notifySuccess('Pedido confirmado exitosamente')

      // Recargar los datos después de 2 segundos
      setTimeout(() => {
        load() // Recargar la tabla principal
        cerrarDialogoEditar(false)
      }, 2000)
    }
  } catch (error) {
    console.error('Error al confirmar pedido:', error)

    // Mostrar notificación de error
    const { notifyError } = await import('src/assets/js/util/notify')
    notifyError('Error al confirmar el pedido')
  } finally {
    dialogLoad.value = false
  }
}*/

const confirmarPedido = async () => {
  try {
    dialogLoad.value = true

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

      // 🔑 Array de líneas
      detalles: lineasPedidoEditando.value.map(linea => ({
        id: linea.id || null, // null → insertar nueva
        productoVarianteId: linea.productoVarianteId, // clave foránea
        cantidad: linea.cantidad,
        precioUnitario: linea.precioUnitario,
        descuentoAplicado: linea.descuentoAplicado || 0,
        lineTotal: linea.cantidad * linea.precioUnitario,
        estadoLinea: linea.estadoLinea || 'Pendiente'
      }))
    }

    console.log("DTO a enviar al backend:", dtoActualizarPedido)

    const { api } = await import('src/boot/axios')
    dialogLoad.value=true
    const response = await api.post('Pedido/ActualizarPedidoConLineas', dtoActualizarPedido)
    dialogLoad.value=false

    if (response.data.success || response.status === 200) {
      pedidoConfirmado.value = true
      Success('Pedido confirmado exitosamente')
      setTimeout(() => {
        load()
        cerrarDialogoEditar(false)
      }, 2000)
    }
  } catch (error) {
    console.error('Error al confirmar pedido:', error)
    Error('Error al confirmar el pedido')
  } finally {
    dialogLoad.value = false
  }
}



// Función para cerrar el diálogo de editar
const cerrarDialogoEditar = (valor) => {
  if (valor === false) {
    dialogEditarPedido.value = false
    pedidoSeleccionado.value = null
    lineasPedidoEditando.value = []
    pedidoConfirmado.value = false
    verDatosPedido.value = false

  }
}

// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = 'Pedido/Crear'

  saveData(url, objeto, load, close, dialogLoad)
}

// 2- Funcion para pasar por parametro el arreglo de los elmentos de la tabla
const load = async () => {
  items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`)??[]
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await CancelarPedido(
    'Pedido/CancelarPedido',
    idElementoSeleccionado.value,
    load,
    dialogLoad
  )
}

// Funcion para abrir el dialog de eliminar y pasar el id del elemento
const abrirDialogoEliminar = (id) => {
  idElementoSeleccionado.value = id
  isDialogoEliminarAbierto.value = true
}

// Funcion para cerrar el dialog
const handleCloseDialog = () => {
  isDialogoEliminarAbierto.value = false
}

// Funcion para cerrar el dialog principal de Adicionar y Editar y resetear los campos del formulario
const close = async () => {
  closeDialog(objeto, objetoInicial, myForm, dialog)
}

// Funcion para cargar los datos al cargar la pagina
onMounted(async () => {
  dialogLoad.value = true
  items.value = await loadGet(`Pedido/ObtenerListadoPaginado?SecuenciaOrdenamiento=${orden.value}&estado=${filtroEstado.value}`)??[]
  itemsMoneda.value = await loadGet('Moneda/ObtenerListadoPaginado')??[]
//await iniciarConexionPedidos();
  filtradoMoneda.value=itemsMoneda.value
  dialogLoad.value = false
})
</script>
