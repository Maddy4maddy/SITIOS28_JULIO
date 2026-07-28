<?php

declare(strict_types=1);

namespace App\Services;

use App\Repositories\OferenteRepository;

class OferenteService
{
    private OferenteRepository $repository;

    public function __construct(
        OferenteRepository $repository
    ) {
        $this->repository = $repository;
    }

    public function obtenerPorPuesto(
        string $codigoPuesto
    ): array {
        $respuesta = $this->repository
            ->obtenerPorPuesto($codigoPuesto);

        if (!is_array($respuesta)) {
            return [];
        }

        return $respuesta;
    }

    public function obtenerDetalle(
        string $codigoOferente
    ): array {
        $respuesta = $this->repository
            ->obtenerDetalle($codigoOferente);

        if (!is_array($respuesta)) {
            return [];
        }

        return $respuesta;
    }
}