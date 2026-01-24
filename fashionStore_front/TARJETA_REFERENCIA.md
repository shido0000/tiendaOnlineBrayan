# ⚡ TARJETA DE REFERENCIA RÁPIDA

## El Problema
```
❌ Error: HubException: Method does not exist
```
Backend NO tiene el método `NotificarNuevoPedido`

---

## La Solución (15 minutos)

### 1. Crear PedidosHub.cs
```
Ruta: Hubs/PedidosHub.cs
Referencia: COPIAR_PEGAR_BACKEND.md (Sección 1)
Tiempo: 2 minutos
```

### 2. Crear NotificacionPedidoDto.cs
```
Ruta: DTOs/NotificacionPedidoDto.cs
Referencia: COPIAR_PEGAR_BACKEND.md (Sección 2)
Tiempo: 2 minutos
```

### 3. Editar Program.cs
```
Agregar: AddSignalR()
Agregar: AddCors()
Agregar: UseCors()
Agregar: MapHub<PedidosHub>()
Referencia: COPIAR_PEGAR_BACKEND.md (Secciones 3-4)
Tiempo: 5 minutos
```

### 4. Ejecutar
```bash
dotnet build
dotnet run
```
Tiempo: 2 minutos

### 5. Verificar
```
URL: https://localhost:3000/admin/DiagnosticoNotificaciones
Esperado: ✅ Conectado
Tiempo: 1 minuto
```

---

## Documentación por Necesidad

| Necesidad | Archivo | Tiempo |
|-----------|---------|--------|
| Entender problema | ESTADO_ACTUAL.md | 2 min |
| Solución rápida | COPIAR_PEGAR_BACKEND.md | 10 min |
| Debugging | DIAGNOSTICO_RAPIDO.md | 5 min |
| Errores | PROBLEMAS_COMUNES.md | 10-20 min |
| Entender flujo | ARQUITECTURA_NOTIFICACIONES.md | 15 min |
| Todo | NOTIFICACIONES_TIEMPO_REAL.md | 30 min |

---

## Checklist Mínimo

- [ ] Creé `Hubs/PedidosHub.cs`
- [ ] Creé `DTOs/NotificacionPedidoDto.cs`
- [ ] Edité `Program.cs` (AddSignalR + MapHub)
- [ ] Ejecuté `dotnet build`
- [ ] Ejecuté `dotnet run`
- [ ] Abrí `/admin/DiagnosticoNotificaciones`
- [ ] Veo `✅ Conectado`

---

## Si Algo Sale Mal

### Error: "Method does not exist"
→ Verifica que creaste `PedidosHub.cs` con el método exacto

### Error: "No se puede conectar"
→ Verifica que aggregaste `MapHub` en Program.cs

### Diagnóstico muestra desconectado
→ Verifica que backend está ejecutando en puerto 6005

### No recibe notificaciones pero sí conecta
→ Lee: PROBLEMAS_COMUNES.md

---

## URLs Importantes

| Propósito | URL |
|-----------|-----|
| Diagnóstico | `https://localhost:3000/admin/DiagnosticoNotificaciones` |
| Crear pedido | `https://localhost:3000/carrito` |
| Frontend | `https://localhost:3000` |
| Backend | `https://localhost:6005` |

---

## Comandos Rápidos

```bash
# Compilar backend
dotnet build

# Ejecutar backend
dotnet run

# Si quieres limpiar antes
dotnet clean
dotnet build
dotnet run
```

---

## Propiedades del DTO (Asegúrate que coincidan)

Frontend envía (PascalCase):
```javascript
{
  PedidoId: "...",
  Codigo: "#12345",
  Total: 199.99,
  Cliente: "Juan",
  CantidadProductos: 1,
  Estado: "Pendiente",
  Fecha: "2026-01-23T...",
  Productos: [...]
}
```

Backend espera (PascalCase):
```csharp
public class NotificacionPedidoDto
{
    public string PedidoId { get; set; }
    public string Codigo { get; set; }
    public decimal Total { get; set; }
    public string Cliente { get; set; }
    public int CantidadProductos { get; set; }
    public string Estado { get; set; }
    public DateTime Fecha { get; set; }
    public List<ProductoNotificacionDto> Productos { get; set; }
}
```

✅ Coinciden → Funciona

---

## Logs a Buscar

### En el Backend (Console)
```
✅ Usuario {id} conectado. Rol: Administrador
✅ Usuario agregado al grupo: Administrador
📦 Notificación recibida: Pedido #12345
✅ Notificación enviada a Admin/Vendedor
```

### En el Frontend (F12 Console)
```
✅ Conectado exitosamente a SignalR
📢 Evento de nuevo pedido confirmado recibido
📤 Intentando enviar notificación al hub
🔄 Invocando método NotificarNuevoPedido en el hub
✅ Notificación de nuevo pedido enviada al hub correctamente
📦 Nuevo pedido recibido
```

---

## Estado Actual

```
✅ Frontend: Completamente implementado
❌ Backend: Falta PedidosHub.cs + NotificacionPedidoDto.cs
⏱️ Tiempo: ~15 minutos para solucionar
🎓 Dificultad: ⭐ Muy fácil (copy-paste)
```

---

## Video de Pasos (Texto)

```
1. Copia el código de PedidosHub.cs (1 min)
   └─ Pégalo en Hubs/PedidosHub.cs

2. Copia el código de NotificacionPedidoDto.cs (1 min)
   └─ Pégalo en DTOs/NotificacionPedidoDto.cs

3. Abre Program.cs y agrega AddSignalR() (2 min)
   └─ ANTES de "var app = builder.Build()"

4. Abre Program.cs y agrega MapHub (2 min)
   └─ DESPUÉS de "var app = builder.Build()"

5. Terminal: "dotnet build" (2 min)
   └─ Espera que compile sin errores

6. Terminal: "dotnet run" (1 min)
   └─ Espera que inicie el servidor

7. Browser: Abre /admin/DiagnosticoNotificaciones (1 min)
   └─ Verifica que dice ✅ Conectado

8. Click en "Enviar Notificación de Prueba" (1 min)
   └─ Debe aparecer en la lista

¡Listo! Sistema funcionando ✅
```

---

## Si Necesitas Ayuda

| Problema | Solución |
|----------|----------|
| No entiendo qué hacer | Lee: ESTADO_ACTUAL.md |
| Quiero copiar-pegar código | Lee: COPIAR_PEGAR_BACKEND.md |
| Tengo error específico | Busca en: PROBLEMAS_COMUNES.md |
| Quiero debuguear | Lee: DIAGNOSTICO_RAPIDO.md |
| Quiero entender cómo funciona | Lee: ARQUITECTURA_NOTIFICACIONES.md |

---

## Tiempo Total
⏱️ **15 minutos máximo**

- Leer: 2 min
- Crear archivos: 4 min
- Editar Program.cs: 5 min
- Compilar/ejecutar: 2 min
- Verificar: 2 min

---

**¡Estás listo! A implementar! 🚀**

Última actualización: Enero 23, 2026
