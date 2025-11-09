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
            <div class="col-auto">
              <q-img
                :src="getFotoUrlFromFoto(p.foto || (p.raw && p.raw.fotos && p.raw.fotos[0]))"
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
              <div class="text-caption text-grey-7">
                $ {{ formatPrice(p.precioVenta) }}
              </div>
            </div>

            <!-- Acciones -->
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

function getFotoUrlFromFoto(foto) {
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

function formatPrice(v) {
  return (v || 0).toLocaleString('es-ES', { minimumFractionDigits: 2 })
}
</script>

<style scoped>
.wishlist-page {
  max-width: 1100px;
  margin: 0 auto;
  margin-top: 32px; /* separación del TopBar */
}

.wishlist-card {
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.wishlist-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 16px rgba(0,0,0,0.15);
}

.wishlist-img {
  width: 100px;
  height: 80px;
  object-fit: cover;
  border-radius: 6px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  transition: transform 0.2s ease;
}
.wishlist-img:hover {
  transform: scale(1.05);
}
</style>
