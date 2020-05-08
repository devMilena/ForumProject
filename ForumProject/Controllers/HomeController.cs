using ForumProject.Models.ForumModels.ForumViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumProject.Models;
using ForumProject.Models.ForumModels;
using Microsoft.AspNet.Identity;

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
        public ActionResult Discussion(int discussionId)
        {
            DiscussionPageViewModel discussmodel = new DiscussionPageViewModel();
            discussmodel.Discussion = context.Discussions.FirstOrDefault(d => d.DiscussionId == discussionId);
            discussmodel.Categories = context.Categories.ToList();

            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == discussmodel.Discussion.UserId);
            discussmodel.Discussion.ApplicationUser = new ApplicationUser();
            
            discussmodel.Discussion.ApplicationUser.UserName = user.UserName;
            discussmodel.Discussion.ApplicationUser.PostsCount = user.PostsCount;
            discussmodel.Discussion.ApplicationUser.DiscussionsCount = user.DiscussionsCount;
            discussmodel.Posts = context.Posts.Where(p => p.DiscussionId == discussionId).ToList();
            foreach (var post in discussmodel.Posts)
            {
                user = context.Users.FirstOrDefault(u => u.Id == post.UserId);
                post.ApplicationUser.UserName = user.UserName;
                post.ApplicationUser.PostsCount = user.PostsCount;
                post.ApplicationUser.DiscussionsCount = user.DiscussionsCount;
            }

            return View(discussmodel);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Discussion(int discussId, string post)
        {
            Post newPost = new Post();
            newPost.Text = post;
            newPost.DiscussionId = discussId;
            newPost.UserId = User.Identity.GetUserId();
            newPost.CreatedDate = DateTime.Now;
            context.Posts.Add(newPost);
            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == newPost.UserId);
            user.PostsCount = user.PostsCount + 1;
            Discussion discussion = context.Discussions.SingleOrDefault(d => d.DiscussionId == newPost.DiscussionId);
            discussion.PostCount = discussion.PostCount + 1;
            context.SaveChanges();
            return RedirectToAction("Discussion",new { discussionId = discussId });
        }
        [HttpGet]
        public ActionResult CreateDiscussion()
        {
            CreateDiscussionViewModel model = new CreateDiscussionViewModel();
            model.Categories = context.Categories.ToList();
            return View(model);

        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateDiscussion(CreateDiscussionViewModel model)
        {
            Discussion newDiscussion = new Discussion();
            newDiscussion.UserId = User.Identity.GetUserId();
            newDiscussion.Title = model.Title;
            newDiscussion.CategoryId = model.CategoryId;
            newDiscussion.Description = model.Description;
            newDiscussion.CreatedDate = DateTime.Now;
            context.Discussions.Add(newDiscussion);
          

            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == newDiscussion.UserId);
            user.DiscussionsCount = user.DiscussionsCount + 1;
            Category category = context.Categories.SingleOrDefault(c => c.CategoryId == newDiscussion.CategoryId);
            category.DiscussionsCount = category.DiscussionsCount + 1;


            context.SaveChanges();
            return RedirectToAction("Index");
          
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