---
# 🧪 PÁGINAS DE TESTING DISPONIBLES

## 📊 Monitor de Carritos - Dos Opciones

Ahora tienes **dos versiones** para testear el monitor:

---

## 1️⃣ TestActiveCartsMonitor (Datos Mock)

**🔗 URL:** `/#/TestActiveCartsMonitor`

**📋 Descripción:**
- Carga datos **simulados/ficticios**
- Botones para cargar, simular cambios, etc.
- **No requiere backend**
- Perfecto para testing de UI sin conexión

**🎯 Casos de Uso:**
- ✅ Desarrollo inicial (sin backend)
- ✅ Testing de estilos y componentes
- ✅ Diseño de interfaz
- ✅ Demostración rápida

**📊 Características:**
- Datos Mock preconfigurados
- Simulación de cambios cada 3 segundos
- Control manual de simulación
- No requiere conexión a BD

**👤 Usuarios Mock Disponibles:**
- Juan Pérez
- María García
- Carlos López
- etc. (Ver MOCK_ACTIVE_CARTS)

**Botones Disponibles:**
```
[Cargar Datos Mock] → Carga el dataset de ejemplo
[Iniciar Simulación] → Simula cambios aleatorios cada 3s
[Vaciar Datos] → Limpia la lista
```

---

## 2️⃣ RealActiveCartsMonitor (Datos de Base de Datos)

**🔗 URL:** `/#/RealActiveCartsMonitor`

**📋 Descripción:**
- Carga datos **reales de la base de datos**
- Conecta con el backend vía API
- **Requiere backend funcionando**
- Perfecto para testing end-to-end

**🎯 Casos de Uso:**
- ✅ Testing con datos reales
- ✅ Validar integración backend
- ✅ Testing en tiempo real (SignalR)
- ✅ Debugging de datos

**📊 Características:**
- Obtiene carritos del endpoint API
- Autorecargar cada 15 segundos (configurable)
- Actualización en tiempo real via SignalR
- Control de conexión SignalR
- Estadísticas en vivo

**Botones Disponibles:**
```
[Cargar Datos] → GET /api/usuarios/carritos-activos
[Autorecargar] → Activa/desactiva recargas periódicas
[Vaciar Datos] → Limpia la lista
```

**Requisitos del Backend:**
- ✅ Endpoint: `GET /api/usuarios/carritos-activos`
- ✅ Evento SignalR: `CarritoActualizado` (opcional)

---

## 📊 COMPARACIÓN RÁPIDA

| Aspecto | TestActive... | RealActive... |
|---|---|---|
| **Datos** | 🎨 Mock | 💾 BD Real |
| **Endpoint** | ❌ Ninguno | ✅ `/carritos-activos` |
| **Backend Requerido** | ❌ No | ✅ Sí |
| **Autorecargar** | Simulación (3s) | ✅ Real (15s) |
| **Cambios** | Aleatorios | Reales del backend |
| **SignalR** | ❌ Simulado | ✅ Real |
| **Velocidad** | ⚡ Instantánea | 🔄 Según API |
| **Uso** | 👨‍💻 Dev inicial | 📊 Integration |

---

## 🎯 ¿Cuál Usar?

### Escenario 1: Backend aún NO está implementado
```
Usa: TestActiveCartsMonitor
Razón: No requiere backend, perfecto para UI/UX
```

### Escenario 2: Backend está implementado pero sin SignalR
```
Usa: RealActiveCartsMonitor con Autorecargar
Razón: Carga datos reales, actualiza cada N segundos
```

### Escenario 3: Backend con SignalR completo
```
Usa: RealActiveCartsMonitor sin Autorecargar
Razón: SignalR actualiza automáticamente
```

### Escenario 4: Debugging detallado
```
Usa: Cualquiera + habilitar "Logs en consola"
Razón: Ver todo lo que ocurre en console
```

---

