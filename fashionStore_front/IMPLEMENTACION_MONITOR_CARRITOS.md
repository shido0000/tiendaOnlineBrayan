# Implementación de Monitor de Carritos Activos

## Descripción General

Se ha implementado un sistema completo de monitoreo en tiempo real de carritos activos en el frontend para vendedores y administradores. Este documento explica cómo completar la implementación en el backend.

## Componentes Frontend Creados

### 1. **ActiveCartsMonitor.vue**
`src/components/ActiveCartsMonitor.vue`

Componente responsable de:
- Mostrar lista de usuarios con carritos activos
- Mostrar detalles del usuario (nombre, email, teléfono, etc.)
- Mostrar productos en el carrito con cantidad y precios
- Colorear carritos según cantidad de items (>3 items = prioritario)
- Actualización en tiempo real vía SignalR
- Acciones de contacto y ver perfil

### 2. **activeCartsService.js**
`src/services/activeCartsService.js`

Servicio que proporciona:
- `getActiveCarts()` - Obtiene usuarios con carritos activos
- `getUserCart(usuarioId)` - Obtiene carrito específico de usuario
- `watchActiveCarts(callback)` - Observa cambios en tiempo real
- `getCartsStatistics()` - Estadísticas generales de carritos
- `getAbandonedCarts(minutosUmbral)` - Carritos abandonados
- `notifyUserAboutCart()` - Notificar usuarios
- `downloadCartsReport()` - Descargar reporte

### 3. **Integración en DashboardPage.vue**

Se agregó el componente `ActiveCartsMonitor` al dashboard:
- Visible solo para roles: `admin`, `administrador`, `vendedor`
- Se muestra debajo de las gráficas de estadísticas
- Se actualiza automáticamente cada 30 segundos

### 4. **Extensión de signalRService.js**

Se agregó:
- Listener para evento `CarritoActualizado`
- Método `handleCarritoActualizado()` para procesar cambios
- Dispara eventos personalizados en el frontend

## Endpoints Backend Requeridos

### 1. GET `/api/usuarios/carritos-activos`
**Descripción**: Obtiene lista de usuarios autenticados con carritos no vacíos

**Parámetros**:
- No requiere parámetros

**Response** (200 OK):
```json
[
  {
    "usuarioId": "550e8400-e29b-41d4-a716-446655440000",
    "nombre": "Juan",
    "apellido": "Pérez",
    "email": "juan@example.com",
    "telefono": "+34 123 456 789",
    "cartCount": 5,
    "cartTotal": 250.50,
    "cartItems": [
      {
        "id": "1",
        "productoId": "prod-001",
        "nombre": "Camiseta Premium",
        "variante": "Talla L - Color Azul",
        "precio": 50.00,
        "cantidad": 1,
        "foto": "https://example.com/images/camisa.jpg"
      },
      {
        "id": "2",
        "productoId": "prod-002",
        "nombre": "Pantalón Denim",
        "variante": "Taille 32",
        "precio": 80.00,
        "cantidad": 2,
        "foto": "https://example.com/images/pantalon.jpg"
      }
    ],
    "lastActivity": "2024-02-21T10:30:00Z"
  }
]
```

**Permisos**: Requiere autenticación + rol (admin, vendedor)

---

### 2. GET `/api/usuarios/{usuarioId}/carrito`
**Descripción**: Obtiene carrito detallado de un usuario específico

**Parámetros**:
- `usuarioId` (path): UUID del usuario

**Response** (200 OK):
```json
{
  "usuarioId": "550e8400-e29b-41d4-a716-446655440000",
  "items": [
    {
      "id": "cart-item-1",
      "productoId": "prod-001",
      "varianteId": "var-001",
      "nombre": "Camiseta Premium",
      "descripcion": "Camiseta 100% algodón",
      "variante": "Talla L - Color Azul",
      "precio": 50.00,
      "cantidad": 1,
      "foto": "https://example.com/images/camisa.jpg",
      "agregadoEn": "2024-02-21T09:15:00Z"
    }
  ],
  "totalItems": 1,
  "totalValue": 50.00,
  "ultimaActualizacion": "2024-02-21T10:30:00Z"
}
```

---

### 3. GET `/api/usuarios/estadisticas-carritos`
**Descripción**: Obtiene estadísticas globales de carritos activos

