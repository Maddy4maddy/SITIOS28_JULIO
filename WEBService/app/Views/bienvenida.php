<?php

declare(strict_types=1);

if (!isset($_SESSION['usuario'])) {
    header('Location: /WEBService/index.php?page=login');
    exit;
}

$usuarioSesion = $_SESSION['usuario'];

if (is_array($usuarioSesion)) {
    $nombre = (string) (
        $usuarioSesion['Nombre']
        ?? $usuarioSesion['nombre']
        ?? $usuarioSesion['Usuario']
        ?? $usuarioSesion['usuario']
        ?? $usuarioSesion['NombreCompleto']
        ?? 'Usuario'
    );
} else {
    $nombre = (string) $usuarioSesion;
}

$nombre = trim($nombre);

if ($nombre === '') {
    $nombre = 'Usuario';
}

$inicial = strtoupper(
    substr($nombre, 0, 1)
);

$nombreSeguro = htmlspecialchars(
    $nombre,
    ENT_QUOTES,
    'UTF-8'
);

$inicialSegura = htmlspecialchars(
    $inicial,
    ENT_QUOTES,
    'UTF-8'
);
?>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1.0"
    >

    <title>AdminPersonal - Principal</title>

    <link
        rel="stylesheet"
        href="/WEBService/public/css/styles.css?v=4"
    >
</head>
<body class="app-body">

<header class="topbar">

    <div class="topbar-brand">

        <div class="brand-icon">
            AP
        </div>

        <div>
            <span class="brand-title">
                AdminPersonal
            </span>

            <span class="brand-subtitle">
                Gestión de personal
            </span>
        </div>

    </div>

    <div class="topbar-user">

        <div class="user-info">
            <span class="user-label">
                Usuario activo
            </span>

            <strong>
                <?= $nombreSeguro ?>
            </strong>
        </div>

        <div class="brand-icon">
            <?= $inicialSegura ?>
        </div>

        <a
            href="/WEBService/actions/logout.php"
            class="btn-logout"
        >
            Cerrar sesión
        </a>

    </div>

</header>

<main class="welcome-page">

    <section class="welcome-hero">

        <span class="welcome-badge">
            Panel principal
        </span>

        <h1>
            Bienvenido, <?= $nombreSeguro ?>
        </h1>

        <p>
            Desde esta sección puede consultar los puestos activos y revisar
            los oferentes asociados a cada puesto.
        </p>

    </section>

    <section class="option-grid">

        <a
            href="/WEBService/index.php?page=puestos"
            class="option-card"
        >

            <div class="option-icon">
                P
            </div>

            <div class="option-content">

                <h2>
                    Ver puestos
                </h2>

                <p>
                    Consulte la lista de puestos activos y seleccione uno para
                    revisar sus oferentes.
                </p>

                <span class="option-link">
                    Ir a puestos →
                </span>

            </div>

        </a>

    </section>

</main>

<footer class="app-footer">
    AdminPersonal
</footer>

</body>
</html>