<?php

declare(strict_types=1);

namespace App\Controllers;

use App\Services\PuestoService;
use Throwable;

class PuestoController
{
    private PuestoService $service;

    public function __construct(
        PuestoService $service
    ) {
        $this->service = $service;
    }

    public function obtenerActivos(): array
    {
        try {
            $puestos = $this->service->obtenerActivos();

            return [
                'exito' => true,
                'mensaje' => count($puestos) > 0
                    ? 'Puestos cargados correctamente.'
                    : 'No hay puestos activos disponibles.',
                'puestos' => $puestos,
            ];
        } catch (Throwable $exception) {
            return [
                'exito' => false,
                'mensaje' => $exception->getMessage(),
                'puestos' => [],
            ];
        }
    }
}