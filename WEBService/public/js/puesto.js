document.addEventListener(
    "DOMContentLoaded",
    cargarPuestosActivos
);

async function cargarPuestosActivos() {

    const tbody = document.getElementById(
        "puestos-container"
    );

    const mensaje = document.getElementById(
        "mensaje-puestos"
    );

    if (!tbody) {
        return;
    }

    tbody.innerHTML = `
        <tr>
            <td colspan="2" class="loading-cell">
                Cargando puestos activos...
            </td>
        </tr>
    `;

    if (mensaje) {
        mensaje.textContent = "";
    }

    try {

        const response = await fetch(
            "actions/obtenerPuestos.php",
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
                "La respuesta del servidor no es válida."
            );

        }

        if (response.status === 401) {

            window.location.href =
                "index.php?page=login";

            return;

        }

        if (!response.ok || !data.exito) {

            throw new Error(
                data.mensaje ??
                "No fue posible cargar los puestos."
            );

        }

        mostrarPuestos(
            data.puestos
        );

    } catch (error) {

        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error-cell">
                    ${escaparHtml(error.message)}
                </td>
            </tr>
        `;

        if (mensaje) {
            mensaje.textContent =
                error.message;
        }

    }

}

function mostrarPuestos(puestos) {

    const tbody = document.getElementById(
        "puestos-container"
    );

    if (
        !Array.isArray(puestos) ||
        puestos.length === 0
    ) {

        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="empty-cell">
                    No hay puestos activos disponibles.
                </td>
            </tr>
        `;

        return;

    }

    tbody.innerHTML = puestos.map(
        puesto => {

            const id =
                Number(
                    puesto.id ??
                    puesto.Id ??
                    0
                );

            const codigo =
                puesto.codigo ??
                puesto.CodigoPuesto ??
                "";

            const nombre =
                puesto.nombre ??
                puesto.NombrePuesto ??
                "";

            const url =
                "index.php?page=oferentes"
                + "&id_puesto="
                + encodeURIComponent(id)
                + "&codigo_puesto="
                + encodeURIComponent(codigo)
                + "&nombre_puesto="
                + encodeURIComponent(nombre);

            return `
                <tr>

                    <td>
                        <strong>
                            ${escaparHtml(codigo)}
                        </strong>
                    </td>

                    <td>

                        <a
                            href="${url}"
                            class="puesto-link"
                        >
                            ${escaparHtml(nombre)}
                        </a>

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