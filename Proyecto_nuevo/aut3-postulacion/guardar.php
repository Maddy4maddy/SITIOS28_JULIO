<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "../config/conexion.php";

if ($_SERVER["REQUEST_METHOD"] !== "POST") {
    header("Location: ../aut2_puestos/");
    exit;
}

$id_puesto = $_POST["id_puesto"] ?? "";
$identificacion = trim($_POST["identificacion"] ?? "");
$tipo_identificacion = trim($_POST["tipo_identificacion"] ?? "");
$nombre_completo = trim($_POST["nombre_completo"] ?? "");
$fecha_nacimiento = trim($_POST["fecha_nacimiento"] ?? "");
$correo = trim($_POST["correo"] ?? "");
$telefono = trim($_POST["telefono"] ?? "");

if (
    empty($id_puesto) ||
    empty($identificacion) ||
    empty($tipo_identificacion) ||
    empty($nombre_completo) ||
    empty($fecha_nacimiento) ||
    empty($correo) ||
    empty($telefono)
) {
    die("Todos los campos son obligatorios.");
}

if (!filter_var($correo, FILTER_VALIDATE_EMAIL)) {
    die("El formato del correo no es válido.");
}

if (!preg_match('/^[0-9+\-\s]{8,20}$/', $telefono)) {
    die("El formato del teléfono no es válido.");
}

if (!isset($_FILES["curriculum"]) || $_FILES["curriculum"]["error"] !== UPLOAD_ERR_OK) {
    die("Debe adjuntar el currículum.");
}

$archivo = $_FILES["curriculum"];
$nombreOriginal = $archivo["name"];
$extension = strtolower(pathinfo($nombreOriginal, PATHINFO_EXTENSION));
$extensionesPermitidas = ["pdf", "doc", "docx"];

if (!in_array($extension, $extensionesPermitidas)) {
    die("El currículum debe ser PDF, DOC o DOCX.");
}

try {
    $conexion->beginTransaction();

    $sqlValidar = "SELECT identificacion, nombre_completo, correo, telefono
                   FROM oferentes
                   WHERE identificacion = :identificacion
                      OR correo = :correo
                      OR telefono = :telefono
                   LIMIT 1";

    $consultaValidar = $conexion->prepare($sqlValidar);
    $consultaValidar->execute([
        ":identificacion" => $identificacion,
        ":correo" => $correo,
        ":telefono" => $telefono
    ]);

    $oferenteExistente = $consultaValidar->fetch();

    if ($oferenteExistente) {
        if ($oferenteExistente["identificacion"] !== $identificacion) {
            throw new Exception("El correo o teléfono ya pertenece a otro oferente.");
        }

        if (
            strtolower(trim($oferenteExistente["nombre_completo"])) !== strtolower($nombre_completo) ||
            strtolower(trim($oferenteExistente["correo"])) !== strtolower($correo) ||
            trim($oferenteExistente["telefono"]) !== $telefono
        ) {
            throw new Exception("La identificación ingresada ya se encuentra registrada con otros datos.");
        }
    }

    $carpetaDestino = "../uploads/curriculums/";

    if (!is_dir($carpetaDestino)) {
        mkdir($carpetaDestino, 0777, true);
    }

    $nombreArchivo = "cv_" . preg_replace('/[^A-Za-z0-9]/', '', $identificacion) . "_" . time() . "." . $extension;
    $rutaArchivo = $carpetaDestino . $nombreArchivo;

    if (!move_uploaded_file($archivo["tmp_name"], $rutaArchivo)) {
        throw new Exception("No se pudo guardar el currículum.");
    }

    if (!$oferenteExistente) {
        $sqlOferente = "INSERT INTO oferentes 
            (identificacion, tipo_identificacion, nombre_completo, fecha_nacimiento, correo, telefono, postulado)
            VALUES 
            (:identificacion, :tipo_identificacion, :nombre_completo, :fecha_nacimiento, :correo, :telefono, 1)";

        $consultaOferente = $conexion->prepare($sqlOferente);
        $consultaOferente->execute([
            ":identificacion" => $identificacion,
            ":tipo_identificacion" => $tipo_identificacion,
            ":nombre_completo" => $nombre_completo,
            ":fecha_nacimiento" => $fecha_nacimiento,
            ":correo" => $correo,
            ":telefono" => $telefono
        ]);
    } else {
        $sqlActualizar = "UPDATE oferentes
                          SET postulado = 1
                          WHERE identificacion = :identificacion";

        $consultaActualizar = $conexion->prepare($sqlActualizar);
        $consultaActualizar->execute([
            ":identificacion" => $identificacion
        ]);
    }

    $sqlPostulacion = "INSERT INTO postulaciones_puestos 
        (identificacion, id_puesto, curriculum, fecha_postulacion)
        VALUES 
        (:identificacion, :id_puesto, :curriculum, NOW())";

    $consultaPostulacion = $conexion->prepare($sqlPostulacion);
    $consultaPostulacion->execute([
        ":identificacion" => $identificacion,
        ":id_puesto" => $id_puesto,
        ":curriculum" => $nombreArchivo
    ]);

    $conexion->commit();

    header("Location: mensaje.php");
    exit;

} catch (Exception $e) {
    if ($conexion->inTransaction()) {
        $conexion->rollBack();
    }

    die("Error: " . $e->getMessage());
}
?>