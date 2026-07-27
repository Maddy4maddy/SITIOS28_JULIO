<!DOCTYPE html>
<html lang="es">

<head>

    <meta charset="UTF-8">

    <meta name="viewport"
          content="width=device-width, initial-scale=1.0">

    <title>
        AdminPersonal - Puestos Activos
    </title>

    <link rel="stylesheet"
          href="css/styles.css">

</head>

<body>

<header class="header">

    <div class="header-right">

        <span>
            Admin
        </span>

        <div class="avatar"></div>

    </div>

</header>

<div class="contenedor-principal">

    <aside class="sidebar">

        <div class="logo-sidebar">

            <div class="logo-circulo"></div>

            <h2>
                AdminPersonal
            </h2>

        </div>

        <nav class="menu-lateral">

            <a href="index.php">
                Principal
            </a>

            <a href="puestos.php"
               class="activo">

                Puestos Activos

            </a>

        </nav>

    </aside>

    <main class="contenido">

        <div class="titulo-seccion">

            Listado de Puestos Activos

        </div>

        <div class="tabla-contenedor">


            <table class="tabla">

                <thead>

                    <tr>

                        <th>
                            ID
                        </th>

                        <th>
                            Puesto
                        </th>

                        <th>
                            Salario Base
                        </th>

                    </tr>

                </thead>

                <tbody id="puestos-container">

                    <tr>

                        <td colspan="3"
                            class="loading">

                             Cargando puestos activos...

                        </td>

                    </tr>

                </tbody>

            </table>

        </div>

    </main>

</div>

<script src="js/main.js"></script>

</body>

</html>