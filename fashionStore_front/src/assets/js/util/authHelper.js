/**
 * Helper para decodificar y extraer información del JWT
 */

/**
 * Decodifica un JWT y retorna el payload
 * @param {string} token - El token JWT completo
 * @returns {object|null} - El payload decodificado o null si es inválido
 */
export function decodeToken(token) {
    try {
        if (!token) return null;

        // El JWT tiene 3 partes separadas por puntos
        const parts = token.split('.');
        if (parts.length !== 3) return null;

        // Decodificar el payload (segunda parte)
        const payload = parts[1];
        const decoded = JSON.parse(atob(payload));

        return decoded;
    } catch (error) {
        console.error('Error decodificando token:', error);
        return null;
    }
}

/**
 * Obtiene el token del storage
 * @returns {string|null} - El token o null
 */
export function getToken() {
    return sessionStorage.getItem('token') || localStorage.getItem('token');
}

/**
 * Obtiene la información del usuario del token decodificado
 * @returns {object|null} - Objeto con la información del usuario
 */
export function getUserInfo() {
    const token = getToken();
    const decoded = decodeToken(token);

    if (!decoded) return null;

    return {
        id: decoded.Id,
        nombreCompleto: decoded.NombreCompleto,
        username: decoded.unique_name,
        telefono: decoded.Telefono,
        correo: decoded.Correo,
        rol: decoded.Rol || decoded.rol || null, // Busca el rol en diferentes formatos
        roles: decoded.Roles || decoded.roles || [], // Para múltiples roles
        exp: decoded.exp
    };
}

/**
 * Obtiene el rol del usuario
 * @returns {string|null} - El rol del usuario
 */
export function getUserRole() {
    const userInfo = getUserInfo();
    return userInfo?.rol || null;
}

/**
 * Obtiene los roles del usuario
 * @returns {array} - Array de roles del usuario
 */
export function getUserRoles() {
    const userInfo = getUserInfo();
    return userInfo?.roles || [];
}

/**
 * Verifica si el usuario tiene un rol específico
 * @param {string} requiredRole - El rol a verificar
 * @returns {boolean}
 */
export function hasRole(requiredRole) {
    const userRole = getUserRole();
    const userRoles = getUserRoles();

    return userRole === requiredRole || userRoles.includes(requiredRole);
}

/**
 * Verifica si el usuario tiene alguno de los roles especificados
 * @param {array} requiredRoles - Array de roles a verificar
 * @returns {boolean}
 */
export function hasAnyRole(requiredRoles) {
    const userRole = getUserRole();
    const userRoles = getUserRoles();

    return requiredRoles.some(role => userRole === role || userRoles.includes(role));
}

/**
 * Verifica si el usuario tiene todos los roles especificados
 * @param {array} requiredRoles - Array de roles a verificar
 * @returns {boolean}
 */
export function hasAllRoles(requiredRoles) {
    const userRoles = getUserRoles();
    const userRole = getUserRole();

    return requiredRoles.every(role => userRole === role || userRoles.includes(role));
}

/**
 * Verifica si el token es válido y no ha expirado
 * @returns {boolean}
 */
export function isTokenValid() {
    const userInfo = getUserInfo();
    if (!userInfo || !userInfo.exp) return false;

    // exp está en segundos, Date.now() en milisegundos
    const expirationTime = userInfo.exp * 1000;
    return Date.now() < expirationTime;
}

/**
 * Obtiene el tiempo restante antes de que expire el token (en minutos)
 * @returns {number|null}
 */
export function getTokenExpirationTime() {
    const userInfo = getUserInfo();
    if (!userInfo || !userInfo.exp) return null;

    const expirationTime = userInfo.exp * 1000;
    const remainingMs = expirationTime - Date.now();

    if (remainingMs <= 0) return 0;

    return Math.floor(remainingMs / 60000); // Convertir a minutos
}

/**
 * Obtiene todos los datos del usuario
 * @returns {object|null}
 */
export function getFullUserData() {
    return getUserInfo();
}

/**
 * Limpia el token del storage
 */
export function clearToken() {
    sessionStorage.removeItem('token');
    localStorage.removeItem('token');
}
