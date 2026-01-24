# Checklist de Implementación - Notificaciones en Tiempo Real

## ✅ Frontend (Completado)

### ConfirmarPedido.vue
- [x] Importación de `signalRService`
- [x] Conexión a SignalR en `onMounted()`
- [x] Función `notificarNuevoPedido()` implementada
- [x] Llamada a `notificarNuevoPedido()` en `confirmOrder()`
- [x] Construcción correcta de datos de notificación
- [x] Disparo de evento personalizado `nuevoPedidoConfirmado`

### signalRService.js
- [x] Método `setupFrontendListeners()` implementado
- [x] Método `notificarNuevoPedido()` implementado
- [x] Integración de `setupFrontendListeners()` en `connect()`
- [x] Escucha de evento `nuevoPedidoConfirmado`
- [x] Invocación de `NotificarNuevoPedido` en el hub
- [x] Manejo correcto de errores
- [x] Logs apropiados para debugging

## ⏳ Backend (TODO)

### PedidosHub.cs
- [ ] Crear clase `PedidosHub` que hereda de `Hub`
- [ ] Implementar `OnConnectedAsync()` para agregar usuarios a grupos por rol
- [ ] Implementar `NotificarNuevoPedido(NotificacionPedidoDto)` como método público
- [ ] Autorizar acceso solo a clientes conectados
- [ ] Enviar notificación a grupos: "Admin", "Administrador", "Vendedor"
- [ ] Implementar `OnDisconnectedAsync()` para logging
- [ ] Agregar método `Ping()` para pruebas de conexión

### Program.cs / Startup.cs
- [ ] `services.AddSignalR()`
- [ ] `endpoints.MapHub<PedidosHub>("/pedidosHub")`
- [ ] Configurar CORS si es necesario
- [ ] Configurar timeouts y keep-alive
- [ ] Usar AuthenticationScheme: Bearer (JWT)

### DTO Models
- [ ] `NotificacionPedidoDto`
  - [ ] `PedidoId: string`
  - [ ] `Codigo: string`
  - [ ] `Total: decimal`
  - [ ] `Cliente: string`
  - [ ] `CantidadProductos: int`
  - [ ] `Estado: string`
  - [ ] `Fecha: DateTime`
  - [ ] `Productos: List<ProductoNotificacionDto>`
- [ ] `ProductoNotificacionDto`
  - [ ] `Nombre: string`
  - [ ] `Cantidad: int`
  - [ ] `Precio: decimal`

### Controlador de Pedidos (Opcional)
- [ ] Usar `IHubContext<PedidosHub>` en inyección de dependencias
- [ ] Llamar a `_hubContext.Clients.Groups("Admin", "Administrador", "Vendedor").SendAsync("PedidoGenerado", notificacion)` al crear pedido
- [ ] Implementar notificaciones para cambios de estado de pedido

## 🔐 Seguridad

### JWT Token
- [x] Token contiene `role` claim
- [x] Rol es uno de: "Admin", "Administrador", "Vendedor", "Cliente"
- [ ] Token se envía en Authorization header: `Bearer {token}`
- [x] Token se extrae correctamente en frontend

### Autorización
- [x] Solo admin y vendedores reciben notificaciones
- [x] Clientes pueden enviar notificaciones pero no recibirlas
- [ ] Backend valida que la notificación venga de un cliente autenticado

## 🧪 Testing

### Pruebas Frontend
- [ ] Abrrir navegador como Admin/Vendedor
- [ ] Ver `✅ Conectado a SignalR` en consola
- [ ] Abrir en otra pestaña como Cliente
- [ ] Confirmar pedido como Cliente
- [ ] Verificar que Admin/Vendedor recibe notificación
- [ ] Verificar mensaje contiene: Código, Total, Cliente, Productos
- [ ] Probar reconexión (desconectar internet)
- [ ] Probar timeout (30 segundos sin actividad)

### Pruebas Backend
- [ ] Hub recibe `NotificarNuevoPedido` correctamente
- [ ] Hub envía a grupos correctos
- [ ] Logs muestran actividad
- [ ] Errores se capturan y registran

