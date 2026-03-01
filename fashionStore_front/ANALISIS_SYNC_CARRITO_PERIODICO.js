/**
 * ANÁLISIS DE ESTRUCTURA: Sincronización Periódica
 *
 * Este archivo muestra exactamente:
 * 1. Qué guarda el localStorage (fashion_cart_v1)
 * 2. Cómo lo formatea el servicio
 * 3. Qué recibe el backend
 */

// ============================================================
// 1. LO QUE GUARDA EL USUARIO EN LOCALSTORAGE
// ============================================================

// localStorage key: "fashion_cart_v1"
// localStorage value:

const LOCALSTORAGE_ITEM = [
    {
        // Datos principales del item
        "id": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",        // Variante ID
        "tieneDescuento": false,
        "precioVentaDescuento": 20,
        "nombre": "tenis de salir",
        "precioVenta": 20,                                      // PRECIO ← Importante
        "cantidad": 1,                                          // CANTIDAD ← Importante
        "foto": "/uploads/productos/fa071ab5-26ee-4ea4-a4cf-a6b98cb4f3cd.jpg",
        "talla": null,
        "color": null,

        // Datos expandidos del producto
        "raw": {
            "id": "44018c27-6c79-4b6e-80e9-b6a7a201a93c",       // Producto ID ← Principal
            "codigo": "t2",
            "descripcion": "tenis de salir",
            "esActivo": true,
            "sku": "t2",
            "precioCosto": 14,
            "precioVenta": 20,
            "monedaCostoId": "8b275763-36eb-4f8f-334f-08de5b4e9132",
            "monedaVentaId": "8b275763-36eb-4f8f-334f-08de5b4e9132",
            "categoriasIds": ["40760036-c9d9-491a-c336-08de5b4e7881"],
            "categoriasDescripcion": "calzado",
            "varianteId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
            "fotos": [],
            "variants": [...]
        }
    },
    // ... más items ...
]

// ============================================================
// 2. CÓMO LO TRANSFORMA periodicCartSyncService
// ============================================================

const FORMATTED_BY_SERVICE = {
    "usuarioId": "550e8400-e29b-41d4-a716-446655440001",    // ← Del token JWT
    "fechaCreacion": "2026-02-21T14:23:45.123Z",             // ← Momento actual
    "detalles": [
        {
            // Cada item del localStorage se convierte en:
            "carritoId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9", // ← De item.id
            "productoId": "44018c27-6c79-4b6e-80e9-b6a7a201a93c", // ← De item.raw.id
            "cantidad": 1,                                        // ← De item.cantidad
            "unitPrice": 20,                                      // ← De item.precioVenta
            "lineTotal": 20                                       // ← Calculado: precio × cantidad
        },
        // Más detalles si hay más items
    ]
}

// ============================================================
// 3. QUÉ RECIBE EL BACKEND
// ============================================================

/*
POST /EnviarDatosAlCarrito

Headers:
- Content-Type: application/json
- Authorization: Bearer [token]

Body (JSON):
{
  "usuarioId": "550e8400-e29b-41d4-a716-446655440001",
  "fechaCreacion": "2026-02-21T14:23:45.123Z",
  "detalles": [
    {
      "carritoId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
      "productoId": "44018c27-6c79-4b6e-80e9-b6a7a201a93c",
      "cantidad": 1,
      "unitPrice": 20,
      "lineTotal": 20
    }
  ]
}

Response esperado (backend):
{
  "success": true,
  "message": "Carrito sincronizado"
}
*/

// ============================================================
// 4. MAPEO DE CAMPOS (localStorage → Backend)
// ============================================================

const FIELD_MAPPING = {

    // De item directo:
    localStorage_id: "carritoId",              // item.id
    localStorage_cantidad: "cantidad",          // item.cantidad
    localStorage_precioVenta: "unitPrice",      // item.precioVenta

    // De item.raw:
    localStorage_raw_id: "productoId",          // item.raw.id

    // Generado automáticamente:
    generado_lineTotal: "lineTotal",            // item.precioVenta * item.cantidad
    generado_fechaCreacion: "fechaCreacion",    // new Date().toISOString()

    // Del JWToken:
    token_usuarioId: "usuarioId",               // Extraído del JWT
}

// ============================================================
// 5. EJEMPLO REAL: De localStorage a Backend
// ============================================================

// ANTES (localStorage):
const BEFORE = [
    {
        "id": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
        "nombre": "tenis de salir",
        "precioVenta": 20,
        "cantidad": 1,
        "raw": {
            "id": "44018c27-6c79-4b6e-80e9-b6a7a201a93c",
            // ... más datos ...
        }
    },
    {
        "id": "f8b1f52a-470d-57fe-c20b-fd5efcb817g0",
        "nombre": "camiseta",
        "precioVenta": 30,
        "cantidad": 2,
        "raw": {
            "id": "55019d38-7d80-6d7f-91f0-c7b8b312c94d",
        }
    }
]


// DESPUÉS (Lo que recibe el backend):
const AFTER = {
    "usuarioId": "550e8400-e29b-41d4-a716-446655440001",
    "fechaCreacion": "2026-02-21T14:23:45.123Z",
    "detalles": [
        {
            "carritoId": "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
            "productoId": "44018c27-6c79-4b6e-80e9-b6a7a201a93c",
            "cantidad": 1,
            "unitPrice": 20,
            "lineTotal": 20
        },
        {
            "carritoId": "f8b1f52a-470d-57fe-c20b-fd5efcb817g0",
            "productoId": "55019d38-7d80-6d7f-91f0-c7b8b312c94d",
            "cantidad": 2,
            "unitPrice": 30,
            "lineTotal": 60
        }
    ]
}

