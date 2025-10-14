<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Monedas" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnMoneda"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Monedas</span>
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
                    objeto.id ? "Editar Moneda" : "Adicionar Moneda"
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
  class="col-xs-12 q-pa-sm"
  label="Tasa de Cambio *"
  v-model.number="objeto.tasaCambio"
  type="text"
  color="primary"
  lazy-rules
  :rules="[
    val => !!val || 'La tasa de cambio es obligatoria',
    val => parseFloat(val) > 0 || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearTasaCambio"
  @focus="quitarFormato"
/>

 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-6 col-sm-6 col-md-5 q-mt-sm"
                        right-label
                        v-model="objeto.esActiva"
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

                          <template v-slot:body-cell-esActiva="props">
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

            <template v-slot:body-cell-tasaCambio="props">
  <q-td :props="props">
    <div>
      {{
        props.row?.tasaCambio != null
          ? Number(props.row.tasaCambio).toLocaleString('es-ES', {
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            })
          : ''
      }}
      <q-tooltip>
        {{
          props.row?.tasaCambio != null
            ? Number(props.row.tasaCambio).toLocaleString('es-ES', {
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

import { dataColumnMoneda } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
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
const itemsTipoCodigo = ref([])
const filtradoCategoria = ref([])

const objetoInicial = {
  // id: null,
  codigo: null,
  descripcion: null,
  tasaCambio:0.00,
    esActiva:true
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'Moneda/Actualizar' : 'Moneda/Crear'
  console.log(objeto)

  objeto.tasaCambio= objeto.tasaCambio
      ? parseFloat(
          objeto.tasaCambio
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0
  saveData(url, objeto, load, close, dialogLoad)
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  filtradoCategoria.value = itemsTipoCodigo
  await obtener('Moneda/ObtenerPorId', id, objeto, dialogLoad, dialog)
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Moneda/Eliminar',
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
  items.value = await loadGet('Moneda/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Moneda/ObtenerListadoPaginado')??[]
  dialogLoad.value = false
})

const formatearTasaCambio = () => {
  if (objeto.tasaCambio !== null && objeto.tasaCambio !== undefined) {
    const valor = Number(objeto.tasaCambio);
    if (!isNaN(valor)) {
      objeto.tasaCambio = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormato = () => {
  if (typeof objeto.tasaCambio === 'string') {
    objeto.tasaCambio = objeto.tasaCambio.replace(/\./g, '').replace(',', '.');
    objeto.tasaCambio = parseFloat(objeto.tasaCambio);
  }
};

</script>
