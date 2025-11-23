<template>
  <q-dialog v-model="showDialog" persistent>
    <q-card style="min-width: 600px; max-width: 800px">
      <q-card-section>
        <div class="text-h6">Confirmar Pedido</div>
      </q-card-section>

      <q-separator />

      <q-card-section>
        <!-- Si no está logueado -->
        <div v-if="!isLoggedIn" class="text-center q-pa-md">
          <q-icon name="warning" color="negative" size="48px" />
          <div class="text-subtitle1 q-mt-md">
            Debes estar registrado para continuar con tu compra.
          </div>
          <q-btn
            color="primary"
            label="Ir a Registro"
            class="q-mt-md"
            @click="goToRegister"
          />
        </div>

        <!-- Si está logueado -->
        <div v-else>
          <!-- Resumen del pedido -->
          <div class="text-subtitle1 q-mb-md">Resumen del pedido:</div>
          <q-list bordered separator>
            <q-item v-for="it in items" :key="it.id">
              <q-item-section>
                <q-item-label>{{ it.nombre }}</q-item-label>
                <q-item-label caption>
                  Cantidad: {{ it.cantidad }} · Precio: ${{ it.precioVenta.toFixed(2) }}
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <div class="text-weight-bold text-primary">
                  ${{ (it.precioVenta * it.cantidad).toFixed(2) }}
                </div>
              </q-item-section>
            </q-item>
          </q-list>

          <div class="q-mt-md text-right text-h6">
            Total: ${{ totalPrice.toFixed(2) }}
          </div>

          <!-- Formulario de datos -->
          <div class="q-mt-lg">
            <div class="text-subtitle1 text-weight-bold q-mb-sm">Detalles de Facturación</div>
            <q-input v-model="form.nombre" label="Nombre *" dense outlined class="q-mb-md"
                     :rules="[val => !!val || 'Campo requerido']" />
            <q-input v-model="form.telefono" label="Número de teléfono móvil *" dense outlined class="q-mb-md"
                     :rules="[val => !!val || 'Campo requerido']" />
            <q-input v-model="form.email" label="Email" type="email" dense outlined class="q-mb-md" />

            <div class="text-subtitle1 text-weight-bold q-mt-lg q-mb-sm">Detalles Adicionales</div>
             <q-select
                      class="q-mb-md"
                        v-model="form.gestorId"
                              outlined
            dense
                        label="Nombre del Gestor"
                        emit-value
                        map-options
                        :use-input="
                            form.gestorId === null ||
                            form.gestorId === ''
                        "
                        option-label="nombre"
                        option-value="id"
                        :options="filtradoGestor"
                        @filter="
                            (val, update) => {
                                filtradoGestor = filterOptions(
                                    val,
                                    update,
                                    filtradoGestor,
                                    'nombre',
                                    itemsGestor
                                );
                            }
                        "

                    >
                        <template v-slot:no-option>
                            <q-item>
                                <q-item-section class="text-italic text-grey">
                                    No hay elementos disponibles
                                </q-item-section>
                            </q-item>
                        </template>
                          <template v-slot:append v-if="form.gestorId!==null && form.gestorId!==''">
    <q-icon
      name="close"
      class="cursor-pointer"
      @click.stop="form.gestorId = ''"
    >
      <q-tooltip>Limpiar selección</q-tooltip>
    </q-icon>
  </template>
                    </q-select>
            <q-input v-model="form.impuestos" type="number" :min="0" label="Impuestos" dense outlined class="q-mb-md" />
            <q-input v-model="form.direccion"
                     label="Dirección (Calle Principal, número de vivienda, entre calles, municipio, reparto)"
                     type="textarea" dense outlined class="q-mb-md" />

                     <q-select
                 class="q-mb-md"
                        v-model="form.mensajeriaId"
                              outlined
            dense
                        label="Mensajería *"
                        emit-value
                        map-options
                        :use-input="
                            form.mensajeriaId === null ||
                            form.mensajeriaId === ''
                        "
                        option-label="textoMensajeriaPrecio"
                        option-value="id"
                        :options="filtradoMensajeria"
                        @filter="
                            (val, update) => {
                                filtradoMensajeria = filterOptions(
                                    val,
                                    update,
                                    filtradoMensajeria,
                                    'textoMensajeriaPrecio',
                                    itemsMensajeria
                                );
                            }
                        "
                        lazy-rules
                        :rules="[
                            (val) =>
                                (val !== null && val !== '') ||
                                'Debe seleccionar un elemento',
                        ]"
                    >
                        <template v-slot:no-option>
                            <q-item>
                                <q-item-section class="text-italic text-grey">
                                    No hay elementos disponibles
                                </q-item-section>
                            </q-item>
                        </template>

                         <template v-slot:append v-if="form.mensajeriaId!==null && form.mensajeriaId!==''">
    <q-icon
      name="close"
      class="cursor-pointer"
      @click.stop="form.mensajeriaId = ''"
    >
      <q-tooltip>Limpiar selección</q-tooltip>
    </q-icon>
  </template>
                    </q-select>
          </div>
        </div>
      </q-card-section>

      <q-separator />

      <q-card-actions align="right">
        <q-btn flat label="Cancelar" color="grey" v-close-popup />
        <q-btn
          v-if="isLoggedIn"
          color="primary"
          label="Confirmar Pedido"
          @click="confirmOrder"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>

        <DialogLoad :dialogLoad="dialogLoad" />

