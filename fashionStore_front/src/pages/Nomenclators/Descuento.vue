<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Descuentos" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnDescuento"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Descuentos</span>
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
                    objeto.id ? "Editar Descuento" : "Adicionar Descuento"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
            <q-form @submit.prevent="Guardar()" @reset="close" ref="myForm">
              <div class="h row q-ma-md">
                <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  :disable="!!objeto.id"
                  label="Nombre *"
                  v-model="objeto.nombre"
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
                        class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
                        readonly
                        v-model="objeto.fechaInicio"
                        mask="date"
                        :rules="[]"
                        label="Fecha de Inicio *"
                    >
                        <template v-slot:append>
                            <q-icon name="event" class="cursor-pointer">
                                <q-popup-proxy
                                    ref="modalFechaInicioPopup"
                                    cover
                                    transition-show="scale"
                                    transition-hide="scale"
                                >
                                    <q-date
                                        :disable="!!objeto.id"
                                        v-model="objeto.fechaInicio"

                                        @update:model-value="
                                            hidePopup(modalFechaInicioPopup)
                                        "
                                    >
                                        <div
                                            class="row items-center justify-end"
                                        >
                                            <q-btn
                                                v-close-popup
                                                label="Cerrar"
                                                color="primary"
                                                flat
                                            />
                                        </div>
                                    </q-date>
                                </q-popup-proxy>
                            </q-icon>
                        </template>
                    </q-input>
                    <q-input
                        class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
                        readonly
                        v-model="objeto.fechaFin"
                        mask="date"
                        :rules="[]"
                        label="Fecha de Fin *"
                    >
                        <template v-slot:append>
                            <q-icon name="event" class="cursor-pointer">
                                <q-popup-proxy
                                    ref="modalFechaFinPopup"
                                    cover
                                    transition-show="scale"
                                    transition-hide="scale"
                                >
                                    <q-date
                                        :disable="!!objeto.id"
                                        v-model="objeto.fechaFin"
                                        :options="
                                            (date) => objeto.fechaInicio <= date
                                        "
                                        @update:model-value="
                                            hidePopup(modalFechaFinPopup)
                                        "
                                    >
                                        <div
                                            class="row items-center justify-end"
                                        >
                                            <q-btn
                                                v-close-popup
                                                label="Cerrar"
                                                color="primary"
                                                flat
                                            />
                                        </div>
                                    </q-date>
                                </q-popup-proxy>
                            </q-icon>
                        </template>
                    </q-input>

                    <q-select
                        class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
                        multiple
                        v-model="objeto.productosIds"
                        label="Producto *"
                        emit-value
                        map-options
                        :use-input="
                            objeto.productosIds === null ||
                            objeto.productosIds === '' ||  objeto.productosIds.length === 0
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
                                                objeto.productosIds
                                                    .length > 1
                                            "
                                            class="grey--text caption"
                                            style="margin-left: 8px"
                                        >
                                            (+{{
                                                objeto.productosIds
                                                    .length - 1
                                            }}
                                            más)</span
                                        >
                                    </template>
                    </q-select>

                    <q-option-group
  v-model="tipoDescuento"
  :options="[
    { label: 'Usar Monto Fijo', value: 'monto' },
    { label: 'Usar Porcentaje', value: 'porcentaje' }
  ]"
  type="radio"
  color="primary"
  inline
  class="col-12 q-mt-md"
/>
<div  class="col-xs-12 col-sm-12 col-md-6 row">
<q-input
  class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
  label="Monto Fijo *"
  v-model.number="objeto.montoFijo"
  type="text"
  color="primary"
  :disable="tipoDescuento !== 'monto'"
  :rules="[
    val => tipoDescuento !== 'monto' || (!!val && parseFloat(val) > 0) || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearMontoFijo"
  @focus="quitarFormatoMontoFijo"
/>

<q-input
  class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
  label="Porcentaje *"
  v-model.number="objeto.porcentaje"
  type="text"
  color="primary"
  :disable="tipoDescuento !== 'porcentaje'"
  :rules="[
    val => tipoDescuento !== 'porcentaje' || (!!val && parseFloat(val) > 0) || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearPorcentaje"
  @focus="quitarFormatoPorcentaje"
/>

</div>


 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-12 col-sm-12 col-md-4 q-mt-sm justify-end q-mr-md"
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
  <template v-slot:body-cell-nombre="props">
                <q-td :props="props">
                    <div>
                        {{
                            PonerPuntosSupensivosACampo(
                                props.row?.nombre,
                                30
                            )
                        }}
                        <q-tooltip>{{ props.row?.nombre }}</q-tooltip>
                    </div>
                </q-td>
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

            <template v-slot:body-cell-porcentaje="props">
  <q-td :props="props">
    <div>
      {{
        props.row?.porcentaje != null
          ? Number(props.row.porcentaje).toLocaleString('es-ES', {
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            })
          : ''
      }}
      <q-tooltip>
        {{
          props.row?.porcentaje != null
            ? Number(props.row.porcentaje).toLocaleString('es-ES', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
              })
            : ''
        }}
      </q-tooltip>
    </div>
  </q-td>
