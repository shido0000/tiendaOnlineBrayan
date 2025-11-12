<template>
  <div class="q-pa-md">
    <q-card class="q-pa-lg" style="max-width:720px; margin:auto">
      <q-card-section>
        <div class="text-h6">Mi Perfil</div>
        <div class="text-subtitle2 q-mt-sm">Revisa y actualiza tus datos (no puedes cambiar el rol)</div>
      </q-card-section>

      <q-separator />

      <q-card-section>
        <q-form @submit.prevent="onSave">
          <q-input v-model="name" label="Nombre completo" dense outlined class="q-mb-md" />
          <q-input v-model="email" label="Email" type="email" dense outlined class="q-mb-md" />
          <q-input v-model="password" label="Nueva password (dejar vacío para mantener)" type="password" dense outlined class="q-mb-md" />
          <q-input v-model="confirmPassword" label="Confirmar nueva password" type="password" dense outlined class="q-mb-md" />
          <q-input v-model="role" label="Rol" dense outlined class="q-mb-md" readonly />

          <div class="row items-center q-pt-md">
            <q-btn label="Guardar" type="submit" color="primary" />
            <q-space />
            <q-btn flat label="Cerrar sesión" color="negative" @click="onLogout" />
          </div>
        </q-form>
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'

// Modo SIMULADO: guardamos los cambios en localStorage bajo la clave
// 'fashion_profile_sim'. Esto permite trabajar sin la rama backend.
const STORAGE_KEY = 'fashion_profile_sim'
const FALLBACK_USER_KEYS = ['user', 'currentUser', 'session']

const $q = useQuasar()
const router = useRouter()

const name = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const role = ref('')

function loadFromLocal() {
  // Intenta cargar la versión simulada primero
  const raw = localStorage.getItem(STORAGE_KEY)
  if (raw) {
    try {
      const obj = JSON.parse(raw)
      name.value = obj.nombre || obj.name || ''
      email.value = obj.correo || obj.email || ''
      role.value = obj.rol || obj.role || obj.rolNombre || ''
      return true
    } catch (_) {}
  }

  // Si no hay, intenta cargar alguna clave común usada por el login simulado
  for (const k of FALLBACK_USER_KEYS) {
    const v = localStorage.getItem(k)
    if (!v) continue
    try {
      const obj = JSON.parse(v)
      name.value = obj.nombre || obj.name || obj.fullName || ''
      email.value = obj.correo || obj.email || obj.username || ''
      role.value = obj.rol || obj.role || obj.rolNombre || ''
      return true
    } catch (_) {}
  }

  return false
}

onMounted(() => {
  const ok = loadFromLocal()
  if (!ok) {
    // Redirige al login si no hay sesión conocida
    router.push({ name: 'LoginPage' })
  }
})

function saveToLocal(payload) {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(payload))
}

async function onSave() {
  if (!name.value || !email.value) {
    $q.notify({ type: 'negative', message: 'Nombre y email son requeridos' })
    return
  }
  if (password.value && password.value !== confirmPassword.value) {
    $q.notify({ type: 'negative', message: 'Las contraseñas no coinciden' })
    return
  }

  const updated = {
    nombre: name.value,
    correo: email.value,
    rol: role.value
  }
  if (password.value) updated.contrasenna = password.value

  try {
    saveToLocal(updated)
    $q.notify({ type: 'positive', message: 'Perfil guardado (simulado localmente)' })
  } catch (err) {
    $q.notify({ type: 'negative', message: 'Error al guardar (simulado)' })
  }
}

function onLogout() {
  // Limpia datos simulados y redirige a Login
  localStorage.removeItem(STORAGE_KEY)
  for (const k of FALLBACK_USER_KEYS) localStorage.removeItem(k)
  router.push({ name: 'LoginPage' })
}
</script>

<style scoped>
</style>
