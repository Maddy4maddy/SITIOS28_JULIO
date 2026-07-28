document.addEventListener('DOMContentLoaded', function() {
    if (typeof CODIGO_PUESTO !== 'undefined' && CODIGO_PUESTO) {
        obtenerOferentesPorPuesto(CODIGO_PUESTO);
    } else {
        document.getElementById('oferentes-container').innerHTML = 
            '<tr><td colspan="2" class="error">No se especificó un puesto válido.</td></tr>';
    }
});

function obtenerOferentesPorPuesto(codigoPuesto) {
    const tbody = document.getElementById('oferentes-container');
    tbody.innerHTML = '<tr><td colspan="2" class="loading">Cargando oferentes...</td></tr>';
    
    const wsUrl = `http://localhost:61932/WEBServiceCORE7.svc/ObtenerOferentesPorPuesto?codigoPuesto=${encodeURIComponent(codigoPuesto)}`;
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
        console.log('Oferentes recibidos:', data);
        if (data.error) {
            throw new Error(data.error);
        }
        if (!data || data.length === 0) {
            tbody.innerHTML = '<tr><td colspan="2">No hay oferentes que hayan postulado a este puesto.</td></tr>';
        } else {
            mostrarOferentes(data);
        }
    })
    .catch(error => {
        console.error('Error:', error);
        tbody.innerHTML = `
            <tr>
                <td colspan="2" class="error">
                    Error al cargar los oferentes: ${error.message}
                    <br><small>Verifica que el WebService CORE7 esté disponible</small>
                </td>
            </tr>
        `;
    });
}

function mostrarOferentes(oferentes) {
    const tbody = document.getElementById('oferentes-container');
    
    let html = '';
    oferentes.forEach(oferente => {
        // Construir nombre completo
        let nombreCompleto = oferente.Nombre || 'N/A';
        if (oferente.Apellido) {
            nombreCompleto = `${oferente.Nombre} ${oferente.Apellido}`;
        }
        
        html += `
            <tr>
                <td>${oferente.Identificacion || 'N/A'}</td>
                <td>
                    <a href="#" 
                       style="color: #198754; text-decoration: none; cursor: default;">
                        ${nombreCompleto}
                    </a>
                </td>
            </tr>
        `;
    });
    
    tbody.innerHTML = html;
}