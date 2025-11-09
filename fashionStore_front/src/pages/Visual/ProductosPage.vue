<template>
  <div>
    <TopBar :categories="categories" />

    <div class="q-pa-lg bg-grey-1 row">
      <!-- Sidebar filters -->
      <div class="col-12 col-md-3 q-pr-md">
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
            <q-checkbox v-model="filters.rebajas" label="Rebajas" dense />
            <q-checkbox v-model="filters.disponible" label="Solo disponibles" dense />
          </div>

          <div class="row justify-end q-mt-md">
            <q-btn flat label="Limpiar" @click="resetFilters" />
            <q-btn color="primary" label="Aplicar" @click="applyFilters" />
          </div>
        </q-card>
      </div>

      <!-- Products grid -->
      <div class="col-12 col-md-9">
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
                  :src="(p && p.__imgError) ? '/img/sin-foto.jpg' : getFotoUrl(getProductoImage(p))"
                  ratio="4/3"
                  spinner-color="primary"
                  @error="() => onImgError(p)"
                >
                <template v-slot:error>
                  <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
                </template>
              </q-img>
              <q-card-section>
                <div class="text-subtitle2 text-weight-medium q-mb-xs ellipsis">{{ p.descripcion || p.nombre || 'Sin título' }}</div>
                <div class="text-subtitle1 text-weight-bold">$ {{ formatPrice(p.precioVenta) }}</div>
              </q-card-section>
              <q-card-actions align="right">
                <q-btn dense round flat :icon="wishlist.isFavorito(p.id) ? 'favorite' : 'favorite_border'" @click.stop="() => wishlist.toggle(p)" />
                <q-btn dense round flat icon="add_shopping_cart" color="primary" @click.stop="() => cart.addItem(p,1)" />
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
import { loadGetHastaData, loadGetDatosInicio, loadGet } from 'src/assets/js/util/funciones'
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
  disponible: route.query.disponible === '1' || !!route.query.disponible,
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

  if (debugImg) console.log('[ProductosPage] on mount, initial filters q:', filters.q)

    await loadProducts(true)
  } finally {
    dialogLoad.value = false
  }
})

// keep filters synced when route.query changes (e.g. user navigates directly or uses links)
watch(() => route.query, (q) => {
  if (debugImg) console.log('[ProductosPage] route.query changed ->', q)
  try {
    filters.q = q.q || ''
    filters.precioMin = q.precioMin ? Number(q.precioMin) : null
    filters.precioMax = q.precioMax ? Number(q.precioMax) : null
    filters.novedades = q.novedades === '1' || !!q.novedades
    filters.rebajas = q.rebajas === '1' || !!q.rebajas
    filters.disponible = q.disponible === '1' || !!q.disponible
  } catch (err) {
    if (debugImg) console.warn('[ProductosPage] error syncing route.query to filters', err)
  }
  loadProducts(true)
}, { deep: true })

async function ensureInventoryIndex() {
  if (inventoryIndex.value) return inventoryIndex.value
  try {
    if (debugImg) console.log('[ProductosPage] loading inventory index')
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
    if (debugImg) console.log('[ProductosPage] inventory index built:', { ids: ids.size, codes: codes.size })
    return inventoryIndex.value
  } catch (e) {
    if (debugImg) console.warn('[ProductosPage] could not load inventory index', e)
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
  if (filters.disponible) params.set('disponible', '1')
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
        if (debugImg) console.log('[ProductosPage] trying endpoint:', ep)
        let r
        // use loadGet for endpoints that follow the paginado pattern (it extracts result.elementos)
        if (ep.startsWith('Producto/ObtenerListadoPaginado') || ep.startsWith('Inventario/ObtenerListadoPaginado')) {
          r = await loadGet(ep)
        } else {
          r = await loadGetHastaData(ep)
        }
        if (debugImg) console.log('[ProductosPage] response from', ep, ':', r)
        // accept only responses that actually contain items; otherwise continue to next candidate
        if (responseHasItems(r)) {
          resp = r
          break
        } else {
          if (debugImg) console.log('[ProductosPage] response from', ep, 'had no items, trying next')
        }
      } catch (err) {
        if (debugImg) console.warn('[ProductosPage] endpoint failed:', ep, err)
      }
    }
    if (debugImg) console.log('[ProductosPage] selected response:', resp)

    if (!resp) {
      if (debugImg) console.warn('[ProductosPage] no response returned by endpoints')
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
      console.log('[ProductosPage] normalized items length:', Array.isArray(items) ? items.length : 0)
      if (Array.isArray(items) && items.length) {
        const s = items[0]
        console.log('[ProductosPage] sample item (id,categoriaId,categoriaIds,categorias,categoriasList):', s && {
          id: s.id ?? s.productoId ?? s._id ?? null,
          categoriaId: s.categoriaId ?? s.categoria ?? null,
          categoriaIds: s.categoriaIds ?? s.categoriasIds ?? null,
          categorias: s.categorias ?? null,
          categoriasList: s.categoriasList ?? s.categoriasObj ?? null
        })
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
        // disponibilidad: if backend inventory exists, cross-check by product id or code
        if (filters.disponible) {
          // inventoryIndex may be prepared by caller; if not, attempt best-effort local fields
          const inv = inventoryIndex.value
          if (inv && (inv.ids.size || inv.codes.size)) {
            out = out.filter(p => {
              const pid = p.id ?? p.productoId ?? p.productId ?? p._id
              if (pid && inv.ids.has(String(pid))) return true
              if (p.codigo && inv.codes.has(String(p.codigo))) return true
              if (p.productoCodigo && inv.codes.has(String(p.productoCodigo))) return true
              return false
            })
          } else {
            out = out.filter(p => {
              const q = p.cantidadDisponible ?? p.stock ?? p.existencia ?? p.disponible ?? p.stok ?? 0
              return Number(q) > 0
            })
          }
        }
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

  if (filters.disponible) await ensureInventoryIndex()
  const filtered = applyClientFilters(items)

    // set total and page-slice results
    total.value = filtered.length
    const start = (page.value - 1) * pageSize.value
    const pageItems = filtered.slice(start, start + pageSize.value)
    if (reset) productos.value = []
    productos.value = productos.value.concat(pageItems.map(p => ({ ...p })))
    if (debugImg) console.log('[ProductosPage] loaded items count:', productos.value.length, productos.value[0])
  } catch (e) {
    console.warn('[ProductosPage] loadProducts error', e)
  } finally {
    dialogLoad.value = false
  }
}

