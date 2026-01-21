<template>
  <div class="topbar-container">
    <header class="visual-topbar bg-white shadow-1">
    <div class="header-grid">
      <div class="left-col">
        <q-btn
          flat
          round
          dense
          icon="menu"
          class="mobile-menu-btn"
          v-if="$q.screen.lt.md"
          ref="mobileMenuBtn"
        >
          <!-- Mobile Menu -->
          <q-menu
            auto-close
            anchor="bottom left"
            self="top left"
            class="mobile-menu"
            transition-show="slide-right"
            transition-hide="slide-left"
          >
            <q-list style="min-width: 280px">
              <q-item-label header class="text-h6 q-mb-md">Menú</q-item-label>

              <!-- Inicio Button in Mobile Menu -->
              <q-item clickable class="mobile-menu-item q-mb-sm" @click="router.push({ name: 'IndexPage' })">
                <q-item-section avatar>
                  <q-icon name="home" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Inicio</q-item-label>
                </q-item-section>
              </q-item>

              <!-- Wishlist Button in Mobile Menu -->
              <q-item clickable class="mobile-menu-item q-mb-sm" @click="goToWishlist">
                <q-item-section avatar>
                  <q-icon name="favorite_border" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Mi Lista de Deseos</q-item-label>
                </q-item-section>
              </q-item>

              <!-- Cart Button in Mobile Menu -->
              <q-item clickable class="mobile-menu-item q-mb-sm" @click="goToCart">
                <q-item-section avatar>
                  <q-icon name="shopping_cart" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Mi Carrito</q-item-label>
                </q-item-section>
              </q-item>

              <!-- User Profile in Mobile Menu -->
              <q-item clickable class="mobile-menu-item q-mb-md" @click="goLoginMobile">
                <q-item-section avatar>
                  <q-icon name="person" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Mi Perfil</q-item-label>
                </q-item-section>
              </q-item>

              <q-separator class="q-my-md" />

              <!-- Additional Navigation -->
              <q-item-label header class="text-subtitle2 q-mb-sm">Categorías</q-item-label>
              <q-item clickable @click="closeMobileMenu(() => router.push({ name: 'Categorias' }))">
                <q-item-section>
                  <q-item-label>Todas las Categorías</q-item-label>
                </q-item-section>
              </q-item>

              <template v-if="normalizedCategories && normalizedCategories.length">
                <q-item
                  v-for="(cat, index) in normalizedCategories.slice(0, 5)"
                  :key="'mobile-cat-'+(cat.id ?? cat.nombre ?? index)"
                  clickable
                  @click="closeMobileMenu(() => goToCategory(cat.id))"
                >
                  <q-item-section>
                    <q-item-label>{{ cat.nombre }}</q-item-label>
                  </q-item-section>
                </q-item>
              </template>

              <q-separator class="q-my-md" />

              <!-- Quick Links -->
              <q-item-label header class="text-subtitle2 q-mb-sm">Destacados</q-item-label>
              <q-item clickable @click="closeMobileMenu(() => router.push({ name: 'Productos', query: { novedades: 1 } }))">
                <q-item-section>
                  <q-item-label>Novedades</q-item-label>
                </q-item-section>
              </q-item>
              <q-item clickable @click="closeMobileMenu(() => router.push({ name: 'Productos', query: { rebajas: 1 } }))">
                <q-item-section>
                  <q-item-label>Rebajas</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-menu>
        </q-btn>
        <a @click.prevent="goHome" style="cursor:pointer">
          <img src="/public/img/Logotipo.png" alt="Logo" class="topbar-logo"/>
        </a>
      </div>

      <div class="center-col">
        <div class="search-pill">
        <!--  <q-btn-dropdown
            color="transparent"
            text-color="primary"
            flat
            class="category-btn fixed-cat-btn"
            :dense="true"
          >
            <template #label>
              <div class="cat-label">
                <span class="cat-label-text">
                  {{ selectedCatId == null ? 'Categorías' : (normalizedCategories.find(c => c.id === selectedCatId)?.nombre || 'Categorías') }}
                </span>
              </div>
            </template>

            <q-list>
              <q-item clickable v-close-popup @click="selectSearchCategory(null)">
                <q-item-section>
                  <q-item-label>Todos</q-item-label>
                </q-item-section>
              </q-item>

              <template v-if="normalizedCategories && normalizedCategories.length">
                <q-item
                  v-for="(cat, index) in normalizedCategories"
                  :key="'cat-header-'+(cat.id ?? cat.nombre ?? cat._uid ?? index)"
                  clickable
                  v-close-popup
                  @click="selectSearchCategory(cat)"
                >
                  <q-item-section>
                    <q-item-label>{{ cat.nombre }}</q-item-label>
                  </q-item-section>
                </q-item>
              </template>

              <template v-else>
                <q-item disabled>
                  <q-item-section>
                    <q-item-label>No hay categorías</q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-list>
          </q-btn-dropdown>

          <div class="divider" />-->

          <div class="search-input-wrap">
            <q-input v-model="q" dense placeholder="Buscar..." borderless class="search-input" @keyup.enter="search">
              <template #append>
                <q-btn flat round icon="search" color="primary" class="search-icon" @click="search" />
              </template>
            </q-input>
          </div>
        </div>
      </div>

      <div class="right-col desktop-actions">
        <WishlistButton />
        <CartButton />
        <q-btn flat round dense color="primary" icon="person" @click="goLogin" />
      </div>
    </div>

    <!-- secondary nav (left-aligned) -->
    <div class="topbar-nav">
      <div class="row items-center">
        <div class="col row items-center nav-items-container ">
          <q-btn-dropdown color="transparent" text-color="black" flat class="category-btn" :dense="true">
            <template #label>
              <div class="cat-label"><span class="cat-label-text">Categorías</span></div>
            </template>
            <q-list>
              <q-item clickable v-close-popup @click="() => router.push({ name: 'Categorias' })">
                <q-item-section>
                  <q-item-label>Todos</q-item-label>
                </q-item-section>
              </q-item>

              <template v-if="normalizedCategories && normalizedCategories.length">
                <q-item
                  v-for="(cat, index) in normalizedCategories"
                  :key="'nav-'+(cat.id ?? cat.nombre ?? index)"
                  clickable
                  @click="() => goToCategory(cat.id)"
                >
                  <q-item-section>{{ cat.nombre }}</q-item-section>
                </q-item>
              </template>
              <template v-else>
                <q-item disabled>
                  <q-item-section>
                    <q-item-label>No hay categorías</q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-list>
          </q-btn-dropdown>

          <q-btn   flat style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);" label="Novedades" class="nav-btn" @click="() => router.push({ name: 'Productos', query: { novedades: 1 } })" />
          <q-btn   flat style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);" label="Rebajas" class="nav-btn" @click="() => router.push({ name: 'Productos', query: { rebajas: 1 } })" />
        </div>
      </div>
    </div>
  </header>
  </div>
