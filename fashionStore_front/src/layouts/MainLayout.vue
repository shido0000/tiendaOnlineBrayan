<template style="background-color: #686262">
  <q-layout view="hHh Lpr lff" id="mainLayout">
    <q-header class="text-white" style="height: 60px">
      <q-toolbar class="ll">
        <div class="h row">
          <q-btn
            size="md"
            class="qqq"
            flat
            @click="drawer = !drawer"
            round
            dense
            icon="menu"
            ><q-tooltip>{{
              drawer ? "Cerrar Menú" : "Abrir Menú"
            }}</q-tooltip></q-btn
          >
          <q-toolbar-title class="text-subtitle6 text-white">
            FASHION STORE</q-toolbar-title
          >
        </div>
        <q-space />
      <!-- Botón de Logout -->
        <q-btn
          class="q-mr-xl"
          flat
          round
          dense
          icon="logout"
          @click="onLogout"
        >
          <q-tooltip>Salir</q-tooltip>
        </q-btn>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="drawer"
      show-if-above
      :width="275"
      :breakpoint="500"
      class="fondo"
    >
      <q-scroll-area class="fit">
        <q-list padding class="qq menu-list">
          <EssentialLink />
        </q-list>
      </q-scroll-area>
    </q-drawer>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, ref } from 'vue'
import { emp } from 'src/boot/axios'
import EssentialLink from 'components/EssentialLink.vue'
import { useRouter } from 'vue-router'

export default defineComponent({
  name: 'MainLayout',

  components: {
    EssentialLink
  },


  setup () {
     const router = useRouter()
    const leftDrawerOpen = ref(false)

      const onLogout = () => {
      // Aquí puedes limpiar tokens, storage, etc.
      localStorage.removeItem('token')
      sessionStorage.clear()

      // Redirigir al login
      router.push('/login')
    }
    return {
      emp,
      displayHtml: ref(false),
      drawer: ref(false),
      miniState: ref(true),
      leftDrawerOpen,
      toggleLeftDrawer () {
        leftDrawerOpen.value = !leftDrawerOpen.value
      },
      onLogout
    }
  }
})

</script>
<style>
.h {
  justify-content: space-between;
}

.fondo {
  background-color: white;
}

.fondo.custom-drawer {
  transition: transform 1s ease-in-out;
}
</style>
