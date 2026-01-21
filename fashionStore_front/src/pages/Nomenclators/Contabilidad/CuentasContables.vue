<template>
  <div class="q-pa-xl">
    <q-breadcrumbs class="qb cursor-pointer q-pb-md">
      <q-breadcrumbs-el label="Inicio" icon="home" @click="$router.push('/')" />
      <q-breadcrumbs-el
        label="Nomencladores"
        icon="dashboard"
        @click="$router.push('/NomenclatorsCard')"
      />
        <q-breadcrumbs-el
        label="Contabilidad"
        icon="dashboard"
        @click="$router.push('/Contabilidad')"
      />
      <q-breadcrumbs-el label="Cuentas Contables" />
    </q-breadcrumbs>

      <div class="row items-center q-mb-lg">
        <div class="col-12">
          <h5 class="q-my-none">Cuentas Contables</h5>
        </div>
      </div>

      <div class="row q-mb-lg">
        <div class="col-12">
         <!-- <q-btn
            color="primary"
            icon="add"
            label="Nueva Cuenta"
            @click="abrirDialogoCrear()"
            class="q-mr-md"
          />-->
          <q-input
            outlined
            v-model="filter"
            label="Buscar cuenta..."
            debounce="500"
            class="col-3"
            dense
          />
        </div>
      </div>

      <div class="row q-gutter-md">
        <q-tree
          class="col-12"
          :nodes="cuentasArbolizadas"
          node-key="id"
          selected-color="primary"
          :filter="filter"
          @update:selected="seleccionarCuenta"
        >
          <template v-slot:default-header="prop">
            <div class="row items-center" style="width: 100%">
              <div class="col">
                <span class="text-weight-bold">{{ prop.node.label }}</span>
                <q-chip
                  size="sm"
                  :color="prop.node.esActivo ? 'green' : 'grey'"
                  text-color="white"
                  class="q-ml-md"
                >
                  {{ prop.node.esActivo ? "Activo" : "Inactivo" }}
                </q-chip>
              </div>
              <q-space />
              <q-btn
                flat
                dense
                round
                icon="edit"
                size="sm"
                @click="editarCuenta(prop.node)"
                class="q-mr-sm"
              >
                <q-tooltip>Editar</q-tooltip>
              </q-btn>
              <!--<q-btn
                flat
                dense
                round
                icon="delete"
                size="sm"
                color="negative"
                @click="confirmarEliminar(prop.node)"
              >
                <q-tooltip>Eliminar</q-tooltip>
              </q-btn>-->
            </div>
          </template>
        </q-tree>
      </div>

      <!-- Dialog para crear/editar cuenta -->
      <q-dialog v-model="dialogoCuenta" @hide="resetearFormulario">
        <q-card style="min-width: 500px">
          <q-card-section class="row items-center q-pb-none">
            <div class="text-h6">
              {{ modoEdicion ? "Editar Cuenta Contable" : "Nueva Cuenta Contable" }}
            </div>
            <q-space />
            <q-btn icon="close" flat round dense @click="dialogoCuenta = false" />
          </q-card-section>

          <q-separator />

          <q-card-section class="q-pt-md">
            <q-form ref="myForm" @submit="guardarCuenta">
              <q-input
              readonly
                v-model="formulario.codigo"
                label="Código *"
                outlined
                dense
                class="q-mb-md"
                :rules="[val => !!val || 'El código es requerido']"
                placeholder="Ej: 1.1.01"
              />

              <q-input
              readonly
                v-model="formulario.nombre"
                label="Nombre *"
                outlined
                dense
                class="q-mb-md"
                :rules="[val => !!val || 'El nombre es requerido']"
                placeholder="Ej: Caja"
              />

              <q-select
              readonly
                v-model="formulario.cuentaPadreId"
                :options="cuentasPadreDisponibles"
                outlined
                dense
                class="q-mb-md"
                label="Cuenta Padre (opcional)"
                emit-value
                map-options
                option-value="id"
                option-label="label"
                clearable
              />

              <q-toggle
              disable
                v-model="formulario.esDeMovimiento"
                label="Permite asientos contables"
                class="q-mb-md"
              />

              <q-toggle
              disable
                v-model="formulario.esActivo"
                label="Activo"
                class="q-mb-lg"
              />

              <div class="row q-gutter-md justify-end">
                <q-btn
                  label="Cancelar"
                  color="grey"
                  flat
                  @click="dialogoCuenta = false"
                />
                <q-btn
                  label="Guardar"
                  color="primary"
                  type="submit"
                />
              </div>
            </q-form>
          </q-card-section>
        </q-card>
      </q-dialog>


    </div>
   <!-- Dialog de eliminar -->
      <DialogEliminar
        v-if="isDialogoEliminarAbierto"
        :isOpen="isDialogoEliminarAbierto"
        :idElemento="idElementoSeleccionado"
        @eliminar="eliminar"
        @closeDialog="handleCloseDialog"
      />

      <!-- Dialog de carga -->
      <DialogLoad :dialogLoad="dialogLoad" />
