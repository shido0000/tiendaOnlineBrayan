<template>
  <div>
    <!-- Ejemplo de integración de roles en TopBar -->

    <!-- Menú solo para admin -->
    <q-item
      v-has-role="'admin'"
      clickable
      class="menu-item"
      @click="goToAdmin"
    >
      <q-item-section avatar>
        <q-icon name="admin_panel_settings" />
      </q-item-section>
      <q-item-section>
        <q-item-label>Panel de Administración</q-item-label>
      </q-item-section>
    </q-item>

    <!-- Menú para gerente o admin -->
    <q-item
      v-has-role:any="['gerente', 'admin']"
      clickable
      class="menu-item"
      @click="goToReports"
    >
      <q-item-section avatar>
        <q-icon name="bar_chart" />
      </q-item-section>
      <q-item-section>
        <q-item-label>Reportes</q-item-label>
      </q-item-section>
    </q-item>

    <!-- Menú para editor o admin -->
    <q-item
      v-has-role:any="['editor', 'admin']"
      clickable
      class="menu-item"
      @click="goToProductos"
    >
      <q-item-section avatar>
        <q-icon name="inventory_2" />
      </q-item-section>
      <q-item-section>
        <q-item-label>Gestionar Productos</q-item-label>
      </q-item-section>
    </q-item>

    <!-- Divider solo si el usuario tiene algún rol administrativo -->
    <q-separator v-if="hasAdminRoles" class="q-my-md" />

    <!-- Información del usuario actual -->
    <q-item clickable @click="showUserInfo = !showUserInfo">
      <q-item-section avatar>
        <q-avatar icon="person" />
      </q-item-section>
      <q-item-section>
        <q-item-label>{{ user?.nombreCompleto || 'Usuario' }}</q-item-label>
        <q-item-label caption v-if="user?.rol">
          Rol: {{ user.rol }}
        </q-item-label>
      </q-item-section>
      <q-item-section side>
        <q-icon name="info" />
      </q-item-section>
    </q-item>

    <!-- Expandible con información adicional -->
    <q-item v-show="showUserInfo">
      <q-item-section>
        <div class="text-caption q-gutter-xs">
          <div><strong>Email:</strong> {{ user?.correo }}</div>
          <div><strong>Teléfono:</strong> {{ user?.telefono }}</div>
          <div v-if="tokenValid" class="text-green">
            ✓ Token válido
          </div>
          <div v-if="timeUntilExpiration" class="text-orange">
            Expira en {{ timeUntilExpiration }} min
          </div>
        </div>
      </q-item-section>
    </q-item>

    <q-separator class="q-my-md" />

    <!-- Logout -->
    <q-item clickable @click="logout">
      <q-item-section avatar>
        <q-icon name="logout" />
      </q-item-section>
      <q-item-section>
        <q-item-label>Cerrar Sesión</q-item-label>
      </q-item-section>
    </q-item>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useAuth } from '@/assets/js/composables/useAuth';

const router = useRouter();
const showUserInfo = ref(false);

const {
  user,
  isAuthenticated,
  currentRole,
  tokenValid,
  timeUntilExpiration,
  checkAnyRole,
  logout: logoutAuth
} = useAuth();

// Verificar si el usuario tiene roles administrativos
const hasAdminRoles = computed(() => {
  return checkAnyRole(['admin', 'gerente', 'editor']);
});

const goToAdmin = () => {
  router.push({ name: 'AdminPage' }); // Ajusta el nombre de la ruta
};

const goToReports = () => {
  router.push({ name: 'ReportsPage' }); // Ajusta el nombre de la ruta
};

const goToProductos = () => {
  router.push({ name: 'ProductosPage' }); // Ajusta el nombre de la ruta
};

const logout = () => {
  logoutAuth();
  router.push({ name: 'LoginPage' });
};
</script>

<style scoped>
.menu-item {
  border-left: 3px solid transparent;
  transition: all 0.3s ease;
}

.menu-item:hover {
  border-left-color: #1976d2;
  background-color: rgba(25, 118, 210, 0.1);
}
</style>
