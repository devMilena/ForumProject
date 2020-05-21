using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumProject.Models.ForumModels.ForumViewModels
{
    public class DiscussionPageViewModel
    {
        public Discussion Discussion { get; set; }
        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; }
        public List<UserPostLike> UsersPostLikes { get; set; }
        public List<UserPostDislike> UsersPostDislikes { get; set; }


    }
}