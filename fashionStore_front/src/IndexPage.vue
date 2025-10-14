

<template>
  <div class="q-pa-none bg-grey-1">
    <!-- Header -->
    <div class="header q-pa-md row items-center justify-between bg-white shadow-1">
      <div class="row items-center">
        <img src="/public/img/MarcaZun-02.png" alt="Logo" style="height:40px; margin-right:16px;" />
        <q-btn-dropdown label="Categorías" color="purple-2" text-color="purple-7" class="q-mr-md" />
        <q-input dense outlined placeholder="Buscar..." class="q-mr-md" style="width:200px;" />
        <q-btn flat round icon="search" color="purple-4" />
      </div>
      <div class="row items-center">
        <q-btn flat round icon="place" color="purple-4" class="q-mr-sm">
          <q-tooltip class="bg-accent" transition-show="flip-right" transition-hide="flip-left" :offset="[10, 10]">Ubicacion</q-tooltip>
        </q-btn>
        <q-btn flat round icon="shopping_cart" color="purple-4" class="q-mr-sm">
          <q-tooltip class="bg-accent" transition-show="flip-right" transition-hide="flip-left" :offset="[10, 10]">Carrito</q-tooltip>
        </q-btn>
        <q-btn flat round icon="person" color="purple-4" @click="$router.push('/login')">
          <q-tooltip class="bg-accent" transition-show="flip-right" transition-hide="flip-left" :offset="[10, 10]">Usuario</q-tooltip>
        </q-btn>
      </div>
    </div>
    <!-- Navigation -->
    <div class="nav q-px-lg q-py-sm bg-white row items-center shadow-1">
      <q-btn flat label="Productos" icon="expand_more" class="q-mr-md" />
      <q-btn flat label="Catálogo" class="q-mr-md" />
      <q-btn flat label="Ofertas" />
    </div>
    <!-- Banners de Promoción -->
    <div class="q-mt-lg q-mb-md">
      <div class="row items-center justify-center">
        <q-btn flat round icon="chevron_left" />
        <div class="text-h4 text-weight-bold q-mx-lg">Banners de Promoción</div>
        <q-btn flat round icon="chevron_right" />
      </div>
      <div class="row justify-center q-mt-sm">
        <q-icon name="fiber_manual_record" size="sm" color="grey-5" v-for="n in 6" :key="n" class="q-mx-xs" />
      </div>
    </div>

 <!-- Novedades -->
<div class="q-px-lg q-mb-lg">
  <div class="text-h6 text-weight-bold q-mb-md">Novedades</div>
  <div class="row q-col-gutter-md">
    <div
      v-for="producto in objeto.productosNovedades.slice(0, 4)"
      :key="'novedad'+producto.id"
      class="col-12 col-sm-6 col-md-3"
    >
      <q-card class="q-pa-md shadow-2">

        <!-- Foto principal si existe -->
        <q-img
          v-if="producto.fotos && producto.fotos.length"
          :src="producto.fotos.find(f => f.esPrincipal)?.url || producto.fotos[0].url"
          style="height: 180px; object-fit: cover; border-radius: 8px;"
          class="q-mb-md"
        >
          <template v-slot:error>
            <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
          </template>
        </q-img>

        <!-- Código -->
        <q-card-section class="text-center text-primary text-weight-bold">
          {{ producto.codigo }}
        </q-card-section>

        <!-- Descripción recortada -->
        <q-card-section class="text-center text-grey-8">
          {{
            producto.descripcion && producto.descripcion.length > 40
              ? producto.descripcion.substring(0, 40) + "..."
              : producto.descripcion || "Sin descripción"
          }}
        </q-card-section>

        <!-- SKU -->
        <q-card-section class="text-center text-grey-7">
          SKU: {{ producto.sku }}
        </q-card-section>

        <!-- Precio -->
        <q-card-section class="text-center text-grey-6">
          $ {{
            producto.precioVenta != null
              ? Number(producto.precioVenta).toLocaleString("es-ES", {
                  minimumFractionDigits: 2,
                  maximumFractionDigits: 2,
                })
              : "0.00"
          }}
        </q-card-section>

        <q-card-actions align="right">
          <q-btn flat round icon="shopping_cart" color="purple-4" />
        </q-card-actions>
      </q-card>
    </div>
  </div>

  <!-- Paginador de puntitos -->
  <div class="row justify-center q-mt-sm">
    <q-icon
      name="fiber_manual_record"
      size="sm"
      color="grey-5"
      v-for="n in Math.max(1, objeto.productosNovedades.length)"
      :key="'nov-dot'+n"
      class="q-mx-xs"
    />
  </div>
</div>


