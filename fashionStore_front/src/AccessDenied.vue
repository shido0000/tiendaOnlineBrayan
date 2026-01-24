<template>
  <div class="fullscreen bg-primary text-white text-center q-pa-md flex flex-center">
    <div>
      <q-icon name="lock" style="font-size: 30vh" />

      <div class="text-h2 q-mt-md">
        Acceso Denegado
      </div>

      <div class="text-h6" style="opacity:.7; margin-top: 20px;">
        No tienes permiso para acceder a esta sección
      </div>

      <q-btn
       flat
       style="border: 1px solid #C7B5FF; border-radius: 15px; box-shadow: 0 2px 6px rgba(0,0,0,0.45);"
        class="q-mt-xl bg-white"
        text-color="black"
        unelevated
        to="/"
        label="Ir al Inicio"
        no-caps
        size="lg"
      />
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'AccessDenied'
})
</script>

<style scoped>
.fullscreen {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}
</style>
