
var row;
const getForm = () => {
    let resForm = {
        paciente: {},
        valida: true
    }

    $("#formRegister input").each(function (e) {
        if (this.name == "id_paciente") {
            resForm.paciente[this.name] = this.value;
        } else {
            if (this.value.trim().length > 0) {
                resForm.paciente[this.name] = this.value;
            } else {
                resForm.valida = false;
            }
        }
    })

    $("#formRegister select").each(function (e) {
        if (this.value.trim().length > 0) {
            resForm.paciente[this.name] = this.value;
        } else {
            resForm.valida = false;
        }
    })

    return resForm;

}

$("#formRegister").on('submit', function (e) {
    e.preventDefault();
    let { paciente, valida } = getForm();
    if (valida) {
        $.ajax({
            method: "POST",
            url: urlRegistroPac,
            data: paciente,
            responseType: 'json',
            success: async function (res) {
                if (res.oHeader.estado) {

                    getPaciente();
                    btnAction('cancel');
                    Swal.fire(
                        'Exito!',
                        res.oHeader.mensaje,
                        'success'
                    )
                } else {
                    Swal.fire(
                        'Oops!',
                        res.oHeader.mensaje,
                        'error'
                    )
                }
            },
            error: function (err) {
                Swal.fire(
                    'Oops!',
                    err,
                    'error'
                )
            }
        });
    } else {
        Swal.fire(
            'Info!',
            'por favor ingrese todos los campos',
            'info'
        )
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

const getPaciente = async () => {
    await $.ajax({
        method: "GET",
        url: urlListarPac,
        responseType: 'json',
        success: function (res) {
            console.log(res);
            let { oHeader, oPaciente } = res;
            if (oHeader.estado) {
                listarTable(oPaciente);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

getPaciente();

const listarTable = (data) => {
    let bodyTable = document.getElementById("bodyTable");
    console.log(data);
    bodyTable.innerHTML = "";
    let shtml = ``;
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {

            shtml += `<tr>`;
            shtml += `<td>${data[i].nombres}</td>`;
            shtml += `<td>${data[i].primer_apellido}</td>`;
            shtml += `<td>${data[i].segundo_apellido}</td>`;
            shtml += `<td>${data[i].correo_paciente}</td>`;
            shtml += `<td>${data[i].telefono_paciente}</td>`;
            shtml += `<td>
                            <button class="btn btn-info px-1 py-0" onclick="edit(this,'${data[i].id_paciente}')" ><i class="fa fa-pencil-alt"></i></button>
                         </td>`;
            shtml += `</tr>`;
        }
    }
    bodyTable.innerHTML = shtml;

}

const edit = (dt, id_pac) => {
    row = dt;
    $.ajax({
        method: "GET",
        url: urlListarPac + "?id=" + id_pac,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            if (res.oHeader.estado) {
                await viewEdit(res.oPaciente)
                $("#divListar").hide(500);
                $("#divRegister").show(1000);

            }


        },
        error: function (err) {
            Swal.close();
        }

    });
}

const viewEdit = (list) => {
    console.log(list);

    $(".val").each(function (ind) {
        for (var propName in list[0]) {
            if (this.name === propName) {
                this.value = list[0][propName];
            }
        }
    });
}