</template>
<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'
import WishlistButton from 'src/components/WishlistButton.vue'
import CartButton from 'src/components/CartButton.vue'
import { loadGetDatosInicio } from 'src/assets/js/util/funciones'

const $q = useQuasar()

const q = ref('')
const selectedCatId = ref(null)
const router = useRouter()
const emit = defineEmits(['update:searchCategory', 'update:leftDrawer'])

const categories = ref([])

onMounted(async () => {
  try {
    const inicio = await loadGetDatosInicio('ObtenerDatosInicio')
    categories.value =
      inicio?.categoriasProductos ??
      inicio?.categorias ??
      inicio?.data?.categorias ??
      inicio?.result?.categoriasProductos ??
      []
  } catch (e) {
    console.warn('Error cargando categorías', e)
    categories.value = []
  }
})

const normalizedCategories = computed(() => {
  const arr = categories.value || []
  return arr.map((c, idx) => {
    if (!c) return { id: null, nombre: String(c) }
    if (typeof c !== 'object') return { id: c, nombre: String(c) }
    const id = c.id ?? c.categoriaId ?? c._id ?? c.codigo ?? c.value ?? null
    const nombre =
      c.nombre ??
      c.Nombre ??
      c.label ??
      c.descripcion ??
      c.nombreCategoria ??
      c.name ??
      String(id ?? '')
    return { ...c, id, nombre }
  })
})

