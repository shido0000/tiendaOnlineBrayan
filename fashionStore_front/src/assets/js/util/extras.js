import dayjs from "./Day.js";
import { Error } from "./notify.js";

// VALIDACIONES EXTRAS
// #region  VALIDATE EXTRAS
export const PonerPuntosSupensivosACampo = (text = "?", maxLength = 10) => {
    return text.length > maxLength ? text.slice(0, maxLength) + " ..." : text;
};

export function limpiarNumero(input) {
    // Convertir el input a cadena y eliminar cualquier carácter que no sea un dígito (0-9)
    const soloNumeros = input.toString().replace(/[^0-9]/g, "");

    // Si no quedan dígitos, puedes retornar 0 o una cadena vacía, según lo necesites.
    if (soloNumeros === "") {
        return 0;
    }

    // Convertir la cadena resultante a un entero (quitará automáticamente los ceros iniciales)
    const numeroEntero = parseInt(soloNumeros, 10);

    return numeroEntero;
}

// Metodo para permitir solamente la entrada de numeros, sirve para cualquier campo numerico
// y para la cantidad de numeros que quiere insertar
export const validarNumerosSolamente = (event, propiedad) => {
    const tecla = event.key;
    let valor;
    if (propiedad === null) {
        valor = "";
    } else {
        valor = propiedad.toString();
    }

    // Solo acepta numeros
    const regex = /^[0-9]$/;
    if (!regex.test(tecla)) {
        event.preventDefault();
    }
};

export function isEmpty(value) {
    if (Array.isArray(value)) {
        // Verifica si el arreglo está vacío
        return value.length === 0;
    } else if (typeof value === "string") {
        // Para cadenas, verifica si está vacía o contiene solo espacios en blanco
        return value.trim().length === 0;
    } else if (typeof value === "object" && value !== null) {
        // Para objetos, verifica si está vacío utilizando Object.keys()
        return Object.keys(value).length === 0;
    } else {
        // Para otros tipos, verifica si es nulo o indefinido
        return value === null || value === undefined;
    }
}

/**
 * Verifica si una fecha solapa con alguna otra fecha en el array
 * @param {Object} date - Fecha a comprobar (contiene 'from' y 'to')
 * @param {Array} ListadoRecibido - Array de fechas (cada elemento contiene 'from' y 'to')
 * @returns {boolean} - Verdadero si hay solapamiento, falso si no
 **/
export function checkIfExistOverlappingDate(fecha, ListadoRecibido) {
    const start = dayjs(fecha.from);
    const end = dayjs(fecha.to);

    for (const item of ListadoRecibido) {
        const itemStart = dayjs(item.from);
        const itemEnd = dayjs(item.to);
        const isStartBetween = start.isBetween(itemStart, itemEnd, null, "[]");
        const isEndBetween = end.isBetween(itemStart, itemEnd, null, "[]");
        const isEventBetweenOtherEvent =
            itemStart.isSameOrBefore(start) && itemEnd.isSameOrAfter(end);

        if (isStartBetween || isEndBetween || isEventBetweenOtherEvent) {
            return true;
        }
    }
    return false;
}

/**
 * Funcion para cerrar los modales de los Q-Date
 * @param {Object} inputRef - Ref del componente Q-Date
 * @returns {void}
 * /**
 * Función para cerrar los modales de los Q-Date
 * @param {Object} inputRef - Ref del componente Q-Date
 * @returns {void}
 *
 * @example
 * // Ejemplo de uso en un componente Vue
 *
 *     const cerrar3 = ref(null); // Ref para q-popup-proxy
 *
 *     <q-popup-proxy ref="cerrar3">
 *
 *        Llama a hidePopup con la referencia
 *       <q-date @update:modelValue="hidePopup($refs.cerrar3)"/>
 *
 **/
export const hidePopup = (inputRef) => {
    if (inputRef) {
        inputRef.hide();
    }
};

