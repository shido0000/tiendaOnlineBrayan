import { ref } from 'vue'
import { loadGetDatosInicio } from './funciones'

const inicio = ref(null)

export async function fetchInicio() {
    try {
        const data = await loadGetDatosInicio('ObtenerDatosInicio')
        inicio.value =
            data?.categoriasProductos ??
            data?.categorias ??
            data?.data?.categorias ??
            data?.result?.categoriasProductos ??
            []
    } catch (e) {
        console.warn('Error cargando categorías', e)
        inicio.value = []
    }
}

export function useInicio() {
    return { inicio, fetchInicio }
}
