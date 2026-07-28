<?php

declare(strict_types=1);

namespace App\Repositories;

use App\Core\HttpClient;

class AuthRepository
{
    private HttpClient $httpClient;
    private string $loginUrl;

    public function __construct(
        HttpClient $httpClient,
        string $loginUrl
    ) {
        $this->httpClient = $httpClient;
        $this->loginUrl = $loginUrl;
    }

    public function login(
        string $usuario,
        string $contrasena
    ): array {
        return $this->httpClient->post(
            $this->loginUrl,
            [
                'Usuario' => $usuario,
                'Contrasena' => $contrasena,
            ]
        );
    }
}