<?php

declare(strict_types=1);

namespace App\Repositories;

use App\Core\HttpClient;

class OferenteRepository
{
    private HttpClient $httpClient;
    private string $oferentesPorPuestoUrl;
    private string $detalleOferenteUrl;

    public function __construct(
        HttpClient $httpClient,
        string $oferentesPorPuestoUrl,
        string $detalleOferenteUrl
    ) {
        $this->httpClient = $httpClient;

        $this->oferentesPorPuestoUrl =
            $oferentesPorPuestoUrl;

        $this->detalleOferenteUrl =
            $detalleOferenteUrl;
    }

    public function obtenerPorPuesto(
        string $codigoPuesto
    ): array {
        $url = $this->oferentesPorPuestoUrl
            . '?codigoPuesto='
            . rawurlencode($codigoPuesto);

        return $this->httpClient->get($url);
    }

    public function obtenerDetalle(
        string $codigoOferente
    ): array {
        $url = $this->detalleOferenteUrl
            . '?codigo='
            . rawurlencode($codigoOferente);

        return $this->httpClient->get($url);
    }
}