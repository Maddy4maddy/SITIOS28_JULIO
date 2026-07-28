document.addEventListener('DOMContentLoaded', function() {
    cargarPuestosActivos();
});

function cargarPuestosActivos() {
    const tbody = document.getElementById('puestos-core6-container');
    tbody.innerHTML = '<tr><td colspan="2" class="loading">Cargando puestos activos...</td></tr>';

    const wsUrl = 'http://localhost:61932/WEBServiceCORE1.svc/ObtenerPuestosActivos';
    const proxyUrl = `ws_proxy.php?url=${encodeURIComponent(wsUrl)}`;
    
    console.log('Consultando URL (vía proxy):', proxyUrl);
    
    fetch(proxyUrl, {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Error en la respuesta del servidor: ' + response.status);
        }
        return response.json();
    })
    .then(data => {
        console.log('Datos recibidos:', data);
        if (data.error) {
            throw new Error(data.error);
        }
        if (Array.isArray(data)) {
            mostrarPuestos(data);
        } else if (data.ObtenerPuestosActivosResult) {
            mostrarPuestos(data.ObtenerPuestosActivosResult);
        } else {
            mostrarPuestos(data);
        }
    })
    .catch(error => {
        console.error('Error al cargar puestos:', error);
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error">
                    <p>Error al cargar los puestos: ${error.message}</p>
                    <p style="font-size: 0.9em; color: #666; margin-top: 5px;">
                        Verifica que el WebService esté disponible
                    </p>
                </td>
            </tr>
        `;
    });
}

function mostrarPuestos(puestos) {
    const tbody = document.getElementById('puestos-core6-container');
    
    if (!puestos || puestos.length === 0) {
        tbody.innerHTML = '<tr><td colspan="2">No hay puestos activos disponibles.</td></tr>';
        return;
    }
    
    let html = '';
    puestos.forEach(puesto => {
        html += `
            <tr>
                <td><strong>${puesto.CodigoPuesto || 'N/A'}</strong></td>
                <td>
                    <a href="verOferente.php?codigo_puesto=${encodeURIComponent(puesto.CodigoPuesto || '')}&nombre_puesto=${encodeURIComponent(puesto.NombrePuesto || '')}" 
                       style="color: #198754; text-decoration: none;">
                        ${puesto.NombrePuesto || 'N/A'}
                    </a>
                </td>
            </tr>
        `;
    });
    
    tbody.innerHTML = html;
}