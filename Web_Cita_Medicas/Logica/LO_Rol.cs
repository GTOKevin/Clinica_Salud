using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Security;


namespace Web_Cita_Medicas.Logica
{
    public class LO_Rol
    {
        public List<Rol> ObtenerRoles()
        {
            List<Rol> roles = new List<Rol>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_rol", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Rol rol = new Rol();
                        rol.id_rol = Convert.ToInt32(dr["id_rol"].ToString());
                        rol.descripcion_rol = dr["descripcion_rol"].ToString();
                        roles.Add(rol);
                    }
                    cn.Close();
                    cn.Dispose();
                }
            }catch (Exception ex)
            {
                throw;
            }
            return roles;
        }
    }
}