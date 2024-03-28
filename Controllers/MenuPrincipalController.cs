using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class MenuPrincipalController : Controller
    {
        // GET: MenuPrincipal
        public ActionResult Index()
        {
            return View();
        }
    }
}