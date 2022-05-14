using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Security
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            string coneStr = ConfigurationManager.ConnectionStrings["Clinica"].ConnectionString;
            SqlConnection cn = new SqlConnection(coneStr);
            return cn;
        }
    }
}