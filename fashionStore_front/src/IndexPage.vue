
<template>
  <div class="q-pa-none bg-grey-1">
  <!-- centralized TopBar component -->
  <TopBar :categories="objeto.categoriasProductos" v-model:leftDrawer="leftDrawer" @update:searchCategory="selectedCategory = $event" />
    <!-- Banners de Promoción: full-viewport carousel -->
    <q-carousel
      v-model="bannerSlide"
      animated
      arrows
      navigation
      navigation-position="bottom"
      infinite
      class="banner-carousel"
      height="66vh"
    >
      <q-carousel-slide
        v-for="(slide, idx) in bannerSlides"
        :key="'banner-'+idx"
        :name="idx"
        class="banner-slide"
      >
        <div class="banner-bg" :style="{ backgroundImage: 'url(' + slide.image + ')' }">
          <div class="banner-overlay"></div>
          <div class="banner-content">
            <h2 class="banner-title">{{ slide.title }}</h2>
            <p class="banner-subtitle">{{ slide.subtitle }}</p>
            <q-btn color="primary" unelevated @click="$router.push(slide.ctaLink)">{{ slide.ctaText }}</q-btn>
          </div>
        </div>
      </q-carousel-slide>
    </q-carousel>

<!-- Novedades (Amazon-like horizontal) -->
<section class="q-px-lg q-py-md amazon-section">
  <div class="row items-center q-mb-md">
    <div class="col">
      <div class="text-h6 text-weight-bold">Novedades</div>
    </div>
    <div class="col-auto">
      <q-btn flat label="Ver todo" @click="$router.push('/productos?novedades=1')" />
    </div>
  </div>
  <q-carousel
    v-model="novedadesSlide"
      animated
      arrows
      navigation
      navigation-position="bottom"
      infinite
      class="banner-carousel"
      height="66vh"
    >
    <q-carousel-slide v-for="(chunk, sidx) in novedadesSlides" :key="'nov-slide-'+sidx" :name="sidx" class="strip-slide">
      <div class="strip-slide-inner">
        <div v-for="p in chunk" :key="'amazon-nov-'+p.id" class="slide-item">
          <q-card flat bordered class="shadow-1 clickable-card" @click="goToProduct(p.id)">
            <q-img :src="getFotoUrl(getProductoImage(p))" ratio="4/3" style="object-fit: cover" @load="debugImg && debugResolveFoto(getProductoImage(p))" @error="debugImg && console.warn('[IndexPage] q-img error loading product', getProductoImage(p))">
              <template v-slot:after>
                <div class="card-badge">Nuevo</div>
              </template>
            </q-img>
            <q-card-section>
              <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.descripcion || p.nombre || 'Sin título' }}</div>
              <div class="text-subtitle1 text-weight-bold">$ {{ p.precioVenta != null ? Number(p.precioVenta).toLocaleString('es-ES', { minimumFractionDigits:2 }) : '0.00' }}</div>
            </q-card-section>
            <q-card-actions align="right">
              <q-btn dense round flat :icon="wishlist.isFavorito(p.id) ? 'favorite' : 'favorite_border'" color="primary" @click.stop="() => wishlist.toggle(p)">
                <q-tooltip>{{ wishlist.isFavorito(p.id) ? 'Quitar de favoritos' : 'Agregar a favoritos' }}</q-tooltip>
              </q-btn>
              <q-btn dense round flat icon="add_shopping_cart" color="primary" @click.stop="addToCart(p)">
                <q-tooltip>Añadir al carrito</q-tooltip>
              </q-btn>
            </q-card-actions>
          </q-card>
        </div>
      </div>
    </q-carousel-slide>
  </q-carousel>
</section>

