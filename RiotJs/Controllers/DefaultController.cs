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
        private Profesion profesion = new Profesion();

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

        public JsonResult Profesiones()
        {
            return Json(profesion.Todo(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Registrar(List<Empleado> model)
        {
            var r = true;

            try {
                empleado.Registrar(model);
            }
            catch (Exception e)
            {
                r = false;
            }

            return Json(r);
        }
    }
}