function goHome() {
  router.push({ name: 'IndexPage' }).catch(() => router.push('/'))
}
function goLogin() {
  // Verificar si el usuario está logueado
  const token = sessionStorage.getItem('token')

  if (token) {
    // Usuario logueado, ir al perfil
    router.push({ name: 'Perfil' }).catch(() => router.push('/perfil'))
  } else {
    // Usuario no logueado, ir al login
    router.push({ name: 'LoginPage' }).catch(() => router.push('/login'))
  }
}
function goToCategory(id) {
  if (!id) return
  router.push({ name: 'CategoriaProductos', params: { id } }).catch(() =>
    router.push('/categoria/' + id)
  )
}
function selectSearchCategory(cat) {
  selectedCatId.value = cat ? cat.id ?? cat : null
  try {
    emit('update:searchCategory', selectedCatId.value)
  } catch (e) {}
}
function search() {
  const params = {}
  if (q.value) params.q = q.value
  if (selectedCatId.value) params.cat = selectedCatId.value
  try {
    emit('update:searchCategory', selectedCatId.value)
  } catch (e) {}
  router.push({ name: 'Productos', query: params }).catch(() =>
    router.push('/productos')
  )
}

function goToCart() {
  router.push({ name: 'Carrito' }).catch(() => router.push('/carrito'))
}

function goToWishlist() {
  router.push({ name: 'ListaDeseos' }).catch(() => router.push('/lista-deseos'))
}

function goLoginMobile() {
  const token = sessionStorage.getItem('token')
  if (token) {
    router.push({ name: 'Perfil' }).catch(() => router.push('/perfil'))
  } else {
    router.push({ name: 'LoginPage' }).catch(() => router.push('/login'))
  }
}

function closeMobileMenu(callback) {
  if (callback) {
    setTimeout(() => callback(), 100)
  }
}
</script>

<style scoped>
.topbar-container {
  width: 100%;
}

.visual-topbar {
  background: #fff;
  padding: 8px 12px;
  box-sizing: border-box;
}

.topbar-logo {
  max-height: 56px;
  margin-right: 8px;
  object-fit: contain;
}

.header-grid {
  display: grid;
  grid-template-columns: auto 1fr auto;
  grid-template-areas: 'left center right';
  align-items: center;
  gap: 12px;
}

.left-col {
  grid-area: left;
  display: flex;
  align-items: center;
  gap: 8px;
}

.center-col {
  grid-area: center;
  min-width: 0;
}

.right-col {
  grid-area: right;
  display: flex;
  align-items: center;
  gap: 8px;
}

.search-pill {
  display: flex;
  align-items: center;
  gap: 8px;
  background: #fff;
  border-radius: 999px;
  padding: 6px 10px;
  height: 48px;
  box-shadow: 0 6px 18px rgba(0,0,0,0.06);
  box-sizing: border-box;
}

.category-btn {
  margin: 0 6px;
  white-space: nowrap;
}

.fixed-cat-btn {
  width: 180px;
  min-width: 120px;
}

.cat-label {
  display: flex;
  align-items: center;
}

.cat-label-text {
  display: inline-block;
  max-width: 140px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.9rem;
}

.divider {
  width: 1px;
  height: 28px;
  background: rgba(0,0,0,0.06);
}

.search-input-wrap {
  display: flex;
  align-items: center;
  gap: 8px;
  flex: 1 1 auto;
  min-width: 0;
}

.search-input {
  flex: 1 1 auto;
  min-width: 0;
}

.search-icon {
  flex: 0 0 auto;
}

.mobile-menu-btn {
  display: none;
}

.topbar-nav {
  padding: 8px 12px;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0,0,0,0.04);
  box-sizing: border-box;
}

.nav-items-container {
  gap: 0;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}

.nav-btn {
  white-space: nowrap;
  margin: 0 4px ;
}

.desktop-actions {
  display: flex;
}

.mobile-drawer {
  width: 100%;
  max-width: 280px;
}

