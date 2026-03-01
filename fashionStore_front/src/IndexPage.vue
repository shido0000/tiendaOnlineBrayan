
<template>
  <div class="q-pa-none  full-strip_background">
  <!-- centralized TopBar component -->
  <TopBar :categories="objeto.categoriasProductos" v-model:leftDrawer="leftDrawer" @update:searchCategory="selectedCategory = $event" />
    <!-- Banners de Promoción: full-viewport carousel -->
    <q-carousel v-if="bannersCargados.length>0"
      v-model="bannerSlide"
      animated
      navigation
      navigation-position="bottom"
      infinite
      class="banner-carousel"
      :height="getBannerHeight()"
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
            <q-btn class="q-mt-md" color="primary" unelevated @click="$router.push(slide.ctaLink)">{{ slide.ctaText }}</q-btn>
          </div>
        </div>
      </q-carousel-slide>
    </q-carousel>

<!-- Novedades (Amazon-like horizontal) -->
<section class="q-px-lg q-py-md amazon-section responsive-section">
  <div class="row items-center q-mb-md">
    <div class="col">
  <div class="text-h6 text-weight-bold ">
    Novedades
  </div>
</div>

    <div class="col-auto">
  <q-btn
    flat
    label="Ver todo"
    style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);"
    @click="$router.push('/productos?novedades=1')"
  />
</div>

  </div>
  <div class="horizontal-scroll-container">
    <div class="scroll-wrapper">
      <div v-for="p in novedadesSlides" :key="'amazon-nov-'+p.id" class="slide-item">
        <q-card bordered class="custom-shadow clickable-card clickable-card1" @click="goToProduct(p.id)">
          <q-img :src="getFotoUrl(getProductoImage(p))" ratio="4/3" style="object-fit: cover" @load="debugImg && debugResolveFoto(getProductoImage(p))" @error="debugImg">
            <template v-slot:after>
              <div class="card-badge">Nuevo</div>
            </template>
          </q-img>
          <q-card-section>
            <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.codigo || p.nombre || 'Sin título' }}</div>
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
  </div>
</section>

  <q-separator style="height: 3px;"/>

<!-- Catálogo por Categorías (Amazon-like tiles) -->
<section class="q-px-lg q-py-xl responsive-section">
  <div class="row items-center q-mb-md">
    <div class="col">
      <div class="text-h6 text-weight-bold">Categorías</div>
    </div>
    <div class="col-auto">
      <q-btn style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);" flat label="Ver todo" @click="$router.push('/categorias')" />
    </div>
  </div>

     <div class="horizontal-scroll-container">
       <div class="scroll-wrapper">
          <div v-for="cat in categoriasSlides" :key="'cat-'+cat.id" class="slide-item-cat">
            <q-card flat bordered class="custom-shadow cat-card-bg clickable-card1" :style="{ backgroundImage: 'url(' + getFotoUrl(getCategoriaImage(cat)) + ')' }" role="img" :aria-label="cat.nombre" @click="$router.push({ name: 'CategoriaProductos', params: { id: cat.id } })">
              <div class="cat-card-overlay"></div>
              <q-card-section class="cat-tile-label">{{ cat.nombre }}</q-card-section>
            </q-card>
          </div>
       </div>
     </div>
</section>

  <q-separator style="height: 3px;"/>
<!-- Rebajas (Amazon-like deals) -->
<section v-if="rebajasSlides.length > 0" class="q-px-lg q-py-md amazon-section responsive-section ">
<div class="row items-center q-mb-md  " >
  <div class="col">
    <div class="text-h6 text-weight-bold ">
      Ofertas
    </div>
  </div>

  <div class="col-auto">
    <q-btn
      flat
      label="Ver todo"
      style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);"
      @click="$router.push('/productos?rebajas=1')"
    />
  </div>
  </div>

  <div class="horizontal-scroll-container">
    <div class="scroll-wrapper">
      <div v-for="p in rebajasSlides" :key="'rebaja-'+p.id" class="slide-item">
        <q-card bordered class="custom-shadow clickable-card clickable-card1" @click="goToProduct(p.id)">
          <q-img :src="getFotoUrl(getProductoImage(p))" ratio="4/3" style="object-fit: cover" @load="debugImg && debugResolveFoto(getProductoImage(p))" @error="debugImg">
            <template v-slot:after>
              <div class="card-badge badge-sale">-{{ p.descuento || p.porcentajeDescuento || 10 }}%</div>
            </template>
          </q-img>
          <q-card-section>
            <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.codigo || p.nombre || 'Sin título' }}</div>
            <div v-if="p.tieneDescuento" class="row items-center">
              <!-- Precio original tachado -->
              <div class="text-subtitle1 text-grey-6 q-mr-sm">
                <s>$ {{ formatPrice(p.precioVenta) }}</s>
              </div>
              <!-- Precio con descuento -->
              <div class="text-subtitle1 text-weight-bold text-primary">
                $ {{ formatPrice(p.precioVentaDescuento) }}
              </div>
            </div>
            <div v-else class="text-subtitle1 text-weight-bold">
              $ {{ formatPrice(p.precioVenta) }}
            </div>
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
  </div>
