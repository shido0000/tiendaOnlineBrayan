<template>
  <!-- No necesita UI visible, todo se maneja por eventos -->
  <div style="display:none"></div>
</template>

<script setup>
import { onMounted, onUnmounted } from 'vue'
import { api } from 'src/boot/axios'
import useCart from 'src/stores/cartStore'
import { Success, Error,  StockError } from 'src/boot/notify'

const cart = useCart()
let bufferCodigo = ''
/*
async function procesarCodigo(sku) {
  if (!sku) return
  try {
    const response = await api.get(`Producto/ObtenerPorSku/${sku}`)
    const producto = response.data

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
    //  Success(`Producto ${producto.descripcion} agregado al carrito`)
    } else {
      Error('Producto no encontrado')
    }
  } catch (err) {
    console.error(err)
    Error('Error buscando producto')
  }
}*/
 async function procesarCodigo(sku) {
  if (!sku) return

  try {
    const response = await api.get(`Producto/ObtenerPorSku/${sku}`)
    const producto = response.data

    if (!producto) {
      Error('Producto no encontrado')
      return
    }

    // 🎯 VALIDAR STOCK ANTES DE AÑADIR/ACTUALIZAR
    const validation = canAddMore(producto, 1)

    if (!validation.allowed) {
      // ❌ BLOQUEAR: Stock insuficiente
     StockError(producto.descripcion || producto.nombre, validation)
  return
    }

    // ✅ Todo OK: proceder a añadir/actualizar
    const existente = cart.items.find(p => p.id === producto.id)

    if (existente) {
      cart.updateQuantity(producto.id, existente.cantidad + 1)
    } else {
      cart.addItem({
        id: producto.id,
        nombre: producto.descripcion || producto.nombre,
        cantidad: 1,
        precioVenta: producto.precioVenta,
        precioVentaDescuento: producto.precioVentaDescuento,
        tieneDescuento: producto.tieneDescuento,
        raw: producto,
        // 🎯 Guardar foto principal si existe (para evitar buscarla después)
        foto: producto.productoVariantes?.[0]?.fotos?.[0]?.url ||
              producto.fotos?.[0]?.url ||
              null
      })
    }

    // ✅ Feedback discreto (opcional: comentar si es muy frecuente el escaneo)
    // Success(`${producto.descripcion} añadido`)

  } catch (err) {
    console.error('Error en procesarCodigo:', err)
    Error('Error buscando producto')
  }
}
/*
function handleKeydown(e) {
    console.log("e: ",e

    )
  // Si es Enter, Tab o Enter del keypad → procesar
  if (['Enter', 'Tab', 'NumpadEnter'].includes(e.key)) {
    procesarCodigo(bufferCodigo.trim())
    bufferCodigo = ''
    e.preventDefault()
    return
  }

  // Solo agregar caracteres imprimibles
  if (e.key.length === 1) {
    bufferCodigo += e.key
  }
}*/
function handleKeydown(e) {
  // Ignorar si el foco está en un input/textarea (evitar conflicto con formularios)
  const target = e.target
  if (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA' || target.isContentEditable) {
    return
  }

  // Si es Enter, Tab o NumpadEnter → procesar código
  if (['Enter', 'Tab', 'NumpadEnter'].includes(e.key)) {
    e.preventDefault()
    e.stopPropagation()

    if (bufferCodigo.trim()) {
      procesarCodigo(bufferCodigo.trim())
    }
    bufferCodigo = ''
    return
  }

  // Solo agregar caracteres imprimibles (evitar teclas de control)
  if (e.key.length === 1 && !e.ctrlKey && !e.altKey && !e.metaKey) {
    bufferCodigo += e.key
  }
}


onMounted(() => {
  document.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
  document.removeEventListener('keydown', handleKeydown)
})


// 🎯 Función para obtener el stock máximo de un producto (igual que en CarritoPage)
function getMaxStock(producto) {
  // Si el producto viene con raw anidado (caso scanner)
  const prod = producto.raw?.raw || producto.raw || producto

  if (prod) {
    // Stock directo en producto
    let stock = prod.stock || prod.cantidadDisponible || prod.stockTotal
    if (stock) return stock

    // Stock desde variantes (productoVariantes)
    if (Array.isArray(prod.productoVariantes) && prod.productoVariantes.length > 0) {
      // Si hay variante principal, usar esa
      const principal = prod.productoVariantes.find(v => v.principal) || prod.productoVariantes[0]
      if (principal?.stock) return principal.stock
    }

    // Stock desde variants (estructura alternativa)
    if (Array.isArray(prod.variants) && prod.variants.length > 0) {
      const principal = prod.variants.find(v => v.principal) || prod.variants[0]
      if (principal?.stock) return principal.stock
    }
  }

  // Fallback
  return producto.stock || producto.cantidadDisponible || producto.stockTotal || 1
}

// 🎯 Validar si se puede añadir más cantidad de un producto
function canAddMore(producto, cantidadASumar = 1) {
  const maxStock = getMaxStock(producto)
  const existente = cart.items.find(p => p.id === producto.id)
  const cantidadActual = existente?.cantidad || 0
  const cantidadFinal = cantidadActual + cantidadASumar

  return {
    allowed: cantidadFinal <= maxStock,
    current: cantidadActual,
    max: maxStock,
    remaining: Math.max(0, maxStock - cantidadActual)
  }
}

</script>
