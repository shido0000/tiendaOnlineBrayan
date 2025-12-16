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
                  Cantidad: {{ it.cantidad }} · Precio: ${{ it.precioVenta.toFixed(2) }} {{ getItemMonedaCodigo(it) }}
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <div class="text-weight-bold text-primary">
                  ${{ (it.precioVenta * it.cantidad).toFixed(2) }} {{ getItemMonedaCodigo(it) }}
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
            Carrito: ${{ totalPrice.toFixed(2) }} {{ mostrarMonedas }}
          </div>
          <div v-if="gestorPrecio > 0" class="text-right text-subtitle2">
            Gestor: ${{ gestorPrecio.toFixed(2) }} {{ monedaGestor }}
          </div>
          <div v-if="mensajeriaPrecio > 0" class="text-right text-subtitle2">
            Mensajería: ${{ mensajeriaPrecio.toFixed(2) }} {{ monedaMensajeria }}
          </div>
          <div v-if="descuento > 0" class="text-right text-subtitle2 text-negative">
            Descuento: -${{ descuento.toFixed(2) }} {{ monedaDescuento }}
          </div>

          <!-- Totales -->
           <div class="q-mt-lg">
                <div v-if="mostrarSubtotal" class="q-mt-md text-right text-h6">
                    <span v-if="descuento > 0" class="text-negative" style="text-decoration: line-through;">
                     ${{ subtotalConvertido.toFixed(2) }} {{ monedaPrincipal }}
                    </span>
                    <span v-else>
                     ${{ subtotalConvertido.toFixed(2) }} {{ monedaPrincipal }}
                    </span>
                </div>
            <div class="text-right text-h5 text-primary">
            Total a pagar: ${{ totalConExtras.toFixed(2) }} {{ monedaPrincipal }}
          </div>

        </div>

          <!-- Si hay múltiples monedas, mostrar equivalentes -->
          <div v-if="tieneMúltiplesMonedas" class="q-mt-md q-pa-md bg-blue-1 rounded-borders">
            <div class="text-subtitle2 text-weight-bold q-mb-md">Equivalentes en otras monedas:</div>
            <div v-for="(monto, moneda) in totalesEnMonedas" :key="moneda" class="text-right q-mb-sm">
              <strong>Total en {{ moneda }}:</strong> ${{ monto.toFixed(2) }}
            </div>
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
              label="Mensajería"
              emit-value
              map-options
              option-label="textoMensajeriaPrecio"
              option-value="id"
              :options="filtradoMensajeria"
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
const monedas = ref([]) // Lista de monedas del sistema

const descuento = ref(0)
const mensajeCupon = ref('')
const estadoCupon = ref('success')

// Mapeo de monedas: monedaId -> moneda objeto
const monedaMap = computed(() => {
  const map = {}
  monedas.value.forEach(m => {
    map[m.id] = m
  })
  return map
})

// Obtener código de moneda para un item del carrito
function getItemMonedaCodigo(item) {
  if (!item.raw) return 'USD'
  const monedaId = item.raw.monedaVentaId
  const moneda = monedaMap.value[monedaId]
  return moneda ? `(${moneda.codigo})` : '(USD)'
}


// Obtener moneda del gestor
const monedaGestor = computed(() => {
  if (!form.value.gestorId) return '(USD)'
  const gestor = itemsGestor.value.find(g => g.id === form.value.gestorId)
  const monedaId = gestor?.monedaId
  const moneda = monedaMap.value[monedaId]
  return moneda ? `(${moneda.codigo})` : '(USD)'
})

// Obtener moneda de la mensajería
const monedaMensajeria = computed(() => {
  if (!form.value.mensajeriaId) return '(USD)'
  const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
  const monedaId = mensajeria?.monedaId
  const moneda = monedaMap.value[monedaId]
  return moneda ? `(${moneda.codigo})` : '(USD)'
})

// Obtener moneda del descuento (misma que el carrito, generalmente)
const monedaDescuento = computed(() => mostrarMonedas.value)

// Obtener todas las monedas usadas
function obtenerMonedasUsadas() {
  const monedasSet = new Set()

  // Monedas de productos
  items.forEach(item => {
    const monedaId = item.raw?.monedaVentaId
    if (monedaId) monedasSet.add(monedaId)
  })



  // Moneda de la mensajería
  if (form.value.mensajeriaId) {
    const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
    if (mensajeria?.monedaId) monedasSet.add(mensajeria.monedaId)
  }

  return Array.from(monedasSet)
}

