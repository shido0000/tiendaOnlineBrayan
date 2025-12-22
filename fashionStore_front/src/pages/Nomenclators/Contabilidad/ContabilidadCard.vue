<template>
  <q-card class="cursor-pointer card-container" @click="irAContabilidad">
    <q-card-section class="text-center">
      <div class="icon-container">
        <q-icon name="account_balance" size="64px" color="primary" />
      </div>
      <div class="text-h6 q-mt-md">Contabilidad</div>
      <p class="text-caption text-grey">Gestiona cuentas y asientos contables</p>
    </q-card-section>
    <q-card-section class="q-pt-none">
      <div class="row q-gutter-md">
        <q-btn
          flat
          size="sm"
          label="Cuentas"
          color="primary"
          class="col-grow"
          @click.stop="$router.push('/CuentasContables')"
        />
        <q-btn
          flat
          size="sm"
          label="Asientos"
          color="secondary"
          class="col-grow"
          @click.stop="$router.push('/AsientosContables')"
        />
        <q-btn
          flat
          size="sm"
          label="Reportes"
          color="positive"
          class="col-grow"
          @click.stop="$router.push('/ReporteContable')"
        />
      </div>
    </q-card-section>
  </q-card>
</template>

<script setup>
import { useRouter } from 'vue-router'

const $router = useRouter()

const irAContabilidad = () => {
  $router.push('/Contabilidad')
}
</script>

<style scoped>
.card-container {
  transition: all 0.3s ease;
  height: 100%;
}

.card-container:hover {
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
  transform: translateY(-4px);
}

.icon-container {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
