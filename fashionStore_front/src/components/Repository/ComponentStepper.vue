<template>
      <q-stepper v-model="step" ref="stepper" color="primary" animated>
                <q-step :name="1" title="Step-1" :done="step > 1">
                  <div class="h row q-ma-md">
                  </div>
                </q-step>

                <q-step :name="2" title="Step-2" :done="step > 2">
                  <div class="h row q-ma-md">
                  </div>
                </q-step>

                <q-step :name="3" title="Step-3">
                  <div class="h row q-ma-md">
                  </div>
                </q-step>
              </q-stepper>ç
              <div class="col-xs-12 q-mx-md q-py-md" align="right">
                      <q-btn color="primary"
                        :label="step === 3 || objeto.id !== null ? 'Guardar' : 'Siguiente'" @click="avanzar()"/>
                      <q-btn v-if="step > 1" flat color="primary" label="Atrás" @click="atras()"
                        class="q-ml-sm" />
                      <q-btn class="q-mx-sm" outline color="primary" type="reset" label="Cancelar" />
                  </div>

</template>

<script setup>
import { ref } from 'vue'
const step = ref(1)
</script>
