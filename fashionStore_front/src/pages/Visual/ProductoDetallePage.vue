<template>
  <TopBar/>
  <div class="q-pa-lg bg-grey-1 producto-detalle">
    <div v-if="loading" class="flex flex-center q-pa-xl">
      <div>Cargando producto...</div>
    </div>

    <div v-else-if="!producto" class="flex flex-center q-pa-xl">
      <div>Producto no encontrado.</div>
    </div>

    <div v-else class="row q-col-gutter-md">
      <!-- Imagen principal -->
        <div class="col-12 col-md-6">
        <q-carousel
          v-if="getFotosForCarousel().length"
          v-model="slide"
          animated
          arrows
          navigation
          infinite
          height="520px"
        >
          <q-carousel-slide
            v-for="(foto, idx) in getFotosForCarousel()"
            :key="idx"
            :name="idx"
            class="flex flex-center bg-grey-2"
          >
            <q-img :src="getFotoUrlFromFoto(foto)" style="height:100%; width:100%; object-fit:cover;" />
          </q-carousel-slide>
        </q-carousel>

        <q-img v-else src="/img/sin-foto.jpg" style="height:520px; object-fit:cover;" />
      </div>

      <!-- Debug block removed from here to keep image and details side-by-side -->

      <!-- Información del producto -->
      <div class="col-12 col-md-6 q-mt-md ">
        <div class="product-header row items-center q-gutter-sm">
          <div class="col">
            <div class="text-h5 text-weight-bold">{{ producto.nombre || producto.descripcion || 'Sin título' }}</div>
            <div class="text-caption text-grey-7">Código: {{ displayedCodigo() }}</div>
            <div class="text-caption text-grey-7">SKU: {{ displayedSKU() }}</div>
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

        <!-- Variant selector -->
        <!-- Size selector: show one button per talla (size) -->
        <div v-if="producto.variants && producto.variants.length" class="q-mt-md row items-center q-gutter-sm">
          <div class="col-12 text-caption text-grey-7 q-mb-xs">Variantes:</div>
          <div class="col-12 row q-gutter-sm">
            <q-btn
              v-for="(v, idx) in producto.variants"
              :key="v.id || idx"
              dense
              outline
              :color="selectedVariantIndex.value === idx ? 'primary' : 'grey-6'"
              @click="selectVariant(idx)"
              class="q-mr-sm q-mb-sm"
            >
              <div style="display:flex;align-items:center;gap:8px">
                <div style="width:12px;height:12px;border-radius:50%;border:1px solid #ccc" :style="{ backgroundColor: v.color || '#ccc' }"></div>
                <span>{{ v.talla || '—' }}</span>
              </div>
            </q-btn>
          </div>
        </div>        <div class="product-price q-mt-md">
          <div class="text-subtitle1 text-weight-bold">
            $ {{ displayedPrice() != null ? Number(displayedPrice()).toLocaleString('es-ES', { minimumFractionDigits:2 }) : '0.00' }}
          </div>
          <div v-if="displayedOriginalPrice()" class="text-caption text-grey-6">
            <s>$ {{ Number(displayedOriginalPrice()).toLocaleString('es-ES',{ minimumFractionDigits:2 }) }}</s>
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
          <q-chip dense outline>Stock: {{ displayedStock() }}</q-chip>
          <q-chip dense outline>Categoria: {{ producto.categoriaNombre || '-' }}</q-chip>
          <q-chip dense outline>Talla: {{ displayedTalla() }}</q-chip>
          <div class="row items-center q-gutter-xs">
            <span>Color:</span>
            <div class="color-circle" :style="{ backgroundColor: displayedColor() || '#ccc' }"></div>
          </div>
        </div>
      </div>
    </div>

    <!-- Debug: resolved image URLs (temporary) -->
    <div class="q-pa-sm">
      <div class="text-caption text-grey-6">Debug: URLs resueltas para las fotos (haz click para abrir)</div>
      <div class="q-mt-xs">
        <div v-for="(u, i) in getResolvedImageUrls()" :key="i" class="q-mb-xs">
          <a :href="u" target="_blank" rel="noreferrer">{{ u }}</a>
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
const selectedVariantIndex = ref(null)

