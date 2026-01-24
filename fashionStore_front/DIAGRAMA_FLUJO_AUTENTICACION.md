# 📊 DIAGRAMA DE FLUJO - SISTEMA DE AUTENTICACIÓN

## 🔄 Flujo General de Navegación

```
┌─────────────────────────────────────────────────────────────────────┐
│                    NAVEGACIÓN DE USUARIO                            │
└─────────────────────────────────────────────────────────────────────┘
                                 │
                                 ▼
                    ┌────────────────────────┐
                    │  Usuario intenta ir    │
                    │  a una ruta            │
                    └────────────────────────┘
                                 │
                                 ▼
                    ┌────────────────────────┐
                    │  Router.beforeEach()   │
                    │  se activa             │
                    └────────────────────────┘
                                 │
                ┌───────────────┬─────────────┬──────────────┐
                │               │             │              │
                ▼               ▼             ▼              ▼
        ┌──────────────┐ ┌──────────┐ ┌────────────┐ ┌──────────┐
        │ ¿Ruta Pública?│ │ ¿Token   │ │ ¿Requiere  │ │ Otros    │
        │ (public:true) │ │ válido?  │ │ auth?      │ │ casos    │
        └──────────────┘ └──────────┘ └────────────┘ └──────────┘
              │SÍ             │SÍ          │SÍ           │NO
              │               │            │              │
              ▼               ▼            ▼              ▼
        ┌──────────────┐ ┌─────────┐ ┌────────────┐ ┌──────────────┐
        │ ✅ PERMITIR   │ │ Permite │ │ ✅ PERMITIR│ │ ❌ REDIRIGIR │
        │ ACCESO       │ │ acceso  │ │ ACCESO     │ │ A /login     │
        └──────────────┘ └─────────┘ └────────────┘ └──────────────┘
```

---

## 🚦 Decisiones del beforeEach()

```javascript
Router.beforeEach((to, from, next) => {
    const isPublic = to.meta?.public === true
    const requiresAuth = to.meta?.requiresAuth === true
    const isAuthenticated = isTokenValid()

    // Decisión #1: ¿Es ruta pública?
    if (isPublic) return next() ✅

    // Decisión #2: ¿Requiere auth Y no hay token?
    if (requiresAuth && !isAuthenticated)
        return next({ name: 'LoginPage', query: { redirect: to.fullPath } }) ❌

    // Decisión #3: ¿Hay autenticación Y no es pública?
    if (isAuthenticated && !isPublic)
        return next() ✅

    // Decisión #4: ¿No hay auth Y no es pública?
    if (!isAuthenticated && !isPublic)
        return next({ name: 'LoginPage', query: { redirect: to.fullPath } }) ❌

    // Caso por defecto
    return next() ✅
})
```

---

## 🎯 Matriz de Acceso

```
┌────────────────────┬─────────────┬────────────────┬──────────────┐
│ ESCENARIO          │ TOKEN VÁLIDO│ RUTA PÚBLICA   │ RESULTADO    │
├────────────────────┼─────────────┼────────────────┼──────────────┤
│ Inicio sin login   │ ❌ No       │ ✅ Sí          │ ✅ ACCESO    │
│ Login sin token    │ ❌ No       │ ✅ Sí          │ ✅ ACCESO    │
│ Dashboard sin token│ ❌ No       │ ❌ No          │ 🔀 REDIRIGE  │
│ Dashboard con token│ ✅ Sí       │ ❌ No          │ ✅ ACCESO    │
│ Productos sin token│ ❌ No       │ ✅ Sí          │ ✅ ACCESO    │
│ Productos con token│ ✅ Sí       │ ✅ Sí          │ ✅ ACCESO    │
│ 404 sin token      │ ❌ No       │ ✅ Sí          │ ✅ ACCESO    │
│ 404 con token      │ ✅ Sí       │ ✅ Sí          │ ✅ ACCESO    │
└────────────────────┴─────────────┴────────────────┴──────────────┘
```

---

## 🔐 Ciclo Completo - Usuario SIN Autenticación

