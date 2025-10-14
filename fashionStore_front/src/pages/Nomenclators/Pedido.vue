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
        <q-dialog v-model="dialog" persistent>
          <q-card style="width: 700px; max-width: 80vw; height: auto">
            <header class="q-pa-sm bg-primary">
              <q-toolbar>
                <q-toolbar-title class="text-subtitle6 text-white">
                  {{
                    objeto.id ? "Editar Pedido" : "Adicionar Pedido"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
            <q-form @submit.prevent="Guardar()" @reset="close" ref="myForm">
              <div class="h row q-ma-md">
                <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  :disable="!!objeto.id"
                  label="Código *"
                  v-model="objeto.codigo"
                  color="primary"
                  counter
                  maxlength="50"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Código',
                    (val) =>
                      (items.length > 0
                        ? !isValorRepetido(val, 'codigo', objeto, items)
                        : true) || 'Ya existe un codigo con ese valor',
                  ]"
                />
                   <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  :disable="!!objeto.id"
                  label="sku *"
                  v-model="objeto.sku"
                  color="primary"
                  counter
                  maxlength="100"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un sku',
                    (val) =>
                      (items.length > 0
                        ? !isValorRepetido(val, 'sku', objeto, items)
                        : true) || 'Ya existe un sku con ese valor',
                  ]"
                />

                <q-input
                  class="col-xs-12 q-pa-sm"
                  label="Descripción *"
                  v-model="objeto.descripcion"
                  color="primary"
                  counter
                  autogrow
                  maxlength="100"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Descripción',
                  ]"
                />

<q-input
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Precio Costo *"
  v-model.number="objeto.precioCosto"
  type="text"
  color="primary"
  lazy-rules
  :rules="[
    val => !!val || 'Precio Costo es obligatoria',
    val => parseFloat(val) > 0 || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearPrecioCosto"
  @focus="quitarFormatoPrecioCosto"
/>

<q-input
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Precio Venta *"
  v-model.number="objeto.precioVenta"
  type="text"
  color="primary"
  lazy-rules
  :rules="[
    val => !!val || 'Precio Venta es obligatoria',
    val => parseFloat(val) > 0 || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearPrecioVenta"
  @focus="quitarFormatoPrecioVenta"
/>

<q-select
                        class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
                        v-model="objeto.monedaId"
                        label="Moneda *"
                        emit-value
                        map-options
                        :use-input="
                            objeto.monedaId === null ||
                            objeto.monedaId === ''
                        "
                        option-label="codigo"
                        option-value="id"
                        :options="filtradoMoneda"
                        @filter="
                            (val, update) => {
                                filtradoMoneda = filterOptions(
                                    val,
                                    update,
                                    filtradoMoneda,
                                    'codigo',
                                    itemsMoneda
                                );
                            }
                        "
                        lazy-rules
                        :rules="[
                            (val) =>
                                (val !== null && val !== '') ||
                                'Debe seleccionar un elemento',
                        ]"
                    >
                        <template v-slot:no-option>
                            <q-item>
                                <q-item-section class="text-italic text-grey">
                                    No hay elementos disponibles
                                </q-item-section>
                            </q-item>
                        </template>
                    </q-select>

<q-field
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Color"
  stack-label
>
  <template v-slot:control>
    <div class="row items-center no-wrap">
      <!-- Vista previa del color -->
      <div
        :style="{
          width: '20px',
          height: '20px',
          borderRadius: '4px',
          border: '1px solid #ccc',
          backgroundColor: objeto.color || '#ffffff'
        }"
      ></div>

      <!-- Icono para abrir el selector -->
      <q-icon
        name="colorize"
        class="q-ml-sm cursor-pointer"
      >
        <q-popup-proxy cover transition-show="scale" transition-hide="scale">
          <q-color
            v-model="objeto.color"
            format-model="hex"
            default-view="palette"
          />
        </q-popup-proxy>
      </q-icon>
    </div>
  </template>
