using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Cita_Medicas.Entity;
using Web_Cita_Medicas.Logica;

namespace Web_Cita_Medicas.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            if (Session["PI_USUARIO"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Login(Usuario enti)
        {
            LO_Usuario lo = new LO_Usuario();
            var resLogin = lo.LoginUsuario(enti);
            if (resLogin.oHeader.estado)
            {
                Session["PI_USUARIO"] = resLogin.oUsuario;
                Session["PI_USERNAME"] = resLogin.oUsuario.nombres;
                Session["PI_ROL"] = resLogin.oUsuario.id_rol;
                Session["PI_FILE"] = "";
                FormsAuthentication.SetAuthCookie(resLogin.oUsuario.correo, false);
            }

            return Json(resLogin,JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Registrar(Usuario enti)
        {
            LO_Usuario lo = new LO_Usuario();
            var resLogin = lo.registrarUsuarioMaster(enti);
            return Json(resLogin, JsonRequestBehavior.AllowGet);
        }


        public ActionResult logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Auth");
        }
    }
}