document.addEventListener("DOMContentLoaded", () => {
    const contenedor = document.getElementById(
        "detalle-oferente"
    );

    if (!contenedor) {
        return;
    }

    if (
        typeof CODIGO_OFERENTE === "undefined" ||
        !CODIGO_OFERENTE
    ) {
        mostrarError(
            contenedor,
            "No se especificó un oferente válido."
        );

        return;
    }

    if (
        typeof ID_PUESTO === "undefined" ||
        !Number.isInteger(Number(ID_PUESTO)) ||
        Number(ID_PUESTO) <= 0
    ) {
        mostrarError(
            contenedor,
            "No se recibió un puesto válido."
        );

        return;
    }

    obtenerDetalleOferente(
        CODIGO_OFERENTE
    );
});

async function obtenerDetalleOferente(
    codigoOferente
) {
    const contenedor = document.getElementById(
        "detalle-oferente"
    );

    contenedor.innerHTML = `
        <p class="loading">
            Cargando información del oferente...
        </p>
    `;

    try {
        const url =
            "/WEBService/actions/obtenerDetalleOferente.php"
            + "?codigoOferente="
            + encodeURIComponent(
                codigoOferente
            );

        const response = await fetch(
            url,
            {
                method: "GET",
                headers: {
                    Accept: "application/json"
                },
                cache: "no-store"
            }
        );

        const texto = await response.text();

        let data;

        try {
            data = JSON.parse(texto);
        } catch {
            throw new Error(
                "El servidor no devolvió una respuesta JSON válida."
            );
        }

        if (response.status === 401) {
            window.location.href =
                "/WEBService/index.php?page=login";

            return;
        }

        if (!response.ok || !data.exito) {
            throw new Error(
                data.mensaje ||
                "No fue posible obtener la información del oferente."
            );
        }

        const oferente =
            data.detalle ??
            data.oferente ??
            data;

        if (
            !oferente ||
            typeof oferente !== "object" ||
            Array.isArray(oferente)
        ) {
            throw new Error(
                "No se encontró información del oferente."
            );
        }

        mostrarDetalle(
            oferente
        );
    } catch (error) {
        mostrarError(
            contenedor,
            error.message
        );
    }
}

function mostrarDetalle(oferente) {
    const contenedor = document.getElementById(
        "detalle-oferente"
    );

    const codigoOferente = obtenerValor(
        oferente,
        [
            "CodigoOferente",
            "codigoOferente",
            "codigo_oferente"
        ]
    );

    const identificacion = obtenerValor(
        oferente,
        [
            "Identificacion",
            "identificacion"
        ]
    );

    const tipoIdentificacion = obtenerValor(
        oferente,
        [
            "TipoIdentificacion",
            "tipoIdentificacion",
            "tipo_identificacion"
        ]
    );

    const nombreCompleto =
        obtenerNombreCompleto(
            oferente
        );

    const correo = obtenerValor(
        oferente,
        [
            "Correo",
            "correo",
            "Email",
            "email"
        ]
    );

    const telefono = obtenerValor(
        oferente,
        [
            "Telefono",
            "telefono"
        ]
    );

    const fechaNacimientoOriginal =
        obtenerValor(
            oferente,
            [
                "FechaNacimiento",
                "fechaNacimiento",
                "fecha_nacimiento"
            ]
        );

    const fechaNacimiento =
        formatearFechaVisual(
            fechaNacimientoOriginal
        );

    contenedor.innerHTML = `
        <div class="detalle-grid">
            ${crearCampo(
                "Código del oferente",
                codigoOferente
            )}

            ${crearCampo(
                "Identificación",
                identificacion
            )}

            ${crearCampo(
                "Tipo de identificación",
                tipoIdentificacion
            )}

            ${crearCampo(
                "Nombre completo",
                nombreCompleto
            )}

            ${crearCampo(
                "Correo electrónico",
                correo
            )}

            ${crearCampo(
                "Teléfono",
                telefono
            )}

            ${crearCampo(
                "Fecha de nacimiento",
                fechaNacimiento
            )}
        </div>

        <div class="acciones-detalle">
            <button
                type="button"
                id="btnCrearEmpleado"
                class="btn-crear-empleado"
            >
                Crear empleado
            </button>

            <button
                type="button"
                id="btnCancelar"
                class="btn-cancelar"
            >
                Cancelar
            </button>
        </div>

        <div id="mensaje-empleado"></div>
    `;

    const botonCancelar =
        document.getElementById(
            "btnCancelar"
        );

    if (botonCancelar) {
        botonCancelar.addEventListener(
            "click",
            regresarAOferentes
        );
    }

    const botonCrearEmpleado =
        document.getElementById(
            "btnCrearEmpleado"
        );

    if (botonCrearEmpleado) {
        botonCrearEmpleado.addEventListener(
            "click",
            () => {
                mostrarFormularioEmpleado(
                    oferente
                );
            }
        );
    }
}

