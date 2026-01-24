# ✅ VERIFICACIÓN RÁPIDA: Antes de Implementar

Usa este checklist antes de comenzar a implementar en el backend.

---

## ✅ Frontend - Verificar que está Listo

### 1. Archivo: ConfirmarPedido.vue
```vue
✅ Función notificarNuevoPedido existe
✅ Se llama en confirmOrder() después de éxito
✅ Envía datos con PascalCase
✅ Dispara evento 'nuevoPedidoConfirmado'
```
**Ubicación:** `src/pages/Visual/components/ConfirmarPedido.vue` (línea ~823)
**Verifica:** Puedes ver `console.log('✅ Notificación de nuevo pedido enviada')`

### 2. Archivo: signalRService.js
```javascript
✅ Método setupFrontendListeners() existe
✅ Escucha evento 'nuevoPedidoConfirmado'
✅ Invoca connection.invoke('NotificarNuevoPedido', datos)
✅ Tiene logging detallado
```
**Ubicación:** `src/services/signalRService.js` (línea ~330+)
**Verifica:** Puedes ver métodos: `notificarNuevoPedido()` y `setupFrontendListeners()`

### 3. Archivo: DiagnosticoNotificaciones.vue
```vue
✅ Componente existe
✅ Muestra estado de conexión
✅ Permite enviar notificación de prueba
✅ Muestra notificaciones recibidas
```
**Ubicación:** `src/pages/Test/DiagnosticoNotificaciones.vue`
**Verifica:** Puedes abrirlo en `/admin/DiagnosticoNotificaciones`

### 4. Archivo: routes.js
```javascript
✅ Ruta /admin/DiagnosticoNotificaciones existe
✅ Importa DiagnosticoNotificaciones.vue
✅ Está en el router
```
**Ubicación:** `src/router/routes.js`
**Verifica:** Busca "DiagnosticoNotificaciones"

---

## ❌ Backend - Verificar que Falta

### 1. PedidosHub.cs
```
Estado: ❌ NO EXISTE
Dónde debería estar: Hubs/PedidosHub.cs
Qué contiene: Clase hub con método NotificarNuevoPedido()
```

### 2. NotificacionPedidoDto.cs
```
Estado: ❌ NO EXISTE
Dónde debería estar: DTOs/NotificacionPedidoDto.cs
Qué contiene: Modelos de datos de notificación
```

### 3. Program.cs
```
Estado: ⚠️ NO CONFIGURADO
Dónde: En tu archivo Program.cs
Cambios necesarios:
  - Agregar: builder.Services.AddSignalR()
  - Agregar: builder.Services.AddCors()
  - Agregar: app.UseCors("SignalRPolicy")
  - Agregar: app.MapHub<PedidosHub>("/pedidosHub")
```

---

## 🧪 Test: ¿Funciona el Frontend?

### Abre el navegador en modo de desarrollador (F12)

1. **Ir a:** `https://localhost:3000/admin/DiagnosticoNotificaciones`

2. **Verificar en consola:**
   ```
   ✅ Conectado exitosamente a SignalR
   ```

3. **Si ves esa línea:** Frontend está OK ✅

4. **Si NO la ves:**
   - Busca error en consola
   - Verifica que el frontend está corriendo
   - Verifica que tienes token de autenticación

---

## 📊 Tabla de Verificación

| Item | Status | Dónde | Qué Hacer |
|------|--------|-------|-----------|
| ConfirmarPedido.vue | ✅ Listo | src/pages/Visual/components/ | Nada |
| signalRService.js | ✅ Listo | src/services/ | Nada |
| DiagnosticoNotificaciones.vue | ✅ Listo | src/pages/Test/ | Nada |
| routes.js | ✅ Listo | src/router/ | Nada |
| PedidosHub.cs | ❌ Falta | Hubs/ | Crear |
| NotificacionPedidoDto.cs | ❌ Falta | DTOs/ | Crear |
| Program.cs | ⚠️ Editar | Raíz | Editar |

---

## 🚀 Ahora Que Todo Está Verificado

### Paso 1: Frontend está listo ✅
Pasa a Paso 2

### Paso 2: Implementa Backend (15 min)
Lee: **COPIAR_PEGAR_BACKEND.md**

### Paso 3: Verifica que funciona
Abre: `/admin/DiagnosticoNotificaciones`
Espera: `✅ Conectado`

---

## ⚠️ Checklist de Dependencias

Asegúrate que tienes en tu backend:

- [ ] NuGet package: `Microsoft.AspNetCore.SignalR`
- [ ] Autenticación JWT configurada
- [ ] CORS configurado (o será necesario agregar)

Si no tienes SignalR:
```bash
dotnet add package Microsoft.AspNetCore.SignalR
```

---

## 🎯 Resumen de Verificación

```
FRONTEND:  ✅✅✅✅ COMPLETAMENTE LISTO
BACKEND:   ❌❌❌ NECESITA 3 ARCHIVOS
DOCS:      ✅✅✅✅ MÁS QUE COMPLETA

ACCIÓN: Implementa backend usando COPIAR_PEGAR_BACKEND.md
TIEMPO: ~15 minutos
```

---

**¿Listo para implementar?**
Sí → Abre: `COPIAR_PEGAR_BACKEND.md`

---

Última actualización: Enero 23, 2026
