<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "../config/conexion.php";

if (!isset($_GET['id_puesto']) || empty($_GET['id_puesto'])) {
    header("Location: ../aut2_puestos/");
    exit;
}

$id_puesto = $_GET['id_puesto'];

$sql = "SELECT id_puesto, nombre_puesto, descripcion, area
        FROM puestos
        WHERE id_puesto = :id_puesto AND disponible = 1";

$consulta = $conexion->prepare($sql);
$consulta->bindParam(":id_puesto", $id_puesto, PDO::PARAM_INT);
$consulta->execute();
$puesto = $consulta->fetch();

if (!$puesto) {
    header("Location: ../aut2_puestos/");
    exit;
}
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Postulación</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../assets/css/portal.css">
</head>
<body>

<aside class="sidebar">
    <div class="logo-circulo"></div>
    <h1>AdminPersonal</h1>

    <nav class="menu-lateral">
        <a href="../">Principal</a>
        <a href="../quienes-somos/">¿Quiénes somos?</a>
        <a href="../nuestros-valores-fundamentales/">Nuestros valores fundamentales</a>
        <a href="../donde-nos-ubicamos/">¿Dónde nos ubicamos?</a>
        <a href="../nuestros-servicios/">Nuestros servicios</a>
        <a href="../nuestro-equipo/">Nuestro equipo</a>
        <a href="../compromiso-con-la-comunidad/">Compromiso con la comunidad</a>
        <a href="../unete-a-nosotros/">Únete a nosotros</a>
        <a href="../beneficios-de-trabajar-con-nosotros/">Beneficios de trabajar con nosotros</a>
        <a href="../aut2_puestos/">Puestos disponibles</a>
    </nav>
</aside>

<div class="contenido-principal">

    <header class="barra-superior">
        <span>Admin</span>
        <div class="avatar-admin"></div>
    </header>

    <main class="area-contenido">

        <div class="titulo-morado">
            -REGISTRO DE POSTULACIÓN
        </div>

        <section class="tarjeta-contenido">
            <h2>Registrar postulación</h2>

            <div class="puesto-seleccionado">
                <h3>Puesto seleccionado:</h3>
                <p><strong><?php echo htmlspecialchars($puesto['nombre_puesto']); ?></strong></p>
                <p><strong>Área:</strong> <?php echo htmlspecialchars($puesto['area']); ?></p>
                <p><?php echo htmlspecialchars($puesto['descripcion']); ?></p>
            </div>

            <form action="guardar.php" method="POST" enctype="multipart/form-data" class="form-postulacion" id="formPostulacion">

                <input type="hidden" name="id_puesto" value="<?php echo htmlspecialchars($puesto['id_puesto']); ?>">

                <div class="grupo-form">
                    <label for="identificacion">Identificación:</label>
                    <input type="text" id="identificacion" name="identificacion" required>
                </div>

                <div class="grupo-form">
                    <label for="tipo_identificacion">Tipo identificación:</label>
                    <select id="tipo_identificacion" name="tipo_identificacion" required>
                        <option value="">Seleccione</option>
                        <option value="Cédula de identidad">Cédula de identidad</option>
                        <option value="DIMEX">DIMEX</option>
                        <option value="Pasaporte">Pasaporte</option>
                    </select>
                </div>

                <div class="grupo-form">
                    <label for="nombre_completo">Nombre completo:</label>
                    <input type="text" id="nombre_completo" name="nombre_completo" required>
                </div>

                <div class="grupo-form">
                    <label for="fecha_nacimiento">Fecha de nacimiento:</label>
                    <input type="date" id="fecha_nacimiento" name="fecha_nacimiento" required>
                </div>

                <div class="grupo-form">
                    <label for="correo">Correo electrónico:</label>
                    <input type="email" id="correo" name="correo" required>
                </div>

                <div class="grupo-form">
                    <label for="telefono">Teléfono de contacto:</label>
                    <input type="text" id="telefono" name="telefono" required>
                </div>

                <div class="grupo-form">
                    <label for="curriculum">Currículum:</label>
                    <input type="file" id="curriculum" name="curriculum" accept=".pdf,.doc,.docx" required>
                </div>

                <div class="acciones-form">
                    <button type="submit" class="btn-postular">Aceptar</button>
                    <a href="../aut2_puestos/" class="btn-cancelar">Cancelar</a>
                </div>

            </form>
        </section>

    </main>
</div>

<script src="../assets/js/validaciones.js"></script>

</body>
</html>