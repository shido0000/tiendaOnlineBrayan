# 🔐 Guía de Autenticación y Roles

## Resumen Rápido

Tu JWT contiene la información del usuario, pero **actualmente NO incluye un campo `rol`**. Necesitas que tu backend lo agregue en el token.

## ¿Qué Hemos Creado?

### 1. **authHelper.js** - Funciones auxiliares
Proporciona funciones para decodificar el token y trabajar con roles.

**Funciones disponibles:**

```javascript
// Decodificar token
decodeToken(token)              // Retorna el payload decodificado

// Obtener información
getToken()                      // Obtiene el token del storage
getUserInfo()                   // Obtiene toda la info del usuario
getUserRole()                   // Obtiene el rol del usuario
getUserRoles()                  // Obtiene array de roles

// Verificar roles
hasRole('admin')                // ¿Tiene el rol 'admin'?
hasAnyRole(['admin', 'user'])   // ¿Tiene alguno de estos roles?
hasAllRoles(['admin', 'editor']) // ¿Tiene TODOS estos roles?

// Validar token
isTokenValid()                  // ¿El token no ha expirado?
getTokenExpirationTime()        // Minutos hasta expiración
getFullUserData()               // Obtiene todos los datos
```

**Uso en JavaScript:**

```javascript
import { hasRole, getUserRole, isTokenValid } from '@/assets/js/util/authHelper';

if (hasRole('admin')) {
  console.log('El usuario es admin');
}

if (isTokenValid()) {
  console.log('Token válido');
}
```

---

### 2. **useAuth.js** - Composable Vue 3

Composable reactivo para usar en componentes Vue 3.

**Propiedades:**
```javascript
const {
  user,                   // Objeto con datos del usuario
  token,                  // El JWT
  isAuthenticated,        // ¿Está autenticado?
  currentRole,            // El rol actual
  currentRoles,           // Array de roles
  tokenValid,             // ¿Token válido?
  timeUntilExpiration,    // Minutos hasta expiración

  // Métodos
  loadUser(),             // Cargar/recargar datos del usuario
  checkRole(role),        // Verificar un rol
  checkAnyRole(roles),    // Verificar si tiene alguno
  checkAllRoles(roles),   // Verificar si tiene todos
  logout()                // Cerrar sesión
} = useAuth();
```

**Uso en componentes:**

```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const { user, isAuthenticated, checkRole } = useAuth();
</script>

<template>
  <div v-if="isAuthenticated">
    <p>Bienvenido {{ user?.nombreCompleto }}</p>
  </div>
</template>
```

---

### 3. **v-has-role** - Directiva Vue

Directiva para mostrar/ocultar elementos basado en roles.

**Sintaxis:**

```vue
<!-- Mostrar solo si tiene el rol 'admin' -->
<div v-has-role="'admin'">
  Panel de administración
</div>

<!-- Mostrar si tiene alguno de estos roles -->
<div v-has-role="['admin', 'gerente']">
  Panel de gestión
</div>

<!-- Mostrar si tiene TODOS estos roles -->
<div v-has-role:all="['admin', 'editor']">
  Solo admins y editors
</div>

<!-- Equivalente a :any (es el por defecto) -->
<div v-has-role:any="['admin', 'vendedor']">
  Admin o vendedor
</div>
```

---

## Ejemplos Completos

### Ejemplo 1: Menú condicional

```vue
<template>
  <q-drawer>
    <q-list>
      <q-item to="/dashboard" v-has-role="'admin'">
        <q-item-section avatar>
          <q-icon name="dashboard" />
        </q-item-section>
        <q-item-section>Panel de Admin</q-item-section>
      </q-item>

      <q-item to="/productos" v-has-role="['editor', 'admin']">
        <q-item-section avatar>
          <q-icon name="inventory" />
        </q-item-section>
        <q-item-section>Gestionar Productos</q-item-section>
      </q-item>

      <q-item to="/carrito">
        <q-item-section avatar>
          <q-icon name="shopping_cart" />
        </q-item-section>
        <q-item-section>Mi Carrito</q-item-section>
      </q-item>
    </q-list>
  </q-drawer>
</template>
```

