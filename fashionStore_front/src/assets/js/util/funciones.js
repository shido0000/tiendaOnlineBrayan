/* eslint-disable no-unreachable */
// IMPORT
import { api, apiDatosInicio } from "src/boot/axios";
import { ref, reactive, isRef } from "vue";
import { Error, Success } from "./notify";
import {
    Error_Notify_DeletingElementIsRelated,
    Error_Notify_ErrorImprimir,
    Error_Notify_NotFound404,
} from "../../js/util/dicc_notify";

const title = ref("");

const enviarStorage = (key, value) => {
    if (value !== "") {
        localStorage.setItem(key, JSON.stringify(value));
    }
};

const recibirStorage = (key) => {
    const v = JSON.parse(localStorage.getItem(key));
    return v;
};

// #region LOAD GET
// Funcion para cargar todos los datos de una tabla
const loadGet = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r?.data?.result?.elementos
                ? r?.data?.result?.elementos
                : r?.data?.result;
        })
        .catch((error) => {
            if (
                error?.response?.status === 404 ||
                error?.response?.status === 400
            ) {
                return;
            }
            if (error?.response?.data?.errorMessage) {
                Error(error.response.data.errorMessage);
            } else {
                error?.response === undefined
                    ? Error(error?.message)
                    : Error(error?.response?.data?.mensajeError);
            }
        });
};


const loadGetDatosInicio = async (endpoint) => {
    return await apiDatosInicio
        .get(endpoint)
        .then((r) => {
            return r?.data?.result?.elementos
                ? r?.data?.result?.elementos
                : r?.data;
        })
        .catch((error) => {
            if (
                error?.response?.status === 404 ||
                error?.response?.status === 400
            ) {
                return;
            }
            if (error?.response?.data?.errorMessage) {
                Error(error.response.data.errorMessage);
            } else {
                error?.response === undefined
                    ? Error(error?.message)
                    : Error(error?.response?.data?.mensajeError);
            }
        });
};

// #region LOAD GET HASTA DATA
const loadGetHastaData = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};
const loadGetHastaDataPersonalizado = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.errorMessage);
        });
};

const loadGetDisponibilidadCupos = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.errorMessage);
        });
};