**Response** (200 OK):
```json
{
  "totalActiveCarts": 15,
  "totalValue": 3250.75,
  "averageCartValue": 216.72,
  "topProducts": [
    {
      "productoId": "prod-001",
      "nombre": "Camiseta Premium",
      "cantidadEnCarritos": 8,
      "valorTotal": 400.00
    }
  ],
  "usuariosConMasItems": [
    {
      "usuarioId": "550e8400-e29b-41d4-a716-446655440000",
      "nombre": "Juan Pérez",
      "cartCount": 12
    }
  ]
}
```

---

### 4. GET `/api/usuarios/carritos-abandonados`
**Descripción**: Obtiene carritos que llevan tiempo sin actualización

**Parámetros Query**:
- `minutosUmbral` (optional, default: 30): Minutos sin actividad para considerar "abandonado"

**Response** (200 OK):
```json
[
  {
    "usuarioId": "550e8400-e29b-41d4-a716-446655440000",
    "nombre": "Carlos",
    "email": "carlos@example.com",
    "cartCount": 3,
    "cartTotal": 150.00,
    "lastActivity": "2024-02-21T09:00:00Z",
    "minutosSinActividad": 90
  }
]
```

---

### 5. POST `/api/usuarios/{usuarioId}/notificar-carrito`
**Descripción**: Envía notificación a usuario sobre su carrito

**Body**:
```json
{
  "mensaje": "¡Tu carrito está esperando! Completa tu compra y obtén 10% descuento."
}
```

**Response** (200 OK):
```json
{
  "exito": true,
  "mensaje": "Notificación enviada"
}
```

---

### 6. GET `/api/usuarios/reporte-carritos`
**Descripción**: Descarga reporte de carritos activos

**Parámetros Query**:
- `formato`: "csv" o "pdf"

**Response**: Archivo binario

---

## Implementación de SignalR (Backend)

### Evento: `CarritoActualizado`

Se debe emitir este evento cuando:
1. Usuario agrega producto al carrito
2. Usuario quita producto del carrito
3. Usuario cambia cantidad de un producto
4. Usuario vacía el carrito

**Data emitida**:
```json
{
  "usuarioId": "550e8400-e29b-41d4-a716-446655440000",
  "nombre": "Juan",
  "apellido": "Pérez",
  "email": "juan@example.com",
  "cartCount": 5,
  "cartTotal": 250.50,
  "cartItems": [...],
  "lastActivity": "2024-02-21T10:30:00Z"
}
```

**Destinatarios**:
- Solo usuarios con rol `admin` o `vendedor`
- Debe incluir a todos los conectados

**Código C# (ejemplo)**:
```csharp
public class PedidosHub : Hub
{
    public async Task NotificarCarritoActualizado(UsuarioCartViewModel data)
    {
        // Enviar a todos los admin y vendedores conectados
        await Clients
            .Group("AdminVendedores")
            .SendAsync("CarritoActualizado", data);
    }
}
```

---

## Ejemplo: Implementación en el Backend (C#)