/**
 * Función para formatear un número sin redondear, permitiendo especificar:
 *   - La cantidad deseada de dígitos en la parte decimal (decimales).
 *   - La cantidad permitida de dígitos en la parte entera (cifrasEnteras).
 *
 * Reglas:
 * 1. Si el número se ingresa con separador decimal (punto o coma):
 *    - Se eliminan todos los caracteres que no sean dígitos.
 *    - Se eliminan los ceros a la izquierda en la parte entera (por lo que "03.00" se convierte en "3.00").
 *    - Si la parte entera tiene más dígitos que lo permitido (cifrasEnteras) se truncan,
 *      conservándose solo los primeros dígitos.
 *    - Si la parte decimal excede la cantidad de dígitos indicados en "decimales", se trunca;
 *      NO se completan ceros a la derecha.
 *
 * 2. Si el número se ingresa sin separador:
 *    - Se interpretan los dígitos directos; si se especifica cifrasEnteras se asume que
 *      los primeros cifrasEnteras corresponden a la parte entera y el resto a la parte decimal.
 *    - Se elimina cualquier caracter no numérico.
 *    - También se eliminan los ceros a la izquierda de la parte entera.
 *    - La parte decimal se trunca si excede el número indicado, sin completarse con ceros.
 *
 * @param {number|string} valor - Valor a formatear.
 * @param {number} decimales - Cantidad deseada de dígitos en la parte decimal.
 * @param {number} cifrasEnteras - Cantidad permitida de dígitos en la parte entera.
 * @returns {string} Número formateado según las restricciones (sin agregar ceros a la derecha ni ceros a la izquierda en la parte entera).
 */
export function formatearDecimalesSinRedondeo(valor, decimales, cifrasEnteras) {
    // Convertir el valor a cadena y quitar espacios
    let str = valor?.toString().trim();

    // Declaramos variables para la parte entera (ip) y la decimal (dp)
    let ip = "";
    let dp = "";

    // Verificamos si el valor contiene separador decimal (punto o coma)
    if (str.includes(".") || str.includes(",")) {
        // Determinar el separador usado (se prioriza el punto)
        const separador = str.includes(".") ? "." : ",";
        const partes = str.split(separador);
        ip = partes[0];
        dp = partes[1] || "";

        // Eliminar cualquier carácter que no sea dígito de ambas partes
        ip = ip.replace(/\D/g, "");
        dp = dp.replace(/\D/g, "");

        // Eliminar ceros a la izquierda en la parte entera
        ip = ip.replace(/^0+/, "");
        if (ip === "") ip = "0";

        // Si se especifica la cantidad permitida de dígitos en la parte entera,
        // y ésta excede cifrasEnteras, se truncarían (se conservan los primeros dígitos)
        if (typeof cifrasEnteras === "number" && ip.length > cifrasEnteras) {
            ip = ip.substring(0, cifrasEnteras);
        }

        // Para la parte decimal, si hay más dígitos de los indicados se truncan.
        if (typeof decimales === "number" && dp.length > decimales) {
            dp = dp.substring(0, decimales);
        }

        // **Aquí se completa con ceros hasta alcanzar la cantidad deseada de decimales**
        if (typeof decimales === "number" && decimales > 0) {
            dp = dp.padEnd(decimales, "0");
            return `${ip}.${dp}`;
        } else {
            return dp ? `${ip}.${dp}` : ip;
        }
    } else {
        // Caso: valor ingresado SIN separador decimal.
        // Extraer únicamente dígitos (preservando el orden y los ceros que el usuario haya colocado)
        let raw = str.replace(/\D/g, "");

        // Si se especifica cifrasEnteras, se toman los primeros cifrasEnteras dígitos como parte entera
        // y el resto (si existe) como parte decimal.
        if (typeof cifrasEnteras === "number") {
            if (raw.length > cifrasEnteras) {
                ip = raw.substring(0, cifrasEnteras);
                dp = raw.substring(cifrasEnteras);
            } else {
                ip = raw;
                dp = "";
            }
        } else {
            ip = raw;
            dp = "";
        }

        // Eliminar ceros a la izquierda en la parte entera
        ip = ip.replace(/^0+/, "");
        if (ip === "") ip = "0";

        // En la parte decimal, si hay más dígitos de los que se requieren, se truncan.
        if (typeof decimales === "number" && dp.length > decimales) {
            dp = dp.substring(0, decimales);
        }

        // **Aquí se completa con ceros hasta alcanzar la cantidad deseada de decimales**
        if (typeof decimales === "number" && decimales > 0) {
            dp = dp.padEnd(decimales, "0"); // Completa ceros a la derecha
            return `${ip}.${dp}`;
        } else {
            return dp
                ? `${ip}.${dp.padEnd(decimales, "0")}`
                : `${ip}.${"0".repeat(decimales)}`;
        }
    }
}

