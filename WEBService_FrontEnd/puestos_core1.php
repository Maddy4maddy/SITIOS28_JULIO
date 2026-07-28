<?php
session_start();

if (!isset($_SESSION['usuario'])) {
    header("Location: login.php");
    exit();
}

$nombre = $_SESSION['usuario'];
$inicial = strtoupper(substr($nombre, 0, 1));
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AdminPersonal - Puestos</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>

<header class="header">
    <div class="header-right">
        <span><strong><?php echo htmlspecialchars($nombre, ENT_QUOTES, "UTF-8"); ?></strong></span>
        <a href="logout.php" class="btn-logout">Cerrar sesión</a>
        <div class="avatar"><?php echo htmlspecialchars($inicial, ENT_QUOTES, "UTF-8"); ?></div>
    </div>
</header>

<div class="contenedor-principal">
    <aside class="sidebar">
        <div class="logo-sidebar">
            <div class="logo-circulo"></div>
            <h2>AdminPersonal</h2>
        </div>
        <nav class="menu-lateral">
            <a href="puestos_core1.php" class="active">Puestos</a>
            <a href="logout.php">Cerrar Sesión</a>
        </nav>
    </aside>

    <main class="contenido">
        <div class="titulo-seccion">
            Puestos Activos
            <span class="sub-titulo">Seleccione un puesto para ver sus oferentes</span>
        </div>

        <div class="tabla-contenedor">
            <table class="tabla">
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Nombre del Puesto</th>
                    </tr>
                </thead>
                <tbody id="puestos-core6-container">
                    <tr>
                        <td colspan="2" class="loading">Cargando puestos...</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>
</div>

<script src="js/main_core6.js"></script>
</body>
</html>