<template>
  <TopBar/>
  <div class="q-pa-lg bg-grey-1 producto-detalle">
    <div v-if="loading" class="flex flex-center q-pa-xl">
      <div>Cargando producto...</div>
    </div>

    <div v-else-if="!producto" class="flex flex-center q-pa-xl">
      <div>Producto no encontrado.</div>
    </div>

    <div v-else class="row q-col-gutter-md main-row">
      <!-- Imagen principal -->
      <div class="col-12 col-md-6 left-col">
        <q-carousel
          v-if="producto.fotos && producto.fotos.length"
          v-model="slide"
          animated
          arrows
          navigation
          infinite
          height="520px"
        >
          <q-carousel-slide
            v-for="(foto, idx) in producto.fotos"
            :key="idx"
            :name="idx"
            class="flex flex-center bg-grey-2"
          >
            <q-img :src="getFotoUrlFromFoto(foto)" style="height:100%; width:100%; object-fit:cover;" />
          </q-carousel-slide>
        </q-carousel>

        <q-img v-else src="/img/sin-foto.jpg" style="height:520px; object-fit:cover;" />
      </div>

      <!-- Información del producto -->
      <div class="col-12 col-md-5 right-col q-mt-md">
        <div class="product-header row items-center q-gutter-sm">
          <div class="col">
            <div class="text-h5 text-weight-bold">{{ producto.nombre || producto.descripcion || 'Sin título' }}</div>
            <div class="text-caption text-grey-7">Código: {{ producto.codigo || '-' }}</div>
            <div class="text-caption text-grey-7">SKU: {{ producto.sku || '-' }}</div>
          </div>
          <div class="col-auto">
            <q-btn
              dense
              round
              flat
              :icon="wishlist.isFavorito(producto.id) ? 'favorite' : 'favorite_border'"
              color="primary"
              @click="() => wishlist.toggle(producto)"
            />
          </div>
        </div>

        <div class="product-price q-mt-md">
          <div class="text-subtitle1 text-weight-bold">
            $ {{ producto.precioVenta != null ? Number(producto.precioVenta).toLocaleString('es-ES', { minimumFractionDigits:2 }) : '0.00' }}
          </div>
          <div v-if="producto.precioOriginal" class="text-caption text-grey-6">
            <s>$ {{ Number(producto.precioOriginal).toLocaleString('es-ES',{ minimumFractionDigits:2 }) }}</s>
          </div>
        </div>

        <div class="q-mb-lg product-desc">{{ producto.descripcion || 'Sin descripción' }}</div>

        <div class="row items-center q-gutter-sm actions-row">
          <q-input type="number" v-model.number="cantidad" min="1" style="width:110px" dense />
          <q-btn color="primary" unelevated @click="onAddToCart">
            <q-icon name="add_shopping_cart" /> Añadir al carrito
          </q-btn>
          <q-btn flat color="secondary">Comprar ahora</q-btn>
        </div>

        <!-- Detalles con color visual -->
        <div class="q-mt-md row items-center q-gutter-sm">
          <q-chip dense outline>Stock: {{ producto.stock ?? 'N/A' }}</q-chip>
          <q-chip dense outline>Categoria: {{ producto.categoriaNombre || '-' }}</q-chip>
          <div class="row items-center q-gutter-xs">
            <span>Color:</span>
            <div class="color-circle" :style="{ backgroundColor: producto.color || '#ccc' }"></div>
          </div>
        </div>
      </div>
    </div>

    <!-- Sección inferior unificada -->
    <div class="q-mt-xl related-section">
      <div class="row items-center q-mb-md">
        <div class="col">
          <div class="text-h6 text-weight-bold">
            {{ relatedProducts.length ? 'Productos relacionados' : 'Explora otras opciones' }}
          </div>
        </div>
      </div>

      <div class="related-row">
  <div
    v-for="item in (relatedProducts.length ? relatedProducts : exploreProducts)"
    :key="item.id"
    class="rel-card"
    @click="goToRelated(item)"
  >
    <q-card flat class="shadow-1 rel-card-inner">
      <!-- Imagen principal -->
      <q-img
        :src="getFotoUrl(getProductoImage(item))"
        ratio="1"
        class="rel-card-img"
      >
        <template #error>
          <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
        </template>
      </q-img>

      <!-- Información -->
      <q-card-section class="rel-card-info">
        <div class="text-subtitle2 ellipsis rel-card-name">
          {{ item.descripcion || item.nombre }}
        </div>
        <div class="text-subtitle1 text-weight-bold rel-card-price">
          $ {{ item.precioVenta != null ? Number(item.precioVenta).toLocaleString('es-ES',{ minimumFractionDigits:2 }) : '0.00' }}
        </div>


      </q-card-section>
    </q-card>
  </div>
</div>
    </div>

    <DialogLoad :dialogLoad="dialogLoad" />
  </div>
