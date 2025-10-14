<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Usuarios" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnUsuario"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Usuarios</span>
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
          <q-card style="width: 900px; max-width: 80vw; height: auto">
            <header class="q-pa-sm bg-primary">
              <q-toolbar>
                <q-toolbar-title class="text-subtitle6 text-white">
                  {{
                    objeto.id ? "Editar Usuario" : "Adicionar Usuario"
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
                      (val && val.length > 0) || 'Debe insertar un nombre',

                  ]"
                />
                <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  :disable="!!objeto.id"
                  label="Apellidos *"
                  v-model="objeto.apellidos"
                  color="primary"
                  counter
                  maxlength="50"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un apellido',

                  ]"
                />

                <q-input
                :disable="!!objeto.id"
                  class="col-xs-12 q-pa-sm"
                  label="Username *"
                  v-model="objeto.username"
                  color="primary"
                  counter
                  autogrow
                  maxlength="50"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Username',
                      (val) =>
                      (items.length > 0
                        ? !isValorRepetido(val, 'username', objeto, items)
                        : true) || 'Ya existe un username con ese valor',

                  ]"
                />
              <q-input
  v-if="!objeto.id"
  class="col-xs-12 q-pa-sm"
  label="Contraseña *"
  v-model="objeto.contrasenna"
  :type="isPwd ? 'password' : 'text'"
  color="primary"
  counter
  maxlength="50"
  lazy-rules
  :rules="[
    val => (val && val.length > 0) || 'Debe insertar una Contraseña'
  ]"
>
  <template v-slot:append>
    <q-icon
      :name="isPwd ? 'visibility_off' : 'visibility'"
      class="cursor-pointer"
      @click="isPwd = !isPwd"
    />
  </template>
</q-input>

<q-input
  v-if="!objeto.id"
  class="col-xs-12 q-pa-sm"
  label="Confirmar Contraseña *"
  v-model="objeto.contrasennaConfirmada"
  :type="isPwdConfirm ? 'password' : 'text'"
  color="primary"
  counter
  maxlength="50"
  lazy-rules
  :rules="[
    val => (val && val.length > 0) || 'Debe insertar una Contraseña'
  ]"
>
  <template v-slot:append>
    <q-icon
      :name="isPwdConfirm ? 'visibility_off' : 'visibility'"
      class="cursor-pointer"
      @click="isPwdConfirm = !isPwdConfirm"
    />
  </template>
</q-input>


                             <q-input
  :disable="!!objeto.id"
  class="col-xs-12 col-sm-12 col-md-9 q-pa-sm"
  label="Correo *"
  v-model="objeto.correo"
  color="primary"
  counter
  autogrow
  maxlength="50"
  lazy-rules
  :rules="[
    val => (val && val.length > 0) || 'Debe insertar un Correo',
    val => /^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$/.test(val) || 'Formato de correo no válido',
    val => (items.length > 0
      ? !isValorRepetido(val, 'correo', objeto, items)
      : true) || 'Ya existe un correo con ese valor'
  ]"
/>

                <q-select
                        class="col-xs-12 col-sm-12 col-md-3 q-pa-sm"
                        v-model="objeto.rolId"
                        label="Rol *"
                        emit-value
                        map-options
                        :use-input="
                            objeto.rolId === null ||
                            objeto.rolId === ''
                        "
                        option-label="nombre"
                        option-value="id"
                        :options="filtradoRol"
                        @filter="
                            (val, update) => {
                                filtradoRol = filterOptions(
                                    val,
                                    update,
                                    filtradoRol,
                                    'nombre',
                                    itemsRol
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
 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-6 col-sm-6 col-md-5 q-mt-sm"
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
            :name="props.value == 0 ? 'highlight_off' : 'check_circle'"
            :class="props.value == 0 ? 'text-grey' : 'text-primary'"
            size="20px"
          />
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
          <!--  <q-btn
              flat
              dense
              size="sm"
              @click="abrirDialogoEliminar(props.row.id)"
              text-color="negative"
              icon="delete"
            >
              <q-tooltip>Eliminar</q-tooltip>
            </q-btn>-->
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

import { dataColumnUsuario } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData, saveDataParaObjetosConFotos } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'
import { apiFotosBaseUrl } from 'src/boot/axios'

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)
const isPwd = ref(true)
const isPwdConfirm = ref(true)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)

// Variables vacias
const filter = ref('')

// Arreglos
const items = ref([])
const itemsRol = ref([])
const filtradoRol = ref([])

const objetoInicial = {
  // id: null,
  nombreCompleto: null,
  nombre: null,
  apellidos: null,
  username: null,
  correo: null,
  correo: null,
  rolId:null,
  esActivo:true,
  contrasenna:null,
  contrasennaConfirmada:null,
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  //const url = objeto.id ? 'Usuario/Actualizar' : 'Usuario/Crear'
  const url = objeto.id ? 'Usuario/Actualizar' : 'Usuario/Crear'
  console.log("objeto: ",objeto)
   console.log("url: ",url)
  saveData(url, objeto, load, close, dialogLoad)
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  await obtener('Usuario/ObtenerPorId', id, objeto, dialogLoad, dialog)

 filtradoRol.value = itemsRol.value

}



// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Usuario/Eliminar',
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
  items.value = await loadGet('Usuario/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Usuario/ObtenerListadoPaginado')??[]
  itemsRol.value = await loadGet('Rol/ObtenerListadoPaginado')??[]

 filtradoRol.value = itemsRol.value
  dialogLoad.value = false
})

function getFotoUrl(foto) {
  if (!foto) return ''
  if (/^https?:\/\//.test(foto)) return foto
  const url = apiFotosBaseUrl + (foto.startsWith('/') ? foto : '/' + foto)
  return url
}

</script>