</template>

<script setup>
import { ref, computed,onMounted } from 'vue'
import { useRouter } from 'vue-router'
import useCart from 'src/stores/cartStore'
import { filterOptions, loadGet } from 'src/assets/js/util/funciones'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
const dialogLoad = ref(false)

const router = useRouter()
const cart = useCart()

const showDialog = ref(false)
const items = cart.items
const totalPrice = cart.totalPrice

const itemsGestor = ref([])
const filtradoGestor = ref([])

const itemsMensajeria = ref([])
const filtradoMensajeria= ref([])

// Formulario
const form = ref({
  nombre: '',
  telefono: '',
  email: '',
  gestorId: '',
  impuestos: 0,
  direccion: '',
  mensajeriaId: ''
})

// Verificar login
const isLoggedIn = computed(() => {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  return !!token
})

// Decodificar token para autocompletar datos
function getUserFromToken() {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  if (!token) return null

  try {
    const payload = JSON.parse(atob(token.split('.')[1]))
    console.log("payload: ", payload)
    return {
      nombre: payload.NombreCompleto || payload.unique_name || '',
      email: payload.Correo || '',
      telefono: payload.Telefono || ''
    }
  } catch (err) {
    console.warn('Error al decodificar token:', err)
    return null
  }
}

function openDialog() {
  if (isLoggedIn.value) {
    const user = getUserFromToken()
    if (user) {
      form.value.nombre = user.nombre
      form.value.email = user.email
      form.value.telefono = user.telefono
    }
  }
  showDialog.value = true
}

function goToRegister() {
  showDialog.value = false
  router.push({ name: 'RegisterPage', query: { redirect: 'CarritoPage' } })
}

function confirmOrder() {
  // Validación básica
  if (!form.value.nombre || !form.value.telefono || !form.value.mensajeria) {
    alert('Por favor completa los campos obligatorios.')
    return
  }

  // Objeto pedido
  const pedido = {
    productos: items,
    total: totalPrice,
    datosCliente: { ...form.value }
  }

  console.log('Pedido confirmado:', pedido)
  showDialog.value = false
  router.push({ name: 'CheckoutPage' }) // o la página de confirmación final
}

defineExpose({ openDialog })

onMounted(async () => {
  dialogLoad.value = true
  itemsGestor.value = await loadGet('Gestor/ObtenerListadoPaginado')??[]
   itemsMensajeria.value = await loadGet('Mensajeria/ObtenerListadoPaginado')??[]
  filtradoGestor.value=itemsGestor.value
  filtradoMensajeria.value=itemsMensajeria.value

    dialogLoad.value = false
})
</script>
