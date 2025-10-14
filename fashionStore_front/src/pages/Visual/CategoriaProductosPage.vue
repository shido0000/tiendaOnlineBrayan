<template>
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
        :src="getFotoUrl(foto.url || foto)"
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
            <q-btn flat round icon="shopping_cart" color="purple-4" />
          </q-card-actions>
        </q-card>
      </div>
    </div>

    <DialogLoad :dialogLoad="dialogLoad" />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { loadGet, loadGetDatosInicio, loadGetHastaData } from 'src/assets/js/util/funciones'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import { apiFotosBaseUrl } from 'src/boot/axios'

const route = useRoute()
const productos = ref([])
const categoria = ref(null)
const dialogLoad = ref(false)

onMounted(async () => {
  dialogLoad.value = true
  // Obtener datos de la categoría
  categoria.value = await loadGet(`CategoriaProducto/ObtenerPorId/${route.params.id}`)
  // Obtener productos de la categoría
await loadGetHastaData(
  `Inventario/ObtenerProductosDelInventarioPorCategoria/${route.params.id}`
).then(respuesta => {
  console.log("respuesta: ", respuesta)
  productos.value = (respuesta ?? []).map(p => ({
    ...p,
    slide: 0 // 👈 estado inicial del carrusel de ese producto
  }))
})

console.log("productos.value : ", productos.value)

  dialogLoad.value = false
})

function getFotoUrl(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  if (/^https?:\/\//.test(foto)) return foto
  return apiFotosBaseUrl + (foto.startsWith('/') ? foto : '/' + foto)
}
</script>
