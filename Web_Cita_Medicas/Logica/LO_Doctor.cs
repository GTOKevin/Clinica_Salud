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
    public class LO_Doctor
    {
        public DoctorRes ListarDoctores()
        {
            DoctorRes oDoctorRes = new DoctorRes();
            Header header = new Header();
            List<Doctor> DoctorList = new List<Doctor>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_Doctor", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Doctor oDoctor = new Doctor();
                        oDoctor.id_doctor = Convert.ToInt32(dr["id_doctor"].ToString());
                        oDoctor.dni_doctor = dr["dni_doctor"].ToString();
                        oDoctor.nombres = dr["nombres"].ToString();
                        oDoctor.primer_apellido = dr["primer_apellido"].ToString();
                        oDoctor.segundo_apellido = dr["segundo_apellido"].ToString();
                        oDoctor.telefono_doctor = dr["telefono_doctor"].ToString();
                        oDoctor.correo_doctor = dr["correo_doctor"].ToString();
                        oDoctor.id_tipo_documento = Convert.ToInt32(dr["id_tipo_documento"].ToString());
                        oDoctor.id_genero = Convert.ToInt32(dr["id_genero"].ToString());
                        oDoctor.id_especialidad = Convert.ToInt32(dr["id_especialidad"].ToString());
                        oDoctor.id_usuario = Convert.ToInt32(dr["id_usuario"].ToString());
                        DoctorList.Add(oDoctor);

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
            oDoctorRes.oDoctor = DoctorList;
            oDoctorRes.oHeader = header;
            return oDoctorRes;
        }
        public Header RegistrarDoctores(Doctor enti)
        {
            Header header = new Header();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_registrar_Doctor", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni_doctor", enti.dni_doctor);
                    cmd.Parameters.AddWithValue("@nombres", enti.nombres);
                    cmd.Parameters.AddWithValue("@primer_apellido", enti.primer_apellido);
                    cmd.Parameters.AddWithValue("@segundo_apellido", enti.segundo_apellido);
                    cmd.Parameters.AddWithValue("@telefono_doctor", enti.telefono_doctor);
                    cmd.Parameters.AddWithValue("@correo_doctor", enti.correo_doctor);
                    cmd.Parameters.AddWithValue("@id_tipo_documento", enti.id_tipo_documento);
                    cmd.Parameters.AddWithValue("@id_genero", enti.id_genero);
                    cmd.Parameters.AddWithValue("@id_especialidad", enti.id_especialidad);
                    cmd.Parameters.AddWithValue("@id_usuario", enti.id_usuario);
                    int respuesta = cmd.ExecuteNonQuery();

                    if (respuesta > 0)
                    {
                        header.estado = true;
                        header.mensaje = "se ha registrado un doctor";
                    }
                    else
                    {
                        header.estado = false;
                        header.mensaje = "doctor se encuentra registrado";
                    }

                    cn.Close();
                    cn.Dispose();

                }
            }
            catch (Exception ex)
            {
                header.estado = false;
                header.mensaje = ex.Message;
            }
       
            return header;
        }
    }
}