// helper to obtain fotos for carousel preferring product.fotos then first variant fotos
function getFotosForCarousel() {
  try {
    // if a variant is selected prefer its fotos
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    if (sel != null && producto.value?.variants && producto.value.variants[sel]) {
      const v = producto.value.variants[sel]
      if (Array.isArray(v.fotos) && v.fotos.length) return v.fotos.map(f => (typeof f === 'object' ? (f.url || f.img || f.path) : f))
      const cand = v.fotos && v.fotos.length ? v.fotos[0] : (v.foto || v.fotoUrl || v.url)
      if (cand) return [cand]
    }

    if (producto.value?.fotos && producto.value.fotos.length) return producto.value.fotos

    // fallback: try first variant (if any)
    const vars = producto.value?.variants || producto.value?.variantes
    if (Array.isArray(vars) && vars.length) {
      const f0 = vars[0]
      if (f0) {
        if (Array.isArray(f0.fotos) && f0.fotos.length) return f0.fotos
        const candidate = f0.foto || f0.fotoUrl || f0.imagen || f0.imagenUrl || f0.url || f0.image || f0.picture
        if (candidate) return [candidate]
      }
    }

    // check Spanish API naming: productosVariantes
    const pvs = producto.value?.productosVariantes
    if (Array.isArray(pvs) && pvs.length) {
      const pv0 = pvs[0]
      if (pv0) {
        if (Array.isArray(pv0.fotos) && pv0.fotos.length) return pv0.fotos.map(f => (typeof f === 'object' ? (f.url || f.img || f.path) : f))
        const candidate = pv0.foto || pv0.fotoUrl || pv0.imagen || pv0.imagenUrl || pv0.url || pv0.image || pv0.picture
        if (candidate) return [candidate]
      }
    }
    return []
  } catch (e) {
    console.warn('getFotosForCarousel error', e)
    return []
  }
}

function getResolvedImageUrls() {
  try {
    const fotos = getFotosForCarousel()
    return fotos.map(f => {
      try { return getFotoUrlFromFoto(f) } catch (e) { return String(f) }
    })
  } catch (e) { return [] }
}

