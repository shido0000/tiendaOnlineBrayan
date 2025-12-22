import { ContabilidadAPI } from "src/assets/js/util/contabilidadAPI";

export default {
    namespaced: true,

    state: {
        cuentas: [],
        asientos: [],
        cuentaSeleccionada: null,
        asientoSeleccionado: null,
        cargando: false,
        error: null,
        estadoCuentas: [],
        resumenMovimientos: [],
    },

    getters: {
        getCuentas: (state) => state.cuentas,
        getAsientos: (state) => state.asientos,
        getCuentaSeleccionada: (state) => state.cuentaSeleccionada,
        getAsientoSeleccionado: (state) => state.asientoSeleccionado,
        getCargando: (state) => state.cargando,
        getError: (state) => state.error,
        getEstadoCuentas: (state) => state.estadoCuentas,
        getResumenMovimientos: (state) => state.resumenMovimientos,

        // Getters calculados
        getTotalDebe: (state) => {
            return state.estadoCuentas.reduce((sum, cuenta) => sum + (cuenta.totalDebe || 0), 0);
        },
        getTotalHaber: (state) => {
            return state.estadoCuentas.reduce((sum, cuenta) => sum + (cuenta.totalHaber || 0), 0);
        },
        getDiferencia: (state, getters) => {
            return getters.getTotalDebe - getters.getTotalHaber;
        },
        estaEquilibrado: (state, getters) => {
            return Math.abs(getters.getDiferencia) < 0.01;
        },
    },

    mutations: {
        SET_CUENTAS(state, cuentas) {
            state.cuentas = cuentas;
        },
        ADD_CUENTA(state, cuenta) {
            state.cuentas.push(cuenta);
        },
        UPDATE_CUENTA(state, cuenta) {
            const index = state.cuentas.findIndex((c) => c.id === cuenta.id);
            if (index > -1) {
                state.cuentas.splice(index, 1, cuenta);
            }
        },
        DELETE_CUENTA(state, id) {
            state.cuentas = state.cuentas.filter((c) => c.id !== id);
        },

        SET_ASIENTOS(state, asientos) {
            state.asientos = asientos;
        },
        ADD_ASIENTO(state, asiento) {
            state.asientos.push(asiento);
        },
        SET_ASIENTO_SELECCIONADO(state, asiento) {
            state.asientoSeleccionado = asiento;
        },

        SET_CUENTA_SELECCIONADA(state, cuenta) {
            state.cuentaSeleccionada = cuenta;
        },

        SET_CARGANDO(state, value) {
            state.cargando = value;
        },

        SET_ERROR(state, error) {
            state.error = error;
        },

        SET_ESTADO_CUENTAS(state, estadoCuentas) {
            state.estadoCuentas = estadoCuentas;
        },

        SET_RESUMEN_MOVIMIENTOS(state, resumen) {
            state.resumenMovimientos = resumen;
        },

        CLEAR_ERROR(state) {
            state.error = null;
        },
    },

    actions: {
        // ===== CUENTAS CONTABLES =====

        async cargarCuentas({ commit }) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const cuentas = await ContabilidadAPI.getCuentas();
                commit("SET_CUENTAS", cuentas);
                return cuentas;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarCuentaById({ commit }, id) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const cuenta = await ContabilidadAPI.getCuentaById(id);
                commit("SET_CUENTA_SELECCIONADA", cuenta);
                return cuenta;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async crearCuenta({ commit }, data) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const cuenta = await ContabilidadAPI.crearCuenta(data);
                commit("ADD_CUENTA", cuenta);
                return cuenta;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async actualizarCuenta({ commit }, { id, data }) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const cuenta = await ContabilidadAPI.actualizarCuenta(id, data);
                commit("UPDATE_CUENTA", cuenta);
                return cuenta;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async eliminarCuenta({ commit }, id) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                await ContabilidadAPI.eliminarCuenta(id);
                commit("DELETE_CUENTA", id);
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        // ===== ASIENTOS CONTABLES =====

        async cargarAsientos({ commit }, params = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const asientos = await ContabilidadAPI.getAsientos(params);
                commit("SET_ASIENTOS", asientos.items || asientos);
                return asientos;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarAsientoById({ commit }, id) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const asiento = await ContabilidadAPI.getAsientoById(id);
                commit("SET_ASIENTO_SELECCIONADO", asiento);
                return asiento;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarAsientosEntreFechas({ commit }, { fechaInicio, fechaFin }) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const asientos = await ContabilidadAPI.getAsientosEntreFechas(fechaInicio, fechaFin);
                commit("SET_ASIENTOS", asientos);
                return asientos;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarAsientosPorTipo({ commit }, tipoReferencia) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const asientos = await ContabilidadAPI.getAsientosPorTipo(tipoReferencia);
                commit("SET_ASIENTOS", asientos);
                return asientos;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async crearAsiento({ commit }, data) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const asiento = await ContabilidadAPI.crearAsiento(data);
                commit("ADD_ASIENTO", asiento);
                return asiento;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        // ===== REPORTES =====

        async cargarEstadoCuentas({ commit }, { fechaInicio = null, fechaFin = null } = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const estadoCuentas = await ContabilidadAPI.getEstadoDeCuentas(fechaInicio, fechaFin);
                commit("SET_ESTADO_CUENTAS", estadoCuentas);
                return estadoCuentas;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarResumenMovimientos({ commit }, { fechaInicio = null, fechaFin = null } = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const resumen = await ContabilidadAPI.getResumenMovimientos(fechaInicio, fechaFin);
                commit("SET_RESUMEN_MOVIMIENTOS", resumen);
                return resumen;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async cargarBalanceDePrueba({ commit }, { fechaInicio = null, fechaFin = null } = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const balance = await ContabilidadAPI.getBalanceDePrueba(fechaInicio, fechaFin);
                return balance;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async exportarAsientosCSV({ commit }, params = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const csv = await ContabilidadAPI.exportarAsientosCSV(params);
                return csv;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        async exportarAsientosExcel({ commit }, params = {}) {
            try {
                commit("SET_CARGANDO", true);
                commit("CLEAR_ERROR");
                const excel = await ContabilidadAPI.exportarAsientosExcel(params);
                return excel;
            } catch (error) {
                commit("SET_ERROR", error.message);
                throw error;
            } finally {
                commit("SET_CARGANDO", false);
            }
        },

        clearError({ commit }) {
            commit("CLEAR_ERROR");
        },

        clearAsientoSeleccionado({ commit }) {
            commit("SET_ASIENTO_SELECCIONADO", null);
        },

        clearCuentaSeleccionada({ commit }) {
            commit("SET_CUENTA_SELECCIONADA", null);
        },
    },
};