</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { useQuasar } from 'quasar'
import { loadGet, saveData, closeDialog, eliminarElemento, obtener } from 'src/assets/js/util/funciones'
import DialogEliminar from 'components/DialogBoxes/DialogEliminar.vue'
import DialogLoad from 'components/DialogBoxes/DialogLoad.vue'
import { Success, Error } from 'src/assets/js/util/notify'

const $q = useQuasar()

// Variables Booleanas
const dialogoCuenta = ref(false)
const dialogLoad = ref(false)
const isDialogoEliminarAbierto = ref(false)
const modoEdicion = ref(false)

// Variables Nulas
const myForm = ref(null)
const idElementoSeleccionado = ref(null)

// Variables vacías
const filter = ref('')

// Arreglos
const cuentas = ref([])

// Objeto inicial para el formulario
const objetoInicial = {
  codigo: '',
  nombre: '',
  cuentaPadreId: null,
  esDeMovimiento: true,
  esActivo: true,
}

// Crear una copia del objeto inicial como reactive
const formulario = reactive({ ...objetoInicial })

// Computed para el árbol de cuentas
const cuentasArbolizadas = computed(() => {
  return construirArbol(cuentas.value.filter(c => !c.cuentaPadreId))
})

// Computed para cuentas padre disponibles
const cuentasPadreDisponibles = computed(() => {
  const cuentasDisponibles = cuentas.value.filter(
    c => c.id !== formulario.id
  )
  return cuentasDisponibles.map(c => ({
    id: c.id,
    label: `${c.codigo} - ${c.nombre}`,
  }))
})

// Funciones
const construirArbol = (cuentasLista) => {
  return cuentasLista.map(cuenta => ({
    id: cuenta.id,
    label: `${cuenta.codigo} - ${cuenta.nombre}`,
    esActivo: cuenta.esActivo,
    children: construirArbol(
      cuentas.value.filter(c => c.cuentaPadreId === cuenta.id)
    ),
    ...cuenta,
  }))
}

const cargarCuentas = async () => {
  try {
    dialogLoad.value = true
    const response = await loadGet('CuentaContable/ObtenerListadoPaginado') ?? []
    cuentas.value = response || []
  } catch (error) {
    Error('Error al cargar las cuentas contables')
    console.error(error)
  } finally {
    dialogLoad.value = false
  }
}

const abrirDialogoCrear = () => {
  modoEdicion.value = false
  resetearFormulario()
  dialogoCuenta.value = true
}

const editarCuenta = (cuenta) => {
  modoEdicion.value = true
  formulario.id = cuenta.id
  formulario.codigo = cuenta.codigo
  formulario.nombre = cuenta.nombre
  formulario.cuentaPadreId = cuenta.cuentaPadreId
  formulario.esDeMovimiento = cuenta.esDeMovimiento
  formulario.esActivo = cuenta.esActivo
  dialogoCuenta.value = true
}

const resetearFormulario = () => {
  formulario.id = undefined
  formulario.codigo = ''
  formulario.nombre = ''
  formulario.cuentaPadreId = null
  formulario.esDeMovimiento = true
  formulario.esActivo = true
  myForm.value?.resetValidation()
}

const guardarCuenta = async () => {
  try {
    const url = modoEdicion.value ? 'CuentaContable/Actualizar' : 'CuentaContable/Crear'
    await saveData(url, formulario, cargarCuentas, close, dialogLoad)
    dialogoCuenta.value = false
  } catch (error) {
    Error(
      error.response?.data?.message ||
      'Error al guardar la cuenta contable'
    )
  }
}

const confirmarEliminar = (cuenta) => {
  idElementoSeleccionado.value = cuenta.id
  isDialogoEliminarAbierto.value = true
}

const eliminar = async () => {
  try {
    await eliminarElemento(
      'CuentaContable/Eliminar',
      idElementoSeleccionado.value,
      cargarCuentas,
      dialogLoad
    )
    handleCloseDialog()
  } catch (error) {
    Error(
      error.response?.data?.message ||
      'Error al eliminar la cuenta contable'
    )
  }
}

const handleCloseDialog = () => {
  isDialogoEliminarAbierto.value = false
}

const close = async () => {
  closeDialog(formulario, objetoInicial, myForm, dialogoCuenta)
}

const seleccionarCuenta = (cuenta) => {
}

// Ciclo de vida
onMounted(async () => {
  await cargarCuentas()
})
</script>