// Detectar si hay múltiples monedas
const tieneMúltiplesMonedas = computed(() => {
  return obtenerMonedasUsadas().length > 1
})

// Mostrar código de moneda principal
const monedaPrincipal = computed(() => {
  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length === 0) return '(USD)'

  // Usar la primera moneda encontrada
  const moneda = monedaMap.value[monedasUsadas[0]]
  return moneda ? `(${moneda.codigo})` : '(USD)'
})

// Mostrar códigos de monedas para el carrito
const mostrarMonedas = computed(() => {
  const monedasSet = new Set()
  items.forEach(item => {
    const monedaId = item.raw?.monedaVentaId
    const moneda = monedaMap.value[monedaId]
    if (moneda) monedasSet.add(moneda.codigo)
  })

  if (monedasSet.size === 0) return '(USD)'
  if (monedasSet.size === 1) return `(${Array.from(monedasSet)[0]})`
  return `(${Array.from(monedasSet).join(', ')})`
})

// Calcular totales en cada moneda
const totalesEnMonedas = computed(() => {
  const totales = {}

  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length <= 1) return totales

  // Encontrar la moneda base (primera con tasa de cambio)
  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return totales

  // Calcular el monto total en la moneda base
  let totalEnBase = 0

  // Sumar productos
  items.forEach(item => {
    const monedaItem = monedaMap.value[item.raw?.monedaVentaId]
    const monto = item.precioVenta * item.cantidad

    if (monedaItem && monedaItem.id !== monedaBase.id) {
      // Convertir a moneda base: (monto en monedaItem * tasaBase) / tasaItem
      const tasaItem = monedaItem.tasaCambio || 1
      const tasaBase = monedaBase.tasaCambio || 1
      totalEnBase += (monto * tasaBase) / tasaItem
    } else {
      totalEnBase += monto
    }
  })  // Sumar gestor
  if (gestorPrecio.value > 0) {
    const gestor = itemsGestor.value.find(g => g.id === form.value.gestorId)
    const monedaGestorObj = monedaMap.value[gestor?.monedaId]

    if (monedaGestorObj && monedaGestorObj.id !== monedaBase.id) {
      const tasaGestor = monedaGestorObj.tasaCambio || 1
      const tasaBase = monedaBase.tasaCambio || 1
      totalEnBase += (gestorPrecio.value * tasaBase) / tasaGestor
    } else {
      totalEnBase += gestorPrecio.value
    }
  }

  // Sumar mensajería
  if (mensajeriaPrecio.value > 0) {
    const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
    const monedaMensajeriaObj = monedaMap.value[mensajeria?.monedaId]

    if (monedaMensajeriaObj && monedaMensajeriaObj.id !== monedaBase.id) {
      const tasaMensajeria = monedaMensajeriaObj.tasaCambio || 1
      const tasaBase = monedaBase.tasaCambio || 1
      totalEnBase += (mensajeriaPrecio.value * tasaBase) / tasaMensajeria
    } else {
      totalEnBase += mensajeriaPrecio.value
    }
  }

  // Restar descuento (en moneda base)
  totalEnBase -= descuento.value

  // Convertir a cada moneda
  monedasUsadas.forEach(monedaId => {
    const moneda = monedaMap.value[monedaId]
    if (moneda) {
      const tasaDestino = moneda.tasaCambio || 1
      const tasaBase = monedaBase.tasaCambio || 1
      totales[moneda.codigo] = (totalEnBase * tasaDestino) / tasaBase
    }
  })

  return totales
})

// precios extras
const gestorPrecio = computed(() => {
  // aseguramos que siempre sea un número
  return Number(form.value.impuestos) || 0
})
const mensajeriaPrecio = computed(() => {
  const m = itemsMensajeria.value.find(x => x.id === form.value.mensajeriaId)
  return m?.precio || 0
})

