document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('loginForm');
    const mensaje = document.getElementById('mensajeLogin');

    form.addEventListener('submit', function(e) {
        e.preventDefault();

        const usuario = document.getElementById('usuario').value.trim();
        const contrasena = document.getElementById('contrasena').value.trim();

        if (!usuario || !contrasena) {
            mensaje.textContent = 'Por favor, complete todos los campos';
            mensaje.className = 'mensaje-login error';
            return;
        }

        mensaje.textContent = 'Validando credenciales...';
        mensaje.className = 'mensaje-login loading';

        const formData = new FormData();
        formData.append('usuario', usuario);
        formData.append('contrasena', contrasena);

        fetch('server.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                mensaje.textContent = 'Login exitoso, redirigiendo...';
                mensaje.className = 'mensaje-login success';
                
                //Redirige a la URL que viene del servidor o a puestos por defecto
                setTimeout(() => {
                    const redirectUrl = data.redirect || 'puestos_core1.php';
                    window.location.href = redirectUrl;
                }, 1000);
            } else {
                mensaje.textContent = data.message || 'Error al iniciar sesión';
                mensaje.className = 'mensaje-login error';
            }
        })
        .catch(error => {
            mensaje.textContent = 'Error de conexión: ' + error.message;
            mensaje.className = 'mensaje-login error';
        });
    });
});