</section>




    <!-- Footer -->
<div class="footer q-pt-xl q-pb-md bg-primary text-black">
  <!-- Links -->
  <div class="footer-links q-mb-lg">
    <router-link to="/" class="footer-link">Inicio</router-link>
    <router-link to="/informacion#sobre-nosotros" class="footer-link">Sobre Nosotros</router-link>
    <router-link to="/informacion#privacidad" class="footer-link">Política de Privacidad</router-link>
    <router-link to="/informacion#terminos" class="footer-link">Términos y Condiciones</router-link>
    <router-link to="/informacion#devoluciones" class="footer-link">Devoluciones y Cambios</router-link>
    <router-link to="/informacion#colabora" class="footer-link">Colabora con Nosotros</router-link>
  </div>

  <!-- Contacto -->
<div class="footer-contact q-mb-lg text-caption flex flex-center column">
  <!-- Dirección -->
  <div class="row items-center q-mb-sm justify-center">
    <q-icon name="place" size="sm" color="black" class="q-mr-sm" />
    <span class="text-weight-bold q-mr-sm">Dirección:</span>
    <span class="text-body2">{{ info.direccionTienda }}</span>
  </div>

  <!-- Horario -->
  <div class="row items-center q-mb-sm justify-center">
    <q-icon name="schedule" size="sm" color="black" class="q-mr-sm" />
    <span class="text-weight-bold q-mr-sm">Horario:</span>
    <span class="text-body2">{{ info.horarioTienda }}</span>
  </div>

  <!-- Teléfono -->
  <div class="row items-center q-mb-sm justify-center">
    <q-icon name="phone" size="sm" color="black" class="q-mr-sm" />
    <span class="text-weight-bold q-mr-sm">Teléfono:</span>
    <a :href="`tel:${info.telefonoTienda}`" class="footer-link">
      {{ info.telefonoTienda }}
    </a>
  </div>
</div>


  <!-- Redes sociales -->
  <div class="footer-social q-mb-lg">
  <q-btn flat round :icon="'telegram'" color="black" class="footer-social-btn" @click="abrirEnlace(info.enlaceTelegram,2)"/>
  <q-btn flat round :icon="'mdi-facebook'" color="black" class="footer-social-btn" @click="abrirEnlace(info.enlaceFacebook,4)"/>
  <q-btn flat round :icon="'mdi-whatsapp'" color="black" class="footer-social-btn" @click="abrirEnlace(info.enlaceWhatsapp,1)"/>
  <q-btn flat round :icon="'mdi-instagram'" color="black" class="footer-social-btn" @click="abrirEnlace(info.enlaceInstagram,3)"/>
</div>


  <!-- Copyright -->
  <div class="text-caption">© 2025. Todos los derechos reservados.</div>
</div>


      <DialogLoad :dialogLoad="dialogLoad" />
    </div>

  </template>



<script setup>
import { onMounted, reactive, ref, computed, watch } from 'vue'
import { useQuasar } from 'quasar'
import { loadGet, loadGetDatosInicio, getFotoFromVarianteWithFallback } from './assets/js/util/funciones'
import DialogLoad from './components/DialogBoxes/DialogLoad.vue'
import { apiFotosBaseUrl } from './boot/axios'
import { EventBus } from './assets/js/util/eventBus'
import useCart from './stores/cartStore'
import { useWishlist } from './stores/wishlistStore'
import TopBar from 'src/pages/Visual/components/TopBar.vue'

const $q = useQuasar()

const categoriaSlide = ref(0)
const bannerSlide = ref(0)
const items = ref([])
const infoInicial = {
  sobreNosotros: '',
  privacidad: '',
  terminos: '',
  devoluciones: '',
  colabora: ''
}
const info = reactive({ ...infoInicial })

