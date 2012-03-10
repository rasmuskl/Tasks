using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Read;
using Tasks.Read.Queries;
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

        [HttpPost]
        public ActionResult Create(TaskCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));

            var createCommand = new CreateTask(model.Title, userId, model.ContextId);
            _executor.Execute(createCommand);

            if(model.PrevTaskId != Guid.Empty)
            {
                var prioritizeCommand = new PrioritizeTask(userId, createCommand.TaskId, model.PrevTaskId, false, DateTime.UtcNow);
                _executor.Execute(prioritizeCommand);
            }

            return Json(createCommand.TaskId);
        }

        public ActionResult CompleteTask(Guid id)
        {
            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));

            _executor.Execute(new CompleteTask(id, userId));

            if(Request.IsAjaxRequest())
            {
                return Json(true);
            }

            return RedirectToAction("Index", "Contexts");
        }

        public ActionResult MoveTaskToContext(Guid targetContextId, Guid taskId, Guid fromContextId)
        {
            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            var context = ReadStorage.Query(new QueryContextById(userId, fromContextId));

            _executor.Execute(new MoveTaskToContext(taskId, userId, targetContextId));

            return RedirectToAction("Index", "Contexts", new { id = context.Name });
        }
    }
}