function mostrarFormularioEmpleado(oferente) {
    const contenedor =
        document.getElementById(
            "mensaje-empleado"
        );

    if (!contenedor) {
        return;
    }

    const identificacion = obtenerValor(
        oferente,
        [
            "Identificacion",
            "identificacion"
        ]
    );

    const nombreCompleto =
        obtenerNombreCompleto(
            oferente
        );

    contenedor.innerHTML = `
        <div class="formulario-empleado">

            <div class="formulario-empleado-header">
                <div>
                    <h3>Crear empleado</h3>

                    <p>
                        Complete la información de contratación.
                    </p>
                </div>
            </div>

            <div class="formulario-resumen">
                <p>
                    Oferente:
                    <strong>
                        ${escaparHtml(
                            nombreCompleto ||
                            "N/A"
                        )}
                    </strong>
                </p>

                <p>
                    Identificación:
                    <strong>
                        ${escaparHtml(
                            identificacion ||
                            "N/A"
                        )}
                    </strong>
                </p>
            </div>

            <form id="formCrearEmpleado">

                <div class="form-grid">

                    <div class="form-group">
                        <label for="numeroEmpleado">
                            Número de empleado
                        </label>

                        <input
                            type="text"
                            id="numeroEmpleado"
                            name="numeroEmpleado"
                            maxlength="50"
                            autocomplete="off"
                            required
                        >
                    </div>

                    <div class="form-group">
                        <label for="fechaContratacion">
                            Fecha de contratación
                        </label>

                        <input
                            type="date"
                            id="fechaContratacion"
                            name="fechaContratacion"
                            value="${obtenerFechaActual()}"
                            required
                        >
                    </div>

                    <div class="form-group">
                        <label for="estadoEmpleado">
                            Estado
                        </label>

                        <select
                            id="estadoEmpleado"
                            name="estadoEmpleado"
                            required
                        >
                            <option value="Activo">
                                Activo
                            </option>

                            <option value="Inactivo">
                                Inactivo
                            </option>
                        </select>
                    </div>

                </div>

                <div
                    id="resultado-crear-empleado"
                    class="resultado-formulario"
                ></div>

                <div class="acciones-formulario">

                    <button
                        type="submit"
                        id="btnConfirmarEmpleado"
                        class="btn-crear-empleado"
                    >
                        Confirmar creación
                    </button>

                    <button
                        type="button"
                        id="btnCerrarFormulario"
                        class="btn-cancelar"
                    >
                        Cerrar
                    </button>

                </div>

            </form>

        </div>
    `;

    const formulario =
        document.getElementById(
            "formCrearEmpleado"
        );

    const botonCerrar =
        document.getElementById(
            "btnCerrarFormulario"
        );

    if (botonCerrar) {
        botonCerrar.addEventListener(
            "click",
            () => {
                contenedor.innerHTML = "";
            }
        );
    }

    if (formulario) {
        formulario.addEventListener(
            "submit",
            async evento => {
                evento.preventDefault();

                await crearEmpleado(
                    oferente
                );
            }
        );
    }

    const numeroEmpleado =
        document.getElementById(
            "numeroEmpleado"
        );

    if (numeroEmpleado) {
        numeroEmpleado.focus();
    }
}

