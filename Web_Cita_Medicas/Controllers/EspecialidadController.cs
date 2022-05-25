using System.Web.Mvc;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Logica;

namespace Web_Cita_Medicas.Controllers
{
    public class EspecialidadController : Controller
    {
        // GET: Especialidad
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RegistrarEspecialidad(Especialidad enti)
        {
            LO_Especialidad ldoctor = new LO_Especialidad();
            var res = ldoctor.RegistrarEspecialidades(enti);

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListarEspecialidad()
        {
            LO_Especialidad loEspecialidad = new LO_Especialidad();
            var resEsp = loEspecialidad.ListarEspecialidades();

            return Json(resEsp, JsonRequestBehavior.AllowGet);
        }
    }
}