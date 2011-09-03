using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tasks.App.Models;
using Tasks.Read;
using Tasks.Read.Models;
using Tasks.Read.Queries;
using Tasks.Write;
using Tasks.Write.Commands;

namespace Tasks.App.Controllers
{
    public class ContextsController : Controller
    {
        private readonly CommandExecutor _executor;

        public ContextsController(CommandExecutor executor)
        {
            _executor = executor;
        }

        public ActionResult MenuList()
        {
            Guid userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            return View(ReadStorage.Query(new QueryContextsByUserId(userId)));
        }

        public ActionResult Index(string id)
        {
            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            Guid contextId = ReadStorage.GetContextIdByName(userId, id);

            IEnumerable<TaskReadModel> tasks = ReadStorage.GetTasksByContextId(userId, contextId);
            IEnumerable<NoteReadModel> notes = ReadStorage.GetNotesByContextId(userId, contextId);
            IEnumerable<ContextReadModel> otherContexts = ReadStorage.GetContextsExceptContextId(userId, contextId);

            return View(new ContextIndexModel { Tasks = tasks, Notes = notes, OtherContexts = otherContexts });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContextCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));

            if(ReadStorage.Query(new QueryContextsByUserId(userId)).Any(x => string.Equals(x.Name, model.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                ModelState.AddModelError("Name", "You already have a context by this name.");
            }

            _executor.Execute(new CreateContext(Guid.NewGuid(), model.Name, userId));

            return RedirectToAction("Index", new { id = model.Name });
        }
    }
}