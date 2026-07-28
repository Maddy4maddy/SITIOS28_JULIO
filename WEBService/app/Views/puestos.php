<?php

declare(strict_types=1);

if (!isset($_SESSION['usuario'])) {
    header('Location: /WEBService/index.php?page=login');
    exit;
}

$usuario = $_SESSION['usuario'];

if (is_array($usuario)) {
    $nombre = (string) (
        $usuario['nombre']
        ?? $usuario['Nombre']
        ?? $usuario['usuario']
        ?? $usuario['Usuario']
        ?? 'Usuario'
    );
} else {
    $nombre = (string) $usuario;
}

$nombre = trim($nombre);

if ($nombre === '') {
    $nombre = 'Usuario';
}

$inicial = function_exists('mb_substr')
    ? mb_substr($nombre, 0, 1, 'UTF-8')
    : substr($nombre, 0, 1);

$inicial = function_exists('mb_strtoupper')
    ? mb_strtoupper($inicial, 'UTF-8')
    : strtoupper($inicial);

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

    <title>
        AdminPersonal - Puestos activos
    </title>

    <link
        rel="stylesheet"
        href="/WEBService/public/css/styles.css?v=10"
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

<main class="content-page">

    <div class="page-header">

        <a
            href="/WEBService/index.php?page=bienvenida"
            class="back-link"
        >
            ← Volver al inicio
        </a>

        <h1>
            Puestos activos
        </h1>

        <p>
            Seleccione un puesto para consultar sus oferentes.
        </p>

    </div>

    <section class="table-card">

        <div class="table-card-header">

            <div>
                <h2>
                    Lista de puestos
                </h2>

                <p>
                    Puestos disponibles actualmente.
                </p>
            </div>

        </div>

        <div class="table-responsive">

            <table class="tabla">

                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Nombre</th>
                    </tr>
                </thead>

                <tbody id="puestos-container">
                    <tr>
                        <td colspan="2" class="loading">
                            Cargando puestos...
                        </td>
                    </tr>
                </tbody>

            </table>

        </div>

    </section>

</main>

<footer class="app-footer">
    AdminPersonal
</footer>

<script
    src="/WEBService/public/js/puesto.js?v=10"
></script>

</body>
</html>