const objetoBanner=ref({
image:'',
title:'',
subtitle:'',
ctaText:'',
ctaLink:'',

})

const bannersCargados=ref([])
// Amazon-like banner slides (image + headline + subtitle + CTA)
const bannerSlides = ref([])
/*const bannerSlides = [
  {
    image: '/img/sunrise-in-summer-beach-background-free-vector.jpg',
    title: 'Productos',
    subtitle: 'Los mejores precios en ropa y accesorios. Envío rápido.',
    ctaText: 'Comprar ahora',
    ctaLink: '/productos'
  },
  {
    image: '/img/nuevasColecciones.webp',
    title: 'Nuevas Colecciones',
    subtitle: 'Descubre las novedades de la temporada.',
    ctaText: 'Ver colección',
    ctaLink: '/productos?novedades=1'
  },
  {
    image: '/img/rebajas.jpg',
    title: 'Promociones Exclusivas',
    subtitle: 'Aprovecha descuentos por tiempo limitado.',
    ctaText: 'Aprovechar',
    ctaLink: '/productos?rebajas=1'
  }
]*/
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

// Función auxiliar para obtener stock de un producto
function getProductoStock(p) {
  let stock = p.stock || p.cantidadDisponible || p.stockTotal || 0
  if (!stock && Array.isArray(p.productoVariantes) && p.productoVariantes.length > 0) {
    stock = p.productoVariantes[0].stock || 0
  }
  if (!stock && Array.isArray(p.variants) && p.variants.length > 0) {
    stock = p.variants[0].stock || 0
  }
  return stock
}

 //const novedadesSlides = computed(() => chunkCategorias(((objeto.productosNovedades || []).filter(p => getProductoStock(p) > 0).slice(0, 20)), itemsPerSlideProducts))

// Computed properties para carruseles - sin chunks, items directos
const novedadesSlides = computed(() => {
  return objeto.productosNovedades || []
})

const rebajasSlides = computed(() => {
  return (rebajas.value || []).filter(p => getProductoStock(p) > 0).slice(0, 20)
})

const categoriasSlides = computed(() => {
  return objeto.categoriasProductos || []
})

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
import { useInicio } from './assets/js/util/useInicio'
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
    // Normalizar el producto antes de agregarlo
    const productoNormalizado = normalizarProductoParaCarrito(product)
    cart.addItem(productoNormalizado, 1)
  } catch (e) {
    console.warn('[IndexPage] addToCart error', e)
  }
}

