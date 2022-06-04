using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Security;
using System.Data.SqlClient;

namespace Web_Cita_Medicas.Logica
{
    public class LO_Paciente
    {
        public PacienteRes ListarPacientes(int id_pac)
        {
            PacienteRes oPacienteRes = new PacienteRes();
            Header header = new Header();
            List<Paciente> PacienteList = new List<Paciente>();
            try
            {
                using(SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_Paciente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_paciente", id_pac);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Paciente oPaciente = new Paciente();
                        oPaciente.id_paciente = Convert.ToInt32(dr["id_paciente"].ToString());
                        oPaciente.dni_paciente = dr["dni_paciente"].ToString();
                        oPaciente.nombres = dr["nombres"].ToString();
                        oPaciente.primer_apellido = dr["primer_apellido"].ToString();
                        oPaciente.segundo_apellido = dr["segundo_apellido"].ToString();
                        oPaciente.telefono_paciente = dr["telefono_paciente"].ToString();
                        oPaciente.correo_paciente = dr["correo_paciente"].ToString();
                        oPaciente.id_tipo_documento = Convert.ToInt32(dr["id_tipo_documento"].ToString());
                        oPaciente.id_genero = Convert.ToInt32(dr["id_genero"].ToString());
                        oPaciente.id_usuario = Convert.ToInt32(dr["id_usuario"].ToString());

                        PacienteList.Add(oPaciente);
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
            oPacienteRes.oPaciente = PacienteList;
            oPacienteRes.oHeader = header;
            return oPacienteRes;
        }

        public PacienteRegister RegistrarPacientes(Paciente enti)
        {
            PacienteRegister oRegister = new PacienteRegister();
            Header header = new Header();
            int id_registro = 0;
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_registrar_Paciente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_paciente", enti.id_paciente);
                    cmd.Parameters.AddWithValue("@dni_paciente", enti.dni_paciente);
                    cmd.Parameters.AddWithValue("@nombres", enti.nombres);
                    cmd.Parameters.AddWithValue("@primer_apellido", enti.primer_apellido);
                    cmd.Parameters.AddWithValue("@segundo_apellido", enti.segundo_apellido);
                    cmd.Parameters.AddWithValue("@telefono_paciente", enti.telefono_paciente);
                    cmd.Parameters.AddWithValue("@correo_paciente", enti.correo_paciente);
                    cmd.Parameters.AddWithValue("@id_tipo_documento", enti.id_tipo_documento);
                    cmd.Parameters.AddWithValue("@id_genero", enti.id_genero);
                    cmd.Parameters.AddWithValue("@id_usuario", enti.id_usuario);
                    int respuesta = cmd.ExecuteNonQuery();

                    if (respuesta > 0)
                    {
                        id_registro = respuesta;
                        header.estado = true;
                        if (enti.id_paciente == 0)
                        {

                            header.mensaje = "se ha registrado el paciente";
                        }
                        else
                        {
                            header.mensaje = "se ha actualizado el paciente";
                        }
                    }
                    else
                    {
                        header.estado = false;
                        header.mensaje = "paciente se encuentra registrado";
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
            oRegister.oHeader = header;
            oRegister.id_register = id_registro;
            return oRegister;
        }
    }
}