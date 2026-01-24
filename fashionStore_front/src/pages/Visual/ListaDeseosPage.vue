<template>
  <div>
    <TopBar :categories="[]" />

    <div class="q-pa-lg bg-grey-1 wishlist-page">
      <!-- Encabezado -->
      <div class="row items-center q-mb-lg">
        <div class="col">
          <div class="text-h5 text-weight-bold text-primary">
             Lista de deseos
          </div>
        </div>
        <div class="col-auto">
          <q-btn
          dense
          round
            flat
            color="negative"
            @click="clearAll"
            icon="delete"
            v-if="items.length"
          ><q-tooltip>Eliminar Lista de Deseos</q-tooltip>
        </q-btn>
          </div>
      </div>

      <!-- Vacía -->
      <div v-if="!items.length" class="q-pa-xl text-center text-grey-7">
        <q-icon name="favorite_border" size="48px" color="grey-5" class="q-mb-md" />
        <div class="text-subtitle1">
          No tienes productos en tu lista de deseos. Agrega productos desde la tienda.
        </div>
      </div>

      <!-- Lista -->
      <div v-else>
        <div v-for="p in items" :key="p.id" class="q-mb-md">
          <q-card bordered class="shadow-2 rounded-borders q-pa-sm row items-center wishlist-card">
            <!-- Imagen del producto -->
            <div class="wishlist-img-container col-auto">
              <q-img
                :src="obtenerFotoDelItem(p)"
                class="wishlist-img"
              >
                <template v-slot:error>
                  <div class="text-center text-grey-6 q-pa-md">Sin imagen</div>
                </template>
              </q-img>
            </div>

            <!-- Información del producto -->
            <div class="col q-pl-md">
              <div class="text-subtitle1 text-weight-bold">{{ p.nombre }}</div>
              <div v-if="p.tieneDescuento" class="row items-center q-gutter-sm">
  <div class="text-caption text-grey-6">
    <s>$ {{ formatPrice(p.precioVenta) }}</s>
  </div>
  <div class="text-caption text-weight-bold text-primary">
    $ {{ formatPrice(p.precioVentaDescuento) }}
  </div>
</div>
<div v-else class="text-caption text-grey-7">
  $ {{ formatPrice(p.precioVenta) }}
</div>

            </div>

            <!-- Acciones -->
            <div class="wishlist-actions-container">
              <div class="col-auto">
                <q-btn
                  dense
                  flat
                  round
                  icon="remove_shopping_cart"
                  color="negative"
                  @click="removeItem(p.id)"
                />
                 <q-tooltip>Remover producto</q-tooltip>
              </div>
              <div class="col-auto">
                <q-btn
                 dense round flat icon="add_shopping_cart" color="primary"

                  @click="addToCartFromWish(p)"

                />
      <q-tooltip>Agregar producto</q-tooltip>
                </div>
                <div class="col-auto">
                  <q-btn
                  flat
                  round
                  dense
                  icon="open_in_new"
                  @click="goToProduct(p.id)"
                >
                  <q-tooltip>Ver producto</q-tooltip>
                </q-btn>
                </div>
            </div>


          </q-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import TopBar from './components/TopBar.vue'