function loadMore() {
  page.value += 1
  loadProducts(false)
}

function resetFilters() {
  filters.q = ''
  filters.precioMin = null
  filters.precioMax = null
  filters.novedades = false
  filters.rebajas = false
  filters.disponible = false
  // also update route
  router.replace({ name: 'Productos', query: {} }).catch(() => {})
  loadProducts(true)
}

async function applyFilters() {
  // update route query so linkable. Push arrays for multiple categories to preserve selections.
  const query = {}
  if (filters.q) query.q = String(filters.q)
  if (filters.precioMin != null && filters.precioMin !== '') query.precioMin = String(filters.precioMin)
  if (filters.precioMax != null && filters.precioMax !== '') query.precioMax = String(filters.precioMax)
  if (filters.novedades) query.novedades = '1'
  if (filters.rebajas) query.rebajas = '1'
  if (filters.disponible) query.disponible = '1'
  router.push({ name: 'Productos', query }).catch(() => {})
  loadProducts(true)
}

function goToProduct(productOrId) {
  let id = productOrId
  if (typeof productOrId === 'object') id = productOrId.id || productOrId.productoId || productOrId.productId || productOrId._id || null
  if (!id) return
  router.push({ name: 'ProductoDetalle', params: { id: String(id) } }).catch(() => router.push('/producto/' + id))
}

function formatPrice(v) {
  if (v == null) return '0.00'
  return Number(v).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
}

// photo helpers borrowed from existing pages
function getFotoUrl(foto) {
  try {
    if (!foto) return '/img/sin-foto.jpg'
    let candidate = foto
    if (typeof foto === 'object') {
      candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto || null
    }
    if (!candidate) return '/img/sin-foto.jpg'
    if (typeof candidate !== 'string') candidate = String(candidate)
    if (/^https?:\/\//.test(candidate)) return candidate
    const base = apiFotosBaseUrl || ''
    if (!base) return (candidate.startsWith('/') ? candidate : '/' + candidate)
    const sep = base.endsWith('/') ? '' : '/'
    const path = candidate.startsWith('/') ? candidate.replace(/^\//, '') : candidate
    return base + sep + path
  } catch (err) {
    if (debugImg) console.warn('[ProductosPage] getFotoUrl error', err, foto)
    return '/img/sin-foto.jpg'
  }
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

  if (debugImg) console.log('[ProductosPage] resolved image candidate for product', prod.id ?? prod.productoId ?? prod._id ?? null, '->', candidate)
  return candidate || null
}
  // expose image error handler in script-setup scope
  function onImgError(p) {
    try { if (p) p.__imgError = true } catch (e) { /* ignore */ }
  }
</script>

<style scoped>
.ellipsis { overflow: hidden; text-overflow: ellipsis; white-space: nowrap }
.clickable-card { cursor: pointer }
.product-img { min-height: 160px; display: block }
.product-img .q-img__image { object-fit: cover; width: 100%; height: 100% }
</style>
