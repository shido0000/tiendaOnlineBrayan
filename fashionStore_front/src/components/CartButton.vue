<template>
  <div class="cart-button-root">
    <q-btn color="primary" flat round dense icon="shopping_cart" @click="openCart" :class="{ animated: anim }">
        <q-tooltip  transition-show="flip-right" transition-hide="flip-left" :offset="[10, 10]">Carrito</q-tooltip>
      <q-badge v-if="count > 0" color="red" floating transparent class="cart-badge">{{ count }}</q-badge>
    </q-btn>
  </div>
</template>

<script setup>
import { ref, watch, computed } from 'vue'
import { useRouter } from 'vue-router'
import useCart from 'src/stores/cartStore'

const router = useRouter()
const cart = useCart()
const count = cart.totalCount
const anim = ref(false)

watch(() => cart.lastAddedAt, (v) => {
  if (!v) return
  anim.value = true
  setTimeout(() => anim.value = false, 700)
})

function openCart() {
  router.push({ name: 'Carrito' }).catch(() => { router.push('/carrito') })
}
</script>

<style scoped>
.cart-badge { font-size: 12px; min-width: 18px; height:18px }
.animated { transform-origin: center; animation: pop 0.7s ease; }
@keyframes pop {
  0% { transform: scale(1); }
  30% { transform: scale(1.25); }
  60% { transform: scale(0.95); }
  100% { transform: scale(1); }
}
</style>
