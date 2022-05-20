using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class Tipo_DocumentoRes
    {
        public Header oHeader { get; set; }
        public List<Tipo_Documento> oTipo_Documento { get; set; }
    }
    public class Tipo_Documento
    {
        public int id_tipo_documento { get; set; }
        public string nombre_documento { get; set; }
    }
}