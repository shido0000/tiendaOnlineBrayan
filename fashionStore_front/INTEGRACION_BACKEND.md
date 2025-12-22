# Guía de Integración Backend - Módulo de Contabilidad

## 📌 Requisitos

Este documento describe los endpoints y métodos necesarios en el backend para que funcione correctamente el módulo de contabilidad del frontend.

## 🔌 Endpoints Requeridos

### 1. **CUENTAS CONTABLES**

#### GET `/api/CuentasContables`
Obtiene la lista de todas las cuentas contables.

**Parámetros Query (opcionales):**
```
- pageNumber: int (número de página)
- pageSize: int (elementos por página)
- search: string (búsqueda por código o nombre)
- esActivo: bool (filtrar por estado)
```

**Respuesta (200 OK):**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "codigo": "1.1.01",
    "nombre": "Caja General",
    "esActivo": true,
    "esDeMovimiento": true,
    "cuentaPadreId": null,
    "cuentaPadre": null,
    "subCuentas": []
  }
]
```

---

#### GET `/api/CuentasContables/{id}`
Obtiene una cuenta contable específica con todas sus relaciones.

**Parámetros:**
- `id` (path): UUID de la cuenta

**Respuesta (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "codigo": "1.1.01",
  "nombre": "Caja General",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null,
  "cuentaPadre": null,
  "subCuentas": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440001",
      "codigo": "1.1.02",
      "nombre": "Caja Secundaria",
      "esActivo": true
    }
  ]
}
```

---

#### POST `/api/CuentasContables`
Crea una nueva cuenta contable.

**Body (JSON):**
```json
{
  "codigo": "1.1.01",
  "nombre": "Caja General",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null
}
```

**Respuesta (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "codigo": "1.1.01",
  "nombre": "Caja General",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null,
  "cuentaPadre": null,
  "subCuentas": []
}
```

**Validaciones:**
- Código no puede estar duplicado
- Nombre es requerido
- Si `cuentaPadreId` es proporcionado, debe existir una cuenta con ese ID

---

#### PUT `/api/CuentasContables/{id}`
Actualiza una cuenta contable existente.

**Parámetros:**
- `id` (path): UUID de la cuenta

**Body (JSON):**
```json
{
  "codigo": "1.1.01",
  "nombre": "Caja General Actualizada",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null
}
```

**Respuesta (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "codigo": "1.1.01",
  "nombre": "Caja General Actualizada",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null,
  "cuentaPadre": null,
  "subCuentas": []
}
```

---

#### DELETE `/api/CuentasContables/{id}`
Elimina una cuenta contable.

**Parámetros:**
- `id` (path): UUID de la cuenta

**Respuesta (204 No Content):**
Sin cuerpo de respuesta

**Restricciones:**
- No se puede eliminar una cuenta que tenga subcuentas
- No se puede eliminar una cuenta que tenga movimientos contables asociados

---

### 2. **ASIENTOS CONTABLES**

#### GET `/api/AsientosContables`
Obtiene la lista de asientos contables con paginación y filtros.

**Parámetros Query:**
```
- pageNumber: int (número de página, default: 1)
- pageSize: int (elementos por página, default: 10)
- descripcion: string (búsqueda en descripción)
- tipoReferencia: string (filtrar por tipo: "Venta", "Devolucion", etc.)
- fechaInicio: date (YYYY-MM-DD)
- fechaFin: date (YYYY-MM-DD)
```

**Respuesta (200 OK):**
```json
{
  "items": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "fecha": "2025-01-15T10:30:00Z",
      "descripcion": "Venta de productos",
      "referenciaId": "550e8400-e29b-41d4-a716-446655440100",
      "tipoReferencia": "Venta",
      "movimientos": [
        {
          "id": "550e8400-e29b-41d4-a716-446655440200",
          "asientoContableId": "550e8400-e29b-41d4-a716-446655440000",
          "cuentaContableId": "550e8400-e29b-41d4-a716-446655440001",
          "cuenta": {
            "id": "550e8400-e29b-41d4-a716-446655440001",
            "codigo": "1.1.01",
            "nombre": "Caja"
          },
          "debe": 1000.00,
          "haber": 0.00
        }
      ]
    }
  ],
  "totalCount": 50,
  "pageNumber": 1,
  "pageSize": 10
}
```

---