// #region LOAD GET TIPO TARIFA TIEMPO
const loadGetTipoTarifaTiempo = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD GET ALL ACTIVOS
// Funcion para cargar todos los datos de una tabla
const loadGetTodosActivos = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOADGET CODIGO DESCRIPCION
// Funcion para cargar todos los datos de una tabla
const loadGetParaCodigoDescripcion = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD GET X ID
// Funcion para obtener el objeto por id seleccionado
const loadGetPorId = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data.result;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD LIST FILTRO
// Funcion para obtener una lista filtrada pasandole el endpoint y el arreglo
const loadListaFiltro = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data.result.elementos;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD GET USO X ID
// Funcion para obtener si esta en uso por id seleccionado
const loadGetEnUsoPorId = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD SELECT LIST
// Funcion que se utiliza para cargar los datos en las opciones de los select
const loadSelectList = async (endpoint) => {
    return await api
        .get(endpoint)
        .then((r) => {
            return r.data.result;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region LOAD
// Funcion para obtener una lista pasandole el endpoint y el arreglo
const load = async (endpoint, lista) => {
    await api
        .get(endpoint)
        .then((r) => {
            lista.value = r.data.result.elementos;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// #region SAVE DATA
// Funcion para guardar y editar
const saveData = async (endpoint, objeto, load, close, dialogLoad) => {
    dialogLoad.value = true;
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    if (objeto.id) {
        return await api
            .put(`/${endpoint}/${objeto.id}`, objeto)
            .then(async (response) => {
                respuesta.resultado = response;
                await load();
                await close();
                dialogLoad.value = false;
                Success("El elemento ha sido modificado correctamente");
                return respuesta;
            })
            .catch(async (error) => {
                error?.response?.data?.errorMessage
                    ? (respuesta.mensajeError =
                        error?.response?.data?.errorMessage)
                    : (respuesta.mensajeError = error);
                //await load();
                // await close();
                dialogLoad.value = false;
                return respuesta;
            })
            .finally(() => {
                dialogLoad.value = false;
            });
    } else {
        return await api
            .post(`/${endpoint}`, objeto)
            .then(async (response) => {
                respuesta.resultado = response;
                await load();
                await close();
                dialogLoad.value = false;
                Success.call(this, "El elemento ha sido creado correctamente");
                return respuesta;
            })
            .catch(async (error) => {
                !!error?.response?.data?.errorMessage
                    ? (respuesta.mensajeError =
                        error?.response?.data?.errorMessage ?? "")
                    : (respuesta.mensajeError = error);
                // await load();
                // await close();
                dialogLoad.value = false;
                return respuesta;
            })
            .finally(() => {
                dialogLoad.value = false;
            });
    }
};

const saveDataParaObjetosConFotos = async (endpoint, data, load, close, dialogLoad, esFormData = false, id = null) => {
    dialogLoad.value = true
    let respuesta = { resultado: null, mensajeError: null }

    const config = esFormData
        ? { headers: { 'Content-Type': 'multipart/form-data' } }
        : {}

    if (id) {
        return await api
            .put(`/${endpoint}/${id}`, data, config)
            .then(async (response) => {
                respuesta.resultado = response
                await load()
                await close()
                Success("El elemento ha sido modificado correctamente")
                return respuesta
            })
            .catch((error) => {
                respuesta.mensajeError = error?.response?.data?.errorMessage || error
                return respuesta
            })
            .finally(() => { dialogLoad.value = false })
    } else {
        return await api
            .post(`/${endpoint}`, data, config)
            .then(async (response) => {
                respuesta.resultado = response
                await load()
                await close()
                Success("El elemento ha sido creado correctamente")
                return respuesta
            })
            .catch((error) => {
                respuesta.mensajeError = error?.response?.data?.errorMessage || error
                return respuesta
            })
            .finally(() => { dialogLoad.value = false })
    }
}


// Funcion para guardar y editar sin cerrar Modal
const saveDataSinCerrar = async (endpoint, objeto, load, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;
    if (objeto.id) {
        return await api
            .put(`/${endpoint}/${objeto.id}`, objeto)
            .then(async (response) => {
                respuesta.resultado = response;
                await load();

                Success("El elemento ha sido modificado correctamente");
                return respuesta;
            })
            .catch(async (error) => {
                error?.response?.data?.errorMessage
                    ? (respuesta.mensajeError =
                        error?.response?.data?.errorMessage)
                    : (respuesta.mensajeError = error);
                // await load();
                // await close();

                return respuesta;
            })
            .finally((dialogLoad.value = false));
    } else {
        return await api
            .post(`/${endpoint}`, objeto)
            .then(async (response) => {
                respuesta.resultado = response;
                await load();

                dialogLoad.value = false;
                Success.call(this, "El elemento ha sido creado correctamente");
                return respuesta;
            })
            .catch(async (error) => {
                !!error?.response?.data?.errorMessage
                    ? (respuesta.mensajeError =
                        error?.response?.data?.errorMessage ?? "")
                    : (respuesta.mensajeError = error);
                // await load();
                // await close();
                dialogLoad.value = false;
                return respuesta;
            })
            .finally((dialogLoad.value = false));
    }
};

const saveDataCheckOut = async (endpoint, objeto, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response;
            // await load();

            dialogLoad.value = false;
            Success.call(this, "Operación realizada con éxito");
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataPronosticoEnviarObjeto = async (endpoint, objeto, dialogLoad) => {
    dialogLoad.value = true;
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    return await api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response.data;

            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);

            return respuesta;
        })
        .finally(() => {
            dialogLoad.value = false;
        });
};

// Funcion para guardar y editar
const saveDataCalculosPronosticos = async (endpoint) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    // dialogLoad.value = true;
    // dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response.data;
            Success.call(this, "La operación se ha ejecutado correctamente");

            //       dialogLoad.value = false;
            //       dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);

            return respuesta;
        });
};

const saveDataCalculosPronosticosConGet = async (endpoint) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    // dialogLoad.value = true;
    // dialogLoad.value = true;

    return await api
        .get(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response.data;
            Success.call(this, "La operación se ha ejecutado correctamente");

            //       dialogLoad.value = false;
            //       dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);

            return respuesta;
        });
};

