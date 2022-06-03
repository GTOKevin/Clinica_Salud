
const getForm = () => {
    let resForm = {
        especialidad: {},
        valida: true
    }

    $("#formRegister input").each(function (e) {
        if (this.value.trim().length > 0) {
            resForm.especialidad[this.name] = this.value;
        } else {
            resForm.valida = false;
        }
    })


    $("#formRegister select").each(function (e) {
        if (this.value.trim().length > 0) {
            resForm.especialidad[this.name] = this.value;
        } else {
            resForm.valida = false;
        }
    })

    return resForm;
}

$("#formRegister").on('submit', function (e) {
    e.preventDefault();
    let { especialidad, valida } = getForm();


    if (valida) {
        $.ajax({
            method: "POST",
            url: urlRegistroEsp,
            data: especialidad,
            responseType: 'json',
            success: async function (res) {
                console.log(res);

                if (res.estado) {
                    getEspecialidad();
                    btnAction('cancel');
                } else {
                    alert(header.mensaje);
                }
            },
            error: function (err) {
                Swal.close();
            }

        });
    } else {
        alert("Ingresar el campo");
    }
})

const btnAction = (valor) => {

    switch (valor) {
        case 'cancel':
            $("#divRegister").hide(500);
            $("#divListar").show(1000);
            break;
        case 'new':
            $("#divListar").hide(500);
            $("#divRegister").show(1000);
            $("#formRegister")[0].reset();
            break;

    }
}


const getEspecialidad = async () => {
    await $.ajax({
        method: "GET",
        url: urlListarEsp,
        responseType: 'json',
        success: function (res) {
            console.log(res);
            let { oHeader, oEspecialidad } = res;
            if (oHeader.estado) {
                listarTable(oEspecialidad);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

getEspecialidad();

const listarTable = (data) => {
    let bodyTable = document.getElementById("bodyTable");
    bodyTable.innerHTML = "";
    let shtml = ``;
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {

            shtml += `<tr>`;
            shtml += `<td>${data[i].id_especialidad}</td>`;
            shtml += `<td>${data[i].nombre_especialidad}</td>`;
            shtml += `<td>${data[i].descripcion_especialidad}</td>`;
            shtml += `</tr>`;
        }
    }
    bodyTable.innerHTML = shtml;

}