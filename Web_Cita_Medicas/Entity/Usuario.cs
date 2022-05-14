using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class UsuarioRes
    {
        public Usuario oUsuario { get; set; }
        public Header oHeader { get; set; }
    }
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public int id_rol { get; set; }
    }
}