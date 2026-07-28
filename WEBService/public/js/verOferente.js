document.addEventListener("DOMContentLoaded", () => {
    const tbody = document.getElementById(
        "oferentes-container"
    );

    if (!tbody) {
        return;
    }

    if (
        typeof CODIGO_PUESTO === "undefined" ||
        !CODIGO_PUESTO
    ) {
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error-cell">
                    No se indicó un puesto válido.
                </td>
            </tr>
        `;

        return;
    }

    if (
        typeof ID_PUESTO === "undefined" ||
        !Number.isInteger(Number(ID_PUESTO)) ||
        Number(ID_PUESTO) <= 0
    ) {
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error-cell">
                    No se recibió el ID del puesto.
                </td>
            </tr>
        `;

        return;
    }

    obtenerOferentesPorPuesto(
        CODIGO_PUESTO
    );
});

async function obtenerOferentesPorPuesto(
    codigoPuesto
) {
    const tbody = document.getElementById(
        "oferentes-container"
    );

    tbody.innerHTML = `
        <tr>
            <td colspan="2" class="loading-cell">
                Cargando oferentes...
            </td>
        </tr>
    `;

    try {
        const url =
            "/WEBService/actions/obtenerOferente.php"
            + "?codigoPuesto="
            + encodeURIComponent(codigoPuesto);

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
                "El servidor no devolvió JSON. Respuesta: "
                + texto.substring(0, 300)
            );
        }

        if (!response.ok || !data.exito) {
            throw new Error(
                data.mensaje ||
                "No fue posible obtener los oferentes."
            );
        }

        mostrarOferentes(
            data.oferentes
        );
    } catch (error) {
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error-cell">
                    ${escaparHtml(error.message)}
                </td>
            </tr>
        `;
    }
}

function mostrarOferentes(oferentes) {
    const tbody = document.getElementById(
        "oferentes-container"
    );

    if (
        !Array.isArray(oferentes) ||
        oferentes.length === 0
    ) {
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="empty-cell">
                    No existen oferentes para este puesto.
                </td>
            </tr>
        `;

        return;
    }

    tbody.innerHTML = oferentes.map(
        oferente => {
            const codigoOferente =
                oferente.CodigoOferente ??
                oferente.codigoOferente ??
                oferente.codigo_oferente ??
                oferente.CODIGO_OFERENTE ??
                "";

            const identificacion =
                oferente.Identificacion ??
                oferente.identificacion ??
                oferente.NumeroIdentificacion ??
                oferente.numeroIdentificacion ??
                "";

            const nombre =
                oferente.NombreCompleto ??
                oferente.nombreCompleto ??
                [
                    oferente.Nombre ??
                    oferente.nombre ??
                    "",

                    oferente.PrimerApellido ??
                    oferente.primerApellido ??
                    oferente.Apellido ??
                    oferente.apellido ??
                    "",

                    oferente.SegundoApellido ??
                    oferente.segundoApellido ??
                    ""
                ]
                    .filter(Boolean)
                    .join(" ");

            const urlDetalle =
                "/WEBService/index.php"
                + "?page=detalleOferente"
                + "&codigoOferente="
                + encodeURIComponent(
                    codigoOferente
                )
                + "&id_puesto="
                + encodeURIComponent(
                    Number(ID_PUESTO)
                )
                + "&codigo_puesto="
                + encodeURIComponent(
                    CODIGO_PUESTO
                );

            const enlace = codigoOferente
                ? `
                    <a
                        href="${urlDetalle}"
                        class="puesto-link"
                    >
                        ${escaparHtml(
                            nombre ||
                            "Ver oferente"
                        )}
                    </a>
                `
                : `
                    <span
                        title="El oferente no tiene código asignado"
                    >
                        ${escaparHtml(
                            nombre ||
                            "Sin nombre"
                        )}
                    </span>
                `;

            return `
                <tr>
                    <td>
                        ${escaparHtml(
                            identificacion
                        )}
                    </td>

                    <td>
                        ${enlace}
                    </td>
                </tr>
            `;
        }
    ).join("");
}

function escaparHtml(valor) {
    return String(valor ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}