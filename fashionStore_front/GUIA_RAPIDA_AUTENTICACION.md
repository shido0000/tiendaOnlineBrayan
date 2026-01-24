# ⚡ GUÍA RÁPIDA - AUTENTICACIÓN Y CONTROL DE ACCESO

## 🎯 Lo que se implementó

| Característica | Detalles |
|---|---|
| 🔐 **Protección de rutas** | Solo acceso con token JWT válido a rutas administrativas |
| 📲 **Redirección automática** | Si no tienes token, vas a /login |
| ✅ **Rutas públicas** | /, /login, /register, /productos, /categorias son libres |
| 🚫 **Acceso denegado** | Nueva página en /access-denied |
| 🔄 **Validación de token** | Se verifica expiración automáticamente |

---

## 📋 Rutas Rápidas

### Públicas (SIN login)
- `/#/` - Inicio
- `/#/login` - Login
- `/#/register` - Registro
- `/#/productos` - Productos
- `/#/categorias` - Categorías
- `/#/carrito` - Carrito

### Protegidas (CON login)
- `/#/Dashboard` - Dashboard
- `/#/Usuario` - Gestión usuarios
- `/#/Producto` - CRUD productos
- `/#/Moneda` - Monedas
- `/#/Contabilidad` - Contabilidad

---

## 🧪 Pruebas Rápidas

### Test 1: Bloqueo sin login ❌
```bash
1. Abre: http://localhost:9000/#/Dashboard
2. ¿Resultado? Redirige a /login ✅
```

### Test 2: Acceso a público sin login ✅
```bash
1. Abre: http://localhost:9000/#/
2. ¿Resultado? Carga inicio ✅
```

### Test 3: Con login ✅
```bash
1. Login con usuario
2. Abre: http://localhost:9000/#/Dashboard
3. ¿Resultado? Carga Dashboard ✅
```

---

## 📁 Archivos Modificados

| Archivo | Cambios |
|---------|---------|
| `src/router/index.js` | + beforeEach() guard |
| `src/router/routes.js` | + meta en todas las rutas |
| `src/AccessDenied.vue` | ✨ Nuevo |

---

## 💻 Usar en Componentes

```vue
<script setup>
import { useAuth } from '@/assets/js/composables/useAuth'

const { isAuthenticated, user, logout } = useAuth()
</script>

<template>
  <div v-if="isAuthenticated">
    <p>Hola {{ user?.nombreCompleto }}</p>
    <button @click="logout">Logout</button>
  </div>
  <div v-else>
    <p>No autenticado</p>
  </div>
</template>
```

---

## 🔍 Validar Token

```javascript
import { isTokenValid, getUserInfo } from '@/assets/js/util/authHelper'

// ¿Token válido?
if (isTokenValid()) {
    console.log('Token OK ✅')
    console.log('Usuario:', getUserInfo())
} else {
    console.log('Token inválido ❌')
}
```

---

## 📊 Resumen de Seguridad

```
┌─ ANTES ────────────────────────┐
│ ❌ Sin protección de rutas     │
│ ❌ Acceso libre a Dashboard    │
│ ❌ Sin validación de tokens    │
│ ❌ Sin página de acceso negado │
└────────────────────────────────┘

┌─ AHORA ────────────────────────┐
│ ✅ Rutas protegidas            │
│ ✅ Validación de autenticación │
│ ✅ Token verificado            │
│ ✅ Acceso denegado con página  │
│ ✅ Redirección automática      │
└────────────────────────────────┘
```

---

## 🚨 Troubleshooting

### Problema: "Redirige a login aunque tengo token"
**Solución**: Verifica que el token no haya expirado
```javascript
import { getTokenExpirationTime } from '@/assets/js/util/authHelper'
console.log('Tiempo restante (min):', getTokenExpirationTime())
```

### Problema: "No puedo acceder a ruta pública"
**Solución**: Verifica que tenga `meta: { public: true }`
```javascript
// En routes.js
{
    path: '/mi-ruta',
    meta: { public: true } // ← Agregar esto
}
```

### Problema: "Logout no funciona"
**Solución**: Usa `useAuth().logout()`
```javascript
import { useAuth } from '@/assets/js/composables/useAuth'
const { logout } = useAuth()
logout() // Limpia token y state
```

---

## 🎯 Checklist Final

- ✅ Rutas públicas accesibles sin token
- ✅ Rutas protegidas requieren token
- ✅ Token se valida automáticamente
- ✅ Redirección a login cuando falta token
- ✅ Página de acceso denegado creada
- ✅ Composable useAuth funcionando
- ✅ Sin errores de compilación

---

**Estado**: 🟢 COMPLETADO
**Seguridad**: 🔐 IMPLEMENTADA
**Listo para**: ✅ PRODUCCIÓN
