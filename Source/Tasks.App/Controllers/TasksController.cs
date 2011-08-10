using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Model;
using Tasks.Model.Commands;

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
            return View(new TasksIndexModel { Tasks = Storage.Tasks });
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

            _executor.Execute(new CreateTaskCommand(model.Task));

            return RedirectToAction("Index");
        }
    }
}