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
        public ActionResult Index(int  categoryId=0 )
        {
            HomePageViewModel model = new HomePageViewModel();
            
            model.Categories = context.Categories.ToList();
           
            if (categoryId == 0)
                model.Discussions = context.Discussions.ToList();
            else
                model.Discussions = context.Discussions.Where(d => d.CategoryId == categoryId).ToList();
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

            discussmodel.Discussion.UsersDiscussionLikes = context.UsersDiscussionLikes.Where(l => l.DiscussionId == discussionId).ToList();
            discussmodel.Discussion.UsersDiscussionDislikes = context.UsersDiscussionDislikes.Where(l => l.DiscussionId == discussionId).ToList();

            var userId = User.Identity.GetUserId();
            discussmodel.Discussion.DiscussLikedByUser = (discussmodel.Discussion.UsersDiscussionLikes.Where(e => e.UserId == userId)).Any();
            discussmodel.Discussion.DiscussDislikedByUser = (discussmodel.Discussion.UsersDiscussionDislikes.Where(e => e.UserId == userId)).Any();
            discussmodel.Discussion.DiscussionCreatedByUser = discussmodel.Discussion.UserId == userId;
  
            foreach (var post in discussmodel.Posts)
            {
                // Add user data
                user = context.Users.FirstOrDefault(u => u.Id == post.UserId);
                post.ApplicationUser.UserName = user.UserName;
                post.ApplicationUser.PostsCount = user.PostsCount;
                post.ApplicationUser.DiscussionsCount = user.DiscussionsCount;

                // Add likes and dislikes
                post.UsersPostLikes = context.UsersPostLikes.Where(l => l.PostId == post.PostId).ToList();
                post.UsersPostDislikes = context.UsersPostDislikes.Where(l => l.PostId == post.PostId).ToList();

                post.LikedByUser = (post.UsersPostLikes.Where(e => e.UserId == userId)).Any();
                post.DislikedByUser = (post.UsersPostDislikes.Where(e => e.UserId == userId)).Any();
                post.PostedByUser = post.UserId == userId;
            }

            return View(discussmodel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DiscussionLike(int discussionId)
        {
            var userId = User.Identity.GetUserId();

            var like = context.UsersDiscussionLikes.SingleOrDefault(l => l.DiscussionId == discussionId && l.UserId == userId);
            if (like != null)
                context.UsersDiscussionLikes.Remove(like);
            else
            {
                like = new UserDiscussionLike() { DiscussionId = discussionId, UserId = userId };
                context.UsersDiscussionLikes.Add(like);

                var dislike = context.UsersDiscussionDislikes.SingleOrDefault(l => l.DiscussionId == discussionId && l.UserId == userId);
                if (dislike != null)
                    context.UsersDiscussionDislikes.Remove(dislike);
            }

            context.SaveChanges();

            return RedirectToAction("Discussion", new { discussionId = discussionId });
        }

        [HttpPost]
        [Authorize]
        public ActionResult DiscussionDislike(int discussionId)
        {
            var userId = User.Identity.GetUserId();

            var dislike = context.UsersDiscussionDislikes.SingleOrDefault(l => l.DiscussionId == discussionId && l.UserId == userId);
            if (dislike != null)
                context.UsersDiscussionDislikes.Remove(dislike);
            else
            {
                dislike = new UserDiscussionDislike() { DiscussionId = discussionId, UserId = userId };
                context.UsersDiscussionDislikes.Add(dislike);

                var like = context.UsersDiscussionLikes.SingleOrDefault(l => l.DiscussionId == discussionId && l.UserId == userId);
                if (like != null)
                    context.UsersDiscussionLikes.Remove(like);
            }

            context.SaveChanges();

            return RedirectToAction("Discussion", new { discussionId = discussionId });
        }

        [HttpPost]
        [Authorize]
        public ActionResult PostLike(int postId, int discussionId)
        {
            var userId = User.Identity.GetUserId();

            var like = context.UsersPostLikes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);
            if (like != null)
                context.UsersPostLikes.Remove(like);
            else
            {
                like = new UserPostLike() { PostId = postId, UserId = userId };
                
                context.UsersPostLikes.Add(like);

                var dislike = context.UsersPostDislikes.SingleOrDefault(l => l.PostId==postId && l.UserId == userId);
                if (dislike != null)
                    context.UsersPostDislikes.Remove(dislike);
            }


            context.SaveChanges();
            return RedirectToAction("Discussion", new { discussionId = discussionId });
        }

        [HttpPost]
        [Authorize]
        public ActionResult PostDislike(int postId, int discussionId)
        {
            var userId = User.Identity.GetUserId();

            var dislike = context.UsersPostDislikes.SingleOrDefault(l => l.PostId == postId &&  l.UserId == userId);
            if (dislike != null)
                context.UsersPostDislikes.Remove(dislike);
            else
            {
                dislike = new UserPostDislike() { PostId=postId, UserId = userId };
                context.UsersPostDislikes.Add(dislike);

                var like = context.UsersPostLikes.SingleOrDefault(l => l.PostId == postId && l.UserId == userId);
                if (like != null)
                    context.UsersPostLikes.Remove(like);
            }
            context.SaveChanges();
            return RedirectToAction("Discussion", new {  discussionId = discussionId});
        }
        public ActionResult DeletePost(int discussionId, int postId)
        {
            Post post = context.Posts.SingleOrDefault(p => p.PostId == postId);
            post.UserId = User.Identity.GetUserId();
            post.DiscussionId = discussionId;
           
            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == post.UserId);
            user.PostsCount = user.PostsCount - 1;

            Discussion discussion = context.Discussions.SingleOrDefault(d => d.DiscussionId == post.DiscussionId);
            discussion.PostCount = discussion.PostCount - 1;
 
            context.Posts.Remove(post);

            context.SaveChanges();
            return RedirectToAction("Discussion", new { discussionId = discussionId });
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
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussion(CreateDiscussionViewModel model)
        {
            if (!ModelState.IsValid)
            {

                model.Categories = context.Categories.ToList();
                return View(model);
            }
               
            else
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
        }

        public ActionResult DeleteDiscussion(int discussionId, int categoryId)
        {
           
            Discussion discussion = context.Discussions.SingleOrDefault(d => d.DiscussionId == discussionId);
            discussion.UserId = User.Identity.GetUserId();
            discussion.CategoryId = categoryId;

            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == discussion.UserId);
            user.DiscussionsCount = user.DiscussionsCount - 1;

            Category category = context.Categories.SingleOrDefault(d => d.CategoryId == discussion.CategoryId);
            category.DiscussionsCount = category.DiscussionsCount - 1;
            context.Discussions.Remove(discussion);

            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreatePost(int discussionId)
        {
            CreatePostViewModel model = new CreatePostViewModel();
            model.Discussion = context.Discussions.FirstOrDefault(d => d.DiscussionId == discussionId);
            
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreatePost(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)

            return View(model); 
          else      
            {

                Post newPost = new Post();

                newPost.Text = model.Text;
                newPost.DiscussionId = model.DiscussionId;
                newPost.UserId = User.Identity.GetUserId();
                newPost.CreatedDate = DateTime.Now;
                
                
                context.Posts.Add(newPost);
                ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == newPost.UserId);
                user.PostsCount = user.PostsCount + 1;

                Discussion discussion = context.Discussions.SingleOrDefault(d => d.DiscussionId == newPost.DiscussionId);
                discussion.PostCount = discussion.PostCount + 1;
                
                context.SaveChanges();
               
                return RedirectToAction("Discussion", new { discussionid = newPost.DiscussionId});
            }
         
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Milena Jelcic.";

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