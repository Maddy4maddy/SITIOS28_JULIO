<?php
session_start();
header('Content-Type: application/json');

$response = ['success' => false, 'message' => ''];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $usuario = $_POST['usuario'] ?? '';
    $contrasena = $_POST['contrasena'] ?? '';

    if (empty($usuario) || empty($contrasena)) {
        $response['message'] = 'Usuario y contraseña son requeridos';
        echo json_encode($response);
        exit();
    }

    // URL 
    $wsUrl = 'http://localhost:61932/WEBSERVICEcore4.svc/Login';

    $data = json_encode([
        'Usuario' => $usuario,
        'Contrasena' => $contrasena
    ]);

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $wsUrl);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
    curl_setopt($ch, CURLOPT_HTTPHEADER, [
        'Content-Type: application/json',
        'Accept: application/json',
        'Content-Length: ' . strlen($data)
    ]);
    curl_setopt($ch, CURLOPT_TIMEOUT, 30);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
    
    $result = curl_exec($ch);
    
    if (curl_errno($ch)) {
        $response['message'] = 'Error de conexión: ' . curl_error($ch);
        curl_close($ch);
        echo json_encode($response);
        exit();
    }
    
    $httpCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    curl_close($ch);

    if ($httpCode === 200 && $result) {
        $loginResult = json_decode($result, true);
        
        if ($loginResult) {
            if (isset($loginResult['Exito']) && $loginResult['Exito'] === true) {
                $_SESSION['usuario'] = $loginResult['Nombre'] ?? $usuario;
                $_SESSION['id_usuario'] = $loginResult['IdUsuario'] ?? 0;
                $response['success'] = true;
                $response['message'] = 'Login exitoso';
                $response['redirect'] = 'puestos_core1.php';
            } else {
                $response['message'] = $loginResult['Mensaje'] ?? 'Credenciales incorrectas';
            }
        } else {
            $response['message'] = 'Respuesta inválida del servidor';
        }
    } else {
        $response['message'] = 'Error al conectar con el servicio de autenticación (HTTP ' . $httpCode . ')';
    }
}

echo json_encode($response);
?>