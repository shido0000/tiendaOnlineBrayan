/**
 * Datos de ejemplo para testing del Monitor de Carritos Activos
 * Úsalo para probar el frontend antes de que el backend esté completo
 */

// Para usar esto, simula la respuesta del backend en el servicio activeCartsService.js
// O agrega un interceptor en axios.js para devolver estos datos en desarrollo

export const MOCK_ACTIVE_CARTS = [
    {
        usuarioId: "550e8400-e29b-41d4-a716-446655440001",
        nombre: "Juan",
        apellido: "Pérez García",
        email: "juan.perez@example.com",
        telefono: "+34 912 345 678",
        cartCount: 5,
        cartTotal: 250.50,
        lastActivity: new Date(Date.now() - 5 * 60000).toISOString(), // Hace 5 minutos
        cartItems: [
            {
                id: "1",
                productoId: "prod-001",
                nombre: "Camiseta Premium",
                variante: "Talla L - Color Azul",
                precio: 50.00,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Camiseta"
            },
            {
                id: "2",
                productoId: "prod-002",
                nombre: "Pantalón Denim",
                variante: "Talla 32",
                precio: 80.00,
                cantidad: 2,
                foto: "https://via.placeholder.com/100?text=Pantalon"
            },
            {
                id: "3",
                productoId: "prod-003",
                nombre: "Chaqueta Deportiva",
                variante: "S",
                precio: 120.00,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Chaqueta"
            }
        ]
    },
    {
        usuarioId: "550e8400-e29b-41d4-a716-446655440002",
        nombre: "María",
        apellido: "López Fernández",
        email: "maria.lopez@example.com",
        telefono: "+34 913 456 789",
        cartCount: 2,
        cartTotal: 89.99,
        lastActivity: new Date(Date.now() - 15 * 60000).toISOString(), // Hace 15 minutos
        cartItems: [
            {
                id: "4",
                productoId: "prod-004",
                nombre: "Zapatillas Running",
                variante: "Talla 38",
                precio: 79.99,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Zapatillas"
            },
            {
                id: "5",
                productoId: "prod-005",
                nombre: "Calcetines Sport",
                variante: "Pack 3",
                precio: 10.00,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Calcetines"
            }
        ]
    },
    {
        usuarioId: "550e8400-e29b-41d4-a716-446655440003",
        nombre: "Carlos",
        apellido: "Martínez Ruiz",
        email: "carlos.martinez@example.com",
        telefono: "+34 914 567 890",
        cartCount: 8,
        cartTotal: 542.75,
        lastActivity: new Date(Date.now() - 2 * 60000).toISOString(), // Hace 2 minutos
        cartItems: [
            {
                id: "6",
                productoId: "prod-006",
                nombre: "Blazer Formal",
                variante: "Talla M - Negro",
                precio: 150.00,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Blazer"
            },
            {
                id: "7",
                productoId: "prod-007",
                nombre: "Pantalón Formal",
                variante: "Talla 32",
                precio: 90.00,
                cantidad: 2,
                foto: "https://via.placeholder.com/100?text=Pantalon2"
            },
            {
                id: "8",
                productoId: "prod-008",
                nombre: "Corbata",
                variante: "Seda Azul",
                precio: 35.00,
                cantidad: 2,
                foto: "https://via.placeholder.com/100?text=Corbata"
            },
            {
                id: "9",
                productoId: "prod-009",
                nombre: "Camisa Blanca",
                variante: "L",
                precio: 60.00,
                cantidad: 2,
                foto: "https://via.placeholder.com/100?text=Camisa"
            },
            {
                id: "10",
                productoId: "prod-010",
                nombre: "Cinturón Cuero",
                variante: "Marrón",
                precio: 45.75,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Cinturon"
            }
        ]
    },
    {
        usuarioId: "550e8400-e29b-41d4-a716-446655440004",
        nombre: "Ana",
        apellido: "Rodríguez Sánchez",
        email: "ana.rodriguez@example.com",
        telefono: "+34 915 678 901",
        cartCount: 3,
        cartTotal: 175.50,
        lastActivity: new Date(Date.now() - 45 * 60000).toISOString(), // Hace 45 minutos (ABANDONADO)
        cartItems: [
            {
                id: "11",
                productoId: "prod-011",
                nombre: "Vestido Casual",
                variante: "S",
                precio: 95.00,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Vestido"
            },
            {
                id: "12",
                productoId: "prod-012",
                nombre: "Bolso",
                variante: "Negro",
                precio: 80.50,
                cantidad: 1,
                foto: "https://via.placeholder.com/100?text=Bolso"
            }
        ]
    }
]

