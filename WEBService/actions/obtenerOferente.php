<?php

declare(strict_types=1);

ini_set('display_errors', '0');

header('Content-Type: application/json; charset=utf-8');

try {
    require_once dirname(__DIR__) . '/bootstrap.php';

    if (!isset($_SESSION['usuario'])) {
        http_response_code(401);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'Debe iniciar sesión.',
            'oferentes' => []
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $codigoPuesto = trim(
        (string) ($_GET['codigoPuesto'] ?? '')
    );

    if ($codigoPuesto === '') {
        http_response_code(400);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'Debe indicar el código del puesto.',
            'oferentes' => []
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $urlOferentes = $config['services']['oferentes_por_puesto'] ?? '';
    $urlDetalle = $config['services']['detalle_oferente'] ?? '';

    if ($urlOferentes === '' || $urlDetalle === '') {
        throw new RuntimeException(
            'Las direcciones del servicio de oferentes no están configuradas.'
        );
    }

    $httpClient = new App\Core\HttpClient();

    $repository = new App\Repositories\OferenteRepository(
        $httpClient,
        $urlOferentes,
        $urlDetalle
    );

    $service = new App\Services\OferenteService(
        $repository
    );

    $controller = new App\Controllers\OferenteController(
        $service
    );

    $resultado = $controller->obtenerPorPuesto(
        $codigoPuesto
    );

    http_response_code(
        $resultado['exito'] ? 200 : 500
    );

    echo json_encode(
        $resultado,
        JSON_UNESCAPED_UNICODE
        | JSON_INVALID_UTF8_SUBSTITUTE
    );
} catch (Throwable $error) {
    http_response_code(500);

    echo json_encode([
        'exito' => false,
        'mensaje' => $error->getMessage(),
        'oferentes' => []
    ], JSON_UNESCAPED_UNICODE);
}