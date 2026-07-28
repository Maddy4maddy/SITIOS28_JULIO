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

$codigoPuesto = trim(
    (string) ($_GET['codigo_puesto'] ?? '')
);

$nombrePuesto = trim(
    (string) ($_GET['nombre_puesto'] ?? '')
);

if (
    $idPuesto === false
    || $idPuesto === null
    || $idPuesto <= 0
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

$codigoSeguro = htmlspecialchars(
    $codigoPuesto,
    ENT_QUOTES,
    'UTF-8'
);

$puestoSeguro = htmlspecialchars(
    $nombrePuesto,
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

    <title>AdminPersonal - Oferentes</title>

    <link
        rel="stylesheet"
        href="/WEBService/public/css/oferentes.css?v=4"
    >

    <style>
        .oferentes-actions{
            display:flex;
            justify-content:flex-end;
            margin-bottom:20px;
        }

        .btn-cancelar{
            display:inline-flex;
            align-items:center;
            justify-content:center;
            padding:10px 26px;
            background:#ffffff;
            color:#003366;
            text-decoration:none;
            font-weight:600;
            border:2px solid #003366;
            border-radius:10px;
            transition:all .25s ease;
        }

        .btn-cancelar:hover{
            background:#003366;
            color:#ffffff;
            box-shadow:0 4px 12px rgba(0,51,102,.25);
            transform:translateY(-1px);
        }
    </style>

</head>

<body>

<header class="oferentes-topbar">

    <div class="oferentes-brand">

        <div class="oferentes-logo">
            AP
        </div>

        <div class="oferentes-brand-text">
            <strong>AdminPersonal</strong>
            <span>Gestión de personal</span>
        </div>

    </div>

    <div class="oferentes-user">

        <div class="oferentes-user-info">
            <span>Usuario activo</span>
            <strong><?= $nombreSeguro ?></strong>
        </div>

        <div class="oferentes-avatar">
            <?= $inicialSegura ?>
        </div>

        <a
            href="/WEBService/actions/logout.php"
            class="oferentes-logout"
        >
            Cerrar sesión
        </a>

    </div>

</header>

<main class="oferentes-main">

    <section class="oferentes-heading">

        <div class="oferentes-actions">

            <a
                href="/WEBService/index.php?page=puestos"
                class="btn-cancelar"
            >
                Cancelar
            </a>

        </div>

        <div class="oferentes-title">

            <h1>Oferentes</h1>

            <p>
                <?= $puestoSeguro !== ''
                    ? $puestoSeguro
                    : 'Puesto seleccionado'
                ?>
            </p>

        </div>

        <p class="oferentes-instruction">
            Seleccione el nombre de un oferente para consultar su información.
        </p>

    </section>

    <section class="oferentes-card">

        <div class="oferentes-card-header">

            <h2>Lista de oferentes</h2>

            <span>
                Código del puesto:
                <strong><?= $codigoSeguro ?></strong>
            </span>

        </div>

        <div class="oferentes-table-wrapper">

            <table class="oferentes-table">

                <thead>
                    <tr>
                        <th>Identificación</th>
                        <th>Nombre del oferente</th>
                    </tr>
                </thead>

                <tbody id="oferentes-container">
                    <tr>
                        <td
                            colspan="2"
                            class="loading-cell"
                        >
                            Cargando oferentes...
                        </td>
                    </tr>
                </tbody>

            </table>

        </div>

    </section>

</main>

<script>
    const ID_PUESTO = <?= (int) $idPuesto ?>;

    const CODIGO_PUESTO =
        <?= json_encode(
            $codigoPuesto,
            JSON_UNESCAPED_UNICODE
            | JSON_UNESCAPED_SLASHES
        ) ?>;
</script>

<script src="/WEBService/public/js/verOferente.js?v=6"></script>

</body>
</html>