# 🎯 Guía de Implementación Paso a Paso

## 1️⃣ Configuración Backend (Muy Importante)

Tu backend debe retornar el JWT con el campo `Rol`. Ejemplo:

```csharp
// En tu controlador de login (C# / .NET)
var token = new JwtSecurityToken(
    issuer: "webBrayanAPI",
    audience: "webBrayanClient",
    claims: new List<Claim> {
        new Claim("unique_name", usuario.Username),
        new Claim("Id", usuario.Id),
        new Claim("NombreCompleto", usuario.NombreCompleto),
        new Claim("Telefono", usuario.Telefono),
        new Claim("Correo", usuario.Correo),
        new Claim("Rol", usuario.Rol), // ← AGREGAR ESTA LÍNEA
        // O para múltiples roles:
        // new Claim(ClaimTypes.Role, "admin"),
        // new Claim(ClaimTypes.Role, "editor"),
    },
    expires: DateTime.UtcNow.AddHours(48),
    signingCredentials: new SigningCredentials(...)
);
```

---

## 2️⃣ Configuración Frontend

### Paso 1: Verificar que el archivo `boot/auth.js` existe

✅ Debe existir en: `src/boot/auth.js`

**Contenido:**
```javascript
import { boot } from 'quasar/wrappers'
import { vHasRole } from '@/assets/js/directives/vHasRole'

export default boot(({ app }) => {
  app.directive('v-has-role', vHasRole)
})
```

### Paso 2: Registrar el boot en `quasar.config.js`

Abre tu `quasar.config.js` y verifica que `auth.js` está en la lista de boots:

```javascript
boot: [
  'axios',
  'i18n',
  'lodash',
  'notify',
  'signalR',
  'auth'  // ← AGREGAR ESTA LÍNEA si no está
]
```

### Paso 3: En LoginPage, guardar el token

Abre `src/pages/LoginPage.vue` y asegúrate de que al hacer login, se guarde el token:

```javascript
// En tu función onLogin()
const response = await api.post('/auth/login', {
  username: username.value,
  password: password.value
});

if (response.data.token) {
  // Guardar en sessionStorage (más seguro) o localStorage
  sessionStorage.setItem('token', response.data.token);
  // O
  localStorage.setItem('token', response.data.token);

  // Redirigir a home
  router.push({ name: 'IndexPage' });
}
```

---

## 3️⃣ Usando el Sistema en Componentes

### Opción A: En Templates (Recomendado)

```vue
<template>
  <!-- Mostrar solo si tiene el rol 'admin' -->
  <q-btn
    v-has-role="'admin'"
    label="Panel de Admin"
    color="negative"
  />

  <!-- Mostrar si tiene alguno de estos roles -->
  <q-item v-has-role="['editor', 'admin']">
    Gestionar Contenido
  </q-item>

  <!-- Mostrar si tiene TODOS estos roles -->
  <q-btn v-has-role:all="['admin', 'auditor']">
    Revisar Auditoría
  </q-btn>
</template>
```

### Opción B: En Script Setup (Vue 3)

```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const { user, isAuthenticated, checkRole, checkAnyRole } = useAuth();

const canEdit = () => {
  return checkAnyRole(['editor', 'admin']);
};

const isAdmin = () => {
  return checkRole('admin');
};
</script>

<template>
  <div v-if="isAuthenticated">
    <h1>Hola {{ user?.nombreCompleto }}</h1>

    <q-btn
      v-if="isAdmin()"
      label="Administración"
      to="/admin"
    />

    <q-btn
      v-if="canEdit()"
      label="Editar"
      @click="editItem"
    />
  </div>
</template>
```

### Opción C: Funciones Directas

```vue
<script setup>
import { hasRole, getUserInfo } from '@/assets/js/util/authHelper';

const userInfo = getUserInfo();

if (hasRole('admin')) {
  console.log('Es administrador');
}

if (userInfo?.nombreCompleto) {
  console.log('Nombre:', userInfo.nombreCompleto);
}
</script>
```

---

## 4️⃣ Guards en el Router

Abre `src/router/index.js` y agrega guards:

```javascript
import { useAuth } from '@/assets/js/composables/useAuth';

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes: [
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
  ]
});

router.beforeEach((to, from, next) => {
  const { checkRole, checkAnyRole, isAuthenticated } = useAuth();

  if (to.meta.requiresRole) {
    if (!isAuthenticated.value) {
      next('/login');
      return;
    }

    const roles = Array.isArray(to.meta.requiresRole)
      ? to.meta.requiresRole
      : [to.meta.requiresRole];

    const hasAccess = Array.isArray(to.meta.requiresRole)
      ? checkAnyRole(roles)
      : checkRole(roles);

    if (hasAccess) {
      next();
    } else {
      next('/'); // O a una página de acceso denegado
    }
  } else {
    next();
  }
});

export default router;
```

---

## 5️⃣ Integración en Componentes Existentes

