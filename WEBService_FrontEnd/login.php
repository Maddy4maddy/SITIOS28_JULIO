<?php
session_start();

if(isset($_SESSION['usuario'])){
    header("Location: index.php");
    exit();
}
?>

<!DOCTYPE html>
<html lang="es">
<head>
<meta charset="UTF-8">
<title>AdminPersonal - Login</title>

<link rel="stylesheet" href="css/styles.css">

</head>

<body class="login-body">


<div class="login-container">

    <div class="login-box">

        <h1>AdminPersonal</h1>

        <input 
            type="text" 
            id="usuario" 
            placeholder="Usuario">


        <input 
            type="password" 
            id="contrasena" 
            placeholder="Contraseña">


        <button onclick="login()">
            Aceptar
        </button>


        <p id="mensaje"></p>


    </div>

</div>


<script src="js/login.js"></script>

</body>
</html>