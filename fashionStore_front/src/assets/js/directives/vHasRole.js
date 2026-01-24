/**
 * Directiva v-has-role para Vue 3
 * Muestra u oculta elementos basado en roles del usuario
 *
 * Uso:
 *   v-has-role="'admin'"                    - Mostrar si el rol es 'admin'
 *   v-has-role="['admin', 'manager']"       - Mostrar si tiene alguno de esos roles
 *   v-has-role:any="['admin', 'user']"      - Mostrar si tiene alguno de esos roles
 *   v-has-role:all="['admin', 'editor']"    - Mostrar si tiene TODOS esos roles
 */

//import { hasRole, hasAnyRole, hasAllRoles } from '@/assets/js/util/authHelper';
import { hasRole, hasAnyRole, hasAllRoles } from 'assets/js/util/authHelper';
export const vHasRole = {
    mounted(el, binding) {
        const { value, arg } = binding;

        let hasAccess = false;

        if (arg === 'all') {
            // Requiere todos los roles
            const roles = Array.isArray(value) ? value : [value];
            hasAccess = hasAllRoles(roles);
        } else if (arg === 'any' || !arg) {
            // Requiere al menos uno de los roles (por defecto)
            if (Array.isArray(value)) {
                hasAccess = hasAnyRole(value);
            } else {
                hasAccess = hasRole(value);
            }
        }

        if (!hasAccess) {
            el.style.display = 'none';
            el.setAttribute('data-role-hidden', 'true');
        } else {
            el.removeAttribute('data-role-hidden');
        }
    },

    updated(el, binding) {
        const { value, arg } = binding;

        let hasAccess = false;

        if (arg === 'all') {
            const roles = Array.isArray(value) ? value : [value];
            hasAccess = hasAllRoles(roles);
        } else if (arg === 'any' || !arg) {
            if (Array.isArray(value)) {
                hasAccess = hasAnyRole(value);
            } else {
                hasAccess = hasRole(value);
            }
        }

        if (!hasAccess) {
            el.style.display = 'none';
            el.setAttribute('data-role-hidden', 'true');
        } else {
            el.style.display = '';
            el.removeAttribute('data-role-hidden');
        }
    }
};

export default vHasRole;
