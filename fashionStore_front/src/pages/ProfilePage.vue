<template>
  <div>
    <div class="q-pa-md profile-container">
      <q-card class="q-pa-md q-pa-sm-sm profile-card">
        <q-card-section>
          <div class="text-h6">Mi Perfil</div>
          <div class="text-subtitle2 q-mt-sm">
            Revisa y actualiza tus datos (no puedes cambiar el rol)
          </div>
        </q-card-section>

        <q-separator />

        <q-card-section>
          <q-form @submit.prevent="onSave">
            <q-input v-model="nombre" label="Nombre" dense outlined class="q-mb-md" />
            <q-input v-model="apellidos" label="Apellidos" dense outlined class="q-mb-md" />
            <q-input v-model="username" label="Usuario" dense outlined class="q-mb-md" />
            <q-input v-model="name" label="Nombre completo" dense outlined class="q-mb-md" readonly/>
            <q-input v-model="email" label="Email" type="email" dense outlined class="q-mb-md" />
            <q-input v-model="telefono" label="Teléfono" type="text" dense outlined class="q-mb-md" />
            <q-input v-model="password" label="Nueva password (dejar vacío para mantener)" type="password" dense outlined class="q-mb-md" />
            <q-input v-model="confirmPassword" label="Confirmar nueva password" type="password" dense outlined class="q-mb-md" />
            <q-input v-model="role" label="Rol" dense outlined class="q-mb-md" readonly />

            <div class="row items-center q-pt-md button-group">
              <q-btn label="Guardar" type="submit" color="primary" />
              <q-space />
              <q-btn flat label="Cerrar sesión" color="negative" @click="onLogout" />
            </div>
          </q-form>
        </q-card-section>
      </q-card>
    </div>

    <DialogLoad :dialogLoad="dialogLoad" />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'
import { api } from 'src/boot/axios'
import { Success, Error } from 'src/boot/notify'
import { saveData, saveDataPerfil } from 'src/assets/js/util/funciones'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'
import TopBar from './Visual/components/TopBar.vue'

const dialogLoad = ref(false)

const $q = useQuasar()
const router = useRouter()

// Campos del perfil
const Id = ref('')
const nombre = ref('')
const apellidos = ref('')
const username = ref('')
const name = ref('')
const email = ref('')
const telefono = ref('')
const password = ref('')
const confirmPassword = ref('')
const role = ref('')

// --- Helpers ---
function checkUserSession() {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  const exp = localStorage.getItem('token_exp') || sessionStorage.getItem('token_exp')

  if (!token) return false
  if (exp) {
    const expDate = new Date(exp)
    if (expDate < new Date()) {
      console.warn('Token expirado')
      return false
    }
  }
  return true
}

// Cargar usuario desde backend
async function loadUserFromBackend() {
    dialogLoad.value=true
  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  if (!token) return false

  try {
    const response = await api.get('/Autenticacion/UsuarioActual')

    const obj = response.data.result
    Id.value=obj.id|| ''
    nombre.value = obj.nombre || ''
    apellidos.value = obj.apellidos || ''
    username.value = obj.username || ''
    name.value = obj.nombreCompleto || ''
    email.value = obj.correo || ''
    telefono.value = obj.telefono || ''
    role.value = obj.rolNombre || obj.role || ''
    dialogLoad.value=false

    return true
  } catch (err) {
    console.error('Error al obtener usuario:', err)
    dialogLoad.value=false

    return false
  }

}

// Guardar cambios en backend
async function onSave() {
  if (!nombre.value||!apellidos.value || !email.value || !telefono.value) {
    $q.notify({ type: 'negative', message: 'Nombre, apellidos, teléfono  y email son requeridos' })
    return
  }
  if (password.value && password.value !== confirmPassword.value) {
    $q.notify({ type: 'negative', message: 'Las contraseñas no coinciden' })
    return
  }

  const token = localStorage.getItem('token') || sessionStorage.getItem('token')
  if (!token) {
    router.push({ name: 'LoginPage' })
    return
  }

  const usuarioDto = {
    id:Id.value,
  nombre: nombre.value,
  apellidos: apellidos.value,
  username: username.value,
  correo: email.value,
  telefono: telefono.value,
  contrasenna: "",
  }
  if (password.value) usuarioDto.contrasenna = password.value

  try {
      const url =   'Usuario/ActualizarPerfil'
    await saveDataPerfil(url, usuarioDto, dialogLoad)
    Success('Perfil actualizado correctamente')
    router.push({ name: 'LoginPage' })
  } catch (err) {
    Error('Error al actualizar perfil')
    console.error(err)
  }
}

// Cerrar sesión
function onLogout() {
  localStorage.removeItem('token')
  localStorage.removeItem('token_exp')
  sessionStorage.removeItem('token')
  sessionStorage.removeItem('token_exp')
  router.push({ name: 'LoginPage' })
}

// --- Ciclo de vida ---
onMounted(async () => {
  const hasSession = checkUserSession()
  if (!hasSession) {
    router.push({ name: 'LoginPage' })
    return
  }

  const loaded = await loadUserFromBackend()

  if (!loaded) {
    Error('No se pudo cargar la información del usuario')
  }
})
</script>

<style scoped lang="scss">
.profile-container {
  min-height: 100vh;
  background-color: #f5f5f5;
  padding: 20px;
}

.profile-card {
  max-width: 720px;
  margin: auto;
}

.button-group {
  flex-wrap: wrap;
  gap: 10px;
}

/* Media Queries para responsividad */
@media (max-width: 599px) {
  .profile-container {
    padding: 10px;
  }

  .profile-card {
    padding: 15px !important;
  }

  :deep(.q-field) {
    font-size: 14px;
  }

  .button-group {
    flex-direction: column;
  }

  .button-group q-btn {
    width: 100%;
  }
}

@media (max-width: 1023px) and (min-width: 600px) {
  .profile-card {
    max-width: 100%;
  }
}

@media (min-width: 1366px) {
  .profile-container {
    padding: 40px 20px;
  }
}
</style>