### Casos Borde
- [ ] Cliente crea pedido sin Admin conectado
- [ ] Admin se conecta después de pedido
- [ ] Múltiples pedidos en rápida sucesión
- [ ] Desconexión y reconexión de Admin
- [ ] Token expirado

## 📊 Monitoreo

### Logs a Verificar

**Frontend (Consola - F12)**
```
✅ Conectado a SignalR
📢 Evento de nuevo pedido confirmado recibido: {...}
✅ Notificación de nuevo pedido enviada al hub
📦 Nuevo pedido recibido
🛒 ¡Nuevo Pedido! #12345
```

**Backend (Output)**
```
Usuario {userId} con rol {role} conectado
Usuario agregado al grupo: {role}
Notificación de nuevo pedido recibida
Notificación enviada a Administradores y Vendedores
```

## 🔗 Configuración de URLs

### Local Development
```
Frontend: https://localhost:3000 (Vite)
Backend API: https://localhost:6005 (ASP.NET)
SignalR Hub: https://localhost:6005/pedidosHub
```

### Producción
```
Frontend: https://yourdomain.com
Backend API: https://api.yourdomain.com
SignalR Hub: https://api.yourdomain.com/pedidosHub
```

⚠️ **IMPORTANTE**: Cambiar URL en `signalRService.js` para producción

## 📝 Documentación

- [x] NOTIFICACIONES_TIEMPO_REAL.md - Documentación completa
- [x] EJEMPLO_BACKEND_SIGNALR.cs - Ejemplo de implementación
- [x] RESUMEN_NOTIFICACIONES.md - Resumen rápido
- [x] Este archivo - Checklist de implementación

## 🚀 Deployment

### Pre-requisitos
- [ ] Backend compilado y desplegado
- [ ] Base de datos migrations ejecutadas
- [ ] HTTPS configurado
- [ ] CORS configurado correctamente
- [ ] JWT signing key configurada

### Deploy Steps
1. [ ] Compilar frontend: `npm run build`
2. [ ] Desplegar a CDN o servidor estático
3. [ ] Verificar que las URLs de API y SignalR son correctas
4. [ ] Probar conexión a SignalR
5. [ ] Monitorear logs de backend
6. [ ] Verificar que las notificaciones llegan

## 🆘 Troubleshooting

| Problema | Solución | Archivo |
|----------|----------|---------|
| No se conecta a SignalR | Verificar URL en signalRService.js | signalRService.js:164 |
| No recibe notificaciones | Verificar rol en token | signalRService.js:295 |
| Pedido no se guarda | Verificar backend API | - |
| Reconexión lenta | Ajustar timeouts en backend | Program.cs |
| CORS error | Configurar CORS en backend | Program.cs |

## 📞 Contacto / Soporte

Documentación adicional:
- `NOTIFICACIONES_TIEMPO_REAL.md` - Guía completa
- `EJEMPLO_BACKEND_SIGNALR.cs` - Código de referencia

## 📋 Notas

- El sistema usa **WebSockets** como transporte principal
- Fallback automático a **Long Polling** si WebSockets no está disponible
- Las notificaciones se envían **solo a conectados**, no se persisten
- Si necesitas persistencia, agregar tabla `Notificaciones` en BD
- Si necesitas historial, agregar tabla `HistorialPedidos` en BD

## ✨ Próximas Mejoras

1. **Persistencia**: Guardar notificaciones no leídas en BD
2. **Historial**: Página de historial de notificaciones
3. **Sound**: Agregar sonido cuando llega notificación
4. **Desktop Notifications**: Usar Notification API
5. **Email**: Enviar email además de notificación en tiempo real
6. **SMS**: Agregar notificaciones por SMS a vendedores
7. **Analytics**: Registrar qué notificaciones fueron vistas

---

**Estado**: Implementación Completada ✅ (Frontend)
**Próximo**: Implementar Backend (PedidosHub.cs)
**Fecha Inicio**: Enero 23, 2026
**Última Actualización**: Enero 23, 2026
