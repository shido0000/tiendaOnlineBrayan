<template>
  <div class="q-pa-none bg-grey-1">
    <TopBar :categories="categories" />

    <div class="q-pa-lg bg-grey-1 row filters-products-container">
      <!-- Sidebar filters -->
      <div class="col-12 col-md-3 q-pr-md filters-sidebar">
        <q-card class="q-pa-md">
          <div class="text-h6 q-mb-md">Filtros</div>

          <div class="q-mb-md">
            <div class="text-subtitle2 q-mb-xs">Buscar</div>
            <q-input v-model="filters.q" dense placeholder="Buscar por nombre o código" @keyup.enter="applyFilters" />
          </div>

          <div class="q-mb-md">
          </div>

          <div class="q-mb-md">
            <div class="text-subtitle2 q-mb-xs">Precio</div>
            <div class="row q-gutter-sm">
              <div class="col">
                <q-input v-model.number="filters.precioMin" type="number" dense placeholder="Mín" />
              </div>
              <div class="col">
                <q-input v-model.number="filters.precioMax" type="number" dense placeholder="Máx" />
              </div>
            </div>
          </div>

          <div class="q-mb-md">
            <q-checkbox v-model="filters.novedades" label="Novedades" dense />
            <q-checkbox class="q-ml-sm" v-model="filters.rebajas" label="Ofertas" dense />
          </div>

          <div class="row justify-end q-mt-md">
            <q-btn flat label="Limpiar" @click="resetFilters" />
            <q-btn color="primary" label="Aplicar" @click="applyFilters" />
          </div>
        </q-card>
      </div>

      <!-- Products grid -->
      <div class="col-12 col-md-9 products-section">
        <div class="row items-center q-mb-sm">
          <div class="col">
            <div class="text-h6">Productos</div>
            <div class="text-caption text-grey-6">Mostrando {{ productos.length }} productos</div>
          </div>
        </div>

          <div v-if="!productos.length" class="q-pa-xl flex flex-center">
            <div class="text-center">
              <div class="text-h6">No se encontraron productos</div>
              <div class="q-mt-md">
                <q-btn label="Reintentar" color="primary" @click="loadProducts(true)" />
              </div>
            </div>
          </div>

          <div v-else class="row q-col-gutter-md">
            <div v-for="p in productos" :key="p.id || p._id || p.productoId" class="col-12 col-sm-6 col-md-4 col-lg-3">
              <q-card class="q-pa-md shadow-2 clickable-card" @click="goToProduct(p)">
              <q-img
                  class="product-img"
                  :src="getFotoUrl(getProductoImagePreferVariant(p) || getProductoImage(p))"
                  ratio="4/3"
                  spinner-color="primary"
                  @error="() => onImgError(p)"
                >
                <template v-slot:error>
                  <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
                </template>
              </q-img>
             <q-card-section>
  <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">
    {{ p.codigo || p.nombre || 'Sin título' }}
  </div>

  <!-- Mostrar precio normal o con descuento -->
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
                <q-btn dense round flat color="primary" :icon="wishlist.isFavorito(p.id) ? 'favorite' : 'favorite_border'" @click.stop="() => wishlist.toggle(normalizarProductoParaCarrito(p, getProductoImagePreferVariant(p) || getProductoImage(p)))" />
                <q-btn dense round flat icon="add_shopping_cart" color="primary" @click.stop="() => cart.addItem(normalizarProductoParaCarrito(p, getProductoImagePreferVariant(p) || getProductoImage(p)),1)" />
              </q-card-actions>
            </q-card>
          </div>
        </div>

        <div class="q-mt-md row justify-center">
          <q-btn flat label="Cargar más" v-if="canLoadMore" @click="loadMore" />
        </div>

        <DialogLoad :dialogLoad="dialogLoad" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import TopBar from 'src/pages/Visual/components/TopBar.vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import { loadGetHastaData, loadGetDatosInicio, loadGet, getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'
import { apiFotosBaseUrl } from 'src/boot/axios'
import { useWishlist } from 'src/stores/wishlistStore'
import useCart from 'src/stores/cartStore'