async function crearEmpleado(oferente) {
    const numeroEmpleado =
        document.getElementById(
            "numeroEmpleado"
        );

    const fechaContratacion =
        document.getElementById(
            "fechaContratacion"
        );

    const estadoEmpleado =
        document.getElementById(
            "estadoEmpleado"
        );

    const botonConfirmar =
        document.getElementById(
            "btnConfirmarEmpleado"
        );

    const resultado =
        document.getElementById(
            "resultado-crear-empleado"
        );

    if (
        !numeroEmpleado ||
        !fechaContratacion ||
        !estadoEmpleado ||
        !resultado
    ) {
        return;
    }

    const numero = numeroEmpleado.value.trim();
    const fecha = fechaContratacion.value;
    const estado = estadoEmpleado.value.trim();

    resultado.textContent = "";
    resultado.className =
        "resultado-formulario";

    if (numero === "") {
        mostrarResultado(
            resultado,
            "Debe ingresar el número de empleado.",
            false
        );

        numeroEmpleado.focus();

        return;
    }

    if (fecha === "") {
        mostrarResultado(
            resultado,
            "Debe seleccionar la fecha de contratación.",
            false
        );

        fechaContratacion.focus();

        return;
    }

    if (estado === "") {
        mostrarResultado(
            resultado,
            "Debe seleccionar el estado del empleado.",
            false
        );

        estadoEmpleado.focus();

        return;
    }

    const solicitud = {
        NumeroEmpleado: numero,
        Identificacion: obtenerValor(
            oferente,
            [
                "Identificacion",
                "identificacion"
            ]
        ),
        TipoIdentificacion: obtenerValor(
            oferente,
            [
                "TipoIdentificacion",
                "tipoIdentificacion",
                "tipo_identificacion"
            ]
        ),
        NombreCompleto:
            obtenerNombreCompleto(
                oferente
            ),
        FechaNacimiento:
            formatearFechaServicio(
                obtenerValor(
                    oferente,
                    [
                        "FechaNacimiento",
                        "fechaNacimiento",
                        "fecha_nacimiento"
                    ]
                )
            ),
        Correo: obtenerValor(
            oferente,
            [
                "Correo",
                "correo",
                "Email",
                "email"
            ]
        ),
        Telefono: obtenerValor(
            oferente,
            [
                "Telefono",
                "telefono"
            ]
        ),
        IdPuesto: Number(
            ID_PUESTO
        ),
        FechaContratacion: fecha,
        Estado: estado
    };

    botonConfirmar.disabled = true;
    botonConfirmar.textContent =
        "Creando empleado...";

    try {
        const response = await fetch(
            "/WEBService/actions/crearEmpleado.php",
            {
                method: "POST",
                headers: {
                    "Content-Type":
                        "application/json",
                    Accept:
                        "application/json"
                },
                body: JSON.stringify(
                    solicitud
                )
            }
        );

        const texto = await response.text();

        let data;

        try {
            data = JSON.parse(texto);
        } catch {
            throw new Error(
                "El servidor devolvió una respuesta inválida."
            );
        }

        if (response.status === 401) {
            window.location.href =
                "/WEBService/index.php?page=login";

            return;
        }

        if (!response.ok || !data.exito) {
            throw new Error(
                data.mensaje ||
                "No fue posible crear el empleado."
            );
        }

        mostrarResultado(
            resultado,
            data.mensaje ||
            "El empleado fue creado correctamente.",
            true
        );

        numeroEmpleado.disabled = true;
        fechaContratacion.disabled = true;
        estadoEmpleado.disabled = true;

        botonConfirmar.remove();
    } catch (error) {
        mostrarResultado(
            resultado,
            error.message ||
            "No fue posible crear el empleado.",
            false
        );

        botonConfirmar.disabled = false;
        botonConfirmar.textContent =
            "Confirmar creación";
    }
}

function crearCampo(etiqueta, valor) {
    return `
        <div class="detalle-item">
            <label>
                ${escaparHtml(etiqueta)}:
            </label>

            <span>
                ${escaparHtml(
                    valor || "N/A"
                )}
            </span>
        </div>
    `;
}

function mostrarResultado(
    elemento,
    mensaje,
    exito
) {
    elemento.textContent = mensaje;

    elemento.className =
        exito
            ? "resultado-formulario resultado-exito"
            : "resultado-formulario resultado-error";
}

