using System;
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


        public ActionResult EditDescription(Guid id)
        {
            var model = new NoteEditDescriptionModel();

            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            var readModel = ReadStorage.Query(new QueryNoteById(userId, id));

            model.Title = readModel.Title;
            model.Description = readModel.DescriptionRaw;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDescription(Guid id, NoteEditDescriptionModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }



            return RedirectToAction("Index", "Contexts");
        }
    }
}