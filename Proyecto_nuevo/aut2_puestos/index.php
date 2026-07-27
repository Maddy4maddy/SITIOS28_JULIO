<?php
ini_set('display_errors', 1);
error_reporting(E_ALL);

require_once "../config/conexion.php";

$sql = "SELECT id_puesto, nombre_puesto, descripcion, area, fecha_publicacion
        FROM puestos
        WHERE disponible = 1
        ORDER BY nombre_puesto ASC";

$consulta = $conexion->prepare($sql);
$consulta->execute();
$puestos = $consulta->fetchAll();
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Puestos disponibles</title>
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
    </nav>
</aside>

<div class="contenido-principal">

    <header class="barra-superior">
        <span>Admin</span>
        <div class="avatar-admin"></div>
    </header>

    <main class="area-contenido">

        <div class="titulo-morado">
            -PUESTOS DISPONIBLES
        </div>

        <section class="tarjeta-contenido">
            <h2>Puestos disponibles</h2>
            <p class="intro">
                Seleccione el puesto de su interés para registrar su postulación.
            </p>

            <?php if (count($puestos) > 0): ?>

                <div class="lista-puestos">

                    <?php foreach ($puestos as $puesto): ?>
                        <article class="puesto-item">

                            <h3>
                                <a href="../aut3-postulacion/index.php?id_puesto=<?php echo $puesto['id_puesto']; ?>">
                                    <?php echo htmlspecialchars($puesto['nombre_puesto']); ?>
                                </a>
                            </h3>

                            <p><strong>Área:</strong> <?php echo htmlspecialchars($puesto['area']); ?></p>

                            <p>
                                <?php echo htmlspecialchars($puesto['descripcion']); ?>
                            </p>

                            <p class="fecha">
                                Publicado el: <?php echo htmlspecialchars($puesto['fecha_publicacion']); ?>
                            </p>

                            <a class="btn-postular" href="../aut3-postulacion/index.php?id_puesto=<?php echo $puesto['id_puesto']; ?>">
                                Postularme
                            </a>

                        </article>
                    <?php endforeach; ?>

                </div>

            <?php else: ?>

                <div class="mensaje-vacio">
                    Actualmente no hay puestos disponibles.
                </div>

            <?php endif; ?>
        </section>

    </main>
</div>

</body>
</html>