<!-- Catálogo por Categorías (Amazon-like tiles) -->
<section class="q-px-lg q-py-xl">
  <div class="row items-center q-mb-md">
    <div class="col">
      <div class="text-h6 text-weight-bold">Catálogo por Categorías</div>
    </div>
    <div class="col-auto">
      <q-btn flat label="Ver categorías" @click="$router.push('/categorias')" />
    </div>
  </div>

     <q-carousel
      v-model="categoriasSlide"
      animated
      arrows
      navigation
      navigation-position="bottom"
      infinite
      class="banner-carousel"
      height="66vh"
    >
      <q-carousel-slide v-for="(chunk, sidx) in categoriasSlides" :key="'cat-slide-'+sidx" :name="sidx">
        <div class="strip-slide-inner strip-cats">
          <div v-for="cat in chunk" :key="'cat-'+cat.id" class="slide-item-cat">
            <q-card flat bordered class="shadow-1 cat-card-bg" :style="{ backgroundImage: 'url(' + getFotoUrl(getCategoriaImage(cat)) + ')' }" role="img" :aria-label="cat.nombre" @click="$router.push({ name: 'CategoriaProductos', params: { id: cat.id } })">
              <div class="cat-card-overlay"></div>
              <q-card-section class="cat-tile-label">{{ cat.nombre }}</q-card-section>
            </q-card>
          </div>
        </div>
      </q-carousel-slide>
    </q-carousel>
</section>

<!-- Rebajas (Amazon-like deals) -->
<section class="q-px-lg q-py-xl bg-rebajas">
  <div class="row items-center q-mb-md">
    <div class="col">
      <div class="text-h6 text-weight-bold">Rebajas</div>
    </div>
    <div class="col-auto">
      <q-btn flat label="Ver ofertas" @click="$router.push('/productos?rebajas=1')" />
    </div>
  </div>

  <div class="amazon-row">
    <div v-for="p in rebajas.slice(0,8)" :key="'rebaja-'+p.id" class="amazon-card">
      <q-card flat bordered class="shadow-1 clickable-card" @click="goToProduct(p.id)">
  <q-img :src="getFotoUrl(getProductoImage(p))" ratio="4/3" style="object-fit: cover" @load="debugImg && debugResolveFoto(getProductoImage(p))" @error="debugImg && console.warn('[IndexPage] q-img error loading product', getProductoImage(p))">
          <template v-slot:after>
            <div class="card-badge badge-sale">-{{ p.descuento || p.porcentajeDescuento || 10 }}%</div>
          </template>
        </q-img>
        <q-card-section>
          <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.descripcion || p.nombre || 'Sin título' }}</div>
          <div class="row items-center">
            <div class="text-subtitle1 text-weight-bold q-mr-sm">$ {{ p.precioVenta != null ? Number(p.precioVenta).toLocaleString('es-ES',{ minimumFractionDigits:2 }) : '0.00' }}</div>
            <div class="text-caption text-grey-6"><s>$ {{ p.precioOriginal ? Number(p.precioOriginal).toLocaleString('es-ES',{ minimumFractionDigits:2 }) : '' }}</s></div>
          </div>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn dense round flat :icon="wishlist.isFavorito(p.id) ? 'favorite' : 'favorite_border'" color="accent" @click.stop="() => wishlist.toggle(p)">
            <q-tooltip>{{ wishlist.isFavorito(p.id) ? 'Quitar de favoritos' : 'Agregar a favoritos' }}</q-tooltip>
          </q-btn>
          <q-btn dense round flat icon="add_shopping_cart" color="accent" @click.stop="addToCart(p)">
            <q-tooltip>Añadir al carrito</q-tooltip>
          </q-btn>
        </q-card-actions>
      </q-card>
    </div>
  </div>

  <q-carousel
      v-model="rebajasSlide"
      animated
      arrows
      navigation
      navigation-position="bottom"
      infinite
      class="banner-carousel"
      height="66vh"
    >
    <q-carousel-slide v-for="(chunk, sidx) in rebajasSlides" :key="'reb-slide-'+sidx" :name="sidx">
      <div class="strip-slide-inner">
        <div v-for="p in chunk" :key="'rebaja-'+p.id" class="slide-item">
          <q-card flat bordered class="shadow-1 clickable-card" @click="goToProduct(p.id)">
            <q-img :src="getFotoUrl(getProductoImage(p))" ratio="4/3" style="object-fit: cover" @load="debugImg && debugResolveFoto(getProductoImage(p))" @error="debugImg && console.warn('[IndexPage] q-img error loading product', getProductoImage(p))">
              <template v-slot:after>
                <div class="card-badge badge-sale">-{{ p.descuento || p.porcentajeDescuento || 10 }}%</div>
              </template>
            </q-img>
            <q-card-section>
              <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.descripcion || p.nombre || 'Sin título' }}</div>
              <div class="row items-center">
                <div class="text-subtitle1 text-weight-bold q-mr-sm">$ {{ p.precioVenta != null ? Number(p.precioVenta).toLocaleString('es-ES',{ minimumFractionDigits:2 }) : '0.00' }}</div>
                <div class="text-caption text-grey-6"><s>$ {{ p.precioOriginal ? Number(p.precioOriginal).toLocaleString('es-ES',{ minimumFractionDigits:2 }) : '' }}</s></div>
              </div>
            </q-card-section>
            <q-card-actions align="right">
              <q-btn dense round flat :icon="wishlist.isFavorito(p.id) ? 'favorite' : 'favorite_border'" color="accent" @click.stop="() => wishlist.toggle(p)">
                <q-tooltip>{{ wishlist.isFavorito(p.id) ? 'Quitar de favoritos' : 'Agregar a favoritos' }}</q-tooltip>
              </q-btn>
              <q-btn dense round flat icon="add_shopping_cart" color="accent" @click.stop="addToCart(p)">
                <q-tooltip>Añadir al carrito</q-tooltip>
              </q-btn>
            </q-card-actions>
          </q-card>
        </div>
      </div>
    </q-carousel-slide>
  </q-carousel>
