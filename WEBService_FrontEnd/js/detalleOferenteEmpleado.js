document.addEventListener('DOMContentLoaded', function() {
    if (typeof ID_POSTULACION !== 'undefined' && ID_POSTULACION) {
        obtenerDetalleOferente(ID_POSTULACION);
    } else {
        document.getElementById('detalle-container').innerHTML = 
            '<p class="error">No se especificó un oferente válido.</p>';
    }
});

function obtenerDetalleOferente(idPostulacion) {
    const container = document.getElementById('detalle-container');
    container.innerHTML = '<p class="loading">Cargando información del oferente...</p>';
    
    // ws_proxy.php
    const wsUrl = `http://localhost:61932/WEBServiceCORE7.svc/ObtenerDetalleOferente?idPostulacion=${idPostulacion}`;
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
        console.log('Detalle recibido:', data);
        if (data.error) {
            throw new Error(data.error);
        }
        if (!data || data.IdPostulacion === 0) {
            throw new Error('No se encontró información del oferente');
        }
        mostrarDetalle(data);
    })
    .catch(error => {
        console.error('Error:', error);
        container.innerHTML = `
            <div class="error">
                <p>Error al cargar el detalle: ${error.message}</p>
                <p style="font-size: 0.9em; color: #666; margin-top: 5px;">
                    Verifica que el WebService CORE7 esté disponible
                </p>
            </div>
        `;
    });
}

function mostrarDetalle(oferente) {
    const container = document.getElementById('detalle-container');
    
    let nombreCompleto = oferente.Nombre || 'N/A';
    if (oferente.Apellido) {
        nombreCompleto = `${oferente.Nombre} ${oferente.Apellido}`;
    }
    
    const salario = Number(oferente.Salario || 0).toLocaleString('es-CR', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
    
    let html = `
        <div class="detalle-grid">
            <div class="detalle-item">
                <label>ID Postulación:</label>
                <span>${oferente.IdPostulacion || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Identificación:</label>
                <span>${oferente.Identificacion || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Nombre Completo:</label>
                <span>${nombreCompleto}</span>
            </div>
            <div class="detalle-item">
                <label>Email:</label>
                <span>${oferente.Email || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Teléfono:</label>
                <span>${oferente.Telefono || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Fecha Nacimiento:</label>
                <span>${oferente.FechaNacimiento || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Puesto:</label>
                <span>${oferente.NombrePuesto || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Código Puesto:</label>
                <span>${oferente.CodigoPuesto || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Salario:</label>
                <span>₡ ${salario}</span>
            </div>
            <div class="detalle-item">
                <label>Estado Puesto:</label>
                <span>${oferente.EstadoPuesto || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Fecha Postulación:</label>
                <span>${oferente.FechaPostulacion || 'N/A'}</span>
            </div>
            <div class="detalle-item">
                <label>Curriculum:</label>
                <span>
                    ${oferente.Curriculum ? 
                        `<a href="uploads/${oferente.Curriculum}" target="_blank" class="btn-cv">Ver CV</a>` : 
                        'Sin CV'}
                </span>
            </div>
        </div>
    `;
    
    container.innerHTML = html;
}