<!-- Catálogo -->
<div class="q-px-lg q-py-xl bg-purple-2">
  <div class="text-h6 text-weight-bold q-mb-md">Catálogo</div>

  <q-carousel
    v-model="categoriaSlide"
    animated
    arrows
    navigation
      navigation-position="bottom"
    infinite
    height="240px"
    class="bg-transparent   q-pb-lg"
  >
    <!-- Agrupamos de 4 en 4 -->
    <q-carousel-slide
      v-for="(chunk, idx) in chunkCategorias(objeto.categoriasProductos, 4)"
      :key="'cat-slide'+idx"
      :name="idx"
      class="row justify-center q-col-gutter-md "
    >
      <div
        v-for="categoria in chunk"
        :key="categoria.id"
        class="col-12 col-sm-6 col-md-3 q-mb-md"
      >
        <q-card
          class="shadow-2 relative-position"
          style="height: 180px; border-radius: 8px; overflow: hidden;"
          @click="$router.push({ name: 'CategoriaProductos', params: { id: categoria.id } })"
        >
          <!-- Fondo con la foto -->
          <q-img
            :src="getFotoUrl(categoria.fotoUrl)"
            style="height: 100%; width: 100%; object-fit: cover; filter: brightness(0.6);"
          />

          <!-- Texto encima -->
          <div
            class="absolute-full flex flex-center text-white text-subtitle1 text-weight-bold"
          >
            {{ categoria.nombre }}
          </div>
        </q-card>
      </div>
    </q-carousel-slide>
  </q-carousel>
</div>



    <!-- Más solicitado -->
    <div class="q-px-lg q-py-xl">
      <div class="text-h6 text-weight-bold q-mb-md">Mas solicitado</div>
      <div class="row q-col-gutter-md">
        <div v-for="n in 4" :key="'mas'+n" class="col-12 col-sm-6 col-md-3">
          <q-card class="q-pa-md shadow-2">
            <q-card-section class="text-center text-grey-8">Producto {{ n }}</q-card-section>
            <q-card-section class="text-center text-grey-6">$ 0.00</q-card-section>
            <q-card-actions align="right">
              <q-btn flat round icon="shopping_cart" color="purple-4" />
            </q-card-actions>
          </q-card>
        </div>
      </div>
      <div class="row justify-center q-mt-sm">
        <q-icon name="fiber_manual_record" size="sm" color="grey-5" v-for="n in 5" :key="'mas-dot'+n" class="q-mx-xs" />
      </div>
    </div>

    <!-- Footer -->
    <div class="footer q-pt-xl q-pb-md bg-purple-2 text-center">
      <div class="q-mb-md">
        <div>Inicio</div>
        <div>Sobre Nosotros</div>
        <div>Política de Privacidad</div>
        <div>Términos y Condiciones</div>
        <div>Política de Devoluciones y Cambios</div>
        <div>Colabora con Nosotros</div>
      </div>
      <div class="q-mb-md text-caption">
        Dirección: 20 de mayo 534 apto 14 / marta abreu y línea de ferrocarril , Habana - 10400, Habana, Cuba<br>
        Horario: Lunes a Sábado 10:00 am - 6:00 pm<br>
        Teléfono: <a href="tel:+5351564397" class="text-purple-8">+5351564397</a>
      </div>
      <div class="q-mb-md">
        <q-btn flat round icon="telegram" color="purple-7" />
        <q-btn flat round icon="facebook" color="purple-7" />
        <q-btn flat round icon="whatsapp" color="purple-7" />
        <q-btn flat round icon="instagram" color="purple-7" />
      </div>
      <div class="text-caption">Copyright © 2025. Todos los derechos reservados.</div>
    </div>

      <DialogLoad :dialogLoad="dialogLoad" />
    </div>

  </template>



<script setup>
import { onMounted, reactive, ref } from 'vue'
import { loadGet, loadGetDatosInicio } from './assets/js/util/funciones'
import DialogLoad from './components/DialogBoxes/DialogLoad.vue'
import { apiFotosBaseUrl } from './boot/axios'

const categoriaSlide = ref(0)

const dialogLoad = ref(false)
const objetoInicial ={
productosNovedades:[],
categoriasProductos:[],
}
const objeto = reactive({ ...objetoInicial })

onMounted(async () => {
  dialogLoad.value = true
  let elementosInicio=await loadGetDatosInicio('ObtenerDatosInicio')
  objeto.productosNovedades = elementosInicio.productosNovedades ?? []
   objeto.categoriasProductos = elementosInicio.categoriasProductos ?? []
  dialogLoad.value = false
})

function getFotoUrl(foto) {
  if (!foto) {
    return '/img/sin-foto.jpg'   // 👈 imagen por defecto en /public/img/
  }
  if (/^https?:\/\//.test(foto)) return foto
  return apiFotosBaseUrl + (foto.startsWith('/') ? foto : '/' + foto)
}

// función para dividir el array en chunks de 4
function chunkCategorias(array, size) {
  const result = []
  for (let i = 0; i < array.length; i += size) {
    result.push(array.slice(i, i + size))
  }
  return result
}
</script>
<style lang="scss" scoped>
@import './css/quasar.variables.scss';
.header {
  border-bottom: 1px solid $bry-grey-4;
}
.nav {
  border-bottom: 1px solid $bry-grey-4;
}
.footer {
  border-top: 1px solid $bry-purple-4;
}

:deep(.q-carousel__navigation) {
  transform: translateY(30px); // 👈 los baja visualmente
}



</style>
