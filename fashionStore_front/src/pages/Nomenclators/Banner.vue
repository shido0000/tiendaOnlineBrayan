<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Banners" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnBanner"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Banners</span>
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
       <!-- <q-btn
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
        <q-dialog v-model="dialog" persistent>
          <q-card style="width: 700px; max-width: 80vw; height: auto">
            <header class="q-pa-sm bg-primary">
              <q-toolbar>
                <q-toolbar-title class="text-subtitle6 text-white">
                  {{
                    objeto.id ? "Editar Banner" : "Adicionar Banner"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
            <q-form @submit.prevent="Guardar()" @reset="close" ref="myForm">
              <div class="h row q-ma-md">
                <q-input
                  class="col-xs-12 col-md-12 q-pa-sm"
                  label="Título *"
                  v-model="objeto.textoTitulo"
                  color="primary"
                  counter
                  autogrow
                  maxlength="300"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Título',
                    (val) =>
                      (items.length > 0
                        ? !isValorRepetido(val, 'codigo', objeto, items)
                        : true) || 'Ya existe un Título con ese valor',
                  ]"
                />

                <q-input
                  class="col-xs-12 q-pa-sm"
                  label="Subtítulo *"
                  v-model="objeto.textoSubtitulo"
                  color="primary"
                  counter
                  autogrow
                  maxlength="500"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Subtítulo',
                  ]"
                />

                 <q-input
                  class="col-xs-12 col-sm-8 q-pa-sm"
                  label="Texto Botón *"
                  v-model="objeto.textoBoton"
                  color="primary"
                  counter
                  autogrow
                  maxlength="20"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un Texto al Botón',
                  ]"
                />

                <!-- Radios para elegir tipo -->
                <div class="col-12 q-mt-md">
                <q-option-group
                    v-model="tipoBanner"
                    :options="[
                    { label: 'Ofertas', value: 'ofertas' },
                    { label: 'Categorías', value: 'categorias' }
                    ]"
                    type="radio"
                    color="primary"
                    inline
                />
                </div>

                  <q-select
                  :disable="tipoBanner === 'ofertas'"
                        clearable
                        class="col-xs-12 col-sm-12 col-md-4 q-pa-sm"
                        v-model="objeto.categoriaProductoId"
                        label="Categoría *"
                        emit-value
                        map-options
                        :use-input="
                            objeto.categoriaProductoId === null ||
                            objeto.categoriaProductoId === ''
                        "
                        option-label="nombre"
                        option-value="id"
                        :options="filtradoCategorias"
                        @filter="
                            (val, update) => {
                                filtradoCategorias = filterOptions(
                                    val,
                                    update,
                                    filtradoCategorias,
                                    'nombre',
                                    itemsCategorias
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
                                            {{ scope.opt.nombre }}</span
                                        >

                                    </template>
                    </q-select>


<!-- Campo de imagen -->
 <q-file class="col-12 q-mt-lg"
                v-model="file"
                label="Imagen"
                accept="image/*"
                filled
                dark
                dense
                @update:model-value="previewFoto"
              >
                <template v-slot:prepend>
                  <q-icon name="photo_camera" color="cyan-5" size="20px" />
                </template>
              </q-file>
              <div v-if="preview" class="q-mt-md flex flex-center">
                <q-avatar size="100px" rounded class="shadow-2">
                  <q-img :src="preview" style="width: 100px; height: 100px;" />
                </q-avatar>
              </div>



 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-12 q-mt-sm"
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

            <template v-slot:body-cell-imagen="props">
            <q-td :props="props" class="text-center">
              <q-avatar size="40px" rounded v-if="props.row.imagen">
                <q-img :src="props.row.imagen" style="width: 40px; height: 40px;" />
              </q-avatar>
              <q-avatar v-else size="40px" rounded color="grey-8" text-color="white" icon="image_not_supported" font-size="20px" />
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

import { dataColumnBanner } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, obtener, saveData } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)
    const tipoBanner = ref('categorias') // valor inicial


// Variables vacias
const filter = ref('')
    const preview = ref(null)
    const file = ref(null)

// Arreglos
const items = ref([])

const itemsCategorias = ref([])
const filtradoCategorias = ref([])

const objetoInicial = {
  // id: null,
  textoTitulo: null,
  textoSubtitulo: null,
  textoBoton: null,
  imagen: null,
  categoriaProductoId: null,
  esActivo:true,
  rebajas:false
}

// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData
const Guardar = () => {
  const url = objeto.id ? 'BannerPromocion/Actualizar' : 'BannerPromocion/Crear'
  saveData(url, objeto, load, close, dialogLoad)
}

// Funcion para Obtener los datos para editar
const obtenerElementoPorId = async (id) => {
  await obtener('BannerPromocion/ObtenerPorId', id, objeto, dialogLoad, dialog)

  // Si el objeto trae una imagen (base64 o URL), inicializar la vista previa
  if (objeto.imagen) {
    preview.value = objeto.imagen
  } else {
    preview.value = null
  }

  // Ajustar tipoBanner según los datos del objeto
  if (objeto.rebajas) {
    tipoBanner.value = 'ofertas'
  } else {
    tipoBanner.value = 'categorias'
  }

}

// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'BannerPromocion/Eliminar',
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
  items.value = await loadGet('BannerPromocion/ObtenerListadoPaginado?activo=false')??[]
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
  items.value = await loadGet('BannerPromocion/ObtenerListadoPaginado?activo=false')??[]
  filtradoCategorias.value = await loadGet('CategoriaProducto/ObtenerListadoPaginado')??[]
  itemsCategorias.value =filtradoCategorias.value
  dialogLoad.value = false
})





const previewFoto = (selectedFile) => {
      const f = Array.isArray(selectedFile) ? selectedFile[0] : selectedFile
      if (f) {
        const reader = new FileReader()
        reader.onload = e => {
          preview.value = e.target.result
          objeto.imagen = e.target.result
        }
        reader.readAsDataURL(f)
      }
    }

 watch(() => objeto, (newVal) => {
      if (newVal) {
        if (newVal.imagen) {
          preview.value = newVal.imagen
        } else {
          preview.value = null
        }
      }
    }, { immediate: true })


watch(tipoBanner, (nuevoValor) => {
  if (nuevoValor === 'ofertas') {
    objeto.rebajas = true
    objeto.categoriaProductoId = null
  } else {
    objeto.rebajas = false
    // aquí no se fuerza categoriaProductoId, el usuario lo selecciona
  }
})


</script>
