using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cita_Medicas.Logica;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Models;

namespace Web_Cita_Medicas.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            ViewDoctorModel model = new ViewDoctorModel();
            LO_Genero lGenero = new LO_Genero();
            LO_Tipo_Documento lTipo_Documento = new LO_Tipo_Documento();
            LO_Especialidad lEspecialidad = new LO_Especialidad();

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

            var resEspecialidad = lEspecialidad.ListarEspecialidades();

            if (resEspecialidad.oHeader.estado)
            {
                model.ListaEspecialidad = resEspecialidad.oEspecialidad;
            }


            
            return View(model);
        }
        public JsonResult RegistrarDoctor(Doctor enti)
        {
            LO_Doctor ldoctor = new LO_Doctor();

            enti.id_usuario = ((Usuario)Session["PI_USUARIO"]).id_usuario;

            var res = ldoctor.RegistrarDoctores(enti);

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarDoctor()
        {
            LO_Doctor loDoctor = new LO_Doctor();
            var resDoc = loDoctor.ListarDoctores();

            return Json(resDoc, JsonRequestBehavior.AllowGet);
        }
    }
}