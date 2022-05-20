using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Security;


namespace Web_Cita_Medicas.Logica
{
    public class LO_Genero
    {
        public GeneroRes ListarGeneros()
        {
            GeneroRes oGeneroRes = new GeneroRes();
            Header header = new Header();
            List<Genero> GeneroList = new List<Genero>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_genero", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Genero oGenero = new Genero();
                        oGenero.id_genero = Convert.ToInt32(dr["id_genero"].ToString());
                        oGenero.nombre_genero = dr["nombre_genero"].ToString();
                        GeneroList.Add(oGenero);
                    }
                    cn.Close();
                    cn.Dispose();

                    header.estado = true;
                }
            }
            catch (Exception ex)
            {
                header.estado = false;
                header.mensaje = ex.Message;
            }
            oGeneroRes.oGenero = GeneroList;
            oGeneroRes.oHeader = header;
            return oGeneroRes;
        }
    }
}