<?php
session_start();

if (!isset($_SESSION["usuario"])) {
    header("Location: login.php");
    exit();
}

$nombre = $_SESSION["usuario"];
$inicial = strtoupper(substr($nombre, 0, 1));
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AdminPersonal - Inicio</title>
    <link rel="stylesheet" href="css/styles.css">
    
    
    <link rel="icon" href="data:image/svg+xml,<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'><circle cx='50' cy='50' r='40' fill='%232d1866'/><text x='50' y='68' font-size='45' text-anchor='middle' fill='white' font-family='Arial'>A</text></svg>">
    
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>

<header class="header">
    <div class="header-right">
        <span><strong><?php echo htmlspecialchars($nombre, ENT_QUOTES, "UTF-8"); ?></strong></span>
        <div class="avatar"><?php echo htmlspecialchars($inicial, ENT_QUOTES, "UTF-8"); ?></div>
        <a href="logout.php" class="btn-logout">Cerrar sesión</a>
    </div>
</header>

<div class="contenedor-principal">
    <aside class="sidebar">
        <div class="logo-sidebar">
            <div class="logo-circulo"></div>
            <h2>AdminPersonal</h2>
        </div>
        <nav class="menu-lateral">
            <a href="index.php">Principal</a>
            <a href="crearEmpleado.php">Crear Empleado</a>
            <a href="puestos_core1.php">Puestos</a>
        </nav>
    </aside>

    <main class="contenido">
        <div class="titulo-seccion">
            Bienvenido, <?php echo htmlspecialchars($nombre, ENT_QUOTES, "UTF-8"); ?>
        </div>
        <div class="dashboard-cards">
            <div class="card">
                <h3>Gestión de Empleados</h3>
                <p>Administra la información de los empleados</p>
                <a href="crearEmpleado.php" class="btn-card">Crear Empleado</a>
            </div>
            <div class="card">
                <h3>Puestos Activos</h3>
                <p>Consulta los puestos disponibles</p>
                <a href="puestos_core1.php" class="btn-card">Ver Puestos</a>
            </div>
        </div>
    </main>
</div>

<script src="js/main.js"></script>
</body>
</html>