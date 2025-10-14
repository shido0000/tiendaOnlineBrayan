<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Inventario" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnInventario"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Productos en Inventario</span>
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
          class="bg-primary"
          style="width: 20px"
          color="primary"
          icon="add"
          @click="dialog = true"
        >
          <q-tooltip
            class="bg-primary"
            transition-show="flip-right"
            transition-hide="flip-left"
            :offset="[10, 10]"
            >Adicionar</q-tooltip
          >
        </q-btn>
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
                    objeto.id ? "Editar producto del Inventario" : "Adicionar producto a Inventario"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
            <q-form @submit.prevent="Guardar()" @reset="close" ref="myForm">
              <div class="h row q-ma-md">
                <q-select v-if="!objeto.id"
                        class="col-xs-12 col-sm-12 col-md-8 q-pa-sm"
                        multiple
                        v-model="objeto.productoIds"
                        label="Producto *"
                        emit-value
                        map-options
                        :use-input="
                            objeto.productoIds === null ||
                            objeto.productoIds === '' ||  objeto.productoIds.length === 0
                        "
                        option-label="codigo"
                        option-value="id"
                        :options="filtradoProducto"
                        @filter="
                            (val, update) => {
                                filtradoProducto = filterOptions(
                                    val,
                                    update,
                                    filtradoProducto,
                                    'codigo',
                                    itemsProducto
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

                         <template v-slot:selected-item="scope">
                                        <span v-if="scope.index === 0">
                                            {{ scope.opt.codigo }}</span
                                        >
                                        <span
                                            v-if="
                                                scope.index === 1 &&
                                                objeto.productoIds
                                                    .length > 1
                                            "
                                            class="grey--text caption"
                                            style="margin-left: 8px"
                                        >
                                            (+{{
                                                objeto.productoIds
                                                    .length - 1
                                            }}
                                            más)</span
                                        >
                                    </template>
                    </q-select>

<q-input v-if="objeto.id===undefined || objeto.id===null"
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Cantidad Disponible *"
  v-model.number="objeto.cantidadDisponible"
  type="text"
  color="primary"
  lazy-rules
  :rules="[
    val => !!val || 'La tasa de cambio es obligatoria',
    val => parseInt(val) > 0 || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
/>

<q-input v-else
  class="col-xs-12 col-sm-12 col-md-12 q-pa-sm"
  label="Cantidad Disponible *"
  v-model.number="objeto.cantidadDisponible"
  type="text"
  color="primary"
  lazy-rules
  :rules="[
    val => !!val || 'La tasa de cambio es obligatoria',
    val => parseInt(val) > 0 || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
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
  <template v-slot:body-cell-productoCodigo="props">
                <q-td :props="props">
                    <div>
                        {{
                            PonerPuntosSupensivosACampo(
                                props.row?.productoCodigo,
                                30
                            )
                        }}
                        <q-tooltip>{{ props.row?.productoCodigo }}</q-tooltip>
                    </div>
                </q-td>
            </template>

  <template v-slot:body-cell-productoDescripcion="props">
                <q-td :props="props">
                    <div>
                        {{
                            PonerPuntosSupensivosACampo(
                                props.row?.productoDescripcion,
                                30
                            )
                        }}
                        <q-tooltip>{{ props.row?.productoDescripcion }}</q-tooltip>
                    </div>
                </q-td>
            </template>

              <template v-slot:body-cell-productoSku="props">
                <q-td :props="props">
                    <div>
                        {{
                            PonerPuntosSupensivosACampo(
                                props.row?.productoSku,
                                30
                            )
                        }}
                        <q-tooltip>{{ props.row?.productoSku }}</q-tooltip>
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

import { dataColumnInventario } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'
import { Error } from 'src/boot/notify'

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
const itemsTipoCodigo = ref([])
const filtradoCategoria = ref([])
const itemsProducto = ref([])
const filtradoProducto = ref([])

const objetoInicial = {
  // id: null,
   productoIds:[],
  cantidadDisponible:0,
  cantidadReservada:0,
  productoCodigo:"",
  productoDescripcion:"",
  productoSku:"",
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
const objetoCopia = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'Inventario/Actualizar' : 'Inventario/Crear'
  console.log(objeto)

  objeto.cantidadDisponible= objeto.cantidadDisponible
      ? parseInt(
          objeto.cantidadDisponible
            .toString()
        )
      : 0

      if(objeto.id && objeto.cantidadReservada==objetoCopia.cantidadDisponible && objetoCopia.cantidadDisponible>objeto.cantidadDisponible){
        Error("Tiene ese producto reservado no puede rebajarlo")
      }
      else{
  saveData(url, objeto, load, close, dialogLoad)
      }
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  filtradoCategoria.value = itemsTipoCodigo
  await obtener('Inventario/ObtenerPorId', id, objeto, dialogLoad, dialog)

  Object.assign(objetoCopia,objeto)
  filtradoProducto.value=itemsProducto.value
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Inventario/Eliminar',
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
  items.value = await loadGet('Inventario/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Inventario/ObtenerListadoPaginado')??[]
    itemsProducto.value = await loadGet(`Producto/ObtenerListadoPaginado?EsProductosParaInventario=${true}`)??[]

  filtradoProducto.value=itemsProducto.value
  dialogLoad.value = false
})

const formatearTasaCambio = () => {
  if (objeto.cantidadDisponible !== null && objeto.cantidadDisponible !== undefined) {
    const valor = Number(objeto.cantidadDisponible);
    if (!isNaN(valor)) {
      objeto.cantidadDisponible = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormato = () => {
  if (typeof objeto.cantidadDisponible === 'string') {
    objeto.cantidadDisponible = objeto.cantidadDisponible.replace(/\./g, '').replace(',', '.');
    objeto.cantidadDisponible = parseFloat(objeto.cantidadDisponible);
  }
};

</script>
