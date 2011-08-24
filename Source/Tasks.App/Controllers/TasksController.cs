using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Read;
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

        public ActionResult Index()
        {
            return View(new TasksIndexModel { Tasks = ReadStorage.Tasks, Notes = ReadStorage.Notes });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            _executor.Execute(new CreateTask(model.Title));

            return RedirectToAction("Index");
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

            _executor.Execute(new CreateNote(model.Title, model.Description));

            return RedirectToAction("Index");
        }
    }
}