//Metodo para convertir la fecha de este formato 2024-12-07T00:00:00 a este 2024-12-07
export function formatoFecha(fechaCompleta) {
    // Crear una instancia de Date a partir de la cadena de fecha y hora
    const fecha = new Date(fechaCompleta);
    // Obtener el año, mes y día
    const año = fecha.getFullYear();
    const mes = String(fecha.getMonth() + 1).padStart(2, "0");
    // Los meses en JavaScript son 0-indexados
    const día = String(fecha.getDate()).padStart(2, "0");
    // Formatear la fecha en YYYY-MM-DD

    return `${año}/${mes}/${día}`;
}

//Metodo para convertir la fecha de este formato 2024-12-07 a este 2024-12-07T00:00:00
export function formatearFechaCompleta(fecha) {
    // Crear una instancia de Date a partir de la cadena de fecha
    const partes = fecha.split("/");
    const año = partes[0];
    const mes = partes[1] - 1; // Los meses en JavaScript son 0-indexados
    const día = partes[2];
    const fechaCompleta = new Date(año, mes, día, 0, 0, 0);

    // Formatear la fecha completa en el formato deseado
    const añoFormateado = fechaCompleta.getFullYear();
    const mesFormateado = String(fechaCompleta.getMonth() + 1).padStart(2, "0"); // Añadir 1 porque los meses en JavaScript son 0-indexados
    const díaFormateado = String(fechaCompleta.getDate()).padStart(2, "0");

    return `${añoFormateado}-${mesFormateado}-${díaFormateado}T00:00:00`;
}

// metodo para verificar el formato de la fecha
export function esFechaCompleta(fecha) {
    // Expresiones regulares para validar los formatos
    const regexCompleto = /^\d{4}\/\d{2}\/\d{2}T\d{2}:\d{2}:\d{2}$/;
    const regexSimple = /^\d{4}\/\d{2}\/\d{2}$/;

    if (regexCompleto.test(fecha)) {
        return true;
    } else if (regexSimple.test(fecha)) {
        return false;
    } else {
        // esFechaCompleta2(fecha)
        return null;
    }
}

export function esFechaCompleta2(fecha) {
    // Expresiones regulares para validar los formatos
    const regexCompleto = /^\d{4}\-\d{2}\-\d{2}T\d{2}:\d{2}:\d{2}$/;
    const regexSimple = /^\d{4}\-\d{2}\-\d{2}$/;
    if (regexCompleto.test(fecha)) {
        return true;
    } else if (regexSimple.test(fecha)) {
        return false;
    } else {
        return null;
    }
}

export function obtenerFechaISO(cadenaFechaHora) {
    // Convertir la cadena de fecha y hora a un objeto Date
    const fecha = new Date(cadenaFechaHora);

    // Obtener el año, mes y día
    const año = fecha.getFullYear();
    const mes = String(fecha.getMonth() + 1).padStart(2, "0");
    const día = String(fecha.getDate()).padStart(2, "0");

    // Formatear la fecha en el formato YYYY-MM-DD
    return `${año}/${mes}/${día}`;
}

export function obtenerFechaISOver2(cadenaFechaHora) {
    // Convertir la cadena de fecha y hora a un objeto Date
    const fecha = new Date(cadenaFechaHora);

    // Obtener el año, mes y día
    const año = fecha.getFullYear();
    const mes = String(fecha.getMonth() + 1).padStart(2, "0");
    const día = String(fecha.getDate()).padStart(2, "0");

    // Formatear la fecha en el formato YYYY-MM-DD
    return `${día}/${mes}/${año}`;
}

export function formatDate(inputDate) {
    const date = new Date(inputDate);
    const year = date.getFullYear();
    // Los meses de JavaScript se retornan de 0 a 11, por eso se suma 1.
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
}

////////////////////////////// METODO PARA CALCULAR LA CANTIDAD DE ELEMENTOS A IGNORAR EN EL PAGINADO
export function calcularValorAIgnorarPaginado(paginaActual, cantidadMostrar) {
    return (paginaActual - 1) * cantidadMostrar;
}