// Funcion para guardar y editar
const saveDataOperaciones = async (endpoint, close, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(this, "La operación se ha ejecutado correctamente");
            //await load();
            await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataOperacionesSoloRuta = async (endpoint, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response;

            //await load();

            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataOperacionesSinClose = async (endpoint, dialogLoad, load) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(this, "La operación se ha ejecutado correctamente");
            await load();

            //   dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            //dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataOperacionesAnularCheckOut = async (endpoint, load) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(this, "La operación se ha ejecutado correctamente");
            await load();

            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);

            return respuesta;
        });
};

const saveDataOperaciones2 = async (endpoint, lista, close, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`, lista)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(this, "La operación se ha ejecutado correctamente");
            //await load();
            await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataConMensajeModificable = async (
    endpoint,
    objeto,
    load,
    close,
    dialogLoad,
    MensajeDado
) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    dialogLoad.value = true;
    if (objeto.id) {
        return await api
            .put(`/${endpoint}/${objeto.id}`, objeto)
            .then(async (response) => {
                respuesta.resultado = response;
                Success(MensajeDado);
                await load();
                await close();
                dialogLoad.value = false;
                return respuesta;
            })
            .catch(async (error) => {
                error?.response?.data?.errorMessage
                    ? (respuesta.mensajeError =
                        error?.response?.data?.errorMessage)
                    : (respuesta.mensajeError = error);
                await load();
                await close();
                dialogLoad.value = false;
                return respuesta;
            })
            .finally((dialogLoad.value = false));
    }
};

const saveDataCambiarEstadoReservas = async (
    endpoint,
    objeto,
    load,
    close,
    dialogLoad,
    MensajeDado
) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response;
            Success(MensajeDado);
            await load();
            await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            error?.response?.data?.errorMessage
                ? (respuesta.mensajeError = error?.response?.data?.errorMessage)
                : (respuesta.mensajeError = error);
            await load();
            await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const saveDataDuplicado = async (endpoint, id, load) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    //dialogLoad.value = true;

    return await api
        .post(`/${endpoint}?reservaId=${id}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(
                this,
                `El elemento ha sido duplicado correctamente, el número de la nueva reserva es ${respuesta.resultado.data.numeroReserva}`
            );
            await load();
            //  dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error.response.data.errorMessage
                ? (respuesta.mensajeError = error.response.data.errorMessage)
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            return respuesta;
        });
};

