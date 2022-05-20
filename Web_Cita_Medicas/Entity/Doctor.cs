using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class DoctorRes
    {
        public Header oHeader { get; set; }
        public List<Doctor> oDoctor { get; set; }
    }
    public class Doctor
    {
        public int id_doctor { get; set; }
        public string dni_doctor { get; set; }
        public string nombres { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string telefono_doctor { get; set; }
        public string correo_doctor { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_genero { get; set; }
        public int id_especialidad { get; set; }
        public int id_usuario { get; set; }
    }
}