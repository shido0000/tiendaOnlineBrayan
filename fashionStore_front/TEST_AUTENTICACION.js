/**
 * SCRIPT DE TESTING PARA AUTENTICACIÓN Y ROLES
 *
 * Copia y pega esto en la consola del navegador (F12) para probar
 */

console.log('🧪 INICIANDO TESTS DE AUTENTICACIÓN\n');

// ============================================================================
// TEST 1: Decodificar Token
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 1: Decodificar Token');
console.log('════════════════════════════════════════════════════════════\n');

function decodeJWT(token) {
    try {
        const parts = token.split('.');
        if (parts.length !== 3) throw new Error('Token inválido');
        const payload = JSON.parse(atob(parts[1]));
        return payload;
    } catch (error) {
        console.error('Error decodificando:', error.message);
        return null;
    }
}

const token = sessionStorage.getItem('token') || localStorage.getItem('token');

if (!token) {
    console.error('❌ No hay token guardado en el storage');
    console.log('\nPara guardar un token de prueba, ejecuta:');
    console.log("sessionStorage.setItem('token', 'TU_TOKEN_AQUI');");
} else {
    console.log('✅ Token encontrado\n');
    const decoded = decodeJWT(token);
    if (decoded) {
        console.log('Contenido decodificado:');
        console.table(decoded);
        console.log('');
    }
}

// ============================================================================
// TEST 2: Información del Usuario
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 2: Información del Usuario');
console.log('════════════════════════════════════════════════════════════\n');

if (token) {
    const decoded = decodeJWT(token);
    console.log('Información extraída:');
    console.log(`  ID: ${decoded?.Id || 'N/A'}`);
    console.log(`  Usuario: ${decoded?.unique_name || 'N/A'}`);
    console.log(`  Nombre: ${decoded?.NombreCompleto || 'N/A'}`);
    console.log(`  Email: ${decoded?.Correo || 'N/A'}`);
    console.log(`  Teléfono: ${decoded?.Telefono || 'N/A'}`);
    console.log(`  Rol: ${decoded?.Rol || '⚠️  NO DEFINIDO'}`);
    console.log(`  Roles: ${decoded?.Roles ? decoded.Roles.join(', ') : '⚠️  NO DEFINIDO'}`);
    console.log('');
}

// ============================================================================
// TEST 3: Validación del Token
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 3: Validación del Token');
console.log('════════════════════════════════════════════════════════════\n');

if (token) {
    const decoded = decodeJWT(token);
    const expirationTime = decoded?.exp * 1000; // Convertir a milisegundos
    const now = Date.now();
    const isValid = now < expirationTime;

    console.log(`  Timestamp actual: ${new Date(now).toLocaleString()}`);
    console.log(`  Vencimiento token: ${new Date(expirationTime).toLocaleString()}`);
    console.log(`  Estado: ${isValid ? '✅ VÁLIDO' : '❌ EXPIRADO'}`);

    if (isValid) {
        const remainingMs = expirationTime - now;
        const remainingMinutes = Math.floor(remainingMs / 60000);
        const remainingHours = Math.floor(remainingMinutes / 60);
        console.log(`  Tiempo restante: ${remainingHours}h ${remainingMinutes % 60}m`);
    }
    console.log('');
}

// ============================================================================
// TEST 4: Prueba del Sistema de Roles (Simulado)
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 4: Sistema de Roles (Simulado)');
console.log('════════════════════════════════════════════════════════════\n');

if (token) {
    const decoded = decodeJWT(token);
    const userRole = decoded?.Rol;
    const userRoles = decoded?.Roles || [];

    console.log('Probando hasRole():');
    const testRoles = ['admin', 'editor', 'vendedor', 'user'];
    testRoles.forEach(role => {
        const hasIt = userRole === role || userRoles.includes(role);
        console.log(`  hasRole('${role}'): ${hasIt ? '✅ true' : '❌ false'}`);
    });
    console.log('');
}

// ============================================================================
// TEST 5: Funciones Disponibles
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 5: Funciones Disponibles');
console.log('════════════════════════════════════════════════════════════\n');

console.log('Importar funciones desde authHelper.js:\n');
console.log(`import {
  decodeToken,
  getUserInfo,
  getUserRole,
  getUserRoles,
  hasRole,
  hasAnyRole,
  hasAllRoles,
  isTokenValid,
  getTokenExpirationTime,
  getToken,
  clearToken
} from '@/assets/js/util/authHelper';\n`);

// ============================================================================
// TEST 6: Composable useAuth
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 6: Usar Composable useAuth en un Componente');
console.log('════════════════════════════════════════════════════════════\n');

console.log(`import { useAuth } from '@/assets/js/composables/useAuth';

export default {
  setup() {
    const {
      user,
      token,
      isAuthenticated,
      currentRole,
      currentRoles,
      tokenValid,
      timeUntilExpiration,
      checkRole,
      checkAnyRole,
      checkAllRoles,
      logout,
      loadUser
    } = useAuth();

    // Usar en el componente...
    return { user, checkRole };
  }
};\n`);

// ============================================================================
// TEST 7: Directiva v-has-role
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('TEST 7: Usar Directiva v-has-role en Templates');
console.log('════════════════════════════════════════════════════════════\n');

console.log(`<!-- Mostrar solo si rol es 'admin' -->
<div v-has-role="'admin'">
  Solo para administradores
</div>

<!-- Mostrar si tiene alguno de estos roles -->
<div v-has-role="['admin', 'editor']">
  Para admin o editor
</div>

<!-- Mostrar si tiene TODOS estos roles -->
<div v-has-role:all="['admin', 'auditor']">
  Admin y auditor
</div>\n`);

// ============================================================================
// RESUMEN FINAL
// ============================================================================
console.log('════════════════════════════════════════════════════════════');
console.log('📋 RESUMEN');
console.log('════════════════════════════════════════════════════════════\n');

if (token) {
    const decoded = decodeJWT(token);
    const hasRole = decoded?.Rol ? '✅' : '❌';
    const isValid = Date.now() < (decoded?.exp * 1000) ? '✅' : '❌';

    console.log(`Token guardado: ✅`);
    console.log(`Token válido: ${isValid}`);
    console.log(`Rol incluido: ${hasRole}`);
    console.log('');

    if (!decoded?.Rol && !decoded?.Roles) {
        console.warn('⚠️  IMPORTANTE: El token no incluye campo "Rol"');
        console.warn('El backend debe agregarlo al generar el JWT\n');
    }
} else {
    console.error('❌ No hay token. Pasos a seguir:');
    console.log('1. Haz login en la aplicación');
    console.log('2. Vuelve a ejecutar este script');
    console.log('3. El token debe guardarse automáticamente en sessionStorage\n');
}

console.log('📖 Documentación: Ver GUIA_ROLES.md');
console.log('🔗 Ejemplos: Ver src/pages/RoleExamplePage.vue');
console.log('✅ Tests completados\n');