### Ejemplo 2: Botones condicionales

```vue
<template>
  <div>
    <q-btn
      v-if="checkRole('admin')"
      label="Eliminar usuario"
      color="negative"
      @click="deleteUser"
    />

    <q-btn
      v-if="checkAnyRole(['editor', 'admin'])"
      label="Editar"
      color="primary"
      @click="editItem"
    />
  </div>
</template>

<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const { checkRole, checkAnyRole } = useAuth();

const deleteUser = () => {
  if (!checkRole('admin')) {
    alert('No tienes permiso');
    return;
  }
  // Eliminar usuario...
};
</script>
```

### Ejemplo 3: Control en el script

```vue
<script setup>
import { getUserInfo, hasRole } from '@/assets/js/util/authHelper';

const userInfo = getUserInfo();

if (hasRole('admin')) {
  // Mostrar opciones de admin
} else if (hasRole('vendedor')) {
  // Mostrar opciones de vendedor
} else {
  // Usuario normal
}
</script>
```

---

## ⚠️ **IMPORTANTE: Tu Backend Debe Incluir el Rol**

Actualmente tu token tiene esto:

```json
{
  "unique_name": "x",
  "Id": "51dae57d-d095-4b13-70eb-a8de58ff651f",
  "NombreCompleto": "x x",
  // ... otros campos
}
```

**Debe tener esto:**

```json
{
  "unique_name": "x",
  "Id": "51dae57d-d095-4b13-70eb-a8de58ff651f",
  "NombreCompleto": "x x",
  "Rol": "admin",  // ← AGREGAR ESTO
  "Roles": ["admin", "editor"],  // ← O ESTO para múltiples roles
  // ... otros campos
}
```

---

## Flujo de Trabajo Recomendado

1. **En el Login:** El backend retorna el JWT con los roles incluidos
2. **En el Frontend:** Se guarda en `sessionStorage` o `localStorage`
3. **En el Router Guard:** Verificar si el usuario tiene acceso a la página
4. **En los Componentes:** Usar `v-has-role` o `useAuth()` para mostrar/ocultar contenido

---

## Ejemplo de Guard en el Router

```javascript
// router/index.js o router/routes.js

const routes = [
  {
    path: '/admin',
    component: () => import('pages/AdminPage.vue'),
    meta: { requiresRole: 'admin' }
  },
  {
    path: '/editor',
    component: () => import('pages/EditorPage.vue'),
    meta: { requiresRole: ['editor', 'admin'] }
  }
];

// En router.beforeEach():
router.beforeEach((to, from, next) => {
  const { checkRole, checkAnyRole } = useAuth();

  if (to.meta.requiresRole) {
    const roles = Array.isArray(to.meta.requiresRole)
      ? to.meta.requiresRole
      : [to.meta.requiresRole];

    if (Array.isArray(roles) ? checkAnyRole(roles) : checkRole(roles)) {
      next();
    } else {
      next('/');
    }
  } else {
    next();
  }
});
```

---

## Checklist de Integración

- [ ] Verificar que el backend incluye `Rol` o `Roles` en el JWT
- [ ] Agregar la directiva `auth.js` en el boot (ya está registrada)
- [ ] Importar `useAuth()` en componentes que lo necesiten
- [ ] Reemplazar condicionales manuales con `v-has-role`
- [ ] Probar con diferentes roles en el JWT
- [ ] Implementar guards en el router si es necesario

---

## Decodificación Manual de JWT

Si necesitas verificar el contenido de tu token:

```javascript
function decodeJWT(token) {
  const parts = token.split('.');
  const payload = parts[1];
  return JSON.parse(atob(payload));
}

const token = localStorage.getItem('token') || sessionStorage.getItem('token');
const decoded = decodeJWT(token);
console.log(decoded);
```

---

## Soporte

Para más información sobre JWT: https://jwt.io
Para más información sobre Vue 3 composables: https://vuejs.org/guide/extras/composition-api-faq.html
