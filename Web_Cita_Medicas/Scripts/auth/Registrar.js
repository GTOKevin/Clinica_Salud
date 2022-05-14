$("#formRegister").on("submit", function (e) {
    e.preventDefault();

    let res = validarCampos();
    let { form, valida } = res;


    if (valida) {

        $.ajax({
            method: "POST",
            url: urlRegister,
            data: form,
            responseType: 'json',
            success: function (res) {
                if (res.oHeader.estado) {
                    alert("usuario registradpo");
                } else {
                    alert(res.oHeader.mensaje);
                }

            },
            error: function (err) {
                console.log(err);
                alert("usuario incorrecto");
            }

        })
    } else {
        alert("por favor ingrese datos correctos en los campos")
    }

})

function validarCampos() {

    let clave = document.getElementsByName("clave")[0];
    let clave2 = document.getElementsByName("confirmarClave")[0];
    let respuesta = {
        form: {},
        valida: true
    }


    if (clave.value == clave2.value) {

        $("#formRegister input").each(function (e) {
            console.log(this);

            if (this.value.trim().length > 0) {
                respuesta.form[this.name] = this.value;
            } else {
                respuesta.valida = false;
            }
        })
    } else {
        respuesta.valida = false;
    }

    return respuesta;
}