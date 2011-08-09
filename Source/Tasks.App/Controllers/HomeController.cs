using System.Web.Mvc;

namespace Tasks.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Tasks");
        }
    }
}
