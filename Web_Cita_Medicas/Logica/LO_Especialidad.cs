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
    public class LO_Especialidad
    {


        public EspecialidadRes ListarEspecialidades()
        {
            EspecialidadRes oEspecialidadRes = new EspecialidadRes();
            Header header = new Header();
            List<Especialidad> EspecialidadList = new List<Especialidad>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_especialidad", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Especialidad oEspecialidad = new Especialidad();
                        oEspecialidad.id_especialidad = Convert.ToInt32(dr["id_Especialidad"].ToString());
                        oEspecialidad.nombre_especialidad = dr["nombre_especialidad"].ToString();
                        EspecialidadList.Add(oEspecialidad);
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
            oEspecialidadRes.oEspecialidad = EspecialidadList;
            oEspecialidadRes.oHeader = header;
            return oEspecialidadRes;
        }
    }
}