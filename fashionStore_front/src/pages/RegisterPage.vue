<template>
  <div class="login-bg">
    <div class="login-card">
      <q-avatar size="80px" class="q-mb-md bg-white"><q-icon name="person_add" size="60px" color="grey-5" /></q-avatar>
      <q-form @submit.prevent="onRegister">
        <q-input v-model="name" label="Nombre completo" type="text" dense outlined class="q-mb-md" prepend-inner-icon="person" />
        <q-input v-model="email" label="Email" type="email" dense outlined class="q-mb-md" prepend-inner-icon="mail" />
        <q-input v-model="password" label="Password" type="password" dense outlined class="q-mb-md" prepend-inner-icon="lock" />
        <q-input v-model="confirmPassword" label="Confirmar Password" type="password" dense outlined class="q-mb-md" prepend-inner-icon="lock" />
        <q-btn type="submit" label="REGISTRARSE" color="purple-7" class="full-width login-btn q-mb-md" />
      </q-form>
      <div class="text-center q-mt-md">
        <span class="text-grey-4">¿Ya tienes cuenta?</span>
        <q-btn flat label="Inicia sesión" color="purple-4" @click="$router.push('/login')" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'RegisterPage',
  data() {
    return {
      name: '',
      email: '',
      password: '',
      confirmPassword: ''
    }
  },
  methods: {
    onRegister() {
      if (!this.name || !this.email || !this.password || !this.confirmPassword) {
        this.$q.notify({ type: 'negative', message: 'Completa todos los campos' })
        return
      }
      if (this.password !== this.confirmPassword) {
        this.$q.notify({ type: 'negative', message: 'Las contraseñas no coinciden' })
        return
      }
      // Registro SIMULADO: persistimos la sesión mínima para trabajar localmente
      const simulated = {
        nombre: this.name,
        correo: this.email,
        rol: 'Cliente',
        // en modo simulado guardamos la contraseña bajo el campo que espera el backend
        contrasenna: this.password
      }
      try {
        localStorage.setItem('fashion_profile_sim', JSON.stringify(simulated))
      } catch (e) {
        // ignore
      }
      this.$q.notify({ type: 'positive', message: 'Registro simulado' })
      this.$router.push('/NomenclatorsCard')
    }
  }
}
</script>

<style scoped lang="scss">
@import '../css/quasar.variables.scss';
.login-bg {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: radial-gradient(circle at 50% 30%, $bry-primary 0%, $bry-secondary 100%);
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
