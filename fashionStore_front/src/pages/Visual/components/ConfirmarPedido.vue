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

          <!-- Campo de cupón -->
          <q-input
            v-model="form.cupon"
            label="Código de cupón"
            dense
            outlined
            class="q-mb-md q-mt-md"
          >
            <template v-slot:append>
              <q-btn
                flat
                dense
                color="primary"
                label="Aplicar"
                @click="aplicarCupon"
              />
              <q-btn
                v-if="descuento > 0"
                flat
                dense
                color="negative"
                label="Cancelar"
                @click="cancelarCupon"
              />
            </template>
          </q-input>

          <!-- Mensaje del cupón -->
          <div
            v-if="mensajeCupon"
            :class="[
              'q-mb-md text-subtitle2',
              estadoCupon === 'success' ? 'text-positive' : 'text-negative'
            ]"
          >
            {{ mensajeCupon }}
          </div>

          <!-- Desglose -->
          <div class="q-mt-md text-right text-subtitle2">
            Carrito: ${{ totalPrice.toFixed(2) }}
          </div>
          <div v-if="gestorPrecio > 0" class="text-right text-subtitle2">
            Gestor: ${{ gestorPrecio.toFixed(2) }}
          </div>
          <div v-if="mensajeriaPrecio > 0" class="text-right text-subtitle2">
            Mensajería: ${{ mensajeriaPrecio.toFixed(2) }}
          </div>
          <div v-if="descuento > 0" class="text-right text-subtitle2 text-negative">
            Descuento: -${{ descuento.toFixed(2) }}
          </div>

          <!-- Totales -->
          <div class="q-mt-md text-right text-h6">
            <span v-if="descuento > 0" class="text-negative" style="text-decoration: line-through;">
              ${{ (totalPrice + gestorPrecio + mensajeriaPrecio).toFixed(2) }}
            </span>
            <span v-else>
              ${{ (totalPrice + gestorPrecio + mensajeriaPrecio).toFixed(2) }}
            </span>
          </div>
          <div class="text-right text-h5 text-primary">
            Total a pagar: ${{ totalConExtras.toFixed(2) }}
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
              option-label="nombre"
              option-value="id"
              :options="filtradoGestor"
            >
              <template v-slot:append v-if="form.gestorId">
                <q-icon name="close" class="cursor-pointer" @click.stop="limpiarGestor()">
                  <q-tooltip>Limpiar selección</q-tooltip>
                </q-icon>
              </template>
            </q-select>

            <q-input :disable="form.gestorId===null||form.gestorId===''" v-model="form.impuestos" type="number" :min="0" label="Impuestos" dense outlined class="q-mb-md" />
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
              option-label="textoMensajeriaPrecio"
              option-value="id"
              :options="filtradoMensajeria"
              lazy-rules
              :rules="[(val) => (val !== null && val !== '') || 'Debe seleccionar un elemento']"
            >
              <template v-slot:append v-if="form.mensajeriaId">
                <q-icon name="close" class="cursor-pointer" @click.stop="form.mensajeriaId = ''">
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
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import useCart from 'src/stores/cartStore'
import { loadGet, saveDataPronosticoEnviarObjeto } from 'src/assets/js/util/funciones'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import { Error, Success } from 'src/assets/js/util/notify'

const dialogLoad = ref(false)
const router = useRouter()
const cart = useCart()

const showDialog = ref(false)
const items = cart.items
const totalPrice = cart.totalPrice

const itemsGestor = ref([])
const filtradoGestor = ref([])
const itemsMensajeria = ref([])
const filtradoMensajeria = ref([])

const descuento = ref(0)
const mensajeCupon = ref('')
const estadoCupon = ref('success')

// precios extras
const gestorPrecio = computed(() => {
  // aseguramos que siempre sea un número
  return Number(form.value.impuestos) || 0
})
const mensajeriaPrecio = computed(() => {
  const m = itemsMensajeria.value.find(x => x.id === form.value.mensajeriaId)
  return m?.precio || 0
})

// total con extras y descuento
const totalConExtras = computed(() => {
  let base = totalPrice.value + gestorPrecio.value + mensajeriaPrecio.value
  return base - descuento.value
})

// formulario
const form = ref({
  nombre: '',
  telefono: '',
  email: '',
  gestorId: '',
  impuestos: 0,
  direccion: '',
  mensajeriaId: '',
  cupon: ''
})

const cuponId=ref(null)

const isLoggedIn = computed(() => {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  return !!token
})

