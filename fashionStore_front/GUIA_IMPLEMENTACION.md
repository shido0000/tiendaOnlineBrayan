# 🚀 GUÍA DE IMPLEMENTACIÓN - MÓDULO DE CONTABILIDAD

## 📋 Tabla de Contenidos
1. [Verificación Inicial](#verificación-inicial)
2. [Instalación Frontend](#instalación-frontend)
3. [Configuración Backend](#configuración-backend)
4. [Pruebas](#pruebas)
5. [Solución de Problemas](#solución-de-problemas)

---

## ✅ Verificación Inicial

Antes de comenzar, verifica que tengas:
- ✓ Node.js v14+ instalado
- ✓ Vue.js 3 y Quasar Framework configurados
- ✓ Backend .NET configurado y ejecutándose
- ✓ Base de datos disponible

---

## 🎯 Instalación Frontend

### Paso 1: Verificar Estructura de Carpetas
```
src/
├── pages/Nomenclators/Contabilidad/
│   ├── CuentasContables.vue ✓
│   ├── AsientosContables.vue ✓
│   ├── ReporteContable.vue ✓
│   ├── ContabilidadCard.vue ✓
│   └── README.md ✓
├── assets/js/util/
│   ├── contabilidadAPI.js ✓
│   ├── contabilidadHelper.js ✓
│   └── EJEMPLOS_CONTABILIDAD.js ✓
├── stores/
│   └── contabilidadStore.js ✓
└── router/
    └── routes.js (actualizado) ✓
```

### Paso 2: Registrar el Store en Vuex
Abre `src/stores/index.js` (o donde esté tu configuración de Vuex):

```javascript
// src/stores/index.js
import { createStore } from 'vuex';
import contabilidadStore from './contabilidadStore';

export default createStore({
  modules: {
    contabilidad: contabilidadStore,
  },
});
```

Si estás usando Pinia en su lugar, la configuración es similar.

### Paso 3: Verificar Rutas
Verifica que en `src/router/routes.js` existan:

```javascript
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

### Paso 4: Verificar Card en Nomencladores
Verifica que `src/assets/js/linksCard/linkCards.js` incluya:

```javascript
{
    title: "Contabilidad",
    icon: "account_balance",
    link: "CuentasContables",
}
```

---

## ⚙️ Configuración Backend

### Paso 1: Crear Entidades

Crea los modelos en tu proyecto C#:

```csharp
using System;
using System.Collections.Generic;

public class CuentaContable : EntidadBase
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public bool EsActivo { get; set; } = true;
    public bool EsDeMovimiento { get; set; } = true;
    public Guid? CuentaPadreId { get; set; }
    public CuentaContable? CuentaPadre { get; set; }
    public ICollection<CuentaContable> SubCuentas { get; set; } = new List<CuentaContable>();
}

public class AsientoContable : EntidadBase
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Descripcion { get; set; } = string.Empty;
    public Guid ReferenciaId { get; set; }
    public string TipoReferencia { get; set; } = string.Empty;
    public ICollection<MovimientoContable> Movimientos { get; set; } = new List<MovimientoContable>();
}

public class MovimientoContable : EntidadBase
{
    public Guid AsientoContableId { get; set; }
    public AsientoContable Asiento { get; set; } = null!;
    public Guid CuentaContableId { get; set; }
    public CuentaContable Cuenta { get; set; } = null!;
    public decimal Debe { get; set; }
    public decimal Haber { get; set; }
}
```

### Paso 2: Crear DbContext

Agrega los DbSets:

```csharp
public DbSet<CuentaContable> CuentasContables { get; set; }
public DbSet<AsientoContable> AsientosContables { get; set; }
public DbSet<MovimientoContable> MovimientosContables { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Relación de CuentaContable
    modelBuilder.Entity<CuentaContable>()
        .HasOne(c => c.CuentaPadre)
        .WithMany(c => c.SubCuentas)
        .HasForeignKey(c => c.CuentaPadreId);

    // Relación de MovimientoContable
    modelBuilder.Entity<MovimientoContable>()
        .HasOne(m => m.Asiento)
        .WithMany(a => a.Movimientos)
        .HasForeignKey(m => m.AsientoContableId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<MovimientoContable>()
        .HasOne(m => m.Cuenta)
        .WithMany()
        .HasForeignKey(m => m.CuentaContableId);
}
```

### Paso 3: Crear Migraciones

```bash
dotnet ef migrations add AddContabilidad
dotnet ef database update
```

### Paso 4: Crear Servicios

```csharp
public interface ICuentaContableService
{
    Task<List<CuentaContable>> GetAllAsync();
    Task<CuentaContable> GetByIdAsync(Guid id);
    Task<CuentaContable> CreateAsync(CreateCuentaContableDto dto);
    Task<CuentaContable> UpdateAsync(Guid id, UpdateCuentaContableDto dto);
    Task DeleteAsync(Guid id);
}

public interface IAsientoContableService
{
    Task<PagedResult<AsientoContable>> GetAsientosAsync(int pageNumber, int pageSize,
        string? descripcion = null, string? tipoReferencia = null,
        DateTime? fechaInicio = null, DateTime? fechaFin = null);
    Task<AsientoContable> GetByIdAsync(Guid id);
    Task<AsientoContable> CreateAsync(CreateAsientoContableDto dto);
    Task ValidarEquilibrioAsync(CreateAsientoContableDto dto);
}
```

### Paso 5: Crear Controladores

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

    [HttpGet("{id}")]
    public async Task<ActionResult<CuentaContable>> GetById(Guid id)
    {
        var cuenta = await _service.GetByIdAsync(id);
        if (cuenta == null)
            return NotFound();
        return Ok(cuenta);
    }

    [HttpPost]
    public async Task<ActionResult<CuentaContable>> Create(CreateCuentaContableDto dto)
    {
        var cuenta = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = cuenta.Id }, cuenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCuentaContableDto dto)
    {
        var cuenta = await _service.UpdateAsync(id, dto);
        return Ok(cuenta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class AsientosContablesController : ControllerBase
{
    private readonly IAsientoContableService _service;

    [HttpGet]
    public async Task<ActionResult<PagedResult<AsientoContable>>> GetAsientos(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? descripcion = null,
        [FromQuery] string? tipoReferencia = null,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null)
    {
        var result = await _service.GetAsientosAsync(pageNumber, pageSize,
            descripcion, tipoReferencia, fechaInicio, fechaFin);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AsientoContable>> GetById(Guid id)
    {
        var asiento = await _service.GetByIdAsync(id);
        if (asiento == null)
            return NotFound();
        return Ok(asiento);
    }

    [HttpPost]
    public async Task<ActionResult<AsientoContable>> Create(CreateAsientoContableDto dto)
    {
        await _service.ValidarEquilibrioAsync(dto);
        var asiento = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = asiento.Id }, asiento);
    }
}
```

### Paso 6: DTOs

```csharp
public class CreateCuentaContableDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public bool EsActivo { get; set; } = true;
    public bool EsDeMovimiento { get; set; } = true;
    public Guid? CuentaPadreId { get; set; }
}

public class CreateAsientoContableDto
{
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public Guid ReferenciaId { get; set; }
    public string TipoReferencia { get; set; } = string.Empty;
    public List<CreateMovimientoContableDto> Movimientos { get; set; } = new();
}

public class CreateMovimientoContableDto
{
    public Guid CuentaContableId { get; set; }
    public decimal Debe { get; set; }
    public decimal Haber { get; set; }
}
```

---

## 🧪 Pruebas

### Prueba 1: Verificar Rutas
```bash
# En la consola del navegador, prueba navegar
router.push('/CuentasContables')
router.push('/AsientosContables')
router.push('/ReporteContable')
```

### Prueba 2: Crear Cuenta Contable
```bash
POST http://localhost:5000/api/CuentasContables
Content-Type: application/json

{
  "codigo": "1.1",
  "nombre": "Caja General",
  "esActivo": true,
  "esDeMovimiento": true,
  "cuentaPadreId": null
}
```

### Prueba 3: Crear Asiento
```bash
POST http://localhost:5000/api/AsientosContables
Content-Type: application/json

{
  "fecha": "2025-01-15T10:30:00Z",
  "descripcion": "Venta test",
  "referenciaId": "550e8400-e29b-41d4-a716-446655440000",
  "tipoReferencia": "Venta",
  "movimientos": [
    {
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440001",
      "debe": 1000,
      "haber": 0
    },
    {
      "cuentaContableId": "550e8400-e29b-41d4-a716-446655440002",
      "debe": 0,
      "haber": 1000
    }
  ]
}
```

### Prueba 4: Verificar en Frontend
1. Navega a `/NomenclatorsCard`
2. Deberías ver la tarjeta "Contabilidad"
3. Haz clic en "Cuentas" para ver la gestión de cuentas
4. Haz clic en "Asientos" para ver los asientos creados

---

## 🔧 Solución de Problemas

### Problema: "404 - Ruta no encontrada"
**Solución:** Verifica que las rutas estén correctamente añadidas en `routes.js`

### Problema: "ERROR: API endpoint not found"
**Solución:**
- Verifica que los controladores estén creados
- Asegúrate de que el backend esté ejecutándose
- Verifica que la URL base en `contabilidadAPI.js` es correcta

### Problema: "Cannot read property 'items' of undefined"
**Solución:** Verifica que el backend retorna la estructura correcta:
```json
{
  "items": [],
  "totalCount": 0,
  "pageNumber": 1,
  "pageSize": 10
}
```

### Problema: "El asiento no está equilibrado"
**Solución:** La suma del debe debe ser exactamente igual a la suma del haber

### Problema: "No se puede eliminar la cuenta porque tiene movimientos"
**Solución:** Primero debe eliminar o reasignar todos los movimientos de esa cuenta

### Problema: La tarjeta de contabilidad no aparece
**Solución:**
- Verifica que el archivo linkCards.js fue actualizado correctamente
- Recarga la página (Ctrl + F5)
- Verifica la consola para errores

### Problema: Los datos no se cargan
**Solución:**
- Abre la consola de desarrollador (F12)
- Verifica la pestaña "Network" para ver qué errores hay en las peticiones
- Verifica CORS si el backend está en otro puerto

---

## 📊 Checklist de Implementación

- [ ] Archivos frontend creados en `src/pages/Nomenclators/Contabilidad/`
- [ ] Rutas actualizadas en `src/router/routes.js`
- [ ] Tarjeta de contabilidad en `linkCards.js`
- [ ] Servicios API creados (`contabilidadAPI.js`)
- [ ] Store Vuex configurado (`contabilidadStore.js`)
- [ ] Entidades creadas en el backend
- [ ] DbContext actualizado
- [ ] Migraciones ejecutadas
- [ ] Servicios backend implementados
- [ ] Controladores creados
- [ ] DTOs creados
- [ ] Pruebas en Postman/Insomnia exitosas
- [ ] Frontend cargando datos correctamente

---

## 📞 Soporte Rápido

Si encuentras problemas:

1. Verifica los logs del backend
2. Abre la consola del navegador (F12)
3. Usa las herramientas de desarrollo de Network
4. Revisa el archivo `INTEGRACION_BACKEND.md` para la estructura de endpoints

---

## 🎉 ¡Listo!

Si llegaste hasta aquí, tu módulo de contabilidad está completamente instalado y configurado. Ahora puedes:

✅ Crear y gestionar cuentas contables
✅ Registrar asientos contables
✅ Visualizar reportes
✅ Validar equilibrio de asientos
✅ Exportar datos (próximamente)

¡Que disfrutes usando el módulo de contabilidad! 🚀
