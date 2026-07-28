<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1.0"
    >

    <title>
        Iniciar sesión | AdminPersonal
    </title>

    <link
        rel="stylesheet"
        href="public/css/styles.css?v=1"
    >
</head>

<body class="login-page">

    <main class="login-wrapper">

        <section class="login-panel">

            <div class="login-brand">

                <div class="login-logo">
                    AP
                </div>

                <div>
                    <h1>AdminPersonal</h1>

                    <p>
                        Sistema de Gestión de Personal
                    </p>
                </div>

            </div>

            <div class="login-heading">

                <h2>Iniciar sesión</h2>

                <p>
                    Ingrese sus credenciales
                    para acceder al sistema.
                </p>

            </div>

            <form
                id="loginForm"
                class="login-form"
            >

                <div class="form-group">

                    <label for="usuario">
                        Usuario
                    </label>

                    <input
                        type="text"
                        id="usuario"
                        name="usuario"
                        autocomplete="username"
                        placeholder="Ingrese su usuario"
                        required
                    >

                </div>

                <div class="form-group">

                    <label for="contrasena">
                        Contraseña
                    </label>

                    <input
                        type="password"
                        id="contrasena"
                        name="contrasena"
                        autocomplete="current-password"
                        placeholder="Ingrese su contraseña"
                        required
                    >

                </div>

                <button
                    type="submit"
                    id="btn-login"
                    class="btn-primary"
                >
                    Iniciar sesión
                </button>

                <p
                    id="mensajeLogin"
                    class="form-message"
                    role="alert"
                ></p>

            </form>

        </section>

    </main>

    <script src="public/js/login.js?v=1"></script>

</body>
</html>