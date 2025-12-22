import axios from "axios";

/**
 * Servicio de API para gestión de contabilidad
 */
const API_BASE = "/api";

export const ContabilidadAPI = {
    // ===== CUENTAS CONTABLES =====

    /**
     * Obtiene todas las cuentas contables
     */
    async getCuentas(params = {}) {
        try {
            const response = await axios.get(`${API_BASE}/CuentasContables`, { params });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene una cuenta contable por ID
     */
    async getCuentaById(id) {
        try {
            const response = await axios.get(`${API_BASE}/CuentasContables/${id}`);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Crea una nueva cuenta contable
     */
    async crearCuenta(data) {
        try {
            const response = await axios.post(`${API_BASE}/CuentasContables`, data);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Actualiza una cuenta contable
     */
    async actualizarCuenta(id, data) {
        try {
            const response = await axios.put(`${API_BASE}/CuentasContables/${id}`, data);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Elimina una cuenta contable
     */
    async eliminarCuenta(id) {
        try {
            const response = await axios.delete(`${API_BASE}/CuentasContables/${id}`);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    // ===== ASIENTOS CONTABLES =====

    /**
     * Obtiene todos los asientos contables con paginación
     */
    async getAsientos(params = {}) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables`, { params });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene un asiento contable por ID
     */
    async getAsientoById(id) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables/${id}`);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Crea un nuevo asiento contable
     */
    async crearAsiento(data) {
        try {
            const response = await axios.post(`${API_BASE}/AsientosContables`, data);
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene asientos por rango de fechas
     */
    async getAsientosEntreFechas(fechaInicio, fechaFin) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables`, {
                params: {
                    fechaInicio,
                    fechaFin,
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene asientos por tipo de referencia
     */
    async getAsientosPorTipo(tipoReferencia) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables`, {
                params: {
                    tipoReferencia,
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    // ===== REPORTES =====

    /**
     * Obtiene el estado de cuentas
     */
    async getEstadoDeCuentas(fechaInicio = null, fechaFin = null) {
        try {
            const params = {};
            if (fechaInicio) params.fechaInicio = fechaInicio;
            if (fechaFin) params.fechaFin = fechaFin;

            const response = await axios.get(`${API_BASE}/AsientosContables/reportes/estado-cuentas`, {
                params,
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene el resumen de movimientos
     */
    async getResumenMovimientos(fechaInicio = null, fechaFin = null) {
        try {
            const params = {};
            if (fechaInicio) params.fechaInicio = fechaInicio;
            if (fechaFin) params.fechaFin = fechaFin;

            const response = await axios.get(`${API_BASE}/AsientosContables/reportes/resumen`, {
                params,
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene balance de prueba
     */
    async getBalanceDePrueba(fechaInicio = null, fechaFin = null) {
        try {
            const params = {};
            if (fechaInicio) params.fechaInicio = fechaInicio;
            if (fechaFin) params.fechaFin = fechaFin;

            const response = await axios.get(`${API_BASE}/AsientosContables/reportes/balance-prueba`, {
                params,
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Exporta asientos a CSV
     */
    async exportarAsientosCSV(params = {}) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables/exportar/csv`, {
                params,
                responseType: "blob",
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Exporta asientos a Excel
     */
    async exportarAsientosExcel(params = {}) {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables/exportar/excel`, {
                params,
                responseType: "blob",
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    /**
     * Obtiene el total de debe y haber
     */
    async getTotalesDyH() {
        try {
            const response = await axios.get(`${API_BASE}/AsientosContables/reportes/totales`);
            return response.data;
        } catch (error) {
            throw error;
        }
    },
};

export default ContabilidadAPI;
