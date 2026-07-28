<?php

declare(strict_types=1);

namespace App\Services;

use App\Repositories\EmpleadoRepository;

class EmpleadoService
{
    private EmpleadoRepository $repository;

    public function __construct(
        EmpleadoRepository $repository
    ) {
        $this->repository = $repository;
    }

    public function crear(
        array $empleado
    ): array {
        return $this->repository->crear(
            $empleado
        );
    }
}