// Normalizar producto antes de agregarlo al carrito
function normalizarProductoParaCarrito(producto) {
  if (!producto) return producto

  // Normalizar variantes exactamente como en ProductoDetallePage
  const variants = (producto.productoVariantes || producto.variants || []).map(v => ({
    id: v.id,
    productoId: v.productoId,
    talla: v.talla,
    color: v.color,
    stock: v.stock,
    principal: v.principal,
    otrasVariantesIds: v.otrasVariantesIds || [],
    // Mapear fotos con la estructura correcta
    fotos: (v.fotos || []).map(f => {
      if (typeof f === 'string') {
        // Si es string, asumimos que es una URL
        return { id: null, url: f, descripcion: '', esPrincipal: false, orden: 0 }
      } else {
        // Si es objeto, mapear con los campos disponibles
        return {
          id: f.id || null,
          url: f.url || f.imagen || f.path || '',
          descripcion: f.descripcion || '',
          esPrincipal: f.esPrincipal || false,
          orden: f.orden || 0
        }
      }
    }),
    slide: 1 // índice inicial del carrusel de esta variante
  }))

  // Obtener ID de variante (usar la primera por defecto, preferentemente la principal)
  const varianteId = variants.find(v => v.principal === true || v.principal === 'true')?.id ||
                     (variants.length > 0 ? variants[0].id : null)

  // Mapear fotos del producto principal con la estructura correcta
  const fotos = (producto.fotos || []).map(f => {
    if (typeof f === 'string') {
      return { id: null, url: f, descripcion: '', esPrincipal: false, orden: 0 }
    } else {
      return {
        id: f.id || null,
        url: f.url || f.imagen || f.path || '',
        descripcion: f.descripcion || '',
        esPrincipal: f.esPrincipal || false,
        orden: f.orden || 0
      }
    }
  })

  // Estructura normalizada idéntica a ProductoDetallePage
  const normalizado = {
    id: producto.id,
    codigo: producto.codigo,
    descripcion: producto.descripcion || producto.nombre,
    esActivo: producto.esActivo,
    sku: producto.sku,
    precioCosto: producto.precioCosto,
    precioVenta: producto.precioVenta || producto.precio,
    monedaCostoId: producto.monedaCostoId,
    monedaVentaId: producto.monedaVentaId,
    categoriasIds: producto.categoriasIds || [],
    categoriasDescripcion: producto.categoriasDescripcion,
    varianteId: varianteId, // ID consistente para identificar en carrito
    fotos: fotos,
    variants: variants
  }

  return normalizado
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
   bannersCargados.value = await loadGet('BannerPromocion/ObtenerListadoPaginado?activo=true')??[]

  bannersCargados.value.forEach(element => {
  const banner = {
    image: element.imagen,
    title: element.textoTitulo,
    subtitle: element.textoSubtitulo,
    ctaText: element.textoBoton,
    ctaLink: element.rebajas ? '/productos?rebajas=1':`categorias/${element.categoriaProductoId}`
  }
  bannerSlides.value.push(banner)
})


  // diagnostic dump to understand backend payload shape

  if (!elementosInicio) {
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
        objeto.productosNovedades = guessProducts
      }
    }

    // support a few possible response shapes (productosNovedades / productos / result.productosNovedades / data.productos)
    objeto.productosNovedades = objeto.productosNovedades.length
      ? objeto.productosNovedades
      : elementosInicio?.productosNovedades ?? elementosInicio?.productos ?? elementosInicio?.data?.productos ?? elementosInicio?.result?.productosNovedades ?? []

    objeto.categoriasProductos = elementosInicio?.categoriasProductos ?? elementosInicio?.categorias ?? elementosInicio?.data?.categorias ?? elementosInicio?.result?.categoriasProductos ?? []


    // Enriquecer novedades con datos completos para obtener fotos desde productoVariantes
    if (objeto.productosNovedades && objeto.productosNovedades.length > 0) {
      try {
        const { api } = await import('src/boot/axios')
        const novedadesEnriquecidas = []

        for (const prod of objeto.productosNovedades) {
          try {
            const response = await api.get(`Producto/ObtenerProductoEspecifico/${prod.id}`)
            if (response?.data) {
              // Extraer datos: pueden estar en response.data.result o directamente en response.data
              const productoCompleto = response.data.result || response.data
              // Mezclar con datos originales, manteniendo IDs y precio
              const merged = { ...prod, ...productoCompleto }
              novedadesEnriquecidas.push(merged)
            } else {
              novedadesEnriquecidas.push(prod)
            }
          } catch (err) {
            novedadesEnriquecidas.push(prod)
          }
        }

        objeto.productosNovedades = novedadesEnriquecidas

      } catch (err) {
        console.warn('[IndexPage] error enriching novedades:', err)
      }
    }
  }
  items.value = await loadGet('InformacionGeneral/ObtenerListadoPaginado') ?? []
  Object.assign(info, items.value[0])

  dialogLoad.value = false
})

// log categories when they arrive to help debug missing images
watch(() => objeto.categoriasProductos, (val) => {
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
  } catch (e) {
    console.warn('[IndexPage] getFotoUrl error for', foto, e)
  }
}


// helper to pick the image field from a category record (covers different backend keys)
function getCategoriaImage(cat) {
  if (!cat) return null
  const candidate = cat.fotoUrl || cat.imagen || cat.imagenUrl || cat.url || cat.image || cat.picture || null
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

  // Check productoVariantes array for photos
  if ((!candidate || candidate === '') && Array.isArray(prod.productoVariantes) && prod.productoVariantes.length > 0) {
    const firstVariant = prod.productoVariantes[0]
    if (firstVariant && Array.isArray(firstVariant.fotos) && firstVariant.fotos.length > 0) {
      const firstFoto = firstVariant.fotos[0]
      if (typeof firstFoto === 'string') candidate = firstFoto
      else if (firstFoto?.url) candidate = firstFoto.url
      else if (firstFoto?.img) candidate = firstFoto.img
      else if (firstFoto?.path) candidate = firstFoto.path
    } else if (firstVariant) {
      // Si la variante no tiene fotos, busca en otras variantes del mismo producto
      const fotoHeredada = getFotoFromVarianteWithFallback(firstVariant, prod)
      if (fotoHeredada) candidate = fotoHeredada
    }
  }

  // Some APIs return an object like { imagen: '...'} at the root
  if ((!candidate || candidate === '') && prod.imagenPrincipal) {
    candidate = prod.imagenPrincipal
  }

  return candidate
}

