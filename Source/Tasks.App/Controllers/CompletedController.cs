using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tasks.Read;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.App.Controllers
{
    public class CompletedController : Controller
    {
        public ActionResult Index()
        {
            Guid userId = ReadStorage.Query(new QueryUserIdByEmail(User.Identity.Name));
            IEnumerable<TaskReadModel> completedTasks = ReadStorage.Query(new QueryRecentlyCompletedTasks(userId));

            return View(completedTasks);
        }
    }
}