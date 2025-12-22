# Módulo de Contabilidad

## 📋 Descripción General

El módulo de contabilidad permite al administrador gestionar las cuentas contables del sistema y visualizar todos los asientos contables generados por las transacciones de ventas. Proporciona herramientas para configuración, seguimiento y análisis de datos contables.

## 🎯 Funcionalidades Principales

### 1. **Gestión de Cuentas Contables** (`CuentasContables.vue`)
Permite crear, editar, eliminar y organizar las cuentas contables del sistema.

#### Características:
- ✅ Crear nuevas cuentas contables con código y nombre
- ✅ Estructura jerárquica de cuentas (cuenta padre/subcuentas)
- ✅ Activar/desactivar cuentas
- ✅ Especificar si una cuenta acepta asientos contables
- ✅ Vista en árbol para visualizar la estructura de cuentas
- ✅ Búsqueda y filtrado de cuentas

#### Propiedades de Cuenta Contable:
```javascript
{
  id: UUID,
  codigo: "1.1.01",        // Ej: 1.1.01 (estructura jerárquica)
  nombre: "Caja",          // Nombre descriptivo
  esActivo: true,          // Estado de la cuenta
  esDeMovimiento: true,    // Si admite asientos contables
  cuentaPadreId: UUID,     // Referencia a cuenta padre (opcional)
  cuentaPadre: {},         // Objeto de cuenta padre
  subCuentas: []           // Array de subcuentas
}
```

### 2. **Visualización de Asientos Contables** (`AsientosContables.vue`)
Permite ver todos los asientos contables del sistema con filtros y detalles completos.

#### Características:
- ✅ Tabla de asientos contables con paginación
- ✅ Filtrar por:
  - Descripción del asiento
  - Tipo de referencia (Venta, Devolución, etc.)
  - Rango de fechas
- ✅ Ver detalles completos del asiento
- ✅ Tabla de movimientos contables (Debe/Haber)
- ✅ Validación de equilibrio del asiento
- ✅ Cálculo automático de totales

#### Propiedades de Asiento Contable:
```javascript
{
  id: UUID,
  fecha: "2025-01-15T10:30:00Z",
  descripcion: "Venta de productos",
  referenciaId: UUID,              // ID de la venta, devolución, etc.
  tipoReferencia: "Venta",         // Tipo: Venta, Devolucion, Ajuste, Cierre
  movimientos: [
    {
      id: UUID,
      asientoContableId: UUID,
      cuentaContableId: UUID,
      cuenta: {},                  // Objeto de cuenta contable
      debe: 1000.00,              // Monto en debe
      haber: 0.00                 // Monto en haber
    }
  ]
}
```

### 3. **Reportes Contables** (`ReporteContable.vue`)
Proporciona análisis y reportes de los datos contables.

#### Características:
- ✅ **Estado de Cuentas**: Tabla con saldos por cuenta
- ✅ **Resumen por Tipo**: Agrupación de movimientos por tipo
- ✅ **Movimientos por Período**: Análisis temporal
- ✅ **Indicadores Clave**:
  - Total de asientos
  - Total debe
  - Total haber
  - Diferencia (equilibrio/desbalance)
- ✅ **Exportación**: (en desarrollo) Export a CSV/Excel
- ✅ Filtrado por fecha y cuenta

## 🛠️ Integración en el Sistema

### Rutas
Las siguientes rutas están disponibles en el sistema:

```javascript
// En src/router/routes.js
{
  path: 'CuentasContables',
  name: 'CuentasContables',
  component: () => import('src/pages/Nomenclators/Contabilidad/CuentasContables.vue')
},
{
  path: 'AsientosContables',
  name: 'AsientosContables',
  component: () => import('src/pages/Nomenclators/Contabilidad/AsientosContables.vue')
},
{
  path: 'ReporteContable',
  name: 'ReporteContable',
  component: () => import('src/pages/Nomenclators/Contabilidad/ReporteContable.vue')
}
```

### Acceso desde el Menú
La tarjeta de "Contabilidad" está disponible en:
- **Nomencladores** > **Contabilidad**

O accede directamente desde el menú de tarjetas en `NomenclatorsCard.vue`.

## 💾 API Backend Requerida

El sistema requiere los siguientes endpoints en el backend:

### Cuentas Contables
```
GET    /api/CuentasContables              - Obtener todas las cuentas
GET    /api/CuentasContables/{id}         - Obtener una cuenta por ID
POST   /api/CuentasContables              - Crear una nueva cuenta
PUT    /api/CuentasContables/{id}         - Actualizar una cuenta
DELETE /api/CuentasContables/{id}         - Eliminar una cuenta
```

### Asientos Contables
```
GET    /api/AsientosContables             - Obtener asientos con paginación
GET    /api/AsientosContables/{id}        - Obtener un asiento por ID
POST   /api/AsientosContables             - Crear un asiento

Parámetros de filtrado:
- pageNumber: número de página
- pageSize: tamaño de página
- descripcion: buscar por descripción
- tipoReferencia: filtrar por tipo
- fechaInicio: fecha de inicio
- fechaFin: fecha de fin
```