/**
 * Simulador de actualizaciones de carrito en tiempo real
 * Usa esto para probar el SignalR sin backend
 */
export function createMockCartUpdate() {
    const updates = [
        {
            usuarioId: "550e8400-e29b-41d4-a716-446655440001",
            nombre: "Juan",
            apellido: "Pérez García",
            email: "juan.perez@example.com",
            cartCount: 6, // Agregó un item más
            cartTotal: 289.50,
            lastActivity: new Date().toISOString(),
            action: "item_added"
        },
        {
            usuarioId: "550e8400-e29b-41d4-a716-446655440002",
            nombre: "María",
            apellido: "López Fernández",
            email: "maria.lopez@example.com",
            cartCount: 0, // Carrito vacío
            cartTotal: 0,
            lastActivity: new Date().toISOString(),
            action: "cart_emptied"
        }
    ]

    return updates[Math.floor(Math.random() * updates.length)]
}

/**
 * Estadísticas de ejemplo
 */
export const MOCK_CART_STATISTICS = {
    totalActiveCarts: 4,
    totalValue: 1058.74,
    averageCartValue: 264.685,
    topProducts: [
        {
            productoId: "prod-002",
            nombre: "Pantalón Denim",
            cantidadEnCarritos: 3,
            valorTotal: 240.00
        },
        {
            productoId: "prod-007",
            nombre: "Pantalón Formal",
            cantidadEnCarritos: 2,
            valorTotal: 180.00
        },
        {
            productoId: "prod-001",
            nombre: "Camiseta Premium",
            cantidadEnCarritos: 1,
            valorTotal: 50.00
        }
    ],
    usuariosConMasItems: [
        {
            usuarioId: "550e8400-e29b-41d4-a716-446655440003",
            nombre: "Carlos Martínez",
            cartCount: 8
        },
        {
            usuarioId: "550e8400-e29b-41d4-a716-446655440001",
            nombre: "Juan Pérez",
            cartCount: 5
        }
    ]
}

/**
 * Carritos abandonados (inactivos > 30 minutos)
 */
export const MOCK_ABANDONED_CARTS = [
    {
        usuarioId: "550e8400-e29b-41d4-a716-446655440004",
        nombre: "Ana",
        email: "ana.rodriguez@example.com",
        cartCount: 3,
        cartTotal: 175.50,
        lastActivity: new Date(Date.now() - 45 * 60000).toISOString(),
        minutosSinActividad: 45
    }
]

/**
 * INSTRUCCIONES PARA TESTING CON DATOS MOCK
 *
 * Opción 1: Usar interceptor en axios.js
 * ======================================
 *
 * En boot/axios.js, agrega:
 *
 * api.interceptors.response.use(response => response, error => {
 *   if (error.config.url === '/api/usuarios/carritos-activos' && process.env.DEV) {
 *     return Promise.resolve({ data: MOCK_ACTIVE_CARTS })
 *   }
 *   return Promise.reject(error)
 * })
 *
 *
 * Opción 2: Simular en el componente
 * ==================================
 *
 * En ActiveCartsMonitor.vue, en el script setup:
 *
 * // Descomentar para testing
 * // import { MOCK_ACTIVE_CARTS, createMockCartUpdate } from 'src/assets/js/util/mockData'
 *
 * // En loadActiveUsers(), reemplaza el try-catch:
 * // state.activeUsers = MOCK_ACTIVE_CARTS
 *
 * // Para simular actualizaciones SignalR:
 * // setInterval(() => {
 * //   handleCarritoUpdated(createMockCartUpdate())
 * // }, 5000)
 *
 *
 * Opción 3: JSON Server Mock
 * =========================
 *
 * Instala json-server: npm install json-server
 * Crea un archivo db.json con los datos mock
 * Ejecuta: json-server --watch db.json --port 3000
 * Cambia la URL en activeCartsService.js
 */
