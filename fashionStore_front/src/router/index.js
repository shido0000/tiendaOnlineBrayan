import { route } from 'quasar/wrappers'
import { createRouter, createMemoryHistory, createWebHistory, createWebHashHistory } from 'vue-router'
import routes from './routes'
import { isTokenValid, getUserRole } from 'assets/js/util/authHelper'
import { canAccessRoute } from 'assets/js/util/essentialListUrl'

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default route(function (/* { store, ssrContext } */) {
    const createHistory = process.env.SERVER
        ? createMemoryHistory
        : (process.env.VUE_ROUTER_MODE === 'history' ? createWebHistory : createWebHashHistory)

    const Router = createRouter({
        scrollBehavior: () => ({ left: 0, top: 0 }),
        routes,

        // Leave this as is and make changes in quasar.conf.js instead!
        // quasar.conf.js -> build -> vueRouterMode
        // quasar.conf.js -> build -> publicPath
        history: createHistory(process.env.MODE === 'ssr' ? void 0 : process.env.VUE_ROUTER_BASE)
    })

    /**
     * Guard de navegación global
     * Valida:
     * 1. Autenticación (token válido)
     * 2. Permisos por rol (acceso a rutas específicas)
     * 3. Redirección automática a login o acceso denegado
     */
    Router.beforeEach((to, from, next) => {
        const isPublic = to.meta?.public === true
        const requiresAuth = to.meta?.requiresAuth === true
        const isAuthenticated = isTokenValid()
        const userRole = getUserRole()
        const routeName = to.name

        // 1️⃣ Si es ruta pública, permitir acceso sin validación
        if (isPublic) {
            return next()
        }

        // 2️⃣ Si la ruta requiere autenticación y no hay token válido
        if (requiresAuth && !isAuthenticated) {
            return next({
                name: 'LoginPage',
                query: { redirect: to.fullPath }
            })
        }

        // 3️⃣ Si está autenticado, verificar permisos por rol
        if (isAuthenticated) {
            // Validar si el usuario tiene permiso para acceder a esta ruta
            const hasPermission = canAccessRoute(routeName, userRole)

            if (!hasPermission) {
                return next({
                    name: 'AccessDenied'
                })
            }

            return next()
        }

        // 4️⃣ Si no está autenticado y no es ruta pública, redirigir a login
        if (!isAuthenticated && !isPublic) {
            return next({
                name: 'LoginPage',
                query: { redirect: to.fullPath }
            })
        }

        // 5️⃣ Permitir acceso en otros casos
        return next()
    })

    /**
     * Guard de navegación posterior
     * Para logging y validación después de que se carga la ruta
     */
    Router.afterEach((to, from, failure) => {
        if (failure) {
            console.error('❌ Falla en la navegación:', failure.message)
        }
    })

    return Router
})
