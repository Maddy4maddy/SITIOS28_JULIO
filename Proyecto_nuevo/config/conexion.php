<?php
$host = "localhost";
$usuario = "tiusr16cp";
$password = "EvyecE#Sj+Tk";
$baseDatos = "tiusr16cp_administracion_personal";

try {
    $conexion = new PDO(
        "mysql:host=$host;dbname=$baseDatos;charset=utf8mb4",
        $usuario,
        $password
    );

    $conexion->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $conexion->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);

} catch (PDOException $e) {
    die("Error de conexión con la base de datos.");
}
?>