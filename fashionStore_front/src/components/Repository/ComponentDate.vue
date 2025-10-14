<template>
<q-input class="col-xs-12 q-pa-sm" outline label="Fecha *" readonly v-model="fecha"
                  lazy-rules :rules="[
                    (val) => (val !== null) || 'Debe insertar una fecha',
                  ]">

                  <template v-slot:append>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy ref="cerrar" cover transition-show="scale" transition-hide="scale">
                        <q-date @update:modelValue="hidePopup" v-model="objeto.fecha" mask="YYYY-MM-DD"
                          :options="date => new Date(date) <= new Date()">
                          <div class="row items-center justify-end">
                            <q-btn v-close-popup label="Cerrar" color="primary" flat />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>

</template>

<script setup>
import { ref } from 'vue'
const cerrar = ref(null)
const hidePopup = () => {
  cerrar.value.hide()
}
</script>