```
1️⃣  INICIO
    │
    ├─ Usuario accede: http://localhost:9000/#/Dashboard
    │
2️⃣  beforeEach() SE EJECUTA
    │
    ├─ ¿Es pública? NO ❌
    ├─ ¿Requiere auth? SÍ ✅
    ├─ ¿Tiene token válido? NO ❌
    │
3️⃣  DECISIÓN
    │
    ├─ Condición: (requiresAuth && !isAuthenticated) = VERDADERO
    ├─ Acción: Redirigir a /login
    ├─ Parámetro: ?redirect=/Dashboard
    │
4️⃣  RESULTADO
    │
    └─ Usuario ve: http://localhost:9000/#/login?redirect=/Dashboard

5️⃣  USUARIO HACE LOGIN
    │
    ├─ Envía credenciales
    ├─ Backend valida y devuelve token
    ├─ Token se guarda en localStorage/sessionStorage
    │
6️⃣  REDIRECCIÓN AUTOMÁTICA
    │
    ├─ beforeEach() detecta token válido
    ├─ Redirige usando parámetro redirect
    │
7️⃣  ACCESO PERMITIDO
    │
    └─ Usuario ve: http://localhost:9000/#/Dashboard ✅
```

---

## 🔐 Ciclo Completo - Usuario CON Autenticación

```
1️⃣  INICIO
    │
    ├─ Usuario accede: http://localhost:9000/#/Dashboard
    ├─ Token ya existe en localStorage
    │
2️⃣  beforeEach() SE EJECUTA
    │
    ├─ ¿Es pública? NO ❌
    ├─ ¿Requiere auth? SÍ ✅
    ├─ ¿Tiene token válido? SÍ ✅
    │
3️⃣  DECISIÓN
    │
    ├─ Condición: (isAuthenticated && !isPublic) = VERDADERO
    ├─ Acción: Permitir acceso
    │
4️⃣  RESULTADO
    │
    └─ Usuario ve: http://localhost:9000/#/Dashboard ✅
       Dashboard se carga correctamente
```

---

## 📂 Estructura de Rutas

```
RUTAS PÚBLICAS (requiresAuth: false, public: true)
├─ /
│  └─ IndexPage
├─ /login
│  └─ LoginPage
├─ /register
│  └─ RegisterPage
├─ /productos
│  └─ ProductosPage
├─ /categorias
│  └─ CategoriasPage
├─ /producto/:id
│  └─ ProductoDetallePage
└─ /informacion
   └─ InformacionPage

RUTAS PROTEGIDAS (requiresAuth: true)
├─ /Dashboard
│  └─ DashboardPage
├─ /Usuario
│  └─ Usuario.vue (CRUD)
├─ /Producto
│  └─ Producto.vue (CRUD)
├─ /Moneda
│  └─ Moneda.vue
├─ /Gestor
│  └─ Gestor.vue
├─ /Mensajeria
│  └─ Mensajeria.vue
├─ /Categoria
│  └─ Categoria.vue
├─ /Descuento
│  └─ Descuento.vue
├─ /Pedido
│  └─ Pedido.vue
├─ /Cupon
│  └─ Cupon.vue
├─ /OtraVariante
│  └─ OtraVariante.vue
├─ /CuentasContables
│  └─ CuentasContables.vue
├─ /AsientosContables
│  └─ AsientosContables.vue
├─ /ReporteContable
│  └─ ReporteContable.vue
├─ /Contabilidad
│  └─ ContabilidadPage.vue
├─ /CrearInformacion
│  └─ CrearInformacionPage.vue
└─ /DiagnosticoNotificaciones
   └─ DiagnosticoNotificaciones.vue

RUTAS ESPECIALES (public: true)
├─ /access-denied
│  └─ AccessDenied.vue
└─ /:catchAll(.*)*
   └─ ErrorNotFound.vue (404)
```

---

## 🔄 Flujo de Logout

```
1️⃣  Usuario hace logout
    │
    ├─ Se ejecuta: clearToken()
    │  ├─ sessionStorage.removeItem('token')
    │  └─ localStorage.removeItem('token')
    │
2️⃣  Token es eliminado
    │
    ├─ isTokenValid() retorna: false
    ├─ Composable useAuth actualiza: isAuthenticated = false
    │
3️⃣  Navegación
    │
    ├─ Usuario redirigido a: /login
    ├─ o manualmente accede a cualquier ruta
    │
4️⃣  beforeEach() en siguiente navegación
    │
    ├─ Detecta: no hay token
    ├─ Valida permisos nuevamente
    ├─ Redirige a /login si intenta acceder a ruta protegida
    │
5️⃣  USUARIO DESLOGUEADO ✅
```