async function cargarProducto(id) {
  loading.value = true
  dialogLoad.value = true
  try {
  // Obtener el objeto específico desde la API (usa la ruta que indicaste)
  const objeto = await loadGetHastaData(`Producto/ObtenerProductoEspecifico/${id}`)
  console.debug('[ProductoDetalle] raw objeto ->', objeto)

  // Adaptamos el objeto recibido a la estructura que usamos en el front
  producto.value = {
    id: objeto?.id,
    codigo: objeto?.codigo,
    descripcion: objeto?.descripcion,
    esActivo: objeto?.esActivo,
    sku: objeto?.sku,
    precioCosto: objeto?.precioCosto,
    precioVenta: objeto?.precioVenta,
    monedaCostoId: objeto?.monedaCostoId,
    monedaVentaId: objeto?.monedaVentaId,
    categoriasIds: objeto?.categoriasIds || [],

    // Mapeamos las variantes
    variants: (objeto?.productoVariantes || []).map(v => ({
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

  console.debug('Producto adaptado:', producto.value)

  // elegir variante inicial: la marcada como principal o la primera
  const principalIndex = producto.value.variants.findIndex(v => v && (v.principal === true || v.principal === 'true'))
  selectedVariantIndex.value = principalIndex >= 0 ? principalIndex : (producto.value.variants.length ? 0 : null)

  // Guardar copia de las fotos originales (rutas) y poblar producto.value.fotos con todas las fotos de las variantes
  let fotosOriginales = []
  try {
    fotosOriginales = (objeto?.productoVariantes || [])
      .flatMap(v => (v.fotos || []).map(f => f.url || f))
  } catch (e) {
    fotosOriginales = []
  }

  // Poblamos producto.value.fotos (usado por el carrusel principal si no hay variante seleccionada) con las rutas encontradas
  producto.value.fotos = fotosOriginales.slice()

  // cargar relacionados (usando la categoría si está disponible)
  const catId = objeto?.categoriaId || objeto?.categoria?.id || producto.value?.categoriasIds?.[0]
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
    const final = apiFotosBaseUrl + (candidate.startsWith('/') ? candidate : '/' + candidate)
    console.debug('[ProductoDetalle] getFotoUrl ->', { apiFotosBaseUrl, candidate, final })
    return final
  }
  console.debug('[ProductoDetalle] getFotoUrl -> absolute', candidate)
  return candidate
}

function getFotoUrlFromFoto(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  const candidate = typeof foto === 'object' ? (foto.url || foto.img || foto.path || foto.imagen || foto.foto) : foto
  console.debug('[ProductoDetalle] getFotoUrlFromFoto candidate ->', candidate)
  return getFotoUrl(candidate)
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

  // prefer image from first variant if available (variants or variantes)
  if ((!candidate || candidate === '') && (Array.isArray(prod.variants) || Array.isArray(prod.variantes))) {
    const vars = Array.isArray(prod.variants) ? prod.variants : prod.variantes
    if (vars && vars.length) {
      const firstVar = vars[0]
      if (firstVar) {
        candidate = candidate || firstVar.fotoUrl || firstVar.foto || firstVar.imagen || firstVar.imagenUrl || firstVar.url || firstVar.image || firstVar.picture || null
        if ((!candidate || candidate === '') && Array.isArray(firstVar.fotos) && firstVar.fotos.length) {
          const f = firstVar.fotos[0]
          if (typeof f === 'string' && f.trim() !== '') candidate = f
          else if (typeof f === 'object' && f !== null) candidate = f.url || f.img || f.path || candidate
        }
      }
    }
  }
  return candidate || null
}

// helpers to prefer variant fields for display
function displayedPrice() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.precioVenta ?? producto.value?.precioVenta ?? producto.value?.precio ?? null
  } catch (e) { return producto.value?.precioVenta ?? producto.value?.precio ?? null }
}
function displayedOriginalPrice() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.precioOriginal ?? producto.value?.precioOriginal ?? null
  } catch (e) { return producto.value?.precioOriginal ?? null }
}
function displayedStock() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.stock ?? producto.value?.stock ?? producto.value?.cantidadDisponible ?? 'N/A'
  } catch (e) { return 'N/A' }
}
function displayedCodigo() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.codigo ?? producto.value?.codigo ?? '-'
  } catch (e) { return '-' }
}
function displayedSKU() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.sku ?? producto.value?.sku ?? '-'
  } catch (e) { return '-' }
}
function displayedColor() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0] || producto.value?.productosVariantes?.[0])
    return v?.color ?? producto.value?.color ?? '#ccc'
  } catch (e) { return '#ccc' }
}

function displayedTalla() {
  try {
    const sel = (selectedVariantIndex.value != null) ? selectedVariantIndex.value : null
    const v = sel != null && producto.value?.variants ? producto.value?.variants[sel] : (producto.value?.variants?.[0] || producto.value?.variantes?.[0])
    if (!v) return '-'
    return v.talla ?? '-'
  } catch (e) { return '-' }
}

function selectVariant(idx) {
  selectedVariantIndex.value = idx
  slide.value = 0
}

function onAddToCart() {
  if (!producto.value) return
  if (cantidad.value < 1) cantidad.value = 1
  // include selected variant information when adding to cart
  let payload = { ...producto.value }
  if (selectedVariantIndex.value != null && producto.value?.variants && producto.value.variants[selectedVariantIndex.value]) {
    payload = { ...payload, selectedVariant: producto.value.variants[selectedVariantIndex.value] }
  }
  cart.addItem(payload, cantidad.value)
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
  justify-content: center;
}

/* Ensure image left / details right layout and vertical centering on desktop */
@media (min-width: 992px) {
  .main-row { align-items: stretch; }
  .left-col, .right-col { min-height: 520px; }
  .right-col { justify-content: center; }
  .left-col .q-carousel, .left-col .q-img { height: 100% !important; }
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
