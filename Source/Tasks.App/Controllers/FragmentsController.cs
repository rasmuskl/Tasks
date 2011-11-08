using System;
using System.Web.Mvc;
using Tasks.Read;
using Tasks.Read.Queries;
using Tasks.Write;
using Tasks.Write.Commands;

namespace Tasks.App.Controllers
{
    public class FragmentsController : Controller
    {
        readonly CommandExecutor _executor;

        public FragmentsController(CommandExecutor executor)
        {
            _executor = executor;
        }

        public ActionResult Index()
        {
            Guid userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            var readModels = ReadStorage.Query(new QueryFragmentsByUserId(userId));
            return View(readModels);
        }

        public ActionResult CreateFragment(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                // TODO Better validation :-)
                return View("Index");
            }

            Guid userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            _executor.Execute(new CreateFragment(text, userId));

            return RedirectToAction("Index");
        }
    }
}