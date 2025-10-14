// VALIDACIONES FOMS

import { Error } from "./notify";

// #region  VALIDATE FORMS
const validateForms = Object.freeze({
    string: (value) => {
        if (value === null || value === "" || value === undefined) {
            return "El campo es obligatorio";
        }
        return true;
    },

    /**
     * Valida que el valor ingresado cumpla con las siguientes condiciones:
     *   - La parte entera no debe tener ceros a la izquierda (excepto cuando es "0")
     *   - La cantidad de dígitos en la parte entera no supere "cifrasEnteras"
     *   - La cantidad de dígitos en la parte decimal no supere "decimales"
     *
     * Se asume que el separador decimal puede ser punto (".") o coma (",").
     *
     * @param {string|number} val - Valor ingresado por el usuario.
     * @param {number} decimales - Cantidad máxima permitida de dígitos decimales.
     * @param {number} cifrasEnteras - Cantidad máxima permitida de dígitos en la parte entera.
     * @returns {true|string} true si es válido; mensaje de error en caso contrario.
     */
    formatoNumerosDecimales: (val, decimales, cifrasEnteras) => {
        // Convertir a cadena y quitar espacios extremos
        const str = val.toString().trim();

        // Definir variables para la parte entera y decimal
        let parteEntera = "";
        let parteDecimal = "";

        // Si el valor incluye separador decimal (punto o coma)
        if (str.includes(".") || str.includes(",")) {
            const separador = str.includes(".") ? "." : ",";
            const partes = str.split(separador);
            parteEntera = partes[0];
            parteDecimal = partes[1] || "";
        } else {
            // Si no hay separador, se toman solo los dígitos y se asume que es un número entero
            parteEntera = str.replace(/\D/g, "");
            parteDecimal = "";
        }

        // Aseguramos que solo queden dígitos en ambas partes (por si hubiera caracteres inesperados)
        parteEntera = parteEntera.replace(/\D/g, "");
        parteDecimal = parteDecimal.replace(/\D/g, "");

        // Validación 1: Si la parte entera tiene más de un dígito, no debe comenzar con "0"
        if (parteEntera.length > 1 && parteEntera.startsWith("0")) {
            Error("No se permiten ceros a la izquierda en la parte entera");
            return false;
        }

        // Validación 2: Verificar que la cantidad de dígitos en la parte entera no exceda lo permitido
        if (parteEntera.length > cifrasEnteras) {
            Error(
                `El número de dígitos enteros no puede exceder ${cifrasEnteras}`
            );
            return false;
        }

        // Validación 3: Verificar que la cantidad de dígitos decimales no exceda lo permitido
        if (parteDecimal.length > decimales) {
            Error(
                `El número de dígitos decimales no puede exceder ${decimales}`
            );
            return false;
        }

        // Si pasa todas las validaciones, retorna true
        return true;
    },
    validarCeroIzquierda: (valor) => {
        // Convertir a cadena y quitar espacios extremos
        const str = valor.toString().trim();

        // Obtener la parte entera del número
        let parteEntera = str.split(/[.,]/)[0]; // Divide por punto o coma y toma la parte entera
        parteEntera = parteEntera.replace(/\D/g, ""); // Asegura que solo contenga dígitos

        // Verificar si tiene más de un dígito y comienza con "0"
        if (parteEntera.length > 1 && parteEntera.startsWith("0")) {
            Error("No se permiten ceros a la izquierda en la parte entera");
            return false;
        }

        return true; // Si pasa la validación
    },

    onlyLetter_Number: (value) => {
        // Permite letras, números y espacios en blanco entre palabras,
        // pero no permite espacios en blanco al inicio ni al final del texto.
        //const patronCadena = /^\S[\w\sñÑ]*\S$|^\S$/;
        const patronCadena =
            /^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+([a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]*)[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+$|^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+$/;
        if (!patronCadena.test(value)) {
            return "Por favor, ingresa solo letras y números.";
        }
        return true;
    },

    onlyNumberYVacio: (value) => {
        // Verifica si el valor no está vacío
        if (value.trim() === "" || value === undefined || value === null) {
            return true;
        }

        // Permite letras, números y espacios en blanco entre palabras,
        // pero no permite espacios en blanco al inicio ni al final del texto.
        const patronCadena = /^[0-9]+([0-9\s]*)[0-9]+$|^[0-9]+$/;

        if (!patronCadena.test(value)) {
            return "Por favor, ingresa solo números.";
        }
        return true;
    },

    onlyLetter_Number_No_White_Spaces: (value) => {
        // Permite solo letras y números, sin espacios en blanco.
        const patronCadena = /^[a-zA-Z0-9ñÑ]+$/;
        if (!patronCadena.test(value)) {
            return "Por favor, ingresa solo letras y números, sin espacios en blanco.";
        }
        return true;
    },

    // solo permite letras, numeros, guion y slash
    onlyLetter_Number_Simbolos: (value) => {
        const patronCadena =
            /^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\-/]+([a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\-/]*)[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\-/]+$|^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\-/]+$/;

        if (!patronCadena.test(value)) {
            return "Por favor, ingresa solo letras, números, guiones (-) y barras (/)";
        }
        return true;
    },

    validarLetrasNumerosYCaracteresEspeciales: (val, opcion) => {
        if (opcion == 1) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas, tildes, números y espacios
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex =
                /^(?!\s)(?!.*\s{2})[A-Za-záéíóúÁÉÍÓÚñÑ0-9\s]+(?<!\s)$/;
            return !regex.test(val)
                ? "Solo se permiten letras, números y espacios entre palabras"
                : true;
        } else if (opcion == 2) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas, tildes, números, espacios, punto, coma, dos puntos, símbolo de número y guion alto
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos

            if (val !== "") {
                const regex =
                    /^(?!\s)(?!.*\s{2})[A-Za-záéíóúÁÉÍÓÚñÑ0-9\s.,:#-]+(?<!\s)$/;

                return !regex.test(val)
                    ? "Solo se permiten letras, números, espacios entre palabras, punto, coma, dos puntos, símbolo de número y guion alto"
                    : true;
            } else return true;
        } else if (opcion == 3) {
            if (val !== "") {
                const regex =
                    /^(?!\s)(?!.*\s{2})[A-Za-záéíóúÁÉÍÓÚñÑ0-9\s.,:#/-]+(?<!\s)$/;

                return !regex.test(val)
                    ? "Solo se permiten letras, números, espacios entre palabras, punto, coma, dos puntos, símbolo de número y guion alto"
                    : true;
            } else return true;
        } else if (opcion == 4) {
            if (val !== "") {
                const regex =
                    /^(?!\s)(?!.*\s{2})[A-Za-záéíóúÁÉÍÓÚñÑ\s]+(?<!\s)$/;

                return !regex.test(val)
                    ? "Solo se permiten letras, espacios entre palabras y tilde"
                    : true;
            } else return true;
        } else if (opcion == 5) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas, números y espacios
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex = /^[A-Za-zñÑ0-9]+(?:\s[A-Za-zñÑ0-9]+)*$/;

            return !regex.test(val)
                ? "Solo se permiten letras, números y espacios entre palabras"
                : true;
        } else if (opcion == 6) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas, números y espacios, tildes
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex =
                /^[A-Za-zÁÉÍÓÚáéíóúñÑ0-9]+(?:\s[A-Za-zÁÉÍÓÚáéíóúñÑ0-9]+)*$/;
            return !regex.test(val)
                ? "Solo se permiten letras, números y espacios entre palabras"
                : true;
        } else if (opcion == 7) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas y espacios
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex = /^[A-Za-zñÑ]+(?:\s[A-Za-zñÑ]+)*$/;
            return !regex.test(val)
                ? "Solo se permiten letras y espacios entre palabras"
                : true;
        } else if (opcion == 8) {
            // Expresión regular que acepta letras en minúsculas y mayúsculas y espacios
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex = /^[A-Za-zÁÉÍÓÚáéíóúñÑ]+(?:\s[A-Za-zÁÉÍÓÚáéíóúñÑ]+)*$/;
            return !regex.test(val)
                ? "Solo se permiten letras y espacios entre palabras"
                : true;
        } else if (opcion == 9) {
            // Expresión regular que acepta numeros y simbolo de mas
            // No permite espacios en blanco al principio ni al final, y no permite dos espacios en blanco consecutivos
            const regex = /^\+?[0-9]+$/;
            return !regex.test(val)
                ? "Solo se permite el símbolo + y números"
                : true;
        } else if (opcion == 10) {
            if (val !== "") {
                const regex = /^[a-zA-Z0-9ñÑ]+$/;
                return !regex.test(val)
                    ? "Por favor, ingresa solo letras y números, sin espacios en blanco."
                    : true;
            } else return true;
        } else if (opcion == 11) {
            if (val !== "") {
                const regex =
                    /^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+([a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]*)[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+$|^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]+$/;
                return !regex.test(val)
                    ? "Por favor, ingresa solo letras y números."
                    : true;
            } else return true;
        }
    },

    compararObjetosJSON2: (obj1, obj2) => {
        // Convertir los objetos a JSON
        const jsonObj1 = JSON.stringify(obj1, Object.keys(obj1).sort());
        const jsonObj2 = JSON.stringify(obj2, Object.keys(obj2).sort());

        if (jsonObj1 === jsonObj2) {
            return compararObjetosProfundamente(obj1, obj2);
        }
    },

    compararObjetosProfundamente2: (obj1, obj2) => {
        if (typeof obj1 !== "object" || typeof obj2 !== "object") {
            return obj1 === obj2;
        }
        if (obj1 === null || obj2 === null) {
            return obj1 === obj2;
        }
        if (Object.keys(obj1).length !== Object.keys(obj2).length) {
            return false;
        }
        for (let key in obj1) {
            if (
                !obj2.hasOwnProperty(key) ||
                !compararObjetosProfundamente(obj1[key], obj2[key])
            ) {
                return false;
            }
        }
        return true;
    },

    compararObjetosJSON: (obj1, obj2) => {
        // Convertir los objetos a JSON
        const jsonObj1 = JSON.stringify(obj1, Object.keys(obj1).sort());
        const jsonObj2 = JSON.stringify(obj2, Object.keys(obj2).sort());
        // Comparar los JSON
        return jsonObj1 === jsonObj2;
    },

    compararObjetosProfundamente: (obj1, obj2) => {
        if (typeof obj1 !== "object" || typeof obj2 !== "object") {
            return obj1 === obj2;
        }
        if (obj1 === null || obj2 === null) {
            return obj1 === obj2;
        }
        if (Object.keys(obj1).length !== Object.keys(obj2).length) {
            return false;
        }
        for (let key in obj1) {
            if (
                !obj2.hasOwnProperty(key) ||
                !compararObjetosProfundamente(obj1[key], obj2[key])
            ) {
                return false;
            }
        }
        return true;
    },

    validarCamposLlenos: (listaCampos, listaCamposAuxiliar, esEditar) => {
        if (!esEditar) {
            for (let i = 0; i < listaCampos.length; i++) {
                let campo = Object.values(listaCampos[i])[0];

                if (Array.isArray(campo)) {
                    if (campo.length === 0) {
                        return false;
                    }
                } else {
                    if (campo === null || campo === "") {
                        return false;
                    }
                }
            }
            return true;
        } else {
            let cantidadSinCambiar = 0;

            for (let i = 0; i < listaCampos.length; i++) {
                let campo = Object.values(listaCampos[i])[0];
                let campoAuxiliar = Object.values(listaCamposAuxiliar[i])[0];

                if (Array.isArray(campo) && Array.isArray(campoAuxiliar)) {
                    if (campo.length === 0 && campoAuxiliar.length === 0) {
                        cantidadSinCambiar++;
                    }
                } else {
                    if (
                        campo === null ||
                        campo === "" ||
                        campo === campoAuxiliar
                    ) {
                        cantidadSinCambiar++;
                    }
                }
            }
            if (cantidadSinCambiar === listaCampos.length) {
                return false;
            }
            return true;
        }
    },

    validateCodigoLlamadaNacional: (val) => {
        const regex = /^([0-9]{2}(\/[0-9]{2})*)?$/;
        if (val === "" || val === undefined || val === null) {
            return true;
        }

        return (
            regex.test(val) ||
            "El campo debe contener exactamente dos números separados por una barra (/)"
        );
    },

    limpiarSiEsCero: (obj, key) => {
        if (obj[key] === 0) {
            obj[key] = "";
        }
    },

    asignarCeroSiEstaVacio: (obj, key) => {
        if (obj[key] === "" || obj[key] === null) {
            obj[key] = 0;
        }
    },

    validarNumerosPuntoYComa: (event, propiedad, cantidadDigitos) => {
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

        // Verificar si en la cadena hay un punto o una coma

        if (!valor.includes(".") && !valor.includes(",")) {
            // No permitir más de 10 números antes del punto o la coma
            if (partes[0].length >= 10 && ![".", ","].includes(tecla)) {
                event.preventDefault();
                return;
            }
        } else {
            if (partes[0].length >= 10 && ![".", ","].includes(tecla)) {
                event.preventDefault();
                return;
            } else {
                return;
            }
        }

        // Permitir el punto o la coma si hay exactamente 10 dígitos antes
        if (partes[0].length === 10 && [".", ","].includes(tecla)) {
            return;
        }
    },

    emailNoRequerido: (value) => {
        /*
    admitirá letras(a-z), números (0-9), con una longitud entre 6 y 18 caracteres.
    No pueden incluir los caracteres especiales signo et (&), el signo igual (=),
    guiones bajos (_), apóstrofos ('), guiones (-), corchetes (< >), el signo más (+),
    comas (,) ni dos o más puntos seguidos. Puede comenzar o terminar con caracteres especiales
    que sean permitidos para el correo, salvo puntos.
    */
        const emailPattern =
            /^(?!.*\.\.)(?!^\.)(?!.*\.$)[a-z0-9.%!#$^*()?~¿{}";:/]+@[a-z0-9.]+\.[a-z%!#$^*()?~¿{}";:/]{2,}$/;
        if (value !== null && value !== "") {
            if (!emailPattern.test(value)) {
                return "Por favor, ingresa una dirección de correo electrónico válida.";
            }
        }
        return true;
    },

    permitirSoloNumeros: (event) => {
        const tecla = event.key;
        // Expresión regular que solo acepta números
        const regex = /^[0-9]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    validarSoloNumerosComaPunto: (event) => {
        // Permitir teclas de control (Backspace, Arrow, etc.) que no sean caracteres simples
        if (event.key.length > 1) return true;

        // Obtener el valor actual del input y concatenar la tecla pulsada.
        // (Para una validación más precisa se debería considerar la posición del cursor, pero aquí usamos la concatenación simple)
        const currentValue = event.target.value;
        const nuevaEntrada = currentValue + event.key;

        // Esta expresión regular permite:
        // • Al menos un dígito al inicio: \d+
        // • Opcionalmente un separador decimal (punto o coma) seguido de 0 o más dígitos: (?:[.,]\d*)?
        // Así se evita que se puedan ingresar múltiples puntos o comas.
        const regex = /^\d+(?:[.,]\d*)?$/;

        if (!regex.test(nuevaEntrada)) {
            event.preventDefault();
            return "Solo se permiten números con un solo punto o coma.";
        }

        return true;
    },
    validarSoloCaracteresPermitidos: (event) => {
        // Permitir teclas de control (Backspace, Arrow, etc.)
        if (event.key.length > 1) return true;

        // Obtener el valor actual del input y concatenar la tecla pulsada
        const currentValue = event.target.value;
        const nuevaEntrada = currentValue + event.key;

        // Expresión regular para permitir solo:
        // - Letras mayúsculas y minúsculas (a-z, A-Z)
        // - Números (0-9)
        // - Los caracteres "/" y "-"
        const regex = /^[a-zA-Z0-9\/-]+$/;

        if (!regex.test(nuevaEntrada)) {
            event.preventDefault();
            return "Solo se permiten letras, números, '/', y '-'.";
        }

        return true;
    },

    permitirSoloNumerosYLetras: (event) => {
        const tecla = event.key;
        // Expresión regular que solo acepta números
        const regex = /^[0-9A-Za-zñÑ]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    permitirSoloNumerosLetrasYSlash: (event) => {
        const tecla = event.key;
        // Expresión regular modificada para incluir la barra /
        const regex = /^[0-9A-Za-zñÑ\/]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    permitirSoloLetrasYEspacio: (event) => {
        const tecla = event.key;
        // Expresión regular modificada para incluir la barra /
        const regex = /^[A-Za-záéíóúÁÉÍÓÚñÑ\s]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },
    permitirSoloLetrasNumerosSimboloPorcientoYEspacio: (event) => {
        const tecla = event.key;
        // Expresión regular modificada para incluir la barra /
        const regex = /^[0-9A-Za-záéíóúÁÉÍÓÚñÑ%\s]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    permitirSoloLetrasYEspacioSinDobleEspacio: (event) => {
        const tecla = event.key;

        // Validar: si se intenta insertar un espacio y el último carácter ya es un espacio, se previene
        if (tecla === " " && event.target.value.slice(-1) === " ") {
            event.preventDefault();
            return;
        }

        // Expresión regular para permitir solo letras (incluyendo acentuadas y ñ) y espacios
        const regex = /^[A-Za-záéíóúÁÉÍÓÚñÑ\s]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    permitirSoloLetrasNumerosYEspacio: (event) => {
        const tecla = event.key;
        // Expresión regular modificada para incluir la barra /
        const regex = /^[0-9A-Za-záéíóúÁÉÍÓÚñÑ\s]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },

    permitirSoloNumerosLetrasSlashYGuion: (event) => {
        const tecla = event.key;
        const input = event.target;
        const valorActual = input.value;
        const posicionCursor = input.selectionStart;

        const regex = /^[a-zA-Z0-9ñÑ/\- ]$/;

        if (!regex.test(tecla)) {
            event.preventDefault();
            return;
        }

        if (tecla === " ") {
            // Caso 1: Espacio al principio
            if (posicionCursor === 0) {
                event.preventDefault();
                return;
            }

            // Caso 2: Doble espacio
            if (valorActual.charAt(posicionCursor - 1) === " ") {
                event.preventDefault();
                return;
            }

            // Caso 3: Si el espacio está después del último caracter (permitido temporalmente)
            if (posicionCursor === valorActual.length) {
                // Permitimos el espacio temporalmente
                // La validación final se hará en @blur
                return;
            }

            // Caso 4: Si el siguiente caracter es un espacio
            if (valorActual.charAt(posicionCursor) === " ") {
                event.preventDefault();
            }
        }
    },

    limitDigits(evt, max) {
        const key = evt.key;
        // permitir controles: retroceso, tab, flechas, etc.
        const controlKeys = ["Backspace", "ArrowLeft", "ArrowRight", "Tab"];
        if (controlKeys.includes(key)) return;

        // solo dígitos
        if (!/^\d$/.test(key)) {
            evt.preventDefault();
            return;
        }

        // valor actual + la pulsación
        const next = evt.target.value + key;
        if (next.length > max) {
            evt.preventDefault();
        }
    },

    /**
     * Formatea un campo de texto: pasa a mayúsculas y elimina todo
     * lo que no sea A–Z ni 0–9.
     *
     * @param {object} target  - Objeto reactivo donde está el campo
     * @param {string} key     - Nombre de la propiedad dentro de `target`
     * @param {string} event     - Valor crudo del input
     */
    formatAlphanumericField: (target, key, event) => {

        target[key] = event.toUpperCase().replace(/[^A-Z0-9]/g, "");

    },

    permitirSoloLetras: (event) => {
        const tecla = event.key;
        // Expresión regular modificada para incluir la barra /
        const regex = /^[A-Za-záéíóúÁÉÍÓÚñÑ]$/;
        if (!regex.test(tecla)) {
            event.preventDefault();
        }
    },
});

export const {
    string,
    onlyLetter_Number,
    onlyLetter_Number_No_White_Spaces,
    validarLetrasNumerosYCaracteresEspeciales,
    validarCamposLlenos,
    validateCodigoLlamadaNacional,
    compararObjetosJSON,
    asignarCeroSiEstaVacio,
    limpiarSiEsCero,
    validarNumerosPuntoYComa,
    emailNoRequerido,
    permitirSoloNumeros,
    compararObjetosProfundamente,
    compararObjetosJSON2,
    compararObjetosProfundamente2,
    onlyNumberYVacio,
    onlyLetter_Number_Simbolos,
    validarSoloNumerosComaPunto,
    permitirSoloNumerosYLetras,
    permitirSoloNumerosLetrasYSlash,
    permitirSoloNumerosLetrasSlashYGuion,
    validarSoloCaracteresPermitidos,
    permitirSoloLetrasYEspacio,
    formatoNumerosDecimales,
    validarCeroIzquierda,
    permitirSoloLetrasNumerosYEspacio,
    permitirSoloLetrasYEspacioSinDobleEspacio,
    limitDigits,
    formatAlphanumericField,
    permitirSoloLetras,
    permitirSoloLetrasNumerosSimboloPorcientoYEspacio
} = validateForms;
