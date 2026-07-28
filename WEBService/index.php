<?php

declare(strict_types=1);

require_once __DIR__ . '/bootstrap.php';

$page = trim(
    (string) ($_GET['page'] ?? 'login')
);

$paginasProtegidas = [
    'bienvenida',
    'puestos',
    'oferentes',
    'detalleOferente'
];

if (
    in_array($page, $paginasProtegidas, true)
    && !isset($_SESSION['usuario'])
) {
    header(
        'Location: /WEBService/index.php?page=login'
    );

    exit;
}

switch ($page) {
    case 'bienvenida':
        require BASE_PATH
            . '/app/Views/bienvenida.php';
        break;

    case 'puestos':
        require BASE_PATH
            . '/app/Views/puestos.php';
        break;

    case 'oferentes':
        require BASE_PATH
            . '/app/Views/oferentes.php';
        break;

    case 'detalleOferente':
        require BASE_PATH
            . '/app/Views/detalleOferente.php';
        break;

    case 'login':
    default:
        if (isset($_SESSION['usuario'])) {
            header(
                'Location: /WEBService/index.php?page=bienvenida'
            );

            exit;
        }

        require BASE_PATH
            . '/app/Views/login.php';
        break;
}