function openDialog() {
  if (isLoggedIn.value) {
    const payload = JSON.parse(
      atob((localStorage.getItem('token') || sessionStorage.getItem('token')).split('.')[1])
    )
    form.value.nombre = payload.NombreCompleto || payload.unique_name || ''
    form.value.email = payload.Correo || ''
    form.value.telefono = payload.Telefono || ''
  }
  showDialog.value = true
}

function goToRegister() {
  showDialog.value = false
  router.push({ name: 'RegisterPage', query: { redirect: 'CarritoPage' } })
}

async function confirmOrder() {
  // Validación básica
  if (!form.value.nombre || !form.value.telefono || !form.value.mensajeriaId) {
    alert('Por favor completa los campos obligatorios.')
    return
  }

const payload = JSON.parse(
      atob((localStorage.getItem('token') || sessionStorage.getItem('token')).split('.')[1])
    )
    let usuarioId = payload.Id

  // Objeto pedido con desglose
  const pedido = {
    productos: items,
    total: totalConExtras.value,
    datosCliente: { ...form.value },
    descuentoAplicado: descuento.value,
    extras: {
      gestor: gestorPrecio.value,
      mensajeria: mensajeriaPrecio.value
    }
  }
  // Construir lista de productos para el DTO
  let productoLista = []
  items.forEach(element => {
    let nuevo = {
      productoId: element.id,       // id del producto
      cantidad: element.cantidad    // cantidad seleccionada
    }
    productoLista.push(nuevo)
  })

  let generarPedidoDto={
    productos:productoLista,
    usuarioId:usuarioId,
    gestorId:form.value.gestorId!==''?form.value.gestorId:null,
    impuestoGestor:form.value.impuestos,
    direccion:form.value.direccion,
    mensajeriaId:form.value.mensajeriaId,
    cuponId:cuponId.value,
  }

  const ruta="Pedido/GenerarPedido"
  await saveDataPronosticoEnviarObjeto(ruta,generarPedidoDto,dialogLoad).then(resultado=>{
    if(!!resultado?.mensajeError){
        Error(resultado?.mensajeError)

    }
    else{
        Success("Pedido enviado con éxito")
        // 🔴 Limpiar carrito del localStorage
      localStorage.removeItem('fashion_cart_v1')

      // 🔴 Si usas el store reactivo, también vacía el array
      items.splice(0, items.length)
    }
  })
  console.log('Pedido confirmado:', pedido)
  showDialog.value = false
 // router.push({ name: 'CheckoutPage' }) // o la página de confirmación final
}

defineExpose({ openDialog })

onMounted(async () => {
  dialogLoad.value = true
  itemsGestor.value = await loadGet('Gestor/ObtenerListadoPaginado') ?? []
  itemsMensajeria.value = await loadGet('Mensajeria/ObtenerListadoPaginado') ?? []
  filtradoGestor.value = itemsGestor.value
  filtradoMensajeria.value = itemsMensajeria.value
  dialogLoad.value = false
})

async function aplicarCupon() {
  const ruta = "Cupon/ObtenerCuponPorCodigo"
  const verificarCuponDto = {
    codigo: form.value.cupon,
    importePedido: totalPrice.value,
  }

  try {
    dialogLoad.value = true
    const respuesta = await saveDataPronosticoEnviarObjeto(ruta, verificarCuponDto, dialogLoad)

    if (respuesta && respuesta.resultado) {
      const cupon = respuesta.resultado // objeto CuponEspecificoDto
      cuponId.value=cupon.id
      if (cupon.esMontoFijo) {
        // descuento fijo en dinero
        descuento.value = cupon.valor
        mensajeCupon.value = `Cupón aplicado: descuento fijo de $${cupon.valor}`
      } else {
        // descuento en porcentaje
        descuento.value = (totalPrice.value * cupon.valor) / 100
        mensajeCupon.value = `Cupón aplicado: ${cupon.valor}% de descuento`
      }
      estadoCupon.value = 'success'
    } else {
      descuento.value = 0
      mensajeCupon.value = 'Cupón inválido o no encontrado'
      estadoCupon.value = 'error'
    }
  } catch (err) {
    console.error('Error al verificar cupón:', err)
    descuento.value = 0
    mensajeCupon.value = 'Error al aplicar cupón'
    estadoCupon.value = 'error'
  } finally {
    dialogLoad.value = false
  }
}

function cancelarCupon() {
  descuento.value = 0
  mensajeCupon.value = ''
  estadoCupon.value = 'success'
  form.value.cupon = ''
  cuponId.value=null
}

function limpiarGestor(){
    form.value.gestorId=null
    form.value.impuestos=0
}
</script>
