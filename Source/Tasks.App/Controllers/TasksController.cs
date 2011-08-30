using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Read;
using Tasks.Read.Models;
using Tasks.Write;
using Tasks.Write.Commands;

namespace Tasks.App.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly CommandExecutor _executor;

        public TasksController(CommandExecutor executor)
        {
            _executor = executor;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = ReadStorage.GetUserIdByEmail(User.Identity.Name);

            _executor.Execute(new CreateTask(model.Title, userId));

            return RedirectToAction("Index", "Contexts");
        }

        public ActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNote(NoteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = ReadStorage.GetUserIdByEmail(User.Identity.Name);

            _executor.Execute(new CreateNote(model.Title, model.Description, userId));

            return RedirectToAction("Index", "Contexts");
        }

        public ActionResult CompleteTask(Guid id)
        {
            var userId = ReadStorage.GetUserIdByEmail(User.Identity.Name);

            _executor.Execute(new CompleteTask(id, userId));

            return RedirectToAction("Index", "Contexts");
        }
    }
}