// ============================================================
// 6. RESUMEN DE TRANSFORMACIÓN
// ============================================================

/*
📊 RESUMEN ESTADÍSTICO:

Origen:     localStorage["fashion_cart_v1"]  (Array de items)
Destino:    POST /EnviarDatosAlCarrito       (JSON formateado)
Frecuencia: Cada 30 segundos
Timeout:    5000ms

Campos extraídos:
✅ usuarioId          ← JWT token
✅ fechaCreacion      ← Sistema
✅ detalles[]         ← Array transformado
   ├─ carritoId       ← item.id
   ├─ productoId      ← item.raw.id
   ├─ cantidad        ← item.cantidad
   ├─ unitPrice       ← item.precioVenta
   └─ lineTotal       ← Calculado

Validaciones:
✅ Usuario debe estar autenticado (token requerido)
✅ Carrito no debe estar vacío
✅ Cada item debe tener id y raw.id
✅ Precios y cantidades deben ser numéricos

Errores comunes y cómo se manejan:
❌ No hay token            → Se salta sincronización
❌ Carrito vacío           → Se salta sincronización
❌ Item sin raw.id         → Se usa item.id
❌ Precio inválido         → Se usa 0
❌ Cantidad inválida       → Se usa 0
❌ Backend error           → Se reintenta en próximo ciclo
*/

// ============================================================
// 7. FLUJO TEMPORAL
// ============================================================

/*
Minuto 0:00
┌─ Usuario abre la app
├─ Boot file detecta token
├─ Inicia periodicCartSyncService.start()
├─ Lee localStorage (fashion_cart_v1) → 3 items
├─ Formatea datos
└─ POST /EnviarDatosAlCarrito (1er sync) ✅

Minuto 0:30
├─ Timer dispara
├─ Lee localStorage → 3 items (sin cambios)
├─ POST /EnviarDatosAlCarrito (mismo contendo)
└─ Backend recibe

Minuto 1:00
├─ Usuario agrega producto
├─ Se guarda en localStorage → 4 items
├─ Timer dispara
├─ Lee localStorage → 4 items (CAMBIO)
├─ POST /EnviarDatosAlCarrito (incluye nuevo) ✅
└─ Backend actualiza BD

Minuto 1:30
├─ Lee localStorage → 4 items
├─ POST /EnviarDatosAlCarrito
└─ Backend recibe

...

Minuto 5:00
├─ Usuario hace logout
├─ Token se elimina
├─ Boot file lo detecta
├─ Llama periodicCartSyncService.stop()
└─ Ya no sincroniza
*/

// ============================================================
// 8. ESTRUCTURA DEL REQUEST EN DETALLE
// ============================================================

const DETAILED_REQUEST = {
    // 📌 HEADERS
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
        "Accept": "application/json"
    },

    // 📌 BODY
    body: {
        // ✅ Usuario (extraído del JWT)
        usuarioId: "550e8400-e29b-41d4-a716-446655440001",

        // ✅ Timestamp de la solicitud
        fechaCreacion: "2026-02-21T14:23:45.123Z",

        // ✅ Detalles del carrito (array de items transformados)
        detalles: [
            {
                carritoId: "87a0e52a-360c-46ed-b19a-ed4dfcb817f9",
                productoId: "44018c27-6c79-4b6e-80e9-b6a7a201a93c",
                cantidad: 1,
                unitPrice: 20.00,
                lineTotal: 20.00
            },
            {
                carritoId: "f8b1f52a-470d-57fe-c20b-fd5efcb817g0",
                productoId: "55019d38-7d80-6d7f-91f0-c7b8b312c94d",
                cantidad: 2,
                unitPrice: 30.50,
                lineTotal: 61.00
            }
        ]
    },

    // 📌 RESPUESTA ESPERADA
    response: {
        status: 200,
        body: {
            success: true,
            message: "Carrito sincronizado exitosamente",
            data: {
                carritoId: "abc-123",
                usuarioId: "550e8400-e29b-41d4-a716-446655440001",
                totalItems: 2,
                totalValue: 81.00,
                sincronizadoEn: "2026-02-21T14:23:45.567Z"
            }
        }
    }
}

// ============================================================
// CONCLUSIÓN
// ============================================================

/*
✅ TODO ESTÁ AUTOMATIZADO

El servicio periodicCartSyncService:
1. ✅ Lee localStorage automáticamente
2. ✅ Transforma la estructura automáticamente
3. ✅ Extrae usuarioId del JWT automáticamente
4. ✅ Envía POST automáticamente
5. ✅ Repite cada 30 segundos automáticamente

LO ÚNICO QUE NECESITA:
- ✅ Usuario autenticado (con token)
- ✅ Carrito guardado en localStorage
- ✅ Backend escuchando en /EnviarDatosAlCarrito

FRECUENCIA:
- Cada 30 segundos (configurable)
- Comienza con autenticación
- Para con desautenticación
- Se reinicia con F5 (sin pérdida)

RESULTADO:
- Backend siempre tiene carrito actualizado ✅
- Admin/Vendedor ve carritos en tiempo real ✅
- Base de datos sincronizada ✅
*/

export default DETAILED_REQUEST