const router = useRouter()
const route = useRoute()
const dialogLoad = ref(false)
const productos = ref([])
const debugImg = true
const page = ref(1)
const pageSize = ref(24)
const total = ref(null)
const canLoadMore = computed(() => total.value == null || productos.value.length < total.value)

const wishlist = useWishlist()
const cart = useCart()

const categories = ref([])
const inventoryIndex = ref(null) // { ids: Set, codes: Set }

const filters = reactive({
  q: route.query.q || '',
  // normalize incoming route.query.cat into an array when appropriate
  // category filter removed (start from 0)
  precioMin: route.query.precioMin ? Number(route.query.precioMin) : null,
  precioMax: route.query.precioMax ? Number(route.query.precioMax) : null,
  novedades: route.query.novedades === '1' || !!route.query.novedades,
  rebajas: route.query.rebajas === '1' || !!route.query.rebajas,
})

// normalizedCategories removed; category filtering will be reimplemented from scratch

onMounted(async () => {
  dialogLoad.value = true
  try {
    // load site-wide categories for filter sidebar
    try {
      const inicio = await loadGetDatosInicio('ObtenerDatosInicio')
      categories.value = inicio?.categoriasProductos ?? inicio?.categorias ?? inicio?.data?.categorias ?? inicio?.result?.categoriasProductos ?? []
    } catch (e) {
      categories.value = []
    }
    await loadProducts(true)
  } finally {
    dialogLoad.value = false
  }
})

// keep filters synced when route.query changes (e.g. user navigates directly or uses links)
watch(() => route.query, async( q) => {
  try {
    filters.q = q.q || ''
    filters.precioMin = q.precioMin ? Number(q.precioMin) : null
    filters.precioMax = q.precioMax ? Number(q.precioMax) : null
    filters.novedades = q.novedades === '1' || !!q.novedades
    filters.rebajas = q.rebajas === '1' || !!q.rebajas
  } catch (err) {
    if (debugImg) console.warn('[ProductosPage] error syncing route.query to filters', err)
  }
  await loadProducts(true)
}, { deep: true })

async function ensureInventoryIndex() {
  if (inventoryIndex.value) return inventoryIndex.value
  try {
    // use loadGetHastaData to try to obtain the raw list (handles several payload shapes)
    const invRaw = await loadGetHastaData('Inventario/ObtenerListadoPaginado')
    // normalize to an array
    let inv = []
    if (Array.isArray(invRaw)) inv = invRaw
    else if (Array.isArray(invRaw?.result?.elementos)) inv = invRaw.result.elementos
    else if (Array.isArray(invRaw?.data?.result?.elementos)) inv = invRaw.data.result.elementos
    else if (Array.isArray(invRaw?.elementos)) inv = invRaw.elementos
    else if (Array.isArray(invRaw?.data)) inv = invRaw.data
    else if (Array.isArray(invRaw?.result)) inv = invRaw.result
    else if (typeof invRaw === 'object' && invRaw !== null) {
      const firstArray = Object.values(invRaw).find(v => Array.isArray(v))
      if (firstArray) inv = firstArray
    }

    const ids = new Set()
    const codes = new Set()
    for (const it of (inv || [])) {
      try {
        const cantidad = Number(it?.cantidadDisponible ?? it?.cantidad ?? it?.stock ?? it?.stok ?? 0)
        if (!Number.isFinite(cantidad) || cantidad <= 0) continue
        // try different product identifier fields
        if (it.productoId) ids.add(String(it.productoId))
        if (it.productoIds && Array.isArray(it.productoIds)) for (const pid of it.productoIds) ids.add(String(pid))
        if (it.producto && (it.producto.id || it.producto._id)) ids.add(String(it.producto.id ?? it.producto._id))
        if (it.productoCodigo) codes.add(String(it.productoCodigo))
        if (it.codigo) codes.add(String(it.codigo))
      } catch (inner) {
        if (debugImg) console.warn('[ProductosPage] skipping malformed inventory item', it, inner)
      }
    }
    inventoryIndex.value = { ids, codes }
    return inventoryIndex.value
  } catch (e) {
    inventoryIndex.value = { ids: new Set(), codes: new Set() }
    return inventoryIndex.value
  }
}