## 🚀 Flujo de Testing Recomendado

```
FASE 1: Frontend SOLO
┌─────────────────────────────┐
│ TestActiveCartsMonitor      │
│ - OK datos mock             │
│ - OK estilos                │
│ - OK componentes            │
└─────────────────────────────┘
          ↓
FASE 2: Backend Endpoint
┌─────────────────────────────┐
│ RealActiveCartsMonitor      │
│ + Backend retorna carritos  │
│ + Botón "Cargar Datos" OK   │
│ + Autorecargar funciona     │
└─────────────────────────────┘
          ↓
FASE 3: Tiempo Real
┌─────────────────────────────┐
│ RealActiveCartsMonitor      │
│ + SignalR evento recibido   │
│ + Actualización sin recargar│
│ + Staff cambio en tiempo    │
│    tiempo                   │
└─────────────────────────────┘
          ↓
FASE 4: Producción
┌─────────────────────────────┐
│ DashboardPage               │
│ + Componente integrado      │
│ + Datos reales              │
│ + Usuarios autenticados     │
│ + Permisos verificados      │
└─────────────────────────────┘
```

---

## 📝 Archivos Involucrados

### TestActiveCartsMonitor
```
src/pages/Test/TestActiveCartsMonitor.vue
src/assets/js/util/mockActiveCartsData.js
```

### RealActiveCartsMonitor
```
src/pages/Test/RealActiveCartsMonitor.vue  ← NUEVO
src/services/activeCartsService.js
src/services/signalRService.js
```

### Rutas
```
src/router/routes.js
─ /TestActiveCartsMonitor
─ /RealActiveCartsMonitor (nuevo)
```

---

## 🔧 Acceso Rápido desde Console

```javascript
// Para ver datos en TestActiveCartsMonitor
console.__mockData

// Para ver datos en RealActiveCartsMonitor
console.log(window.__realActiveCartsData)
```

---

## 📊 Estructura de Datos (Ambas Páginas)

```javascript
{
  usuarioId: "550e8400-e29b-41d4-a716-446655440001",
  nombre: "Juan",
  apellido: "Pérez",
  email: "juan@mail.com",
  cartCount: 5,                    // Cantidad de items
  cartTotal: 250.00,               // Valor total
  cartItems: [                     // Detalles
    {
      id: "item-1",
      nombre: "Producto 1",
      cantidad: 2,
      precio: 50.00,
      variante: "Rojo XL"
    }
  ]
}
```

---

## 🎓 Guía para Elegir

**Pregunta:** ¿Tengo backend implementado?

```
SÍ → ¿Quiero probar en tiempo real?
    SÍ → RealActiveCartsMonitor
    NO → RealActiveCartsMonitor (sin autorecargar)

NO → ¿Quiero hacer testing de UI?
    SÍ → TestActiveCartsMonitor
    NO → Espera a tener backend
```

---

## 📞 Support

**Página:** `RealActiveCartsMonitor.vue` no funciona?

**Checklist:**
1. ✅ ¿Backend está corriendo?
2. ✅ ¿Endpoint `/api/usuarios/carritos-activos` existe?
3. ✅ ¿Token de autenticación válido?
4. ✅ ¿Datos en la BD?
5. ✅ ¿Habilitar "Logs en consola" para debug?

**Solución Log:**
```javascript
// En console:
await activeCartsService.getActiveCarts()
// Debe retornar datos o error claro
```

---

## 🎉 Resumen

**Tienes 2 opciones:**

| Opción | Testing | Link | Requiere Backend |
|---|---|---|---|
| 1 | UI/UX con Datos Mock | `/#/TestActiveCartsMonitor` | ❌ |
| 2 | Datos Reales de BD | `/#/RealActiveCartsMonitor` | ✅ |

**Usa ambas según tu momento de desarrollo:**
- Inicio: Opción 1 (sin backend)
- Integración: Opción 2 (con backend)