import { useWishlist } from 'src/stores/wishlistStore'
import { useCart } from 'src/stores/cartStore'
import { useRouter } from 'vue-router'
import { apiFotosBaseUrl } from 'src/boot/axios'
import { Success } from 'src/boot/notify'
import { getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'

const wishlist = useWishlist()
const cart = useCart()
const router = useRouter()

const items = computed(() => Array.isArray(wishlist.items) ? wishlist.items : [])

function removeItem(id) {
  wishlist.remove(id)
}

function clearAll() {
  wishlist.items.splice(0, wishlist.items.length)
  Success("Lista de deseos eliminada")
}

function addToCartFromWish(p) {
  const raw = p?.raw ?? p
  cart.addItem(raw, 1)
  if (p?.id) wishlist.remove(p.id)
}

function goToProduct(id) {
  router.push({ name: 'ProductoDetalle', params: { id } }).catch(() =>
    router.push('/producto/' + id)
  )
}

function obtenerFotoDelItem(item) {
  if (!item) return '/img/sin-foto.jpg'

  // Nivel 1: Foto directa en el item
  if (item.foto) {
    return getFotoUrl(item.foto)
  }

  // Nivel 2: Array de fotos en item.raw.fotos
  if (item.raw?.fotos && Array.isArray(item.raw.fotos) && item.raw.fotos.length > 0) {
    return getFotoUrl(item.raw.fotos[0])
  }

  // Nivel 3: Variante seleccionada en item.raw.variants o variants
  const variants = item.raw?.variants || item.variants
  if (variants && Array.isArray(variants) && variants.length > 0) {
    if (variants[0].fotos && Array.isArray(variants[0].fotos) && variants[0].fotos.length > 0) {
      return getFotoUrl(variants[0].fotos[0])
    }
    // Si la variante no tiene fotos, busca en otras variantes del mismo producto
    const fotoHeredada = getFotoFromVarianteWithFallback(variants[0], item.raw)
    if (fotoHeredada) return getFotoUrl(fotoHeredada)
  }

  // Nivel 4: productoVariantes de la API
  const productoVariantes = item.raw?.productoVariantes
  if (productoVariantes && Array.isArray(productoVariantes) && productoVariantes.length > 0) {
    if (productoVariantes[0].fotos && Array.isArray(productoVariantes[0].fotos) && productoVariantes[0].fotos.length > 0) {
      return getFotoUrl(productoVariantes[0].fotos[0])
    }
    // Si la variante no tiene fotos, busca en otras variantes del mismo producto
    const fotoHeredada = getFotoFromVarianteWithFallback(productoVariantes[0], item.raw)
    if (fotoHeredada) return getFotoUrl(fotoHeredada)
  }

  return '/img/sin-foto.jpg'
}

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
  return getFotoUrl(foto)
}

function formatPrice(v) {
  return (v || 0).toLocaleString('es-ES', { minimumFractionDigits: 2 })
}
</script>

<style scoped lang="scss">
.wishlist-page {
  max-width: 1100px;
  margin: 0 auto;
  margin-top: 32px;
  padding: 16px;
   border-radius: 15px;
}

.wishlist-card {
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.wishlist-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
}

.wishlist-img {
  width: 100px;
  height: 80px;
  object-fit: cover;
  border-radius: 6px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
  transition: transform 0.2s ease;
}

.wishlist-img:hover {
  transform: scale(1.05);
}

.wishlist-actions-container {
  display: flex;
  gap: 8px;
}

/* Media Queries para responsividad */
@media (max-width: 599px) {
  .wishlist-actions-container {
    width: 100% !important;
    flex: 1 1 100% !important;
    display: flex !important;
    flex-direction: row !important;
    gap: 12px !important;
    justify-content: center !important;
    padding-top: 10px;
    border-top: 1px solid rgba(0, 0, 0, 0.05);

  }

  .wishlist-img-container {
    width: 100% !important;
    flex: 1 1 100% !important;
  }

  .wishlist-page {
    margin-top: 16px;
    padding: 12px;
  }

  .q-card {
    flex-direction: column !important;
  }

  .wishlist-img {
    width: 100% !important;
    height: 200px;
    margin-bottom: 10px;
  }

  .text-h5 {
    font-size: 18px !important;
  }

  .text-subtitle1 {
    font-size: 14px !important;
  }

  .text-caption {
    font-size: 12px !important;
  }

  :deep(.q-field) {
    font-size: 12px;
  }
}

@media (max-width: 1023px) and (min-width: 600px) {
  .wishlist-page {
    padding: 16px;
  }

  .wishlist-img {
    width: 90px;
    height: 70px;
  }
}

@media (min-width: 1366px) {
  .wishlist-page {
    padding: 24px;
  }

  .wishlist-img {
    width: 120px;
    height: 90px;
  }
}
</style>