async function buildQueryParams(append = {}) {
  const params = new URLSearchParams()
  if (filters.q) params.set('q', filters.q)
  // category filter removed; do not include 'cat' in query
  if (filters.precioMin != null && filters.precioMin !== '') params.set('precioMin', String(filters.precioMin))
  if (filters.precioMax != null && filters.precioMax !== '') params.set('precioMax', String(filters.precioMax))
  if (filters.novedades) params.set('novedades', '1')
  if (filters.rebajas) params.set('rebajas', '1')
  if (page.value) params.set('page', String(page.value))
  if (pageSize.value) params.set('pageSize', String(pageSize.value))
  Object.entries(append).forEach(([k, v]) => params.set(k, String(v)))
  return params.toString()
}

async function loadProducts(reset = false) {
  if (reset) {
    page.value = 1
    productos.value = []
    total.value = null
  }
  dialogLoad.value = true
  try {
    const qs = await buildQueryParams()
  let resp = null

    // category-based inventory loading removed; will reimplement from scratch when ready

    // Prefer the public Producto listing (used elsewhere), then try Inventario paginated listing
    const triedEndpoints = [
      `Producto/ObtenerListadoPaginado?${qs}`,
      `Producto/ObtenerListadoPaginado`,
      `Inventario/ObtenerListadoPaginado?${qs}`,
      `Inventario/ObtenerListadoPaginado`,
    ]

  function responseHasItems(r) {
      if (!r) return false
      if (Array.isArray(r)) return r.length > 0
      if (Array.isArray(r?.result?.elementos)) return r.result.elementos.length > 0
      if (Array.isArray(r?.data?.result?.elementos)) return r.data.result.elementos.length > 0
      if (Array.isArray(r?.elementos)) return r.elementos.length > 0
      if (Array.isArray(r?.productos)) return r.productos.length > 0
      if (Array.isArray(r?.data)) return r.data.length > 0
      if (Array.isArray(r?.result)) return r.result.length > 0
      // look for any array-valued property with length
      if (typeof r === 'object' && r !== null) {
        const firstArray = Object.values(r).find(v => Array.isArray(v) && v.length > 0)
        if (firstArray) return true
      }
      return false
    }

    for (const ep of triedEndpoints) {
      try {
        let r
        // use loadGet for endpoints that follow the paginado pattern (it extracts result.elementos)
        if (ep.startsWith('Producto/ObtenerListadoPaginado')) {
          r = await loadGet(ep)
        } else {
          r = await loadGetHastaData(ep)
        }
        // accept only responses that actually contain items; otherwise continue to next candidate
        if (responseHasItems(r)) {
          resp = r
          break
        } else {
        }
      } catch (err) {
        if (debugImg) console.warn('[ProductosPage] endpoint failed:', ep, err)
      }
    }

    if (!resp) {
      productos.value = []
      total.value = 0
      return
    }

    // normalize resp into an array of items
    let items = []
    if (Array.isArray(resp)) items = resp
    else if (Array.isArray(resp?.result?.elementos)) items = resp.result.elementos
    else if (Array.isArray(resp?.data?.result?.elementos)) items = resp.data.result.elementos
    else if (Array.isArray(resp?.elementos)) items = resp.elementos
    else if (Array.isArray(resp?.productos)) items = resp.productos
    else if (Array.isArray(resp?.data)) items = resp.data
    else if (Array.isArray(resp?.result)) items = resp.result
    else if (typeof resp === 'object' && resp !== null) {
      items = resp?.result?.elementos ?? resp?.data ?? resp?.result ?? []
      if (!Array.isArray(items)) {
        const firstArray = Object.values(resp).find(v => Array.isArray(v))
        if (firstArray) items = firstArray
      }
    }

    if (debugImg) {
      if (Array.isArray(items) && items.length) {
        const s = items[0]

      }
    }

    // apply client-side filtering if backend didn't apply filters
    function parseDateVal(v) {
      if (!v) return null
      // if Date
      if (v instanceof Date) return v
      // if number (unix ms or seconds)
      if (typeof v === 'number') return new Date(v < 1e12 ? v * 1000 : v)
      // if string
      if (typeof v === 'string') {
        const n = Number(v)
        if (!Number.isNaN(n) && String(n).length >= 10) return new Date(n < 1e12 ? n * 1000 : n)
        const d = new Date(v)
        if (!Number.isNaN(d.getTime())) return d
      }
      return null
    }

    function getCreationDate(p) {
      if (!p) return null
      const candidates = [
        'createdAt', 'fechaCreacion', 'fechaCreado', 'fechaAlta', 'created_on', 'created', 'fecha', 'creadoEn', 'createdDate', 'fechaRegistro', 'fecha_creacion'
      ]
      for (const k of candidates) {
        if (p[k]) {
          const d = parseDateVal(p[k])
          if (d) return d
        }
      }
      // sometimes nested under metadata
      if (p.meta?.createdAt) return parseDateVal(p.meta.createdAt)
      return null
    }

    function applyClientFilters(list) {
      let out = (list || []).slice()

      // Filtrar productos inactivos
      out = out.filter(p => p.esActivo !== false)

      // search q
      if (filters.q) {
        const ql = String(filters.q).toLowerCase()
        out = out.filter(p => (p.descripcion || p.nombre || p.codigo || '').toString().toLowerCase().includes(ql))
      }
      // category filter supports multiple selection (robust: match by id or by name)
        // Category client-side filtering removed; reimplement from scratch when needed.
      // price range
      if (filters.precioMin != null) out = out.filter(p => Number(p.precioVenta ?? p.precio ?? p.precioVenta) >= Number(filters.precioMin))
      if (filters.precioMax != null) out = out.filter(p => Number(p.precioVenta ?? p.precio ?? p.precioVenta) <= Number(filters.precioMax))

      // SIEMPRE filtrar productos sin stock - no se muestran productos sin existencias
      out = out.filter(p => {
        let stock = p.stock || p.cantidadDisponible || p.stockTotal || 0
        if (!stock && Array.isArray(p.productoVariantes) && p.productoVariantes.length > 0) {
          stock = p.productoVariantes[0].stock || 0
        }
        return stock > 0
      })
      // rebajas flag
      if (filters.rebajas) {
        out = out.filter(p => {
          const hasDiscount = !!p.tieneDescuento || Number(p.descuento ?? p.porcentajeDescuento ?? 0) > 0 || (p.precioOriginal && p.precioVenta && Number(p.precioOriginal) > Number(p.precioVenta))
          return hasDiscount
        })
      }
      // novedades: don't filter, sort by creation date descending so newest first
      if (filters.novedades) {
        out.sort((a, b) => {
          const da = getCreationDate(a)
          const db = getCreationDate(b)
          if (da && db) return db - da
          if (da && !db) return -1
          if (!da && db) return 1
          return 0
        })
      }
      return out
    }

  const filtered = applyClientFilters(items)

    // set total and page-slice results
    total.value = filtered.length
    const start = (page.value - 1) * pageSize.value
    const pageItems = filtered.slice(start, start + pageSize.value)
    if (reset) productos.value = []

    // Pre-load images for each product in parallel
    await Promise.all(pageItems.map(async (p) => {
      if (!getProductoImage(p)) {
        const prodId = p.id || p.productoId || p.productId || p._id
        if (prodId) {
          try {
            const detailed = await loadGetHastaData(`Producto/ObtenerProductoEspecifico/${prodId}`)
            // Fusionar variantes del producto detallado al producto del listado
            if (detailed && detailed.productosVariantes) {
              p.productosVariantes = detailed.productosVariantes
            }
                        if (detailed && detailed.productoVariantes && detailed.productoVariantes.length) {
              const pv = detailed.productoVariantes[0]
              let fotoUrl = null

              // Prioridad 1: Foto directa de la variante
              if (pv && Array.isArray(pv.fotos) && pv.fotos.length) {
                const f0 = pv.fotos[0]
                fotoUrl = (typeof f0 === 'object') ? (f0.url || f0.img || f0.path) : f0
              }

              // Prioridad 2: Buscar herencia en otras variantes
              if (!fotoUrl) {
                const fotoHeredada = getFotoFromVarianteWithFallback(pv, detailed)
                if (fotoHeredada) {
                  fotoUrl = typeof fotoHeredada === 'object' ? (fotoHeredada.url || fotoHeredada.img || fotoHeredada.path) : fotoHeredada
                }
              }

              if (fotoUrl) {
                p.__preloadedFoto = fotoUrl
              }
            }
          } catch (e) {
            if (debugImg) console.warn('[ProductosPage] could not preload image for', prodId, e)
          }
        }
      }
    }))

    if (reset) {
      productos.value = pageItems.map(p => ({ ...p }))
    } else {
      productos.value = productos.value.concat(pageItems.map(p => ({ ...p })))
    }
  } catch (e) {
    console.warn('[ProductosPage] loadProducts error', e)
  } finally {
    dialogLoad.value = false
  }
}