const saveDataDuplicadoRooming = async (endpoint, id, load, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };

    //dialogLoad.value = true;

    return await api
        .post(`/${endpoint}?roomingId=${id}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(
                this,
                `El elemento ha sido duplicado correctamente, el número de la nueva rooming es ${respuesta.resultado.data.numeroRooming}`
            );
            await load();
            //  dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            !!error.response.data.errorMessage
                ? (respuesta.mensajeError = error.response.data.errorMessage)
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            //dialogLoad.value = false;
            return respuesta;
        });
    // .finally((dialogLoad.value = false));
};

const saveDataAutomaticamente = async (endpoint, load, dialogLoad) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });

    dialogLoad.value = true;
    return await api
        .post(`/${endpoint}`)
        .then(async (response) => {
            respuesta.resultado = response;
            Success.call(this, "Los elementos han sido creados correctamente");
            await load();
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            let mensajeError =
                "Error en los datos insertados: \r\n -- Codigo: Ya existe un Codigo con el mismo texto. \r\n -- Descripcion: Ya existe una Descripción con el mismo texto. ";
            respuesta.mensajeError = error;
            let respuestaAMostrar =
                respuesta.mensajeError.response.data.errorMessage;
            if (
                respuestaAMostrar.includes("Codigo") ||
                respuestaAMostrar.includes("Descripcion")
            ) {
                Error(
                    "No puede crear una extensión automáticamente para la habitación porque ya existe un código o descripción de la extensión con el mismo código de la habitación."
                );
            } else {
                Error(respuesta.mensajeError.response.data.errorMessage);
            }

            await load();
            dialogLoad.value = false;
            return respuesta;
        });
};

// #region ELIMINAR ELEMENTOS
// Funcion de Eliminar
const eliminarElemento = async (endpoint, id, load, dialogLoad) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });
    dialogLoad.value = true;
    return await api
        .delete(`/${endpoint}/${id}`)
        .then(async () => {
            setTimeout(async () => {
                await load();
                dialogLoad.value = false;
                Success.call(
                    this,
                    "El elemento ha sido eliminado correctamente"
                );
            });
            return respuesta;
        })
        .catch((error) => {
            if (
                error?.response?.data?.errorMessage ===
                ("Este elemento no puede ser eliminado debido a que otro depende de el" ||
                    "Ocurrió un error al eliminar porque está asociado a otros registros.")
            )
                Error(Error_Notify_DeletingElementIsRelated);
            else Error("Ocurrio un error al eliminar el elemento");
            respuesta.mensajeError = error ?? null;
            dialogLoad.value = false;
            return respuesta;
        });
};

const eliminarElementoGenerico = async (endpoint, id, load, dialogLoad) => {
    let respuesta = {
        resultado: null,
        mensajeError: null,
    };
    dialogLoad.value = true;
    return await api
        .delete(`/${endpoint}/${id}`)
        .then(async () => {
            setTimeout(async () => {
                await load();
                dialogLoad.value = false;
            });
            return respuesta;
        })
        .catch(async (error) => {
            !!error?.response?.data?.errorMessage
                ? (respuesta.mensajeError =
                    error?.response?.data?.errorMessage ?? "")
                : (respuesta.mensajeError = error);
            // await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        })
        .finally((dialogLoad.value = false));
};

const eliminarElementoSinLoad = async (endpoint, id, dialogLoad) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });
    dialogLoad.value = true;
    return await api
        .delete(`/${endpoint}/${id}`)
        .then(async () => {
            Success.call(this, "El elemento ha sido eliminado correctamente");
            setTimeout(async () => {
                dialogLoad.value = false;
            });
            return respuesta;
        })
        .catch((error) => {
            if (
                error?.response?.data?.errorMessage ===
                ("Este elemento no puede ser eliminado debido a que otro depende de el" ||
                    "Ocurrió un error al eliminar porque está asociado a otros registros.")
            )
                Error(Error_Notify_DeletingElementIsRelated);
            else Error("Ocurrio un error al eliminar el elemento");
            respuesta.mensajeError = error ?? null;
            dialogLoad.value = false;
            return respuesta;
        });
};

// #region OBTENER
// Funcion para Obtener datos por id esto se utiliza generalmente para cargar los datos del formulario
const obtener = async (endpoint, id, objeto, dialogLoad, dialog) => {
    dialogLoad.value = true;
    await api
        .get(`${endpoint}/${id}`)
        .then((r) => {
            Object.assign(objeto, r.data.result);
            title.value = `Editar ${endpoint}`;
            dialog ? (dialog.value = true) : null;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
    dialogLoad.value = false;
};

const obtenerHastaData = async (endpoint, id, objeto, dialogLoad, dialog) => {
    dialogLoad.value = true;
    await api
        .get(`${endpoint}/${id}`)
        .then((r) => {
            Object.assign(objeto, r.data);
            title.value = `Editar ${endpoint}`;
            dialog ? (dialog.value = true) : null;
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
    dialogLoad.value = false;
};

/**
 * Funcion para Obtener datos por id esto se utiliza para cargar un elemento de la BD no hay que pasarle dialog
 * @param {String} endpoint - Url de la petición
 * @param {String} id - Id del Elementos a obtener
 * @param {Object} objeto - Objeto al cual asignar el elemento obtenido
 * @returns {void}  No retorna valor, solo emite notificación de error
 **/
const obtenerElemento = async (endpoint, id, objeto) => {
    await api
        .get(`${endpoint}/${id}`)
        .then((r) => {
            Object.assign(objeto, r.data.result);
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

/**
 * Funcion para Obtener datos por id esto se utiliza para cargar un elemento de la BD no hay que pasarle dialog
 * @param {String} endpoint - Url de la petición
 * @param {String} id - Id del Elementos a obtener
 * @param {Object} objeto - Objeto al cual asignar el elemento obtenido
 * @returns {void}  No retorna valor, solo emite notificación de error
 **/
const obtenerElementoHastaData = async (endpoint, id, objeto) => {
    await api
        .get(`${endpoint}/${id}`)
        .then((r) => {
            Object.assign(objeto, r.data);
        })
        .catch((error) => {
            error.response === undefined
                ? Error(error.message)
                : Error(error.response.data.mensajeError);
        });
};

// Funcion para Obtener datos por id esto se utiliza generalmente para cargar los datos del formulario

// #region IS VALOR REPETIDO
// Funcion para verificar que los campos sean unicos
const isValorRepetido = (val, propiedad, objeto, items) => {
    return items.some(
        (e) =>
            String(e[propiedad]).toLowerCase() === String(val).toLowerCase() &&
            objeto.id !== e.id
    );
};

// #region CLOSE DIALOG
// Funcion para resetear los campos del dialogo y cerrarlo
const closeDialog = (objeto, objetoInicial, myForm, dialog) => {
    myForm.value.resetValidation();
    // Asignar propiedades de 'objetoInicial' a 'objeto'

    Object.assign(objeto, objetoInicial);

    delete objeto.id;

    // Sintaxis: delete objeto.propiedad o delete objeto['propiedad']
    // Retorno: Devuelve true si la propiedad se elimina con éxito, o false si la propiedad no se puede eliminar(por ejemplo, si es una propiedad no configurable)

    if (dialog && isRef(dialog)) {
        dialog.value = false;
    }
};

// Funcion para resetear los campos del dialogo y cerrarlo
const resetDialog = (objeto, objetoInicial, myForm) => {
    myForm.value.resetValidation();

    // Asignar propiedades de 'objetoInicial' a 'objeto'
    Object.assign(objeto, objetoInicial);

    delete objeto.id;

    // Sintaxis: delete objeto.propiedad o delete objeto['propiedad']
    // Retorno: Devuelve true si la propiedad se elimina con éxito, o false si la propiedad no se puede eliminar(por ejemplo, si es una propiedad no configurable)
};

// #region FILTER OPTIONS
// Funcion para Filtrado
const filterOptions = (val, update, options, filterField, array) => {
    update(
        () => {
            const needle = val ? val.toString().toLowerCase() : "";
            options = array.filter(
                (v) =>
                    v[filterField] &&
                    v[filterField].toString().toLowerCase().indexOf(needle) >= 0
            );
        },
        (ref) => {
            if (
                val !== "" &&
                ref.options.length > 0 &&
                ref.getOptionIndex() === -1
            ) {
                ref.moveOptionSelection(1, true); // enfoca la primera opción seleccionable y no actualiza el valor del input
                ref.toggleOption(ref.options[ref.optionIndex], true); // alterna la opción enfocada
            }
        }
    );
    return options;
};

const filterOptionsParaNumeros = (val, update, options, filterField, array) => {
    update(
        () => {
            const needle = val ? val.toString().toLowerCase() : "";
            options = array.filter(
                (v) =>
                    v[filterField] &&
                    v[filterField].toString().toLowerCase().indexOf(needle) >= 0
            );
        },
        (ref) => {
            if (
                val !== "" &&
                ref.options.length > 0 &&
                ref.getOptionIndex() === -1
            ) {
                ref.moveOptionSelection(1, true); // enfoca la primera opción seleccionable y no actualiza el valor del input
                ref.toggleOption(ref.options[ref.optionIndex], true); // alterna la opción enfocada
            }
        }
    );
    return options;
};

/* PROBANDO */

const filterOptions1 = (val, update, options, filterField, array) => {
    update(
        () => {
            const needle = val ? val.toLowerCase() : "";
            options = array.filter(
                (v) =>
                    v[filterField] &&
                    v[filterField].toLowerCase().includes(needle)
            );
        },
        (ref) => {
            if (
                val !== "" &&
                ref.options.length > 0 &&
                ref.getOptionIndex() === -1
            ) {
                ref.moveOptionSelection(1, true);
                // enfoca la primera opción seleccionable y no actualiza el valor del input
                ref.toggleOption(ref.options[ref.optionIndex], true); // alterna la opción enfocada
            }
        }
    );
    return options;
};

// #region VAL CARNET
// Función para validar carnet
const validarCarnet = (val) => {
    // Verificar que tenga 11 dígitos
    if (val.length !== 11) {
        return false;
    }

    // Obtener el número de mes
    const mes = parseInt(val.substr(2, 2), 10);

    // Verificar que el mes esté entre 01 y 12
    if (mes < 1 || mes > 12) {
        return false;
    }

    // Obtener el número de días permitidos para el mes
    const diasEnMes = new Date(2024, mes, 0).getDate();

    // Obtener los dígitos 5 y 6
    const diaStr = val.substr(4, 2);
    const dia = parseInt(diaStr, 10);

    // Verificar que el día esté dentro del rango válido
    if (dia < 1 || dia > diasEnMes) {
        return false;
    }

    // Si pasó todas las validaciones, es un val válido
    return true;
};

// #region VALIDARLETRAS&ESPACIOS
// Funcion para validar letras y caracteres especiales(¨Generalmente se utiliza para validar los nombres¨)
const validarLetrasYCaracteresEspeciales = (val) => {
    // Expresión regular que acepta letras y caracteres especiales, pero no números
    const regex = /^[A-Za-záéíóúÁÉÍÓÚñÑ\s#&]+$/;

    return regex.test(val);
};

// #region VALIDARCORREO
// Funcion para validar correo
const validarCorreo = (val) => {
    // Expresión regular para validar direcciones de correo electrónico
    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    return regex.test(val);
};

// #region VAL SOLONUMEROS
// Funcion para validar solo numeros
const validarSoloNumeros = (val) => {
    // Expresión regular que solo acepta números
    const regex = /^[0-9]+$/;

    return regex.test(val) || "Por favor, ingrese solo números";
};

// #region VAL NUMEROSDECIMALES
const validarSoloNumerosDecimales = (val) => {
    // Expresión regular que acepta números enteros y decimales
    const regex = /^[0-9]+(\.[0-9]+)?$/;

    return regex.test(val) || "Por favor, ingrese solo números";
};

// #region VAL URL
// Funcion para validar URL
const validarURL = (val) => {
    // Expresión regular para validar URLs
    const regex = /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([\/\w .-]*)*\/?$/;

    return regex.test(val);
};

// #region VAL URLPUERTOS
// Funcion para validar URL para puertos y localhost
const validarURLPuertos = (val) => {
    // Expresión regular mejorada para validar URLs, incluyendo localhost y puertos
    /*  const regex =
        /^(https?:\/\/)?((localhost:\d+)|([\da-z.-]+\.[a-z.]{2,6}))([\/\w .-]*)*\/?$/;

    return regex.test(val); */
    const regex =
        /^(https?:\/\/)?(localhost(:\d+)?|[\w.-]+\.[a-z]{2,6})(:\d+)?(\/[\w .-]*)*\/?$/;
    return regex.test(val);
};

// #region IMPRIMIRPORID
const imprimirPorId = async (id, endpoint) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.get(`${endpoint}ImprimirPorId/?id=${id}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        Error(Error_Notify_ErrorImprimir);
    }
};

