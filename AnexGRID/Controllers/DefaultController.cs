using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnexGRID.Controllers
{
    public class DefaultController : Controller
    {
        private Empleado empleado = new Empleado();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Obtener(int id)
        {
            return Json(empleado.Obtener(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar(Model.AnexGRID agrid)
        {
            return Json(empleado.Listar(agrid), JsonRequestBehavior.AllowGet);
        }
    }
}