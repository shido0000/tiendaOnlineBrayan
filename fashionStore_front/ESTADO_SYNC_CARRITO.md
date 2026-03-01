---
# 🎯 ESTADO ACTUAL - Sincronización Periódica de Carrito

## ✅ IMPLEMENTADO EN EL FRONTEND

### 1. Service Principal
**Archivo:** `src/services/periodicCartSyncService.js`
- ✅ Lee localStorage cada 30 segundos automáticamente
- ✅ Extrae usuarioId del JWT token
- ✅ Transforma datos al formato de backend
- ✅ Envía POST a `/EnviarDatosAlCarrito`
- ✅ Maneja errores silenciosamente
- ✅ Mantiene estadísticas (syncCount, errorCount, etc)

**Métodos disponibles:**
```javascript
// Auto-inicia sincronización periódica
periodicCartSyncService.start(30000) // Cada 30 segundos

// Detiene la sincronización
periodicCartSyncService.stop()

// Ejecuta una sincronización manual inmediata
await periodicCartSyncService.syncCartFromStorage()

// Obtiene estado actual
periodicCartSyncService.getStatus()
// Returns: { isRunning, lastSyncTime, syncCount, errorCount, successRate }

// Reinicia desde cero
periodicCartSyncService.restart()
```

### 2. Autoinicio Automático
**Archivo:** `src/boot/periodicCartSync.js`
- ✅ Detecta autenticación al cargar la app
- ✅ Inicia servicio automáticamente al login
- ✅ Detiene servicio automáticamente al logout
- ✅ Persiste sincronización en recargas (F5)
- ✅ Expone `window.__periodicCartSyncService` para debug

### 3. Integración en Quasar
**Archivo:** `quasar.config.js`
- ✅ Boot file registrado en array de boots
- ✅ Se carga automáticamente al iniciar la app

### 4. Documentación
**Archivos:**
- ✅ `SYNC_CARRITO_PERIODICO.md` - Documentación completa
- ✅ `ANALISIS_SYNC_CARRITO_PERIODICO.js` - Análisis detallado
- ✅ `CHECKLIST_IMPLEMENTACION_FINAL.md` - Validación

---

## ⏳ PENDIENTE EN EL BACKEND

### 1. Endpoint Principal
**Necesario implementar:**
```
POST /EnviarDatosAlCarrito
```

**Recibe (JSON):**
```json
{
  "usuarioId": "550e8400-e29b-41d4-a716-446655440001",
  "fechaCreacion": "2026-02-21T14:23:45.123Z",
  "detalles": [
    {
      "carritoId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
      "productoId": "44018c27-6c79-4b6e-80e9-b6a7a201a93c",
      "cantidad": 1,
      "unitPrice": 20.00,
      "lineTotal": 20.00
    }
  ]
}
```

**Debe hacer:**
- Validar usuarioId y token
- Guardar/actualizar carrito en BD
- Registrar sincronización (marca de tiempo)
- Emitir evento SignalR `CarritoActualizado` a admins/vendedores
- Retornar respuesta 200 con éxito

**Respuesta esperada:**
```json
{
  "success": true,
  "message": "Carrito sincronizado"
}
```

### 2. Evento SignalR (Opcional pero recomendado)
**Nombre:** `CarritoActualizado`
**Listeners:** Admin, Vendedor
**Datos a emitir:** Estado del carrito actualizado
```javascript
await Clients.Groups("Admins", "Vendedores")
  .SendAsync("CarritoActualizado",
    new {
      usuarioId,
      totalItems = detalles.Count,
      totalValue = detalles.Sum(d => d.lineTotal),
      timestamp = DateTime.UtcNow
    })
```

---

## 🔄 CÓMO FUNCIONA EL FLUJO COMPLETO

```
┌─────────────────────────────────────────────────────────────┐
│ 1. USUARIO ABRE LA APP                                      │
├─────────────────────────────────────────────────────────────┤
│ ✅ Frontend: Boot file detecta token JWT                    │
│ ✅ Frontend: Inicia periodicCartSyncService.start()         │
└─────────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────┐
│ 2. CADA 30 SEGUNDOS (AUTOMÁTICO)                            │
├─────────────────────────────────────────────────────────────┤
│ ✅ Frontend: Lee localStorage["fashion_cart_v1"]            │
│ ✅ Frontend: Extrae usuarioId del JWT                       │
│ ✅ Frontend: Formatea estructura                            │
│ ✅ Frontend: POST /EnviarDatosAlCarrito                     │
└─────────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────┐
│ 3. BACKEND RECIBE POST                                      │
├─────────────────────────────────────────────────────────────┤
│ 🔄 Backend: Validar datos                                   │
│ 🔄 Backend: Guardar en BD                                   │
│ 🔄 Backend: Emitir SignalR event                            │
│ 🔄 Backend: Retornar 200 OK                                 │
└─────────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────┐
│ 4. FRONTEND RECIBE RESPUESTA                                │
├─────────────────────────────────────────────────────────────┤
│ ✅ Frontend: Actualiza estadísticas internas                │
│ ✅ Frontend: Espera 30 segundos                             │
│ ✅ Frontend: Repite desde paso 2                            │
└─────────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────┐
│ 5. ADMIN/VENDEDOR MONITOREA EN TIEMPO REAL                  │
├─────────────────────────────────────────────────────────────┤
│ 🔄 Backend: Emite SignalR "CarritoActualizado"              │
│ ✅ Frontend: Recibe evento                                  │
│ ✅ Frontend: Actualiza ActiveCartsMonitor en tiempo real    │
└─────────────────────────────────────────────────────────────┘
```

