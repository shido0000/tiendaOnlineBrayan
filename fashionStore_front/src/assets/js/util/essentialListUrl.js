import { getUserRole } from './authHelper';

const allMenuItems = [
    {
        title: 'INICIO',
        link: 'IndexPage'
    },
    {
        title: 'DASHBOARD',
        link: 'Dashboard'
    },
    {
        title: 'PERFIL',
        link: 'Perfil'
    },
    {
        title: 'PEDIDOS',
        link: 'Pedido'
    },
    {
        title: 'NOMENCLADORES',
        link: 'NomenclatorsCard'
    },
    {
        title: 'CREAR INFORMACIÓN',
        link: 'CrearInformacion'
    },
    {
        title: 'VER CARRITO DE CLIENTES',
        link: 'RealActiveCartsMonitor'
    },
];

/**
 * Configuración de menús por rol
 * Determina qué items de menú ve cada rol
 */
const menuByRole = {
    Admin: ['INICIO', 'DASHBOARD', 'PERFIL', 'PEDIDOS', 'NOMENCLADORES', 'CREAR INFORMACIÓN'],
    Vendedor: ['INICIO', 'PERFIL', 'PEDIDOS'],
    Cliente: ['INICIO', 'PERFIL', 'PEDIDOS']
};

/**
 * Configuración de permisos por ruta
 * Determina qué roles pueden acceder a cada ruta protegida
 * Si una ruta no está en esta lista, solo Admin puede acceder
 */
/*export const routePermissions = {
    'Dashboard': ['Admin'],
    'Moneda': ['Admin'],
    'Gestor': ['Admin'],
    'Mensajeria': ['Admin', 'Vendedor'],
    'Categoria': ['Admin'],
    'Producto': ['Admin', 'Vendedor'],
    'Descuento': ['Admin'],
    'Pedido': ['Admin', 'Vendedor', 'Cliente'],
    'Cupon': ['Admin'],
    'Usuario': ['Admin'],
    'Perfil': ['Admin', 'Vendedor', 'Cliente'],
    'OtraVariante': ['Admin'],
    'CuentasContables': ['Admin'],
    'AsientosContables': ['Admin'],
    'ReporteContable': ['Admin'],
    'Contabilidad': ['Admin'],
    'CrearInformacion': ['Admin'],
    'DiagnosticoNotificaciones': ['Admin'],
    'NomenclatorsCard': ['Admin', 'Vendedor']
};*/
export const routePermissions = {
    'Dashboard': ['Administrador'],
    'Moneda': ['Administrador'],
    'Gestor': ['Administrador'],
    'Mensajeria': ['Administrador'],
    'Categoria': ['Administrador'],
    'Producto': ['Administrador'],
    'Descuento': ['Administrador'],
    'Pedido': ['Administrador', 'Vendedor', 'Cliente'],
    'Cupon': ['Administrador'],
    'Usuario': ['Administrador'],
    'Perfil': ['Administrador', 'Vendedor', 'Cliente'],
    'OtraVariante': ['Administrador'],
    'CuentasContables': ['Administrador'],
    'AsientosContables': ['Administrador'],
    'ReporteContable': ['Administrador'],
    'Contabilidad': ['Administrador'],
    'Banner': ['Administrador'],
    'CrearInformacion': ['Administrador'],
    'DiagnosticoNotificaciones': ['Administrador'],
    'NomenclatorsCard': ['Administrador'],
    'TestActiveCartsMonitor': ['Administrador', 'Vendedor'],
    'RealActiveCartsMonitor': ['Administrador', 'Vendedor']
};

/**
 * Obtiene la lista de menú filtrada según el rol del usuario
 * @returns {array} - Lista de items del menú disponibles para el usuario
 */
export function getEssentialListUrl() {
    const userRole = getUserRole();

    // Si no hay rol definido o el rol no tiene restricciones, retorna todos los items
    if (!userRole || !menuByRole[userRole]) {
        return allMenuItems;
    }

    // Filtrar items según el rol
    const allowedTitles = menuByRole[userRole];
    return allMenuItems.filter(item => allowedTitles.includes(item.title));
}

/**
 * Verifica si un rol puede acceder a una ruta específica
 * @param {string} routeName - Nombre de la ruta (ej: 'Dashboard', 'Categoria')
 * @param {string} userRole - Rol del usuario
 * @returns {boolean} - true si el usuario puede acceder, false si no
 */
export function canAccessRoute(routeName, userRole) {
    // Si no hay rol, denegar acceso
    if (!userRole) return false;

    // Si el rol no tiene restricciones definidas, solo Admin puede acceder
    if (!routePermissions[routeName]) {
        return userRole === 'Admin';
    }

    // Verificar si el rol está en la lista de permisos
    return routePermissions[routeName].includes(userRole);
}

// Mantener exportación de la lista completa para compatibilidad
export const essentialListUrl = allMenuItems;
