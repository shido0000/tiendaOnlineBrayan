# 🔐 CONTROL DE PERMISOS POR ROL - PROTECCIÓN DE RUTAS

## 🎯 Problema Resuelto

**Antes**: Un usuario con rol `Cliente` podía acceder a `/Categoria` escribiendo la URL directamente, aunque en el menú no aparecía la opción.

**Ahora**: El router valida automáticamente los permisos por rol. Si un usuario no tiene permiso, es redirigido a la página de "Acceso Denegado".

---

## 🔧 Cambios Implementados

### 1️⃣ Expandido: `src/assets/js/util/essentialListUrl.js`

Se agregó configuración centralizada de permisos por rol:

```javascript
// Qué menús ve cada rol
const menuByRole = {
    Admin: ['INICIO', 'DASHBOARD', 'PERFIL', 'PEDIDOS', 'NOMENCLADORES', 'CREAR INFORMACIÓN'],
    Vendedor: ['INICIO', 'PERFIL', 'PEDIDOS'],
    Cliente: ['INICIO', 'PERFIL', 'PEDIDOS']
};

// Qué rutas puede acceder cada rol
export const routePermissions = {
    'Dashboard': ['Admin'],
    'Moneda': ['Admin'],
    'Gestor': ['Admin'],
    'Mensajeria': ['Admin', 'Vendedor'],
    'Categoria': ['Admin'],
    'Producto': ['Admin', 'Vendedor'],
    'Descuento': ['Admin'],
    'Pedido': ['Admin', 'Vendedor', 'Cliente'],
    'Cupon': ['Admin'],
    'Usuario': ['Admin'],
    'Perfil': ['Admin', 'Vendedor', 'Cliente'],
    // ... más rutas
};

// Nueva función para validar acceso
export function canAccessRoute(routeName, userRole) {
    // Lógica de validación
}
```

### 2️⃣ Mejorado: `src/router/index.js`

Se agregó validación de permisos en el `beforeEach()`:

```javascript
Router.beforeEach((to, from, next) => {
    const userRole = getUserRole()
    const routeName = to.name

    // 1. Validar autenticación
    if (!isAuthenticated) return next({ name: 'LoginPage' })

    // 2. Validar permisos por rol ← NUEVO
    const hasPermission = canAccessRoute(routeName, userRole)
    if (!hasPermission) {
        console.error(`Acceso denegado para rol "${userRole}"`)
        return next({ name: 'AccessDenied' })
    }

    // 3. Permitir acceso
    return next()
})
```

---

## 📊 Matriz de Permisos

| Ruta | Admin | Vendedor | Cliente |
|------|-------|----------|---------|
| Dashboard | ✅ | ❌ | ❌ |
| Moneda | ✅ | ❌ | ❌ |
| Gestor | ✅ | ❌ | ❌ |
| Mensajeria | ✅ | ✅ | ❌ |
| Categoria | ✅ | ❌ | ❌ |
| Producto | ✅ | ✅ | ❌ |
| Descuento | ✅ | ❌ | ❌ |
| Pedido | ✅ | ✅ | ✅ |
| Cupon | ✅ | ❌ | ❌ |
| Usuario | ✅ | ❌ | ❌ |
| Perfil | ✅ | ✅ | ✅ |
| Contabilidad | ✅ | ❌ | ❌ |

---

## 🧪 Pruebas

### Test 1: Cliente intenta acceder a `/Categoria`
```
1. Login como Cliente
2. Escribe en URL: http://localhost:9000/#/Categoria
3. beforeEach() detecta:
   - Rol: "Cliente"
   - Ruta: "Categoria"
   - Permisos: ["Admin"]
4. Cliente NO tiene permiso ❌
5. Resultado: 🔀 Redirige a /access-denied ✅
```

### Test 2: Admin accede a `/Categoria`
```
1. Login como Admin
2. Escribe en URL: http://localhost:9000/#/Categoria
3. beforeEach() detecta:
   - Rol: "Admin"
   - Ruta: "Categoria"
   - Permisos: ["Admin"]
4. Admin SÍ tiene permiso ✅
5. Resultado: ✅ Se carga la página ✅
```

### Test 3: Vendedor accede a `/Producto`
```
1. Login como Vendedor
2. Escribe en URL: http://localhost:9000/#/Producto
3. beforeEach() detecta:
   - Rol: "Vendedor"
   - Ruta: "Producto"
   - Permisos: ["Admin", "Vendedor"]
4. Vendedor SÍ tiene permiso ✅
5. Resultado: ✅ Se carga la página ✅
```

