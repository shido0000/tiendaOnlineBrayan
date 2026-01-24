<template>
  <div class="q-pa-md">
    <h1>Ejemplo de uso de Roles</h1>

    <!-- Mostrar información del usuario actual -->
    <q-card class="q-mb-md">
      <q-card-section>
        <h2>Información del Usuario</h2>
        <p v-if="user"><strong>Nombre:</strong> {{ user.nombreCompleto }}</p>
        <p v-if="user"><strong>Email:</strong> {{ user.correo }}</p>
        <p v-if="user"><strong>Rol actual:</strong> {{ user.rol || 'Sin rol asignado' }}</p>
        <p v-if="user && user.roles?.length"><strong>Roles:</strong> {{ user.roles.join(', ') }}</p>
        <p v-if="tokenValid"><strong>Token válido:</strong> Sí</p>
        <p v-if="tokenValid && timeUntilExpiration"><strong>Expira en:</strong> {{ timeUntilExpiration }} minutos</p>
      </q-card-section>
    </q-card>

    <!-- Ejemplo 1: Mostrar solo para usuarios con rol 'admin' -->
    <q-card class="q-mb-md" v-has-role="'admin'">
      <q-card-section>
        <h3>Panel de Administrador</h3>
        <p>Este contenido solo lo ven los administradores</p>
        <q-btn label="Gestionar usuarios" color="primary" />
      </q-card-section>
    </q-card>

    <!-- Ejemplo 2: Mostrar para roles de gerente o admin -->
    <q-card class="q-mb-md" v-has-role:any="['gerente', 'admin']">
      <q-card-section>
        <h3>Panel de Gestión</h3>
        <p>Este contenido lo ven gerentes y administradores</p>
        <q-btn label="Ver reportes" color="info" />
      </q-card-section>
    </q-card>

    <!-- Ejemplo 3: Mostrar solo si el usuario tiene TODOS estos roles -->
    <q-card class="q-mb-md" v-has-role:all="['admin', 'editor']">
      <q-card-section>
        <h3>Panel de Editor Administrador</h3>
        <p>Este contenido solo lo ven los que son admin Y editor</p>
      </q-card-section>
    </q-card>

    <!-- Ejemplo 4: Usar métodos en el script para decisiones programáticas -->
    <q-card class="q-mb-md">
      <q-card-section>
        <h3>Control dinámico de permisos</h3>

        <q-btn
          v-if="checkRole('admin')"
          label="Opción solo para admins"
          color="negative"
          class="q-mr-md"
        />

        <q-btn
          v-if="checkAnyRole(['vendedor', 'admin'])"
          label="Opción para vendedores o admins"
          color="warning"
        />
      </q-card-section>
    </q-card>

    <!-- Ejemplo 5: Lista de acciones permitidas basada en rol -->
    <q-card class="q-mb-md">
      <q-card-section>
        <h3>Acciones permitidas:</h3>
        <ul>
          <li v-if="checkRole('admin')">✓ Gestionar usuarios</li>
          <li v-if="checkAnyRole(['vendedor', 'admin'])">✓ Crear pedidos</li>
          <li v-if="checkAnyRole(['editor', 'admin'])">✓ Editar productos</li>
          <li v-if="isAuthenticated">✓ Ver mi perfil</li>
        </ul>
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const {
  user,
  isAuthenticated,
  currentRole,
  currentRoles,
  tokenValid,
  timeUntilExpiration,
  checkRole,
  checkAnyRole,
  checkAllRoles,
  logout
} = useAuth();
</script>

<style scoped>
ul {
  list-style: none;
  padding: 0;
}

ul li {
  padding: 8px;
  margin: 5px 0;
  background: #f5f5f5;
  border-left: 4px solid #1976d2;
}
</style>
