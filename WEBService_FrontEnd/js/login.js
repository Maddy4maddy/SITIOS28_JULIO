async function login(){

    const usuario = document.getElementById("usuario").value;
    const contrasena = document.getElementById("contrasena").value;

    const mensaje = document.getElementById("mensaje");


    if(usuario === "" || contrasena === ""){

        mensaje.innerHTML =
        "Usuario y/o contraseña incorrectos.";

        return;
    }


    try{


        const response = await fetch(
        "http://localhost:61932/WEBSERVICEcore4.svc/Login",
        {
            method:"POST",

            headers:{
                "Content-Type":"application/json"
            },

            body:JSON.stringify({

                Usuario:usuario,
                Contrasena:contrasena

            })
        });



        const data = await response.json();



        if(data.Exito){


            // Guardamos sesión PHP

            const formData = new FormData();

            formData.append(
                "id",
                data.IdUsuario
            );

            formData.append(
                "nombre",
                data.Nombre
            );


            await fetch(
                "guardarSesion.php",
                {
                    method:"POST",
                    body:formData
                }
            );


            window.location.href="index.php";


        }
        else{

            mensaje.innerHTML=data.Mensaje;

        }



    }catch(error){

        console.error(error);

        mensaje.innerHTML=
        "Error al conectar con el servicio.";

    }


}