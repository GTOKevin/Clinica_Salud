using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class EspecialidadRes
    {
        public Header oHeader { get; set; }
        public List<Especialidad> oEspecialidad { get; set; }
    }
    public class Especialidad
    {
        public int id_especialidad { get; set; }
        public string nombre_especialidad { get; set; }
    }
}