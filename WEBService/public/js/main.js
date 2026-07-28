/**
 * main.js
 * Consume el WebService WCF y muestra los puestos activos
 */

// ==========================================
// URL DEL WEBSERVICE
// ==========================================

// LOCAL
const URL_PUESTOS =
    "http://localhost:61932/WEBServiceCORE1.svc/ObtenerPuestosActivos";

// PRODUCCIÓN (Plesk)
// const URL_PUESTOS =
//     "https://tiusr22pl.cuc-carrera-ti.ac.cr/WEBServiceCORE1.svc/ObtenerPuestosActivos";

// ==========================================
// OBTENER PUESTOS
// ==========================================

async function obtenerPuestos() {

    const tbody =
        document.getElementById("puestos-container");

    if (!tbody) return;

    tbody.innerHTML = `
        <tr>
            <td colspan="3" class="loading">
                ⏳ Cargando puestos activos...
            </td>
        </tr>
    `;

    try {

        const response = await fetch(URL_PUESTOS, {

            method: "GET",

            mode: "cors",

            headers: {
                "Accept": "application/json"
            }

        });

        if (!response.ok) {

            throw new Error(
                `Error HTTP: ${response.status}`
            );

        }

        const text =
            await response.text();

        let puestos = [];
        // ==================================
        // RESPUESTA XML
        // ==================================

        if (text.trim().startsWith("<")) {


            puestos =
                parseXML(text);


        }
        // ==================================
        // RESPUESTA JSON
        // ==================================

        else {


            const data =
                JSON.parse(text);



            puestos =
                data.ObtenerPuestosActivosResult
                ||
                data;

        }
        // Ordenar por ID ascendente

        puestos.sort((a, b) => {

            const idA =
                parseInt(a.Id || a.id || 0);

            const idB =
                parseInt(b.Id || b.id || 0);

            return idA - idB;

        });

        if (!puestos || puestos.length === 0) {


            tbody.innerHTML = `
                <tr>
                    <td colspan="3" class="loading">
                        📭 No hay puestos activos disponibles
                    </td>
                </tr>
            `;


            return;

        }

        renderizarPuestos(
            tbody,
            puestos
        );

    }
    catch(error) {


        console.error(
            "Error:",
            error
        );



        tbody.innerHTML = `

            <tr>

                <td colspan="3"
                    class="loading"
                    style="color:#dc3545">

                    ❌ Error al cargar puestos:
                    ${error.message}

                </td>

            </tr>

        `;

    }

}
// ==========================================
// PARSER XML WCF
// ==========================================

function parseXML(xmlString) {


    const parser =
        new DOMParser();


    const xml =
        parser.parseFromString(
            xmlString,
            "text/xml"
        );



    const items =
        xml.getElementsByTagName(
            "PuestoDTO"
        );



    const puestos = [];



    for (
        let i = 0;
        i < items.length;
        i++
    ) {


        const item =
            items[i];



        puestos.push({

            Id:
                item.getElementsByTagName("Id")[0]
                ?.textContent
                ||
                "",


            CodigoPuesto:
                item.getElementsByTagName("CodigoPuesto")[0]
                ?.textContent
                ||
                "",


            NombrePuesto:
                item.getElementsByTagName("NombrePuesto")[0]
                ?.textContent
                ||
                "",


            Salario:
                item.getElementsByTagName("Salario")[0]
                ?.textContent
                ||
                0,


            Estado:
                item.getElementsByTagName("Estado")[0]
                ?.textContent
                ||
                "",


            FechaCreacion:
                item.getElementsByTagName("FechaCreacion")[0]
                ?.textContent
                ||
                ""

        });


    }



    return puestos;

}

// ==========================================
// MOSTRAR TABLA
// ==========================================

function renderizarPuestos(
    tbody,
    puestos
) {


    let html = "";



    puestos.forEach(puesto => {



        const id =
            puesto.Id
            ||
            puesto.id
            ||
            "N/A";



        const nombre =
            puesto.NombrePuesto
            ||
            puesto.nombrePuesto
            ||
            "Sin nombre";



        const salario =
            puesto.Salario
            ||
            puesto.salario
            ||
            0;



        html += `

            <tr>

                <td>${id}</td>

                <td>${nombre}</td>

                <td>
                    ₡ ${formatearSalario(salario)}
                </td>

            </tr>

        `;


    });



    tbody.innerHTML =
        html;

}

// ==========================================
// FORMATO MONEDA
// ==========================================

function formatearSalario(valor) {


    return Number(valor)
        .toLocaleString(
            "es-CR",
            {

                minimumFractionDigits:2,

                maximumFractionDigits:2

            }
        );

}
// ==========================================
// INICIO
// ==========================================

document.addEventListener(
    "DOMContentLoaded",
    () => {


        if(
            document.getElementById(
                "puestos-container"
            )
        ){

            obtenerPuestos();

        }


    }
);