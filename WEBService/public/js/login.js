document.addEventListener(
    "DOMContentLoaded",
    () => {
        const formulario =
            document.getElementById("loginForm");

        const mensaje =
            document.getElementById("mensajeLogin");

        const boton =
            document.getElementById("btn-login");

        if (!formulario) {
            return;
        }

        formulario.addEventListener(
            "submit",
            async (event) => {
                event.preventDefault();

                const usuario =
                    document
                        .getElementById("usuario")
                        .value
                        .trim();

                const contrasena =
                    document
                        .getElementById("contrasena")
                        .value;

                if (!usuario || !contrasena) {
                    mostrarMensaje(
                        "Debe ingresar el usuario y la contraseña."
                    );

                    return;
                }

                try {
                    boton.disabled = true;
                    boton.textContent =
                        "Validando...";

                    mostrarMensaje(
                        "Validando credenciales..."
                    );

                    const response = await fetch(
                        "actions/guardarSesion.php",
                        {
                            method: "POST",

                            headers: {
                                "Content-Type":
                                    "application/json"
                            },

                            body: JSON.stringify({
                                usuario,
                                contrasena
                            })
                        }
                    );

                    const data =
                        await response.json();

                    if (!response.ok || !data.exito) {
                        throw new Error(
                            data.mensaje
                            || "No fue posible iniciar sesión."
                        );
                    }

                    window.location.href =
                        data.redirect;

                } catch (error) {
                    mostrarMensaje(
                        error.message
                        || "No fue posible iniciar sesión."
                    );
                } finally {
                    boton.disabled = false;

                    boton.textContent =
                        "Iniciar sesión";
                }
            }
        );

        function mostrarMensaje(texto) {
            if (mensaje) {
                mensaje.textContent = texto;
            }
        }
    }
);