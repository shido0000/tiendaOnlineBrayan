<template>
  <!-- No necesita UI visible, todo se maneja por eventos -->
  <div style="display:none"></div>
</template>

<script setup>
import { onMounted, onUnmounted } from 'vue'
import { api } from 'src/boot/axios'
import useCart from 'src/stores/cartStore'
import { Success, Error } from 'src/boot/notify'

const cart = useCart()
let bufferCodigo = ''

async function procesarCodigo(sku) {
  if (!sku) return
  try {
    const response = await api.get(`Producto/ObtenerPorSku/${sku}`)
    const producto = response.data.result

    if (producto) {
      const existente = cart.items.find(p => p.id === producto.id)
      if (existente) {
        cart.updateQuantity(producto.id, existente.cantidad + 1)
      } else {
        cart.addItem({
          id: producto.id,
          nombre: producto.descripcion,
          cantidad: 1,
          precioVenta: producto.precioVenta,
          raw: producto
        })
      }
      Success(`Producto ${producto.descripcion} agregado al carrito`)
    } else {
      Error('Producto no encontrado')
    }
  } catch (err) {
    console.error(err)
    Error('Error buscando producto')
  }
}

function handleKeydown(e) {
  if (e.key === 'Enter') {
    procesarCodigo(bufferCodigo)
    bufferCodigo = ''
  } else {
    bufferCodigo += e.key
  }
}

onMounted(() => {
  document.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
  document.removeEventListener('keydown', handleKeydown)
})
</script>
