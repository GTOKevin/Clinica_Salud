using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Logica;
using Web_Cita_Medicas.Models;

namespace Web_Cita_Medicas.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index()
        {
            ViewPacienteModel model = new ViewPacienteModel();
            LO_Genero lGenero = new LO_Genero();
            LO_Tipo_Documento lTipo_Documento = new LO_Tipo_Documento();

            var respGenero = lGenero.ListarGeneros();
            if (respGenero.oHeader.estado)
            {
                model.ListaGenero = respGenero.oGenero;
            }

            var respTipo = lTipo_Documento.ListarTipo_Documentoes();

            if (respTipo.oHeader.estado)
            {
                model.ListaTipoDocumento = respTipo.oTipo_Documento;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult RegistrarPaciente(Paciente enti)
        {
            LO_Paciente lpaciente = new LO_Paciente();
            PacienteRes oPacienteRes = new PacienteRes();

            enti.id_usuario = ((Usuario)Session["PI_USUARIO"]).id_usuario;

            var res = lpaciente.RegistrarPacientes(enti);
            if (res.oHeader.estado)
            {
                oPacienteRes.oHeader = res.oHeader;
                var paciente = lpaciente.ListarPacientes(res.id_register);
                if (paciente.oHeader.estado)
                {
                    oPacienteRes.oPaciente = paciente.oPaciente;
                }
            }
            else
            {
                oPacienteRes.oHeader = res.oHeader;
            }
            return Json(oPacienteRes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPaciente(int id = 0)
        {
            LO_Paciente loPaciente = new LO_Paciente();
            var resPaciente = loPaciente.ListarPacientes(id);

            return Json(resPaciente, JsonRequestBehavior.AllowGet);
        }
    }
}