<template>
  <div>
    <TopBar/>

    <div class="q-pa-lg bg-grey-1 carrito-page">
      <div class="row items-center q-mb-lg">
        <div class="col">
          <div class="text-h5 text-weight-bold text-primary">
             Carrito de Compras
          </div>
        </div>
        <div class="col-auto">
          <q-btn
          dense
          round
            flat
            color="negative"
            @click="clearAll"
            icon="delete"
            v-if="items.length"
          ><q-tooltip>Eliminar Productos</q-tooltip>
        </q-btn>
          </div>
      </div>


      <!-- Carrito vacío -->
      <div v-if="!items.length" class="q-pa-xl text-center text-grey-7">
        <q-icon name="shopping_cart" size="48px" color="grey-5" class="q-mb-md" />
        <div class="text-subtitle1">Tu carrito está vacío</div>
      </div>

      <!-- Lista de productos -->
      <div v-else>
        <div class="q-mb-md">
          <div v-for="it in items" :key="it.id" class="q-mb-md">
            <q-card bordered class="q-pa-sm row items-center shadow-2 rounded-borders">
              <!-- Imagen -->
              <div class="col-auto">
                <q-img
                  :src="obtenerFotoDelItem(it)"
                  class="carrito-img"
                />
              </div>

              <!-- Info producto -->
              <div class="col q-pl-md">
                <div class="text-subtitle1 text-weight-bold">{{ it.nombre }}</div>
                <div class="text-caption text-grey-6">
                  Precio unitario: ${{ (it.precioVenta || 0).toLocaleString('es-ES',{ minimumFractionDigits:2 }) }}
                </div>
              </div>


              <!-- Subtotal -->
              <div class="col-auto text-right q-pr-md">
                <div class="text-subtitle2 text-weight-bold text-primary">
                  ${{ ((it.precioVenta||0) * (it.cantidad||0)).toLocaleString('es-ES',{ minimumFractionDigits:2 }) }}
                </div>
              </div>
<!-- Cantidad -->
             <div class="col-auto flex items-end ">
  <q-input
    type="number"
    dense
    outlined
    v-model.number="it.cantidad"
    @change="onQtyChange(it)"
    style="width:90px; margin-top: 20px;"
    :min="1"
    :max="getMaxStock(it)"
    :rules="[
      val => val >= 1 || 'La cantidad mínima es 1',
      val => val <= getMaxStock(it) || `La cantidad máxima disponible es ${getMaxStock(it)}`
    ]"
  >
    <q-tooltip>Cantidad máxima: {{ getMaxStock(it) }}</q-tooltip>
  </q-input>
              </div>

              <!-- Botón eliminar -->
              <div class="col-auto">
                <q-btn round dense flat color="negative" icon="delete" @click="remove(it.id)">
                  <q-tooltip>Eliminar</q-tooltip>
                </q-btn>
              </div>
            </q-card>
          </div>
        </div>

        <!-- Total -->
        <q-separator spaced />
        <div class="q-pt-md row items-center justify-end carrito-total">
          <div class="text-h6 text-weight-bold q-mr-lg">
            Total: ${{ totalPrice.toLocaleString('es-ES',{ minimumFractionDigits:2 }) }}
          </div>
          <q-btn
  color="primary"
  unelevated
  icon="payment"
  label="Proceder al pago"
  @click="confirmarPedido.openDialog()"
/>
        </div>
      </div>
    </div>
  </div>

  <ConfirmarPedido ref="confirmarPedido" :desdeElCarrito="true" :productoItem="null" :cantidad="0"/>
</template>

<script setup>
import { Success } from 'src/boot/notify'
import TopBar from './components/TopBar.vue'
import useCart from 'src/stores/cartStore'
import { apiFotosBaseUrl } from 'src/boot/axios'
import ConfirmarPedido from './components/ConfirmarPedido.vue'
import { ref } from 'vue'
import { getFotoFromVarianteWithFallback } from 'src/assets/js/util/funciones'

const cart = useCart()
const items = cart.items
const totalPrice = cart.totalPrice
const confirmarPedido = ref(null)

