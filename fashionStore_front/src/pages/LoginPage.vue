<template>
  <div class="login-bg">
    <div class="login-card">
      <q-avatar size="80px" class="q-mb-md bg-white"><q-icon name="person" size="60px" color="grey-5" /></q-avatar>
      <q-form @submit.prevent="onLogin">
        <q-input v-model="email" label="Email" type="email" dense outlined class="q-mb-md" prepend-inner-icon="mail" />
        <q-input v-model="password" label="Password" type="password" dense outlined class="q-mb-md" prepend-inner-icon="lock" />
        <div class="row items-center q-mb-md">
          <q-checkbox v-model="remember" label="Recuérdame" />
          <q-space />
          <q-btn flat label="Forgot Password?" class="text-grey-4" @click="onForgot" />
        </div>
        <q-btn type="submit" label="LOGIN" color="purple-7" class="full-width login-btn q-mb-md" />
      </q-form>
      <div class="text-center q-mt-md">
        <span class="text-grey-4">¿No tienes cuenta?</span>
        <q-btn flat label="Regístrate" color="purple-4" @click="onRegister" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LoginPage',
  data() {
    return {
      email: '',
      password: '',
      remember: false
    }
  },
  methods: {
    onLogin() {
      // Lógica de login SIMULADO: persistimos una sesión mínima en localStorage
      const simulated = {
        nombre: 'Usuario Simulado',
        correo: this.email || 'demo@local',
        rol: 'Cliente'
      }
      try {
        localStorage.setItem('fashion_profile_sim', JSON.stringify(simulated))
      } catch (e) {
        // ignore
      }
      this.$q.notify({ type: 'positive', message: 'Login simulado' });
      this.$router.push('/NomenclatorsCard');
    },
    onForgot() {
      this.$q.notify({ type: 'info', message: 'Funcionalidad de recuperación no implementada' })
    },
    onRegister() {
      this.$router.push('/register')
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
  background: rgba(255,255,255,0.08);
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
