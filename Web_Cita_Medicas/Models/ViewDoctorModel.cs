using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cita_Medicas.Entity;

namespace Web_Cita_Medicas.Models
{
    public class ViewDoctorModel
    {
        public List<Doctor> ListaDoctor { get; set; }
        public List<Especialidad> ListaEspecialidad { get; set; }
        public List<Genero> ListaGenero { get; set; }
        public List<Tipo_Documento> ListaTipoDocumento { get; set; }
    }
}