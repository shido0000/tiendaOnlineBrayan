<template>
  <div>
    <TopBar :categories="categories" />

    <div class="q-pa-lg bg-grey-1">
      <div class="text-h5 text-weight-bold q-mb-lg">
        Todas las Categorías
      </div>

      <!-- Mensaje si no hay categorías -->
      <div v-if="!categorias.length" class="flex flex-center q-my-xl">
        <div class="text-grey-7 text-subtitle1 text-center">
          No hay categorías disponibles
        </div>
      </div>

      <!-- Grid de categorías -->
      <div v-else class="row q-col-gutter-md">
        <div
          v-for="categoria in categorias"
          :key="categoria.id"
          class="col-12 col-sm-6 col-md-4 col-lg-3"
        >
          <q-card
            class="q-pa-md shadow-2 cursor-pointer full-height categoria-card"
            @click="goToCategory(categoria.id)"
          >
            <!-- Icono de categoría -->
            <div class="text-center q-mb-md">
              <q-icon
                name="category"
                size="64px"
                color="primary"
              />
            </div>

            <!-- Nombre categoría -->
            <div class="text-center">
              <div class="text-h6 text-weight-bold q-mb-xs">
                {{ categoria.nombre }}
              </div>
              <div class="text-caption text-grey-6">
                {{ categoria.descripcion || 'Sin descripción' }}
              </div>
            </div>

            <!-- Botón ver productos -->
            <div class="text-center q-mt-md">
              <q-btn
                flat
                label="Ver productos"
                color="primary"
                size="sm"
              />
            </div>
          </q-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import TopBar from 'src/pages/Visual/components/TopBar.vue'
import { loadGetDatosInicio, loadGet } from 'src/assets/js/util/funciones'

const router = useRouter()
const categorias = ref([])
const categories = ref([])

onMounted(async () => {
  try {
    // Cargar datos iniciales
    const inicio = await loadGetDatosInicio('ObtenerDatosInicio')
    categories.value = inicio?.categoriasProductos ?? inicio?.categorias ?? inicio?.data?.categorias ?? inicio?.result?.categoriasProductos ?? []

    // Intentar cargar todas las categorías desde la API
    try {
      const response = await loadGet('CategoriaProducto/ObtenerListadoPaginado')
      if (response && Array.isArray(response)) {
        categorias.value = response
      } else if (response && response.data && Array.isArray(response.data)) {
        categorias.value = response.data
      } else {
        // Si no funciona la API, usar las categorías del inicio
        categorias.value = categories.value
      }
    } catch (e) {
      console.warn('Error cargando categorías desde API, usando las del inicio', e)
      categorias.value = categories.value
    }
  } catch (e) {
    console.error('Error cargando categorías:', e)
    categorias.value = []
  }
})

function goToCategory(id) {
  if (!id) return
  router.push({ name: 'CategoriaProductos', params: { id } }).catch(() =>
    router.push('/categoria/' + id)
  )
}
</script>

<style scoped lang="scss">
.categoria-card {
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  height: 100%;
}

.categoria-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.15) !important;
}

/* Media Queries para responsividad */
@media (max-width: 599px) {
  :deep(.q-page) {
    padding: 12px;
  }

  .text-h5 {
    font-size: 18px !important;
  }

  .text-h6 {
    font-size: 16px !important;
  }

  .text-caption {
    font-size: 12px !important;
  }

  .q-icon {
    font-size: 48px !important;
  }

  .row.q-col-gutter-md {
    margin-left: -8px;
    margin-right: -8px;
  }

  .row.q-col-gutter-md > [class*="col-"] {
    padding-left: 8px;
    padding-right: 8px;
  }
}

@media (max-width: 1023px) and (min-width: 600px) {
  .col-12.col-sm-6 {
    flex: 0 0 50%;
    max-width: 50%;
  }

  .text-h6 {
    font-size: 18px !important;
  }
}

@media (min-width: 1366px) {
  :deep(.q-page) {
    padding: 24px;
  }

  .col-12.col-lg-3 {
    flex: 0 0 25%;
    max-width: 25%;
  }
}
</style>
