<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
      <q-breadcrumbs-el label="Productos" />
    </q-breadcrumbs>
    <q-table
      class="q-pa-md"
      :filter="filter"
      :rows="items"
      :columns="dataColumnProducto"
      row-key="id"
      no-data-label="No hay elementos disponibles"
      no-results-label="No hay elementos disponibles"
      loading-label="Cargando..."
      rows-per-page-label="Filas por página"
    >
      <template v-slot:top>
        <div class="col-4 q-table__title">
          <span>Productos</span>
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
          @click="abrirDialogoCrear()"
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
       <q-card style="width: 1200px; max-width: 80vw; height: auto">
        <header class="q-px-sm bg-primary">
              <q-toolbar>
                <q-toolbar-title class="text-subtitle6 text-white">
                  {{
                    producto.id ? "Editar Producto" : "Adicionar Producto"
                  }}</q-toolbar-title
                >
              </q-toolbar>
            </header>
    <q-form @submit.prevent="guardarProducto" @reset="close" ref="myForm">
      <!-- Datos generales -->

                 <div class="h row q-ma-md">
                    <div class="row col-12">
      <q-input  class="col-xs-12 col-md-4 q-py-md q-px-sm" v-model="producto.codigo" label="Código *"  dense
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
       <q-input v-model="producto.sku" label="SKU *"  dense class="col-xs-12 col-md-4 q-py-md q-px-sm" />
      <q-select
                        class="col-xs-12 col-sm-12 col-md-4  "
                        v-model="producto.categoriasIds"
                        label="Categorías *"
                        emit-value
                        map-options
                        multiple
                        :use-input="
                            producto.categoriasIds === null ||
                            producto.categoriasIds === '' ||  producto.categoriasIds.length === 0
                        "
                        option-label="nombre"
                        option-value="id"
                        :options="filtradoCategoria"
                        @filter="
                            (val, update) => {
                                filtradoCategoria = filterOptions(
                                    val,
                                    update,
                                    filtradoCategoria,
                                    'nombre',
                                    itemsCategoria
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
                                        <span
                                            v-if="
                                                scope.index === 1 &&
                                                producto.categoriasIds
                                                    .length > 1
                                            "
                                            class="grey--text caption"
                                            style="margin-left: 8px"
                                        >
                                            (+{{
                                                producto.categoriasIds
                                                    .length - 1
                                            }}
                                            más)</span
                                        >
                                    </template>
                    </q-select>

                        <q-input v-model.number="producto.precioCosto" label="Precio Costo" type="number" outlined dense class="col-xs-12 col-md-3 q-py-md q-px-sm"  />
        <q-select
                       class="col-xs-12 col-md-3 q-py-md q-px-sm"
                        v-model="producto.monedaCostoId"
                              outlined
            dense
                        label="Moneda Costo*"
                        emit-value
                        map-options
                        :use-input="
                            producto.monedaCostoId === null ||
                            producto.monedaCostoId === ''
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
          <q-input v-model.number="producto.precioVenta" label="Precio Venta" type="number" outlined dense class="col-xs-12 col-md-3 q-py-md q-px-sm"  />


     <q-select
                    class="col-xs-12 col-md-3 q-py-md q-px-sm"
                        v-model="producto.monedaVentaId"
                        label="Moneda Venta*"
                        emit-value
                              outlined
            dense
                        map-options
                        :use-input="
                            producto.monedaVentaId === null ||
                            producto.monedaVentaId === ''
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

      <q-input  class="col-xs-12 col-md-12 q-py-md q-px-sm" v-model="producto.descripcion" label="Descripción *"  dense
         color="primary"
                  counter
                  maxlength="100"
                  lazy-rules
                  :rules="[
                    (val) =>
                      (val && val.length > 0) || 'Debe insertar un descripción',
                    (val) =>
                      (items.length > 0
                        ? !isValorRepetido(val, 'descripcion', objeto, items)
                        : true) || 'Ya existe un Descripción con ese valor',
                  ]"
      />

</div>
      <q-checkbox v-model="producto.esActivo" label="Activo" color="primary" />

      <q-separator class="q-my-md" />

      <!-- Variantes -->
       <div class="row col-12">
      <div class="text-h6 q-mb-md col-xs-12 col-sm-12 col-md-12">Variantes
        <!-- Botón para añadir variantes -->
      <q-btn
        color="primary"
        outline
        icon="add"
        label="Añadir Variante"
        class=" q-ml-md col-xs-12 col-sm-12 col-md-3"
        @click="agregarVariante"
      /></div></div>
      <div v-for="(variant, index) in producto.variants" :key="index" class="col-xs-12 q-pa-sm q-mb-md bordered">
        <div class="row col-xs-12 col-sm-12 col-md-12">


             <q-select
           class="col-xs-12 col-sm-12 col-md-3 q-px-sm"
                        v-model="variant.talla"
                              outlined
            dense
                        label="Talla"
                        emit-value
                        map-options
                        :use-input="
                            variant.talla === null ||
                            variant.talla === ''
                        "
                        option-label="descripcion"
                        option-value="descripcion"
                        :options="filtradoTallas"
                        @filter="
                            (val, update) => {
                                filtradoTallas = filterOptions(
                                    val,
                                    update,
                                    filtradoTallas,
                                    'descripcion',
                                    itemsTallas
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
class="col-xs-12 col-sm-12 col-md-3 q-px-sm"
  label="Color"
  stack-label
  outlined dense
>
  <template v-slot:control>
    <div class="row items-center no-wrap">

      <div
        :style="{
          width: '20px',
          height: '20px',
          borderRadius: '4px',
          border: '1px solid #ccc',
          backgroundColor: variant.color || '#ffffff'
        }"
      ></div>

      <q-icon
        name="colorize"
        class="q-ml-sm cursor-pointer"
      >
        <q-popup-proxy cover transition-show="scale" transition-hide="scale">
          <q-color
            v-model="variant.color"
            format-model="hex"
            default-view="palette"
          />
        </q-popup-proxy>
      </q-icon>
    </div>
  </template>
</q-field>
  <q-select
                        class="col-xs-12 col-sm-12 col-md-3 q-px-sm"
                        v-model="variant.talla"
                              outlined
            dense
                        label="Otras variantes"
                        emit-value
                        map-options
                        :use-input="
                            variant.talla === null ||
                            variant.talla === ''
                        "
                        option-label="descripcion"
                        option-value="descripcion"
                        :options="filtradoTallas"
                        @filter="
                            (val, update) => {
                                filtradoTallas = filterOptions(
                                    val,
                                    update,
                                    filtradoTallas,
                                    'descripcion',
                                    itemsTallas
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
          <q-input v-model.number="variant.stock" label="Stock" type="number" outlined dense    class="col-xs-12 col-sm-12 col-md-3 q-px-sm" :min="1"/>

          <!--<q-input v-model="variant.color" label="Color" outlined dense class="col-6" />-->


        </div>


        <!-- Fotos -->

          <div class="row col-12 ">
            <div class="col-12 col-md-3 q-mr-xs q-pr-md q-mt-sm">
              <q-uploader
                class="w-full"
                 style="height: 150px;"
                label="Seleccionar fotos"
                accept="image/*"
                multiple
                :auto-upload="false"
                @added="files => variant.fotos = files"
              />
            </div>
           <div class="col-12 col-md-8 q-mt-sm q-ml-auto">
  <div v-if="variant.fotos && variant.fotos.length">
    <q-carousel
      v-model="variant.slide"
      animated
      navigation
      infinite
      :autoplay="autoplay"
      arrows
      height="150px"
      class="bg-grey-2 full-width"
      transition-prev="slide-right"
      transition-next="slide-left"
      @mouseenter="autoplay = false"
      @mouseleave="autoplay = true"
    >
      <q-carousel-slide
        v-for="(foto, idx) in getValidFotosVariant(variant)"
        :key="idx"
        :name="idx + 1"
        class="relative-position"
      >
        <q-img
          :src="foto.img"
          :alt="foto.alt"
          style="width: 100%; height: 100%; object-fit: contain; border-radius: 8px;"
        />
        <q-btn
          dense
          round
          color="negative"
          icon="close"
          size="sm"
          class="absolute-top-right q-mt-sm q-mr-sm z-max"
          @click.stop="confirmarEliminarFotoVariant(idx, variant)"
        />
      </q-carousel-slide>
    </q-carousel>
    <q-dialog v-model="dialogEliminarFoto">
                  <q-card>
                    <q-card-section class="row items-center">
                      <q-icon name="warning" color="warning" size="md" class="q-mr-md" />
                      <span>¿Está seguro que desea eliminar esta foto del producto?</span>
                    </q-card-section>
                    <q-card-actions align="right">
                      <q-btn flat label="Cancelar" color="primary" @click="cancelarEliminarFoto" />
                      <q-btn flat label="Eliminar" color="negative" @click="eliminarFotoCarruselConfirmada" />
                    </q-card-actions>
                  </q-card>
                </q-dialog>
  </div>
  <div v-else class="q-pa-sm text-grey">No hay imágenes para mostrar.</div>
</div>

          </div>
          <!-- Botón para eliminar variante -->
      <div class="row q-mt-sm">
        <q-btn
        color="negative"
        icon="delete"
        label="Eliminar Variante"
        outline
        dense
        @click="eliminarVariante(index)"
        class="q-my-md"
        />
      </div>
          </div>


      <q-separator class="q-my-md" />

      <!-- Acciones -->
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

import { dataColumnProducto } from 'src/assets/js/column_data/columnDataNomencladores'
import { closeDialog, eliminarElemento, filterOptions, isValorRepetido, loadGet, loadGetHastaData, obtener, obtenerHastaData, saveData, saveDataParaObjetosConFotos } from 'src/assets/js/util/funciones'
import { PonerPuntosSupensivosACampo } from 'src/assets/js/util/extras'
import { api, apiFotosBaseUrl } from 'src/boot/axios'

// Variables Booleanas
const dialog = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)
// Estado para el índice del carrusel de fotos (slide) y autoplay
const slide = ref(1)
const autoplay = ref(true)
// Computed para filtrar y mapear las fotos válidas
import { computed } from 'vue'
import { Error, Success } from 'src/boot/notify'
const validFotos = computed(() =>
  (objeto.fotos || [])
    .filter(foto => !!getPreviewUrl(foto))
    .map((foto, idx) => ({
      name: idx,
      img: getPreviewUrl(foto),
      alt: `Foto ${idx + 1}`
    }))
)
// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)

// Variables vacias
const filter = ref('')

// Arreglos
const items = ref([])
const itemsMoneda = ref([])
const filtradoMoneda = ref([])

const itemsCategoria = ref([])
const filtradoCategoria = ref([])

const itemsTallas = ref([])
const filtradoTallas = ref([])

const objetoInicial = {
  // id: null,
  codigo: null,
  descripcion: null,
    esActivo:true,
    sku: null,
  precioCosto:0.00,
    precioVenta:0.00,
     color:"#ffffff",
       monedaCostoId:null,
              monedaVentaId:null,
    fotosArchivos: [], // aquí guardamos los archivos seleccionados
     fotos: [], // aquí guardamos los archivos seleccionados
      fotosExistentesIds: [], // aquí guardamos los archivos seleccionados
  categoriasIds: [], // aquí guardamos los archivos seleccionados
}
// Guardar copia de las fotos originales al cargar el producto para comparar después
let fotosOriginales = []
// Crear una copia del objeto inicial
const objeto = reactive({ ...objetoInicial })
// Funciones
// 1- Funcion para pasar parametros en el Adicionar SaveData

const Guardar = () => {
  const url = objeto.id ? 'Producto/ActualizarConFotos' : 'Producto/CrearConFotos'

  objeto.precioCosto = objeto.precioCosto
    ? parseFloat(
        objeto.precioCosto
          .toString()
          .replace(/\./g, '')
          .replace(',', '.')
      )
    : 0

  objeto.precioVenta = objeto.precioVenta
    ? parseFloat(
        objeto.precioVenta
          .toString()
          .replace(/\./g, '')
          .replace(',', '.')
      )
    : 0


  const formData = new FormData()
  formData.append('Codigo', objeto.codigo)
  formData.append('Sku', objeto.sku)
  formData.append('Descripcion', objeto.descripcion)
  formData.append('PrecioCosto', objeto.precioCosto)
  formData.append('PrecioVenta', objeto.precioVenta)
  formData.append('MonedaId', variant.monedaCostoId)
  formData.append('Color', objeto.color || "#ffffff")
  formData.append('EsActivo', objeto.esActivo)
  // Agregar categorías seleccionadas
  if (Array.isArray(producto.value.categoriasIds)) {
    producto.value.categoriasIds.forEach(id => {
      formData.append('categoriasIds', id)
    })
  }

  if (objeto.id) {
    // --- ACTUALIZAR ---
    // Enviar rutas de fotos existentes en 'FotosExistentes' y archivos nuevos en 'Fotos'
    if (Array.isArray(objeto.fotos)) {
      objeto.fotos.forEach(ruta => {
        formData.append('FotosExistentes', ruta)
      })
    }
    if (objeto.fotosArchivos && objeto.fotosArchivos.length > 0) {
      objeto.fotosArchivos.forEach(file => {
        formData.append('Fotos', file)
      })
    }
    saveDataParaObjetosConFotos(url, formData, load, close, dialogLoad, true, objeto.id)
  } else {
    // CREAR
    if (objeto.fotosArchivos && objeto.fotosArchivos.length > 0) {
      objeto.fotosArchivos.forEach(file => {
        formData.append('Fotos', file)
      })
      saveDataParaObjetosConFotos(url, formData, load, close, dialogLoad, true, objeto.id)
    } else {
      saveDataParaObjetosConFotos(url, objeto, load, close, dialogLoad, false, objeto.id)
    }
  }
}

// Funcion para Obtener los datos para editar
/*const obtenerElementoPorId = async (id) => {
  //await obtener('Producto/ObtenerPorId', id, objeto, dialogLoad, dialog)
await obtenerHastaData('Producto/ObtenerProductoEspecifico', id, objeto, dialogLoad, dialog)

//Object.assign(producto.value,objeto)
//llenarCategoriasIds()

 producto.value = {
    id: objeto.id,
    codigo: objeto.codigo,
    descripcion: objeto.descripcion,
    esActivo: objeto.esActivo,
    categoriasIds: objeto.productoCategorias.map(pc => pc.categoriaId),
     precioCosto: objeto.precioCosto,
      precioVenta: objeto.precioVenta,
      monedaCostoId: objeto.monedaCostoId,
      monedaVentaId: objeto.monedaVentaId,
    variants: objeto.variants.map(v => ({
      id: v.id,
      sku: v.sku,
      talla: v.talla,
      color: v.color,
      stock: v.stock,
      fotos: (v.fotos || []).map(f => ({
        id: f.id,
        url: f.url,
        descripcion: f.descripcion
      }))
    }))
  };

console.log("objeto: ",producto.value)
  // Guardar copia de las fotos originales (rutas) y poblar objeto.fotos con todas las fotos de las variantes
  fotosOriginales = []
  try {
    fotosOriginales = (objeto.variants || []).flatMap(v => (v.fotos || []).map(f => f.url || f))
  } catch (e) {
    fotosOriginales = []
  }
  // Poblamos objeto.fotos (usado por el carrusel principal) con las rutas encontradas
  objeto.fotos = fotosOriginales.slice()
  filtradoMoneda.value = itemsMoneda.value
  filtradoCategoria.value=itemsCategoria.value

}
*/

const obtenerElementoPorId = async (id) => {
  // Traemos el objeto desde el backend
  await obtenerHastaData('Producto/ObtenerProductoEspecifico', id, objeto, dialogLoad, dialog)

  // Adaptamos el objeto recibido a la estructura que usamos en el front
  producto.value = {
    id: objeto.id,
    codigo: objeto.codigo,
    descripcion: objeto.descripcion,
    esActivo: objeto.esActivo,
    sku: objeto.sku,
    precioCosto: objeto.precioCosto,
    precioVenta: objeto.precioVenta,
    monedaCostoId: objeto.monedaCostoId,
    monedaVentaId: objeto.monedaVentaId,
    categoriasIds: objeto.categoriasIds || [],

    // Mapeamos las variantes
    variants: (objeto.productoVariantes || []).map(v => ({
      id: v.id,
      productoId: v.productoId,
      talla: v.talla,
      color: v.color,
      stock: v.stock,
      principal: v.principal,
      otrasVariantesIds: v.otrasVariantesIds || [],
      fotos: (v.fotos || []).map(f => ({
        id: f.id,
        url: f.url,
        descripcion: f.descripcion,
        esPrincipal: f.esPrincipal,
        orden: f.orden
      })),
       slide: 1 // índice inicial del carrusel de esta variante
    }))
  }

  console.log("Producto adaptado:", producto.value)

  // Guardar copia de las fotos originales (rutas) y poblar objeto.fotos con todas las fotos de las variantes
  let fotosOriginales = []
  try {
    fotosOriginales = (objeto.productoVariantes || [])
      .flatMap(v => (v.fotos || []).map(f => f.url || f))
  } catch (e) {
    fotosOriginales = []
  }

  // Poblamos objeto.fotos (usado por el carrusel principal) con las rutas encontradas
  objeto.fotos = fotosOriginales.slice()

  // Refrescamos filtros
  filtradoMoneda.value = itemsMoneda.value
  filtradoCategoria.value = itemsCategoria.value
}


// Funcion para eliminar elemento
const eliminar = async () => {
  await eliminarElemento(
    'Producto/Eliminar',
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
  items.value = await loadGet('Producto/ObtenerListadoPaginado')??[]
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
  items.value = await loadGet('Producto/ObtenerListadoPaginado')??[]
  itemsMoneda.value = await loadGet('Moneda/ObtenerListadoPaginado')??[]
  itemsCategoria.value = await loadGet('CategoriaProducto/ObtenerListadoPaginado')??[]
itemsTallas.value = await loadGetHastaData('Talla')??[]
  filtradoMoneda.value=itemsMoneda.value
  filtradoCategoria.value=itemsCategoria.value
  filtradoTallas.value=itemsTallas.value


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


// Estado para el diálogo de confirmación de eliminación de foto
const dialogEliminarFoto = ref(false)
const variantAFotoEliminar = ref(null)
const idxFotoAEliminar = ref(null)

function confirmarEliminarFoto(idx) {
  // Confirm deletion for main objeto.fotos
  variantAFotoEliminar.value = null
  idxFotoAEliminar.value = idx
  dialogEliminarFoto.value = true
}

function confirmarEliminarFotoVariant(idx, variant) {
  // Guardamos qué variante y qué foto se quiere eliminar
  variantAFotoEliminar.value = variant
  idxFotoAEliminar.value = idx
  dialogEliminarFoto.value = true
}

function cancelarEliminarFoto() {
  dialogEliminarFoto.value = false
  variantAFotoEliminar.value = null
  idxFotoAEliminar.value = null
}

function eliminarFotoCarruselConfirmada() {
  if (variantAFotoEliminar.value && idxFotoAEliminar.value !== null) {
    // Quitamos la foto del array de esa variante
    variantAFotoEliminar.value.fotos.splice(idxFotoAEliminar.value, 1)
  }
  cancelarEliminarFoto()
}


// Este método lo defines tú
function archivosAgregados (files) {
  // files es un array de File
  objeto.fotosArchivos = files
}
// Función para obtener la URL de una foto guardada
function getFotoUrl(foto) {
  if (!foto) return ''
  // foto puede ser string o un objeto con propiedad 'url'
  const path = typeof foto === 'string' ? foto : (foto && foto.url ? foto.url : '')
  if (!path) return ''
  if (/^https?:\/\//.test(path)) return path
  const url = apiFotosBaseUrl + (path.startsWith('/') ? path : '/' + path)
  return url
}

// Función para obtener la URL de previsualización de un archivo nuevo
function getPreviewUrl(file) {
  if (!file) return ''
  // Si es string (ruta)
  if (typeof file === 'string') return getFotoUrl(file)
  // Si es un objeto con propiedad url
  if (typeof file === 'object' && file.url) return getFotoUrl(file.url)
  // Si es un File (subido por el usuario)
  if (typeof File !== 'undefined' && file instanceof File) return URL.createObjectURL(file)
  return ''
}

// Devuelve fotos válidas para una variante (para uso en el template)
function getValidFotosVariant(variant) {
  if (!variant || !Array.isArray(variant.fotos)) return []
  return variant.fotos
    .filter(f => !!getPreviewUrl(f))
    .map((f, idx) => ({ img: getPreviewUrl(f), alt: `Foto ${idx + 1}` }))
}


const producto = ref({
  id: undefined,
  codigo: '',
  descripcion: '',
  esActivo: true,
  sku: '',
  precioCosto: 1,
   precioVenta: 1,
    monedaCostoId: null,
   monedaVentaId: null,
   stockTotal: 0,
  categoriasIds: [],
  variants: []
})

function agregarVariante() {
  producto.value.variants.push({
    talla: '',
    color: null,
    principal: true,
    stock: 1,
    fotos: []
  })
}

// Eliminar variante por índice
function eliminarVariante(index) {
  if (producto.value.variants.length > 1) {
    producto.value.variants.splice(index, 1)
  } else {
    // Si solo hay una variante, puedes mostrar un mensaje o simplemente limpiar el array
    producto.value.variants = []
  }
}

/*
async function guardarProducto() {
  dialogLoad.value = true
  try {
    const url = producto.value?.id ? 'Producto/ActualizarConFotos' : 'Producto/CrearConFotos'

    const formData = new FormData()

    // Campos simples
    formData.append("Codigo", producto.value?.codigo || '')
    formData.append("Descripcion", producto.value?.descripcion || '')
    formData.append("EsActivo", producto.value?.esActivo ?? true)
    formData.append("SKU", producto.value?.sku || '')
    formData.append("PrecioCosto", producto.value?.precioCosto || 0)
    formData.append("PrecioVenta", producto.value?.precioVenta || 0)
    formData.append("MonedaCostoId", producto.value?.monedaCostoId || '')
    formData.append("MonedaVentaId", producto.value?.monedaVentaId || '')
    formData.append("StockTotal", 0)

    // Categorías
    if (Array.isArray(producto.value?.categoriasIds)) {
      producto.value.categoriasIds.forEach(id => {
        formData.append("CategoriasIds", id)
      })
    }

    // Variantes
    if (Array.isArray(producto.value?.variants)) {
      producto.value.variants.forEach((variant, index) => {
        formData.append(`ProductoVariantes[${index}].ProductoId`, variant.id || '')
        formData.append(`ProductoVariantes[${index}].Talla`, variant.talla || '')
        formData.append(`ProductoVariantes[${index}].Color`, variant.color || '')
        formData.append(`ProductoVariantes[${index}].Stock`, variant.stock || 0)
        formData.append(`ProductoVariantes[${index}].Principal`, variant.principal !== false)

        if (Array.isArray(variant.otrasVariantesIds)) {
          variant.otrasVariantesIds.forEach(id => {
            formData.append(`ProductoVariantes[${index}].OtrasVariantesIds`, id)
          })
        }

        if (Array.isArray(variant.fotos)) {
          variant.fotos.forEach(file => {
            if (file instanceof File) {
              formData.append(`ProductoVariantes[${index}].Fotos`, file)
            }
          })
        }
      })
    }

    const config = {
      headers: { 'Content-Type': 'multipart/form-data' }
    }

    if (producto.value?.id) {
      await api.put(`/${url}/${producto.value.id}`, formData, config)
      Success("El elemento ha sido modificado correctamente")
    } else {
      await api.post(`/${url}`, formData, config)
      Success("El elemento ha sido creado correctamente")
    }

    await load()
    await close()
  } catch (error) {
    const mensajeError = error?.response?.data?.errorMessage || error?.message || 'Error al guardar'
    console.error('Error:', error?.response?.data)
    alert('Error: ' + mensajeError)
  } finally {
    dialogLoad.value = false
  }
}*/

async function guardarProducto() {
  dialogLoad.value = true
  try {
    const url = producto.value?.id ? 'Producto/ActualizarConFotos' : 'Producto/CrearConFotos'

    const formData = new FormData()

    // Campos simples
    formData.append("Codigo", producto.value?.codigo || '')
    formData.append("Descripcion", producto.value?.descripcion || '')
    formData.append("EsActivo", producto.value?.esActivo ?? true)
    formData.append("SKU", producto.value?.sku || '')
    formData.append("PrecioCosto", producto.value?.precioCosto || 0)
    formData.append("PrecioVenta", producto.value?.precioVenta || 0)
    formData.append("MonedaCostoId", producto.value?.monedaCostoId || '')
    formData.append("MonedaVentaId", producto.value?.monedaVentaId || '')
    formData.append("StockTotal", 0)

    // Categorías
    if (Array.isArray(producto.value?.categoriasIds)) {
      producto.value.categoriasIds.forEach(id => {
        formData.append("CategoriasIds", id)
      })
    }

    // Variantes
    if (Array.isArray(producto.value?.variants)) {
      producto.value.variants.forEach((variant, index) => {
        formData.append(`ProductoVariantes[${index}].ProductoId`, variant.id || '')
        formData.append(`ProductoVariantes[${index}].Talla`, variant.talla || '')
        formData.append(`ProductoVariantes[${index}].Color`, variant.color || '')
        formData.append(`ProductoVariantes[${index}].Stock`, variant.stock || 0)
        formData.append(`ProductoVariantes[${index}].Principal`, variant.principal !== false)

        if (Array.isArray(variant.otrasVariantesIds)) {
          variant.otrasVariantesIds.forEach(id => {
            formData.append(`ProductoVariantes[${index}].OtrasVariantesIds`, id)
          })
        }

        // Fotos: separar existentes y nuevas
        if (Array.isArray(variant.fotos)) {
          variant.fotos.forEach(f => {
            if (f instanceof File) {
              // Foto nueva
              formData.append(`ProductoVariantes[${index}].FotosNuevas`, f)
            } else if (f.id) {
              // Foto existente que se mantiene
              formData.append(`ProductoVariantes[${index}].FotosExistentesIds`, f.id)
            }
          })
        }
      })
    }

    const config = {
      headers: { 'Content-Type': 'multipart/form-data' }
    }

    if (producto.value?.id) {
      await api.put(`/${url}/${producto.value.id}`, formData, config)
      Success("El elemento ha sido modificado correctamente")
    } else {
      await api.post(`/${url}`, formData, config)
      Success("El elemento ha sido creado correctamente")
    }

    await load()
    await close()
  } catch (error) {
    const mensajeError = error?.response?.data?.errorMessage || error?.message || 'Error al guardar'
    console.error('Error:', error?.response?.data)
    alert('Error: ' + mensajeError)
  } finally {
    dialogLoad.value = false
  }
}


function cancelar() {
  producto.value = {
    codigo: '',
    descripcion: '',
    esActivo: true,
    categoriasIds: [],
    variants: []
  }
}

function abrirDialogoCrear(){
producto.value.id=undefined
 producto.value.codigo=''
producto.value.descripcion=''
producto.value.esActivo=true

 producto.value.sku=''
producto.value.precioCosto=1
 producto.value.precioVenta=1
producto.value.monedaCostoId=null
producto.value.monedaVentaId=null

producto.value.categoriasIds=[]
producto.value.variants=[]

    dialog.value = true
}
</script>
