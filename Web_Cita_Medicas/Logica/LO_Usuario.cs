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
    public class LO_Usuario
    {


        public UsuarioRes registrarUsuarioMaster (Usuario enti)
        {
            UsuarioRes usuarioRes = new UsuarioRes(); 
            Header header = new Header();
            enti.clave = EncryptMD5.Encrypt(enti.clave);
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_registrar_usuario_master", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombres", enti.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", enti.apellidos);
                    cmd.Parameters.AddWithValue("@correo", enti.correo);
                    cmd.Parameters.AddWithValue("@clave", enti.clave);
                    cmd.Parameters.AddWithValue("@id_rol", enti.id_rol);
                    cmd.ExecuteNonQuery();

                    cn.Close();
                    cn.Dispose();

                    header.estado = true;
                    header.mensaje = "correcto";
                }
            }catch (Exception ex)
            {
                header.estado = false;
                header.mensaje = ex.Message;
            }
            usuarioRes.oHeader= header;
            return usuarioRes;
        }
        public UsuarioRes LoginUsuario(Usuario enti)
        {
            UsuarioRes usuarioRes = new UsuarioRes();
            Header header = new Header();
            List<Usuario> usuarioList = new List<Usuario>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_login_usuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", enti.correo);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.id_usuario = Convert.ToInt32(dr["id_usuario"].ToString());
                        usuario.nombres = dr["nombres"].ToString();
                        usuario.apellidos = dr["apellidos"].ToString();
                        usuario.correo = dr["correo"].ToString();
                        usuario.clave = dr["clave"].ToString();
                        usuario.id_rol = Convert.ToInt32(dr["id_rol"].ToString());
                        usuarioList.Add(usuario);
                    }
                    cn.Close();
                    cn.Dispose();
                    if (usuarioList.Count > 0)
                    {
                        var user = usuarioList.FirstOrDefault();
                        user.clave = EncryptMD5.Decrypt(user.clave);
                        if (user.clave == enti.clave)
                        {
                            user.clave = null;
                            usuarioRes.oUsuario = user;
                            header.estado = true;
                            header.mensaje = "correcto";
                        }
                        else
                        {
                            header.estado = false;
                            header.mensaje = "corre u/o clave incorrecta";
                        }
                    }
                    else
                    {
                        header.estado = false;
                        header.mensaje = "corre u/o clave incorrecta";
                    }
                }
            }
            catch (Exception ex)
            {
                header.estado = false;
                header.mensaje = ex.Message;
            }
            usuarioRes.oHeader = header;
            return usuarioRes;
        }
    }
}