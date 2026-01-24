/**
 * Test de autenticación y roles
 * Ejecuta esto en la consola del navegador o en un script
 */

import { decodeToken, getUserInfo, hasRole, isTokenValid, getTokenExpirationTime } from '@/assets/js/util/authHelper';

// Tu token (el que compartiste)
const tuToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IngiLCJqdGkiOiIyYTk3M2M3OS1lNzY5LTRiODYtYjQ0Ni05YjQ5YTdhMTM1N2YiLCJJZCI6IjUxZGFlNTdkLWQwOTUtNGIxMy03MGViLTA4ZGU1OGZmNjUxZiIsIk5vbWJyZUNvbXBsZXRvIjoieCB4IiwiVGVsZWZvbm8iOiI1Mzg5Mzg4NyIsIkNvcnJlbyI6InhAeHgueHgiLCJleHAiOjE3NjkwNzIxOTMsImlzcyI6IndlYkJyYXlhbkFQSSIsImF1ZCI6IndlYkJyYXlhbkNsaWVudCJ9.zHzb87OMFccPkEoPx1hZG3FiBtZQhe6wgB35Uwu1XVI';

console.log('=== TEST DE AUTENTICACIÓN ===\n');

// 1. Decodificar token
console.log('1. Decodificando token...');
const decoded = decodeToken(tuToken);
console.log('Payload:', decoded);
console.log('');

// 2. Obtener información del usuario
console.log('2. Información del usuario:');
const userInfo = getUserInfo();
console.log('ID:', userInfo?.id);
console.log('Nombre:', userInfo?.nombreCompleto);
console.log('Email:', userInfo?.correo);
console.log('Rol:', userInfo?.rol || 'NO DEFINIDO');
console.log('Roles:', userInfo?.roles || 'NO DEFINIDO');
console.log('');

// 3. Verificar si es válido
console.log('3. Validación del token:');
console.log('¿Token válido?:', isTokenValid());
console.log('Minutos hasta expiración:', getTokenExpirationTime());
console.log('');

// 4. Verificar roles (con rol ficticio para prueba)
console.log('4. Verificación de roles (resultado esperado: false):');
console.log('¿Es admin?:', hasRole('admin'));
console.log('¿Es vendedor?:', hasRole('vendedor'));
console.log('');

// 5. Información completa
console.log('5. Datos completos del usuario:');
console.log(JSON.stringify(userInfo, null, 2));

export { tuToken };
