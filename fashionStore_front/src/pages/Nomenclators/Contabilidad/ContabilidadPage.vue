<template>
  <div class="q-pa-xl">
    <!-- Breadcrumbs -->
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />

      <q-breadcrumbs-el label="Contabilidad" />
    </q-breadcrumbs>

    <!-- Título -->
    <div class="row items-center q-mb-lg">
      <div class="col-12">
        <h4 class="q-my-none">
          <q-icon name="account_balance" color="primary" size="lg" />
          Gestión Contable
        </h4>
        <p class="text-caption text-grey q-mt-sm">Administra cuentas, asientos contables y reportes</p>
      </div>
    </div>

    <!-- Cards de opciones principales -->
    <div class="row q-gutter-lg q-mb-lg">
      <!-- Card Cuentas Contables -->
      <q-card
        class="col-xs-12 col-sm-12 col-md-6 col-lg-3 cursor-pointer card-option"
        @click="$router.push('/CuentasContables')"
      >
        <q-card-section class="text-center">
          <q-icon name="account_balance_wallet" size="48px" color="primary" />
          <h6 class="q-my-md">Cuentas Contables</h6>
          <p class="text-caption text-grey">
            Crea, edita y organiza la estructura de cuentas contables
          </p>
        </q-card-section>
        <q-separator />
        <q-card-section class="text-center">
          <q-btn
            flat
            color="primary"
            label="Acceder"
            icon-right="arrow_forward"
          />
        </q-card-section>
      </q-card>

      <!-- Card Asientos Contables -->
      <q-card
        class="col-xs-12 col-sm-12 col-md-6 col-lg-3 cursor-pointer card-option"
        @click="$router.push('/AsientosContables')"
      >
        <q-card-section class="text-center">
          <q-icon name="receipt_long" size="48px" color="secondary" />
          <h6 class="q-my-md">Asientos Contables</h6>
          <p class="text-caption text-grey">
            Visualiza y gestiona todos los asientos contables generados
          </p>
        </q-card-section>
        <q-separator />
        <q-card-section class="text-center">
          <q-btn
            flat
            color="secondary"
            label="Acceder"
            icon-right="arrow_forward"
          />
        </q-card-section>
      </q-card>

      <!-- Card Reportes Contables -->
      <q-card
        class="col-xs-12 col-sm-12 col-md-6 col-lg-3 cursor-pointer card-option"
        @click="$router.push('/ReporteContable')"
      >
        <q-card-section class="text-center">
          <q-icon name="bar_chart" size="48px" color="positive" />
          <h6 class="q-my-md">Reportes Contables</h6>
          <p class="text-caption text-grey">
            Analiza y visualiza reportes contables detallados
          </p>
        </q-card-section>
        <q-separator />
        <q-card-section class="text-center">
          <q-btn
            flat
            color="positive"
            label="Acceder"
            icon-right="arrow_forward"
          />
        </q-card-section>
      </q-card>
    </div>

    <!-- Información de ayuda -->
    <q-card class="bg-blue-1">
      <q-card-section>
        <div class="row items-start q-gutter-md">
          <q-icon name="info" color="info" size="md" />
          <div class="col">
            <h6 class="q-my-none">Información útil</h6>
            <p class="q-my-sm text-caption">
              El módulo de contabilidad permite gestionar todas las operaciones contables del sistema.
              Comienza configurando tus cuentas contables, luego revisa los asientos generados
              automáticamente por las transacciones del negocio.
            </p>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
// Este componente es un dashboard/índice de contabilidad
// Actúa como punto de entrada al módulo de contabilidad
</script>

<style scoped>


.card-option {
  transition: all 0.3s ease;
}

.card-option:hover {
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
  transform: translateY(-4px);
}
</style>
