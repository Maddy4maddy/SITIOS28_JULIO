<?php
session_start();

// Si ya está logueado, redirigir al index
if (isset($_SESSION["usuario"])) {
    header("Location: index.php");
    exit();
}
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AdminPersonal - Login</title>
    <link rel="stylesheet" href="css/styles.css">
    
    <link rel="icon" href="data:image/svg+xml,<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'><circle cx='50' cy='50' r='40' fill='%232d1866'/><text x='50' y='68' font-size='45' text-anchor='middle' fill='white' font-family='Arial'>A</text></svg>">
    
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <div class="login-container">
        <div class="login-box">
            <div class="login-header">
                <div class="logo-circulo"></div>
                <h1>AdminPersonal</h1>
                <p>Iniciar sesión</p>
            </div>
            
            <form id="loginForm" class="login-form">
                <div class="form-group">
                    <label for="usuario">Usuario</label>
                    <input type="text" id="usuario" name="usuario" required>
                </div>
                
                <div class="form-group">
                    <label for="contrasena">Contraseña</label>
                    <input type="password" id="contrasena" name="contrasena" required>
                </div>
                
                <div id="mensajeLogin" class="mensaje-login"></div>
                
                <button type="submit" class="btn-login">Iniciar sesión</button>
            </form>
        </div>
    </div>
    <script src="js/login.js"></script>
</body>
</html>