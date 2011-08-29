using System;
using System.Web.Mvc;
using Tasks.Read;

namespace Tasks.App.Controllers
{
    public class ContextsController : Controller
    {
        public ActionResult MenuList()
        {
            Guid userId = ReadStorage.GetUserIdByEmail(User.Identity.Name);
            return View(ReadStorage.GetContextsByUserId(userId));
        }

        public ActionResult Index(string id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}