const imprimirGeneralPorId = async (id, endpoint) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.get(`${endpoint}ImprimirPorId?id=${id}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        Error(Error_Notify_ErrorImprimir);
    }
};

// #region imprimir una lista de elementos por ID enviados
const imprimirPorIdLista = async (url, lista) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.post(`${url}`, lista, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

// #region IMPRIMIRTODOS
const imprimirTodos = async (endpoint) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.get(`${endpoint}Imprimir`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        Error(Error_Notify_ErrorImprimir);
    }
};

const imprimirTodosFiltrado = async (url, texto, esActivo) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"

        const { data } = await api.get(
            `${url}?texto=${texto}&esActivo=${esActivo}`,
            {
                responseType: "blob",
            }
        );
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

const imprimirTodosFiltradoConObjeto = async (url) => {
    try {
        const { data } = await api.get(`${url}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

// #region IMPRIMIRTODOS
const imprimir = async (url, id) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        let data;
        if (id) {
            ({ data } = await api.get(`${url}?Id=${id}`, {
                responseType: "blob",
            }));
        } else {
            ({ data } = await api.get(`${url}`, { responseType: "blob" }));
        }
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        Error(Error_Notify_ErrorImprimir);
    }
};

const imprimirTodosFiltradoSoloTexto = async (url, texto) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"

        const { data } = await api.get(`${url}?texto=${texto}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

const imprimirPronosticos = async (url) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.get(`${url}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

const imprimirPronosticosPost = async (url) => {
    try {
        // Realiza una solicitud HTTP al endpoint "/descargar/pdf"
        const { data } = await api.post(`${url}`, {
            responseType: "blob",
        });
        // Crea un objeto Blob a partir de los datos de la respuesta
        const blob = new Blob([data], { type: "application/pdf" });
        // Crea una URL para el objeto Blob
        const dir = window.URL.createObjectURL(blob);
        // Abre una nueva ventana o pestaña del navegador con el PDF
        window.open(dir, "_blank");
        return data;
    } catch (error) {
        throw new Error(error);
    }
};

const imprimirPronosticosConObjeto = async (url, objeto) => {
    try {
        // Cambiar a POST y enviar el objeto en el body
        const { data } = await api.post(
            url, // URL completa: "cliente-por-polo/imprimir"
            objeto, // <- Enviar el objeto como body (JSON)
            {
                responseType: "blob",
            }
        );

        // Crear y abrir PDF
        const blob = new Blob([data], { type: "application/pdf" });
        const dir = window.URL.createObjectURL(blob);
        window.open(dir, "_blank");

        return data;
    } catch (error) {
        // Manejar error del backend
        if (error.response?.data) {
            const errorBlob = new Blob([error.response.data], {
                type: "application/json",
            });
            const errorJson = JSON.parse(await errorBlob.text());
            Error(errorJson.errorMessage || "Error generando PDF");
        }
    }
};

const imprimirPronosticosConObjetoSoloParaNotaDebitoCredito = async (
    url,
    objeto
) => {
    try {
        // Cambiar a POST y enviar el objeto en el body
        const { data } = await api.post(
            url, // URL completa: "cliente-por-polo/imprimir"
            objeto, // <- Enviar el objeto como body (JSON)
            {
                responseType: "blob",
            }
        );

        // Crear y abrir PDF
        const blob = new Blob([data], { type: "application/pdf" });
        const dir = window.URL.createObjectURL(blob);
        window.open(dir, "_blank");

        return { ok: true, data };
    } catch (error) {
        // Manejar error del backend
        if (error.response?.data) {
            const errorBlob = new Blob([error.response.data], {
                type: "application/json",
            });
            const errorJson = JSON.parse(await errorBlob.text());
            Error(errorJson.errorMessage || "Error generando PDF");
            return { ok: false, error: errorJson.errorMessage };
        }
    }
};

const imprimirPronosticosConObjetoConMensajeSinElementos = async (
    url,
    objeto
) => {
    try {
        // Cambiar a POST y enviar el objeto en el body
        const { data } = await api.post(
            url, // URL completa: "cliente-por-polo/imprimir"
            objeto, // <- Enviar el objeto como body (JSON)
            {
                responseType: "blob",
            }
        );

        // Crear y abrir PDF

        const blob = new Blob([data], { type: "application/pdf" });
        const dir = window.URL.createObjectURL(blob);
        window.open(dir, "_blank");

        return data;
    } catch (error) {
        // Manejar error del backend
        if (error.response?.data) {
            const errorBlob = new Blob([error.response.data], {
                type: "application/json",
            });
            const errorJson = JSON.parse(await errorBlob.text());
            Error(errorJson.errorMessage || "Error generando PDF");
        } else {
            Error(error.message);
        }
    }
};

const preventInvalidInput = (event) => {
    const key = event.key;
    // Permitir solo números, punto decimal y teclas de control
    if (
        !/^[0-9.]$/.test(key) &&
        !["Backspace", "Tab", "ArrowLeft", "ArrowRight", "Delete"].includes(key)
    ) {
        event.preventDefault(); // Evita la entrada de caracteres no válidos
    }
};

const convertArrayElementsToString = (array, key) => {
    return array.map((element) => {
        if (typeof element === "object" && element !== null) {
            return String(element[key]);
        }
        return String(element);
    });
};

const validateDecimal = (value, totalNumber = 13, totalDigits = 8) => {
    // Expresión regular para permitir hasta 13 dígitos en total y hasta totalDigits decimales
    const regex = new RegExp(
        `^(?=\\d)(\\d{1,${totalNumber}})(\\.\\d{0,${totalDigits}})?$`
    );
    return (
        regex.test(value) ||
        `Formato no válido. Decimal (${totalNumber}, ${totalDigits}).`
    );
};

const UploadFile = async (file, url) => {
    try {
        const formData = new FormData();
        formData.append("archivo", file);

        const { data } = await api.post(`${url}`, formData, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
        Success("Datos del excel guardados correctamente");
        return data;
    } catch (error) {
        Error(error.response.data.errorMessage);
    }
};

/**
 * Obtiene los elementos que corresponden con la paginación definida
 * @param {string} endpoint - Url del método
 * @param {Object} params - Valores de la paginación
 **/
const loadGetPaginado = async (endpoint, objeto = {}) => {
    try {
        const response = await api.get(`/${endpoint}`, { params: objeto });
        return {
            elementos: Array.isArray(response.data.result.elementos)
                ? response.data.result.elementos
                : [],
            total: Number(response.data.result.cantidad) || 0,
        };
    } catch (error) {
        Error.call(this, error);
        return {
            elementos: [],
            total: 0,
        };
    }
};

const generarDatos = async (endpoint, objeto, dialogLoad) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response.data.result;
            respuesta.mensajeError = response.data.errorMessage;
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            error?.response?.data
                ? (respuesta.mensajeError = error?.response?.data)
                : (respuesta.mensajeError = error);
            //await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        });
};

const loguin = async (endpoint, objeto, dialogLoad) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });
    dialogLoad.value = true;

    return await api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response.data.result;
            respuesta.mensajeError = response.data.errorMessage;
            dialogLoad.value = false;
            return respuesta;
        })
        .catch(async (error) => {
            error?.response?.data
                ? (respuesta.mensajeError = error?.response?.data)
                : (respuesta.mensajeError = error);
            //await load();
            // await close();
            dialogLoad.value = false;
            return respuesta;
        });
};

