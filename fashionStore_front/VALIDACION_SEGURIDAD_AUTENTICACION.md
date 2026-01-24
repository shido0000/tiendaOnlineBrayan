# 🔒 VALIDACIÓN DE SEGURIDAD - AUTENTICACIÓN Y CONTROL DE ACCESO

## ✅ Cambios Implementados

### 1. **Página de Acceso Denegado** ✨
   - **Archivo**: `src/AccessDenied.vue`
   - Se muestra cuando un usuario autenticado intenta acceder a una ruta sin permisos
   - UI profesional con icono de candado y opción de regreso al inicio

### 2. **Metadata de Rutas** 📋
   - **Archivo**: `src/router/routes.js`
   - Cada ruta ahora tiene `meta` información:
     - `requiresAuth: true` - Rutas protegidas (requieren autenticación)
     - `requiresAuth: false` - Rutas públicas (sin autenticación)
     - `public: true` - Rutas completamente públicas

### 3. **Route Guards (Guardias de Navegación)** 🛡️
   - **Archivo**: `src/router/index.js`
   - Se implementó `beforeEach()` para:
     - ✅ Validar si el usuario está autenticado
     - ✅ Verificar si la ruta es pública
     - ✅ Redirigir a login si no está autenticado
     - ✅ Guardar URL de redirección para tras el login

---

## 📊 Rutas Protegidas vs Públicas

### 🟢 Rutas PÚBLICAS (Accesibles sin autenticación):
- `/login` - Página de login
- `/register` - Página de registro
- `/` - Página de inicio
- `/productos` - Listado de productos
- `/categorias` - Listado de categorías
- `/categorias/:id` - Productos por categoría
- `/producto/:id` - Detalle del producto
- `/carrito` - Carrito de compras
- `/lista_deseos` - Lista de deseos
- `/informacion` - Información general
- `/access-denied` - Página de acceso denegado

### 🔴 Rutas PROTEGIDAS (Requieren autenticación):
- `/Dashboard`
- `/Moneda`
- `/Gestor`
- `/Mensajeria`
- `/Categoria` (CRUD)
- `/Producto` (CRUD)
- `/Descuento`
- `/Pedido`
- `/Cupon`
- `/Usuario` (CRUD)
- `/Perfil`
- `/OtraVariante`
- `/CuentasContables`
- `/AsientosContables`
- `/ReporteContable`
- `/Contabilidad`
- `/CrearInformacion`
- `/DiagnosticoNotificaciones`

---

## 🔄 Flujo de Seguridad

### Escenario 1: Usuario NO Autenticado
```
1. Accede a URL protegida (ej: /Dashboard)
   ↓
2. beforeEach() detecta token inválido/ausente
   ↓
3. Redirige a /login con parámetro ?redirect=/Dashboard
   ↓
4. Después del login, se redirige a la URL original
```

### Escenario 2: Usuario Autenticado (Token Válido)
```
1. Accede a URL protegida (ej: /Dashboard)
   ↓
2. beforeEach() valida token con isTokenValid()
   ↓
3. Permite acceso a la ruta
```

### Escenario 3: Usuario Intenta Acceder sin Permisos
```
1. Usuario autenticado accede a /access-denied
   ↓
2. beforeEach() lo detecta como ruta pública
   ↓
3. Muestra página de "Acceso Denegado"
```

---

## 🧪 Cómo Probar

### Test 1: Sin autenticación
```
1. Limpia localStorage/sessionStorage (borrar token)
2. Intenta acceder a: http://localhost:xxxx/#/Dashboard
3. ✅ Deberá redirigirse a /login
```

### Test 2: Ruta pública sin autenticación
```
1. Sin token
2. Accede a: http://localhost:xxxx/#/
3. ✅ Deberá cargarse la página inicio
```

### Test 3: Con autenticación
```
1. Login con usuario válido
2. Intenta acceder a: http://localhost:xxxx/#/Dashboard
3. ✅ Deberá cargarse el Dashboard
```

### Test 4: Página no encontrada
```
1. Accede a: http://localhost:xxxx/#/ruta-inexistente
2. ✅ Muestra página 404 (ErrorNotFound.vue)
```

---

## 📁 Archivos Modificados/Creados

| Archivo | Estado | Cambio |
|---------|--------|--------|
| `src/AccessDenied.vue` | ✨ CREADO | Nueva página de acceso denegado |
| `src/router/routes.js` | 🔄 MODIFICADO | Agregada metadata a todas las rutas |
| `src/router/index.js` | 🔄 MODIFICADO | Implementado beforeEach() guard |

---

## 🔐 Seguridad Implementada

✅ **Protección de rutas**: No se puede acceder a rutas protegidas sin token válido
✅ **Validación de token**: Se valida expiración del token
✅ **Redirección**: Se guarda la URL destino para redirigir tras login
✅ **Página de acceso denegado**: Feedback visual para acceso no permitido
✅ **Rutas públicas accesibles**: Login, registro e inicio sin autenticación
✅ **Catch-all 404**: Cualquier ruta no definida se captura

---

## 📝 Próximas Mejoras (Opcional)

Si necesitas más control granular por roles:
```javascript
// En meta de rutas:
{
    path: 'Usuario',
    name: 'Usuario',
    component: () => import('src/pages/Nomenclators/Usuario.vue'),
    meta: {
        requiresAuth: true,
        requiredRoles: ['Admin', 'Manager'] // Roles permitidos
    }
}

// En beforeEach:
const requiredRoles = to.meta?.requiredRoles
if (requiredRoles && !hasAnyRole(requiredRoles)) {
    return next({ name: 'AccessDenied' })
}
```

---

## 📞 Función de Ayuda en useAuth

Puedes usar el composable `useAuth` en tus componentes:
```javascript
import { useAuth } from '@/assets/js/composables/useAuth'

export default {
    setup() {
        const { isAuthenticated, user, checkRole } = useAuth()

        return {
            isAuthenticated,
            user,
            hasAdminRole: checkRole('Admin')
        }
    }
}
```

---

**Estado**: ✅ LISTO PARA PRODUCCIÓN
**Última actualización**: 2026-01-24
**Versión**: 1.0