</section>




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
import { onMounted, reactive, ref, computed, watch } from 'vue'
import { loadGet, loadGetDatosInicio } from './assets/js/util/funciones'
import DialogLoad from './components/DialogBoxes/DialogLoad.vue'
import { apiFotosBaseUrl } from './boot/axios'
import { EventBus } from './assets/js/util/eventBus'
import useCart from './stores/cartStore'
import { useWishlist } from './stores/wishlistStore'
import TopBar from 'src/pages/Visual/components/TopBar.vue'

const categoriaSlide = ref(0)
const bannerSlide = ref(0)
// Amazon-like banner slides (image + headline + subtitle + CTA)
const bannerSlides = [
  {
    image: '/img/sunrise-in-summer-beach-background-free-vector.jpg',
    title: 'Ofertas de Verano',
    subtitle: 'Los mejores precios en ropa y accesorios. Envío rápido.',
    ctaText: 'Comprar ahora',
    ctaLink: '/productos'
  },
  {
    image: '/img/MarcaZun-02.png',
    title: 'Nuevas Colecciones',
    subtitle: 'Descubre las novedades de la temporada.',
    ctaText: 'Ver colección',
    ctaLink: '/catalogo'
  },
  {
    image: '/img/ZUN_fta_negative.png',
    title: 'Promociones Exclusivas',
    subtitle: 'Aprovecha descuentos por tiempo limitado.',
    ctaText: 'Aprovechar',
    ctaLink: '/ofertas'
  }
]
const dialogLoad = ref(false)
const leftDrawer = ref(false)
const debugImg = true // set to true temporarily to print image debug info
const objetoInicial ={
productosNovedades:[],
categoriasProductos:[],
}
const objeto = reactive({ ...objetoInicial })

// placeholder array for rebajas — if backend provides descuento field use it
const rebajas = computed(() => {
  // try to pick products with descuento flag or price drop; fallback to productosNovedades
  const all = (objeto.productosNovedades || []).concat([])
  return all.filter(p => p.tieneDescuento || p.descuento || p.precioOriginal).map(p => ({ ...p }))
})

// carousel state and slides
const novedadesSlide = ref(0)
const rebajasSlide = ref(0)
const categoriasSlide = ref(0)

const itemsPerSlideProducts = 5
const itemsPerSlideCategories = 5

