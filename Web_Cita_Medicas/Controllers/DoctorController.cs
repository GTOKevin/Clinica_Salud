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

        [HttpPost]
        public JsonResult RegistrarDoctor(Doctor enti)
        {
            LO_Doctor ldoctor = new LO_Doctor();
            DoctorRes oDoctorRes = new DoctorRes();

            enti.id_usuario = ((Usuario)Session["PI_USUARIO"]).id_usuario;

            var res = ldoctor.RegistrarDoctores(enti);
            if (res.oHeader.estado)
            {
                oDoctorRes.oHeader = res.oHeader;
                var doctor = ldoctor.ListarDoctores(res.id_register);
                if (doctor.oHeader.estado)
                {
                    oDoctorRes.oDoctor = doctor.oDoctor;
                }
            }
            else
            {
                oDoctorRes.oHeader = res.oHeader;
            }

            return Json(oDoctorRes, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JsonResult ActualizarDoctor(Doctor enti)
        //{
        //    LO_Doctor ldoctor = new LO_Doctor();
        //    DoctorRes oDoctorRes = new DoctorRes();
        //    var res = ldoctor.ActualizarDoctores(enti);

        //    if (res.oHeader.estado)
        //    {
        //        var doctor = ldoctor.ListarDoctores(res.id_register);
        //        if (doctor.oHeader.estado)
        //        {
        //            oDoctorRes.oDoctor = doctor.oDoctor;
        //        }
        //    }
        //    else
        //    {
        //        oDoctorRes.oHeader = res.oHeader;
        //    }

        //    return Json(oDoctorRes, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult CambiarEstado(int id, bool estado)
        {
            LO_Doctor ldoctor = new LO_Doctor();
            Header oHeader = new Header();
            if (estado)
            {
                oHeader = ldoctor.CambiarEstado(id, false);
            }
            else
            {

                oHeader = ldoctor.CambiarEstado(id, true);
            }

            return Json(oHeader, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarDoctor(int id=0)
        {
            LO_Doctor loDoctor = new LO_Doctor();
            var resDoc = loDoctor.ListarDoctores(id);

            return Json(resDoc, JsonRequestBehavior.AllowGet);
        }
    }
}