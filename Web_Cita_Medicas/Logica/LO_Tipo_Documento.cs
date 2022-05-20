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
    public class LO_Tipo_Documento
    {


        public Tipo_DocumentoRes ListarTipo_Documentoes()
        {
            Tipo_DocumentoRes Tipo_DocumentoRes = new Tipo_DocumentoRes();
            Header header = new Header();
            List<Tipo_Documento> Tipo_DocumentoList = new List<Tipo_Documento>();
            try
            {
                using (SqlConnection cn = Conexion.Conectar())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_listar_tipo_documento", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Tipo_Documento oTipo_Documento = new Tipo_Documento();
                        oTipo_Documento.id_tipo_documento = Convert.ToInt32(dr["id_tipo_documento"].ToString());
                        oTipo_Documento.nombre_documento = dr["nombre_documento"].ToString();
                        Tipo_DocumentoList.Add(oTipo_Documento);
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
            Tipo_DocumentoRes.oTipo_Documento = Tipo_DocumentoList;
            Tipo_DocumentoRes.oHeader = header;
            return Tipo_DocumentoRes;
        }
    }
}