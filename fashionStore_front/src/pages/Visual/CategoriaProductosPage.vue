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
    <q-card
      class="q-pa-md shadow-2 cursor-pointer"
      @click="goToProduct(producto.id)"
    >

      <!-- Carrusel de fotos -->
      <div v-if="producto.productosVariantes && producto.productosVariantes.length" class="q-mb-md">
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
            v-for="(foto, idx) in producto.productosVariantes"
            :key="idx"
            :name="idx"
            class="flex flex-center bg-grey-2"
          >
            <q-img
              :src="getFotoUrlFromFoto(foto, producto)"
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
        <q-btn
          dense
          round
          flat
          :icon="wishlist.isFavorito(producto.id) ? 'favorite' : 'favorite_border'"
          @click.stop="() => wishlist.toggle(normalizarProductoParaCarrito(producto, getProductoImagePreferVariant(producto) || getProductoImage(producto)))"
        />
        <q-btn
          flat
          round
          icon="shopping_cart"
          color="purple-4"
          @click.stop="() => cart.addItem(normalizarProductoParaCarrito(producto, getProductoImagePreferVariant(producto) || getProductoImage(producto)),1)"
        />
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
import { useRoute,useRouter } from 'vue-router'
import { loadGet, loadGetDatosInicio, loadGetHastaData, getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import TopBar from 'src/pages/Visual/components/TopBar.vue'
import { apiFotosBaseUrl } from 'src/boot/axios'
import { useWishlist } from 'src/stores/wishlistStore'
import useCart from 'src/stores/cartStore'
const router = useRouter()
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

    let lista = await loadGetHastaData(`Producto/ObtenerProductosPorCategoria/${id}`)??[]
    // Filtrar productos sin stock
    const productosConStock = (lista ?? []).filter(p => {
      let stock = p.stock || p.cantidadDisponible || p.stockTotal || 0
      if (!stock && Array.isArray(p.productoVariantes) && p.productoVariantes.length > 0) {
        stock = p.productoVariantes[0].stock || 0
      }
      return stock > 0
    })

    // Precargar variantes completas y fotos para cada producto
    await Promise.all(productosConStock.map(async (p) => {
      try {
        const prodId = p.id || p.productoId || p.productId || p._id
        if (prodId) {
          if (debugImg) console.log('[CategoriaProductos] preloading variants for product', prodId)
          const detailed = await loadGetHastaData(`Producto/ObtenerProductoEspecifico/${prodId}`)
          if (debugImg) {
            console.log('[CategoriaProductos] got detailed product for', prodId)
            console.log('[CategoriaProductos] detailed object keys:', Object.keys(detailed || {}))
            console.log('[CategoriaProductos] detailed.productosVariantes:', detailed?.productosVariantes)
            console.log('[CategoriaProductos] detailed.productoVariantes:', detailed?.productoVariantes)
          }

          // Fusionar variantes del producto detallado al producto del listado
          // Probar ambas claves (productosVariantes y productoVariantes)
          const variantes = detailed?.productosVariantes || detailed?.productoVariantes
          if (detailed && variantes) {
            p.productosVariantes = variantes
            if (debugImg) console.log('[CategoriaProductos] merged productosVariantes:', variantes.length, 'variants')
          } else if (debugImg) {
            console.log('[CategoriaProductos] NO VARIANTES FOUND - p.productosVariantes exists?', !!p.productosVariantes)
          }
        }
      } catch (e) {
        if (debugImg) console.error('[CategoriaProductos] error preloading variants', e)
      }
    }))

    productos.value = productosConStock.map(p => ({ ...p, slide: 0 }))
    if (debugImg) console.log('[CategoriaProductos] mapped productos count:', productos.value.length)
console.log(" productos.value : ", productos.value)

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

function getFotoUrlFromFoto(foto, producto = null) {
  if (!foto) return '/img/sin-foto.jpg'

  if (debugImg) {
    console.log('[getFotoUrlFromFoto] foto object:', foto)
    console.log('[getFotoUrlFromFoto] foto keys:', typeof foto === 'object' ? Object.keys(foto) : 'not object')
    console.log('[getFotoUrlFromFoto] foto.fotos:', foto?.fotos, 'is array?', Array.isArray(foto?.fotos), 'length:', foto?.fotos?.length)
  }

  // Si es un objeto con propiedad fotos (array de variante)
  if (foto.fotos && Array.isArray(foto.fotos) && foto.fotos.length > 0) {
    const fotoObj = foto.fotos[0]
    if (debugImg) console.log('[getFotoUrlFromFoto] Found fotos array, using first:', fotoObj)
    if (typeof fotoObj === 'object') {
      return getFotoUrl(fotoObj.url || fotoObj.imagen || fotoObj.path)
    }
    return getFotoUrl(fotoObj)
  }

  // Si la variante no tiene fotos, buscar heredadas de hermanas
  if (foto.fotos && Array.isArray(foto.fotos) && foto.fotos.length === 0 && producto) {
    if (debugImg) console.log('[getFotoUrlFromFoto] Variante sin fotos, buscando heredadas...')
    const fotoHeredada = getFotoFromVarianteWithFallback(foto, producto)
    if (fotoHeredada) {
      if (debugImg) console.log('[getFotoUrlFromFoto] ✅ Encontró foto heredada:', fotoHeredada)
      return getFotoUrl(fotoHeredada)
    }
  }

  // Si es un objeto con propiedad url
  if (typeof foto === 'object') {
    const candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto || null
    if (debugImg && candidate) console.log('[getFotoUrlFromFoto] Found direct property:', candidate)
    if (candidate) return getFotoUrl(candidate)
  }

  // Si es un string directo
  if (typeof foto === 'string') {
    if (debugImg) console.log('[getFotoUrlFromFoto] String foto:', foto)
    return getFotoUrl(foto)
  }

  return '/img/sin-foto.jpg'
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

// Normalizar producto antes de agregarlo al carrito/wishlist
function normalizarProductoParaCarrito(producto, fotoPreferida = null) {
  if (!producto) return producto

  console.log('[CategoriaProductos] normalizarProductoParaCarrito producto:', producto)

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

  // Si se proporcionó una fotoPreferida y no hay fotos, agregarla al principio
  if (fotoPreferida && fotos.length === 0) {
    fotos.unshift({
      id: null,
      url: fotoPreferida,
      descripcion: '',
      esPrincipal: true,
      orden: 0
    })
  }

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

// Photo resolution functions from ProductosPage
function tryFields(obj, fieldNames) {
  if (!obj || typeof obj !== 'object') return null
  for (const k of fieldNames) {
    const v = obj[k]
    if (typeof v === 'string' && v.trim() !== '') return v
    if (typeof v === 'object' && v !== null) {
      const nested = v.url ?? v.img ?? v.path ?? v.imagen ?? v.foto ?? v.ruta ?? v.rutaArchivo ?? v.archivo ?? v.file ?? null
      if (typeof nested === 'string' && nested.trim() !== '') return nested
    }
  }
  return null
}

function getProductoImage(prod) {
  if (!prod) return null

  let candidate = tryFields(prod, ['fotoUrl', 'foto', 'imagen', 'imagenUrl', 'url', 'image', 'picture', 'imagenPrincipal', 'imagen_principal', 'fotoPrincipal'])
  console.log('[CategoriaProductos] getProductoImage paso 1 (campos directos):', { productId: prod.id, candidate })

  // arrays of photos
  if ((!candidate || candidate === '') && Array.isArray(prod.fotos) && prod.fotos.length > 0) {
    for (const first of prod.fotos) {
      if (!first) continue
      if (typeof first === 'string' && first.trim() !== '') { candidate = first; break }
      const nested = tryFields(first, Object.keys(first))
      if (nested) { candidate = nested; break }
    }
    console.log('[CategoriaProductos] getProductoImage paso 2 (array fotos):', { productId: prod.id, candidate })
  }

  // prefer image from productosVariantes (API uses Spanish naming)
  if ((!candidate || candidate === '') && Array.isArray(prod.productosVariantes) && prod.productosVariantes.length) {
    console.log('[CategoriaProductos] getProductoImage paso 3: verificando productosVariantes...', { productId: prod.id, variantes: prod.productosVariantes.length })
    const pv = prod.productosVariantes[0]
    if (pv) {
      if (Array.isArray(pv.fotos) && pv.fotos.length) {
        const f0 = pv.fotos[0]
        if (typeof f0 === 'string' && f0.trim() !== '') candidate = f0
        else if (f0 && typeof f0 === 'object') candidate = f0.url || f0.img || f0.path || candidate
        console.log('[CategoriaProductos] getProductoImage paso 3a (variante con fotos):', { productId: prod.id, candidate })
      }
      // Si la variante no tiene fotos, busca en otras variantes del mismo producto
      if (!candidate || candidate === '') {
        console.log('[CategoriaProductos] getProductoImage paso 3b: llamando getFotoFromVarianteWithFallback...')
        const fotoHeredada = getFotoFromVarianteWithFallback(pv, prod)
        if (fotoHeredada) {
          console.log('[CategoriaProductos] getProductoImage paso 3b: foto heredada encontrada:', fotoHeredada)
          candidate = fotoHeredada
        }
      }
      if ((!candidate || candidate === '') && typeof pv === 'object') {
        const vCandidate = tryFields(pv, ['fotoUrl', 'foto', 'imagen', 'imagenUrl', 'url', 'image', 'picture'])
        if (vCandidate) candidate = vCandidate
        console.log('[CategoriaProductos] getProductoImage paso 3c (campos variante):', { productId: prod.id, candidate })
      }
    }
  }

  if (debugImg) console.log('[CategoriaProductos] resolved image candidate for product', prod.id ?? prod.productoId ?? prod._id ?? null, '->', candidate)
  return candidate || null
}

function getProductoImagePreferVariant(prod) {
  if (!prod) return null

  let c = getProductoImage(prod)
  if (c) return c

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

  if (debugImg) console.log('[CategoriaProductos] getProductoImagePreferVariant returning null for product', prod.id)
  return null
}
</script>