#### GET `/api/AsientosContables/{id}`
Obtiene un asiento contable específico con todos sus movimientos.

**Parámetros:**
- `id` (path): UUID del asiento

**Respuesta (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "fecha": "2025-01-15T10:30:00Z",
  "descripcion": "Venta de productos",
  "referenciaId": "550e8400-e29b-41d4-a716-446655440100",
  "tipoReferencia": "Venta",
  "movimientos": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440200",
      "asientoContableId": "550e8400-e29b-41d4-a716-446655440000",
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440001",
      "cuenta": {
        "id": "550e8400-e29b-41d4-a716-446655440001",
        "codigo": "1.1.01",
        "nombre": "Caja"
      },
      "debe": 1000.00,
      "haber": 0.00
    },
    {
      "id": "550e8400-e29b-41d4-a716-446655440201",
      "asientoContableId": "550e8400-e29b-41d4-a716-446655440000",
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440002",
      "cuenta": {
        "id": "550e8400-e29b-41d4-a716-446655440002",
        "codigo": "4.1.01",
        "nombre": "Ingresos por Ventas"
      },
      "debe": 0.00,
      "haber": 1000.00
    }
  ]
}
```

---

#### POST `/api/AsientosContables`
Crea un nuevo asiento contable con sus movimientos.

**Body (JSON):**
```json
{
  "fecha": "2025-01-15T10:30:00Z",
  "descripcion": "Venta de productos",
  "referenciaId": "550e8400-e29b-41d4-a716-446655440100",
  "tipoReferencia": "Venta",
  "movimientos": [
    {
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440001",
      "debe": 1000.00,
      "haber": 0.00
    },
    {
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440002",
      "debe": 0.00,
      "haber": 1000.00
    }
  ]
}
```

**Respuesta (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "fecha": "2025-01-15T10:30:00Z",
  "descripcion": "Venta de productos",
  "referenciaId": "550e8400-e29b-41d4-a716-446655440100",
  "tipoReferencia": "Venta",
  "movimientos": [...]
}
```

**Validaciones:**
- Debe haber al menos 2 movimientos
- La suma del debe debe ser igual a la suma del haber
- Todas las cuentas deben tener `esDeMovimiento = true`
- Todas las cuentas deben existir
- El `tipoReferencia` es requerido

---

### 3. **REPORTES CONTABLES (Opcional pero Recomendado)**

#### GET `/api/AsientosContables/reportes/estado-cuentas`
Obtiene el estado actual de todas las cuentas con saldos.

**Parámetros Query:**
```
- fechaInicio: date (opcional)
- fechaFin: date (opcional)
```

**Respuesta (200 OK):**
```json
[
  {
    "cuentaContableId": "550e8400-e29b-41d4-a716-446655440001",
    "codigo": "1.1.01",
    "nombre": "Caja",
    "totalDebe": 5000.00,
    "totalHaber": 3000.00,
    "saldo": 2000.00
  }
]
```

---

#### GET `/api/AsientosContables/reportes/balance-prueba`
Obtiene el balance de prueba (todos los saldos).

**Respuesta (200 OK):**
```json
{
  "cuentas": [
    {
      "codigo": "1.1.01",
      "nombre": "Caja",
      "debe": 5000.00,
      "haber": 3000.00,
      "saldo": 2000.00
    }
  ],
  "totalDebe": 50000.00,
  "totalHaber": 50000.00,
  "diferencia": 0.00,
  "estaEquilibrado": true
}
```

---

#### GET `/api/AsientosContables/reportes/totales`
Obtiene los totales generales.

**Respuesta (200 OK):**
```json
{
  "totalDebe": 50000.00,
  "totalHaber": 50000.00,
  "diferencia": 0.00,
  "cantidadAsientos": 150,
  "estaEquilibrado": true
}
```

---

#### GET `/api/AsientosContables/exportar/csv`
Exporta los asientos a formato CSV.

**Parámetros Query:** (mismo que GET /api/AsientosContables)

**Respuesta:** Archivo CSV descargable

---

#### GET `/api/AsientosContables/exportar/excel`
Exporta los asientos a formato Excel.

**Parámetros Query:** (mismo que GET /api/AsientosContables)

**Respuesta:** Archivo XLSX descargable

---

## 🔄 Flujo de Integración