const novedadesSlides = computed(() => chunkCategorias((objeto.productosNovedades || []).slice(0, 20), itemsPerSlideProducts))
const rebajasSlides = computed(() => chunkCategorias((rebajas.value || []).slice(0, 20), itemsPerSlideProducts))
const categoriasSlides = computed(() => chunkCategorias((objeto.categoriasProductos || []), itemsPerSlideCategories))

// selected category id used as filter for search
const selectedCategory = ref(null)
const selectedCategory1 = ref(null)
const searchQuery = ref('')

const selectedCategoryLabel = computed(() => {
  if (!selectedCategory.value) return 'Categorías'
  const found = (objeto.categoriasProductos || []).find(c => c.id === selectedCategory.value)
  return found ? found.nombre : 'Categorías'
})
const selectedCategory1Label = computed(() => {
  if (!selectedCategory1.value) return 'Categorías'
  const found = (objeto.categoriasProductos || []).find(c => c.id === selectedCategory1.value)
  return found ? found.nombre : 'Categorías'
})

function performSearch() {
  // navigate to productos list with query and category as params
  const params = {}
  if (searchQuery.value) params.q = searchQuery.value
  if (selectedCategory.value) params.cat = selectedCategory.value
  router.push({ name: 'Productos', query: params }).catch(() => {
    // fallback path
    const qs = new URLSearchParams(params).toString()
    router.push('/productos' + (qs ? '?' + qs : '')).catch(() => {})
  })
}

// navigation helper used by dropdowns
import { useRouter } from 'vue-router'
const router = useRouter()
function navigateToCategory(categoryId) {
  if (!categoryId) return
  leftDrawer.value = false
  // prefer named route but fallback to path
  if (router && router.push) {
    router.push({ name: 'CategoriaProductos', params: { id: categoryId } }).catch(() => {
      router.push('/categoria/' + categoryId)
    })
  }
}

async function goToProduct(productOrId) {
  if (!productOrId) {
    if (debugImg) console.warn('[IndexPage] goToProduct called with empty value', productOrId)
    return
  }

  // accept either an id or a product object
  let id = productOrId
  if (typeof productOrId === 'object') {
    id = productOrId.id || productOrId.productoId || productOrId.productId || productOrId._id || productOrId.codigo || null
  }

  if (!id) {
    console.warn('[IndexPage] goToProduct: could not resolve product id from', productOrId)
    return
  }

  id = String(id)

  // prefer the visual product detail named route; fallback to path
  try {
    await router.push({ name: 'ProductoDetalle', params: { id } })
  } catch (err) {
    console.warn('[IndexPage] named route ProductoDetalle failed, falling back to path', err)
    try {
      await router.push('/producto/' + id)
    } catch (err2) {
      console.error('[IndexPage] navigation to product failed', err2)
    }
  }
}

function addToCart(product) {
  if (!product) return
  // emit a global event so any cart component can handle adding the item
  try {
    // prefer using cart store
    const cart = useCart()
    cart.addItem(product, 1)
    if (debugImg) console.log('[IndexPage] added to cart', product && (product.id || product.productoId))
  } catch (e) {
    console.warn('[IndexPage] addToCart error', e)
  }
}

const wishlist = useWishlist()

// options for q-btn-dropdown populated from objeto.categoriasProductos
const categoriaOptions = computed(() => {
  return (objeto.categoriasProductos || []).map(cat => ({
    label: cat.nombre || 'Sin nombre',
    // q-btn-dropdown supports `handler` per option
    handler: () => navigateToCategory(cat.id)
  }))
})

