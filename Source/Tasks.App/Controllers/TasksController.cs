using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Model;
using Tasks.Model.CommandHandlers;
using Tasks.Model.Commands;

namespace Tasks.App.Controllers
{
    public class TasksController : Controller
    {
        private CommandBus _commandBus;

        public TasksController()
        {
            _commandBus = new CommandBus();
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

            _commandBus.Handle(new CreateTaskCommand(model.Task));

            return RedirectToAction("Index");
        }
    }
}