---

## 📊 EXAMPLE C# BACKEND (ASP.NET Core)

```csharp
[HttpPost("EnviarDatosAlCarrito")]
[Authorize]
public async Task<IActionResult> EnviarDatosAlCarrito(
    [FromBody] CartSyncRequest request)
{
    try
    {
        // 1. Validar datos
        if (string.IsNullOrEmpty(request.UsuarioId) ||
            request.Detalles == null || request.Detalles.Count == 0)
        {
            return BadRequest(new { success = false, message = "Datos inválidos" });
        }

        // 2. Obtener usuario autenticado
        var usuarioAutenticado = User.FindFirst("userId")?.Value;
        if (usuarioAutenticado != request.UsuarioId)
        {
            return Unauthorized();
        }

        // 3. Guardar/actualizar carrito en BD
        var carrito = new Carrito
        {
            UsuarioId = request.UsuarioId,
            FechaCreacion = request.FechaCreacion,
            FechaActualizacion = DateTime.UtcNow
        };

        foreach (var detalle in request.Detalles)
        {
            carrito.Detalles.Add(new CarritoDetalle
            {
                CarritoId = detalle.CarritoId,
                ProductoId = detalle.ProductoId,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.UnitPrice,
                Total = detalle.LineTotal
            });
        }

        await _context.Carritos.AddOrUpdateAsync(carrito);
        await _context.SaveChangesAsync();

        // 4. Emitir evento SignalR a admins/vendedores
        var totalValue = request.Detalles.Sum(d => d.LineTotal);
        await _hubContext.Clients.Groups("Admins", "Vendedores")
            .SendAsync("CarritoActualizado", new
            {
                usuarioId = request.UsuarioId,
                totalItems = request.Detalles.Count,
                totalValue = totalValue,
                timestamp = DateTime.UtcNow
            });

        // 5. Retornar respuesta exitosa
        return Ok(new
        {
            success = true,
            message = "Carrito sincronizado"
        });
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error en EnviarDatosAlCarrito: {ex.Message}");
        return StatusCode(500, new { success = false, message = "Error del servidor" });
    }
}
```

---

## 🧪 CÓMO PROBAR DESDE FRONTEND

### 1. Verificar que está sincronizando
```javascript
// En consola del navegador:
window.__periodicCartSyncService.getStatus()

// Salida esperada:
{
  isRunning: true,
  lastSyncTime: 2026-02-21T14:23:45.123Z,
  syncCount: 42,
  errorCount: 0,
  successRate: 1.0
}
```

### 2. Forzar sincronización manual
```javascript
// Sincroniza inmediatamente (no espera 30 seg)
await window.__periodicCartSyncService.syncCartFromStorage()
```

### 3. Verificar localStorage
```javascript
// Ver qué datos se están leyendo:
JSON.parse(localStorage.getItem('fashion_cart_v1'))
```

### 4. Monitorear requests en Network
```
Abre DevTools → Network → Filtra por "EnviarDatosAlCarrito"
Deberías ver requests cada 30 segundos si hay carrito
```

---

## 🔍 CHECKLIST FINAL

### ✅ Frontend (100% Completado)
- [x] Lectura de localStorage periódica
- [x] Transformación de estructura
- [x] Extracción de usuarioId del JWT
- [x] POST a /EnviarDatosAlCarrito
- [x] Manejo de errores
- [x] Autoinicio automático
- [x] Estadísticas internas
- [x] Debug tools

### ⏳ Backend (Pendiente)
- [ ] Endpoint POST /EnviarDatosAlCarrito
- [ ] Validación de datos
- [ ] Almacenamiento en BD
- [ ] Emisión de evento SignalR
- [ ] Respuesta con estado 200
- [ ] Logging de sincronizaciones

### ⏳ Integración
- [ ] ActiveCartsMonitor mostrando carritos reales
- [ ] SignalR actualizando monitor en tiempo real
- [ ] Testing end-to-end

---

## 📞 PRÓXIMOS PASOS

1. **Backend:** Implementar `/EnviarDatosAlCarrito` endpoint
2. **Backend:** Emitir evento SignalR `CarritoActualizado`
3. **Testing:** Verificar que datos llegan correctos a BD
4. **Frontend:** Validar que monitor muestra carts en tiempo real
5. **Optimización:** Ajustar intervalo según carga del servidor

---

## 📋 REFERENCIA RÁPIDA

| Concepto | Frontend | Backend |
|----------|----------|---------|
| Lectura localStorage | ✅ Hecho | - |
| Transformación datos | ✅ Hecho | - |
| POST al endpoint | ✅ Hecho | ⏳ Falta |
| Guardar en BD | - | ⏳ Falta |
| Emitir SignalR | - | ⏳ Falta |
| Monitoreo real-time | ✅ Listo | ⏳ Falta |
| Periodicidad automática | ✅ Hecho | - |
| Manejo de errores | ✅ Hecho | ⏳ Falta |

---

**Estado General:** 🟢 Frontend 100% Listo | 🟡 Backend 0% Listo | 🔴 Integración No Validada

**Impedimento Actual:** Esperando backend para completar flujo