// Función para calcular precio con descuento
function calculateDiscountedPrice(product) {
  if (!product || product.precioVenta == null) return '0.00'

  const precio = Number(product.precioVenta)
  const descuento = product.descuento || product.porcentajeDescuento || 10
  const precioConDescuento = precio * (1 - descuento / 100)

  return precioConDescuento.toLocaleString('es-ES', { minimumFractionDigits: 2 })
}

// Función para formatear precio (igual a ProductosPage)
function formatPrice(v) {
  if (v == null) return '0.00'
  return Number(v).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
}

// Funciones para calcular alturas responsivas
function getBannerHeight() {
  if ($q.screen.xs) return '50vh'
  if ($q.screen.sm) return '55vh'
  if ($q.screen.md) return '60vh'
  return '66vh'
}

function getNovedadesHeight() {
  if ($q.screen.xs) return '45vh'
  if ($q.screen.sm) return '50vh'
  if ($q.screen.md) return '55vh'
  return '60vh'
}

function getRebajasHeight() {
  if ($q.screen.xs) return '50vh'
  if ($q.screen.sm) return '50vh'
  if ($q.screen.md) return '55vh'
  return '60vh'
}

function getCategoriesHeight() {
  if ($q.screen.xs) return '30vh'
  if ($q.screen.sm) return '10vh'
  if ($q.screen.md) return '10vh'
  return '38vh'
}

const abrirEnlace = (url, red) => {
    /*
    1-Whatsapp
    2-Telegram
    3-Instagram
    4-Facebook
    */
    let nuevaUrl=''
    if(red===1){
        nuevaUrl='https://wa.me/'+url
    }
    else if(red===2){
        nuevaUrl='https://t.me/'+url
    }
    else if(red===3){
        nuevaUrl='https://www.instagram.com/'+url
    }
    else if(red===4){
        nuevaUrl='https://www.facebook.com/'+url
    }
    window.open(nuevaUrl, '_blank') // abre en nueva pestaña
 }
</script>

<style lang="scss" scoped>
.banner-slide { padding: 0 !important; }
/*.banner-bg {
  min-height: 50vh;
  width: 100%;
  background-size: cover;
  background-position: center center;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: flex-start;
}*/
.banner-bg {
  min-height: 50vh;
  width: 100%;
  background-size: cover;
  background-position: center center;
  background-repeat: no-repeat;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: flex-start;
}

/*.banner-bg {
  width: 100%;
  height: 66vh;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  display: flex;
  flex-direction: column;
  justify-content: center;
}*/

.banner-overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(90deg, rgba(0,0,0,0.70) 0%, rgba(0,0,0,0.70) 40%, rgba(0,0,0,0.7) 100%);
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
  line-height: 1.2;
}
.banner-subtitle {
  font-size: 1.05rem;
  margin: 0 0 18px 0;
  color: #fff;
  line-height: 1.4;
}
.banner-content .q-btn {
  background: #ffd54f;
  color: #111;
  font-weight: 600;
}

