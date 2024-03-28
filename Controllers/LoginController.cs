using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using Proyecto.Models;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();

        }

        public ActionResult CerrarSession()
        {
            Session["Usuario"] = null;
            Session["Rol"] = null;
            return RedirectToAction("Index");

        }
        [HttpPost]

        public ActionResult Login(string Nombre, string Password)
        {



            if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Password))

            {



                using (var bd = new controlvisitanteEntities3())
                {

                    var user = bd.Usuario.FirstOrDefault(e => e.nombre == Nombre && e.password == Password);

                    if (user != null)

                    {

                        FormsAuthentication.SetAuthCookie(user.nombre, true);


                        return RedirectToAction("Index", "MenuPrincipal");


                    }

                    else
                    {

                        return RedirectToAction("Index", new { message = "Usuario - Contraseña es Incorrecto" });

                    }

                }
            }
            else

            {
                return RedirectToAction("Index", new { message = "Debe Ingresar Todos los Datos Correctamente" });


            }


        }
        [Authorize]

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            return RedirectToAction("Index");

        }

    }
}



    
