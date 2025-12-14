import { reactive } from 'vue'

const state = reactive({
  productosActualizados: 0
})

export function notifyProductosActualizados() {
  state.productosActualizados++
}

export function getProductosActualizados() {
  return state.productosActualizados
}
