<template>
  <q-page class="row bg-grey-1">
    <!-- Menú lateral -->
    <aside class="col-3 q-pa-md bg-white shadow-2">
      <div class="text-h6 text-weight-bold q-mb-md text-primary">Información</div>
      <q-list bordered separator>
        <q-item clickable v-ripple @click="scrollTo('sobre-nosotros')">
          <q-item-section avatar><q-icon name="business" color="primary" /></q-item-section>
          <q-item-section>Sobre Nosotros</q-item-section>
        </q-item>
        <q-item clickable v-ripple @click="scrollTo('privacidad')">
          <q-item-section avatar><q-icon name="lock" color="primary" /></q-item-section>
          <q-item-section>Política de Privacidad</q-item-section>
        </q-item>
        <q-item clickable v-ripple @click="scrollTo('terminos')">
          <q-item-section avatar><q-icon name="gavel" color="primary" /></q-item-section>
          <q-item-section>Términos y Condiciones</q-item-section>
        </q-item>
        <q-item clickable v-ripple @click="scrollTo('devoluciones')">
          <q-item-section avatar><q-icon name="autorenew" color="primary" /></q-item-section>
          <q-item-section>Devoluciones y Cambios</q-item-section>
        </q-item>
        <q-item clickable v-ripple @click="scrollTo('colabora')">
          <q-item-section avatar><q-icon name="group" color="primary" /></q-item-section>
          <q-item-section>Colabora con Nosotros</q-item-section>
        </q-item>
      </q-list>
    </aside>

    <!-- Contenido principal -->
    <main class="col-9 q-pa-lg">
      <q-card flat bordered class="q-mb-xl section-card" id="sobre-nosotros">
        <q-card-section>
          <div class="text-h5 text-primary text-weight-bold q-mb-sm">Sobre Nosotros</div>
          <div class="text-body1">{{ info.sobreNosotros }}</div>
        </q-card-section>
      </q-card>

      <q-card flat bordered class="q-mb-xl section-card" id="privacidad">
        <q-card-section>
          <div class="text-h5 text-primary text-weight-bold q-mb-sm">Política de Privacidad</div>
          <div class="text-body1">{{ info.privacidad }}</div>
        </q-card-section>
      </q-card>

      <q-card flat bordered class="q-mb-xl section-card" id="terminos">
        <q-card-section>
          <div class="text-h5 text-primary text-weight-bold q-mb-sm">Términos y Condiciones</div>
          <div class="text-body1">{{ info.terminos }}</div>
        </q-card-section>
      </q-card>

      <q-card flat bordered class="q-mb-xl section-card" id="devoluciones">
        <q-card-section>
          <div class="text-h5 text-primary text-weight-bold q-mb-sm">Política de Devoluciones y Cambios</div>
          <div class="text-body1">{{ info.devoluciones }}</div>
        </q-card-section>
      </q-card>

      <q-card flat bordered class="q-mb-xl section-card" id="colabora">
        <q-card-section>
          <div class="text-h5 text-primary text-weight-bold q-mb-sm">Colabora con Nosotros</div>
          <div class="text-body1">{{ info.colabora }}</div>
        </q-card-section>
      </q-card>
    </main>
  </q-page>

  <DialogLoad :dialogLoad="dialogLoad" />
</template>

<script setup>
import { nextTick, onMounted, reactive, ref } from 'vue'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import { loadGet } from 'src/assets/js/util/funciones'

const dialogLoad = ref(false)
const items = ref([])
const objetoInicial = {
  sobreNosotros: '',
  privacidad: '',
  terminos: '',
  devoluciones: '',
  colabora: ''
}
const info = reactive({ ...objetoInicial })

const scrollTo = async (id) => {
  await nextTick()
  const el = document.getElementById(id)
  if (el) {
    el.scrollIntoView({ behavior: 'smooth' })
  }
}

onMounted(async () => {
  dialogLoad.value = true
  items.value = await loadGet('InformacionGeneral/ObtenerListadoPaginado') ?? []
  Object.assign(info, items.value[0])
  dialogLoad.value = false
})
</script>

<style scoped>
.section-card {
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}
.section-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 4px 20px rgba(0,0,0,0.12);
}
aside {
  position: sticky;
  top: 0;
  height: 100vh;
  overflow-y: auto;
  border-right: 1px solid #eee;
}
</style>