async function loadMore() {
  page.value += 1
  await loadProducts(false)
}

async function resetFilters() {
  filters.q = ''
  filters.precioMin = null
  filters.precioMax = null
  filters.novedades = false
  filters.rebajas = false
  // also update route
  router.replace({ name: 'Productos', query: {} }).catch(() => {})
  await loadProducts(true)
}

async function applyFilters() {
  // update route query so linkable. Push arrays for multiple categories to preserve selections.
  const query = {}
  if (filters.q) query.q = String(filters.q)
  if (filters.precioMin != null && filters.precioMin !== '') query.precioMin = String(filters.precioMin)
  if (filters.precioMax != null && filters.precioMax !== '') query.precioMax = String(filters.precioMax)
  if (filters.novedades) query.novedades = '1'
  if (filters.rebajas) query.rebajas = '1'
  router.push({ name: 'Productos', query }).catch(() => {})
  await loadProducts(true)
}

function goToProduct(productOrId) {
  let id = productOrId
  if (typeof productOrId === 'object') id = productOrId.id || productOrId.productoId || productOrId.productId || productOrId._id || null
  if (!id) return
  router.push({ name: 'ProductoDetalle', params: { id: String(id) } }).catch(() => router.push('/producto/' + id))
}

// Normalizar producto antes de agregarlo al carrito
function normalizarProductoParaCarrito(producto, fotoPreferida = null) {
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
    fotos: (v.fotos || []).map(f => {
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
    }),
    slide: 1
  }))


  // Mapear fotos del producto principal
  let fotos = (producto.fotos || []).map(f => {
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

  if (fotoPreferida && fotos.length === 0) {
    fotos.unshift({
      id: null,
      url: fotoPreferida,
      descripcion: '',
      esPrincipal: true,
      orden: 0
    })
  }

    // Obtener ID de variante (usar la principal si existe, si no la primera)
// Obtener ID de variante (usar la principal si existe, si no la primera, si no el producto)
const varianteId = variants.find(v => v.principal === true || v.principal === 'true')?.id || (variants.length > 0 ? variants[0].id : producto.id)
  // Estructura normalizada
  const normalizado = {
    id: varianteId, // 👈 ahora guarda el id de la variante
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
    varianteId: varianteId,
    fotos,
    variants,
    tieneDescuento: producto.tieneDescuento ?? false,
    precioVentaDescuento: producto.precioVentaDescuento ?? producto.precioVenta
  }

  return normalizado
}


function formatPrice(v) {
  if (v == null) return '0.00'
  return Number(v).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
}

// photo helpers borrowed from existing pages
function getFotoUrl(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  if (typeof foto !== 'string') foto = String(foto)
  if (/^https?:\/\//.test(foto)) return foto
  // Normalize slashes to avoid double-slash issues
  const base = apiFotosBaseUrl || ''
  if (!base) return (foto.startsWith('/') ? foto : '/' + foto)
  const sep = base.endsWith('/') ? '' : '/'
  const path = foto.startsWith('/') ? foto.replace(/^\//, '') : foto
  return base + sep + path
}
function getFotoUrlFromFoto(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  if (typeof foto === 'object') {
    const candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || null
    return getFotoUrl(candidate)
  }
  return getFotoUrl(foto)
}

function getProductoImage(prod) {
  if (!prod) return null
  // robust resolution: try many common fields and nested structures
  const tryFields = (obj, keys) => {
    if (!obj) return null
    for (const k of keys) {
      const v = obj[k]
      if (!v && v !== 0) continue
      if (typeof v === 'string' && v.trim() !== '') return v
      if (typeof v === 'object' && v !== null) {
        // common nested props
        const nested = v.url ?? v.img ?? v.path ?? v.imagen ?? v.foto ?? v.ruta ?? v.rutaArchivo ?? v.archivo ?? v.file ?? null
        if (typeof nested === 'string' && nested.trim() !== '') return nested
      }
    }
    return null
  }

  // top-level common names
  let candidate = tryFields(prod, ['fotoUrl', 'foto', 'imagen', 'imagenUrl', 'url', 'image', 'picture', 'imagenPrincipal', 'imagen_principal', 'fotoPrincipal'])

  // arrays of photos
  if ((!candidate || candidate === '') && Array.isArray(prod.fotos) && prod.fotos.length > 0) {
    for (const first of prod.fotos) {
      if (!first) continue
      if (typeof first === 'string' && first.trim() !== '') { candidate = first; break }
      const nested = tryFields(first, Object.keys(first))
      if (nested) { candidate = nested; break }
    }
  }

  // prefer image from first variant if available (variants or variantes)
  if ((!candidate || candidate === '') && (Array.isArray(prod.variants) || Array.isArray(prod.variantes))) {
    const vars = Array.isArray(prod.variants) ? prod.variants : prod.variantes
    if (vars && vars.length) {
      const firstVar = vars[0]
      if (firstVar) {
        // try common fields on variant
        const vCandidate = tryFields(firstVar, ['fotoUrl', 'foto', 'imagen', 'imagenUrl', 'url', 'image', 'picture'])
        if (vCandidate) candidate = vCandidate
        // variant may have array of fotos
        if ((!candidate || candidate === '') && Array.isArray(firstVar.fotos) && firstVar.fotos.length) {
          const f = firstVar.fotos[0]
          if (typeof f === 'string' && f.trim() !== '') candidate = f
          else if (typeof f === 'object' && f !== null) {
            const nested = tryFields(f, Object.keys(f))
            if (nested) candidate = nested
          }
        }
        // Si la variante no tiene fotos, busca en otras variantes del mismo producto
        if ((!candidate || candidate === '') && Array.isArray(vars)) {
          const fotoHeredada = getFotoFromVarianteWithFallback(firstVar, prod)
          if (fotoHeredada) candidate = fotoHeredada
        }
      }
    }
  }

  // prefer image from productosVariantes (API uses Spanish naming)
  if ((!candidate || candidate === '') && Array.isArray(prod.productosVariantes) && prod.productosVariantes.length) {
    const pv = prod.productosVariantes[0]
    if (pv) {
      // pv may have fotos array with objects that contain 'url'
      if (Array.isArray(pv.fotos) && pv.fotos.length) {
        const f0 = pv.fotos[0]
        if (typeof f0 === 'string' && f0.trim() !== '') candidate = f0
        else if (f0 && typeof f0 === 'object') candidate = f0.url || f0.img || f0.path || candidate
      }
      // Si la variante no tiene fotos, busca en otras variantes del mismo producto
      if ((!candidate || candidate === '') && Array.isArray(prod.productosVariantes)) {
        const fotoHeredada = getFotoFromVarianteWithFallback(pv, prod)
        if (fotoHeredada) candidate = fotoHeredada
      }
      // also check common fields on the variante object
      if ((!candidate || candidate === '') && typeof pv === 'object') {
        const vCandidate = tryFields(pv, ['fotoUrl', 'foto', 'imagen', 'imagenUrl', 'url', 'image', 'picture'])
        if (vCandidate) candidate = vCandidate
      }
    }
  }

  // other possible arrays
  if ((!candidate || candidate === '') && Array.isArray(prod.imagenes) && prod.imagenes.length > 0) {
    for (const it of prod.imagenes) {
      if (typeof it === 'string' && it.trim() !== '') { candidate = it; break }
      const nested = tryFields(it, Object.keys(it))
      if (nested) { candidate = nested; break }
    }
  }

  // some APIs nest product inside another object
  if ((!candidate || candidate === '') && prod.producto) {
    candidate = tryFields(prod.producto, ['fotoUrl', 'imagen', 'url', 'image']) || candidate
  }

  // as last resort check for any string-looking property that looks like a path/URL
  if (!candidate || candidate === '') {
    for (const v of Object.values(prod)) {
      if (typeof v === 'string' && (v.startsWith('/') || v.startsWith('http') || v.includes('/uploads/') || v.match(/\.png$|\.jpg$|\.jpeg$|\.webp$/i))) {
        candidate = v
        break
      }
    }
  }

  return candidate || null
}

function getProductoImagePreferVariant(prod) {
  if (!prod) return null

  // Return pre-loaded foto if available
  if (prod.__preloadedFoto) {
    return prod.__preloadedFoto
  }

  // Try existing resolution first
  let c = getProductoImage(prod)
  if (c) return c

  // check English variants
  const vars = prod.variants || prod.variantes
  if (Array.isArray(vars) && vars.length) {
    const v0 = vars[0]
    if (v0) {
      if (Array.isArray(v0.fotos) && v0.fotos.length) {
        const f0 = v0.fotos[0]
        return (typeof f0 === 'object') ? (f0.url || f0.img || f0.path) : f0
      }
      // Si la variante no tiene fotos, busca en otras variantes del mismo producto
      const fotoHeredada = getFotoFromVarianteWithFallback(v0, prod)
      if (fotoHeredada) return fotoHeredada
      return v0.fotoUrl || v0.foto || v0.imagen || v0.imagenUrl || v0.url || v0.image || v0.picture || null
    }
  }

  // check Spanish naming productosVariantes
  const pvs = prod.productosVariantes
  if (Array.isArray(pvs) && pvs.length) {
    const pv0 = pvs[0]
    if (pv0) {
      if (Array.isArray(pv0.fotos) && pv0.fotos.length) {
        const f0 = pv0.fotos[0]
        return (typeof f0 === 'object') ? (f0.url || f0.img || f0.path) : f0
      }
      // Si la variante no tiene fotos, busca en otras variantes del mismo producto
      const fotoHeredada = getFotoFromVarianteWithFallback(pv0, prod)
      if (fotoHeredada) return fotoHeredada
      return pv0.fotoUrl || pv0.foto || pv0.imagen || pv0.url || null
    }
  }

  return null
}

  // expose image error handler in script-setup scope
  function onImgError(p) {
    try { if (p) p.__imgError = true } catch (e) { /* ignore */ }
  }
</script>

<style scoped lang="scss">
.ellipsis {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.clickable-card {
  cursor: pointer;
  transition: transform 0.2s ease, box-shadow 0.2s ease;

  &:hover {
    transform: translateY(-4px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.15) !important;
  }
}

.product-img {
  min-height: 160px;
  display: block;
}

.product-img .q-img__image {
  object-fit: cover;
  width: 100%;
  height: 100%;
}

.text-strike {
  text-decoration: line-through;
}

.filters-products-container {
  max-width: auto;
  margin: 0 auto;
}

.filters-sidebar {
  display: block;
}

.products-section {
  display: block;
}

/* Media Queries para responsividad */
@media (max-width: 599px) {
  .filters-sidebar {
    margin-bottom: 20px;
  }

  .filters-products-container {
    flex-direction: column;
  }

  :deep(.q-field) {
    font-size: 14px;
  }

  :deep(.col-12) {
    padding-left: 0 !important;
    padding-right: 0 !important;
  }

  .col-12.col-md-3 {
    padding-right: 0 !important;
  }

  .col-12.col-md-9 {
    margin-bottom: 20px;
  }

  .product-img {
    min-height: 120px;
  }

  .text-h6 {
    font-size: 18px !important;
  }

  .text-subtitle2 {
    font-size: 13px !important;
  }

  .text-caption {
    font-size: 11px !important;
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
  .filters-sidebar {
    padding-right: 12px !important;
  }

  .col-12.col-md-3 {
    flex: 0 0 35%;
    max-width: 35%;
  }

  .col-12.col-md-9 {
    flex: 0 0 65%;
    max-width: 65%;
  }

  .product-img {
    min-height: 140px;
  }
}

@media (min-width: 1366px) {
  .filters-products-container {
    padding: 24px;
  }

  .product-img {
    min-height: 200px;
  }
}
</style>
