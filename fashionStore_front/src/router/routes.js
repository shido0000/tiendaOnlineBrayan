import InformacionPage from 'src/pages/extra/InformacionPage.vue'

const routes = [
    {
        path: '/register',
        name: 'RegisterPage',
        component: () => import('src/pages/RegisterPage.vue')
    },
    {
        path: '/login',
        name: 'LoginPage',
        component: () => import('src/pages/LoginPage.vue')
    },
    {
        path: '/',
        name: 'IndexPage',
        component: () => import('src/IndexPage.vue')
    },
    {
        path: '/categorias/:id',
        name: 'CategoriaProductos',
        component: () => import('src/pages/Visual/CategoriaProductosPage.vue')
    },
    {
        path: '/lista_deseos',
        name: 'ListaDeseos',
        component: () => import('src/pages/Visual/ListaDeseosPage.vue')
    },
    {
        path: '/producto/:id',
        name: 'ProductoDetalle',
        component: () => import('src/pages/Visual/ProductoDetallePage.vue')
    },
    {
        path: '/productos',
        name: 'Productos',
        component: () => import('src/pages/Visual/ProductosPage.vue')
    },
    {
        path: '/categorias',
        name: 'Categorias',
        component: () => import('src/pages/Visual/CategoriasPage.vue')
    },
    {
        path: '/carrito',
        name: 'Carrito',
        component: () => import('src/pages/Visual/CarritoPage.vue')
    },
    {
        path: '/',
        component: () => import('layouts/MainLayout.vue'),
        children: [
            {
                path: 'NomenclatorsCard',
                name: 'NomenclatorsCard',
                component: () => import('src/pages/Nomenclators/NomenclatorsCard.vue')
            },
            {
                path: 'Dashboard',
                name: 'Dashboard',
                component: () => import('src/pages/DashboardPage.vue')
            },
            {
                path: 'Moneda',
                name: 'Moneda',
                component: () => import('src/pages/Nomenclators/Moneda.vue')
            },
            {
                path: 'Gestor',
                name: 'Gestor',
                component: () => import('src/pages/Nomenclators/Gestor.vue')
            }, {
                path: 'Mensajeria',
                name: 'Mensajeria',
                component: () => import('src/pages/Nomenclators/Mensajeria.vue')
            },
            {
                path: 'Categoria',
                name: 'Categoria',
                component: () => import('src/pages/Nomenclators/Categoria.vue')
            },
            {
                path: 'Producto',
                name: 'Producto',
                component: () => import('src/pages/Nomenclators/Producto.vue')
            },
            {
                path: 'Descuento',
                name: 'Descuento',
                component: () => import('src/pages/Nomenclators/Descuento.vue')
            },
            {
                path: 'Pedido',
                name: 'Pedido',
                component: () => import('src/pages/Nomenclators/Pedido.vue')
            },
            {
                path: 'Cupon',
                name: 'Cupon',
                component: () => import('src/pages/Nomenclators/Cupon.vue')
            },
            {
                path: 'Inventario',
                name: 'Inventario',
                component: () => import('src/pages/Nomenclators/Inventario.vue')
            },
            {
                path: 'Usuario',
                name: 'Usuario',
                component: () => import('src/pages/Nomenclators/Usuario.vue')
            },
            {
                path: 'Perfil',
                name: 'Perfil',
                component: () => import('src/pages/ProfilePage.vue')
            },
            {
                path: 'OtraVariante',
                name: 'OtraVariante',
                component: () => import('src/pages/Nomenclators/OtraVariante.vue')
            },
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
            },
            {
                path: 'Contabilidad',
                name: 'Contabilidad',
                component: () => import('src/pages/Nomenclators/Contabilidad/ContabilidadPage.vue')
            },
            { path: '/informacion', name: 'Informacion', component: InformacionPage },
            {
                path: 'CrearInformacion',
                name: 'CrearInformacion',
                component: () => import('src/pages/extra/CrearInformacionPage.vue')
            },
        ]
    },

    {
        path: '/:catchAll(.*)*',
        component: () => import('src/ErrorNotFound.vue')
    }
]

export default routes