.mobile-menu-item {
  transition: background-color 0.2s ease;
  border-radius: 4px;
}

.mobile-menu-item:hover {
  background-color: rgba(0, 0, 0, 0.05);
}

/* Extra small devices (xs) - 0px to 599px */
@media (max-width: 599px) {
  .visual-topbar {
    padding: 6px 8px;
  }

  .topbar-logo {
    max-height: 40px;
    margin-right: 6px;
  }

  .header-grid {
    gap: 8px;
  }

  .search-pill {
    height: 40px;
    padding: 4px 8px;
    gap: 6px;
  }

  .fixed-cat-btn {
    width: 120px;
    min-width: 100px;
    font-size: 0.8rem;
  }

  .cat-label-text {
    max-width: 80px;
    font-size: 0.75rem;
  }

  .divider {
    height: 24px;
  }

  .search-input {
    font-size: 0.8rem;
  }

  .mobile-menu-btn {
    display: inline-flex;
  }

  .right-col {
    gap: 6px;
  }

  .desktop-actions {
    display: none;
  }

  .topbar-nav {
    padding: 6px 8px;
    overflow-x: auto;
    -webkit-overflow-scrolling: touch;
  }

  .nav-btn {
    font-size: 0.75rem;
    padding: 4px 8px;
    margin: 0 2px;
  }

  .category-btn {
    margin: 0 4px;
    font-size: 0.75rem;
  }
}

/* Small devices (sm) - 600px to 1023px */
@media (min-width: 600px) and (max-width: 1023px) {
  .visual-topbar {
    padding: 8px 12px;
  }

  .topbar-logo {
    max-height: 48px;
    margin-right: 8px;
  }

  .search-pill {
    height: 44px;
    padding: 5px 10px;
  }

  .fixed-cat-btn {
    width: 150px;
    min-width: 110px;
  }

  .cat-label-text {
    max-width: 120px;
    font-size: 0.85rem;
  }

  .search-input {
    font-size: 0.85rem;
  }

  .mobile-menu-btn {
    display: none;
  }

  .topbar-nav {
    padding: 8px 12px;
  }

  .nav-btn {
    font-size: 0.85rem;
    padding: 6px 12px;
    margin: 0 6px;
  }

  .category-btn {
    margin: 0 6px;
    font-size: 0.85rem;
  }

  .right-col {
    gap: 8px;
  }
}

/* Medium devices (md) - 1024px to 1365px */
@media (min-width: 1024px) and (max-width: 1365px) {
  .visual-topbar {
    padding: 8px 16px;
  }

  .topbar-logo {
    max-height: 52px;
    margin-right: 10px;
  }

  .search-pill {
    height: 46px;
    padding: 6px 12px;
  }

  .fixed-cat-btn {
    width: 160px;
    min-width: 120px;
  }

  .cat-label-text {
    max-width: 130px;
    font-size: 0.9rem;
  }

  .topbar-nav {
    padding: 8px 16px;
  }

  .nav-btn {
    font-size: 0.9rem;
    padding: 6px 16px;
    margin: 0 8px;
  }

  .category-btn {
    margin: 0 8px;
    font-size: 0.9rem;
  }

  .mobile-menu-btn {
    display: none;
  }

  .desktop-actions {
    display: flex;
  }
}

/* Large devices (lg+) - 1366px and up */
@media (min-width: 1366px) {
  .visual-topbar {
    padding: 8px 20px;
  }

  .topbar-logo {
    max-height: 56px;
    margin-right: 12px;
  }

  .search-pill {
    height: 48px;
    padding: 6px 12px;
  }

  .fixed-cat-btn {
    width: 180px;
    min-width: 130px;
  }

  .cat-label-text {
    max-width: 140px;
    font-size: 0.95rem;
  }

  .topbar-nav {
    padding: 10px 20px;
  }

  .nav-btn {
    font-size: 0.95rem;
    padding: 6px 20px;
    margin: 0 12px;
  }

  .category-btn {
    margin: 0 12px;
    font-size: 0.95rem;
  }

  .mobile-menu-btn {
    display: none;
  }

  .desktop-actions {
    display: flex;
  }
}
</style>

