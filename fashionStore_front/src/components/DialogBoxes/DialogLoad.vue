<template>
  <q-dialog v-model="dialog" persistent>
    <q-layout container class="bg-white" style="height: 80px; max-width: 400px">
      <q-card class="bg-primary text-white">
        <q-card-section class="q-pb-none">
          <div class="text-subtitle1 text-center">
            <span>Cargando...</span>
          </div>
        </q-card-section>
        <q-card-section>
          <div class="row">
            <div class="col-12">
              <q-linear-progress color="white" indeterminate/>
            </div>
          </div>
        </q-card-section>
      </q-card>
    </q-layout>
  </q-dialog>
</template>

<script>
import { computed } from 'vue'
export default {
  props: {
    dialogLoad: Boolean
  },
  setup (props) {
    const dialog = computed(() => props.dialogLoad)
    return {
      dialog
    }
  }
}
</script>
