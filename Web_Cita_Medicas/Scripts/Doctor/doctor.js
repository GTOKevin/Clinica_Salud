

var row;

const getForm = () => {
    let resForm = {
        doctor: {},
        valida:true
    }

    $("#formRegister input").each(function (e) {
        if (this.name == "id_doctor") {
            resForm.doctor[this.name] = this.value;
        } else {
            if (this.value.trim().length > 0) {
                resForm.doctor[this.name] = this.value;
            } else {
                resForm.valida = false;
            }
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
                if (res.oHeader.estado) {
                    
                    getDoctor();
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
    console.log(data);
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
            shtml += `<td>${data[i].oEspecialidad.nombre_especialidad}</td>`;
            if (data[i].estado) {
                shtml += `<td class="font-weight-bold text-success">Activo</td>`;
                shtml += `<td>
                            <button class="btn btn-danger px-1 py-0" onclick="change(this,'${data[i].id_doctor}','${data[i].estado}')" ><i class="fa fa-power-off"></i></button>
                            <button class="btn btn-info px-1 py-0" onclick="edit(this,'${data[i].id_doctor}')" ><i class="fa fa-pencil-alt"></i></button>
                         </td>`;

            } else {
                shtml += `<td class="font-weight-bold text-danger">Inactivo</td>`;
                shtml += `<td>
                            <button class="btn btn-success px-1 py-0" onclick="change(this,'${data[i].id_doctor}','${data[i].estado}')"><i class="fa fa-universal-access"></i></button>
                            <button class="btn btn-info px-1 py-0" onclick="edit(this,'${data[i].id_doctor}')" ><i class="fa fa-pencil-alt"></i></button>
                          </td>`;
            }
         
            shtml += `</tr>`;
        }
    }
    bodyTable.innerHTML = shtml;

}

const change = (dt, id_doc,state) => {
    $.ajax({
        method: "POST",
        url: urlChangeEstado,
        data: { id:id_doc, estado:state},
        responseType: 'json',
        success: async function (res) {
            if (res.estado) {
                await dibujarEstado(dt,state,id_doc);
            }

        },
        error: function (err) {
            Swal.close();
        }

    });
}

const dibujarEstado = (dt, state,id_doc) => {
    console.log(dt, state);
    let parent = (dt.parentElement).parentElement;
    console.log(parent.children);
    let { [6]: estado, [7]: btns } = parent.children;
    console.log(state);
    if (state=="true") {
        estado.innerHTML = 'Inactivo';
        estado.classList.remove("text-success");
        estado.classList.add("text-danger");
        btns.innerHTML =
           `
                            <button class="btn btn-danger px-1 py-0" onclick="change(this,'${id_doc}','false')"><i class="fa fa-power-off"></i></button>
                            <button class="btn btn-info px-1 py-0" onclick="edit(this,'${id_doc}')"><i class="fa fa-pencil-alt"></i></button>
          `;
    } else {
        console.log("llegamos");
        estado.innerHTML = 'Activo';
        estado.classList.remove("text-danger");
        estado.classList.add("text-success");
        btns.innerHTML =
            `
                            <button class="btn btn-success px-1 py-0" onclick="change(this,'${id_doc}','true')"><i class="fa fa-universal-access"></i></button>
                            <button class="btn btn-info px-1 py-0" onclick="edit(this,'${id_doc}')"><i class="fa fa-pencil-alt"></i></button>
     `
    }
}

const edit = (dt, id_doc) => {
    row = dt;
    $.ajax({
        method: "GET",
        url: urlListarDoc + "?id=" + id_doc,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            if (res.oHeader.estado) {
                await viewEdit(res.oDoctor)
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
//fa fa - universal - acce