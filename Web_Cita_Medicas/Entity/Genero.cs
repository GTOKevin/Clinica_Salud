using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{

    public class GeneroRes
    {
        public Header oHeader { get; set; }
        public List<Genero> oGenero { get; set; }
    }

    public class Genero
    {
        public int id_genero { get; set; }
        public string nombre_genero { get; set; }
    }
}