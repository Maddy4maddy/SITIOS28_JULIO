<?php
/**
 * server.php - Script para iniciar el servidor PHP en localhost
 */

echo "========================================\n";
echo "  INICIANDO SERVIDOR PHP\n";
echo "========================================\n\n";

// Configuracion del servidor
$host = 'localhost';
$port = 8000;
$document_root = __DIR__;

echo "Directorio: " . $document_root . "\n";
echo "Servidor:   http://$host:$port\n";
echo "Archivo:    index.php\n\n";

echo "========================================\n";
echo "  Presiona Ctrl+C para detener el servidor\n";
echo "========================================\n\n";

echo "Servidor iniciado correctamente!\n";
echo "Abre tu navegador en: http://$host:$port\n";
echo "O visita: http://$host:$port/index.php\n\n";

// Usa la ruta completa de PHP de XAMPP
$phpPath = 'C:\xampp\php\php.exe';

// Ejecutar el servidor con la ruta completa de PHP
$command = "$phpPath -S $host:$port -t $document_root";
passthru($command);
?>