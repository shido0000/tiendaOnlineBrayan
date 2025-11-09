<template>
  <div>
    <TopBar/>

    <div class="q-pa-lg bg-grey-1 carrito-page">
      <div class="text-h5 text-weight-bold q-mb-lg text-primary">
        Carrito de Compras
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
                  :src="getFotoUrl(it.foto || (it.raw && it.raw.fotos && it.raw.fotos[0]))"
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

   :rules="[val => val >= 1 || 'La cantidad mínima es 1']"
> <q-tooltip>Cantidad</q-tooltip></q-input>
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
          <q-btn color="primary" unelevated icon="payment" label="Proceder al pago" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import TopBar from './components/TopBar.vue'
import useCart from 'src/stores/cartStore'
import { apiFotosBaseUrl } from 'src/boot/axios'

const cart = useCart()
const items = cart.items
const totalPrice = cart.totalPrice

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

function remove(id) { cart.removeItem(id) }
function onQtyChange(item) {
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
