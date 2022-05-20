



const getForm = () => {
    let resForm = {
        doctor: {},
        valida:true
    }

    $("#formRegister input").each(function (e) {
        if (this.value.trim().length > 0) {
            resForm.doctor[this.name] = this.value;
        } else {
            resForm.valida = false;
        }
    })


    $("#formRegister select").each(function (e) {
        if (this.value.trim().length > 0) {
            resForm.doctor[this.name] = this.value;
        } else {
            resForm.valida = false;
        }
    })

    return resForm;
}



$("#formRegister").on('submit', function (e) {
    e.preventDefault();
    let { doctor, valida } = getForm();


    if (valida) {
        $.ajax({
            method: "POST",
            url: urlRegistroDoc,
            data: doctor,
            responseType: 'json',
            success: async function (res) {
                console.log(res);

                if (res.estado) {
                    getDoctor();
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
        alert("Ingresar todo los campos");
    }
})


const btnAction=(valor)=>{

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


const getDoctor = async() => {
   await  $.ajax({
        method: "GET",
        url: urlListarDoc,
       responseType: 'json',
       success: function (res) {
            console.log(res);
            let { oHeader, oDoctor } = res;
           if (oHeader.estado) {
               listarTable(oDoctor);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

getDoctor();


const listarTable = (data) => {
    let bodyTable = document.getElementById("bodyTable");
    bodyTable.innerHTML = "";
    let shtml = ``;
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {

            shtml += `<tr>`;
            shtml += `<td>${data[i].nombres}</td>`;
            shtml += `<td>${data[i].primer_apellido}</td>`;
            shtml += `<td>${data[i].segundo_apellido}</td>`;
            shtml += `<td>${data[i].correo_doctor}</td>`;
            shtml += `<td>${data[i].telefono_doctor}</td>`;
            shtml += `<td>${data[i].id_especialidad}</td>`;
            shtml += `</tr>`;
        }
    }
    bodyTable.innerHTML = shtml;

}

