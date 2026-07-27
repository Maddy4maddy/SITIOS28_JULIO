<?php

session_start();


$_SESSION['idUsuario']=$_POST['id'];

$_SESSION['usuario']=$_POST['nombre'];


echo "ok";

?>