</template>
<script setup>
import { ref, onMounted, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { loadGet, loadGetHastaData } from 'src/assets/js/util/funciones'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import { apiFotosBaseUrl } from 'src/boot/axios'
import useCart from 'src/stores/cartStore'
import { useWishlist } from 'src/stores/wishlistStore'
import TopBar from './components/TopBar.vue'

const route = useRoute()
const router = useRouter()
const producto = ref(null)
const loading = ref(true)
const dialogLoad = ref(false)
const slide = ref(0)
const cantidad = ref(1)
const relatedProducts = ref([])
const exploreProducts = ref([])
const wishlist = useWishlist()
const cart = useCart()

async function cargarProducto(id) {
  loading.value = true
  dialogLoad.value = true
  try {
    producto.value = await loadGet(`Producto/ObtenerPorId/${id}`)
    if (producto.value) {
      producto.value.fotos = (producto.value.fotos || []).map(f => f ?? null)
    }
    // cargar relacionados
    const catId = producto.value?.categoriaId || producto.value?.categoria?.id
    if (catId) {
      const lista = await loadGetHastaData(`Inventario/ObtenerProductosDelInventarioPorCategoria/${catId}`)
      relatedProducts.value = (lista ?? []).filter(p => p && String(p.id) !== String(producto.value?.id)).slice(0, 8)
    }
    // cargar explorar
    const all = await loadGet('Producto/ObtenerListadoPaginado')
    exploreProducts.value = (all ?? []).filter(p => p && String(p.id) !== String(producto.value?.id)).slice(0, 8)
  } catch (e) {
    console.warn('Error cargando producto', e)
  } finally {
    loading.value = false
    dialogLoad.value = false
  }
}

onMounted(() => {
  cargarProducto(route.params.id)
})

watch(() => route.params.id, (nuevoId) => {
  if (nuevoId) cargarProducto(nuevoId)
})
function getFotoUrl(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  let candidate = typeof foto === 'object'
    ? foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto
    : foto
  if (!candidate) return '/img/sin-foto.jpg'
  if (!/^https?:\/\//.test(candidate)) {
    candidate = apiFotosBaseUrl + (candidate.startsWith('/') ? candidate : '/' + candidate)
  }
  return candidate
}

function getFotoUrlFromFoto(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  return getFotoUrl(typeof foto === 'object' ? (foto.url || foto.img || foto.path || foto.imagen) : foto)
}

function getProductoImage(prod) {
  if (!prod) return null
  let candidate =
    prod.fotoUrl ||
    prod.imagen ||
    prod.imagenUrl ||
    prod.imagenPrincipal ||
    prod.url ||
    prod.image ||
    prod.picture

  if ((!candidate || candidate === '') && Array.isArray(prod.fotos) && prod.fotos.length > 0) {
    const first = prod.fotos[0]
    candidate = typeof first === 'string' ? first : (first.url || first.img || first.path)
  }
  return candidate || null
}

function onAddToCart() {
  if (!producto.value) return
  if (cantidad.value < 1) cantidad.value = 1
  cart.addItem(producto.value, cantidad.value)
}

function goToRelated(p) {
  const id = p?.id || p?.productoId || p?.productId
  if (!id) return
  router.push({ name: 'ProductoDetalle', params: { id } })
}
</script>

<style scoped>
.producto-detalle {
  max-width: 1200px;
  margin: 0 auto;
  margin-top: 32px;
}

.main-row {
  gap: 32px;
}

.left-col .q-carousel,
.left-col .q-img {
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.right-col {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.product-header {
  border-bottom: 1px solid #eee;
  padding-bottom: 8px;
}

.product-price {
  display: flex;
  gap: 12px;
  align-items: baseline;
  font-size: 1.2rem;
}

.product-desc {
  color: #444;
  line-height: 1.5;
}

.actions-row {
  margin-top: 12px;
}
.actions-row q-btn {
  min-width: 150px;
}

.related-section {
  margin-top: 48px;
}

.related-row {
  display: flex;
  gap: 24px;
  overflow-x: auto;
  padding-bottom: 12px;
}

.rel-card {
  flex: 0 0 240px;
  cursor: pointer;
}
.rel-card-inner {
  display: flex;
  flex-direction: column;
  height: 100%;
  border-radius: 8px;
}

.rel-card-img {
  border-bottom: 1px solid #eee;
}

.rel-card-info {
  padding: 8px;
  text-align: center;
}

.rel-card-name {
  font-size: 0.95rem;
  font-weight: 500;
  margin-bottom: 4px;
}

.rel-card-price {
  font-size: 1rem;
  color: #333;
}


.rel-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 16px rgba(0,0,0,0.15);
}

.rel-card .q-card {
  height: 100%;
  border-radius: 8px;
}

.ellipsis {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

/* círculo de color */
.color-circle {
  width: 20px;
  height: 20px;
  border-radius: 50%;
  border: 1px solid #ccc;
}

@media (max-width: 768px) {
  .left-col .q-img,
  .left-col .q-carousel {
    height: 320px !important;
  }
}
</style>
