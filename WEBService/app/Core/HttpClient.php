<?php

declare(strict_types=1);

namespace App\Core;

use RuntimeException;

class HttpClient
{
    public function get(string $url): array
    {
        return $this->request(
            'GET',
            $url
        );
    }

    public function post(
        string $url,
        array $data
    ): array {
        return $this->request(
            'POST',
            $url,
            $data
        );
    }

    private function request(
        string $method,
        string $url,
        ?array $data = null
    ): array {
        $url = trim($url);

        if ($url === '') {
            throw new RuntimeException(
                'La dirección del Web Service está vacía.'
            );
        }

        $curl = curl_init();

        if ($curl === false) {
            throw new RuntimeException(
                'No fue posible inicializar cURL.'
            );
        }

        $options = [
            CURLOPT_URL => $url,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_CONNECTTIMEOUT => 10,
            CURLOPT_TIMEOUT => 30,
            CURLOPT_HTTPHEADER => [
                'Accept: application/json',
                'Content-Type: application/json; charset=utf-8'
            ]
        ];

        if ($method === 'POST') {
            $json = json_encode(
                $data,
                JSON_UNESCAPED_UNICODE
                | JSON_INVALID_UTF8_SUBSTITUTE
            );

            if ($json === false) {
                curl_close($curl);

                throw new RuntimeException(
                    'No fue posible convertir los datos a JSON.'
                );
            }

            $options[CURLOPT_POST] = true;
            $options[CURLOPT_POSTFIELDS] = $json;
        }

        curl_setopt_array(
            $curl,
            $options
        );

        $response = curl_exec($curl);

        if ($response === false) {
            $error = curl_error($curl);

            curl_close($curl);

            throw new RuntimeException(
                'No se pudo conectar con el Web Service: '
                . $error
            );
        }

        $statusCode = (int) curl_getinfo(
            $curl,
            CURLINFO_HTTP_CODE
        );

        curl_close($curl);

        $decoded = json_decode(
            $response,
            true
        );

        if ($statusCode < 200 || $statusCode >= 300) {
            $mensaje = '';

            if (is_array($decoded)) {
                $mensaje = trim(
                    (string) (
                        $decoded['Mensaje']
                        ?? $decoded['mensaje']
                        ?? ''
                    )
                );
            }

            if ($mensaje === '') {
                $mensaje =
                    'El Web Service respondió con código HTTP '
                    . $statusCode
                    . '.';
            }

            throw new RuntimeException(
                $mensaje,
                $statusCode
            );
        }

        if (json_last_error() !== JSON_ERROR_NONE) {
            throw new RuntimeException(
                'El Web Service no devolvió un JSON válido. Respuesta: '
                . $response
            );
        }

        return is_array($decoded)
            ? $decoded
            : [];
    }
}