/* Amazon-like section styles */
.amazon-row {
  display: grid;
  gap: 16px;
  grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
  padding-bottom: 8px;
}
.amazon-card { width: 100%; }
.card-badge {
  position: absolute;
  top: 8px;
  left: 8px;
  background: rgba(0,0,0,0.6);
  color: #fff;
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 12px;
  z-index: 2;
}
.badge-sale { background: #e53935 !important; }
.ellipsis { overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.cat-strip { display: flex; gap: 12px; overflow-x: auto; }
.cat-tile { width: 200px; cursor: pointer; }
.cat-card-bg {
  width: 100%;
  min-height: 120px;
  background-size: cover;
  background-position: center center;
  position: relative;
  display: flex;
  align-items: flex-end;
  border-radius: 6px;
  overflow: hidden;
}
.cat-card-bg .q-card__section {
  background: linear-gradient(180deg, rgba(0,0,0,0) 0%, rgba(0,0,0,0.45) 100%);
  color: #fff;
  width: 100%;
}
.cat-card-overlay { position: absolute; inset: 0; }
.cat-tile-label { text-align: center; padding: 8px 6px; font-weight: 600; z-index: 2; }

/* Extra small devices (xs) */
@media (max-width: 599px) {
  .banner-bg {
    min-height: 50vh;
    justify-content: flex-start;
  }
  .banner-content {
    max-width: 100%;
    margin-left: 0;
    padding: 12px 16px;
  }
  .banner-title {
    font-size: 1.4rem;
    margin-bottom: 8px;
  }
  .banner-subtitle {
    font-size: 0.9rem;
    margin-bottom: 12px;
  }
  .banner-content .q-btn {
    font-size: 0.85rem;
    padding: 8px 16px;
  }
  .amazon-row {
    gap: 12px;
    grid-template-columns: repeat(auto-fill, minmax(160px, 1fr));
  }
  .amazon-card { width: 100%; }
  .card-badge {
    font-size: 11px;
    padding: 3px 6px;
  }

  .slide-item { flex: 1 1 100%; }
}

/* Small devices (sm) */
@media (min-width: 600px) and (max-width: 1023px) {
  .banner-bg { min-height: 55vh; }
  .banner-content {
    max-width: 90%;
    margin-left: 5%;
    padding: 18px 24px;
  }
  .banner-title {
    font-size: 1.8rem;
    margin-bottom: 10px;
  }
  .banner-subtitle {
    font-size: 0.95rem;
    margin-bottom: 14px;
  }
  .amazon-row {
    gap: 14px;
    grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
  }

  .slide-item { flex: 1 1 calc(50% - 16px); }
}

/* Medium devices (md) */
@media (min-width: 1024px) and (max-width: 1365px) {
  .banner-bg { min-height: 60vh; }
  .amazon-row {
    gap: 15px;
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  }

  .slide-item { flex: 1 1 calc(33.333% - 16px); }
}

/* Large devices (lg+) */
@media (min-width: 1366px) {
  .banner-bg { min-height: 66vh; }
  .amazon-row {
    gap: 16px;
    grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
  }
}

/* Responsive sections */
.responsive-section {
  padding-left: 16px !important;
  padding-right: 16px !important;
  padding-top: 16px !important;
  padding-bottom: 16px !important;
}

@media (min-width: 600px) {
  .responsive-section {
    padding-left: 24px !important;
    padding-right: 24px !important;
    padding-top: 24px !important;
    padding-bottom: 24px !important;
  }
}

@media (min-width: 1024px) {
  .responsive-section {
    padding-left: 32px !important;
    padding-right: 32px !important;
    padding-top: 32px !important;
    padding-bottom: 32px !important;
  }
}

@media (min-width: 1366px) {
  .responsive-section {
    padding-left: 48px !important;
    padding-right: 48px !important;
    padding-top: 48px !important;
    padding-bottom: 48px !important;
  }
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

/* Contenedor con scroll horizontal */
.horizontal-scroll-container {
  width: 100%;
  overflow: hidden;
  padding: 16px 0;
}

.scroll-wrapper {
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  gap: 20px;
  padding: 16px 24px;
  overflow-x: auto;
  overflow-y: hidden;
  scroll-behavior: smooth;
  -webkit-overflow-scrolling: touch;
  scroll-snap-type: x mandatory;
}

.scroll-wrapper::-webkit-scrollbar {
  display: none;
}

.scroll-wrapper {
  -ms-overflow-style: none;  /* IE and Edge */
  scrollbar-width: none;     /* Firefox */
}

/* strip carousels (product/category rows) */
.strip-carousel {
  background: transparent;
}
.strip-slide {
  padding: 0px 10px;
  overflow: hidden;
}
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

.slide-item {
  flex: 0 0 auto;
  scroll-snap-align: start;
  width: 220px;
}

.slide-item-cat {
  flex: 0 0 auto;
  scroll-snap-align: start;
  width: 240px;
}


/* Extra small devices (xs) */
@media (max-width: 599px) {
  .scroll-wrapper {
    flex-direction: row;
    padding: 12px 16px;
    gap: 12px;
  }
  .slide-item {
    flex: 0 0 auto;
    width: calc(50vw - 18px);
    max-width: calc(50vw - 18px);
    min-height: auto;
  }
  .slide-item-cat {
    flex: 0 0 auto;
    width: calc(50vw - 18px);
    max-width: calc(50vw - 18px);
  }
  .slide-item .q-card,
  .slide-item-cat .q-card {
    height: auto;
    min-height: 280px;
  }
  .slide-item .q-img,
  .slide-item-cat .q-img {
    height: 140px;
    min-height: 120px;
  }
  .slide-item-cat .cat-card-bg {
    height: 150px;
  }
}

/* Small devices (sm) */
@media (min-width: 600px) and (max-width: 1023px) {
  .scroll-wrapper {
    flex-direction: row;
    padding: 20px 24px;
    gap: 16px;
  }
  .slide-item {
    flex: 0 0 auto;
    width: 240px;
    max-width: 240px;
  }
  .slide-item-cat {
    flex: 0 0 auto;
    width: 260px;
    max-width: 260px;
  }
  .slide-item .q-card,
  .slide-item-cat .q-card {
    height: auto;
    min-height: 320px;
  }
  .slide-item .q-img,
  .slide-item-cat .q-img {
    height: 160px;
  }
  .slide-item-cat .cat-card-bg {
    height: 160px;
  }
}

/* Medium devices (md) */
@media (min-width: 1024px) and (max-width: 1365px) {
  .scroll-wrapper {
    flex-direction: row;
    padding: 24px 32px;
    gap: 18px;
  }
  .slide-item {
    flex: 0 0 auto;
    width: 240px;
    max-width: 240px;
  }
  .slide-item-cat {
    flex: 0 0 auto;
    width: 280px;
    max-width: 280px;
  }
  .slide-item .q-card,
  .slide-item-cat .q-card {
    height: auto;
    min-height: 340px;
  }
  .slide-item .q-img,
  .slide-item-cat .q-img {
    height: 200px;
  }
  .slide-item-cat .cat-card-bg {
    height: 180px;
  }
}

/* Large devices (lg+) */
@media (min-width: 1366px) {
  .scroll-wrapper {
    flex-direction: row;
    padding: 32px 48px 16px;
    gap: 24px;
  }
  .slide-item {
    flex: 0 0 auto;
    width: 260px;
    max-width: 260px;
  }
  .slide-item-cat {
    flex: 0 0 auto;
    width: 300px;
    max-width: 300px;
  }
  .slide-item .q-card,
  .slide-item-cat .q-card {
    display: flex;
    flex-direction: column;
    height: 380px;
  }
  .slide-item .q-img,
  .slide-item-cat .q-img {
    height: 220px;
    min-height: 180px;
  }
  .slide-item-cat .cat-card-bg {
    height: 200px;
  }
}

.slide-item .q-card-section { flex: 1 1 auto; }
.clickable-card { cursor: pointer; }

/* Footer responsive */
.footer {
  text-align: center;
  padding-left: 16px;
  padding-right: 16px;
  box-sizing: border-box;
}

.footer-links {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 0.8rem;
  margin-bottom: 1.5rem;
}

.footer-link {
  color: #000000;
  font-weight: 500;
  text-decoration: none;
  transition: color 0.3s ease;
  font-size: 0.9rem;
}

.footer-link:hover {
  color: #ffeb3b;
}

.footer-contact {
  max-width: 700px;
  margin: 0 auto 1.5rem;
  line-height: 1.6;
  font-size: 0.85rem;
}

.footer-social {
  display: flex;
  justify-content: center;
  gap: 0.8rem;
  margin-bottom: 1rem;
  flex-wrap: wrap;
}

.footer-social-btn {
  transition: transform 0.2s ease;
}

.footer-social-btn:hover {
  transform: scale(1.2);
}

/* Extra small footer adjustments */
@media (max-width: 599px) {
  .footer-links {
    gap: 0.6rem;
    margin-bottom: 1rem;
  }
  .footer-link {
    font-size: 0.8rem;
  }
  .footer-contact {
    font-size: 0.75rem;
    margin-bottom: 1rem;
  }
  .footer-social-btn {
    transform: scale(0.9);
  }
}

/* Small footer adjustments */
@media (min-width: 600px) and (max-width: 1023px) {
  .footer-links {
    gap: 0.9rem;
  }
  .footer-link {
    font-size: 0.85rem;
  }
}


.custom-shadow {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}

.clickable-card1 {
  cursor: pointer;
  transition: transform 0.2s ease, box-shadow 0.2s ease;

  &:hover {
    transform: translateY(-4px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.4) !important;
  }
}


.padding_card {
  margin-left: 30px;
}


.full-strip_background {
  background: linear-gradient(90deg, #EDE3FF, #ffffff);
}
</style>

