using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Cita_Medicas.Entity
{
    public class PacienteRes
    {
        public Header oHeader { get; set; }
        public List<Paciente> oPaciente { get; set; }
    }

    public class PacienteRegister
    {
        public Header oHeader { get; set; }
        public int id_register { get; set; }
    }
    public class Paciente
    {
        public int id_paciente { get; set; }
        public string dni_paciente { get; set; }
        public string nombres { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string telefono_paciente { get; set; }
        public string correo_paciente { get; set; }
        public int id_tipo_documento { get; set; }
        public int id_genero { get; set; }
        public int id_usuario { get; set; }
    }

//    create proc sp_listar_Paciente
//@id_paciente int  
//as    
// if @id_paciente=0  
//  begin
//    select* from PACIENTE
// end
// else  
//  begin
//   select* from PACIENTE where id_paciente=@id_paciente
// end
// go


//create proc sp_registrar_Paciente
//@id_paciente int,
//@dni_paciente varchar(20),    
//@nombres varchar(30),    
//@primer_apellido varchar(30),    
//@segundo_apellido varchar(50),    
//@telefono_paciente varchar(9),    
//@correo_paciente varchar(50),    
//@id_tipo_documento int,    
//@id_genero int,       
//@id_usuario int    
//as    
// declare @id int  
// if @id_paciente =0
//	begin
//		 if not exists(select* from PACIENTE where dni_paciente= @dni_paciente and id_tipo_documento = @id_tipo_documento)

//          begin
//           insert into PACIENTE(dni_paciente, nombres, primer_apellido, segundo_apellido, telefono_paciente, correo_paciente,
//                id_tipo_documento, id_genero, id_usuario)

//             values(@dni_paciente, @nombres, @primer_apellido, @segundo_apellido, @telefono_paciente, @correo_paciente,
//             @id_tipo_documento, @id_genero, @id_usuario)


//         set @id = SCOPE_IDENTITY()


//          end
//    end
//  else
//	begin
//         update PACIENTE
//          set

//          dni_paciente=@dni_paciente,  
//		  nombres=@nombres,  
//		  primer_apellido=@primer_apellido,  
//		  segundo_apellido=@segundo_apellido,  
//		  telefono_paciente=@telefono_paciente,  
//		  correo_paciente=@correo_paciente,  
//		  id_tipo_documento=@id_tipo_documento,  
//		  id_genero=@id_genero
//          where id_paciente=@id_paciente

//          set @id=@id_paciente
//    end
//   select @id
// go
}