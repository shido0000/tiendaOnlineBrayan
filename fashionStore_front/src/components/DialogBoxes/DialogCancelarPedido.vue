<template>
  <q-dialog v-model="abrirDialog">
    <q-card style="width: 600px; max-width: 80vw;">
      <q-card-section class="row">
                <div class="col-1">
                <q-avatar icon="warning" color="primary" text-color="white" />
                </div>
              <div class="col-10 q-ml-md q-mt-md text-weight-bold">
                ¿Está seguro que desea cancelar el pedido seleccionado?
              </div>
            </q-card-section>
            <q-card-actions class="q-pb-md" align="right">
              <q-btn flat
              class="bg-primary"
              outline
              size="12px"
              label="Aceptar"
              color="white"
              @click= confirmarEliminacion()
               />
              <q-btn
                  class="q-mr-md"
                  outline
                  color="primary"
                  label="Cancelar"
                  size="12px"
                  v-close-popup
              />
            </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup>
import { ref, defineProps, defineEmits, watch } from 'vue'

const props = defineProps({
  isOpen: {
    type: Boolean,
    required: true
  },
  idElemento: {
    type: String,
    required: false,
    default: ""
  }
})
const abrirDialog = ref(props.isOpen)
watch(abrirDialog, (newVal) => {
  if (!newVal) {
    emit('closeDialog')
  }
})

const emit = defineEmits(['eliminar', 'closeDialog'])

const confirmarEliminacion = () => {
  emit('eliminar', props.idElemento)
  abrirDialog.value = false
  emit('closeDialog')
}
</script>
