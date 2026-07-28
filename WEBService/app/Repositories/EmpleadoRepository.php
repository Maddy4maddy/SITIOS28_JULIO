<?php

declare(strict_types=1);

namespace App\Repositories;

use App\Core\HttpClient;
use RuntimeException;
use Throwable;

class EmpleadoRepository
{
    public function __construct(
        private readonly HttpClient $httpClient,
        private readonly string $urlCrearEmpleado
    ) {
    }

    public function crear(
        array $empleado
    ): array {
        try {
            $respuesta = $this->httpClient->post(
                $this->urlCrearEmpleado,
                $empleado
            );

            return [
                'exito' => (bool) (
                    $respuesta['Exito']
                    ?? $respuesta['exito']
                    ?? false
                ),
                'mensaje' => (
                    $respuesta['Mensaje']
                    ?? $respuesta['mensaje']
                    ?? ''
                ),
                'idEmpleado' => (
                    $respuesta['IdEmpleado']
                    ?? $respuesta['idEmpleado']
                    ?? 0
                ),
                'numeroEmpleado' => (
                    $respuesta['NumeroEmpleado']
                    ?? $respuesta['numeroEmpleado']
                    ?? ''
                ),
                'codigoHttp' => 201
            ];
        } catch (Throwable $e) {
            throw new RuntimeException(
                $e->getMessage(),
                0,
                $e
            );
        }
    }
}