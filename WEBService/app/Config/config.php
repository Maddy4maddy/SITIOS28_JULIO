<?php

declare(strict_types=1);

return [
    'services' => [
        'login' =>
            'http://localhost:61932/WEBSERVICEcore4.svc/Login',

        'puestos_activos' =>
            'http://localhost:61932/WEBServiceCORE1.svc/ObtenerPuestosActivos',

        'oferentes_por_puesto' =>
            'http://localhost:61932/WEBServiceCORE7.svc/ObtenerOferentesPorPuesto',

        'detalle_oferente' =>
            'http://localhost:61932/WEBServiceCORE8.svc/ObtenerOferente',

        'crear_empleado' =>
            'http://localhost:61932/WEBServiceCORE3.svc/CrearEmpleado',
    ]
];