```csharp
// Controller
[ApiController]
[Route("api/usuarios")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IHubContext<PedidosHub> _hubContext;
    private readonly ICarritoService _carritoService;
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(
        IHubContext<PedidosHub> hubContext,
        ICarritoService carritoService,
        IUsuarioService usuarioService)
    {
        _hubContext = hubContext;
        _carritoService = carritoService;
        _usuarioService = usuarioService;
    }

    [HttpGet("carritos-activos")]
    [Authorize(Roles = "Admin, Vendedor")]
    public async Task<ActionResult<List<UsuarioCartViewModel>>> GetActiveCarts()
    {
        try
        {
            var usuariosConCarrito = await _carritoService.GetUsuariosConCarritosActivos();
            return Ok(usuariosConCarrito);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{usuarioId}/carrito")]
    [Authorize(Roles = "Admin, Vendedor")]
    public async Task<ActionResult<CarritoViewModel>> GetUserCart(string usuarioId)
    {
        try
        {
            var carrito = await _carritoService.GetCarritoByUsuarioId(usuarioId);
            if (carrito == null)
                return NotFound();

            return Ok(carrito);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("estadisticas-carritos")]
    [Authorize(Roles = "Admin, Vendedor")]
    public async Task<ActionResult<CarritosStatisticsViewModel>> GetCartsStatistics()
    {
        try
        {
            var stats = await _carritoService.GetCarritosStatistics();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("carritos-abandonados")]
    [Authorize(Roles = "Admin, Vendedor")]
    public async Task<ActionResult<List<AbandonedCartViewModel>>> GetAbandonedCarts(
        [FromQuery] int minutosUmbral = 30)
    {
        try
        {
            var cartosAbandonados = await _carritoService
                .GetAbandonedCarts(TimeSpan.FromMinutes(minutosUmbral));
            return Ok(cartosAbandonados);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{usuarioId}/notificar-carrito")]
    [Authorize(Roles = "Admin, Vendedor")]
    public async Task<ActionResult> NotifyUserAboutCart(
        string usuarioId,
        [FromBody] NotificacionCarritoRequest request)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioById(usuarioId);
            if (usuario == null)
                return NotFound();

            // Enviar notificación (Email, SMS, SignalR, etc.)
            await _hubContext.Clients
                .User(usuarioId)
                .SendAsync("NotificacionCarrito", request.Mensaje);

            return Ok(new { exito = true, mensaje = "Notificación enviada" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

// Service
public interface ICarritoService
{
    Task<List<UsuarioCartViewModel>> GetUsuariosConCarritosActivos();
    Task<CarritoViewModel> GetCarritoByUsuarioId(string usuarioId);
    Task<CarritosStatisticsViewModel> GetCarritosStatistics();
    Task<List<AbandonedCartViewModel>> GetAbandonedCarts(TimeSpan inactivityThreshold);
}

// SignalR Hub - Agregar a PedidosHub.cs
public async Task NotificarCarritoActualizado(UsuarioCartViewModel carrito)
{
    // Solo enviar a Admin y Vendedores
    var rolesAutorizados = new[] { "Admin", "Administrador", "Vendedor" };

    await Clients
        .Group("AdminVendedores")
        .SendAsync("CarritoActualizado", carrito);
}
```

## ViewModels (Backend)

```csharp
public class UsuarioCartViewModel
{
    public string UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public int CartCount { get; set; }
    public decimal CartTotal { get; set; }
    public List<CartItemViewModel> CartItems { get; set; }
    public DateTime LastActivity { get; set; }
}

public class CartItemViewModel
{
    public string Id { get; set; }
    public string ProductoId { get; set; }
    public string Nombre { get; set; }
    public string Variante { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string Foto { get; set; }
}

public class CarritosStatisticsViewModel
{
    public int TotalActiveCarts { get; set; }
    public decimal TotalValue { get; set; }
    public decimal AverageCartValue { get; set; }
    public List<TopProductViewModel> TopProducts { get; set; }
}

public class AbandonedCartViewModel
{
    public string UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public int CartCount { get; set; }
    public decimal CartTotal { get; set; }
    public DateTime LastActivity { get; set; }
    public int MinutosSinActividad { get; set; }
}
```

## Flujo Completo

1. **Admin/Vendedor abre Dashboard**
   - Dashboard carga DashboardPage.vue
   - Se verifica rol con `checkAnyRole(['admin', 'administrador', 'vendedor'])`
   - Si tiene rol correcto, se carga ActiveCartsMonitor

2. **ActiveCartsMonitor se monta**
   - Llama `activeCartsService.getActiveCarts()` para obtener datos iniciales
   - Se carga lista de usuarios con carritos

3. **En tiempo real (SignalR)**
   - Cuando un usuario agrega/quita/modifica carrito
   - Backend emite evento `CarritoActualizado` al grupo "AdminVendedores"
   - signalRService recibe evento y llama `handleCarritoActualizado()`
   - Se actualiza UI automáticamente

4. **Refresco automático**
   - Cada 30 segundos se recarga la lista completa
   - Backup si SignalR falla

## Testing

### Probar localmente:
1. Abre 2 navegadores
2. En uno: Accede como cliente (usuario normal)
3. En otro: Accede como admin/vendedor
4. En cliente: Agrega productos al carrito
5. En admin/vendedor: Deberías ver el carrito actualizado en tiempo real

## Seguridad

✅ Solo usuarios con rol admin/vendedor pueden ver esta información
✅ Datos sensibles (emails, teléfonos) visibles solo en context de trabajo
✅ Recomendado: Auditar quién accede a esta información
✅ Recomendado: Rate limiting en endpoints

## Próximos pasos opcionales

1. Agregar exportación a CSV/Excel de carritos activos
2. Agregar gráficos de tendencias de carritos
3. Agregar filtros (por rango de precios, por tiempo de inactividad, etc.)
4. Agregar "Follow-up" automático a carritos abandonados
5. Agregar integraciones con email marketing para recuperar carritos
