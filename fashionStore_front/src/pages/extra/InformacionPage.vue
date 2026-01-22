<template>
  <div>
    <!-- Topbar opcional para que se vea bien -->
    <TopBar :categories="[]" />
 <q-list   bordered class="rounded-borders col-12" >
      <q-expansion-item
        icon="business"
        label="Sobre Nosotros"
        group="somegroup"
      >
        <q-card class="bg-grey-1">
          <q-card-section>
          {{ info.sobreNosotros }}
          </q-card-section>
        </q-card>
      </q-expansion-item>

      <q-expansion-item
        icon="lock"
        label="Política de Privacidad"
        group="somegroup"
      >
        <q-card class="bg-grey-1">
          <q-card-section>
          {{ info.privacidad }}
          </q-card-section>
        </q-card>
      </q-expansion-item>

      <q-expansion-item
        icon="gavel"
        label="Términos y Condiciones"
        group="somegroup"
      >
        <q-card class="bg-grey-1">
          <q-card-section>
           {{ info.terminos }}
          </q-card-section>
        </q-card>
      </q-expansion-item>
      <q-expansion-item
        icon="autorenew"
        label="Devoluciones y Cambios"
        group="somegroup"
      >
        <q-card class="bg-grey-1">
          <q-card-section>
          {{ info.devoluciones }}
          </q-card-section>
        </q-card>
      </q-expansion-item>
      <q-expansion-item
        icon="group"
        label="Colabora con Nosotros"
        group="somegroup"
      >
        <q-card class="bg-grey-1">
          <q-card-section>
           {{ info.colabora }}
          </q-card-section>
        </q-card>
      </q-expansion-item>
    </q-list>

    <DialogLoad :dialogLoad="dialogLoad" />
  </div>
</template>

<script setup>
import { nextTick, onMounted, reactive, ref } from 'vue'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import TopBar from 'src/pages/Visual/components/TopBar.vue'
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

<style scoped lang="scss">


.info-sidebar {
  width: 25%;
  padding: 24px;
  overflow-y: auto;
  border-right: 1px solid #eee;
  position: sticky;
  top: 140px;
  height: calc(100vh - 140px);
  box-sizing: border-box;
}

.info-main {
  flex: 1;
  overflow-y: auto;
  padding: 24px;
}

.section-card {
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  max-width: 900px;
}

.section-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 4px 20px rgba(0,0,0,0.12);
}

/* Extra small devices (xs) - 0px to 599px */
@media (max-width: 599px) {
  .info-container {
    flex-direction: column;
    min-height: auto;
  }

  .info-sidebar {
    width: 100%;
    position: static;
    height: auto;
    border-right: none;
    border-bottom: 1px solid #eee;
    padding: 16px;
    max-height: 300px;
    overflow-y: auto;
  }

  .info-main {
    padding: 12px;
  }

  .text-h5 {
    font-size: 18px !important;
  }

  .text-h6 {
    font-size: 16px !important;
  }

  .text-body1 {
    font-size: 14px !important;
    line-height: 1.5;
  }

  :deep(.q-item) {
    padding: 8px 12px;
  }

  :deep(.q-item__section--avatar) {
    min-width: 32px;
  }

  :deep(.q-icon) {
    font-size: 20px;
  }
}

/* Small devices (sm) - 600px to 1023px */
@media (min-width: 600px) and (max-width: 1023px) {
  .info-container {
    flex-direction: row;
  }

  .info-sidebar {
    width: 30%;
    padding: 16px;
  }

  .info-main {
    flex: 1;
    padding: 16px;
  }

  .text-h5 {
    font-size: 20px !important;
  }

  .text-h6 {
    font-size: 17px !important;
  }

  .text-body1 {
    font-size: 14px !important;
  }

  :deep(.q-item) {
    padding: 10px 12px;
  }
}

/* Medium devices (md) - 1024px to 1365px */
@media (min-width: 1024px) and (max-width: 1365px) {
  .info-sidebar {
    width: 28%;
    padding: 20px;
  }

  .info-main {
    padding: 20px;
  }

  .text-h5 {
    font-size: 22px !important;
  }

  .text-body1 {
    font-size: 15px !important;
  }
}

/* Large devices (lg+) - 1366px and up */
@media (min-width: 1366px) {
  .info-sidebar {
    width: 25%;
    padding: 24px;
  }

  .info-main {
    padding: 24px;
  }

  .text-h5 {
    font-size: 24px !important;
  }

  .text-body1 {
    font-size: 16px !important;
  }
}

</style>
