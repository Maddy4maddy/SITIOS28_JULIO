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
            'oferente' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $codigoOferente = trim(
        (string) ($_GET['codigoOferente'] ?? '')
    );

    if ($codigoOferente === '') {
        http_response_code(400);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'Debe indicar el código del oferente.',
            'oferente' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $urlOferentes = $config['services']['oferentes_por_puesto'] ?? '';
    $urlDetalle = $config['services']['detalle_oferente'] ?? '';

    if ($urlDetalle === '') {
        throw new RuntimeException(
            'La dirección del servicio de detalle del oferente no está configurada.'
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

    $resultado = $controller->obtenerDetalle(
        $codigoOferente
    );

    http_response_code(
        $resultado['exito'] ? 200 : 404
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
        'oferente' => null
    ], JSON_UNESCAPED_UNICODE);
}