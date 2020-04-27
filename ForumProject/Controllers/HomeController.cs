using ForumProject.Models.ForumModels.ForumViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumProject.Models;

namespace ForumProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.Discussions = context.Discussions.ToList();
            model.Categories = context.Categories.ToList();
            foreach (var discuss in model.Discussions)
            {
                ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == discuss.UserId);
                discuss.ApplicationUser.UserName = user.UserName;
            }
            return View(model);
        }
        [HttpGet]
        //public ActionResult Discussion(int discussionId)
        //{
        //    DiscussionPageViewModel model = new DiscussionPageViewModel();
        //    model.Discussion = context.Discussions.FirstOrDefault(d => d.DiscussionId == discussionId);

        //    return View();
        //}

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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(disposing)
            {
                context.Dispose();
            }
        }

    }
}