### Paso 1: Crear Estructura de Cuentas
```csharp
// Crear cuentas padre
POST /api/CuentasContables
{
  "codigo": "1",
  "nombre": "Activos",
  "esActivo": true,
  "esDeMovimiento": false
}

// Crear subcuentas
POST /api/CuentasContables
{
  "codigo": "1.1",
  "nombre": "Activos Circulantes",
  "esActivo": true,
  "esDeMovimiento": false,
  "cuentaPadreId": "{id_de_activos}"
}
```

### Paso 2: Crear Asientos al Registrar Ventas
Cuando se registra una venta, crear automáticamente un asiento:
```csharp
POST /api/AsientosContables
{
  "fecha": DateTime.UtcNow,
  "descripcion": "Venta de productos",
  "referenciaId": ventaId,
  "tipoReferencia": "Venta",
  "movimientos": [
    {
      "cuentaContableId": cuentaCajaId,
      "debe": totalVenta,
      "haber": 0
    },
    {
      "cuentaContableId": cuentaIngresosId,
      "debe": 0,
      "haber": totalVenta
    }
  ]
}
```

### Paso 3: Consultar Reportes
```csharp
GET /api/AsientosContables/reportes/estado-cuentas?fechaInicio=2025-01-01&fechaFin=2025-01-31
```

## 📋 Estructura de Base de Datos Recomendada

```sql
-- Cuentas Contables
CREATE TABLE CuentasContables (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Codigo NVARCHAR(50) NOT NULL UNIQUE,
    Nombre NVARCHAR(255) NOT NULL,
    EsActivo BIT NOT NULL DEFAULT 1,
    EsDeMovimiento BIT NOT NULL DEFAULT 1,
    CuentaPadreId UNIQUEIDENTIFIER NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FechaModificacion DATETIME NULL,
    FOREIGN KEY (CuentaPadreId) REFERENCES CuentasContables(Id)
);

-- Asientos Contables
CREATE TABLE AsientosContables (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETUTCDATE(),
    Descripcion NVARCHAR(MAX) NOT NULL,
    ReferenciaId UNIQUEIDENTIFIER NOT NULL,
    TipoReferencia NVARCHAR(50) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FechaModificacion DATETIME NULL
);

-- Movimientos Contables
CREATE TABLE MovimientosContables (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AsientoContableId UNIQUEIDENTIFIER NOT NULL,
    CuentaContableId UNIQUEIDENTIFIER NOT NULL,
    Debe DECIMAL(18, 2) NOT NULL DEFAULT 0,
    Haber DECIMAL(18, 2) NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (AsientoContableId) REFERENCES AsientosContables(Id) ON DELETE CASCADE,
    FOREIGN KEY (CuentaContableId) REFERENCES CuentasContables(Id)
);

-- Índices
CREATE INDEX IX_CuentasContables_CuentaPadreId ON CuentasContables(CuentaPadreId);
CREATE INDEX IX_AsientosContables_Fecha ON AsientosContables(Fecha);
CREATE INDEX IX_AsientosContables_TipoReferencia ON AsientosContables(TipoReferencia);
CREATE INDEX IX_MovimientosContables_AsientoContableId ON MovimientosContables(AsientoContableId);
CREATE INDEX IX_MovimientosContables_CuentaContableId ON MovimientosContables(CuentaContableId);
```

## ⚙️ Implementación en C#

### Controlador Base
```csharp
[ApiController]
[Route("api/[controller]")]
public class CuentasContablesController : ControllerBase
{
    private readonly ICuentaContableService _service;

    [HttpGet]
    public async Task<ActionResult<List<CuentaContable>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<CuentaContable>> Create(CreateCuentaContableDto dto)
    {
        var cuenta = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = cuenta.Id }, cuenta);
    }
}
```

## 🧪 Pruebas Recomendadas

1. Verificar que el total debe = total haber en asientos
2. Validar que no se pueden eliminar cuentas con movimientos
3. Verificar que los reportes calculan correctamente los saldos
4. Probar filtros de fecha en asientos
5. Validar que solo cuentas con esDeMovimiento=true acepten movimientos

## 📞 Notas Importantes

- Los IDs deben ser UNIQUEIDENTIFIER (GUID)
- Las fechas deben almacenarse en UTC
- El cálculo de debe/haber debe ser decimal con 2 decimales
- Los reportes deben considerar el rango de fechas si se proporciona
- Los asientos deben ser de solo lectura una vez creados (no se pueden editar)
