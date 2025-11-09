<template>
  <header class="visual-topbar bg-white shadow-1 q-pa-sm">
    <div class="header-grid">
      <div class="left-col">
        <q-btn flat round dense icon="menu" class="mobile-menu-btn" v-if="$q.screen.lt.md" @click="$emit('update:leftDrawer', true)" />
        <a @click.prevent="goHome" style="cursor:pointer">
          <img src="/public/img/Logotipo.png" alt="Logo" class="topbar-logo"/>
        </a>
      </div>

      <div class="center-col">
        <div class="search-pill">
          <q-btn-dropdown
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

          <div class="divider" />

          <div class="search-input-wrap">
            <q-input v-model="q" dense placeholder="Buscar..." borderless class="search-input" @keyup.enter="search">
              <template #append>
                <q-btn flat round icon="search" color="primary" class="search-icon" @click="search" />
              </template>
            </q-input>
          </div>
        </div>
      </div>

      <div class="right-col">
        <WishlistButton />
        <CartButton />
        <q-btn flat round dense color="primary" icon="person" @click="goLogin" />
      </div>
    </div>

    <!-- secondary nav (left-aligned) -->
    <div class="topbar-nav q-pt-sm q-pb-sm">
      <div class="row items-center">
        <div class="col row items-center">
          <q-btn-dropdown color="transparent" text-color="black" flat class="category-btn" :dense="true">
            <template #label>
              <div class="cat-label"><span class="cat-label-text">Categorías</span></div>
            </template>
            <q-list>
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

          <q-btn flat label="Novedades" class="q-ml-md" @click="() => router.push({ name: 'IndexPage', query: { novedades: 1 } })" />
          <q-btn flat label="Rebajas" class="q-ml-sm" @click="() => router.push({ name: 'IndexPage', query: { rebajas: 1 } })" />
        </div>
      </div>
    </div>
  </header>
</template>
<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import WishlistButton from 'src/components/WishlistButton.vue'
import CartButton from 'src/components/CartButton.vue'
import { loadGetDatosInicio } from 'src/assets/js/util/funciones'

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
  router.push({ name: 'LoginPage' }).catch(() => router.push('/login'))
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
</script>

<style scoped>
.visual-topbar { background: #fff; }
.topbar-logo { max-height: 56px; margin-right: 8px }
.header-grid {
  display: grid;
  grid-template-columns: auto 1fr auto;
  grid-template-areas: 'left center right';
  align-items: center;
  gap: 12px;
}
.left-col { grid-area: left; display:flex; align-items:center; gap:8px }
.center-col { grid-area: center; }
.right-col { grid-area: right; display:flex; align-items:center; gap:8px }

.search-pill { display:flex; align-items:center; gap:8px; background: #fff; border-radius: 999px; padding: 6px 10px; height:48px; box-shadow: 0 6px 18px rgba(0,0,0,0.06); }
.category-btn { margin: 0 6px; }
.fixed-cat-btn { width: 180px; min-width: 120px }
.cat-label { display:flex; align-items:center }
.cat-label-text { display:inline-block; max-width: 140px; overflow:hidden; text-overflow:ellipsis; white-space:nowrap }
.divider { width:1px; height:28px; background: rgba(0,0,0,0.06) }
.search-input-wrap { display:flex; align-items:center; gap:8px; flex:1 1 auto; min-width:0 }
.search-input { flex:1 1 auto; min-width:0 }
.search-icon { flex:0 0 auto }

.mobile-menu-btn { display:none }
@media (max-width: 1024px) {
  .header-grid { grid-template-columns: auto 1fr; grid-template-areas: 'left center'; }
  .mobile-menu-btn { display:inline-flex }
  .right-col { display:none }
}

.topbar-nav { padding: 8px 16px; background: #fff; box-shadow: 0 1px 4px rgba(0,0,0,0.04); }
</style>

