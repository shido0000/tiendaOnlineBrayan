import InformacionPage from 'src/pages/extra/InformacionPage.vue'

// Rutas públicas (sin autenticación requerida)
const publicRoutes = [
    {
        path: '/register',
        name: 'RegisterPage',
        component: () => import('src/pages/RegisterPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/login',
        name: 'LoginPage',
        component: () => import('src/pages/LoginPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/',
        name: 'IndexPage',
        component: () => import('src/IndexPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/categorias/:id',
        name: 'CategoriaProductos',
        component: () => import('src/pages/Visual/CategoriaProductosPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/lista_deseos',
        name: 'ListaDeseos',
        component: () => import('src/pages/Visual/ListaDeseosPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/producto/:id',
        name: 'ProductoDetalle',
        component: () => import('src/pages/Visual/ProductoDetallePage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/productos',
        name: 'Productos',
        component: () => import('src/pages/Visual/ProductosPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/categorias',
        name: 'Categorias',
        component: () => import('src/pages/Visual/CategoriasPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    {
        path: '/carrito',
        name: 'Carrito',
        component: () => import('src/pages/Visual/CarritoPage.vue'),
        meta: { requiresAuth: false, public: true }
    },
    { path: '/informacion', name: 'Informacion', component: InformacionPage, meta: { requiresAuth: false, public: true } },

    {
        path: '/',
        component: () => import('layouts/MainLayout.vue'),
        meta: { requiresAuth: true },
        children: [
            {
                path: 'NomenclatorsCard',
                name: 'NomenclatorsCard',
                component: () => import('src/pages/Nomenclators/NomenclatorsCard.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Dashboard',
                name: 'Dashboard',
                component: () => import('src/pages/DashboardPage.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Moneda',
                name: 'Moneda',
                component: () => import('src/pages/Nomenclators/Moneda.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Gestor',
                name: 'Gestor',
                component: () => import('src/pages/Nomenclators/Gestor.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Mensajeria',
                name: 'Mensajeria',
                component: () => import('src/pages/Nomenclators/Mensajeria.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Categoria',
                name: 'Categoria',
                component: () => import('src/pages/Nomenclators/Categoria.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Producto',
                name: 'Producto',
                component: () => import('src/pages/Nomenclators/Producto.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Descuento',
                name: 'Descuento',
                component: () => import('src/pages/Nomenclators/Descuento.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Pedido',
                name: 'Pedido',
                component: () => import('src/pages/Nomenclators/Pedido.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Cupon',
                name: 'Cupon',
                component: () => import('src/pages/Nomenclators/Cupon.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Usuario',
                name: 'Usuario',
                component: () => import('src/pages/Nomenclators/Usuario.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Perfil',
                name: 'Perfil',
                component: () => import('src/pages/ProfilePage.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'OtraVariante',
                name: 'OtraVariante',
                component: () => import('src/pages/Nomenclators/OtraVariante.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'CuentasContables',
                name: 'CuentasContables',
                component: () => import('src/pages/Nomenclators/Contabilidad/CuentasContables.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'AsientosContables',
                name: 'AsientosContables',
                component: () => import('src/pages/Nomenclators/Contabilidad/AsientosContables.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'ReporteContable',
                name: 'ReporteContable',
                component: () => import('src/pages/Nomenclators/Contabilidad/ReporteContable.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Contabilidad',
                name: 'Contabilidad',
                component: () => import('src/pages/Nomenclators/Contabilidad/ContabilidadPage.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'Banner',
                name: 'Banner',
                component: () => import('src/pages/Nomenclators/Banner.vue'),
                meta: { requiresAuth: true }
            },

            {
                path: 'CrearInformacion',
                name: 'CrearInformacion',
                component: () => import('src/pages/extra/CrearInformacionPage.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'DiagnosticoNotificaciones',
                name: 'DiagnosticoNotificaciones',
                component: () => import('src/pages/Test/DiagnosticoNotificaciones.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'TestActiveCartsMonitor',
                name: 'TestActiveCartsMonitor',
                component: () => import('src/pages/Test/TestActiveCartsMonitor.vue'),
                meta: { requiresAuth: true }
            },
            {
                path: 'RealActiveCartsMonitor',
                name: 'RealActiveCartsMonitor',
                component: () => import('src/pages/Test/RealActiveCartsMonitor.vue'),
                meta: { requiresAuth: true }
            },
        ]
    },

    // Rutas especiales
    {
        path: '/access-denied',
        name: 'AccessDenied',
        component: () => import('src/AccessDenied.vue'),
        meta: { public: true }
    },

    // Catch-all para 404
    {
        path: '/:catchAll(.*)*',
        component: () => import('src/ErrorNotFound.vue'),
        meta: { public: true }
    }
]

export default publicRoutes
