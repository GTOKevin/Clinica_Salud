using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cita_Medicas.Entity;

namespace Web_Cita_Medicas.Models
{
    public class ViewPacienteModel
    {
        public List<Paciente> ListaPaciente { get; set; }
        public List<Genero> ListaGenero { get; set; }
        public List<Tipo_Documento> ListaTipoDocumento { get; set; }
    }
}