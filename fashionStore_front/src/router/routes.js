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
        path: '/categoria/:id',
        name: 'CategoriaProductos',
        component: () => import('src/pages/Visual/CategoriaProductosPage.vue')
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
                path: 'Moneda',
                name: 'Moneda',
                component: () => import('src/pages/Nomenclators/Moneda.vue')
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


        ]
    },
    {
        path: '/:catchAll(.*)*',
        component: () => import('src/ErrorNotFound.vue')
    }
]

export default routes
