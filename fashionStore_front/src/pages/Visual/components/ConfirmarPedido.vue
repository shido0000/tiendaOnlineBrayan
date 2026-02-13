<template>
  <q-dialog v-model="showDialog" persistent>
    <q-card class="confirmar-pedido-card">
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
          <q-list v-if="items.length > 0" bordered separator>
            <q-item v-for="it in items" :key="it.id">
              <q-item-section>
                <q-item-label>{{ it.nombre || it.descripcion || 'Sin nombre' }}</q-item-label>
                <q-item-label caption>
  Cantidad: {{ $props.desdeElCarrito ? it.cantidad : props.cantidad || 0 }} ·
  <span v-if="it.tieneDescuento">
    <s class="text-grey-6">
      ${{ (it.precioVenta || 0).toFixed(2) }}
    </s>
    <span class="text-weight-bold text-primary">
      ${{ (it.precioVentaDescuento || 0).toFixed(2) }}
    </span>
  </span>
  <span v-else>
    ${{ (it.precioVenta || 0).toFixed(2) }}
  </span>
  {{ getItemMonedaCodigo(it) }}
</q-item-label>

              </q-item-section>
              <q-item-section side>
                <div v-if="it.tieneDescuento" class="row items-center q-gutter-sm">
  <div class="text-subtitle2 text-grey-6">
    <s>
      ${{ ((it.precioVenta || 0) * ($props.desdeElCarrito ? it.cantidad : props.cantidad || 0)).toFixed(2) }}
    </s>
  </div>
  <div class="text-weight-bold text-primary">
    ${{ ((it.precioVentaDescuento || it.precioVenta || 0) * ($props.desdeElCarrito ? it.cantidad : props.cantidad || 0)).toFixed(2) }}
  </div>
  {{ getItemMonedaCodigo(it) }}
</div>
<div v-else class="text-weight-bold text-primary">
  ${{ ((it.precioVenta || 0) * ($props.desdeElCarrito ? it.cantidad : props.cantidad || 0)).toFixed(2) }} {{ getItemMonedaCodigo(it) }}
</div>

              </q-item-section>
            </q-item>
          </q-list>
          <div v-else class="text-center text-grey-6 q-pa-md">
            Cargando productos...
          </div>

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
           <!-- <q-select
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
        -->
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
import { ref, computed, onMounted, watch, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'
import useCart from 'src/stores/cartStore'
import { loadGet, saveDataPronosticoEnviarObjeto } from 'src/assets/js/util/funciones'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import { Error, Success } from 'src/assets/js/util/notify'
import signalRService from 'src/services/signalRService'

const props = defineProps({
  desdeElCarrito: Boolean,
  productoItem: {
    type: Object,
    default: ref(null),
    required:false
  },
  cantidad:Number,

})


const dialogLoad = ref(false)
const router = useRouter()
const cart = useCart()

const showDialog = ref(false)

// Items como ref para que se actualice con watch
const items = ref([])

// Función para actualizar items desde las props
function actualizarItems() {
  let sourceItems = []
  if (props.desdeElCarrito) {
    sourceItems = cart.items || []
  } else {
    // Cuando viene desde props.productoItem, normalizar la estructura
    if (props.productoItem) {
      let producto = props.productoItem
      if (!Array.isArray(producto)) {
        producto = [producto]
      }

      sourceItems = producto.map(p => {
        // Obtener la variante si existe (para acceder a ID de variante)
        let varianteId = p.id
        let variantePrincipal = null

        if (p.variants && Array.isArray(p.variants) && p.variants.length > 0) {
          variantePrincipal = p.variants[0]
          varianteId = variantePrincipal.id
        } else if (p.selectedVariant) {
          variantePrincipal = p.selectedVariant
          varianteId = p.selectedVariant.id
        }

        return {
          // Propiedades básicas del producto
          id: p.id,
          varianteId: varianteId, // ID de la variante para la orden
          nombre: p.nombre || p.descripcion,
          descripcion: p.descripcion,
          cantidad: props.cantidad || 1,
          precioVenta: p.precioVenta || p.precio || 0,
          monedaVentaId: p.monedaVentaId,

          // Propiedad raw simulada para compatibilidad con getMonedaVentaId
          raw: {
            monedaVentaId: p.monedaVentaId
          },

          // Propiedades adicionales que puedan venir del producto
          codigo: p.codigo,
          sku: p.sku,
          stock: p.stock,
          color: p.color,
          talla: p.talla,
          selectedVariant: p.selectedVariant,
          variants: p.variants
        }
      })
    }
  }

  // Filtrar null y items sin id
  items.value = (sourceItems || []).filter(item => item && item.id)
}

// Watch para detectar cambios en las props y el carrito
watch(
  [() => props.productoItem, () => props.desdeElCarrito, () => cart.items],
  () => {
    actualizarItems()
  },
  { deep: true }
)

// Inicializar items
actualizarItems()

//const totalPrice = computed(() => props.desdeElCarrito ? cart.totalPrice :  props.cantidad*props.productoItem.precioVenta )

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

// Helper para obtener monedaVentaId de un item (compatible con items del carrito y productos directos)
function getMonedaVentaId(item) {
  if (!item) return null
  // Si viene del carrito, tendrá la propiedad raw
  if (item.raw?.monedaVentaId) return item.raw.monedaVentaId
  // Si viene directamente como props (producto), tendrá monedaVentaId directo
  return item.monedaVentaId || null
}

// Obtener código de moneda para un item del carrito
function getItemMonedaCodigo(item) {
  if (!item) return 'USD'
  const monedaId = getMonedaVentaId(item)
  if (!monedaId) return 'USD'
  const moneda = monedaMap.value[monedaId]
  return moneda ? `(${moneda.codigo})` : '(USD)'
}


// Obtener moneda del gestor (siempre es la moneda base de la compra)
const monedaGestor = computed(() => {
  if (!form.value.gestorId) return '(USD)'
  // El gestor siempre usa la moneda de la compra (moneda base)
  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length === 0) return '(USD)'
  const monedaId = monedasUsadas[0]
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
  items.value.forEach(item => {
    const monedaId = getMonedaVentaId(item)
    if (monedaId) monedasSet.add(monedaId)
  })

  // Nota: El gestor NO tiene moneda, siempre usa la moneda de la compra (moneda base)

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
  items.value.forEach(item => {
    const monedaId = getMonedaVentaId(item)
    const moneda = monedaMap.value[monedaId]
    if (moneda) monedasSet.add(moneda.codigo)
  })

  if (monedasSet.size === 0) return '(USD)'
  if (monedasSet.size === 1) return `(${Array.from(monedasSet)[0]})`
  return `(${Array.from(monedasSet).join(', ')})`
})