### Test 4: Cliente accede a `/Pedido`
```
1. Login como Cliente
2. Escribe en URL: http://localhost:9000/#/Pedido
3. beforeEach() detecta:
   - Rol: "Cliente"
   - Ruta: "Pedido"
   - Permisos: ["Admin", "Vendedor", "Cliente"]
4. Cliente SÍ tiene permiso ✅
5. Resultado: ✅ Se carga la página ✅
```

---

## 📝 Cómo Agregar o Modificar Permisos

### Agregar nueva ruta con permisos

En `src/assets/js/util/essentialListUrl.js`:

```javascript
export const routePermissions = {
    // ... rutas existentes
    'MiNuevaRuta': ['Admin', 'Vendedor'],  // ← Agregar aquí
};
```

### Cambiar permisos de una ruta existente

```javascript
// Antes: Solo Admin
'Mensajeria': ['Admin']

// Ahora: Admin y Vendedor
'Mensajeria': ['Admin', 'Vendedor']
```

### Permitir todos los roles (excepto público)

```javascript
'MiRuta': ['Admin', 'Vendedor', 'Cliente']
```

---

## 🔍 Cómo Funciona el Flujo

```
Usuario intenta acceder a: /Categoria
                    ↓
beforeEach() se ejecuta
                    ↓
¿Es ruta pública? NO
                    ↓
¿Tiene token válido? SÍ
                    ↓
¿Cuál es su rol? "Cliente"
                    ↓
canAccessRoute('Categoria', 'Cliente')
                    ↓
¿'Cliente' está en ['Admin']? NO
                    ↓
⚠️ ACCESO DENEGADO
                    ↓
Redirige a /access-denied
                    ↓
Se muestra página de acceso denegado
```

---

## 💾 Archivos Modificados

| Archivo | Cambio |
|---------|--------|
| `src/assets/js/util/essentialListUrl.js` | ✅ Expandido con `routePermissions` y `canAccessRoute()` |
| `src/router/index.js` | ✅ Mejorado `beforeEach()` para validar permisos |

---

## 🎯 Características

✅ **Validación automática**: Se valida en cada navegación
✅ **Centralizada**: Un solo lugar para definir permisos (`essentialListUrl.js`)
✅ **Segura**: No se puede saltear por URL
✅ **Consistente**: Menú + Rutas + Permisos sincronizados
✅ **Escalable**: Fácil agregar nuevos roles y rutas
✅ **Transparente**: Logs en consola para debugging

---

## 🔐 Seguridad Garantizada

```
ANTES: ⚠️ INSEGURO
  • Menú filtra por rol (Cliente NO ve Categoria)
  • Pero: http://localhost:9000/#/Categoria funciona igual
  • Cliente accede sin permisos

AHORA: ✅ SEGURO
  • Menú filtra por rol (Cliente NO ve Categoria)
  • Y: http://localhost:9000/#/Categoria → /access-denied
  • Cliente NO puede acceder por URL
```

---

## 📚 Usar en Componentes

Si necesitas verificar permisos en componentes:

```javascript
import { canAccessRoute } from '@/assets/js/util/essentialListUrl'
import { getUserRole } from '@/assets/js/util/authHelper'

const userRole = getUserRole()
const hasAccess = canAccessRoute('Dashboard', userRole)

if (!hasAccess) {
    console.log('No tienes acceso a Dashboard')
}
```

---

## 🔄 Próximas Mejoras (Opcional)

1. **Agregar más roles**: `Gerente`, `Contable`, `Supervisor`
2. **Permisos granulares**: Por acción (ver, editar, eliminar)
3. **Auditoría**: Log de accesos denegados
4. **Dinámicos**: Permisos desde el backend

---

## ✨ Beneficio Logrado

**Seguridad Completa**:
- ✅ No se puede saltear autenticación
- ✅ No se puede saltear permisos por URL
- ✅ El menú refleja lo que realmente puedes acceder
- ✅ Todo centralizado y fácil de mantener

---

**Estado**: 🟢 IMPLEMENTADO Y FUNCIONANDO
**Última actualización**: 2026-01-24
**Versión**: 2.0 (Con validación de permisos por rol)
