/**
 * Utilidades para el módulo de contabilidad
 * Proporciona funciones helper para cálculos y validaciones
 */

/**
 * Estructura de una cuenta contable jerárquica
 */
export class CuentaContableHelper {
    /**
     * Construye un árbol de cuentas a partir de una lista plana
     * @param {Array} cuentas - Array de cuentas
     * @returns {Array} Array en estructura de árbol
     */
    static construirArbol(cuentas) {
        const mapa = {};
        const raices = [];

        // Crear mapa de ID a objeto
        cuentas.forEach(cuenta => {
            mapa[cuenta.id] = { ...cuenta, hijos: [] };
        });

        // Construir árbol
        cuentas.forEach(cuenta => {
            if (cuenta.cuentaPadreId) {
                if (mapa[cuenta.cuentaPadreId]) {
                    mapa[cuenta.cuentaPadreId].hijos.push(mapa[cuenta.id]);
                }
            } else {
                raices.push(mapa[cuenta.id]);
            }
        });

        return raices;
    }

    /**
     * Obtiene todas las cuentas hija de una cuenta
     * @param {Object} cuenta - Cuenta padre
     * @returns {Array} Array de todas las cuentas hija
     */
    static obtenerHijas(cuenta) {
        const hijas = [...(cuenta.subCuentas || [])];
        cuenta.subCuentas?.forEach(sub => {
            hijas.push(...CuentaContableHelper.obtenerHijas(sub));
        });
        return hijas;
    }

    /**
     * Valida una estructura de código contable
     * @param {String} codigo - Código a validar
     * @returns {Boolean} True si es válido
     */
    static validarCodigo(codigo) {
        const patronContable = /^\d+(\.\d+)*$/;
        return patronContable.test(codigo);
    }

    /**
     * Formatea un código contable
     * @param {String} codigo - Código sin formato
     * @returns {String} Código formateado
     */
    static formatearCodigo(codigo) {
        return codigo
            .replace(/\D/g, '')
            .split('')
            .map((digit, index) => {
                if (index > 0 && index % 2 === 0) {
                    return '.' + digit;
                }
                return digit;
            })
            .join('');
    }
}

/**
 * Utilidades para asientos contables
 */
export class AsientoContableHelper {
    /**
     * Valida que un asiento esté equilibrado
     * @param {Array} movimientos - Array de movimientos
     * @returns {Object} { equilibrado: Boolean, diferencia: Number }
     */
    static validarEquilibrio(movimientos) {
        const totalDebe = movimientos.reduce((sum, mov) => sum + (mov.debe || 0), 0);
        const totalHaber = movimientos.reduce((sum, mov) => sum + (mov.haber || 0), 0);
        const diferencia = Math.abs(totalDebe - totalHaber);

        return {
            equilibrado: diferencia < 0.01, // Permite pequeño margen por redondeo
            diferencia,
            totalDebe,
            totalHaber,
        };
    }

    /**
     * Calcula el saldo de una cuenta
     * @param {Array} movimientos - Array de movimientos de la cuenta
     * @returns {Number} Saldo (debe - haber)
     */
    static calcularSaldo(movimientos) {
        const totalDebe = movimientos.reduce((sum, mov) => sum + (mov.debe || 0), 0);
        const totalHaber = movimientos.reduce((sum, mov) => sum + (mov.haber || 0), 0);
        return totalDebe - totalHaber;
    }

    /**
     * Crea un asiento simple de dos líneas (Debe/Haber)
     * @param {Object} config - { descripcion, cuentaDebe, cuentaHaber, monto, referencia }
     * @returns {Object} Estructura de asiento
     */
    static crearAsientoSimple({
        descripcion,
        cuentaDebe,
        cuentaHaber,
        monto,
        referenciaId,
        tipoReferencia,
    }) {
        return {
            fecha: new Date().toISOString(),
            descripcion,
            referenciaId,
            tipoReferencia,
            movimientos: [
                {
                    cuentaContableId: cuentaDebe,
                    debe: monto,
                    haber: 0,
                },
                {
                    cuentaContableId: cuentaHaber,
                    debe: 0,
                    haber: monto,
                },
            ],
        };
    }

