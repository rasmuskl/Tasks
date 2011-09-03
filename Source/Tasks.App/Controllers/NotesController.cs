using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Read;
using Tasks.Read.Queries;
using Tasks.Write;
using Tasks.Write.Commands;

namespace Tasks.App.Controllers
{
    public class NotesController : Controller
    {
        private readonly CommandExecutor _executor;

        public NotesController(CommandExecutor executor)
        {
            _executor = executor;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NoteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));

            _executor.Execute(new CreateNote(model.Title, model.Description, userId));

            return RedirectToAction("Index", "Contexts");
        }

    }
}