function obtenerFotoDelItem(item) {
  // Prioridad 1: foto directo del item (guardada desde cartStore)
  if (item.foto) {
    return getFotoUrl(item.foto)
  }

  // Prioridad 2: primer foto del raw (producto completo)
  if (item.raw) {
    // Si tiene fotos como array de objetos (estructura ProductoDetallePage)
    if (Array.isArray(item.raw.fotos) && item.raw.fotos.length > 0) {
      const foto = item.raw.fotos[0]
      if (typeof foto === 'object') {
        return getFotoUrl(foto.url || foto.imagen || foto.path)
      } else {
        return getFotoUrl(foto)
      }
    }

    // Si tiene fotoUrl directo
    if (item.raw.fotoUrl) {
      return getFotoUrl(item.raw.fotoUrl)
    }

    // Si tiene variants (variantes)
    if (Array.isArray(item.raw.variants) && item.raw.variants.length > 0) {
      const variant = item.raw.variants[0]
      if (Array.isArray(variant.fotos) && variant.fotos.length > 0) {
        const foto = variant.fotos[0]
        if (typeof foto === 'object') {
          return getFotoUrl(foto.url || foto.imagen || foto.path)
        } else {
          return getFotoUrl(foto)
        }
      }
      // Si la variante no tiene foto, buscar en otras variantes del mismo producto
      const fotoHeredada = getFotoFromVarianteWithFallback(variant, item.raw)
      if (fotoHeredada) {
        const heredada = typeof fotoHeredada === 'object' ? (fotoHeredada.url || fotoHeredada.imagen || fotoHeredada.path) : fotoHeredada
        if (heredada) return getFotoUrl(heredada)
      }
    }

    // Si tiene productoVariantes (estructura del API)
    if (Array.isArray(item.raw.productoVariantes) && item.raw.productoVariantes.length > 0) {
      const variant = item.raw.productoVariantes[0]
      if (Array.isArray(variant.fotos) && variant.fotos.length > 0) {
        const foto = variant.fotos[0]
        if (typeof foto === 'object') {
          return getFotoUrl(foto.url || foto.imagen || foto.path)
        } else {
          return getFotoUrl(foto)
        }
      }
      // Si la variante no tiene foto, buscar en otras variantes del mismo producto
      const fotoHeredada = getFotoFromVarianteWithFallback(variant, item.raw)
      if (fotoHeredada) {
        const heredada = typeof fotoHeredada === 'object' ? (fotoHeredada.url || fotoHeredada.imagen || fotoHeredada.path) : fotoHeredada
        if (heredada) return getFotoUrl(heredada)
      }
    }
  }

  // Default
  return '/img/sin-foto.jpg'
}

function getFotoUrl(foto) {
  if (!foto) return '/img/sin-foto.jpg'
  let candidate = foto
  if (typeof foto === 'object') {
    candidate = foto?.url || foto?.img || foto?.path || foto?.imagen || foto?.foto || null
  }
  if (!candidate) return '/img/sin-foto.jpg'
  if (typeof candidate !== 'string') candidate = String(candidate)
  if (/^https?:\/\//.test(candidate)) return candidate
  return apiFotosBaseUrl + (candidate.startsWith('/') ? candidate : '/' + candidate)
}

function clearAll() {
  cart.items.splice(0, cart.items.length)
  Success("Carrito vaciado")
}

function getMaxStock(item) {
  // Intentar obtener stock desde el producto raw
  if (item.raw) {
    let stock = item.raw.stock || item.raw.cantidadDisponible || item.raw.stockTotal
    if (stock) return stock
    // Si tiene variantes, obtener del primer variante
    if (Array.isArray(item.raw.productoVariantes) && item.raw.productoVariantes.length > 0) {
      stock = item.raw.productoVariantes[0].stock
      if (stock) return stock
    }
    if (Array.isArray(item.raw.variants) && item.raw.variants.length > 0) {
      stock = item.raw.variants[0].stock
      if (stock) return stock
    }
  }
  // Fallback a valores directos del item
  return item.stock || item.cantidadDisponible || item.stockTotal || 1
}

function remove(id) { cart.removeItem(id) }
function onQtyChange(item) {
  const maxStock = getMaxStock(item)
  // Validar que no exceda el stock disponible
  if (item.cantidad > maxStock) {
    item.cantidad = maxStock
  }
  // si la cantidad es menor que 1, la forzamos a 1
  if (!item.cantidad || item.cantidad < 1) {
    item.cantidad = 1
  }
  cart.updateQuantity(item.id, item.cantidad)
}
</script>

<style scoped>
.carrito-page {
  max-width: 1100px;
  margin: 0 auto;
  margin-top: 32px;   /* 👈 añade separación desde arriba */
  padding-top: 16px;  /* opcional, para más aire interno */
}

.carrito-img {
  width: 100px;
  height: 80px;
  object-fit: cover;
  border-radius: 6px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  transition: transform 0.2s ease;
}
.carrito-img:hover {
  transform: scale(1.05);
}

.carrito-total {
  background: #f9fafb;
  padding: 16px;
  border-radius: 8px;
}
</style>