### Ejemplo: TopBar.vue

En el menú mobile, agrega:

```vue
<!-- En tu q-list del menú mobile -->

<!-- Menú Admin (solo para admins) -->
<q-item
  v-has-role="'admin'"
  clickable
  @click="router.push('/admin')"
>
  <q-item-section avatar>
    <q-icon name="admin_panel_settings" />
  </q-item-section>
  <q-item-section>
    <q-item-label>Panel Admin</q-item-label>
  </q-item-section>
</q-item>

<!-- Menú Productos (para editor o admin) -->
<q-item
  v-has-role="['editor', 'admin']"
  clickable
  @click="router.push('/productos')"
>
  <q-item-section avatar>
    <q-icon name="inventory_2" />
  </q-item-section>
  <q-item-section>
    <q-item-label>Gestionar Productos</q-item-label>
  </q-item-section>
</q-item>

<!-- Divider solo si tiene roles de admin -->
<q-separator v-if="checkAnyRole(['admin', 'editor'])" />
```

Y en el script setup:

```javascript
import { useAuth } from '@/assets/js/composables/useAuth';

const { checkAnyRole } = useAuth();
```

---

## 6️⃣ Ejemplos de Integración en Tus Páginas

### En Nomenclators/Gestor.vue (ejemplo)

```vue
<q-btn
  v-has-role="'admin'"
  label="Eliminar Nomenclador"
  color="negative"
  @click="delete"
/>

<q-btn
  v-has-role="['editor', 'admin']"
  label="Editar"
  color="primary"
  @click="edit"
/>
```

### En ProductosPage.vue (ejemplo)

```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const { checkAnyRole } = useAuth();

const canCreateProduct = () => {
  return checkAnyRole(['admin', 'editor']);
};
</script>

<template>
  <div v-if="canCreateProduct()" class="q-mb-md">
    <q-btn
      label="Crear Producto"
      color="positive"
      icon="add"
      to="/nomenclators/producto/new"
    />
  </div>
</template>
```

---

## 7️⃣ Pruebas

### Prueba 1: Verificar que el token se guarda

1. Abre DevTools (F12)
2. Ve a Applications → Local Storage o Session Storage
3. Verifica que existe la clave `token`

### Prueba 2: Decodificar el token

En la consola, ejecuta:

```javascript
const token = sessionStorage.getItem('token');
const parts = token.split('.');
const payload = JSON.parse(atob(parts[1]));
console.log(payload);
```

### Prueba 3: Verificar que el rol está presente

```javascript
import { getUserInfo } from 'src/assets/js/util/authHelper';
const user = getUserInfo();
console.log('Rol:', user.rol);
```

### Prueba 4: Probar la directiva

En el template, agrega:

```vue
<div v-has-role="'admin'">
  Solo visible para admins
</div>
```

Debes ver este div solo si tienes el rol 'admin'.

---

## 8️⃣ Troubleshooting

| Problema | Solución |
|----------|----------|
| `v-has-role` no funciona | Verifica que `src/boot/auth.js` existe y está registrado en `quasar.config.js` |
| El rol es undefined | Verifica que el backend lo incluye en el JWT |
| Token expirado no se detecta | Asegúrate de que `exp` está en segundos (no milisegundos) |
| `useAuth()` retorna null | Asegúrate de que el token está guardado en sessionStorage/localStorage |
| Directiva no se actualiza | Usa `:key` en el elemento o llama a `loadUser()` manualmente |

---

## 9️⃣ Mejores Prácticas

✅ **Hacer:**
- Guardar el token en `sessionStorage` (más seguro que `localStorage`)
- Usar `v-has-role` en templates (mejor rendimiento)
- Implementar guards en rutas protegidas
- Mostrar mensaje amigable cuando acceso denegado
- Renovar token antes de expirar

❌ **No Hacer:**
- Guardar rol en cookies sin HTTPS
- Confiar solo en verificación frontend (siempre verificar en backend)
- Usar rol para operaciones sensibles sin validar en servidor
- Guardar datos sensibles en localStorage sin encriptar

---

## 🔟 Checklist Final

- [ ] Backend retorna `Rol` en el JWT
- [ ] `src/boot/auth.js` existe
- [ ] `auth.js` está registrado en `quasar.config.js`
- [ ] Token se guarda en sessionStorage en el login
- [ ] Puedes decodificar y ver el rol en la consola
- [ ] `v-has-role` funciona en templates
- [ ] `useAuth()` funciona en components
- [ ] Guards en router funcionan
- [ ] Mensajes de error cuando acceso denegado
- [ ] Tests con diferentes roles

---

## 📞 Soporte

Si algo no funciona:

1. Revisa la consola del navegador (F12)
2. Abre la pestaña Network y busca el request de login
3. Verifica que el JWT contiene el campo `Rol`
4. Ejecuta los tests en la consola

---

¡Listo! Tu sistema de roles está completamente configurado. 🎉
