<?php
session_start();

if (!isset($_SESSION['usuario'])) {
    header("Location: login.php");
    exit();
}

$nombre = $_SESSION['usuario'];
$inicial = strtoupper(substr($nombre, 0, 1));

$idPostulacion = isset($_GET['id']) ? $_GET['id'] : '';
$codigoPuesto = isset($_GET['codigo_puesto']) ? $_GET['codigo_puesto'] : '';

if (empty($idPostulacion)) {
    header("Location: puestos_core1.php");
    exit();
}
?>
<!DOCTYPE html>
<html lang="es">
<head>
    
    <link rel="icon" href="data:image/svg+xml,<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'><circle cx='50' cy='50' r='40' fill='%232d1866'/><text x='50' y='68' font-size='45' text-anchor='middle' fill='white' font-family='Arial'>A</text></svg>">
    
    <link rel="stylesheet" href="css/styles.css">
</head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AdminPersonal - Detalle del Oferente</title>
    <link rel="stylesheet" href="css/styles.css">
    <style>
        .detalle-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
        }
        .detalle-item {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
        }
        .detalle-item label {
            display: block;
            font-weight: bold;
            color: #2d1866;
            margin-bottom: 5px;
            font-size: 14px;
        }
        .detalle-item span {
            font-size: 16px;
            color: #333;
        }
        .btn-cv {
            display: inline-block;
            padding: 8px 16px;
            background: #2d1866;
            color: white;
            border-radius: 5px;
            text-decoration: none;
        }
        .btn-cv:hover {
            background: #42258c;
        }
        @media (max-width: 768px) {
            .detalle-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
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
            Detalle del Oferente
            <span class="sub-titulo">Información completa del oferente</span>
        </div>

        <div style="margin-bottom: 15px;">
            <a href="verOferente.php?codigo_puesto=<?php echo urlencode($codigoPuesto); ?>" class="btn-volver">
                ← Volver a oferentes
            </a>
        </div>

        <div id="detalle-container">
            <p class="loading">Cargando información del oferente...</p>
        </div>
    </main>
</div>

<script>
    const ID_POSTULACION = <?php echo json_encode($idPostulacion); ?>;
</script>
<script src="js/detalleOferenteEmpleado.js"></script>
</body>
</html>