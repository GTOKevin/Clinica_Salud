using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class RolRes
    {
        public Header oHeader { get; set; }
        public Rol oRol { get; set; }
    }
    public class Rol
    {
        public int id_rol { get; set; }
        public string descripcion_rol { get; set; }
    }
}