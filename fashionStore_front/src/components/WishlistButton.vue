<template>
  <div class="wishlist-button-root">
    <q-btn color="primary" flat round dense :icon="iconName" @click="openWishlist" :class="{ animated: anim } ">
        <q-tooltip   transition-show="flip-right" transition-hide="flip-left" :offset="[10, 10]">Lista de deseos</q-tooltip>
      <q-badge v-if="count > 0" color="red" floating transparent class="wishlist-badge">{{ count }}</q-badge>
    </q-btn>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import { useWishlist } from 'src/stores/wishlistStore'

const router = useRouter()
const wishlist = useWishlist()
const count = wishlist.count
const anim = ref(false)

watch(() => wishlist.items, (v) => {
  if (!v) return
  anim.value = true
  setTimeout(() => anim.value = false, 700)
})

function openWishlist() {
  router.push({ name: 'ListaDeseos' }).catch(() => router.push('/lista_deseos'))
}

const iconName = 'favorite'
</script>

<style scoped>
.wishlist-badge { font-size: 12px; min-width: 18px; height:18px }
.animated { transform-origin: center; animation: pop 0.7s ease; }
@keyframes pop {
  0% { transform: scale(1); }
  30% { transform: scale(1.25); }
  60% { transform: scale(0.95); }
  100% { transform: scale(1); }
}
</style>
