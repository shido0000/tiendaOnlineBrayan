<template>
  <div class="login-bg">
    <div class="login-card">
      <q-avatar size="80px" class="q-mb-md bg-white">
        <q-icon name="person" size="60px" color="grey-5" />
      </q-avatar>

      <q-form @submit.prevent="onLogin">
       <q-input
  v-model="username"
  label="Username"
  type="text"
  dense
  outlined
  class="q-mb-md"
  prepend-inner-icon="person"
/>

        <q-input
          v-model="password"
          label="Password"
          type="password"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="lock"
        />

        <div class="row items-center q-mb-md">
          <q-checkbox v-model="remember" label="Recuérdame" />
          <q-space />
          <!--   <q-btn
            flat
            label="Forgot Password?"
            class="text-grey-4"
            @click="onForgot"
          />-->
        </div>

        <q-btn
          type="submit"
          label="LOGIN"
          color="purple-7"
          class="full-width login-btn q-mb-md"
        />
      </q-form>

      <div class="text-center q-mt-md">
        <span class="text-black-4">¿No tienes cuenta?</span>
        <q-btn flat label="Regístrate" color="purple-4" @click="onRegister" />
      </div>
    </div>
  </div>

   <!-- Diálogo para recuperar contraseña -->
      <q-dialog v-model="showForgotDialog">
        <q-card style="min-width: 350px">
          <q-card-section>
            <div class="text-h6">Recuperar contraseña</div>
          </q-card-section>

          <q-card-section>
            <q-input
              v-model="forgotEmail"
              label="Correo electrónico"
              type="email"
              outlined
              dense
              autofocus
            />
          </q-card-section>

          <q-card-actions align="right">
            <q-btn flat label="Cancelar" v-close-popup />
            <q-btn flat label="Enviar" color="primary" @click="onForgot" />
          </q-card-actions>
        </q-card>
      </q-dialog>

  <DialogLoad :dialogLoad="dialogLoad" />
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'

import { loadGet, saveDataPronosticoEnviarObjeto } from 'src/assets/js/util/funciones'
import { Error } from 'src/assets/js/util/notify'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'

// refs reactivas
const username = ref('')
const password = ref('')
const remember = ref(false)
const dialogLoad = ref(false)

const showForgotDialog = ref(false)
const forgotEmail = ref('')

const router = useRouter()
const $q = useQuasar()

// Métodos
const onLogin = async () => {
  //$q.notify({ type: 'positive', message: 'Login simulado' })
  //router.push('/NomenclatorsCard')

  // Si quieres usar la API real:
   await login()
}

const onForgot = async () => {
  if (!forgotEmail.value) {
    $q.notify({ type: 'negative', message: 'Introduce tu correo' })
    return
  }

  const url = 'Autenticacion/ForgotPassword'
  const payload = { email: forgotEmail.value }

  try {
    await saveDataPronosticoEnviarObjeto(url, payload)
    $q.notify({ type: 'positive', message: 'Se ha enviado un correo de recuperación' })
    showForgotDialog.value = false
  } catch (err) {
    $q.notify({ type: 'negative', message: 'Error al enviar correo de recuperación' })
  }
}

const onRegister = () => {
  router.push('/register')
}

// Función de login real
const login = async () => {
  const url = 'Autenticacion/Login'
  const payload = {
    username: username.value,
    contrasenna: password.value
  }

  await saveDataPronosticoEnviarObjeto(url, payload, dialogLoad).then(
    async (respuesta) => {
      if (!!respuesta?.mensajeError) {
        Error(respuesta?.mensajeError)
      } else {
        let  token=respuesta?.resultado?.result?.token;
 let  exp=respuesta?.resultado?.result?.fechaExpiracion;
        
        if (token) {
          if (remember.value) {
            // ✅ Guardar en localStorage si marcó "Recuérdame"
            localStorage.setItem('token', token)
            localStorage.setItem('token_exp', exp)
          } else {
            // ✅ Guardar en sessionStorage si NO marcó "Recuérdame"
            sessionStorage.setItem('token', token)
            sessionStorage.setItem('token_exp', exp)
          }
        }

        $q.notify({ type: 'positive', message: 'Login exitoso' })

     //const informacion = await loadGet('Autenticacion/ObtenerInformacionUsuario')
      //console.log("informacion: ", informacion)

        router.push('/NomenclatorsCard')
      }
    }
  )
}
</script>

<style scoped lang="scss">
@import '../css/quasar.variables.scss';

.login-bg {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: radial-gradient(
    circle at 50% 30%,
    $bry-white 0%,
    $primary 100%
  );
}

.login-card {
  width: 350px;
  padding: 40px 32px 32px 32px;
  border-radius: 32px;
  background: rgba(255, 255, 255, 0.08);
  box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.37);
  backdrop-filter: blur(8px);
  -webkit-backdrop-filter: blur(8px);
  display: flex;
  flex-direction: column;
  align-items: center;
}

.login-btn {
  background: linear-gradient(90deg, $bry-secondary 0%, $bry-primary 100%);
  color: $bry-white;
  font-weight: bold;
  letter-spacing: 2px;
}
</style>
