<?php

declare(strict_types=1);

if (!isset($_SESSION['usuario'])) {
    header('Location: /WEBService/index.php?page=login');
    exit;
}

$usuario = $_SESSION['usuario'];

if (is_array($usuario)) {
    $nombre = (string) (
        $usuario['Nombre']
        ?? $usuario['nombre']
        ?? $usuario['Usuario']
        ?? $usuario['usuario']
        ?? $usuario['NombreCompleto']
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

$idPuesto = filter_input(
    INPUT_GET,
    'id_puesto',
    FILTER_VALIDATE_INT
);

$codigoOferente = trim(
    (string) ($_GET['codigoOferente'] ?? '')
);

$codigoPuesto = trim(
    (string) ($_GET['codigo_puesto'] ?? '')
);

if (
    $idPuesto === false
    || $idPuesto === null
    || $idPuesto <= 0
    || $codigoOferente === ''
    || $codigoPuesto === ''
) {
    header('Location: /WEBService/index.php?page=puestos');
    exit;
}

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

$codigoOferenteSeguro = htmlspecialchars(
    $codigoOferente,
    ENT_QUOTES,
    'UTF-8'
);

$urlRegreso =
    '/WEBService/index.php?page=oferentes'
    . '&id_puesto='
    . rawurlencode((string) $idPuesto)
    . '&codigo_puesto='
    . rawurlencode($codigoPuesto);

$urlRegresoSeguro = htmlspecialchars(
    $urlRegreso,
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
        AdminPersonal - Detalle del oferente
    </title>

    <link
        rel="stylesheet"
        href="/WEBService/public/css/styles.css?v=9"
    >

    <link
        rel="stylesheet"
        href="/WEBService/public/css/oferentes.css?v=9"
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
            href="<?= $urlRegresoSeguro ?>"
            class="back-link"
        >
            ← Volver a oferentes
        </a>

        <h1>
            Detalle del oferente
        </h1>

        <p>
            Consulte la información del oferente seleccionado.
        </p>

    </div>

    <section class="detail-card">

        <div class="detail-card-header">

            <div>
                <h2>
                    Información del oferente
                </h2>

                <p>
                    Datos personales registrados en el sistema.
                </p>
            </div>

            <span class="detail-code">
                Código:
                <strong>
                    <?= $codigoOferenteSeguro ?>
                </strong>
            </span>

        </div>

        <div id="detalle-oferente">

            <p class="loading">
                Cargando información del oferente...
            </p>

        </div>

    </section>

</main>

<footer class="app-footer">
    AdminPersonal
</footer>

<script>
    const ID_PUESTO = <?= (int) $idPuesto ?>;

    const CODIGO_OFERENTE =
        <?= json_encode(
            $codigoOferente,
            JSON_UNESCAPED_UNICODE
            | JSON_UNESCAPED_SLASHES
        ) ?>;

    const CODIGO_PUESTO =
        <?= json_encode(
            $codigoPuesto,
            JSON_UNESCAPED_UNICODE
            | JSON_UNESCAPED_SLASHES
        ) ?>;
</script>

<script
    src="/WEBService/public/js/detalleOferenteEmpleado.js?v=9"
></script>

</body>
</html>