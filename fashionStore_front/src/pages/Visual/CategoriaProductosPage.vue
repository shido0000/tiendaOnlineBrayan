<template>
  <div>
  <TopBar :categories="categories" />
    <div class="q-pa-lg bg-grey-1">
    <div class="text-h6 text-weight-bold q-mb-md">
      Productos en {{ categoria?.nombre }}
    </div>

<!-- Mensaje si no hay productos -->
  <div v-if="!productos.length" class="flex flex-center q-my-xl">
    <div class="text-grey-7 text-subtitle1 text-center">
      No hay productos disponibles en esta categoría
    </div>
  </div>

    <div class="row q-col-gutter-md">
      <div
        v-for="producto in productos"
        :key="producto.id"
        class="col-12 col-sm-6 col-md-3"
      >
        <q-card class="q-pa-md shadow-2">

        <!-- Carrusel de fotos -->
<div v-if="producto.fotos && producto.fotos.length" class="q-mb-md">
  <q-carousel
    v-model="producto.slide"
    animated
    arrows
    navigation
    infinite
    height="180px"
    class="rounded-borders"
  >
      <q-carousel-slide
      v-for="(foto, idx) in producto.fotos"
      :key="idx"
      :name="idx"
      class="flex flex-center bg-grey-2"
    >
      <q-img
        :src="getFotoUrlFromFoto(foto)"
        style="height: 100%; width: 100%; object-fit: cover;"
      >
        <template v-slot:error>
          <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
        </template>
      </q-img>
    </q-carousel-slide>
  </q-carousel>
</div>

<!-- Si no hay fotos -->
<q-img
  v-else
  src="/img/sin-foto.jpg"
  style="height: 180px; object-fit: cover; border-radius: 8px;"
  class="q-mb-md"
/>

          <!-- Código -->
          <q-card-section class="text-center text-primary text-weight-bold">
            {{ producto.codigo }}
          </q-card-section>

          <!-- Descripción -->
          <q-card-section class="text-center text-grey-8">
            {{
              producto.descripcion && producto.descripcion.length > 40
                ? producto.descripcion.substring(0, 40) + "..."
                : producto.descripcion || "Sin descripción"
            }}
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
            <q-btn dense round flat :icon="wishlist.isFavorito(producto.id) ? 'favorite' : 'favorite_border'" @click.stop="() => wishlist.toggle(producto)" />
            <q-btn flat round icon="shopping_cart" color="purple-4" @click.stop="() => cart.addItem(producto,1)" />
          </q-card-actions>
        </q-card>
      </div>
    </div>

    <DialogLoad :dialogLoad="dialogLoad" />
    </div>
  </div>
</template>
<script setup>
import { ref, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import { loadGet, loadGetDatosInicio, loadGetHastaData } from 'src/assets/js/util/funciones'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import TopBar from 'src/pages/Visual/components/TopBar.vue'
import { apiFotosBaseUrl } from 'src/boot/axios'
import { useWishlist } from 'src/stores/wishlistStore'
import useCart from 'src/stores/cartStore'

const route = useRoute()
const productos = ref([])
const categoria = ref(null)
const categories = ref([])
const debugImg = true
const dialogLoad = ref(false)
const wishlist = useWishlist()
const cart = useCart()

async function cargarDatosCategoria(id) {
  dialogLoad.value = true
  try {
    // Obtener datos de la categoría
    categoria.value = await loadGet(`CategoriaProducto/ObtenerPorId/${id}`)

    // Obtener productos de la categoría
    const respuesta = await loadGetHastaData(`Inventario/ObtenerProductosDelInventarioPorCategoria/${id}`)
    if (debugImg) console.log('[CategoriaProductos] raw respuesta:', respuesta)

    let lista = []
    if (Array.isArray(respuesta)) lista = respuesta
    else if (respuesta == null) lista = []
    else if (Array.isArray(respuesta.result?.elementos)) lista = respuesta.result.elementos
    else if (Array.isArray(respuesta.data?.result?.elementos)) lista = respuesta.data.result.elementos
    else if (Array.isArray(respuesta.elementos)) lista = respuesta.elementos
    else if (Array.isArray(respuesta.productos)) lista = respuesta.productos
    else if (Array.isArray(respuesta.data)) lista = respuesta.data
    else if (Array.isArray(respuesta.result)) lista = respuesta.result
    else if (typeof respuesta === 'object' && respuesta !== null) {
      const firstArray = Object.values(respuesta).find(v => Array.isArray(v))
      if (firstArray) lista = firstArray
    }

    productos.value = (lista ?? []).map(p => ({ ...p, slide: 0 }))
    if (debugImg) console.log('[CategoriaProductos] mapped productos count:', productos.value.length)

    // cargar categorías para el TopBar
    try {
      const inicio = await loadGetDatosInicio('ObtenerDatosInicio')
      categories.value = inicio?.categoriasProductos ?? inicio?.categorias ?? inicio?.data?.categorias ?? inicio?.result?.categoriasProductos ?? []
    } catch (e2) {
      if (debugImg) console.warn('[CategoriaProductos] could not load categorias from inicio', e2)
      categories.value = []
    }
  } catch (e) {
    console.warn('[CategoriaProductos] error loading category products', e)
    productos.value = []
  } finally {
    dialogLoad.value = false
  }
}

onMounted(() => {
  cargarDatosCategoria(route.params.id)
})

// 👇 Aquí está la clave: observar cambios en el parámetro id
watch(
  () => route.params.id,
  (nuevoId) => {
    cargarDatosCategoria(nuevoId)
  }
)

function getFotoUrl(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  let candidate = foto
  if (typeof foto === 'object') {
    candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto || null
  }
  if (!candidate) return '/img/sin-foto.jpg'
  if (typeof candidate !== 'string') candidate = String(candidate)
  if (/^https?:\/\//.test(candidate)) return candidate
  return apiFotosBaseUrl + (candidate.startsWith('/') ? candidate : '/' + candidate)
}

function getFotoUrlFromFoto(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  if (typeof foto === 'object') {
    const candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || null
    return getFotoUrl(candidate)
  }
  return getFotoUrl(foto)
}
</script>