    /**
     * Agrupa movimientos por cuenta
     * @param {Array} movimientos - Array de movimientos
     * @returns {Object} Movimientos agrupados por cuentaId
     */
    static agruparPorCuenta(movimientos) {
        const agrupados = {};

        movimientos.forEach(mov => {
            const cuentaId = mov.cuentaContableId;
            if (!agrupados[cuentaId]) {
                agrupados[cuentaId] = {
                    cuentaId,
                    cuenta: mov.cuenta,
                    totalDebe: 0,
                    totalHaber: 0,
                    movimientos: [],
                };
            }
            agrupados[cuentaId].totalDebe += mov.debe || 0;
            agrupados[cuentaId].totalHaber += mov.haber || 0;
            agrupados[cuentaId].movimientos.push(mov);
        });

        return Object.values(agrupados);
    }

    /**
     * Obtiene el tipo de operación de un asiento
     * @param {String} tipoReferencia - Tipo de referencia
     * @returns {Object} { icono: String, color: String, label: String }
     */
    static obtenerTipoOperacion(tipoReferencia) {
        const tipos = {
            Venta: { icono: 'shopping_cart', color: 'primary', label: 'Venta' },
            Devolucion: { icono: 'assignment_return', color: 'warning', label: 'Devolución' },
            Ajuste: { icono: 'edit', color: 'info', label: 'Ajuste' },
            Cierre: { icono: 'lock', color: 'secondary', label: 'Cierre de Período' },
        };
        return tipos[tipoReferencia] || { icono: 'description', color: 'grey', label: tipoReferencia };
    }
}

/**
 * Utilidades para reportes contables
 */
export class ReporteContableHelper {
    /**
     * Calcula los saldos de cuentas
     * @param {Array} asientos - Array de asientos
     * @returns {Array} Array con saldos por cuenta
     */
    static calcularEstadoCuentas(asientos) {
        const estadoMap = {};

        asientos.forEach(asiento => {
            asiento.movimientos?.forEach(mov => {
                const cuentaId = mov.cuentaContableId;
                if (!estadoMap[cuentaId]) {
                    estadoMap[cuentaId] = {
                        id: cuentaId,
                        cuenta: mov.cuenta,
                        totalDebe: 0,
                        totalHaber: 0,
                    };
                }
                estadoMap[cuentaId].totalDebe += mov.debe || 0;
                estadoMap[cuentaId].totalHaber += mov.haber || 0;
            });
        });

        return Object.values(estadoMap).map(estado => ({
            ...estado,
            saldo: estado.totalDebe - estado.totalHaber,
        }));
    }

    /**
     * Agrupa asientos por período (mes)
     * @param {Array} asientos - Array de asientos
     * @returns {Array} Asientos agrupados por mes
     */
    static agruparPorMes(asientos) {
        const agrupados = {};

        asientos.forEach(asiento => {
            const fecha = new Date(asiento.fecha);
            const mes = fecha.toLocaleDateString('es-ES', { year: 'numeric', month: '2-digit' });

            if (!agrupados[mes]) {
                agrupados[mes] = {
                    mes,
                    cantidad: 0,
                    totalDebe: 0,
                    totalHaber: 0,
                    asientos: [],
                };
            }

            agrupados[mes].cantidad += 1;
            agrupados[mes].asientos.push(asiento);
            asiento.movimientos?.forEach(mov => {
                agrupados[mes].totalDebe += mov.debe || 0;
                agrupados[mes].totalHaber += mov.haber || 0;
            });
        });

        return Object.values(agrupados).sort((a, b) => b.mes.localeCompare(a.mes));
    }

    /**
     * Agrupa asientos por tipo de referencia
     * @param {Array} asientos - Array de asientos
     * @returns {Object} Asientos agrupados por tipo
     */
    static agruparPorTipo(asientos) {
        const agrupados = {};

        asientos.forEach(asiento => {
            const tipo = asiento.tipoReferencia;
            if (!agrupados[tipo]) {
                agrupados[tipo] = {
                    tipo,
                    cantidad: 0,
                    totalDebe: 0,
                    totalHaber: 0,
                    monto: 0,
                };
            }

            agrupados[tipo].cantidad += 1;
            let montoAsiento = 0;
            asiento.movimientos?.forEach(mov => {
                agrupados[tipo].totalDebe += mov.debe || 0;
                agrupados[tipo].totalHaber += mov.haber || 0;
                montoAsiento += mov.debe || mov.haber || 0;
            });
            agrupados[tipo].monto += montoAsiento;
        });

        return Object.values(agrupados);
    }