</template>

            <template v-slot:body-cell-montoFijo="props">
  <q-td :props="props">
    <div>
      {{
        props.row?.montoFijo != null
          ? Number(props.row.montoFijo).toLocaleString('es-ES', {
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            })
          : ''
      }}
      <q-tooltip>
        {{
          props.row?.montoFijo != null
            ? Number(props.row.montoFijo).toLocaleString('es-ES', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
              })
            : ''
        }}
      </q-tooltip>
    </div>
  </q-td>
</template>

      <template v-slot:body-cell-fechaInicio="props">
                            <q-td :props="props">
                                <div>
                                    {{
                                        dayjs(
                                            props.row?.fechaInicio
                                        ).format("DD/MM/YYYY")
                                    }}
                                </div>
                            </q-td>
                        </template>
   <template v-slot:body-cell-fechaFin="props">
                            <q-td :props="props">
                                <div>
                                    {{
                                        dayjs(
                                            props.row?.fechaFin
                                        ).format("DD/MM/YYYY")
                                    }}
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
import { ref, reactive, onMounted, watch } from 'vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import DialogEliminar from 'components/DialogBoxes/DialogEliminar.vue'

import { dataColumnDescuento } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
import { hidePopup, PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'
import dayjs from 'dayjs'
import { Error } from 'src/boot/notify'

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)
const modalFechaInicioPopup = ref(null);
const modalFechaFinPopup = ref(null);


// Variables vacias
const filter = ref('')

// Arreglos
const items = ref([])
const itemsProducto = ref([])
const filtradoProducto = ref([])

const objetoInicial = {
  // id: null,
  nombre: null,
  fechaInicio: null,
  fechaFin: null,
  montoFijo:0.00,
    porcentaje:0.00,
       productosIds:[],
    esActivo:true
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'Descuento/Actualizar' : 'Descuento/Crear'
let esValidaLasFechas=true
  objeto.montoFijo= objeto.montoFijo
      ? parseFloat(
          objeto.montoFijo
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0

  objeto.porcentaje= objeto.porcentaje
      ? parseFloat(
          objeto.porcentaje
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0

if(objeto.fechaInicio!==null){
objeto.fechaInicio=dayjs(objeto.fechaInicio).toISOString()
}
else{
    Error("Debe seleccionar la fecha de inicio")
    esValidaLasFechas=false
}

if(objeto.fechaFin!==null){
objeto.fechaFin=dayjs(objeto.fechaFin).toISOString()
}
else{
    Error("Debe seleccionar la fecha de fin")
    esValidaLasFechas=false
}

if(esValidaLasFechas){
  saveData(url, objeto, load, close, dialogLoad)
  }
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  await obtener('Descuento/ObtenerPorId', id, objeto, dialogLoad, dialog)
  filtradoProducto.value = itemsProducto.value
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Descuento/Eliminar',
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
  items.value = await loadGet('Descuento/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Descuento/ObtenerListadoPaginado')??[]
  itemsProducto.value = await loadGet('Producto/ObtenerListadoPaginado')??[]

  filtradoProducto.value=itemsProducto.value
  dialogLoad.value = false
})

const formatearMontoFijo = () => {
  if (objeto.montoFijo !== null && objeto.montoFijo !== undefined) {
    const valor = Number(objeto.montoFijo);
    if (!isNaN(valor)) {
      objeto.montoFijo = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormatoMontoFijo = () => {
  if (typeof objeto.montoFijo === 'string') {
    objeto.montoFijo = objeto.montoFijo.replace(/\./g, '').replace(',', '.');
    objeto.montoFijo = parseFloat(objeto.montoFijo);
  }
};

const formatearPorcentaje = () => {
  if (objeto.porcentaje !== null && objeto.porcentaje !== undefined) {
    const valor = Number(objeto.porcentaje);
    if (!isNaN(valor)) {
      objeto.porcentaje = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormatoPorcentaje = () => {
  if (typeof objeto.porcentaje === 'string') {
    objeto.porcentaje = objeto.porcentaje.replace(/\./g, '').replace(',', '.');
    objeto.porcentaje = parseFloat(objeto.porcentaje);
  }
};

const tipoDescuento = ref('monto') // valor inicial

watch(tipoDescuento, (nuevo) => {
  if (nuevo === 'monto') {
    objeto.porcentaje = 0
  } else if (nuevo === 'porcentaje') {
    objeto.montoFijo = 0
  }
})


</script>
