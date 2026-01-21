/**
 * EJEMPLOS DE USO DEL MÓDULO DE CONTABILIDAD
 * Este archivo contiene ejemplos prácticos de cómo usar el módulo
 */

import { ContabilidadAPI } from 'src/assets/js/util/contabilidadAPI';
import {
    CuentaContableHelper,
    AsientoContableHelper,
    ReporteContableHelper,
    ContabilidadFormato,
} from 'src/assets/js/util/contabilidadHelper';

// ============================================
// EJEMPLO 1: Crear estructura de cuentas
// ============================================
export async function ejemploCrearEstructuraCuentas() {
    try {
        // Crear cuentas padre
        const activos = await ContabilidadAPI.crearCuenta({
            codigo: '1',
            nombre: 'Activos',
            esActivo: true,
            esDeMovimiento: false,
            cuentaPadreId: null,
        });

        // Crear subcuenta
        const caja = await ContabilidadAPI.crearCuenta({
            codigo: '1.1',
            nombre: 'Caja',
            esActivo: true,
            esDeMovimiento: true,
            cuentaPadreId: activos.id,
        });

        // Crear cuenta de ingresos
        const ingresos = await ContabilidadAPI.crearCuenta({
            codigo: '4',
            nombre: 'Ingresos',
            esActivo: true,
            esDeMovimiento: false,
            cuentaPadreId: null,
        });

        const ingresosVentas = await ContabilidadAPI.crearCuenta({
            codigo: '4.1',
            nombre: 'Ingresos por Ventas',
            esActivo: true,
            esDeMovimiento: true,
            cuentaPadreId: ingresos.id,
        });

        return { activos, caja, ingresos, ingresosVentas };
    } catch (error) {
        console.error('Error al crear estructura:', error);
    }
}

// ============================================
// EJEMPLO 2: Crear asiento simple (Venta)
// ============================================
export async function ejemploCrearAsientoVenta(ventaId, monto, cuentaCajaId, cuentaIngresosId) {
    try {
        // Opción 1: Usar helper para crear asiento simple
        const asiento = AsientoContableHelper.crearAsientoSimple({
            descripcion: 'Venta de productos',
            cuentaDebe: cuentaCajaId,      // Aumenta Caja
            cuentaHaber: cuentaIngresosId, // Aumenta Ingresos
            monto,
            referenciaId: ventaId,
            tipoReferencia: 'Venta',
        });

        // Validar equilibrio antes de crear
        const equilibrio = AsientoContableHelper.validarEquilibrio(asiento.movimientos);
        if (!equilibrio.equilibrado) {
            console.error('Asiento desbalanceado:', equilibrio);
            return;
        }

        // Crear en el backend
        const resultado = await ContabilidadAPI.crearAsiento(asiento);
        return resultado;
    } catch (error) {
        console.error('Error al crear asiento:', error);
    }
}

// ============================================
// EJEMPLO 3: Crear asiento complejo (Devolución)
// ============================================
export async function ejemploCrearAsientoDevolucion(
    devolucionId,
    monto,
    cuentaCajaId,
    cuentaIngresosId,
    cuentaDevoluciones
) {
    try {
        const asiento = {
            fecha: new Date().toISOString(),
            descripcion: 'Devolución de productos',
            referenciaId: devolucionId,
            tipoReferencia: 'Devolucion',
            movimientos: [
                {
                    cuentaContableId: cuentaDevoluciones,
                    debe: monto, // Gasto por devolución
                    haber: 0,
                },
                {
                    cuentaContableId: cuentaIngresosId,
                    debe: 0,
                    haber: monto, // Reduce ingresos
                },
                {
                    cuentaContableId: cuentaCajaId,
                    debe: 0,
                    haber: monto, // Sale efectivo
                },
            ],
        };

        // Validar y crear
        const equilibrio = AsientoContableHelper.validarEquilibrio(asiento.movimientos);

        return await ContabilidadAPI.crearAsiento(asiento);
    } catch (error) {
        console.error('Error al crear devolución:', error);
    }
}

