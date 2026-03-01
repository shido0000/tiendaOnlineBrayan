<template>
  <q-page padding>
    <div class="text-h5 q-mb-lg">Editar Información del Sitio</div>

    <q-form @submit.prevent="guardarInfo">
      <!-- Sobre Nosotros -->
      <q-input
        v-model="info.sobreNosotros"
        type="textarea"
        label="Sobre Nosotros"
        autogrow
        counter
        outlined
        class="q-mb-md"
      />

      <!-- Política de Privacidad -->
      <q-input
        v-model="info.privacidad"
        type="textarea"
        label="Política de Privacidad"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!-- Términos y Condiciones -->
      <q-input
        v-model="info.terminos"
        type="textarea"
        label="Términos y Condiciones"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!-- Política de Devoluciones y Cambios -->
      <q-input
        v-model="info.devoluciones"
        type="textarea"
        label="Política de Devoluciones y Cambios"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!-- Colabora con Nosotros -->
      <q-input
        v-model="info.colabora"
        type="textarea"
        label="Colabora con Nosotros"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!--Direccion tienda -->
      <q-input
        v-model="info.direccionTienda"
        type="textarea"
        label="Dirección de la tienda"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!--Horario tienda -->
      <q-input
        v-model="info.horarioTienda"
        type="textarea"
        label="Horario de la tienda"
        autogrow
        outlined
        counter
        class="q-mb-md"
      />

      <!--Telefono tienda -->
       <q-input
  v-model="info.telefonoTienda"
  label="Teléfono de la tienda"
  outlined
  counter
  maxlength="20"
  class="q-mb-md"
  @keypress="validarKeypress"
/>

<div class="row col-12">
  <!--Enlace telegram -->
      <q-input
        v-model="info.enlaceTelegram"
        type="textarea"
        label="Enlace de Telegram (usuario)"
        autogrow
        outlined
        maxlength="150"
        counter
        class="q-mb-md col-xs-12 col-sm-12 col-md-3 q-pr-sm"
      />
        <!--Enlace facebook -->
      <q-input
        v-model="info.enlaceFacebook"
        type="textarea"
        label="Enlace de Facebook (usuario)"
        autogrow
        outlined
        maxlength="150"
        counter
        class="q-mb-md col-xs-12 col-sm-12 col-md-3 q-pr-sm"
      />
        <!--Enlace whatsapp -->
      <q-input
        v-model="info.enlaceWhatsapp"
        type="textarea"
        label="Enlace de Whatsapp (teléfono)"
        autogrow
        outlined
        maxlength="150"
        counter
        class="q-mb-md col-xs-12 col-sm-12 col-md-3 q-pr-sm"
      />
        <!--Enlace instagram -->
      <q-input
        v-model="info.enlaceInstagram"
        type="textarea"
        label="Enlace de Instagram (usuario)"
        autogrow
        outlined
        maxlength="150"
        counter
        class="q-mb-md col-xs-12 col-sm-12 col-md-3"
      />
</div>

      <div class="row justify-end q-mt-lg">
        <q-btn label="Guardar" color="primary" type="submit" />
      </div>
    </q-form>
  </q-page>
        <DialogLoad :dialogLoad="dialogLoad" />

</template>

<script setup>
import { reactive, ref } from 'vue'
import { useQuasar } from 'quasar'
import { loadGet, obtener, saveData, saveDataSinCerrar } from 'src/assets/js/util/funciones'
import { onMounted } from 'vue'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'

const $q = useQuasar()
const dialogLoad = ref(false)
const items = ref([])

// Estado local con los textos

const objetoInicial = {
sobreNosotros: '',
  privacidad: '',
  terminos: '',
  devoluciones: '',
  colabora: '',
  direccionTienda: '',
  horarioTienda: '',
  telefonoTienda: '',
  enlaceTelegram: '',
  enlaceFacebook: '',
  enlaceWhatsapp: '',
  enlaceInstagram: '',
}
const info = reactive({ ...objetoInicial })

const guardarInfo = async() => {
  const url = info.id ? 'InformacionGeneral/Actualizar' : 'InformacionGeneral/Crear'
  dialogLoad.value = true
  await saveDataSinCerrar(url, info, load, dialogLoad)
  dialogLoad.value = false
}

onMounted(async () => {
  dialogLoad.value = true
 await load()
  Object.assign(info, items.value[0])
  dialogLoad.value = false
})

const load = async () => {
  items.value = await loadGet('InformacionGeneral/ObtenerListadoPaginado')??[]
}

const validarKeypress = (event) => {
  const char = event.key
  const regex = /^[0-9+]$/

  // Si el carácter no es número ni '+', bloqueamos
  if (!regex.test(char)) {
    event.preventDefault()
  }

  // Opcional: permitir '+' solo al inicio
  if (char === '+' && event.target.selectionStart > 0) {
    event.preventDefault()
  }
}

</script>