onMounted(async () => {
  dialogLoad.value = true
  let elementosInicio = await loadGetDatosInicio('ObtenerDatosInicio')
  // diagnostic dump to understand backend payload shape
  if (debugImg) console.log('[IndexPage] ObtenerDatosInicio payload:', elementosInicio)

  if (!elementosInicio) {
    console.warn('[IndexPage] ObtenerDatosInicio returned empty or failed', elementosInicio)
    objeto.productosNovedades = []
    objeto.categoriasProductos = []
  } else {
    // If API returned an array directly, try to locate product lists inside it
    if (Array.isArray(elementosInicio)) {
      if (elementosInicio.length === 0) {
        console.warn('[IndexPage] ObtenerDatosInicio returned an empty array')
      }
      // try to guess where products might be: look for objects that have precioVenta or descripcion
      const guessProducts = elementosInicio.filter(e => e && (e.precioVenta !== undefined || e.descripcion || e.nombre))
      if (guessProducts.length) {
        console.log('[IndexPage] guessed productosNovedades from array response, count:', guessProducts.length)
        objeto.productosNovedades = guessProducts
      }
    }

    // support a few possible response shapes (productosNovedades / productos / result.productosNovedades / data.productos)
    objeto.productosNovedades = objeto.productosNovedades.length
      ? objeto.productosNovedades
      : elementosInicio?.productosNovedades ?? elementosInicio?.productos ?? elementosInicio?.data?.productos ?? elementosInicio?.result?.productosNovedades ?? []

    objeto.categoriasProductos = elementosInicio?.categoriasProductos ?? elementosInicio?.categorias ?? elementosInicio?.data?.categorias ?? elementosInicio?.result?.categoriasProductos ?? []

    if (debugImg) console.log('[IndexPage] mapped productosNovedades length:', (objeto.productosNovedades || []).length, 'categoriasProductos length:', (objeto.categoriasProductos || []).length)
  }
  dialogLoad.value = false
})

// log categories when they arrive to help debug missing images
watch(() => objeto.categoriasProductos, (val) => {
  if (debugImg) console.log('[IndexPage] categoriasProductos loaded:', val)
}, { immediate: true })

// quick runtime test: try loading images via Image() to get onload/onerror events
function testImageLoad(url) {
  if (!debugImg || !url) return
  try {
    const img = new Image()
    img.onload = () => console.log('[IndexPage] image onload:', url)
    img.onerror = (e) => console.warn('[IndexPage] image onerror:', url, e)
    img.src = url
  } catch (e) {
    console.warn('[IndexPage] testImageLoad exception for', url, e)
  }
}

// when categories arrive, try loading the first few images to detect network/ssl/CORS issues
watch(() => objeto.categoriasProductos.length, (len) => {
  if (!debugImg) return
  const cats = objeto.categoriasProductos || []
  for (let i = 0; i < Math.min(3, cats.length); i++) {
    const cat = cats[i]
    const candidate = getCategoriaImage(cat)
    const resolved = getFotoUrl(candidate)
    console.log('[IndexPage] testing image', { id: cat.id, candidate, resolved })
    testImageLoad(resolved)
  }
}, { immediate: true })