// ============================================
// EJEMPLO 4: Consultar y formatear datos
// ============================================
export async function ejemploConsultarAsientos() {
    try {
        // Obtener asientos con paginación
        const resultado = await ContabilidadAPI.getAsientos({
            pageNumber: 1,
            pageSize: 10,
            fechaInicio: '2025-01-01',
            fechaFin: '2025-01-31',
        });


        // Procesar cada asiento
        resultado.items.forEach(asiento => {
            // Agrupar movimientos por cuenta
            const movimientosPorCuenta = AsientoContableHelper.agruparPorCuenta(
                asiento.movimientos
            );

            movimientosPorCuenta.forEach(grupo => {
                console.log(`
          Cuenta: ${grupo.cuenta.nombre} (${grupo.cuenta.codigo})
          Total Debe: ${ContabilidadFormato.formatearMoneda(grupo.totalDebe)}
          Total Haber: ${ContabilidadFormato.formatearMoneda(grupo.totalHaber)}
          Saldo: ${ContabilidadFormato.formatearMoneda(grupo.totalDebe - grupo.totalHaber)}
        `);
            });
        });
    } catch (error) {
        console.error('Error al consultar asientos:', error);
    }
}

// ============================================
// EJEMPLO 5: Generar reportes
// ============================================
export async function ejemploGenerarReporte() {
    try {
        // Obtener todos los asientos del período
        const asientos = await ContabilidadAPI.getAsientos({
            pageSize: 1000,
            fechaInicio: '2025-01-01',
            fechaFin: '2025-01-31',
        });

        // Generar resumen ejecutivo
        const resumen = ReporteContableHelper.generarResumenEjecutivo(asientos.items);

        console.log('=== RESUMEN EJECUTIVO ===');
        console.log(`Total de Asientos: ${resumen.totalAsientos}`);
        console.log(`Total de Cuentas: ${resumen.totalCuentas}`);
        console.log(`Total Debe: ${ContabilidadFormato.formatearMoneda(resumen.totalDebe)}`);
        console.log(`Total Haber: ${ContabilidadFormato.formatearMoneda(resumen.totalHaber)}`);
        console.log(`Estado: ${resumen.equilibrado ? '✓ Equilibrado' : '✗ Desbalanceado'}`);

        console.log('\n=== MOVIMIENTOS POR TIPO ===');
        resumen.movimientoPorTipo.forEach(tipo => {
            console.log(`${tipo.tipo}: ${tipo.cantidad} asientos (${ContabilidadFormato.formatearMoneda(tipo.monto)})`);
        });

        console.log('\n=== CUENTAS CON MAYOR SALDO ===');
        resumen.cuentasConMayorSaldo.forEach(cuenta => {
            console.log(`${cuenta.cuenta.nombre}: ${ContabilidadFormato.formatearMoneda(cuenta.saldo)}`);
        });

        return resumen;
    } catch (error) {
        console.error('Error al generar reporte:', error);
    }
}

// ============================================
// EJEMPLO 6: Usar en un componente Vue
// ============================================
export const ejemploComponenteVue = `
<template>
  <div>
    <h2>Crear Asiento de Venta</h2>

    <form @submit.prevent="crearAsiento">
      <input
        v-model="monto"
        type="number"
        placeholder="Monto"
      />

      <button type="submit" :disabled="cargando">
        {{ cargando ? 'Creando...' : 'Crear' }}
      </button>
    </form>

    <div v-if="error" class="error">{{ error }}</div>
    <div v-if="exito" class="exito">Asiento creado correctamente</div>
  </div>
</template>

<script>
import { ContabilidadAPI } from 'src/assets/js/util/contabilidadAPI';
import { AsientoContableHelper } from 'src/assets/js/util/contabilidadHelper';

export default {
  data() {
    return {
      monto: 0,
      cargando: false,
      error: null,
      exito: false,
    };
  },
  methods: {
    async crearAsiento() {
      try {
        this.cargando = true;
        this.error = null;

        // Crear asiento simple
        const asiento = AsientoContableHelper.crearAsientoSimple({
          descripcion: 'Venta de productos',
          cuentaDebe: 'ID_CUENTA_CAJA',
          cuentaHaber: 'ID_CUENTA_INGRESOS',
          monto: this.monto,
          referenciaId: this.ventaId,
          tipoReferencia: 'Venta',
        });

        // Validar equilibrio
        const equilibrio = AsientoContableHelper.validarEquilibrio(asiento.movimientos);
        if (!equilibrio.equilibrado) {
          throw new Error('El asiento no está equilibrado');
        }

        // Crear en backend
        await ContabilidadAPI.crearAsiento(asiento);
        this.exito = true;
        this.monto = 0;
      } catch (err) {
        this.error = err.message;
      } finally {
        this.cargando = false;
      }
    },
  },
};
</script>
`;

