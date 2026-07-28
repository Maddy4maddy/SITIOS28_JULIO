<?php

declare(strict_types=1);

namespace App\Services;

use App\Repositories\AuthRepository;
use InvalidArgumentException;

class AuthService
{
    private AuthRepository $repository;

    public function __construct(
        AuthRepository $repository
    ) {
        $this->repository = $repository;
    }

    public function login(
        string $usuario,
        string $contrasena
    ): array {
        $usuario = trim($usuario);
        $contrasena = trim($contrasena);

        if ($usuario === '') {
            throw new InvalidArgumentException(
                'Debe ingresar el usuario.'
            );
        }

        if ($contrasena === '') {
            throw new InvalidArgumentException(
                'Debe ingresar la contraseña.'
            );
        }

        return $this->repository->login(
            $usuario,
            $contrasena
        );
    }
}