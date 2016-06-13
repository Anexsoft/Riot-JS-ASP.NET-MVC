using System.Web.Mvc;

namespace AnexGRID.Controllers
{
    public class ComponentController : Controller
    {
        // GET: Component
        public ActionResult Index(string c)
        {
            return View(string.Format("~/views/components/{0}.cshtml", c));
        }
    }
}