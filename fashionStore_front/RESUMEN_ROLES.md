# 📋 Resumen: Sistema de Autenticación y Roles

## 🔓 Tu Token Decodificado

```json
{
  "unique_name": "x",
  "jti": "2a973c79-e769-4b86-b446-9b49a7da1357f",
  "Id": "51dae57d-d095-4b13-70eb-a8de58ff651f",
  "NombreCompleto": "x x",
  "Telefono": "53893887",
  "Correo": "x@xx.xx",
  "exp": 1769072193,
  "iss": "webBrayanAPI",
  "aud": "webBrayanClient"
}
```

### ⚠️ Problema Identificado
**NO hay campo `Rol` en tu token**. El backend debe agregarlo.

---

## 📁 Archivos Creados

### 1. **src/assets/js/util/authHelper.js** ✅
Funciones para decodificar token y verificar roles.

**Funciones principales:**
```javascript
decodeToken()           // Decodifica el JWT
getUserInfo()           // Obtiene datos del usuario
getUserRole()           // Obtiene el rol
hasRole(rol)            // Verifica si tiene un rol
isTokenValid()          // Verifica si no expiró
```

---

### 2. **src/assets/js/composables/useAuth.js** ✅
Composable Vue 3 reactivo para usar en componentes.

**Ejemplo de uso:**
```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';

const { user, isAuthenticated, checkRole } = useAuth();
</script>
```

---

### 3. **src/assets/js/directives/vHasRole.js** ✅
Directiva para mostrar/ocultar elementos según el rol.

**Ejemplo de uso:**
```vue
<div v-has-role="'admin'">
  Solo para admins
</div>
```

---

### 4. **src/boot/auth.js** ✅
Boot file que registra la directiva globalmente.

---

### 5. **src/pages/RoleExamplePage.vue** ✅
Página de ejemplo con todos los casos de uso.

---

### 6. **GUIA_ROLES.md** 📖
Documentación completa con ejemplos y best practices.

---

## 🚀 Cómo Usar

### Opción 1: Directiva en templates
```vue
<button v-has-role="'admin'">
  Panel de administración
</button>
```

### Opción 2: Composable en script
```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth';
const { checkRole } = useAuth();

if (checkRole('admin')) {
  // Mostrar algo
}
</script>
```

### Opción 3: Funciones directas
```javascript
import { hasRole } from '@/assets/js/util/authHelper';

if (hasRole('admin')) {
  // Hacer algo
}
```

---

## 📝 Próximos Pasos

1. **Backend:** Agregar `Rol` o `Roles` al JWT
2. **Login:** Guardar el token en sessionStorage/localStorage
3. **Componentes:** Usar `v-has-role` en templates o `useAuth()` en scripts
4. **Router:** Implementar guards si es necesario

---

## 🧪 Probar el Sistema

1. Abre la consola del navegador (F12)
2. Guarda tu token en sessionStorage:
```javascript
sessionStorage.setItem('token', 'tu_token_aqui');
```

3. Importa las funciones:
```javascript
import { getUserInfo, decodeToken } from 'src/assets/js/util/authHelper';
console.log(decodeToken(sessionStorage.getItem('token')));
```

---

## 🔐 Flujo Recomendado

```
1. Usuario hace login
   ↓
2. Backend retorna JWT con rol incluido
   ↓
3. Frontend guarda token en sessionStorage
   ↓
4. useAuth() carga automáticamente los datos
   ↓
5. v-has-role muestra/oculta elementos
   ↓
6. Router guards previenen acceso no autorizado
```

---

## 📚 Archivos de Referencia

| Archivo | Propósito |
|---------|-----------|
| `authHelper.js` | Funciones principales |
| `useAuth.js` | Composable Vue 3 |
| `vHasRole.js` | Directiva Vue |
| `auth.js` (boot) | Registro global |
| `RoleExamplePage.vue` | Ejemplos de uso |
| `GUIA_ROLES.md` | Documentación |

---

## ✅ Checklist

- [x] Crear funciones de decodificación
- [x] Crear composable Vue 3
- [x] Crear directiva para templates
- [x] Registrar directiva globalmente
- [x] Crear ejemplos de uso
- [x] Documentación completa
- [ ] **Backend:** Agregar rol al JWT
- [ ] **Integración:** Usar en componentes reales
- [ ] **Testing:** Verificar con diferentes roles

---

## 💡 Tips

1. **Para múltiples roles:** Usa `hasAnyRole(['admin', 'vendedor'])`
2. **Para todos los roles:** Usa `hasAllRoles(['admin', 'editor'])`
3. **En templates:** Siempre usa `v-has-role` (mejor rendimiento)
4. **En scripts:** Usa `useAuth()` o funciones de `authHelper.js`
5. **Guards en router:** Implementa antes de que el usuario acceda a rutas protegidas

---

## 🔗 Rutas Útiles

Para ver un ejemplo funcional:
- Navega a `/role-example` (si añades la ruta)

---

Generated: 2026-01-22