function getFotoUrl(foto) {
  if (!foto) {
    return '/img/sin-foto.jpg'
  }
  // accept either a string URL/path or an object with common fields
  let candidate = foto
  if (typeof foto === 'object') {
    candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto || null
  }
  if (!candidate) return '/img/sin-foto.jpg'
  if (typeof candidate !== 'string') candidate = String(candidate)
  if (/^https?:\/\//.test(candidate)) return candidate
  // Normalize slashes to avoid double-slash issues
  const base = apiFotosBaseUrl || ''
  if (!base) return (candidate.startsWith('/') ? candidate : '/' + candidate)
  const sep = base.endsWith('/') ? '' : '/'
  const path = candidate.startsWith('/') ? candidate.replace(/^\//, '') : candidate
  return base + sep + path
}

// debug: log resolved foto URL when debugging enabled
function debugResolveFoto(foto) {
  if (!debugImg) return
  try {
    const resolved = getFotoUrl(foto)
    console.log('[IndexPage] getFotoUrl input:', foto, '=>', resolved)
  } catch (e) {
    console.warn('[IndexPage] getFotoUrl error for', foto, e)
  }
}


// helper to pick the image field from a category record (covers different backend keys)
function getCategoriaImage(cat) {
  if (!cat) return null
  const candidate = cat.fotoUrl || cat.imagen || cat.imagenUrl || cat.url || cat.image || cat.picture || null
  if (debugImg) console.log('[IndexPage] getCategoriaImage:', { id: cat.id, candidate })
  return candidate
}

// helper to pick the image field from a product record (covers different backend keys)
function getProductoImage(prod) {
  if (!prod) return null
  // First try direct fields
  let candidate = prod.fotoUrl || prod.imagen || prod.imagenUrl || prod.url || prod.image || prod.picture || null

  // If the product has an array of fotos, prefer the first valid entry
  if ((!candidate || candidate === '') && Array.isArray(prod.fotos) && prod.fotos.length > 0) {
    const first = prod.fotos[0]
    if (!first) candidate = null
    else if (typeof first === 'string') candidate = first
    else if (first.url) candidate = first.url
    else if (first.img) candidate = first.img
    else if (first.path) candidate = first.path
  }

  // Some APIs return an object like { imagen: '...'} at the root
  if ((!candidate || candidate === '') && prod.imagenPrincipal) {
    candidate = prod.imagenPrincipal
  }

  if (debugImg) console.log('[IndexPage] getProductoImage:', { id: prod.id, candidate })
  return candidate
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
.banner-slide { padding: 0 !important; }
.banner-bg {
  height: 66vh;
  width: 100%;
  background-size: cover;
  background-position: center center;
  position: relative;
  display: flex;
  align-items: center;
}
.banner-overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(90deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0.25) 40%, rgba(0,0,0,0) 100%);
}
.banner-content {
  position: relative;
  z-index: 2;
  color: #fff;
  max-width: 720px;
  padding: 24px 32px;
  margin-left: 6%;
}
.banner-title {
  font-size: 2.2rem;
  margin: 0 0 12px 0;
  font-weight: 700;
}
.banner-subtitle {
  font-size: 1.05rem;
  margin: 0 0 18px 0;
  color: rgba(255,255,255,0.9);
}
.banner-content .q-btn {
  background: #ffd54f; /* amazon-like yellow */
  color: #111;
}

/* Amazon-like section styles */
.amazon-row { display: flex; gap: 16px; overflow-x: auto; padding-bottom: 8px; }
.amazon-card { flex: 0 0 220px; width: 220px; }
.card-badge { position: absolute; top: 8px; left: 8px; background: rgba(0,0,0,0.6); color: #fff; padding: 4px 8px; border-radius: 4px; font-size: 12px; }
.badge-sale { background: #e53935 !important; }
.ellipsis { overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.cat-strip { display: flex; gap: 12px; overflow-x: auto; }
.cat-tile { width: 200px; cursor: pointer; }
.cat-card-bg { width: 100%; height: 120px; background-size: cover; background-position: center center; position: relative; display:flex; align-items:flex-end; border-radius:6px; overflow:hidden; }
.cat-card-bg .q-card__section { background: linear-gradient(180deg, rgba(0,0,0,0) 0%, rgba(0,0,0,0.45) 100%); color: #fff; }
.cat-card-overlay { position:absolute; inset:0; }
.cat-tile-label { text-align: center; padding: 8px 6px; font-weight: 600; z-index:2; }
.bg-rebajas { background: linear-gradient(180deg, #fff, #fff); }

@media (max-width: 768px) {
  .banner-bg { height: 50vh; }
  .banner-content { max-width: 90%; margin-left: 5%; padding: 16px; }
  .banner-title { font-size: 1.4rem; }
  .banner-subtitle { font-size: 0.95rem; }
}

</style>

<style lang="scss" scoped>
@import './css/quasar.variables.scss';
/* Carousel control colors using app primary */
.banner-carousel ::v-deep .q-carousel__control,
.banner-carousel ::v-deep .q-carousel__control .q-icon {
  color: $primary !important;
}
.banner-carousel ::v-deep .q-carousel__navigation .q-btn {
  color: $primary !important;
  background: transparent !important;
}
.banner-carousel ::v-deep .q-carousel__navigation .q-btn.q-btn--active {
  color: darken($primary, 12%) !important;
}

/* Override banner CTA and sale badge to brand colors */
.banner-carousel ::v-deep .banner-content .q-btn,
.banner-content .q-btn {
  background: $primary !important;
  color: #111 !important;
}
.badge-sale { background: $secondary !important; }

/* strip carousels (product/category rows) */
.strip-carousel {
  background: transparent;
}
.strip-slide { padding: 12px 20px; }
.strip-carousel ::v-deep .q-carousel__control { background: rgba(0,0,0,0.08) !important; }

/* mirror banner controls & navigation so strips look like hero */
.strip-carousel ::v-deep .q-carousel__control,
.strip-carousel ::v-deep .q-carousel__control .q-icon {
  color: $primary !important;
}
.strip-carousel ::v-deep .q-carousel__control {
  background: rgba(0,0,0,0.12) !important;
  width: 44px !important;
  height: 44px !important;
}
.strip-carousel ::v-deep .q-carousel__control .q-icon {
  font-size: 20px !important;
}
.strip-carousel ::v-deep .q-carousel__control:hover {
  transform: translateY(-2px);
}
.strip-carousel ::v-deep .q-carousel__navigation .q-btn {
  color: $primary !important;
  background: transparent !important;
}
.strip-carousel ::v-deep .q-carousel__navigation .q-btn.q-btn--active {
  color: darken($primary, 12%) !important;
}

/* layout for items inside strip slides to avoid overlap with controls */
.strip-slide-inner {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 20px;
  /* padding left/right to keep items away from carousel controls; padding-top to lower items */
  padding: 32px 72px 16px;
  box-sizing: border-box;
  /* center items vertically in the slide area */
  align-items: center;
}
.strip-cats { padding-top: 8px; padding-bottom: 8px; }
.slide-item { flex: 0 0 280px; max-width: 280px; }
.slide-item-cat { flex: 0 0 240px; max-width: 240px; }

/* 5-per-view layout for wide screens */
@media (min-width: 1366px) {
  .strip-slide-inner { padding: 32px 72px 16px; }
  .slide-item { flex: 0 0 calc((100% - 224px)/5); max-width: calc((100% - 224px)/5); }
  .slide-item-cat { flex: 0 0 calc((100% - 224px)/5); max-width: calc((100% - 224px)/5); }
}

/* increase card heights and ensure content fills card */
.slide-item .q-card, .slide-item-cat .q-card {
  display: flex;
  flex-direction: column;
  height: 380px; /* larger card */
}

.clickable-card { cursor: pointer; }
.slide-item .q-img, .slide-item-cat .q-img {
  height: 220px;
  min-height: 180px;
  object-fit: cover;
}
.slide-item .q-card-section { flex: 1 1 auto; }
.slide-item-cat .cat-card-bg { height: 200px; }

@media (max-width: 1024px) {
  .strip-slide-inner { padding: 0 56px; }
  .slide-item { flex: 0 0 220px; max-width: 220px; }
  .slide-item-cat { flex: 0 0 180px; max-width: 180px; }
  .slide-item .q-card, .slide-item-cat .q-card { height: 340px; }
  .slide-item .q-img { height: 200px; }
  .slide-item-cat .cat-card-bg { height: 160px; }
}

@media (max-width: 768px) {
  .strip-slide-inner { padding: 0 36px; }
  .slide-item { flex: 0 0 48%; max-width: 48%; }
  .slide-item-cat { flex: 0 0 46%; max-width: 46%; }
  .slide-item .q-card, .slide-item-cat .q-card { height: auto; }
  .slide-item .q-img, .slide-item-cat .q-img { height: 160px; }
}

@media (max-width: 480px) {
  .strip-slide-inner { padding: 0 20px; }
  .slide-item { flex: 0 0 100%; max-width: 100%; }
  .slide-item-cat { flex: 0 0 48%; max-width: 48%; }
  .slide-item .q-img, .slide-item-cat .q-img { height: 140px; }
}


</style>