export function validarNumerosPuntoYComa2(
    event,
    propiedad,
    cantidadDigitos,
    esPorciento
) {
    const tecla = event.key;
    let valor = propiedad ? propiedad.toString() : "";

    // Permitir solo números, puntos y comas
    const regex = /^[0-9.,]$/;
    if (!regex.test(tecla)) {
        event.preventDefault();
        return;
    }

    // No permitir más de un punto o una coma
    if (
        (tecla === "." && valor.includes(".")) ||
        (tecla === "," && valor.includes(","))
    ) {
        event.preventDefault();
        return;
    }

    // No permitir punto o coma si ya hay uno de los dos
    if (
        (tecla === "." && valor.includes(",")) ||
        (tecla === "," && valor.includes("."))
    ) {
        event.preventDefault();
        return;
    }

    // No permitir punto o coma al principio o al final
    if (
        (tecla === "." || tecla === ",") &&
        (valor === "" || valor.endsWith(".") || valor.endsWith(","))
    ) {
        event.preventDefault();
        return;
    }

    // Dividir el valor en partes antes y después del punto o la coma
    const partes = valor.split(/[.,]/);

    // No permitir más de 10 números antes del punto o la coma

    if (esPorciento === false) {
        // Verificar si en la cadena hay un punto o una coma
        if (!valor.includes(".") && !valor.includes(",")) {
            // No permitir más de 10 números antes del punto o la coma
            if (partes[0].length >= 10 && ![".", ","].includes(tecla)) {
                event.preventDefault();
                return;
            }
        }
    } else {
        if (!valor.includes(".") && !valor.includes(",")) {
            // No permitir más de 2 números antes del punto o la coma
            if (partes[0].length >= 2 && ![".", ","].includes(tecla)) {
                event.preventDefault();
                return;
            }
        }
    }

    // No permitir más de 'cantidadDigitos' números después del punto o la coma
    if (
        partes.length > 1 &&
        partes[1].length >= cantidadDigitos &&
        tecla !== "Backspace" &&
        tecla !== "Delete"
    ) {
        event.preventDefault();
        return;
    }

    if (esPorciento === false) {
        // Permitir el punto o la coma si hay exactamente 10 dígitos antes
        if (partes[0].length === 10 && [".", ","].includes(tecla)) {
            return;
        }
    } else {
        if (partes[0].length === 2 && [".", ","].includes(tecla)) {
            return;
        }
    }
}

export function limpiarSiEsCero(obj, key) {
    if (
        obj[key] === 0 ||
        obj[key] === 0.0 ||
        obj[key] === "0" ||
        obj[key] === "0.0000" ||
        obj[key] === 0.0 ||
        obj[key] === "0.00"
    ) {
        obj[key] = "";
    }
}

export function limpiarSiEsCeroVersion2(val) {
    const text = String(val);
    const zeros = ["0", "0.0", "0.00", "0.0000"];
    return zeros.includes(text) ? "" : val;
}

export function limpiarSiEsCeroNuevo(obj, key) {
    // Si al convertirlo a número queda 0 y no está vacío originalmente…
    // detecta "0", 0, "0.00", etc.
    if (/^0+(\.0+)?$/.test(String(obj[key]))) {
        obj[key] = null;
    }
}

export function asignarCeroSiEstaVacio(obj, key) {
    if (obj[key] === "" || obj[key] === null) {
        obj[key] = 0.0;
    }
}

export function asignarCeroDobleSiEstaVacio(obj, key) {
    if (obj[key] === "" || obj[key] === null) {
        // Asigna la cadena "0.00" en lugar de 0.0
        obj[key] = (0).toFixed(2);
    }
}

export function asignar1SiEstaVacio(obj, key) {
    if (obj[key] === "" || obj[key] === null) {
        obj[key] = 1;
    }
}

export function formatearPrecioAlPerderFoco(obj, key) {
    if (obj[key] === null || obj[key] === undefined) return;

    // Convertir a string y eliminar espacios
    let valor = obj[key].toString().trim();

    // Eliminar punto o coma al final
    /* if (valor.endsWith(".") || valor.endsWith(",")) {
        valor = valor.slice(0, -1);
    }*/

    // Actualizar el valor (evitar asignar '0' aquí)
    obj[key] = valor === "" ? null : valor;

    // Llamar a la función original para asignar 0 si está vacío
    asignarCeroSiEstaVacio(obj, key);
}
