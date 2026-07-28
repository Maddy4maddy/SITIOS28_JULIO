<?php

declare(strict_types=1);

namespace App\Controllers;

use App\Services\AuthService;
use Throwable;

class AuthController
{
    private AuthService $service;

    public function __construct(
        AuthService $service
    ) {
        $this->service = $service;
    }

    public function login(
        array $request
    ): array {
        try {
            $usuario = (string) (
                $request['usuario']
                ?? ''
            );

            $contrasena = (string) (
                $request['contrasena']
                ?? ''
            );

            $response = $this->service->login(
                $usuario,
                $contrasena
            );

            /*
             * Algunos WCF devuelven directamente el resultado.
             * Otros lo envuelven dentro de LoginResult.
             */
            $resultado =
                $response['LoginResult']
                ?? $response;

            $exito = filter_var(
                $resultado['Exito'] ?? false,
                FILTER_VALIDATE_BOOLEAN
            );

            if (!$exito) {
                return [
                    'exito' => false,

                    'mensaje' =>
                        $resultado['Mensaje']
                        ?? 'Usuario o contraseña incorrectos.',
                ];
            }

            session_regenerate_id(true);

            $_SESSION['usuario'] = [
                'id' =>
                    (int) (
                        $resultado['IdUsuario']
                        ?? 0
                    ),

                'nombre' =>
                    (string) (
                        $resultado['Nombre']
                        ?? $usuario
                    ),

                'usuario' => $usuario,
            ];

            return [
                'exito' => true,

                'mensaje' =>
                    $resultado['Mensaje']
                    ?? 'Inicio de sesión correcto.',

                'redirect' =>
                    'index.php?page=bienvenida',
            ];
        } catch (Throwable $exception) {
            return [
                'exito' => false,
                'mensaje' => $exception->getMessage(),
            ];
        }
    }
}