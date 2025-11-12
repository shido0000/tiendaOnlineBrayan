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
        <!--  <q-card style="width: 700px; max-width: 80vw; height: auto">
            <header class="q-pa-sm bg-primary">
              <q-toolbar>
                <q-toolbar-title class="text-subtitle6 text-white">
                  {{
                    objeto.id ? "Editar Producto" : "Adicionar Producto"
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
                        v-model="variant.monedaCostoId"
                        label="Moneda *"
                        emit-value
                        map-options
                        :use-input="
                            variant.monedaCostoId === null ||
                            variant.monedaCostoId === ''
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

                    <div class="col-xs-12 col-sm-12 col-md-8 row">
                    <q-select
                        class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
                        v-model="producto.categoriaIds"
                        label="Categorías *"
                        emit-value
                        map-options
                        multiple
                        :use-input="
                            producto.categoriaIds === null ||
                            producto.categoriaIds === '' ||  producto.categoriaIds.length === 0
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
                                                producto.categoriaIds
                                                    .length > 1
                                            "
                                            class="grey--text caption"
                                            style="margin-left: 8px"
                                        >
                                            (+{{
                                                producto.categoriaIds
                                                    .length - 1
                                            }}
                                            más)</span
                                        >
                                    </template>
                    </q-select>

<q-field
  class="col-xs-12 col-sm-12 col-md-6 q-pa-sm"
  label="Color"
  stack-label
>
  <template v-slot:control>
    <div class="row items-center no-wrap">

      <div
        :style="{
          width: '20px',
          height: '20px',
          borderRadius: '4px',
          border: '1px solid #ccc',
          backgroundColor: objeto.color || '#ffffff'
        }"
      ></div>

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
</div>


 <q-checkbox v-show="objeto.id"
                        aling="right"
                        class="col-xs-12 col-sm-12 col-md-4 q-mt-sm justify-end q-mr-md"
                        right-label
                        v-model="objeto.esActivo"
                        label="Activo"
                        color="primary"
                    />


<q-uploader
  class="col-12 q-pa-sm q-mt-md"
  label="Seleccionar fotos"
  accept="image/*"
  multiple
  :auto-upload="false"
  @added="archivosAgregados"
/>

<div v-if="validFotos.length" class="q-pa-sm col-12">
  <q-carousel
    v-model="slide"
    animated
    navigation
    infinite
    :autoplay="autoplay"
    arrows
  height="350px"
  class="bg-grey-2 full-width"
  style="width: 100%;"
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
          </q-card>-->

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
      <q-input  class="col-xs-12 col-md-4 q-py-md q-px-sm" v-model="producto.descripcion" label="Descripción *"  dense
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
      <q-select
                        class="col-xs-12 col-sm-12 col-md-4  "
                        v-model="producto.categoriaIds"
                        label="Categorías *"
                        emit-value
                        map-options
                        multiple
                        :use-input="
                            producto.categoriaIds === null ||
                            producto.categoriaIds === '' ||  producto.categoriaIds.length === 0
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
                                                producto.categoriaIds
                                                    .length > 1
                                            "
                                            class="grey--text caption"
                                            style="margin-left: 8px"
                                        >
                                            (+{{
                                                producto.categoriaIds
                                                    .length - 1
                                            }}
                                            más)</span
                                        >
                                    </template>
                    </q-select>
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
      <div v-for="(variant, index) in producto.variants" :key="index" class="q-pa-sm q-mb-md bordered">
        <div class="row q-col-gutter-md">
          <q-input v-model="variant.sku" label="SKU *" outlined dense class="col-6" />
             <q-select
                        class="col-xs-12 col-sm-12 col-md-2 "
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
  class="col-xs-12 col-sm-12 col-md-2 "
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
          backgroundColor: objeto.color || '#ffffff'
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
          <q-input v-model.number="variant.stock" label="Stock" type="number" outlined dense class="col-2" :min="1"/>

          <!--<q-input v-model="variant.color" label="Color" outlined dense class="col-6" />-->
          <q-input v-model.number="variant.precioCosto" label="Precio Costo" type="number" outlined dense class="col-3" />
        <q-select
                        class="col-xs-12 col-sm-12 col-md-3 "
                        v-model="variant.monedaCostoId"
                              outlined
            dense
                        label="Moneda Costo*"
                        emit-value
                        map-options
                        :use-input="
                            variant.monedaCostoId === null ||
                            variant.monedaCostoId === ''
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
          <q-input v-model.number="variant.precioVenta" label="Precio Venta" type="number" outlined dense class="col-3" />


     <q-select
                        class="col-xs-12 col-sm-12 col-md-3"
                        v-model="variant.monedaVentaId"
                        label="Moneda Venta*"
                        emit-value
                              outlined
            dense
                        map-options
                        :use-input="
                            variant.monedaVentaId === null ||
                            variant.monedaVentaId === ''
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

        <!-- Fotos -->
       <!-- <div class="q-mt-md">
          <q-uploader
            label="Fotos de la variante"
            accept="image/*"
            multiple
            :auto-upload="false"
            @added="files => variant.fotos = files"
          />
          <div v-if="variant.fotos && variant.fotos.length" class="row q-col-gutter-sm q-mt-sm">
            <q-img
              v-for="(file, idx) in variant.fotos"
              :key="idx"
              :src="getPreviewUrl(file)"
              style="width: 100px; height: 100px; border-radius: 8px;"
            />
          </div>-->
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
                  v-model="slide"
                  animated
                  navigation
                  infinite
                  :autoplay="autoplay"
                  arrows
                  height="150px"
                  class="bg-grey-2 full-width"
                  style="width: 100%;"
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
    sku: null,
  descripcion: null,
  precioCosto:0.00,
    precioVenta:0.00,
     color:"#ffffff",
       monedaId:null,
    esActivo:true,
    fotosArchivos: [], // aquí guardamos los archivos seleccionados
     fotos: [], // aquí guardamos los archivos seleccionados
      fotosExistentesIds: [], // aquí guardamos los archivos seleccionados
  categoriaIds: [], // aquí guardamos los archivos seleccionados
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
  if (Array.isArray(producto.value.categoriaIds)) {
    producto.value.categoriaIds.forEach(id => {
      formData.append('CategoriaIds', id)
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
const obtenerElementoPorId = async (id) => {
  //await obtener('Producto/ObtenerPorId', id, objeto, dialogLoad, dialog)
await obtenerHastaData('Producto/ObtenerProductoEspecifico', id, objeto, dialogLoad, dialog)

//Object.assign(producto.value,objeto)
//llenarCategoriasIds()

 producto.value = {
    id: objeto.id,
    codigo: objeto.codigo,
    descripcion: objeto.descripcion,
    esActivo: objeto.esActivo,
    categoriaIds: objeto.productoCategorias.map(pc => pc.categoriaId),
    variants: objeto.variants.map(v => ({
      id: v.id,
      sku: v.sku,
      talla: v.talla,
      color: v.color,
      precioCosto: v.precioCosto,
      precioVenta: v.precioVenta,
      monedaCostoId: v.monedaCostoId,
      monedaVentaId: v.monedaVentaId,
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

function llenarCategoriasIds() {
  // Limpio el array antes de llenarlo, para evitar duplicados
  producto.value.categoriaIds = [];

  for (let i = 0; i < objeto.productoCategorias.length; i++) {
    let catId = objeto.productoCategorias[i].categoriaId; // acceso a la propiedad Id
    producto.value.categoriaIds.push(catId);
  }
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
const idxFotoAEliminar = ref(null)
const variantAFotoEliminar = ref(null)

function confirmarEliminarFoto(idx) {
  // Confirm deletion for main objeto.fotos
  variantAFotoEliminar.value = null
  idxFotoAEliminar.value = idx
  dialogEliminarFoto.value = true
}

function confirmarEliminarFotoVariant(idx, variant) {
  // Confirm deletion for a specific variant's fotos
  variantAFotoEliminar.value = variant
  idxFotoAEliminar.value = idx
  dialogEliminarFoto.value = true
}

function eliminarFotoCarruselConfirmada() {
  try {
    if (variantAFotoEliminar.value && Array.isArray(variantAFotoEliminar.value.fotos)) {
      variantAFotoEliminar.value.fotos.splice(idxFotoAEliminar.value, 1)
    } else if (Array.isArray(objeto.fotos) && idxFotoAEliminar.value !== null) {
      objeto.fotos.splice(idxFotoAEliminar.value, 1)
    }
  } finally {
    dialogEliminarFoto.value = false
    idxFotoAEliminar.value = null
    variantAFotoEliminar.value = null
  }
}

function cancelarEliminarFoto() {
  dialogEliminarFoto.value = false
  idxFotoAEliminar.value = null
  variantAFotoEliminar.value = null
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
  categoriaIds: [],
  variants: []
})

function agregarVariante() {
  producto.value.variants.push({
    sku: '',
    talla: '',
    color: null,
    precioCosto: 1,
    precioVenta: 1,
    monedaCostoId: null,
    monedaVentaId: null,
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


function guardarProducto() {
  const formData = new FormData()
  const url = producto.value && producto.value.id ? 'Producto/ActualizarConFotos' : 'Producto/CrearConFotos'

  // --- Datos generales del producto ---
  formData.append('Codigo', producto.value ? producto.value.codigo : '')
  formData.append('Descripcion', producto.value ? producto.value.descripcion : '')
  formData.append('EsActivo', producto.value ? producto.value.esActivo : true)

  // Categorías
  if (producto.value && Array.isArray(producto.value.categoriaIds)) {
    producto.value.categoriaIds.forEach(id => {
      formData.append('CategoriaIds', id)
    })
  }

  // --- Variantes ---
  if (producto.value && Array.isArray(producto.value.variants)) {
    producto.value.variants.forEach((variant, index) => {
      formData.append(`Variants[${index}].SKU`, variant.sku || '')
      formData.append(`Variants[${index}].Talla`, variant.talla || '')
      formData.append(`Variants[${index}].Color`, variant.color || '')
      formData.append(`Variants[${index}].PrecioCosto`, variant.precioCosto || 0)
      formData.append(`Variants[${index}].PrecioVenta`, variant.precioVenta || 0)
      formData.append(`Variants[${index}].MonedaCostoId`, variant.monedaCostoId || '')
      formData.append(`Variants[${index}].MonedaVentaId`, variant.monedaVentaId || '')
      formData.append(`Variants[${index}].Stock`, variant.stock || 0)
      if (variant.id) formData.append(`Variants[${index}].Id`, variant.id)

      // Fotos: archivos nuevos (File) vs existentes (id/url)
      if (variant.fotos && variant.fotos.length > 0) {
        variant.fotos.forEach((file) => {
          if (typeof File !== 'undefined' && file instanceof File) {
            formData.append(`Variants[${index}].Fotos`, file)
          } else if (file && file.id) {
            formData.append(`Variants[${index}].FotosExistentes`, file.id)
          } else if (typeof file === 'string') {
            formData.append(`Variants[${index}].FotosExistentes`, file)
          } else if (file && file.url) {
            formData.append(`Variants[${index}].FotosExistentes`, file.url)
          }
        })
      }
    })
  }

  // Enviar todo junto
  saveDataParaObjetosConFotos(url, formData, load, close, dialogLoad, true, producto.value ? producto.value.id : null)
}

function cancelar() {
  producto.value = {
    codigo: '',
    descripcion: '',
    esActivo: true,
    categoriaIds: [],
    variants: []
  }
}

function abrirDialogoCrear(){
producto.value.id=undefined
 producto.value.codigo=''
producto.value.descripcion=''
producto.value.esActivo=true
producto.value.categoriaIds=[]
producto.value.variants=[]

    dialog.value = true
}
</script>