</q-field>



 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-12 col-sm-12 col-md-6 q-mt-sm justify-end q-mr-md"
                        right-label
                        v-model="objeto.esActivo"
                        label="Activo"
                        color="primary"
                    />



                <q-card-actions class="col-12 q-mt-lg justify-end">
                  <q-btn
                    class="text-white"
                    color="primary"
                    aling="right"
                    type="submit"
                    label="Guardar"
                  />
                  <q-btn
                    outline
                    color="primary"
                    type="reset"
                    label="Cancelar"
                  />
                </q-card-actions>
              </div>
            </q-form>
          </q-card>
        </q-dialog>
        <DialogEliminar
          v-if="isDialogoEliminarAbierto"
          :isOpen="isDialogoEliminarAbierto"
          :idElemento="Number(idElementoSeleccionado)"
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
      <!-- Vista previa del color -->
      <div
        :style="{
          width: '20px',
          height: '20px',
          borderRadius: '4px',
          border: '1px solid #ccc',
          backgroundColor: props.row?.color || '#ffffff'
        }"
      ></div></div>
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

            <template v-slot:body-cell-precioCosto="props">
  <q-td :props="props">
    <div>
      {{
        props.row?.precioCosto != null
          ? Number(props.row.precioCosto).toLocaleString('es-ES', {
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            })
          : ''
      }}
      <q-tooltip>
        {{
          props.row?.precioCosto != null
            ? Number(props.row.precioCosto).toLocaleString('es-ES', {
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
              @click="obtenerElementoPorId(props.row.id)"
              text-color="primary"
              icon="edit"
            >
              <q-tooltip>Editar datos</q-tooltip>
            </q-btn>
            <q-btn
              flat
              dense
              size="sm"
              @click="abrirDialogoEliminar(props.row.id)"
              text-color="negative"
              icon="delete"
            >
              <q-tooltip>Eliminar</q-tooltip>
            </q-btn>
          </div>
        </q-td>
      </template>

    </q-table>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import DialogEliminar from 'components/DialogBoxes/DialogEliminar.vue'

import { dataColumnPedido } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)

// Variables vacias
const filter = ref('')

// Arreglos
const items = ref([])
const itemsMoneda = ref([])
const filtradoMoneda = ref([])

const objetoInicial = {
  // id: null,
  codigo: null,
    sku: null,
  descripcion: null,
  precioCosto:0.00,
    precioVenta:0.00,
     color:"#ffffff",
       monedaId:null,
    esActivo:true
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'Pedido/Actualizar' : 'Pedido/Crear'

  objeto.precioCosto= objeto.precioCosto
      ? parseFloat(
          objeto.precioCosto
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0

  objeto.precioVenta= objeto.precioVenta
      ? parseFloat(
          objeto.precioVenta
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0

  saveData(url, objeto, load, close, dialogLoad)
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  await obtener('Pedido/ObtenerPorId', id, objeto, dialogLoad, dialog)
  filtradoMoneda.value = itemsMoneda.value
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Pedido/Eliminar',
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

// 2- Funcion para pasar por parametro el arreglo de los elmentos de la tabla
const load = async () => {
  items.value = await loadGet('Pedido/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Pedido/ObtenerListadoPaginado')??[]
  itemsMoneda.value = await loadGet('Moneda/ObtenerListadoPaginado')??[]

  filtradoMoneda.value=itemsMoneda.value
  dialogLoad.value = false
})

const formatearPrecioCosto = () => {
  if (objeto.precioCosto !== null && objeto.precioCosto !== undefined) {
    const valor = Number(objeto.precioCosto);
    if (!isNaN(valor)) {
      objeto.precioCosto = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormatoPrecioCosto = () => {
  if (typeof objeto.precioCosto === 'string') {
    objeto.precioCosto = objeto.precioCosto.replace(/\./g, '').replace(',', '.');
    objeto.precioCosto = parseFloat(objeto.precioCosto);
  }
};

const formatearPrecioVenta = () => {
  if (objeto.precioVenta !== null && objeto.precioVenta !== undefined) {
    const valor = Number(objeto.precioVenta);
    if (!isNaN(valor)) {
      objeto.precioVenta = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormatoPrecioVenta = () => {
  if (typeof objeto.precioVenta === 'string') {
    objeto.precioVenta = objeto.precioVenta.replace(/\./g, '').replace(',', '.');
    objeto.precioVenta = parseFloat(objeto.precioVenta);
  }
};



</script>
