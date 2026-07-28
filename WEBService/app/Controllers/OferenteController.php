<?php

declare(strict_types=1);

namespace App\Controllers;

use App\Services\OferenteService;
use Throwable;

class OferenteController
{
    private OferenteService $service;

    public function __construct(
        OferenteService $service
    ) {
        $this->service = $service;
    }

    public function obtenerPorPuesto(
        string $codigoPuesto
    ): array {
        try {

            $oferentes = $this->service
                ->obtenerPorPuesto($codigoPuesto);

            return [
                'exito' => true,
                'mensaje' => '',
                'oferentes' => $oferentes
            ];

        } catch (Throwable $e) {

            return [
                'exito' => false,
                'mensaje' => $e->getMessage(),
                'oferentes' => []
            ];

        }
    }

    public function obtenerDetalle(
        string $codigoOferente
    ): array {
        try {

            $detalle = $this->service
                ->obtenerDetalle($codigoOferente);

            return [
                'exito' => true,
                'mensaje' => '',
                'detalle' => $detalle
            ];

        } catch (Throwable $e) {

            return [
                'exito' => false,
                'mensaje' => $e->getMessage(),
                'detalle' => []
            ];

        }
    }
}