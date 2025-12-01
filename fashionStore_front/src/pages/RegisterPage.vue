<template>
  <div class="login-bg">
    <div class="login-card">
<div class="row justify-center q-mb-md">
  <q-avatar size="80px" class="bg-white">
    <q-icon name="person_add" size="60px" color="grey-5" />
  </q-avatar>
</div>


      <q-form @submit.prevent="onRegister">
        <q-input
          v-model="objeto.nombre"
          label="Nombre"
          type="text"
          dense
          outlined
          class="q-mb-md col-12"
          prepend-inner-icon="person"
        />
        <q-input
          v-model="objeto.apellidos"
          label="Apellido"
          type="text"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="person"
        />
         <q-input
          v-model="objeto.username"
          label="Username"
          type="text"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="person"
        />
        <q-input
          v-model="objeto.correo"
          label="Email"
          type="email"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="mail"
        />
        <q-input
          v-model="objeto.telefono"
          label="Teléfono"
          type="text"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="phone"
        />
        <q-input
          v-model="objeto.contrasenna"
          label="Password"
          type="password"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="lock"
        />
        <q-input
          v-model="objeto.contrasennaConfirmada"
          label="Confirmar Password"
          type="password"
          dense
          outlined
          class="q-mb-md"
          prepend-inner-icon="lock"
        />
        <q-btn
          type="submit"
          label="REGISTRARSE"
          color="purple-7"
          class="full-width login-btn q-mb-md"
        />
      </q-form>

      <div class="text-center q-mt-md">
        <span class="text-black-4">¿Ya tienes cuenta?</span>
        <q-btn
          flat
          label="Inicia sesión"
          color="purple-4"
          @click="goToLogin"
        />
      </div>
    </div>
  </div>
  <DialogLoad :dialogLoad="dialogLoad" />

</template>

<script setup>
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'
import { loadGet, saveData, saveDataPronosticoEnviarObjeto } from 'src/assets/js/util/funciones'
import { Error } from 'src/assets/js/util/notify'
import DialogLoad from 'src/components/DialogBoxes/DialogLoad.vue'

const dialogLoad = ref(false)


const objetoInicial = {
  // id: null,
  nombreCompleto: null,
  nombre: null,
  apellidos: null,
  username: null,
  correo: null,
  telefono:null,
  rolId:null,
  esActivo:true,
  contrasenna:null,
  contrasennaConfirmada:null,
}

const objeto = reactive({ ...objetoInicial })


const router = useRouter()
const $q = useQuasar()

const itemsRol = ref([])


const onRegister = () => {
    Guardar()
}

const goToLogin = () => {
  router.push('/login')
}

const Guardar = async() => {
  //const url = objeto.id ? 'Usuario/Actualizar' : 'Usuario/Crear'
  const url =  'Usuario/Crear'
 if (!objeto.nombre || !objeto.apellidos || !objeto.username || !objeto.correo  || !objeto.telefono|| !objeto.contrasenna || !objeto.contrasennaConfirmada) {
    $q.notify({ type: 'negative', message: 'Completa todos los campos' })
    return
  }
  if (objeto.contrasenna !== objeto.contrasennaConfirmada) {
    $q.notify({ type: 'negative', message: 'Las contraseñas no coinciden' })
    return
  }

  await saveDataPronosticoEnviarObjeto(url, objeto, dialogLoad).then(
    (respuesta) => {
      if (!!respuesta?.mensajeError) {
           if (respuesta?.mensajeError?.includes('NombreCompleto')) {
                         Error("Ya existe un usuario con este mismo nombre completo.")
                      }
                      else  if (respuesta?.mensajeError?.includes('Username')) {
                         Error("Ya existe un usuario con este mismo username.")
                      }
                      else{
        Error(respuesta?.mensajeError)
        }
      } else {
         $q.notify({ type: 'positive', message: 'Registro exitoso' })
  router.push('/login')
      }
    }
  )

    // Aquí va la lógica de registro real
}


onMounted(async () => {
  itemsRol.value = await loadGet('Rol/ObtenerListadoPaginado')??[]
  let clienteId= (itemsRol.value.find(e=>e.nombre==="Cliente")).id
  objeto.rolId=clienteId
})

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
}

.login-btn {
  background: linear-gradient(90deg, $bry-secondary 0%, $bry-primary 100%);
  color: $bry-white;
  font-weight: bold;
  letter-spacing: 2px;
}
</style>