### Reportes (Opcionales)
```
GET    /api/AsientosContables/reportes/estado-cuentas
GET    /api/AsientosContables/reportes/resumen
GET    /api/AsientosContables/reportes/balance-prueba
GET    /api/AsientosContables/exportar/csv
GET    /api/AsientosContables/exportar/excel
```

## 📦 Estructura de Carpetas

```
src/
├── pages/
│   └── Nomenclators/
│       └── Contabilidad/
│           ├── CuentasContables.vue      # Gestión de cuentas
│           ├── AsientosContables.vue     # Visualización de asientos
│           ├── ReporteContable.vue       # Reportes y análisis
│           └── ContabilidadCard.vue      # Tarjeta de acceso
├── assets/
│   └── js/
│       └── util/
│           └── contabilidadAPI.js        # Servicio API
├── stores/
│   └── contabilidadStore.js              # Vuex store
└── router/
    └── routes.js                          # Rutas actualizadas
```

## 🔧 Servicio API (contabilidadAPI.js)

Proporciona métodos para interactuar con la API de contabilidad:

```javascript
import { ContabilidadAPI } from 'src/assets/js/util/contabilidadAPI';

// Cuentas
await ContabilidadAPI.getCuentas();
await ContabilidadAPI.crearCuenta(data);
await ContabilidadAPI.actualizarCuenta(id, data);
await ContabilidadAPI.eliminarCuenta(id);

// Asientos
await ContabilidadAPI.getAsientos(params);
await ContabilidadAPI.crearAsiento(data);
await ContabilidadAPI.getAsientosEntreFechas(inicio, fin);

// Reportes
await ContabilidadAPI.getEstadoDeCuentas(inicio, fin);
await ContabilidadAPI.getResumenMovimientos(inicio, fin);
```

## 📊 Vuex Store (contabilidadStore.js)

State disponible:
```javascript
{
  cuentas: [],
  asientos: [],
  cuentaSeleccionada: null,
  asientoSeleccionado: null,
  cargando: false,
  error: null,
  estadoCuentas: [],
  resumenMovimientos: []
}
```

Getters calculados:
- `getTotalDebe`: Suma total del debe
- `getTotalHaber`: Suma total del haber
- `getDiferencia`: Diferencia (Debe - Haber)
- `estaEquilibrado`: Verifica si los asientos están equilibrados

## 📝 Ejemplo de Uso

### Crear una Cuenta Contable
```javascript
const nuevaCuenta = {
  codigo: "1.1.01",
  nombre: "Caja General",
  esDeMovimiento: true,
  esActivo: true,
  cuentaPadreId: null
};

await ContabilidadAPI.crearCuenta(nuevaCuenta);
```

### Crear un Asiento Contable
```javascript
const nuevoAsiento = {
  fecha: new Date().toISOString(),
  descripcion: "Venta de productos",
  referenciaId: ventaId,
  tipoReferencia: "Venta",
  movimientos: [
    {
      cuentaContableId: cuentaCajaId,
      debe: 1000,
      haber: 0
    },
    {
      cuentaContableId: cuentaIngresosId,
      debe: 0,
      haber: 1000
    }
  ]
};

await ContabilidadAPI.crearAsiento(nuevoAsiento);
```

## ✅ Validaciones

### Cuentas Contables
- Código es requerido
- Nombre es requerido
- No se puede asignar una cuenta como padre de sí misma
- Solo se pueden eliminar cuentas sin subcuentas

### Asientos Contables
- Total Debe debe ser igual a Total Haber
- Al menos un movimiento es requerido
- Las cuentas deben permitir movimientos (esDeMovimiento = true)

## 🎨 Estilos

Todos los componentes utilizan:
- **Framework CSS**: Quasar Framework
- **Icono "Contabilidad"**: `account_balance`
- **Colores**: Primary (azul), Secondary (gris), Positive (verde), Negative (rojo)

## 📈 Próximas Mejoras

- [ ] Exportación a Excel con formatos personalizados
- [ ] Exportación a PDF de reportes
- [ ] Gráficos de análisis contable
- [ ] Cierre de período contable
- [ ] Auditoría de cambios
- [ ] Impresión de asientos
- [ ] Cálculo automático de asientos para ventas

## 🔐 Permisos Recomendados

Se recomienda implementar control de acceso basado en roles:
- `contabilidad.ver`: Ver cuentas y asientos
- `contabilidad.crear`: Crear cuentas
- `contabilidad.editar`: Editar cuentas
- `contabilidad.eliminar`: Eliminar cuentas
- `reportes.ver`: Ver reportes contables

## 📞 Soporte

Para reportar errores o sugerencias sobre el módulo de contabilidad, contacta al equipo de desarrollo.