// ============================================
// EJEMPLO 7: Validaciones comunes
// ============================================
export const ejemplosValidaciones = {
    /**
     * Validar código de cuenta
     */
    validarCodigoCuenta(codigo) {
        if (!CuentaContableHelper.validarCodigo(codigo)) {
            return 'El código debe ser numérico separado por puntos (ej: 1.1.01)';
        }
        return null;
    },

    /**
     * Validar movimiento contable
     */
    validarMovimiento(movimiento) {
        const errores = [];

        if (!movimiento.cuentaContableId) {
            errores.push('Debe seleccionar una cuenta');
        }

        if (movimiento.debe < 0 || movimiento.haber < 0) {
            errores.push('Los montos no pueden ser negativos');
        }

        if (movimiento.debe > 0 && movimiento.haber > 0) {
            errores.push('Un movimiento no puede tener debe y haber simultáneamente');
        }

        if (movimiento.debe === 0 && movimiento.haber === 0) {
            errores.push('El monto debe ser mayor a cero');
        }

        return errores.length > 0 ? errores : null;
    },

    /**
     * Validar asiento completo
     */
    validarAsiento(asiento) {
        const errores = [];

        if (!asiento.descripcion?.trim()) {
            errores.push('La descripción es requerida');
        }

        if (!asiento.movimientos || asiento.movimientos.length < 2) {
            errores.push('Se requieren al menos 2 movimientos');
        }

        // Validar equilibrio
        const equilibrio = AsientoContableHelper.validarEquilibrio(asiento.movimientos);
        if (!equilibrio.equilibrado) {
            errores.push(
                `El asiento no está equilibrado. Diferencia: ${ContabilidadFormato.formatearMoneda(
                    equilibrio.diferencia
                )}`
            );
        }

        return errores.length > 0 ? errores : null;
    },
};

// ============================================
// EJEMPLO 8: Exportar datos
// ============================================
export async function ejemploExportarDatos() {
    try {
        // Obtener asientos
        const resultado = await ContabilidadAPI.getAsientos({
            pageSize: 1000,
            fechaInicio: '2025-01-01',
            fechaFin: '2025-01-31',
        });

        // Preparar datos para CSV
        const datosCSV = resultado.items.flatMap(asiento =>
            asiento.movimientos.map(mov => ({
                fecha: ContabilidadFormato.formatearFecha(asiento.fecha),
                descripcion: asiento.descripcion,
                cuenta: mov.cuenta.nombre,
                codigo: mov.cuenta.codigo,
                debe: mov.debe,
                haber: mov.haber,
            }))
        );

        // Convertir a CSV
        const csv = ContabilidadFormato.convertirACSV(datosCSV, [
            'fecha',
            'descripcion',
            'cuenta',
            'codigo',
            'debe',
            'haber',
        ]);

        // Descargar
        const blob = new Blob([csv], { type: 'text/csv' });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'asientos_contables.csv';
        a.click();
    } catch (error) {
        console.error('Error al exportar:', error);
    }
}

export default {
    ejemploCrearEstructuraCuentas,
    ejemploCrearAsientoVenta,
    ejemploCrearAsientoDevolucion,
    ejemploConsultarAsientos,
    ejemploGenerarReporte,
    ejemploExportarDatos,
    ejemplosValidaciones,
};
