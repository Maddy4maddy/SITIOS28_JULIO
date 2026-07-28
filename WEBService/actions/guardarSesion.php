<?php

declare(strict_types=1);

ini_set('display_errors', '0');
error_reporting(E_ALL);

header('Content-Type: application/json; charset=utf-8');

try {
    require_once dirname(__DIR__) . '/bootstrap.php';

    if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
        http_response_code(405);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'Método no permitido.'
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $contenido = file_get_contents('php://input');

    $input = json_decode(
        $contenido ?: '',
        true
    );

    if (!is_array($input)) {
        $input = $_POST;
    }

    if (
        !isset($config['services']['login']) ||
        trim((string)$config['services']['login']) === ''
    ) {
        throw new RuntimeException(
            'La dirección del servicio de login no está configurada.'
        );
    }

    $httpClient = new \App\Core\HttpClient();

    $repository = new \App\Repositories\AuthRepository(
        $httpClient,
        $config['services']['login']
    );

    $service = new \App\Services\AuthService(
        $repository
    );

    $controller = new \App\Controllers\AuthController(
        $service
    );

    $resultado = $controller->login($input);

    http_response_code(
        !empty($resultado['exito']) ? 200 : 400
    );

    echo json_encode(
        $resultado,
        JSON_UNESCAPED_UNICODE
    );
} catch (\Throwable $error) {
    http_response_code(500);

    echo json_encode([
        'exito' => false,
        'mensaje' => $error->getMessage(),
        'archivo' => basename($error->getFile()),
        'linea' => $error->getLine()
    ], JSON_UNESCAPED_UNICODE);
}