function mostrarError(
    contenedor,
    mensaje
) {
    contenedor.innerHTML = `
        <div class="error">
            <p>
                ${escaparHtml(
                    mensaje ||
                    "Ocurrió un error."
                )}
            </p>
        </div>
    `;
}

function regresarAOferentes() {
    if (
        typeof ID_PUESTO !== "undefined" &&
        Number(ID_PUESTO) > 0 &&
        typeof CODIGO_PUESTO !== "undefined" &&
        CODIGO_PUESTO
    ) {
        window.location.href =
            "/WEBService/index.php?page=oferentes"
            + "&id_puesto="
            + encodeURIComponent(
                Number(ID_PUESTO)
            )
            + "&codigo_puesto="
            + encodeURIComponent(
                CODIGO_PUESTO
            );

        return;
    }

    window.location.href =
        "/WEBService/index.php?page=puestos";
}

function obtenerNombreCompleto(oferente) {
    const nombreCompleto = obtenerValor(
        oferente,
        [
            "NombreCompleto",
            "nombreCompleto",
            "nombre_completo"
        ]
    );

    if (nombreCompleto) {
        return nombreCompleto;
    }

    const nombre = obtenerValor(
        oferente,
        [
            "Nombre",
            "nombre"
        ]
    );

    const primerApellido = obtenerValor(
        oferente,
        [
            "PrimerApellido",
            "primerApellido",
            "Apellido",
            "apellido"
        ]
    );

    const segundoApellido = obtenerValor(
        oferente,
        [
            "SegundoApellido",
            "segundoApellido"
        ]
    );

    return [
        nombre,
        primerApellido,
        segundoApellido
    ]
        .filter(Boolean)
        .join(" ");
}

function obtenerValor(
    objeto,
    propiedades
) {
    for (const propiedad of propiedades) {
        const valor = objeto[propiedad];

        if (
            valor !== undefined &&
            valor !== null &&
            String(valor).trim() !== ""
        ) {
            return String(valor).trim();
        }
    }

    return "";
}

function obtenerFechaActual() {
    const fecha = new Date();

    const anio = fecha.getFullYear();

    const mes = String(
        fecha.getMonth() + 1
    ).padStart(2, "0");

    const dia = String(
        fecha.getDate()
    ).padStart(2, "0");

    return `${anio}-${mes}-${dia}`;
}

function formatearFechaVisual(valor) {
    const fechaNormalizada =
        formatearFechaServicio(
            valor
        );

    if (!fechaNormalizada) {
        return "";
    }

    const partes =
        fechaNormalizada.split("-");

    if (partes.length !== 3) {
        return valor;
    }

    return `${partes[2]}/${partes[1]}/${partes[0]}`;
}

function formatearFechaServicio(valor) {
    if (!valor) {
        return "";
    }

    const texto = String(valor).trim();

    const coincidenciaWcf =
        texto.match(
            /\/Date\((-?\d+)(?:[+-]\d+)?\)\//
        );

    if (coincidenciaWcf) {
        const fecha = new Date(
            Number(
                coincidenciaWcf[1]
            )
        );

        return fechaAFormatoIso(
            fecha
        );
    }

    const coincidenciaIso =
        texto.match(
            /^(\d{4})-(\d{2})-(\d{2})/
        );

    if (coincidenciaIso) {
        return (
            coincidenciaIso[1]
            + "-"
            + coincidenciaIso[2]
            + "-"
            + coincidenciaIso[3]
        );
    }

    const coincidenciaLatina =
        texto.match(
            /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/
        );

    if (coincidenciaLatina) {
        return (
            coincidenciaLatina[3]
            + "-"
            + coincidenciaLatina[2]
                .padStart(2, "0")
            + "-"
            + coincidenciaLatina[1]
                .padStart(2, "0")
        );
    }

    const fecha = new Date(texto);

    if (Number.isNaN(fecha.getTime())) {
        return texto;
    }

    return fechaAFormatoIso(
        fecha
    );
}

function fechaAFormatoIso(fecha) {
    const anio = fecha.getFullYear();

    const mes = String(
        fecha.getMonth() + 1
    ).padStart(2, "0");

    const dia = String(
        fecha.getDate()
    ).padStart(2, "0");

    return `${anio}-${mes}-${dia}`;
}

function escaparHtml(valor) {
    return String(valor ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}