// Calcular totalPrice convertido considerando monedas
const totalPriceConvertido = computed(() => {
  if (!tieneMúltiplesMonedas.value) return totalPrice.value

  const monedasUsadas = obtenerMonedasUsadas()
  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return totalPrice.value

  let total = 0
  items.forEach(item => {
    const monedaItem = monedaMap.value[item.raw?.monedaVentaId]
    const monto = item.precioVenta * item.cantidad

    if (monedaItem && monedaItem.id !== monedaBase.id) {
      const tasaItem = monedaItem.tasaCambio || 1
      const tasaBase = monedaBase.tasaCambio || 1
      total += (monto * tasaBase) / tasaItem
    } else {
      total += monto
    }
  })
  return total
})

// Calcular gestorPrecio convertido considerando monedas
const gestorPrecioConvertido = computed(() => {
  if (!tieneMúltiplesMonedas.value || gestorPrecio.value <= 0) return gestorPrecio.value

  const monedasUsadas = obtenerMonedasUsadas()
  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return gestorPrecio.value

  const gestor = itemsGestor.value.find(g => g.id === form.value.gestorId)
  const monedaGestorObj = monedaMap.value[gestor?.monedaId]

  if (monedaGestorObj && monedaGestorObj.id !== monedaBase.id) {
    const tasaGestor = monedaGestorObj.tasaCambio || 1
    const tasaBase = monedaBase.tasaCambio || 1
    return (gestorPrecio.value * tasaBase) / tasaGestor
  }
  return gestorPrecio.value
})

// Calcular mensajeriaPrecio convertido considerando monedas
const mensajeriaPrecioConvertido = computed(() => {
  if (!tieneMúltiplesMonedas.value || mensajeriaPrecio.value <= 0) return mensajeriaPrecio.value

  const monedasUsadas = obtenerMonedasUsadas()
  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return mensajeriaPrecio.value

  const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
  const monedaMensajeriaObj = monedaMap.value[mensajeria?.monedaId]

  if (monedaMensajeriaObj && monedaMensajeriaObj.id !== monedaBase.id) {
    const tasaMensajeria = monedaMensajeriaObj.tasaCambio || 1
    const tasaBase = monedaBase.tasaCambio || 1
    return (mensajeriaPrecio.value * tasaBase) / tasaMensajeria
  }
  return mensajeriaPrecio.value
})

// total con extras y descuento (usando valores convertidos)
const totalConExtras = computed(() => {
  return  totalPriceConvertido.value - descuento.value  + gestorPrecioConvertido.value + mensajeriaPrecioConvertido.value
})

// Calcular subtotal (suma de precios convertidos antes de descuento)
const subtotalConvertido = computed(() => {
  return totalPriceConvertido.value + gestorPrecioConvertido.value + mensajeriaPrecioConvertido.value
})

// Mostrar subtotal solo si difiere del total final
const mostrarSubtotal = computed(() => {
  return Math.abs(subtotalConvertido.value - totalConExtras.value) > 0.01
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
  if (!form.value.nombre || !form.value.telefono) {
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
    mensajeriaId: form.value.mensajeriaId !== '' ? form.value.mensajeriaId : null,
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
  monedas.value = await loadGet('Moneda/ObtenerListadoPaginado') ?? []
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

      // Verificar si el cupón está dentro del rango de fechas
      const hoy = new Date()
      const fechaInicio = new Date(cupon.fechaInicio)
      const fechaFin = new Date(cupon.fechaFin)

      if (hoy < fechaInicio || hoy > fechaFin) {
        descuento.value = 0
        mensajeCupon.value = 'El cupón ha expirado o aún no está válido'
        estadoCupon.value = 'error'
        dialogLoad.value = false
        return
      }

      // Verificar si alcanzó el límite de usos
      if (cupon.usosActuales >= cupon.maximoUsos) {
        descuento.value = 0
        mensajeCupon.value = 'El cupón ha alcanzado su límite máximo de usos'
        estadoCupon.value = 'error'
        dialogLoad.value = false
        return
      }

      cuponId.value = cupon.id
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

      // Incrementar usos actuales del cupón
      await incrementarUsosCupon(cupon.id)
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

async function incrementarUsosCupon(cuponId) {
  try {
    const ruta = `Cupon/IncrementarUsos/${cuponId}`
    await saveDataPronosticoEnviarObjeto(ruta, {}, dialogLoad)
  } catch (err) {
    console.error('Error al incrementar usos del cupón:', err)
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
