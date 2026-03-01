# 🚀 Quick Start - Monitor de Carritos Activos

## En 3 Pasos

### 1. Inicia la aplicación como Admin o Vendedor
```bash
npm run dev
# Ve a http://localhost:xxxx/login
# Accede con cuenta que tenga rol: admin, administrador o vendedor
```

### 2. Accede al Dashboard
```
1. Haz clic en "Dashboard"
2. Baja scroll hasta el final
3. Verás: "Usuarios con Carrito Activo"
```

### 3. Prueba con Datos Mock (sin backend)
```
1. Ve a: http://localhost:xxxx/TestActiveCartsMonitor
2. Haz clic en "Cargar Datos Mock"
3. ¡Verá lista de usuarios con carritos!
4. (Opcional) Inicia simulación para ver cambios en tiempo real
```

---

## 📁 Archivos Principales

| Archivo | Propósito |
|---------|-----------|
| `src/components/ActiveCartsMonitor.vue` | Componente principal (muestra usuarios) |
| `src/services/activeCartsService.js` | Servicio de datos |
| `src/pages/DashboardPage.vue` | Dashboard (integración) |
| `src/pages/Test/TestActiveCartsMonitor.vue` | Página de testing |
| `IMPLEMENTACION_MONITOR_CARRITOS.md` | Documentación completa para backend |
| `MONITOR_CARRITOS_RESUMEN.md` | Este resumen |

---

## 🎯 Casos de Uso

### Caso 1: Ver usuarios con carritos activos
```
Admin/Vendedor abre Dashboard
→ Ve lista de todos los usuarios que tienen productos en carrito
→ Ve datos del usuario (nombre, email, teléfono)
→ Ve cantidad de items y total en carrito
→ Puede expandir para ver detalles de cada producto
```

### Caso 2: Monitoreo en tiempo real
```
Usuario agrega producto al carrito
→ SignalR emite evento CarritoActualizado
→ Un nuevo usuario aparece en la lista del admin
→ O se actualiza cantidad/total del usuario existente
```

### Caso 3: Carritos abandonados
```
Usuario deja producto en carrito pero no compra
→ Aparece marcado (naranja si >3 items)
→ Admin puede ver "Última actividad"
→ Admin puede contactar usuario
```

---

## ⚠️ Antes de Producción

- [ ] Backend debe implementar endpoints en `IMPLEMENTACION_MONITOR_CARRITOS.md`
- [ ] Backend debe emitir eventos SignalR
- [ ] Probar en un dispositivo real/tablet/mobile
- [ ] Verificar permisos y seguridad

---

## 🧪 Testing Checklist

### Sin Backend (Fake Data)
- [ ] Ir a `/TestActiveCartsMonitor`
- [ ] Cargar datos mock
- [ ] Ver tabla de usuarios
- [ ] Iniciar simulación
- [ ] Ver cambios en tiempo real
- [ ] Expandir detalles de usuario

### Con Backend Parcial
- [ ] Backend OK: `GET /api/usuarios/carritos-activos`
- [ ] Dashboard carga datos
- [ ] Se ven los usuarios con carritos
- [ ] Refresco cada 30 segundos funciona
- [ ] Mobile se ve bien

### Production Ready
- [ ] SignalR implementado
- [ ] Evento `CarritoActualizado` emitido
- [ ] Actualizaciones en tiempo real
- [ ] Permisos correctos
- [ ] Sin errores en consola

---

## 🔗 URLs Útiles

| Funcionalidad | URL |
|--------------|-----|
| Dashboard (Producción) | `/Dashboard` |
| Testing | `/TestActiveCartsMonitor` |
| API Docs | `IMPLEMENTACION_MONITOR_CARRITOS.md` |
| Datos Mock | `src/assets/js/util/mockActiveCartsData.js` |

---

## 🛠️ Stack Técnico

- **Frontend Framework:** Vue 3 (Composition API)
- **UI Component Library:** Quasar
- **Real-time:** SignalR
- **HTTP:** Axios
- **State:** Reactive (Vue 3)
- **Styling:** SCSS

---

## 📞 Soporte

### Errores Comunes

**Error: "No hay usuarios con carrito"**
- Normal si no hay datos en base de datos
- Usa `/TestActiveCartsMonitor` para ver demo

**Error: CORS en API**
- Backend debe enviar headers CORS correctos
- Verificar axios.js en boot/

**Error: SignalR no conecta**
- Url correcta: `https://localhost:6005/pedidosHub`
- User debe estar autenticado
- Verificar token en localStorage

---

## 💡 Tips Útiles

1. **Debug Console:** Abre DevTools (F12) para ver logs
2. **Network Tab:** Verifica calls a `/api/usuarios/carritos-activos`
3. **Vue DevTools:** Inspecciona state del componente
4. **Mobile Testing:** Usa Chrome DevTools → Toggle device toolbar

---

## 📊 Métricas Esperadas

**Performance:**
- Carga inicial: < 2s
- Actualización SignalR: < 100ms
- Refresco automático: Cada 30s

**UX:**
- Desktop: 3+ usuarios visibles sin scroll
- Mobile: Expandible/colapsable
- Respuesta a clicks: Inmediata

---

## 🎓 Para Aprender Más

1. Abre `IMPLEMENTACION_MONITOR_CARRITOS.md` - Especificación completa
2. Lee `src/components/ActiveCartsMonitor.vue` - Código bien comentado
3. Revisa `src/services/activeCartsService.js` - Métodos documentados
4. Prueba en `/TestActiveCartsMonitor` - Experimenta

---

## ✅ Checklist de Implementación

### Frontend (Completado ✅)
- [x] Componente ActiveCartsMonitor
- [x] Servicio activeCartsService
- [x] Integración en Dashboard
- [x] Página de testing
- [x] Datos mock
- [x] Documentación
- [x] Rutas configuradas

### Backend (Pendiente ⏳)
- [ ] Endpoint GET /api/usuarios/carritos-activos
- [ ] Endpoint GET /api/usuarios/{usuarioId}/carrito
- [ ] Endpoint GET /api/usuarios/estadisticas-carritos
- [ ] Endpoint GET /api/usuarios/carritos-abandonados
- [ ] Endpoint POST /api/usuarios/{usuarioId}/notificar-carrito
- [ ] Endpoint GET /api/usuarios/reporte-carritos
- [ ] SignalR Hub método NotificarCarritoActualizado
- [ ] Grupo "AdminVendedores" en SignalR
- [ ] Lógica de uso en modificación de carritos

---

**Nota:** Todo el frontend está **100% listo para usar**. Solo necesita los endpoints del backend para funcionar con datos reales.

Próximo paso → Enviar `IMPLEMENTACION_MONITOR_CARRITOS.md` al equipo de backend