const totalesEnMonedas = computed(() => {
  const totales = {}

  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length <= 1) return totales

  // Moneda base (la primera en la lista)
  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return totales

  let totalEnBase = 0
  const tasaBase = monedaBase.tasaCambio || 1

  // Productos
  items.value.forEach(item => {
    const monedaId = getMonedaVentaId(item)
    const monedaItem = monedaMap.value[monedaId]
   const montoUnitario = item.tieneDescuento
  ? (item.precioVentaDescuento || item.precioVenta || 0)
  : (item.precioVenta || 0)

const monto = montoUnitario * (item.cantidad || 0)


    if (monedaItem && monedaItem.id !== monedaBase.id) {
      const tasaItem = monedaItem.tasaCambio || 1
      // convertir a base


      totalEnBase += (monto * tasaBase) / tasaItem
    } else {
      totalEnBase += monto
    }
  })

  // Gestor (ya en moneda base)
  if (gestorPrecio.value > 0) {
    totalEnBase += gestorPrecio.value
  }

  // Mensajería
  if (mensajeriaPrecio.value > 0) {
    const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
    const monedaMensajeriaObj = monedaMap.value[mensajeria?.monedaId]
    const montoMensajeria = mensajeriaPrecio.value

    if (monedaMensajeriaObj && monedaMensajeriaObj.id !== monedaBase.id) {
      const tasaMensajeria = monedaMensajeriaObj.tasaCambio || 1
      // convertir a base
      totalEnBase += (montoMensajeria * tasaMensajeria) / tasaBase
    } else {
      totalEnBase += montoMensajeria
    }
  }

  // Descuento (ya en moneda base)
  if (descuento.value > 0) {
    totalEnBase -= descuento.value
  }

  // Convertir a cada moneda
  monedasUsadas.forEach(monedaId => {
    const moneda = monedaMap.value[monedaId]
    if (moneda) {
      const tasaDestino = moneda.tasaCambio || 1
      // convertir de base a destino
      totales[moneda.codigo] = (totalEnBase * tasaBase) / tasaDestino
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

// Calcular totalPrice correctamente considerando ambas fuentes
const totalPrice = computed(() => {
  if (props.desdeElCarrito) {
    // Si viene del carrito, usar el totalPrice del store (que ya es un computed con .value)
    return cart.totalPrice.value || 0
  } else {
    // Si viene como prop, calcular manualmente
    if (props.productoItem && props.cantidad) {
      return (props.productoItem.precioVenta || 0) * (props.cantidad || 0)
    }
    return 0
  }
})

// Calcular totalPrice convertido considerando monedas
const totalPriceConvertido = computed(() => {
  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length <= 1) return totalPrice.value

  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return totalPrice.value

  let total = 0
  items.value.forEach(item => {
    const monedaId = getMonedaVentaId(item)
    const monedaItem = monedaMap.value[monedaId]
const montoUnitario = item.tieneDescuento
  ? (item.precioVentaDescuento || item.precioVenta || 0)
  : (item.precioVenta || 0)

const monto = montoUnitario * (item.cantidad || 0)

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
// El gestor NO necesita conversión porque siempre está en la moneda base de la compra
const gestorPrecioConvertido = computed(() => {
  return gestorPrecio.value
})

// Calcular mensajeriaPrecio convertido considerando monedas
const mensajeriaPrecioConvertido = computed(() => {
  if (mensajeriaPrecio.value <= 0) return mensajeriaPrecio.value

  const monedasUsadas = obtenerMonedasUsadas()
  if (monedasUsadas.length <= 1) return mensajeriaPrecio.value

  const monedaBase = monedaMap.value[monedasUsadas[0]]
  if (!monedaBase) return mensajeriaPrecio.value

  const mensajeria = itemsMensajeria.value.find(m => m.id === form.value.mensajeriaId)
  const monedaMensajeriaObj = monedaMap.value[mensajeria?.monedaId]

  if (monedaMensajeriaObj && monedaMensajeriaObj.id !== monedaBase.id) {
    const tasaMensajeria = monedaMensajeriaObj.tasaCambio || 1
    const tasaBase = monedaBase.tasaCambio || 1


    return (mensajeriaPrecio.value *tasaMensajeria ) / tasaBase
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
  items.value.forEach(element => {
    // Usar varianteId si está disponible (cuando viene de props), si no usar el id del producto
    let productoId = element.varianteId || element.id
    let nuevo = {
      productoId: productoId,
      cantidad: element.cantidad
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
        // Limpiar carrito del store
        cart.clearCart()

        // Limpiar localStorage también para mayor seguridad
        localStorage.removeItem('fashion_cart_v1')

        // El backend se encargará de notificar a través de SignalR
        // No necesitamos hacerlo desde aquí

        router.push({ name: 'IndexPage' })
        showDialog.value = false
    }
  })


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

  // Conectar a SignalR si no está conectado
  try {
    await signalRService.connect()
  } catch (error) {
    console.warn('No se pudo conectar a SignalR:', error)
  }
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

<style scoped lang="scss">
.confirmar-pedido-card {
  width: 100%;
  max-width: 800px;
  min-width: auto;
}

/* Extra small devices (xs) - 0px to 599px */
@media (max-width: 599px) {
  .confirmar-pedido-card {
    width: 95vw;
    max-width: 100%;
    margin: 0 auto;
  }

  :deep(.q-card__section) {
    padding: 12px !important;
  }

  :deep(.q-card) {
    box-shadow: none;
  }

  :deep(.q-item__label) {
    font-size: 13px;
  }

  :deep(.q-input__native) {
    font-size: 14px;
  }

  .text-h6 {
    font-size: 16px !important;
  }

  .text-h5 {
    font-size: 18px !important;
  }

  .text-subtitle1 {
    font-size: 14px !important;
  }

  .text-subtitle2 {
    font-size: 12px !important;
  }

  :deep(.q-card__actions) {
    flex-direction: column-reverse;
    gap: 8px;
    padding: 12px !important;
    width: 100%;
    align-items: stretch;
  }

  :deep(.q-card__actions .q-btn) {
    width: calc(100% - 20px);
    flex: 1 1 auto;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }

  :deep(.q-item-side) {
    margin-left: 8px;
  }
}

/* Small devices (sm) - 600px to 1023px */
@media (min-width: 600px) and (max-width: 1023px) {
  .confirmar-pedido-card {
    width: 90vw;
    max-width: 600px;
  }

  :deep(.q-card__section) {
    padding: 16px;
  }

  .text-h6 {
    font-size: 18px !important;
  }

  :deep(.q-card__actions button) {
    padding: 8px 16px;
  }
}

/* Medium devices (md) and larger - 1024px and up */
@media (min-width: 1024px) {
  .confirmar-pedido-card {
    width: 100%;
    max-width: 800px;
    min-width: 600px;
  }
}
</style>