    /**
     * Calcula el balance de prueba
     * @param {Array} estadoCuentas - Array de cuentas con saldos
     * @returns {Object} Balance con totales
     */
    static calcularBalancePrueba(estadoCuentas) {
        const totalDebe = estadoCuentas.reduce((sum, cuenta) => sum + cuenta.totalDebe, 0);
        const totalHaber = estadoCuentas.reduce((sum, cuenta) => sum + cuenta.totalHaber, 0);
        const diferencia = Math.abs(totalDebe - totalHaber);

        return {
            cuentas: estadoCuentas,
            totalDebe,
            totalHaber,
            diferencia,
            equilibrado: diferencia < 0.01,
        };
    }

    /**
     * Genera un resumen ejecutivo
     * @param {Array} asientos - Array de asientos
     * @returns {Object} Resumen de datos clave
     */
    static generarResumenEjecutivo(asientos) {
        const estadoCuentas = ReporteContableHelper.calcularEstadoCuentas(asientos);
        const totalDebe = estadoCuentas.reduce((sum, c) => sum + c.totalDebe, 0);
        const totalHaber = estadoCuentas.reduce((sum, c) => sum + c.totalHaber, 0);

        return {
            totalAsientos: asientos.length,
            totalCuentas: estadoCuentas.length,
            totalDebe,
            totalHaber,
            diferencia: totalDebe - totalHaber,
            equilibrado: Math.abs(totalDebe - totalHaber) < 0.01,
            movimientoPorTipo: ReporteContableHelper.agruparPorTipo(asientos),
            movimientoPorMes: ReporteContableHelper.agruparPorMes(asientos),
            cuentasConMayorSaldo: estadoCuentas
                .sort((a, b) => Math.abs(b.saldo) - Math.abs(a.saldo))
                .slice(0, 10),
        };
    }
}

/**
 * Utilidades de formato y conversión
 */
export class ContabilidadFormato {
    /**
     * Formatea un número como moneda
     * @param {Number} valor - Valor a formatear
     * @param {String} idioma - Código de idioma (default: 'es-ES')
     * @returns {String} Número formateado como moneda
     */
    static formatearMoneda(valor, idioma = 'es-ES') {
        if (!valor && valor !== 0) return '-';
        return parseFloat(valor).toLocaleString(idioma, {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        });
    }

    /**
     * Formatea una fecha
     * @param {String|Date} fecha - Fecha a formatear
     * @param {String} idioma - Código de idioma (default: 'es-ES')
     * @returns {String} Fecha formateada
     */
    static formatearFecha(fecha, idioma = 'es-ES') {
        if (!fecha) return '-';
        const date = new Date(fecha);
        return date.toLocaleDateString(idioma, {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        });
    }

    /**
     * Formatea fecha y hora
     * @param {String|Date} fecha - Fecha a formatear
     * @param {String} idioma - Código de idioma (default: 'es-ES')
     * @returns {String} Fecha y hora formateadas
     */
    static formatearFechaHora(fecha, idioma = 'es-ES') {
        if (!fecha) return '-';
        const date = new Date(fecha);
        return date.toLocaleString(idioma, {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
        });
    }

    /**
     * Redondea un número a 2 decimales
     * @param {Number} valor - Valor a redondear
     * @returns {Number} Valor redondeado
     */
    static redondear(valor) {
        return Math.round(valor * 100) / 100;
    }

    /**
     * Convierte objeto a CSV
     * @param {Array} datos - Array de objetos
     * @param {Array} columnas - Nombres de columnas
     * @returns {String} Datos en formato CSV
     */
    static convertirACSV(datos, columnas) {
        const encabezados = columnas.join(',');
        const filas = datos.map(fila =>
            columnas.map(col => {
                const valor = fila[col];
                return typeof valor === 'string' && valor.includes(',') ? `"${valor}"` : valor;
            }).join(',')
        );
        return [encabezados, ...filas].join('\n');
    }
}

export default {
    CuentaContableHelper,
    AsientoContableHelper,
    ReporteContableHelper,
    ContabilidadFormato,
};