---

## 📊 Estados de AuthHelper

```
┌─────────────────────────────────────────┐
│ FUNCIONES DE authHelper.js              │
├─────────────────────────────────────────┤
│                                         │
│ isTokenValid() ───────► boolean         │
│   Verifica si token no ha expirado      │
│                                         │
│ getToken() ───────► string|null         │
│   Obtiene token del storage             │
│                                         │
│ decodeToken(token) ───────► object      │
│   Decodifica el JWT                     │
│                                         │
│ getUserInfo() ───────► object|null      │
│   Obtiene datos del usuario del token   │
│                                         │
│ getUserRole() ───────► string|null      │
│   Obtiene rol del usuario               │
│                                         │
│ clearToken() ───────► void              │
│   Elimina token del storage             │
│                                         │
│ hasRole(role) ───────► boolean          │
│   Verifica si usuario tiene rol         │
│                                         │
│ hasAnyRole(roles) ───────► boolean      │
│   Verifica si usuario tiene algún rol   │
│                                         │
└─────────────────────────────────────────┘
```

---

## 🧪 Matriz de Pruebas

```
┌───────┬────────────────────────┬──────────────┬──────────────┬────────────────┐
│ Test# │ Escenario              │ Acción       │ Token        │ Resultado      │
├───────┼────────────────────────┼──────────────┼──────────────┼────────────────┤
│ T001  │ Acceso Inicio          │ Click / URL  │ ❌ Ninguno   │ ✅ Carga OK    │
│ T002  │ Acceso Login           │ Click / URL  │ ❌ Ninguno   │ ✅ Carga OK    │
│ T003  │ Acceso Dashboard       │ URL directa  │ ❌ Ninguno   │ 🔀 Redirige    │
│ T004  │ Login exitoso          │ Submit form  │ ❌ → ✅      │ ✅ Token OK    │
│ T005  │ Acceso Dashboard       │ URL directa  │ ✅ Token OK  │ ✅ Carga OK    │
│ T006  │ Ruta inexistente       │ URL directa  │ ❌ Ninguno   │ ✅ 404         │
│ T007  │ Logout                 │ Click botón  │ ✅ → ❌      │ ✅ Limpiado    │
│ T008  │ Token expirado         │ URL directa  │ ❌ Expirado  │ 🔀 A login     │
│ T009  │ Acceso productos       │ URL directa  │ ❌ Ninguno   │ ✅ Carga OK    │
│ T010  │ Acceso Moneda          │ URL directa  │ ❌ Ninguno   │ 🔀 Redirige    │
└───────┴────────────────────────┴──────────────┴──────────────┴────────────────┘
```

---

## 🎨 Componentes Visibles

### Sin Autenticación:
```
┌─────────────────────────────────┐
│     PÁGINA DE INICIO            │
│                                 │
│  [Login]  [Registro]            │
│                                 │
│  Productos públicos             │
│  Categorías públicas            │
│  Carrito de compras             │
│  Lista de deseos                │
└─────────────────────────────────┘
```

### Con Autenticación:
```
┌─────────────────────────────────┐
│   MAINLAYOUT + SIDEBAR          │
│  ┌──────────────────────────┐   │
│  │ Dashboard                │   │
│  │ Productos (CRUD)         │   │
│  │ Categorías (CRUD)        │   │
│  │ Monedas                  │   │
│  │ Usuarios (CRUD)          │   │
│  │ Descuentos               │   │
│  │ Pedidos                  │   │
│  │ Cupones                  │   │
│  │ Contabilidad             │   │
│  │ Mensajería               │   │
│  │ Perfil                   │   │
│  │ [Logout]                 │   │
│  └──────────────────────────┘   │
└─────────────────────────────────┘
```

---

## 📞 Función useAuth en Componentes

```javascript
// En cualquier componente Vue
import { useAuth } from '@/assets/js/composables/useAuth'

export default {
    setup() {
        const {
            isAuthenticated,  // boolean: ¿está logueado?
            user,             // object: datos del usuario
            currentRole,      // string: rol actual
            checkRole,        // function: verificar rol
            logout            // function: desloguear
        } = useAuth()

        return {
            isAuthenticated,
            user,
            currentRole,
            checkRole,
            logout
        }
    }
}
```

---

**Última actualización**: 2026-01-24
**Estado**: ✅ OPERATIVO
**Versión**: 1.0
