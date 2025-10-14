<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Cupones" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnCupon"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Cupones</span>
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
                    objeto.id ? "Editar Cupon" : "Adicionar Cupon"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
            <q-form @submit.prevent="Guardar()" @reset="close" ref="myForm">
              <div class="h row q-ma-md">
                <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  :disable="!!objeto.id"
                  label="Codigo *"
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
                        class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
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
                        class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
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

                    <q-option-group
  v-model="tipoCupon"
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
  :disable="tipoCupon !== 'monto'"
  :rules="[
    val => tipoCupon !== 'monto' || (!!val && parseFloat(val) > 0) || 'Debe ser mayor que 0'
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
  :disable="tipoCupon !== 'porcentaje'"
  :rules="[
    val => tipoCupon !== 'porcentaje' || (!!val && parseFloat(val) > 0) || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearPorcentaje"
  @focus="quitarFormatoPorcentaje"
/>

</div>

<q-input
  class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
  label="Máximo Usos *"
  v-model.number="objeto.maximoUsos"
   :rules="[
    val => (!!val && parseInt(val) > 0) || 'Debe ser mayor que 0'
  ]"
  type="text"
  color="primary"
/>
<q-input :disable="true"
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Usos Actuales"
  v-model.number="objeto.usosActuales"
  type="text"
  color="primary"
/>

<q-input
  class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
  label="Monto Mínimo de Pedido *"
  v-model.number="objeto.montoMinimoPedido"
  type="text"
  color="primary"
  :rules="[
    val => (!!val && parseInt(val) > 0) || 'Debe ser mayor que 0'
  ]"
  :input-style="{ textAlign: 'left' }"
  @blur="formatearMontoMinimoPedido"
  @focus="quitarFormatoMontoMinimoPedido"
/>

 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-12 col-sm-12 col-md-3 q-mt-sm justify-end q-mr-md"
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

           <template v-slot:body-cell-montoMinimoPedido="props">
  <q-td :props="props">
    <div>
      {{
        props.row?.montoMinimoPedido != null
          ? Number(props.row.montoMinimoPedido).toLocaleString('es-ES', {
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            })
          : ''
      }}
      <q-tooltip>
        {{
          props.row?.montoMinimoPedido != null
            ? Number(props.row.montoMinimoPedido).toLocaleString('es-ES', {
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

import { dataColumnCupon } from 'src/assets/js/column_data/columnDataNomencladores'
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

const objetoInicial = {
  // id: null,
  codigo: null,
  descripcion: null,
  fechaInicio: null,
  fechaFin: null,
  montoFijo:0.00,
    porcentaje:0.00,
    esActivo:true,
      maximoUsos: 1,
  usosActuales: 0,
  montoMinimoPedido: 0.00,

}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'Cupon/Actualizar' : 'Cupon/Crear'
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

        objeto.montoMinimoPedido= objeto.montoMinimoPedido
      ? parseFloat(
          objeto.montoMinimoPedido
            .toString()
            .replace(/\./g, '')   // elimina separadores de miles
            .replace(',', '.')   // convierte coma decimal a punto
        )
      : 0

           objeto.maximoUsos= objeto.maximoUsos
      ? parseInt(
          objeto.maximoUsos
            .toString()
        )
      : 0

          objeto.usosActuales= objeto.usosActuales
      ? parseInt(
          objeto.usosActuales
            .toString()
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
  await obtener('Cupon/ObtenerPorId', id, objeto, dialogLoad, dialog)
}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Cupon/Eliminar',
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
  items.value = await loadGet('Cupon/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Cupon/ObtenerListadoPaginado')??[]
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

const formatearMontoMinimoPedido = () => {
  if (objeto.montoMinimoPedido !== null && objeto.montoMinimoPedido !== undefined) {
    const valor = Number(objeto.montoMinimoPedido);
    if (!isNaN(valor)) {
      objeto.montoMinimoPedido = valor.toLocaleString('es-ES', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });
    }
  }
};

const quitarFormatoMontoMinimoPedido = () => {
  if (typeof objeto.montoMinimoPedido === 'string') {
    objeto.montoMinimoPedido = objeto.montoMinimoPedido.replace(/\./g, '').replace(',', '.');
    objeto.montoMinimoPedido = parseFloat(objeto.montoMinimoPedido);
  }
};

const tipoCupon = ref('monto') // valor inicial

watch(tipoCupon, (nuevo) => {
  if (nuevo === 'monto') {
    objeto.porcentaje = 0
  } else if (nuevo === 'porcentaje') {
    objeto.montoFijo = 0
  }
})


</script>
