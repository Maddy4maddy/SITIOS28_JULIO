<?php

declare(strict_types=1);

session_start();

header('Content-Type: application/json; charset=UTF-8');

$response = [
    'success' => false,
    'message' => ''
];


if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    http_response_code(405);

    $response['message'] =
        'Método no permitido.';

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

$usuario = trim(
    (string) ($_POST['usuario'] ?? '')
);

$contrasena = (string) (
    $_POST['contrasena'] ?? ''
);

if ($usuario === '' || $contrasena === '') {
    $response['message'] =
        'Debe ingresar el usuario y la contraseña.';

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}


$wsUrl =
    'http://localhost:61932/WEBSERVICEcore4.svc/Login';


$data = json_encode(
    [
        'Usuario' => $usuario,
        'Contrasena' => $contrasena
    ],
    JSON_UNESCAPED_UNICODE
);

if ($data === false) {
    $response['message'] =
        'No fue posible preparar la solicitud.';

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

if (!function_exists('curl_init')) {
    http_response_code(500);

    $response['message'] =
        'La extensión cURL de PHP no está habilitada.';

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

$ch = curl_init($wsUrl);

curl_setopt_array(
    $ch,
    [
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_POST => true,
        CURLOPT_POSTFIELDS => $data,
        CURLOPT_HTTPHEADER => [
            'Content-Type: application/json',
            'Accept: application/json',
            'Content-Length: ' . strlen($data)
        ],
        CURLOPT_CONNECTTIMEOUT => 10,
        CURLOPT_TIMEOUT => 30
    ]
);

$result = curl_exec($ch);

if ($result === false) {
    $error = curl_error($ch);

    curl_close($ch);

    http_response_code(502);

    $response['message'] =
        'No se pudo conectar con el servicio de autenticación: '
        . $error;

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

$httpCode = (int) curl_getinfo(
    $ch,
    CURLINFO_HTTP_CODE
);

curl_close($ch);


if ($httpCode !== 200) {
    http_response_code(502);

    $response['message'] =
        'El servicio de autenticación respondió con HTTP '
        . $httpCode
        . '.';

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

$loginResult = json_decode(
    $result,
    true
);


if (!is_array($loginResult)) {
    http_response_code(502);

    $response['message'] =
        'El servicio de autenticación devolvió '
        . 'una respuesta inválida.';

    $response['detalle'] = $result;

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

if (
    isset($loginResult['LoginResult'])
    && is_array($loginResult['LoginResult'])
) {
    $loginResult = $loginResult['LoginResult'];
}

$exito = filter_var(
    $loginResult['Exito'] ?? false,
    FILTER_VALIDATE_BOOLEAN
);

if (!$exito) {
    $response['message'] =
        (string) (
            $loginResult['Mensaje']
            ?? 'Usuario o contraseña incorrectos.'
        );

    echo json_encode(
        $response,
        JSON_UNESCAPED_UNICODE
    );

    exit;
}

session_regenerate_id(true);

$_SESSION['usuario'] =
    (string) (
        $loginResult['Nombre']
        ?? $usuario
    );

$_SESSION['id_usuario'] =
    (int) (
        $loginResult['IdUsuario']
        ?? 0
    );

$response = [
    'success' => true,
    'message' =>
        (string) (
            $loginResult['Mensaje']
            ?? 'Inicio de sesión exitoso.'
        ),

    'redirect' => 'index.php'
];

echo json_encode(
    $response,
    JSON_UNESCAPED_UNICODE
);