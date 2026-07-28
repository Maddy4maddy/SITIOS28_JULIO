<?php

declare(strict_types=1);

namespace App\Controllers;

use App\Services\EmpleadoService;
use Throwable;

class EmpleadoController
{
    private EmpleadoService $service;

    public function __construct(
        EmpleadoService $service
    ) {
        $this->service = $service;
    }

    public function crear(
        array $empleado
    ): array {
        try {
            return $this->service->crear(
                $empleado
            );
        } catch (Throwable $error) {
            return [
                'exito' => false,
                'mensaje' => $error->getMessage(),
                'idEmpleado' => 0,
                'numeroEmpleado' => '',
                'codigoHttp' => 500
            ];
        }
    }
}