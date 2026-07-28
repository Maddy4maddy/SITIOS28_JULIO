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
            'empleado' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
        http_response_code(405);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'Método no permitido.',
            'empleado' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $contenido = file_get_contents('php://input');

    if ($contenido === false || trim($contenido) === '') {
        http_response_code(400);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'No se recibieron datos para crear el empleado.',
            'empleado' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $datos = json_decode(
        $contenido,
        true
    );

    if (!is_array($datos)) {
        http_response_code(400);

        echo json_encode([
            'exito' => false,
            'mensaje' => 'La solicitud contiene un JSON inválido.',
            'empleado' => null
        ], JSON_UNESCAPED_UNICODE);

        exit;
    }

    $numeroEmpleado = trim(
        (string) ($datos['NumeroEmpleado'] ?? '')
    );

    $identificacion = trim(
        (string) ($datos['Identificacion'] ?? '')
    );

    $tipoIdentificacion = trim(
        (string) ($datos['TipoIdentificacion'] ?? '')
    );

    $nombreCompleto = trim(
        (string) ($datos['NombreCompleto'] ?? '')
    );

    $fechaNacimiento = trim(
        (string) ($datos['FechaNacimiento'] ?? '')
    );

    $correo = trim(
        (string) ($datos['Correo'] ?? '')
    );

    $telefono = trim(
        (string) ($datos['Telefono'] ?? '')
    );

    $idPuesto = filter_var(
        $datos['IdPuesto'] ?? null,
        FILTER_VALIDATE_INT
    );

    $fechaContratacion = trim(
        (string) ($datos['FechaContratacion'] ?? '')
    );

    $estado = trim(
        (string) ($datos['Estado'] ?? '')
    );

    if ($numeroEmpleado === '') {
        responderError(
            400,
            'Debe ingresar el número de empleado.'
        );
    }

    if ($identificacion === '') {
        responderError(
            400,
            'El oferente no tiene identificación.'
        );
    }

    if ($tipoIdentificacion === '') {
        responderError(
            400,
            'El oferente no tiene tipo de identificación.'
        );
    }

    if ($nombreCompleto === '') {
        responderError(
            400,
            'El oferente no tiene nombre completo.'
        );
    }

    if ($fechaNacimiento === '') {
        responderError(
            400,
            'El oferente no tiene fecha de nacimiento.'
        );
    }

    if ($correo === '') {
        responderError(
            400,
            'El oferente no tiene correo electrónico.'
        );
    }

    if ($telefono === '') {
        responderError(
            400,
            'El oferente no tiene teléfono.'
        );
    }

    if (
        $idPuesto === false
        || $idPuesto === null
        || $idPuesto <= 0
    ) {
        responderError(
            400,
            'Debe indicar un puesto válido.'
        );
    }

    if ($fechaContratacion === '') {
        responderError(
            400,
            'Debe indicar la fecha de contratación.'
        );
    }

    if (!fechaValida($fechaNacimiento)) {
        responderError(
            400,
            'La fecha de nacimiento no es válida.'
        );
    }

    if (!fechaValida($fechaContratacion)) {
        responderError(
            400,
            'La fecha de contratación no es válida.'
        );
    }

    if (
        $estado !== 'Activo'
        && $estado !== 'Inactivo'
    ) {
        responderError(
            400,
            'El estado del empleado no es válido.'
        );
    }

    $urlCrearEmpleado =
        $config['services']['crear_empleado'] ?? '';

    if ($urlCrearEmpleado === '') {
        throw new RuntimeException(
            'La dirección del servicio para crear empleados no está configurada.'
        );
    }

    $solicitud = [
        'NumeroEmpleado' => $numeroEmpleado,
        'Identificacion' => $identificacion,
        'TipoIdentificacion' => $tipoIdentificacion,
        'NombreCompleto' => $nombreCompleto,
        'FechaNacimiento' => $fechaNacimiento,
        'Correo' => $correo,
        'Telefono' => $telefono,
        'IdPuesto' => (int) $idPuesto,
        'FechaContratacion' => $fechaContratacion,
        'Estado' => $estado
    ];

    $httpClient = new App\Core\HttpClient();

    $repository = new App\Repositories\EmpleadoRepository(
        $httpClient,
        $urlCrearEmpleado
    );

    $service = new App\Services\EmpleadoService(
        $repository
    );

    $controller = new App\Controllers\EmpleadoController(
        $service
    );

    $resultado = $controller->crear(
        $solicitud
    );

    $codigoHttp = 400;

    if ($resultado['exito'] ?? false) {
        $codigoHttp = 201;
    } elseif (
        isset($resultado['codigoHttp'])
        && is_int($resultado['codigoHttp'])
    ) {
        $codigoHttp = $resultado['codigoHttp'];
    }

    http_response_code(
        $codigoHttp
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
        'empleado' => null
    ], JSON_UNESCAPED_UNICODE);
}

function responderError(
    int $codigoHttp,
    string $mensaje
): never {
    http_response_code(
        $codigoHttp
    );

    echo json_encode([
        'exito' => false,
        'mensaje' => $mensaje,
        'empleado' => null
    ], JSON_UNESCAPED_UNICODE);

    exit;
}

function fechaValida(
    string $fecha
): bool {
    $objetoFecha = DateTime::createFromFormat(
        'Y-m-d',
        $fecha
    );

    return $objetoFecha !== false
        && $objetoFecha->format('Y-m-d') === $fecha;
}