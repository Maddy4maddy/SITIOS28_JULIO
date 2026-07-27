document.addEventListener("DOMContentLoaded", function () {
    const formulario = document.getElementById("formPostulacion");

    if (!formulario) {
        return;
    }

    formulario.addEventListener("submit", function (event) {
        limpiarErrores();

        let valido = true;

        const identificacion = document.getElementById("identificacion");
        const tipoIdentificacion = document.getElementById("tipo_identificacion");
        const nombreCompleto = document.getElementById("nombre_completo");
        const fechaNacimiento = document.getElementById("fecha_nacimiento");
        const correo = document.getElementById("correo");
        const telefono = document.getElementById("telefono");
        const curriculum = document.getElementById("curriculum");

        if (identificacion.value.trim() === "") {
            mostrarError(identificacion, "La identificación es obligatoria.");
            valido = false;
        }

        if (tipoIdentificacion.value.trim() === "") {
            mostrarError(tipoIdentificacion, "Debe seleccionar el tipo de identificación.");
            valido = false;
        }

        if (nombreCompleto.value.trim() === "") {
            mostrarError(nombreCompleto, "El nombre completo es obligatorio.");
            valido = false;
        }

        if (fechaNacimiento.value.trim() === "") {
            mostrarError(fechaNacimiento, "La fecha de nacimiento es obligatoria.");
            valido = false;
        }

        if (correo.value.trim() === "") {
            mostrarError(correo, "El correo electrónico es obligatorio.");
            valido = false;
        } else if (!validarCorreo(correo.value.trim())) {
            mostrarError(correo, "El formato del correo electrónico no es válido.");
            valido = false;
        }

        if (telefono.value.trim() === "") {
            mostrarError(telefono, "Debe indicar al menos un teléfono.");
            valido = false;
        } else if (!validarTelefono(telefono.value.trim())) {
            mostrarError(telefono, "El teléfono debe tener entre 8 y 20 caracteres numéricos.");
            valido = false;
        }

        if (curriculum.files.length === 0) {
            mostrarError(curriculum, "Debe adjuntar el currículum.");
            valido = false;
        } else {
            const archivo = curriculum.files[0];
            const nombreArchivo = archivo.name.toLowerCase();
            const extensionesPermitidas = [".pdf", ".doc", ".docx"];

            const extensionValida = extensionesPermitidas.some(function (extension) {
                return nombreArchivo.endsWith(extension);
            });

            if (!extensionValida) {
                mostrarError(curriculum, "El currículum debe ser PDF, DOC o DOCX.");
                valido = false;
            }
        }

        if (!valido) {
            event.preventDefault();
        }
    });

    function validarCorreo(correo) {
        const expresion = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return expresion.test(correo);
    }

    function validarTelefono(telefono) {
        const expresion = /^[0-9+\-\s]{8,20}$/;
        return expresion.test(telefono);
    }

    function mostrarError(campo, mensaje) {
        campo.classList.add("error-campo");

        const error = document.createElement("div");
        error.className = "texto-error";
        error.textContent = mensaje;

        campo.parentNode.appendChild(error);
    }

    function limpiarErrores() {
        const camposConError = document.querySelectorAll(".error-campo");
        const mensajesError = document.querySelectorAll(".texto-error");

        camposConError.forEach(function (campo) {
            campo.classList.remove("error-campo");
        });

        mensajesError.forEach(function (mensaje) {
            mensaje.remove();
        });
    }
});