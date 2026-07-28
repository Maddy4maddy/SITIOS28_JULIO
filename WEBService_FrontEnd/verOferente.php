<?php
session_start();

if (!isset($_SESSION['usuario'])) {
    header("Location: login.php");
    exit();
}

$nombre = $_SESSION['usuario'];
$inicial = strtoupper(substr($nombre, 0, 1));

$codigoPuesto = isset($_GET['codigo_puesto']) ? $_GET['codigo_puesto'] : '';
$nombrePuesto = isset($_GET['nombre_puesto']) ? $_GET['nombre_puesto'] : '';

if (empty($codigoPuesto)) {
    header("Location: puestos_core1.php");
    exit();
}
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AdminPersonal - Oferentes por Puesto</title>
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
            <a href="index.php">Principal</a>
            <a href="puestos_core1.php">Puestos</a>
        </nav>
    </aside>

    <main class="contenido">
        <div class="titulo-seccion">
            Oferentes - <?php echo htmlspecialchars($nombrePuesto, ENT_QUOTES, "UTF-8"); ?>
            <span class="sub-titulo">Código: <?php echo htmlspecialchars($codigoPuesto, ENT_QUOTES, "UTF-8"); ?></span>
        </div>

        <div style="margin-bottom: 15px;">
            <a href="puestos_core1.php" class="btn-volver">← Volver a puestos</a>
        </div>

        <div class="tabla-contenedor">
            <table class="tabla">
                <thead>
                    <tr>
                        <th>Identificación</th>
                        <th>Nombre del Oferente</th>
                    </tr>
                </thead>
                <tbody id="oferentes-container">
                    <tr>
                        <td colspan="2" class="loading">Cargando oferentes...</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </main>
</div>

<script>
    const CODIGO_PUESTO = '<?php echo htmlspecialchars($codigoPuesto, ENT_QUOTES, "UTF-8"); ?>';
</script>
<script src="js/verOferente.js"></script>
</body>
</html>