const cerrarSesion = (endpoint, objeto) => {
    const respuesta = reactive({
        resultado: null,
        mensajeError: null,
    });

    return api
        .post(`/${endpoint}`, objeto)
        .then(async (response) => {
            respuesta.resultado = response.data;

            return respuesta;
        })
        .catch(async (error) => {
            error?.response?.data
                ? (respuesta.mensajeError = error?.response?.data)
                : (respuesta.mensajeError = error);
            //await load();
            // await close();
            return respuesta;
        });
};

export {
    saveData,
    closeDialog,
    resetDialog,
    load,
    obtener,
    validarSoloNumeros,
    validarSoloNumerosDecimales,
    validarCorreo,
    filterOptions,
    eliminarElemento,
    validarCarnet,
    isValorRepetido,
    validarLetrasYCaracteresEspeciales,
    loadGet,
    loadSelectList,
    loadListaFiltro,
    loadGetPorId,
    loadGetEnUsoPorId,
    loadGetParaCodigoDescripcion,
    validarURL,
    validarURLPuertos,
    imprimirPorId,
    imprimirTodos,
    imprimir,
    loadGetTodosActivos,
    loadGetTipoTarifaTiempo,
    imprimirTodosFiltrado,
    imprimirTodosFiltradoSoloTexto,
    obtenerElemento,
    saveDataAutomaticamente,
    enviarStorage,
    recibirStorage,
    loadGetHastaData,
    filterOptions1,
    saveDataDuplicado,
    saveDataConMensajeModificable,
    imprimirPorIdLista,
    preventInvalidInput,
    convertArrayElementsToString,
    validateDecimal,
    saveDataCambiarEstadoReservas,
    saveDataDuplicadoRooming,
    saveDataOperaciones,
    loadGetDisponibilidadCupos,
    saveDataOperaciones2,
    saveDataCalculosPronosticos,
    imprimirPronosticos,
    saveDataSinCerrar,
    saveDataOperacionesSinClose,
    imprimirGeneralPorId,
    saveDataCheckOut,
    saveDataOperacionesSoloRuta,
    imprimirTodosFiltradoConObjeto,
    filterOptionsParaNumeros,
    saveDataPronosticoEnviarObjeto,
    imprimirPronosticosConObjeto,
    loadGetHastaDataPersonalizado,
    eliminarElementoSinLoad,
    UploadFile,
    loadGetPaginado,
    saveDataOperacionesAnularCheckOut,
    saveDataCalculosPronosticosConGet,
    obtenerElementoHastaData,
    imprimirPronosticosPost,
    eliminarElementoGenerico,
    imprimirPronosticosConObjetoSoloParaNotaDebitoCredito,
    generarDatos,
    loguin,
    cerrarSesion,
    saveDataParaObjetosConFotos,
    loadGetDatosInicio,
    obtenerHastaData,
};
