<?php

declare(strict_types=1);

namespace App\Repositories;

use App\Core\HttpClient;

class PuestoRepository
{
    private HttpClient $httpClient;
    private string $puestosUrl;

    public function __construct(
        HttpClient $httpClient,
        string $puestosUrl
    ) {
        $this->httpClient = $httpClient;
        $this->puestosUrl = $puestosUrl;
    }
    
    public function obtenerActivos(): array
    {
        return $this->httpClient->get(
            $this->puestosUrl
        );
    }
}