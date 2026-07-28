<?php

declare(strict_types=1);

namespace App\Services;

use App\Repositories\PuestoRepository;

class PuestoService
{
    private PuestoRepository $repository;

    public function __construct(
        PuestoRepository $repository
    ) {
        $this->repository = $repository;
    }

    public function obtenerActivos(): array
    {
        $respuesta = $this->repository->obtenerActivos();

        if (
            isset($respuesta['ObtenerPuestosActivosResult']) &&
            is_array($respuesta['ObtenerPuestosActivosResult'])
        ) {
            $puestos = $respuesta['ObtenerPuestosActivosResult'];
        } else {
            $puestos = $respuesta;
        }

        if (!is_array($puestos)) {
            return [];
        }

        $resultado = [];

        foreach ($puestos as $puesto) {
            if (!is_array($puesto)) {
                continue;
            }

            $id = (int) (
                $puesto['Id']
                ?? $puesto['id']
                ?? $puesto['IdPuesto']
                ?? $puesto['idPuesto']
                ?? 0
            );

            $codigo = trim(
                (string) (
                    $puesto['CodigoPuesto']
                    ?? $puesto['codigoPuesto']
                    ?? $puesto['Codigo']
                    ?? $puesto['codigo']
                    ?? ''
                )
            );

            $nombre = trim(
                (string) (
                    $puesto['NombrePuesto']
                    ?? $puesto['nombrePuesto']
                    ?? $puesto['Nombre']
                    ?? $puesto['nombre']
                    ?? ''
                )
            );

            if (
                $id <= 0 ||
                ($codigo === '' && $nombre === '')
            ) {
                continue;
            }

            $resultado[] = [
                'id' => $id,
                'codigo' => $codigo,
                'nombre' => $nombre
            ];
        }

        usort(
            $resultado,
            static function (
                array $a,
                array $b
            ): int {
                return strnatcasecmp(
                    $a['codigo'],
                    $b['codigo']
                );
            }
        );

        return $resultado;
    }
}