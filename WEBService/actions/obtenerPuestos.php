<?php

declare(strict_types=1);

require_once dirname(__DIR__) . '/bootstrap.php';

use App\Controllers\PuestoController;
use App\Core\HttpClient;
use App\Repositories\PuestoRepository;
use App\Services\PuestoService;

header('Content-Type: application/json; charset=utf-8');

if ($_SERVER['REQUEST_METHOD'] !== 'GET') {
    http_response_code(405);

    echo json_encode([
        'exito' => false,
        'mensaje' => 'Método no permitido.',
        'puestos' => []
    ]);

    exit;
}

if (!isset($_SESSION['usuario'])) {
    http_response_code(401);

    echo json_encode([
        'exito' => false,
        'mensaje' => 'Debe iniciar sesión.',
        'puestos' => []
    ]);

    exit;
}

$httpClient = new HttpClient();

$repository = new PuestoRepository(
    $httpClient,
    $config['services']['puestos_activos']
);

$service = new PuestoService($repository);

$controller = new PuestoController($service);

$resultado = $controller->obtenerActivos();

http_response_code(
    $resultado['exito'] ? 200 : 500
);

echo json_encode(
    $resultado,
    JSON_UNESCAPED_UNICODE
);