<?php

declare(strict_types=1);

if (session_status() === PHP_SESSION_NONE) {
    session_start();
}

define(
    'BASE_PATH',
    __DIR__
);

$config = require BASE_PATH
    . '/app/Config/config.php';

spl_autoload_register(
    function (string $class): void {
        $prefix = 'App\\';

        if (!str_starts_with(
            $class,
            $prefix
        )) {
            return;
        }

        $relativeClass = substr(
            $class,
            strlen($prefix)
        );

        $file = BASE_PATH
            . '/app/'
            . str_replace(
                '\\',
                '/',
                $relativeClass
            )
            . '.php';

        if (file_exists($file)) {
            require_once $file;
        }
    }
);