$("#formLogin").on("submit", function (e) {
    e.preventDefault();

   let res= validarCampos();
    let { form, valida } = res;


    if (valida) {

        $.ajax({
            method: "POST",
            url: urlLogin,
            data:form,
            responseType: 'json',
            success: function (res) {
                if (res.oHeader.estado) {
                    location.href = "/Home/Index";
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
        alert("por favor ingrese dato en los campos")
    }

})

function validarCampos() {
    let respuesta = {
        form: {},
        valida:true
    }
    $("#formLogin input").each(function (e) {
        console.log(this);

        if (this.value.trim().length > 0) {
            respuesta.form[this.name] = this.value;
        } else {
            respuesta.valida = false;
        }
    })
    return respuesta;
}