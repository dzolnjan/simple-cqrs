using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cqrs.Core.Command;
using Cqrs.Core.Infrastructure;
using Cqrs.Core.Query;

namespace Cqrs.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuerier _querier;
        private readonly ICommander _commander;

        public HomeController(IQuerier querier, ICommander commander)
        {
            _commander = commander;
            _querier = querier;
        }

        public async Task<ActionResult> Index()
        {
            var commandResult = await _commander.Execute(new AddOrEditBlog() { Name = "blog123" });

            //await _commander.Execute(new AddWelcomePost()
            //{
            //    Id = commandResult.Blog.BlogId,
            //    PostTitle = "This is the welcome post created from event handler from Home"
            //});

            var query = new GetBlogsByName() { Name = "blog"};

            var queryResult = await _querier.Execute(query);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}