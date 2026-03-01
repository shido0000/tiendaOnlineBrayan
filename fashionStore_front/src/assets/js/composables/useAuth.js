/**
 * Composable para manejar autenticación y roles en Vue 3
 */
import { computed, ref } from 'vue';
import {
    getToken,
    decodeToken,
    getUserInfo,
    getUserRole,
    getUserRoles,
    hasRole,
    hasAnyRole,
    hasAllRoles,
    isTokenValid,
    getTokenExpirationTime,
    getFullUserData,
    clearToken
} from 'src/assets/js/util/authHelper';

export function useAuth() {
    // Estado reactivo del usuario
    const user = ref(null);
    const token = ref(null);
    const isAuthenticated = ref(false);

    // Cargar información del usuario
    const loadUser = () => {
        token.value = getToken();
        user.value = getUserInfo();
        isAuthenticated.value = isTokenValid() && !!token.value;
        return user.value;
    };

    // Computadas reactivas
    const currentRole = computed(() => user.value?.rol || null);
    const currentRoles = computed(() => user.value?.roles || []);
    const tokenValid = computed(() => isTokenValid());
    const timeUntilExpiration = computed(() => getTokenExpirationTime());
    const userData = computed(() => user.value);

    // Métodos
    const checkRole = (requiredRole) => {
        return hasRole(requiredRole);
    };

    const checkAnyRole = (requiredRoles) => {
        return hasAnyRole(requiredRoles);
    };

    const checkAllRoles = (requiredRoles) => {
        return hasAllRoles(requiredRoles);
    };

    const logout = () => {
        clearToken();
        user.value = null;
        token.value = null;
        isAuthenticated.value = false;
    };

    // Cargar usuario al iniciar
    loadUser();

    return {
        // Estado
        user,
        token,
        isAuthenticated,

        // Computadas
        currentRole,
        currentRoles,
        tokenValid,
        timeUntilExpiration,
        userData,

        // Métodos
        loadUser,
        checkRole,
        checkAnyRole,
        checkAllRoles,
        logout,
        getFullUserData,
    };
}
