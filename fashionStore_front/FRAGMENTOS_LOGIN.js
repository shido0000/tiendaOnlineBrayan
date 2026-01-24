/**
 * FRAGMENTO DE CÓDIGO PARA GUARDAR TOKEN EN LOGIN
 *
 * Copia y adapta esto en tu archivo LoginPage.vue
 */

// ============================================================================
// OPCIÓN 1: Guardar en sessionStorage (RECOMENDADO - Más seguro)
// ============================================================================

/*async function onLogin() {
    try {
        const response = await api.post('/auth/login', {
            username: username.value,
            password: password.value
        });

        if (response.data?.token) {
            // ✅ Guardar en sessionStorage
            sessionStorage.setItem('token', response.data.token);

            // ✅ Recargar información del usuario en useAuth()
            const { loadUser } = useAuth();
            loadUser();

            // ✅ Redirigir a home
            router.push({ name: 'IndexPage' });

            // ✅ Mostrar mensaje de éxito
            Notify.create({
                type: 'positive',
                message: 'Login exitoso',
                position: 'top'
            });
        } else {
            throw new Error('No se recibió token');
        }
    } catch (error) {
        console.error('Error en login:', error);
        Notify.create({
            type: 'negative',
            message: 'Error en el login: ' + (error.message || 'Intenta de nuevo'),
            position: 'top'
        });
    }
}*/

// ============================================================================
// OPCIÓN 2: Guardar en localStorage (Menos seguro pero persiste)
// ============================================================================

/*async function onLogin() {
    try {
        const response = await api.post('/auth/login', {
            username: username.value,
            password: password.value
        });

        if (response.data?.token) {
            // Guardar en localStorage (persiste entre recargas)
            localStorage.setItem('token', response.data.token);

            // Recargar información del usuario
            const { loadUser } = useAuth();
            loadUser();

            // Redirigir
            router.push({ name: 'IndexPage' });
        }
    } catch (error) {
        console.error('Error en login:', error);
    }
}*/

// ============================================================================
// OPCIÓN 3: Guardar en ambos (sessionStorage + localStorage con "Recuérdame")
// ============================================================================

async function onLogin() {
    try {
        const response = await api.post('/auth/login', {
            username: username.value,
            password: password.value
        });

        if (response.data?.token) {
            // Guardar en sessionStorage (siempre)
            sessionStorage.setItem('token', response.data.token);

            // Guardar en localStorage si "Recuérdame" está marcado
            if (remember.value) {
                localStorage.setItem('token', response.data.token);
            }

            // Recargar información del usuario
            const { loadUser } = useAuth();
            loadUser();

            // Redirigir
            router.push({ name: 'IndexPage' });
        }
    } catch (error) {
        console.error('Error en login:', error);
    }
}

// ============================================================================
// LOGOUT: Limpiar token
// ============================================================================

function onLogout() {
    // Opción A: Usar la función del composable
    const { logout } = useAuth();
    logout(); // Esto limpia todo automáticamente

    // Redirigir al login
    router.push({ name: 'LoginPage' });
}

// Opción B: Manual
function onLogoutManual() {
    sessionStorage.removeItem('token');
    localStorage.removeItem('token');
    router.push({ name: 'LoginPage' });
}

// ============================================================================
// VERIFICAR TOKEN AL INICIAR (En App.vue o un Guard)
// ============================================================================

import { useAuth } from '@/assets/js/composables/useAuth';

export default {
    setup() {
        const { loadUser, isAuthenticated } = useAuth();

        // Al iniciar, cargar información del usuario
        onMounted(() => {
            loadUser();

            if (!isAuthenticated.value) {
                // No hay token válido, redirigir a login
                router.push({ name: 'LoginPage' });
            }
        });
    }
};

// ============================================================================
// EN BOOT/AXIOS.JS: Agregar interceptor para token expirado
// ============================================================================


api.interceptors.response.use(
    response => response,
    error => {
        if (error.response?.status === 401) {
            // Token expirado o inválido
            const { logout } = useAuth();
            logout();

            // Redirigir a login
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

// ============================================================================
// IMPLEMENTACIÓN COMPLETA EN LOGINPAGE.VUE
// ============================================================================

/*

<template>
  <div class="login-container">
    <q-form @submit.prevent="onLogin">
      <q-input
        v-model="username"
        label="Usuario"
        type="text"
        outlined
        dense
      />

      <q-input
        v-model="password"
        label="Contraseña"
        type="password"
        outlined
        dense
      />

      <q-checkbox v-model="remember" label="Recuérdame" />

      <q-btn
        type="submit"
        label="LOGIN"
        color="primary"
        class="full-width"
        :loading="loading"
      />
    </q-form>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuth } from '@/assets/js/composables/useAuth';
import { useQuasar } from 'quasar';
import { api } from 'src/boot/axios';

const router = useRouter();
const { loadUser } = useAuth();
const $q = useQuasar();

const username = ref('');
const password = ref('');
const remember = ref(false);
const loading = ref(false);

async function onLogin() {
  loading.value = true;
  try {
    const response = await api.post('/auth/login', {
      username: username.value,
      password: password.value
    });

    if (response.data?.token) {
      // Guardar token
      sessionStorage.setItem('token', response.data.token);

      if (remember.value) {
        localStorage.setItem('token', response.data.token);
      }

      // Recargar usuario
      loadUser();

      // Notificar
      $q.notify({
        type: 'positive',
        message: 'Login exitoso',
        position: 'top'
      });

      // Redirigir
      router.push({ name: 'IndexPage' });
    }
  } catch (error) {
    $q.notify({
      type: 'negative',
      message: 'Error en el login',
      position: 'top'
    });
  } finally {
    loading.value = false;
  }
}
</script>

*/

// ============================================================================
// RESUMEN
// ============================================================================

/*
PASOS CLAVE:

1. En onLogin():
   sessionStorage.setItem('token', response.data.token);

2. Después de guardar:
   const { loadUser } = useAuth();
   loadUser();

3. En logout():
   const { logout } = useAuth();
   logout();

4. En interceptores de Axios:
   Si error 401 → logout automático → redirigir a login

5. En guards del router:
   Verificar isAuthenticated antes de acceder a rutas
*/
